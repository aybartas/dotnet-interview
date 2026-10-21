# C1.12 Azure Yönetişim ve Uyumluluk

> 📁 `06-certifications/01-az-900-azure-fundamentals/12-governance-compliance/` · Sınav alanı: **Describe Azure management and governance (%30-35)** · Önkoşul: [C1.4](../04-core-architecture/README.md) (hiyerarşi), [C1.9](../09-identity-access/README.md) (RBAC) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe features and tools in Azure for governance and compliance* — **Microsoft Purview** · **Azure Policy** · **resource lock**'lar. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

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

**Yönetişim (governance)**, bir kuruluşun kurallarını — güvenlik, maliyet, uyumluluk, adlandırma, veri — **ölçekte** uygulaması ve uygulandığını **denetleyebilmesidir**. Yüzlerce subscription ve binlerce kaynakta bu kuralları insan dikkatiyle korumak mümkün değildir; Azure bunun için birbirini tamamlayan araçlar sunar:

| Araç | Soru | Neyi kontrol eder? |
|---|---|---|
| **Management group / subscription / RG** (→ C1.4) | Kurallar **nereye** uygulanır? | Kapsam ve miras |
| **Azure RBAC** (→ C1.9) | **Kim** ne yapabilir? | Kullanıcının/kimliğin yetkisi |
| **Azure Policy** ⭐ | Kaynak **nasıl** olabilir? | Kaynağın özellikleri (bölge, SKU, tag, ayar) |
| **Resource lock** ⭐ | Kaynak **silinebilir/değiştirilebilir** mi? | Kazara silme/değişiklik |
| **Tag** (→ C1.11) | Kaynak **kime/neye** ait? | Meta veri, maliyet raporlama |
| **Microsoft Purview** | **Veri** nerede, ne içeriyor, kim kullanıyor? | Veri yönetişimi, risk ve uyumluluk |

Bu konu resmî hedefteki üç aracı — Policy, lock, Purview — derinlemesine işler. Konunun kodu yoktur; pratik, README'deki `az` CLI laboratuvarıdır (3.5).

---

## 2. Neden?

- **Sınav:** *Describe Azure management and governance* alanı sınavın **%30-35**'i; "Policy mi, RBAC mı, lock mı?" ayrımı bu alanın en klasik sorusudur.
- **Gerçek işte:** "Kimse ABD region'ında kaynak açmasın", "her kaynağın `costCenter` tag'i olsun", "üretim veritabanı yanlışlıkla silinmesin" — bu kuralların hepsi bir e-posta duyurusuyla değil, **platformun zorladığı** kurallarla korunur.
- **Uyumluluk:** Regülasyonlar (KVKK, GDPR, sektör standartları) "kuralı koydum" değil "kuralın uygulandığını **kanıtlayabiliyorum**" ister; Policy'nin compliance panosu ve Purview'un veri haritası bu kanıtı üretir.

---

## 3. Nasıl?

### 3.1 Genel çerçeve ve uyumluluk kaynakları

- Yönetişim araçları **kapsam hiyerarşisi** üzerinde çalışır: management group'a atanan bir politika veya lock, altındaki tüm subscription, resource group ve kaynaklara **miras** kalır (→ C1.4).
- *(bağlam)* Microsoft'un kendi uyumluluğunu gösteren kaynaklar: **Microsoft Service Trust Portal** (bağımsız denetim raporları, ISO/SOC belgeleri), **Microsoft Trust Center** (güvenlik, gizlilik ve uyumluluk yaklaşımı), **gizlilik bildirimi** ve **ürün koşulları**. Bunlar *Microsoft'un* uyumluluğunu belgeler; *senin* kaynaklarının uyumluluğu Policy ve Defender for Cloud (→ C1.10) ile izlenir.

### 3.2 Azure Policy ⭐

**Azure Policy**, kaynakların **özelliklerinin** kurumsal kurallara uygun olup olmadığını **değerlendirir** ve gerekirse **zorlar**. Değerlendirme, kaynak oluşturulurken/güncellenirken ve mevcut kaynaklar için periyodik olarak yapılır.

**Yapı taşları:**

| Kavram | Ne? | Örnek |
|---|---|---|
| **Policy definition** | Kural (JSON): hangi koşulda hangi etki | *Allowed locations*, *Require a tag and its value on resources* |
| **Initiative** (policy set) | Birlikte yönetilen tanımlar kümesi | Bir güvenlik benchmark'ı (ör. Microsoft cloud security benchmark), "etiketleme standardı" |
| **Assignment** | Tanımın/initiative'in bir **kapsama** atanması (+ parametreler, istisnalar) | *Allowed locations* → `rg-prod`, `listOfAllowedLocations = [westeurope]` |
| **Exemption** *(bağlam)* | Belirli bir kapsamı değerlendirmenin dışında tutma | Test aboneliği |
| **Remediation task** | Mevcut uyumsuz kaynakları düzeltme (Modify / DeployIfNotExists) | Eksik tag'i gruptan kopyalamak |

**Etkiler (effect)** — her tanımın tek bir etkisi vardır:

| Etki | Ne yapar? | Sınavdaki ipucu |
|---|---|---|
| **Audit** | Kaynağa izin verir, **uyumsuz** olarak raporlar | "uyar / raporla, engelleme" |
| **Deny** | Kurala aykırı oluşturma/güncellemeyi **reddeder** | "izin verme / engelle" |
| **Append** / **Modify** | İstek sırasında özellik/tag **ekler veya düzeltir** | "tag'i otomatik ekle" |
| **AuditIfNotExists** | İlişkili bir kaynak/ayar (ör. tanılama ayarı) yoksa raporlar | |
| **DeployIfNotExists** | İlişkili kaynak/ayar yoksa **dağıtır** | "eksikse otomatik kur" |
| **Disabled** | Tanımı devre dışı bırakır | |
| *(bağlam)* **DenyAction**, **Manual** | Belirli bir eylemi (ör. silme) engeller · uyumluluğu elle beyan | |

**Bir politikanın yaşam döngüsü:**

1. Built-in tanımı bul (ör. *Allowed locations*).
2. Kapsama **ata** (management group / subscription / resource group), parametreyi ver.
3. Aykırı **yeni** kaynak isteği **Deny** ile reddedilir (`RequestDisallowedByPolicy`).
4. **Mevcut** uyumsuz kaynaklar silinmez; **compliance** panosunda "Non-compliant" olarak listelenir.
5. Gerekirse **remediation task** ile düzeltilir (Modify/DeployIfNotExists etkili politikalar için).

**Policy ↔ RBAC:** RBAC **kullanıcının** ne yapabileceğini belirler ("Ali bu RG'de kaynak oluşturabilir"); Policy **kaynağın** nasıl olabileceğini belirler ("bu RG'deki kaynaklar yalnızca West Europe'ta olabilir"). İkisi birlikte çalışır: Owner rolündeki biri bile Deny politikasına aykırı kaynağı oluşturamaz.

### 3.3 Resource lock'lar ⭐

**Resource lock**, kaynakların **kazara silinmesini veya değiştirilmesini** önler. İki tür vardır:

| Tür | Portaldaki adı | Okuma | Değiştirme | Silme |
|---|---|---|---|---|
| **CanNotDelete** | **Delete** | ✅ | ✅ | ❌ |
| **ReadOnly** | **Read-only** | ✅ | ❌ | ❌ |

- Subscription, resource group veya tek kaynak düzeyinde uygulanır; alt kapsamlara **miras** kalır (RG'deki lock, içindeki tüm kaynakları korur).
- **Rol yetkisinden bağımsızdır:** Owner bile kilitli kaynağı silemez; önce lock'ı **kaldırması** gerekir (lock'ı kaldırmak için de `Microsoft.Authorization/locks/*` yetkisi gerekir — Owner ve User Access Administrator'da vardır).
- Lock **yönetim düzlemini** (control plane — Azure Resource Manager istekleri) korur, **veriyi** değil: CanNotDelete'li bir storage account silinemez ama içindeki blob'lar silinebilir (veri koruması için blob soft delete/versioning gibi özellikler gerekir).
- **ReadOnly yan etkileri** — beklenenden fazlasını engelleyebilir: ARM'daki POST işlemleri de engellendiği için storage account'ta **erişim anahtarlarını listeleme** (`listKeys`) başarısız olur; o kapsamda **RBAC rol ataması** yapılamaz; bazı portal ekranları çalışmaz. Bu yüzden ReadOnly dikkatle, çoğu zaman CanNotDelete tercih edilir.

### 3.4 Microsoft Purview

**Microsoft Purview**, kuruluşun **verisini** yönetmek, korumak ve uyumluluğunu sağlamak için bir **veri yönetişimi, risk ve uyumluluk** çözümleri ailesidir. Kimlik ya da maliyet aracı değildir; konusu **veridir**.

| Alan | Ne yapar? | Örnek |
|---|---|---|
| **Birleşik veri yönetişimi** (Data Map + Unified Catalog) | On-prem, multicloud ve SaaS veri kaynaklarını (Azure SQL, Data Lake, Amazon S3, Power BI…) **tarar**, meta verisini bir **veri haritasına** toplar; aranabilir **katalog**, veri **soy ağacı** (lineage), veri kalitesi ve erişim yönetimi | "Müşteri verisi hangi veritabanlarında, kim kullanıyor, nereden besleniyor?" |
| **Risk ve uyumluluk** (çoğunlukla Microsoft 365 verisi) | Hassas veriyi otomatik **keşif ve sınıflandırma** (kredi kartı, kimlik numarası), **bilgi koruma** (sensitivity label), **veri kaybı önleme** (DLP), kayıt/yaşam döngüsü yönetimi, denetim | "Kredi kartı numarası içeren e-posta dışarı gönderilmesin" |

- Purview'un Data Map ve katalog rolleri **meta veriye** erişim verir; alttaki verinin kendisine erişim vermez.
- Sınav düzeyinde akılda kalması gereken: **"tüm verinin haritası, sınıflandırma, soy ağacı, veri yönetişimi" → Purview**.

### 3.5 Pratik — Policy ve lock laboratuvarı (`az` CLI)

Maliyet: politika ve lock'lar **ücretsizdir**; laboratuvardaki tek ücretli kaynak boş bir storage account'tur (kuruşlar düzeyinde). Bütçe alarmın kurulu olsun (→ C1.1). Cloud Shell'de veya yerel `az` ile:

```bash
RG=rg-az900-c112
ST=stc112$RANDOM                      # storage account adı: küçük harf + rakam, global benzersiz
az group create --name $RG --location westeurope

# 1) Built-in "Allowed locations" tanımını bul
az policy definition list \
  --query "[?displayName=='Allowed locations'].{name:name, displayName:displayName}" -o table
# name: e56962a6-4747-49cd-b67b-bf8b01975c4c

# 2) RG kapsamına ata: yalnızca West Europe
az policy assignment create \
  --name allowed-locations-c112 \
  --display-name "C1.12 - yalnızca westeurope" \
  --policy e56962a6-4747-49cd-b67b-bf8b01975c4c \
  --scope $(az group show --name $RG --query id -o tsv) \
  --params '{ "listOfAllowedLocations": { "value": [ "westeurope" ] } }'

# 3) İzin verilmeyen bölgede kaynak dene → RequestDisallowedByPolicy ile reddedilmeli
#    (atamanın etkinleşmesi birkaç dakika sürebilir; hemen reddedilmezse biraz bekleyip tekrar dene)
az storage account create --name $ST --resource-group $RG --location eastus --sku Standard_LRS

# 4) İzin verilen bölgede aynı kaynak → başarılı
az storage account create --name $ST --resource-group $RG --location westeurope --sku Standard_LRS

# 5) CanNotDelete lock: grup silinemez
az lock create --name lock-nodelete --lock-type CanNotDelete --resource-group $RG
az lock list --resource-group $RG -o table
az group delete --name $RG --yes                     # → ScopeLocked hatası
az storage account update --name $ST --resource-group $RG --tags env=lab   # değişiklik serbest ✅

# 6) ReadOnly lock (kaynak düzeyinde): değiştirme ve anahtar listeleme de engellenir
az lock create --name lock-readonly --lock-type ReadOnly --resource-group $RG \
  --resource-name $ST --resource-type Microsoft.Storage/storageAccounts
az storage account update --name $ST --resource-group $RG --tags env=prod  # → ScopeLocked
az storage account keys list --account-name $ST --resource-group $RG       # → ScopeLocked (POST engeli)

# 7) Temizlik: önce lock'lar, sonra atama, sonra grup
az lock delete --name lock-readonly --resource-group $RG \
  --resource-name $ST --resource-type Microsoft.Storage/storageAccounts
az lock delete --name lock-nodelete --resource-group $RG
az policy assignment delete --name allowed-locations-c112 \
  --scope $(az group show --name $RG --query id -o tsv)
az group delete --name $RG --yes --no-wait
```

> **Alıştırma.** Şu kuralları hangi araçla (Policy / RBAC / lock / tag / Purview) ve hangi kapsamda uygularsın?
> 1. Tüm şirkette yalnızca AB bölgelerinde kaynak açılabilsin.
> 2. Stajyerler üretim aboneliğindeki kaynakları yalnızca görebilsin.
> 3. Üretim veritabanı sunucusu yanlışlıkla silinmesin, ama ayarları değiştirilebilsin.
> 4. Her kaynağın `costCenter` tag'i olsun; yoksa resource group'unkinden kopyalansın.
> 5. Şirketin hangi veri kaynaklarında TC kimlik numarası bulunduğu görülebilsin.
>
> <details><summary>Çözüm</summary>
>
> 1. **Azure Policy** (*Allowed locations*, Deny) → **root/üst management group**'a atanır, tüm subscription'lara miras kalır.
> 2. **RBAC** → stajyer grubuna **Reader** rolü, üretim **subscription** kapsamında.
> 3. **Resource lock** → **CanNotDelete**, veritabanı sunucusu kaynağı (veya RG) düzeyinde. ReadOnly değil — ayar değişikliğini de engellerdi.
> 4. **Tag + Azure Policy** → *Inherit a tag from the resource group if missing* (**Modify** etkisi); mevcut kaynaklar için **remediation task**. Tag'ler kendiliğinden miras alınmaz (→ C1.11).
> 5. **Microsoft Purview** → veri kaynaklarını Data Map ile tarama ve otomatik **sınıflandırma**.
> </details>

---

## 4. Gerçek dünya senaryosu

Bir sağlık şirketi, KVKK ve sektör denetimi öncesinde Azure ortamını düzene sokuyor:

1. **Kapsam:** Kök management group altında `Platform`, `Üretim`, `Geliştirme` management group'ları; her biri kendi subscription'larını içeriyor (→ C1.4).
2. **Policy:** Kök management group'a bir **initiative** atanıyor: *Allowed locations* (yalnızca AB bölgeleri, **Deny**), *Require a tag* (`costCenter`, `dataClass`, **Deny**), storage account'larda public erişimin kapalı olması (**Audit** ile başlanıyor, etki görüldükten sonra **Deny**'a çevriliyor), tanılama ayarlarının Log Analytics'e gitmesi (**DeployIfNotExists**). Compliance panosu ilk gün %62 uyum gösteriyor; remediation task'larla ve ekiplere açılan işlerle üç haftada %97'ye çıkıyor.
3. **Lock:** Üretim veritabanlarının ve ağ omurgasının bulunduğu resource group'lara **CanNotDelete**. Bir gece otomasyon hatasıyla çalışan `az group delete` komutu lock'a çarpıp duruyor.
4. **Purview:** Data Map; Azure SQL, Data Lake ve on-prem SQL Server kaynaklarını tarıyor; hasta kimlik bilgisi içeren tablolar otomatik sınıflandırılıyor. Denetçiye "hassas veri nerede ve kim erişiyor?" sorusunun cevabı katalogdan veriliyor.
5. **Kanıt:** Denetimde Microsoft'un kendi sertifikaları (ISO 27001, SOC raporları) Service Trust Portal'dan, şirketin kaynak uyumluluğu Policy compliance raporundan sunuluyor.

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Çoğunlukla "bu gereksinimi hangi araç karşılar?" senaryoları ve evet/hayır tabloları; özellikle **Policy ↔ RBAC ↔ lock** ayrımı ve lock türleri.

| Senaryodaki ifade | Cevap |
|---|---|
| "Kaynakların **yalnızca belirli bölgelerde** oluşturulmasını sağla" | **Azure Policy** (Allowed locations) |
| "Belirli VM boyutlarına izin ver", "her kaynağın tag'i olsun" | **Azure Policy** |
| "Uyumsuz kaynakları **raporla**, engelleme" | Policy, **Audit** etkisi |
| "Birden çok politikayı **tek birim** olarak ata" | **Initiative** |
| "Politikayı **tüm subscription'lara** uygula" | Policy'yi **management group**'a ata |
| "Kullanıcının kaynak **oluşturma yetkisi**" | **RBAC** |
| "Kaynağın **kazara silinmesini** önle" | **Resource lock — CanNotDelete (Delete)** |
| "Kaynağın **değiştirilmesini ve silinmesini** önle" | **Resource lock — ReadOnly** |
| "Owner silmeye çalışıyor ama lock var" | Önce lock **kaldırılmalı** — lock Owner'ı da durdurur |
| "Verinin **haritası**, **sınıflandırma**, **soy ağacı**, veri yönetişimi" | **Microsoft Purview** |
| "Microsoft'un **denetim raporlarına** (ISO, SOC) eriş" | **Service Trust Portal** |

**Tuzaklar:**
- Policy **kaynağı** kısıtlar, RBAC **kullanıcıyı**: "Kullanıcılar yalnızca West Europe'ta kaynak açabilsin" → Policy (bölge bir kaynak özelliğidir), RBAC değil.
- Deny politikası **mevcut** uyumsuz kaynakları **silmez**; yalnızca raporlar.
- Lock'lar **miras alınır**; RG'deki lock içindeki her kaynağı korur.
- "ReadOnly lock kaynağı yalnızca silmeye karşı korur" → **Hayır**, değiştirmeyi de engeller.
- "Purview, kullanıcıların kimliklerini yönetir" → **Hayır** (o Entra ID); "Purview maliyetleri raporlar" → **Hayır** (o Cost Management).

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Policy ile RBAC'ı karıştırmak | RBAC = **kim** ne yapabilir · Policy = kaynak **nasıl** olabilir |
| "Owner her şeyi yapabilir" | Deny politikasına ve lock'a Owner da takılır |
| Doğrudan **Deny** ile başlamak | Önce **Audit** ile etkisini gör, sonra Deny'a geç; aksi halde ekiplerin dağıtımları bir anda kırılır |
| "Politika atandı, eski kaynaklar düzeldi" | Mevcut kaynaklar yalnızca **raporlanır**; düzeltmek için remediation task veya elle müdahale |
| Her yere **ReadOnly** lock koymak | `listKeys`, rol ataması ve bazı yönetim işlemlerini de bozar; çoğu senaryoda **CanNotDelete** yeterli |
| "Lock veriyi korur" | Lock yönetim düzlemini korur; blob/dosya silmeye karşı soft delete, versioning, yedek gerekir |
| Purview'u Policy'nin alternatifi sanmak | Policy kaynak **yapılandırmasını**, Purview **veriyi** yönetir |
| Politikayı her subscription'a tek tek atamak | Management group'a bir kez ata, miras kalsın |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| **Deny** etkisi | Kural ihlali hiç oluşmaz | Yanlış kapsam/parametre meşru dağıtımları kırar; istisna yönetimi gerekir |
| **Audit** etkisi | Kimseyi durdurmadan görünürlük | İhlaller oluşmaya devam eder; birinin raporu takip etmesi gerekir |
| **Modify / DeployIfNotExists** | Uyumsuzluğu otomatik düzeltir | Managed identity ve yetki gerektirir; "sihirli" değişiklikler ekipleri şaşırtabilir |
| Politikaları üst management group'a atamak | Tek noktadan, tüm ortama tutarlı kural | Etki alanı büyük; hata da tüm ortama yayılır — önce test kapsamında dene |
| **CanNotDelete** lock | Kazara silmeye karşı ucuz, etkili koruma | Meşru silme/yeniden dağıtım için önce lock kaldırılmalı (otomasyonda adım) |
| **ReadOnly** lock | Yapılandırma kaymasını tamamen durdurur | Anahtar listeleme, rol ataması, bazı portal işlemleri bozulur |
| Purview ile veri kataloğu | Hassas verinin görünürlüğü, uyumluluk kanıtı | Tarama ve sınıflandırma kurulumu, sahiplik ve süreç gerektirir; ayrı lisans/ücret |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe features and tools in Azure for governance and compliance](https://learn.microsoft.com/en-us/training/modules/describe-features-tools-azure-for-governance-compliance/)
- [What is Azure Policy?](https://learn.microsoft.com/en-us/azure/governance/policy/overview) · [Azure Policy definitions effect basics](https://learn.microsoft.com/azure/governance/policy/concepts/effects)
- [Lock your Azure resources to protect your infrastructure](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/lock-resources) · [Apply a lock to a storage account](https://learn.microsoft.com/en-us/azure/storage/common/lock-account-resource)
- [Microsoft Purview data governance overview](https://learn.microsoft.com/purview/data-governance-overview) · [Unified Catalog](https://learn.microsoft.com/purview/unified-catalog)
- [Service Trust Portal](https://servicetrust.microsoft.com/) · [`az policy assignment`](https://learn.microsoft.com/en-us/cli/azure/policy/assignment) · [`az lock`](https://learn.microsoft.com/en-us/cli/azure/lock)
