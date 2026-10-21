# C1.8 Azure Storage: Blob, Files, Taşıma ve Migration

> 📁 `06-certifications/01-az-900-azure-fundamentals/08-storage/` · Sınav alanı: **Describe Azure architecture and services (%35-40)** · Önkoşul: C1.4 (region, zone, region pair) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe Azure storage services* — storage servislerini karşılaştır · storage tier'ları · redundancy seçenekleri · storage account seçenekleri ve türleri · dosya taşıma seçenekleri (**AzCopy, Azure Storage Explorer, Azure File Sync**) · migration seçenekleri (**Azure Migrate, Azure Data Box**). Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

## İçindekiler

1. [Ne?](#1-ne)
2. [Neden?](#2-neden)
3. [Nasıl?](#3-nasıl)
4. [Gerçek dünya senaryosu](#4-gerçek-dünya-senaryosu)
5. [Sınav perspektifi](#5-sınav-perspektifi)
6. [Yaygın hatalar](#6-yaygın-hatalar)
7. [Performans & trade-off](#7-performans--trade-off)
8. [Kaynaklar](#8-kaynaklar)

---

## 1. Ne?

**Azure Storage**, Microsoft'un yönetilen bulut depolama platformudur: dayanıklı (birden çok kopya), ölçeklenebilir, güvenli (varsayılan olarak şifreli) ve HTTP/HTTPS ile her yerden erişilebilir. Tüm servisler bir **storage account** altında yaşar.

Bu konu sınavın beş sorusunu cevaplar:

| Soru | Kavram |
|------|--------|
| Ne tür veri, hangi servise? | Blob · Files · Queue · Table · Disks |
| Veri hangi arızaya dayanmalı? | **Redundancy**: LRS, ZRS, GRS, GZRS, RA-GRS, RA-GZRS ⭐ |
| Ne sıklıkla erişilecek? | **Access tier**: Hot, Cool, Cold, Archive ⭐ |
| Dosyaları nasıl taşırım/senkronlarım? | AzCopy · Storage Explorer · Azure File Sync |
| Sunucuları/büyük veriyi nasıl taşırım? | Azure Migrate · Azure Data Box |

Kod: modül projesinde [`Sections/`](Sections/) altında 5 bölüm; 4. bölüm **Azurite** emülatörüne (Azure hesabı gerekmez) gerçek Blob Storage çağrıları yapar. Modül klasöründe (`06-certifications/01-az-900-azure-fundamentals/`):

```bash
docker compose up -d          # Azurite (Blob 10000, Queue 10001, Table 10002)
dotnet run -- 1.8 all         # tüm bölümler
dotnet run -- 1.8 4           # yalnızca Blob demosu
docker compose down -v        # emülatörü durdur, veriyi sil
```

> Gerçek bir storage account'a karşı çalıştırmak için `AZURE_STORAGE_CONNECTION_STRING` ortam değişkenini tanımla; tanımlı değilse demo `UseDevelopmentStorage=true` (Azurite) kullanır.

---

## 2. Neden?

- **Sınav:** Storage, "Azure architecture and services" alanının (%35-40) en çok soru üreten konularındandır; özellikle **redundancy ↔ senaryo** ve **tier ↔ erişim sıklığı** eşleştirmeleri hemen her sınavda çıkar.
- **Gerçek işte:** Yanlış redundancy seçimi ya felakette veri kaybı (LRS'te datacenter felaketi) ya da gereksiz maliyettir (her şeyi RA-GZRS yapmak). Yanlış tier ise ya yüksek depolama faturası (yedekleri Hot'ta tutmak) ya da sürpriz erişim/rehydrate ücretleri ve saatlerce bekleme (sık okunan veriyi Archive'a koymak) demektir.
- **Taşıma:** Datacenter'dan buluta geçişte ilk soru "terabaytlarca veriyi nasıl taşıyacağız?" sorusudur; ağ, cihaz ve senkron seçeneklerini bilmek taşıma planının temelidir.

---

## 3. Nasıl?

### 3.1 Storage account ve storage servisleri ([`Section01`](Sections/Section01_StorageServices.cs))

**Storage account:** Azure genelinde **benzersiz** bir ad alanı (3-24 karakter, küçük harf + rakam); her servis bu adla bir endpoint alır:

| Servis | Ne saklar? | Endpoint | Tipik kullanım |
|---|---|---|---|
| **Blob** | Nesne — yapılandırılmamış veri | `https://<ad>.blob.core.windows.net` | Görsel, video, yedek, log, statik web sitesi, veri gölü |
| **Azure Files** | Tam yönetilen **SMB/NFS dosya paylaşımı** | `https://<ad>.file.core.windows.net` | Paylaşılan klasör, lift-and-shift uygulamalar |
| **Queue** | Basit mesaj kuyruğu (mesaj ≤ 64 KB) | `https://<ad>.queue.core.windows.net` | Bileşenler arası asenkron iş listesi |
| **Table** | NoSQL anahtar-değer (şemasız) | `https://<ad>.table.core.windows.net` | Basit yapılandırılmış veri, meta veri |
| **Disks** (managed disks) | VM'e bağlanan **blok** depolama | — (VM'e bağlı kaynak) | VM işletim sistemi ve veri diskleri (→ C1.5) |

**Storage account türleri:**

| Tür | Desteklediği servisler | Redundancy | Ne zaman? |
|---|---|---|---|
| **Standard general-purpose v2** | Blob, Files, Queue, Table | LRS, ZRS, GRS, RA-GRS, GZRS, RA-GZRS | **Çoğu senaryo** — varsayılan seçim |
| **Premium block blobs** | Block ve append blob | LRS, ZRS | Düşük gecikme, yüksek işlem hızı, küçük nesneler |
| **Premium file shares** | Azure Files | LRS, ZRS | Kurumsal/yüksek performanslı dosya paylaşımı |
| **Premium page blobs** | Page blob | LRS, ZRS | Yüksek performanslı page blob senaryoları |

> Premium türler SSD tabanlıdır ve **coğrafi** (GRS/GZRS) redundancy sunmaz.

**Blob hiyerarşisi:** storage account → **container** → **blob**. Blob türleri: **block** (dosyalar; en yaygın), **append** (sona ekleme — log), **page** (rastgele okuma/yazma — VM diskleri).

### 3.2 Redundancy ⭐ ([`Section02`](Sections/Section02_Redundancy.cs))

Azure Storage veriyi her zaman **birden çok kopya** olarak tutar; seçenek, kopyaların **nereye** konduğunu belirler:

| Seçenek | Kopyalar | Disk/raf arızası | Datacenter/zone arızası | Bölgesel felaket | Felakette okuma |
|---|---|---|---|---|---|
| **LRS** (locally redundant) | Birincil bölgede **tek datacenter** içinde 3 kopya | ✔ | ✘ | ✘ | ✘ |
| **ZRS** (zone-redundant) | Birincil bölgede **3 availability zone**'a | ✔ | ✔ | ✘ | ✘ |
| **GRS** (geo-redundant) | LRS + **eşleşik bölgede** LRS (6 kopya) | ✔ | — (failover gerekir) | ✔ (failover ile) | Failover sonrası |
| **GZRS** (geo-zone-redundant) | ZRS + eşleşik bölgede LRS | ✔ | ✔ | ✔ (failover ile) | Failover sonrası |
| **RA-GRS** | GRS + ikincilden **her zaman okuma** | ✔ | — | ✔ | ✔ ikincilden |
| **RA-GZRS** | GZRS + ikincilden **her zaman okuma** | ✔ | ✔ | ✔ | ✔ ikincilden |

- İkincil bölge her zaman birincilin **region pair**'idir (→ C1.4); ikincile replikasyon **asenkrondur** (felakette son birkaç saniyenin/dakikanın verisi kaybolabilir).
- GRS/GZRS'te ikincil bölge **normalde okunamaz**; ancak **failover** (müşteri başlatır veya Microsoft) sonrası yeni birincil olur. Sürekli okuma istiyorsan **RA-** (read-access) seçeneği gerekir.
- Maliyet sırası (kabaca): LRS < ZRS < GRS < RA-GRS ≈ GZRS < RA-GZRS.

### 3.3 Blob Storage ve access tier'ları ⭐ ([`Section03`](Sections/Section03_AccessTiers.cs))

| Tier | Kullanım | Önerilen en az süre | Erişim | Maliyet dengesi |
|---|---|---|---|---|
| **Hot** | Sık erişilen veri (aktif içerik) | — | Anında | Depolama en pahalı, erişim en ucuz |
| **Cool** | Seyrek erişim (kısa vadeli yedek, eski içerik) | **30 gün** | Anında | Depolama ucuz, erişim daha pahalı |
| **Cold** | Nadir erişim ama hızlı okunmalı | **90 gün** | Anında | Depolama daha ucuz, erişim daha pahalı |
| **Archive** | Çok nadir (uzun dönem arşiv, uyumluluk kayıtları) | **180 gün** | **Çevrimdışı** — önce **rehydrate** (saatler sürebilir) | Depolama en ucuz, erişim en pahalı |

- **Archive'daki blob okunamaz:** önce Hot/Cool/Cold'a taşınması (rehydrate) gerekir; standart öncelikte **saatler** (en çok ~15 saat), yüksek öncelikte daha kısa sürer ve daha pahalıdır.
- En az süre dolmadan silmek veya tier değiştirmek **erken silme ücreti** doğurur.
- Tier **blob düzeyinde** ayarlanabilir; hesap düzeyinde bir varsayılan tier vardır (Archive varsayılan olamaz).
- **Lifecycle management:** kurallarla otomatik geçiş — "30 gün değişmeyen blob → Cool, 180 gün → Archive, 7 yıl → sil".

`Section03` 1 TB veri için üç farklı okuma yoğunluğunda aylık maliyeti hesaplar (örnek fiyatlar): az okunan veride Archive, orta yoğunlukta Cool, çok okunan veride Hot kazanır — **depolama ucuzladıkça erişim pahalılaşır**.

### 3.4 Blob demosu — Azurite ile ([`Section04`](Sections/Section04_BlobDemo.cs))

`Azure.Storage.Blobs` SDK'sıyla, Azure hesabı olmadan:

1. Container oluşturma (`CreateIfNotExists`)
2. Üç blob yükleme (`raporlar/…`, `yedekler/…`, `log/…` — `/` ile "sanal klasör")
3. Listeleme (boyut + access tier)
4. İndirme
5. Eski yedeği **Cool → Archive**'a taşıma ve Archive'daki blob'u okuma denemesinin **409 BlobArchived** ile reddedildiğini görme
6. Temizlik (container'ı silme)

```text
5) yedekler/db-2026-09.bak → Cool
5) yedekler/db-2026-09.bak → Archive
   Archive'daki blob'u okuma denemesi reddedildi: 409 BlobArchived
```

> Azurite kapalıysa bölüm açıklayıcı bir mesajla biter (çıkış kodu 2). Modülün `docker-compose.yml`'i Azurite'i `--skipApiVersionCheck` ile başlatır: yeni SDK sürümleri emülatörün henüz tanımadığı bir REST API sürümü isteyebilir.

### 3.5 Azure Files ve Azure File Sync

- **Azure Files:** bulutta tam yönetilen **SMB** (ve Premium'da **NFS**) dosya paylaşımı; Windows, Linux ve macOS'tan **ağ sürücüsü olarak bağlanır**; birden çok VM/kullanıcı aynı paylaşımı kullanır. Kullanım: dosya sunucusunu buluta taşımak, lift-and-shift uygulamaların paylaşılan klasörleri, paylaşılan yapılandırma/log klasörleri.
- **Azure File Sync:** on-prem **Windows Server**'ı Azure Files paylaşımının **önbelleği** yapar:
  - Kullanıcılar yerel sunucudan hızlı erişmeye devam eder; veri Azure Files ile **iki yönlü senkron** kalır.
  - **Cloud tiering:** seyrek kullanılan dosyalar yalnızca bulutta tutulur, yerelde yer açılır (dosya adı görünür, açılınca indirilir).
  - Birden çok şubede aynı paylaşım; sunucu arızalanınca yeni sunucuya hızlı kurtarma.

### 3.6 Dosya taşıma araçları ([`Section05`](Sections/Section05_MovingData.cs))

| Araç | Ne? | Ne zaman? |
|---|---|---|
| **AzCopy** | Blob ve dosya kopyalamak için **komut satırı** aracı | Betikli/toplu kopyalama, `sync`, storage account'lar ve bulutlar (ör. S3) arası kopya |
| **Azure Storage Explorer** | Blob, Files, Queue, Table, disk yönetimi için **grafik** masaüstü uygulaması (Windows/macOS/Linux; arka planda AzCopy) | Elle dosya yükleme/indirme, göz atma, erişim yönetimi |
| **Azure File Sync** | Windows Server ↔ Azure Files **sürekli, iki yönlü senkron** | Şube dosya sunucuları, hybrid dosya paylaşımı |

### 3.7 Migration seçenekleri

- **Azure Migrate:** buluta taşıma için **merkezi hub**: on-prem ortamı **keşfet** (discovery) → **değerlendir** (boyutlandırma, uyumluluk, tahmini maliyet) → **taşı**. Sunucular, veritabanları, web uygulamaları ve sanal masaüstleri için entegre araçlar içerir: *Discovery and assessment*, *Migration and modernization* (sunucu taşıma), *Data Migration Assistant*, *Azure Database Migration Service*, *Web app migration assistant*, Data Box. Ayrıca on-prem ile Azure maliyetini karşılaştıran **business case** üretir (→ C1.11).
- **Azure Data Box:** veriyi **fiziksel cihazla** taşıma — Microsoft güvenli bir cihaz gönderir, veriyi yerelde kopyalarsın, cihaz Azure datacenter'ına döner ve veri storage account'a yüklenir; cihaz sonra NIST standartlarına göre güvenle silinir. Hem **içe** (on-prem → Azure) hem **dışa** (Azure → on-prem) aktarım.
  - Ne zaman: **çok büyük veri** (onlarca TB – petabayt), **ağ bağlantısı yok/yavaş/pahalı**, tek seferlik toplu taşıma, ilk yükleme sonrası ağdan artımlı senkron, felaket kurtarma için toplu geri yükleme.
  - Ailesi *(bağlam — kapasiteler değişir)*: **Data Box Disk** (SSD diskler, küçük-orta veri), **Data Box** (yeni nesil cihazlar ~120 TB ve ~525 TB kullanılabilir kapasite; eski 80 TB'lık cihaz bölge bölge kullanımdan kalkıyor), **Data Box Heavy** emekliye ayrıldı. Sınav kapasite değil **"ağ yetersiz + büyük veri → Data Box"** eşleştirmesini sorar.

Kaba hesap: 100 Mbps'lik bir hatla 40 TB, hattın tamamı bu işe ayrılsa bile **~37 gün** sürer; Data Box birkaç günlük kargo ve kopyalama süresiyle biter.

### 3.8 Pratik — CLI ve AzCopy laboratuvarı

Önce bütçe alarmının kurulu olduğundan emin ol (→ C1.1). Küçük bir Standard LRS hesabı ve birkaç KB'lık dosya **kuruşluk** maliyettir; laboratuvar sonunda her şey silinir.

```bash
RG=rg-az900-c18
ST=stc18$RANDOM                         # Azure genelinde benzersiz olmalı
az group create --name $RG --location westeurope

# 1) Storage account (Standard general-purpose v2, LRS)
az storage account create --resource-group $RG --name $ST \
  --location westeurope --sku Standard_LRS --kind StorageV2 --access-tier Hot
az storage account show --resource-group $RG --name $ST \
  --query "{sku:sku.name, kind:kind, tier:accessTier, blob:primaryEndpoints.blob}" -o table

# 2) Container + blob upload + tier değiştirme (--auth-mode key: CLI hesap anahtarını kendisi alır)
az storage container create --account-name $ST --name yedekler --auth-mode key
echo "ornek yedek" > yedek.txt
az storage blob upload --account-name $ST --container-name yedekler --name yedek.txt --file yedek.txt --auth-mode key
az storage blob set-tier --account-name $ST --container-name yedekler --name yedek.txt --tier Cool --auth-mode key
az storage blob list --account-name $ST --container-name yedekler --auth-mode key \
  --query "[].{ad:name, tier:properties.blobTier}" -o table

# 3) AzCopy: yerel klasörü container'a kopyala (kısa ömürlü SAS ile)
mkdir -p rapor && for i in 1 2 3; do echo "rapor $i" > rapor/r$i.txt; done
SAS=$(az storage container generate-sas --account-name $ST --name yedekler \
  --permissions acdlrw --expiry $(date -u -d '+1 hour' +%Y-%m-%dT%H:%MZ) --auth-mode key -o tsv)
azcopy copy "./rapor" "https://$ST.blob.core.windows.net/yedekler?$SAS" --recursive
azcopy list "https://$ST.blob.core.windows.net/yedekler?$SAS"

# 4) Azure Files paylaşımı (5 GiB kota)
az storage share-rm create --resource-group $RG --storage-account $ST --name paylasim --quota 5
az storage share-rm list --resource-group $RG --storage-account $ST -o table
# Bağlama: portalda paylaşımın "Connect" sekmesi Windows/Linux/macOS için hazır betik üretir
# (SMB 445 portu birçok ev/şirket ağında kapalıdır — bağlanamazsan sebebi bu olabilir).

# 5) Temizlik
az group delete --name $RG --yes --no-wait
```

> **Alıştırma — redundancy ve maliyet.** Pricing Calculator'da aynı Standard general-purpose v2 hesabını (1 TB, Hot, aylık 1 milyon okuma) **LRS** ve **GZRS** olarak fiyatla. (a) Fark yaklaşık kaç kat? (b) Hangi senaryo için GZRS'in ek maliyeti haklıdır?
>
> <details><summary>Çözüm</summary>
>
> (a) Bölgeye göre değişir; GZRS depolama fiyatı LRS'in kabaca **2 katı** civarındadır (ikinci bölgedeki kopyalar + zone yayılımı; ayrıca coğrafi replikasyon için veri aktarım ücreti).
> (b) Verinin hem bir **zone arızasında kesintisiz** erişilebilir olması hem de **bölgesel felakette kaybolmaması** gerekiyorsa (ör. müşteri belgeleri, finansal kayıtlar). Yeniden üretilebilen veri (önbellek, geçici işlem çıktısı) için LRS yeterlidir.
> </details>

---

## 4. Gerçek dünya senaryosu

Bir sigorta şirketi on yıllık dosya arşivini ve dosya sunucularını buluta taşıyor:

| İhtiyaç | Karar | Neden |
|---|---|---|
| 300 TB taranmış poliçe arşivi, ofisin hattı 200 Mbps | **Azure Data Box** (birden çok cihaz) | Ağdan aylar sürer; cihazla birkaç hafta |
| Arşiv yalnızca denetimde okunuyor, 10 yıl saklanmalı | Blob **Archive** tier + lifecycle kuralı | En ucuz depolama; denetimden önce rehydrate için saatler yeterli |
| Son 1 yılın poliçeleri müşteri portalında gösteriliyor | Blob **Hot** (eski olanlar lifecycle ile **Cool**'a) | Sık okunan veri pahalı erişim ücretine girmemeli |
| Müşteri belgeleri hem zone arızasında erişilebilir hem bölgesel felakette korunmalı; felakette okunabilmeli | **RA-GZRS** | Zone + bölge koruması + ikincilden okuma |
| 12 şubedeki Windows dosya sunucuları | **Azure Files** + **Azure File Sync** (cloud tiering) | Şubelerde hızlı yerel erişim, tek kaynak bulutta, yer tasarrufu |
| Sunucuların hangisi nasıl taşınacak, maliyeti ne? | **Azure Migrate** keşif + değerlendirme | Boyutlandırma ve maliyet tahmini taşımadan önce |
| Gece raporlarının blob'a betikle yüklenmesi | **AzCopy** (`azcopy sync`) | Komut satırı, zamanlanmış iş |

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Neredeyse tamamen **senaryo → seçenek** eşleştirmesi: redundancy, tier, servis ve taşıma aracı. Evet/hayır tablolarında Archive ve redundancy kuralları sık çıkar.

| Senaryodaki ifade | Cevap |
|---|---|
| "En düşük maliyetli redundancy", "tek datacenter yeterli" | **LRS** |
| "Bir zone/datacenter düşse de erişim sürsün, veri bölgeden çıkmasın" | **ZRS** |
| "Bölgesel felakete dayan" | **GRS** (zone koruması da isteniyorsa **GZRS**) |
| "Birincil bölge çökse bile veriyi okuyabilmeliyim" | **RA-GRS / RA-GZRS** |
| "Sık erişilen veri" | **Hot** |
| "30 gün+ saklanan, seyrek erişilen yedek" | **Cool** |
| "Yıllarca saklanacak, nadiren ve gecikmeyle okunabilecek veri" | **Archive** |
| "Archive'daki dosyayı hemen okumak" | **Mümkün değil** — önce rehydrate |
| "Yapılandırılmamış veri: görsel, video, yedek" | **Blob** |
| "SMB ile bağlanan paylaşılan klasör" | **Azure Files** |
| "On-prem dosya sunucusu bulutla senkron, sık dosyalar yerelde" | **Azure File Sync** |
| "Komut satırından blob kopyalama" | **AzCopy** |
| "Grafik araçla blob yönetimi" | **Azure Storage Explorer** |
| "Taşıma öncesi keşif, değerlendirme, maliyet" | **Azure Migrate** |
| "Terabaytlarca veri, ağ yetersiz/yok" | **Azure Data Box** |

**Tuzaklar:**
- GRS ile RA-GRS: GRS'te ikincil bölge failover olmadan **okunamaz**.
- ZRS bölgesel felakete dayanmaz; GRS zone arızasında (failover yapılana kadar) kesintisiz erişim sağlamaz — ikisi birden isteniyorsa GZRS.
- File Sync ile AzCopy: sürekli senkron mu, tek seferlik kopya mı?
- Azure Migrate bir **platform/hub**; Data Box bir **cihaz**. "Sunucuları değerlendir" sorusunda Data Box yanlıştır.
- Storage account adı yalnızca küçük harf ve rakam; Azure genelinde benzersiz.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "LRS 3 kopya tutuyor, felakete dayanır" | 3 kopya **aynı datacenter**'da; datacenter felaketinde veri kaybolabilir |
| "GRS ile ikincil bölgeden her zaman okurum" | Yalnızca **RA-**GRS/RA-GZRS'te; GRS'te failover gerekir |
| Sık okunan veriyi "ucuz" diye Archive'a koymak | Archive çevrimdışı; rehydrate saatler sürer ve erişim/rehydrate pahalıdır |
| Cool/Archive'daki veriyi birkaç gün sonra silmek | Erken silme ücreti (30/90/180 gün kuralları) |
| "Azure Files = Blob'a klasör yüklemek" | Azure Files SMB/NFS ile **bağlanan** dosya sistemi; Blob nesne deposu |
| "File Sync tek seferlik taşıma aracı" | Sürekli, iki yönlü senkron + cloud tiering; tek seferlik kopya için AzCopy |
| "Data Box ile sunucu taşınır" | Data Box **veri** taşır; sunucu taşıma Azure Migrate'in işi |
| Premium hesapta GRS aramak | Premium türler yalnızca LRS/ZRS |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| LRS → ZRS | Zone arızasında kesintisiz erişim | Daha yüksek fiyat; tüm bölgelerde/türlerde yok |
| ZRS → GZRS / RA-GZRS | Bölgesel felakete dayanıklılık (+ ikincilden okuma) | ~2× depolama, coğrafi replikasyon trafiği, asenkron replikasyon (RPO > 0) |
| Hot → Cool/Cold | Düşük depolama maliyeti | Daha pahalı okuma/işlem, erken silme ücreti |
| Cool → Archive | En düşük depolama maliyeti | Çevrimdışı; saatlerce rehydrate, pahalı erişim |
| Standard → Premium | Düşük gecikme, yüksek IOPS | Daha yüksek GB fiyatı, coğrafi redundancy yok |
| Ağdan taşıma (AzCopy) → Data Box | Büyük veride çok daha hızlı, ağ yükü yok | Kargo ve kopyalama süresi, cihaz ücreti; sürekli senkron değil |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure storage services](https://learn.microsoft.com/en-us/training/modules/describe-azure-storage-services/)
- [Storage account overview](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-overview) · [Azure Storage redundancy](https://learn.microsoft.com/en-us/azure/storage/common/storage-redundancy)
- [Access tiers for blob data](https://learn.microsoft.com/en-us/azure/storage/blobs/access-tiers-overview) · [Blob rehydration from the archive tier](https://learn.microsoft.com/en-us/azure/storage/blobs/archive-rehydrate-overview)
- [What is Azure Files?](https://learn.microsoft.com/en-us/azure/storage/files/storage-files-introduction) · [Azure File Sync](https://learn.microsoft.com/en-us/azure/storage/file-sync/file-sync-introduction)
- [Get started with AzCopy](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azcopy-v10) · [Azure Storage Explorer](https://azure.microsoft.com/products/storage/storage-explorer/)
- [About Azure Migrate](https://learn.microsoft.com/en-us/azure/migrate/migrate-services-overview) · [Azure Data Box overview](https://learn.microsoft.com/en-us/azure/databox/data-box-overview) (yeni nesil 120/525 TB cihazlar) · [Data Box Heavy overview](https://learn.microsoft.com/en-us/azure/databox/data-box-heavy-overview) (emeklilik notu)
- [Use the Azurite emulator](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite) · [Azure.Storage.Blobs (NuGet)](https://www.nuget.org/packages/Azure.Storage.Blobs)
