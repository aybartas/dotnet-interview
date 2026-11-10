[← AZ-900 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../2.4-networking/README.md)

## 2.5 Azure Storage: Blob, Files, Redundancy, Tiering

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C1.8
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe Azure storage services* — storage servislerini karşılaştır · storage tier'ları · redundancy seçenekleri · storage account seçenekleri ve türleri · dosya taşıma seçenekleri (**AzCopy, Azure Storage Explorer, Azure File Sync**) · migration seçenekleri (**Azure Migrate, Azure Data Box**). Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

### 1. Ne?

**Azure Storage**, Microsoft'un yönetilen bulut depolama platformudur: dayanıklı (birden çok kopya), ölçeklenebilir, güvenli (varsayılan olarak şifreli) ve HTTP/HTTPS ile her yerden erişilebilir. Tüm servisler bir **storage account** altında yaşar.

Bu konu sınavın beş sorusunu cevaplar:

| Soru | Kavram |
|------|--------|
| Ne tür veri, hangi servise? | Blob · Files · Queue · Table · Disks |
| Veri hangi arızaya dayanmalı? | **Redundancy**: LRS, ZRS, GRS, GZRS, RA-GRS, RA-GZRS ⭐ |
| Ne sıklıkla erişilecek? | **Access tier**: Hot, Cool, Cold, Archive ⭐ |
| Dosyaları nasıl taşırım/senkronlarım? | AzCopy · Storage Explorer · Azure File Sync |
| Sunucuları/büyük veriyi nasıl taşırım? | Azure Migrate · Azure Data Box |


---

### 2. Neden?

- **Sınav:** Storage, "Azure architecture and services" alanının (%35-40) en çok soru üreten konularındandır; özellikle **redundancy ↔ senaryo** ve **tier ↔ erişim sıklığı** eşleştirmeleri hemen her sınavda çıkar.
- **Gerçek işte:** Yanlış redundancy seçimi ya felakette veri kaybı (LRS'te datacenter felaketi) ya da gereksiz maliyettir (her şeyi RA-GZRS yapmak). Yanlış tier ise ya yüksek depolama faturası (yedekleri Hot'ta tutmak) ya da sürpriz erişim/rehydrate ücretleri ve saatlerce bekleme (sık okunan veriyi Archive'a koymak) demektir.
- **Taşıma:** Datacenter'dan buluta geçişte ilk soru "terabaytlarca veriyi nasıl taşıyacağız?" sorusudur; ağ, cihaz ve senkron seçeneklerini bilmek taşıma planının temelidir.

---

### 3. Nasıl?

#### 3.1 Storage account ve storage servisleri

**Storage account:** Azure genelinde **benzersiz** bir ad alanı (3-24 karakter, küçük harf + rakam); her servis bu adla bir endpoint alır:

- **Blob**
  - Ne saklar?: Nesne — yapılandırılmamış veri
  - Endpoint: `https://<ad>.blob.core.windows.net`
  - Tipik kullanım: Görsel, video, yedek, log, statik web sitesi, veri gölü
- **Azure Files**
  - Ne saklar?: Tam yönetilen **SMB/NFS dosya paylaşımı**
  - Endpoint: `https://<ad>.file.core.windows.net`
  - Tipik kullanım: Paylaşılan klasör, lift-and-shift uygulamalar
- **Queue**
  - Ne saklar?: Basit mesaj kuyruğu (mesaj ≤ 64 KB)
  - Endpoint: `https://<ad>.queue.core.windows.net`
  - Tipik kullanım: Bileşenler arası asenkron iş listesi
- **Table**
  - Ne saklar?: NoSQL anahtar-değer (şemasız)
  - Endpoint: `https://<ad>.table.core.windows.net`
  - Tipik kullanım: Basit yapılandırılmış veri, meta veri
- **Disks** (managed disks)
  - Ne saklar?: VM'e bağlanan **blok** depolama
  - Endpoint: — (VM'e bağlı kaynak)
  - Tipik kullanım: VM işletim sistemi ve veri diskleri (→ C1.5)

**Storage account türleri:**

- **Standard general-purpose v2**
  - Desteklediği servisler: Blob, Files, Queue, Table
  - Redundancy: LRS, ZRS, GRS, RA-GRS, GZRS, RA-GZRS
  - Ne zaman?: **Çoğu senaryo** — varsayılan seçim
- **Premium block blobs**
  - Desteklediği servisler: Block ve append blob · Redundancy: LRS, ZRS
  - Ne zaman?: Düşük gecikme, yüksek işlem hızı, küçük nesneler
- **Premium file shares**
  - Desteklediği servisler: Azure Files · Redundancy: LRS, ZRS
  - Ne zaman?: Kurumsal/yüksek performanslı dosya paylaşımı
- **Premium page blobs**
  - Desteklediği servisler: Page blob · Redundancy: LRS, ZRS
  - Ne zaman?: Yüksek performanslı page blob senaryoları

> Premium türler SSD tabanlıdır ve **coğrafi** (GRS/GZRS) redundancy sunmaz.

**Blob hiyerarşisi:** storage account → **container** → **blob**. Blob türleri: **block** (dosyalar; en yaygın), **append** (sona ekleme — log), **page** (rastgele okuma/yazma — VM diskleri).

#### 3.2 Redundancy ⭐

Azure Storage veriyi her zaman **birden çok kopya** olarak tutar; seçenek, kopyaların **nereye** konduğunu belirler:

- **LRS** (locally redundant)
  - Kopyalar: Birincil bölgede **tek datacenter** içinde 3 kopya
  - Disk/raf arızası: ✔ · Datacenter/zone arızası: ✘ · Bölgesel felaket: ✘
  - Felakette okuma: ✘
- **ZRS** (zone-redundant)
  - Kopyalar: Birincil bölgede **3 availability zone**'a
  - Disk/raf arızası: ✔ · Datacenter/zone arızası: ✔ · Bölgesel felaket: ✘
  - Felakette okuma: ✘
- **GRS** (geo-redundant)
  - Kopyalar: LRS + **eşleşik bölgede** LRS (6 kopya)
  - Disk/raf arızası: ✔ · Datacenter/zone arızası: — (failover gerekir)
  - Bölgesel felaket: ✔ (failover ile) · Felakette okuma: Failover sonrası
- **GZRS** (geo-zone-redundant)
  - Kopyalar: ZRS + eşleşik bölgede LRS · Disk/raf arızası: ✔
  - Datacenter/zone arızası: ✔ · Bölgesel felaket: ✔ (failover ile)
  - Felakette okuma: Failover sonrası
- **RA-GRS**
  - Kopyalar: GRS + ikincilden **her zaman okuma**
  - Disk/raf arızası: ✔ · Datacenter/zone arızası: — · Bölgesel felaket: ✔
  - Felakette okuma: ✔ ikincilden
- **RA-GZRS**
  - Kopyalar: GZRS + ikincilden **her zaman okuma**
  - Disk/raf arızası: ✔ · Datacenter/zone arızası: ✔ · Bölgesel felaket: ✔
  - Felakette okuma: ✔ ikincilden

- İkincil bölge her zaman birincilin **region pair**'idir (→ C1.4); ikincile replikasyon **asenkrondur** (felakette son birkaç saniyenin/dakikanın verisi kaybolabilir).
- GRS/GZRS'te ikincil bölge **normalde okunamaz**; ancak **failover** (müşteri başlatır veya Microsoft) sonrası yeni birincil olur. Sürekli okuma istiyorsan **RA-** (read-access) seçeneği gerekir.
- Maliyet sırası (kabaca): LRS < ZRS < GRS < RA-GRS ≈ GZRS < RA-GZRS.

#### 3.3 Blob Storage ve access tier'ları ⭐

- **Hot**
  - Kullanım: Sık erişilen veri (aktif içerik) · Önerilen en az süre: —
  - Erişim: Anında
  - Maliyet dengesi: Depolama en pahalı, erişim en ucuz
- **Cool**
  - Kullanım: Seyrek erişim (kısa vadeli yedek, eski içerik)
  - Önerilen en az süre: **30 gün** · Erişim: Anında
  - Maliyet dengesi: Depolama ucuz, erişim daha pahalı
- **Cold**
  - Kullanım: Nadir erişim ama hızlı okunmalı
  - Önerilen en az süre: **90 gün** · Erişim: Anında
  - Maliyet dengesi: Depolama daha ucuz, erişim daha pahalı
- **Archive**
  - Kullanım: Çok nadir (uzun dönem arşiv, uyumluluk kayıtları)
  - Önerilen en az süre: **180 gün**
  - Erişim: **Çevrimdışı** — önce **rehydrate** (saatler sürebilir)
  - Maliyet dengesi: Depolama en ucuz, erişim en pahalı

- **Archive'daki blob okunamaz:** önce Hot/Cool/Cold'a taşınması (rehydrate) gerekir; standart öncelikte **saatler** (en çok ~15 saat), yüksek öncelikte daha kısa sürer ve daha pahalıdır.
- En az süre dolmadan silmek veya tier değiştirmek **erken silme ücreti** doğurur.
- Tier **blob düzeyinde** ayarlanabilir; hesap düzeyinde bir varsayılan tier vardır (Archive varsayılan olamaz).
- **Lifecycle management:** kurallarla otomatik geçiş — "30 gün değişmeyen blob → Cool, 180 gün → Archive, 7 yıl → sil".

`Section03` 1 TB veri için üç farklı okuma yoğunluğunda aylık maliyeti hesaplar (örnek fiyatlar): az okunan veride Archive, orta yoğunlukta Cool, çok okunan veride Hot kazanır — **depolama ucuzladıkça erişim pahalılaşır**.

#### 3.4 Azure Files ve Azure File Sync

- **Azure Files:** bulutta tam yönetilen **SMB** (ve Premium'da **NFS**) dosya paylaşımı; Windows, Linux ve macOS'tan **ağ sürücüsü olarak bağlanır**; birden çok VM/kullanıcı aynı paylaşımı kullanır. Kullanım: dosya sunucusunu buluta taşımak, lift-and-shift uygulamaların paylaşılan klasörleri, paylaşılan yapılandırma/log klasörleri.
- **Azure File Sync:** on-prem **Windows Server**'ı Azure Files paylaşımının **önbelleği** yapar:
  - Kullanıcılar yerel sunucudan hızlı erişmeye devam eder; veri Azure Files ile **iki yönlü senkron** kalır.
  - **Cloud tiering:** seyrek kullanılan dosyalar yalnızca bulutta tutulur, yerelde yer açılır (dosya adı görünür, açılınca indirilir).
  - Birden çok şubede aynı paylaşım; sunucu arızalanınca yeni sunucuya hızlı kurtarma.

#### 3.5 Dosya taşıma araçları

- **AzCopy**
  - Ne?: Blob ve dosya kopyalamak için **komut satırı** aracı
  - Ne zaman?: Betikli/toplu kopyalama, `sync`, storage account'lar ve bulutlar (ör. S3) arası kopya
- **Azure Storage Explorer**
  - Ne?: Blob, Files, Queue, Table, disk yönetimi için **grafik** masaüstü uygulaması (Windows/macOS/Linux; arka planda AzCopy)
  - Ne zaman?: Elle dosya yükleme/indirme, göz atma, erişim yönetimi
- **Azure File Sync**
  - Ne?: Windows Server ↔ Azure Files **sürekli, iki yönlü senkron**
  - Ne zaman?: Şube dosya sunucuları, hybrid dosya paylaşımı

#### 3.6 Migration seçenekleri

- **Azure Migrate:** buluta taşıma için **merkezi hub**: on-prem ortamı **keşfet** (discovery) → **değerlendir** (boyutlandırma, uyumluluk, tahmini maliyet) → **taşı**. Sunucular, veritabanları, web uygulamaları ve sanal masaüstleri için entegre araçlar içerir: *Discovery and assessment*, *Migration and modernization* (sunucu taşıma), *Data Migration Assistant*, *Azure Database Migration Service*, *Web app migration assistant*, Data Box. Ayrıca on-prem ile Azure maliyetini karşılaştıran **business case** üretir (→ C1.11).
- **Azure Data Box:** veriyi **fiziksel cihazla** taşıma — Microsoft güvenli bir cihaz gönderir, veriyi yerelde kopyalarsın, cihaz Azure datacenter'ına döner ve veri storage account'a yüklenir; cihaz sonra NIST standartlarına göre güvenle silinir. Hem **içe** (on-prem → Azure) hem **dışa** (Azure → on-prem) aktarım.
  - Ne zaman: **çok büyük veri** (onlarca TB – petabayt), **ağ bağlantısı yok/yavaş/pahalı**, tek seferlik toplu taşıma, ilk yükleme sonrası ağdan artımlı senkron, felaket kurtarma için toplu geri yükleme.
  - Ailesi *(bağlam — kapasiteler değişir)*: **Data Box Disk** (SSD diskler, küçük-orta veri), **Data Box** (yeni nesil cihazlar ~120 TB ve ~525 TB kullanılabilir kapasite; eski 80 TB'lık cihaz bölge bölge kullanımdan kalkıyor), **Data Box Heavy** emekliye ayrıldı. Sınav kapasite değil **"ağ yetersiz + büyük veri → Data Box"** eşleştirmesini sorar.

Kaba hesap: 100 Mbps'lik bir hatla 40 TB, hattın tamamı bu işe ayrılsa bile **~37 gün** sürer; Data Box birkaç günlük kargo ve kopyalama süresiyle biter.

#### 3.7 Pratik — CLI ve AzCopy laboratuvarı

Önce bütçe alarmının kurulu olduğundan emin ol (→ C1.1). Küçük bir Standard LRS hesabı ve birkaç KB'lık dosya **kuruşluk** maliyettir; laboratuvar sonunda her şey silinir.

```bash
RG=rg-az900-c18
ST=stc18$RANDOM                         # must be unique across Azure
az group create --name $RG --location westeurope

# 1) Storage account (Standard general-purpose v2, LRS)
az storage account create --resource-group $RG --name $ST \
  --location westeurope --sku Standard_LRS --kind StorageV2 --access-tier Hot
az storage account show --resource-group $RG --name $ST \
  --query "{sku:sku.name, kind:kind, tier:accessTier, blob:primaryEndpoints.blob}" -o table

# 2) Container + blob upload + change tier (--auth-mode key: the CLI fetches the account key itself)
az storage container create --account-name $ST --name backups --auth-mode key
echo "sample backup" > backup.txt
az storage blob upload --account-name $ST --container-name backups --name backup.txt --file backup.txt --auth-mode key
az storage blob set-tier --account-name $ST --container-name backups --name backup.txt --tier Cool --auth-mode key
az storage blob list --account-name $ST --container-name backups --auth-mode key \
  --query "[].{name:name, tier:properties.blobTier}" -o table

# 3) AzCopy: copy a local folder to the container (with a short-lived SAS)
mkdir -p reports && for i in 1 2 3; do echo "report $i" > reports/r$i.txt; done
SAS=$(az storage container generate-sas --account-name $ST --name backups \
  --permissions acdlrw --expiry $(date -u -d '+1 hour' +%Y-%m-%dT%H:%MZ) --auth-mode key -o tsv)
azcopy copy "./reports" "https://$ST.blob.core.windows.net/backups?$SAS" --recursive
azcopy list "https://$ST.blob.core.windows.net/backups?$SAS"

# 4) Azure Files share (5 GiB quota)
az storage share-rm create --resource-group $RG --storage-account $ST --name myshare --quota 5
az storage share-rm list --resource-group $RG --storage-account $ST -o table
# Mounting: the share's "Connect" tab in the portal generates a ready-made script for Windows/Linux/macOS
# (SMB port 445 is blocked on many home/corporate networks — if you cannot connect, this may be why).

# 5) Cleanup
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

### 4. Gerçek dünya senaryosu

Bir sigorta şirketi on yıllık dosya arşivini ve dosya sunucularını buluta taşıyor:

- **300 TB taranmış poliçe arşivi, ofisin hattı 200 Mbps**
  - Karar: **Azure Data Box** (birden çok cihaz)
  - Neden: Ağdan aylar sürer; cihazla birkaç hafta
- **Arşiv yalnızca denetimde okunuyor, 10 yıl saklanmalı**
  - Karar: Blob **Archive** tier + lifecycle kuralı
  - Neden: En ucuz depolama; denetimden önce rehydrate için saatler yeterli
- **Son 1 yılın poliçeleri müşteri portalında gösteriliyor**
  - Karar: Blob **Hot** (eski olanlar lifecycle ile **Cool**'a)
  - Neden: Sık okunan veri pahalı erişim ücretine girmemeli
- Müşteri belgeleri hem zone arızasında erişilebilir hem bölgesel felakette korunmalı; felakette okunabilmeli
  - Karar: **RA-GZRS**
  - Neden: Zone + bölge koruması + ikincilden okuma
- **12 şubedeki Windows dosya sunucuları**
  - Karar: **Azure Files** + **Azure File Sync** (cloud tiering)
  - Neden: Şubelerde hızlı yerel erişim, tek kaynak bulutta, yer tasarrufu
- **Sunucuların hangisi nasıl taşınacak, maliyeti ne?**
  - Karar: **Azure Migrate** keşif + değerlendirme
  - Neden: Boyutlandırma ve maliyet tahmini taşımadan önce
- **Gece raporlarının blob'a betikle yüklenmesi** — Karar: **AzCopy** (`azcopy sync`) · Neden: Komut satırı, zamanlanmış iş

---

### 5. Sınav perspektifi

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

### 6. Yaygın hatalar

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

### 7. Performans & trade-off

- **LRS → ZRS**
  - Kazanç: Zone arızasında kesintisiz erişim
  - Bedel: Daha yüksek fiyat; tüm bölgelerde/türlerde yok
- **ZRS → GZRS / RA-GZRS**
  - Kazanç: Bölgesel felakete dayanıklılık (+ ikincilden okuma)
  - Bedel: ~2× depolama, coğrafi replikasyon trafiği, asenkron replikasyon (RPO > 0)
- **Hot → Cool/Cold**
  - Kazanç: Düşük depolama maliyeti
  - Bedel: Daha pahalı okuma/işlem, erken silme ücreti
- **Cool → Archive**
  - Kazanç: En düşük depolama maliyeti
  - Bedel: Çevrimdışı; saatlerce rehydrate, pahalı erişim
- **Standard → Premium**
  - Kazanç: Düşük gecikme, yüksek IOPS
  - Bedel: Daha yüksek GB fiyatı, coğrafi redundancy yok
- **Ağdan taşıma (AzCopy) → Data Box**
  - Kazanç: Büyük veride çok daha hızlı, ağ yükü yok
  - Bedel: Kargo ve kopyalama süresi, cihaz ücreti; sürekli senkron değil

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure storage services](https://learn.microsoft.com/en-us/training/modules/describe-azure-storage-services/)
- [Storage account overview](https://learn.microsoft.com/en-us/azure/storage/common/storage-account-overview) · [Azure Storage redundancy](https://learn.microsoft.com/en-us/azure/storage/common/storage-redundancy)
- [Access tiers for blob data](https://learn.microsoft.com/en-us/azure/storage/blobs/access-tiers-overview) · [Blob rehydration from the archive tier](https://learn.microsoft.com/en-us/azure/storage/blobs/archive-rehydrate-overview)
- [What is Azure Files?](https://learn.microsoft.com/en-us/azure/storage/files/storage-files-introduction) · [Azure File Sync](https://learn.microsoft.com/en-us/azure/storage/file-sync/file-sync-introduction)
- [Get started with AzCopy](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azcopy-v10) · [Azure Storage Explorer](https://azure.microsoft.com/products/storage/storage-explorer/)
- [About Azure Migrate](https://learn.microsoft.com/en-us/azure/migrate/migrate-services-overview) · [Azure Data Box overview](https://learn.microsoft.com/en-us/azure/databox/data-box-overview) (yeni nesil 120/525 TB cihazlar) · [Data Box Heavy overview](https://learn.microsoft.com/en-us/azure/databox/data-box-heavy-overview) (emeklilik notu)

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
