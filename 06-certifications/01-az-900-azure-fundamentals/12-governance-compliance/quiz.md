# C1.12 Azure Yönetişim ve Uyumluluk — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure management and governance* (%30-35) · 18 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, eşleştirme, senaryo); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 15/18**.

---

### 1. Tek seçim
Bir şirket, çalışanlarının yalnızca **West Europe** ve **North Europe** bölgelerinde kaynak oluşturabilmesini istiyor. Hangi Azure özelliğini kullanmalıdır?

- A) Azure RBAC
- B) Resource lock
- C) Azure Policy
- D) Microsoft Purview

<details><summary>Cevap</summary>

**C.** Bölge bir **kaynak özelliğidir**; kaynağın özelliklerini kısıtlayan araç Azure Policy'dir (*Allowed locations*). RBAC kullanıcının hangi işlemleri yapabileceğini belirler, bölgeyi kısıtlamaz.
</details>

### 2. Tek seçim
Bir yönetici, üretim resource group'undaki kaynakların **kazara silinmesini** önlemek ama ekiplerin ayarları değiştirmesine izin vermek istiyor. Ne yapmalıdır?

- A) Resource group'a ReadOnly lock uygulamak
- B) Resource group'a CanNotDelete (Delete) lock uygulamak
- C) Ekiplere Reader rolü vermek
- D) Bir Audit politikası atamak

<details><summary>Cevap</summary>

**B.** CanNotDelete okumaya ve değiştirmeye izin verir, silmeyi engeller. ReadOnly (A) değişikliği de engeller; Reader (C) ekiplerin ayar değiştirmesini de engeller; Audit (D) yalnızca raporlar.
</details>

### 3. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Owner rolüne sahip bir kullanıcı, CanNotDelete lock'ı olan bir kaynağı lock'ı kaldırmadan silebilir. | ☐ | ☐ |
| b) Bir resource group'a uygulanan lock, içindeki kaynaklara miras kalır. | ☐ | ☐ |
| c) ReadOnly lock, kaynağın okunmasına izin verir ama değiştirilmesini ve silinmesini engeller. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Lock'lar rol yetkisinden bağımsızdır; Owner da önce lock'ı kaldırmak zorundadır. Lock'lar alt kapsamlara miras kalır.
</details>

### 4. Tek seçim
Azure Policy ile Azure RBAC arasındaki fark en iyi hangisiyle açıklanır?

- A) Policy kullanıcıların izinlerini, RBAC kaynakların özelliklerini yönetir
- B) Policy kaynakların özelliklerinin kurallara uymasını, RBAC kullanıcıların hangi işlemleri yapabileceğini yönetir
- C) İkisi de aynı işi yapar; Policy yalnızca daha yenidir
- D) Policy yalnızca maliyeti, RBAC yalnızca güvenliği yönetir

<details><summary>Cevap</summary>

**B.** RBAC = **kim** ne yapabilir; Policy = kaynak **nasıl** olabilir. Owner bile Deny politikasına aykırı kaynak oluşturamaz.
</details>

### 5. Tek seçim
Bir kuruluş, kendi güvenlik standardını oluşturan **20 politika tanımını** tek bir birim olarak birçok subscription'a atamak ve uyumluluğu tek bir yerden izlemek istiyor. Hangisini kullanmalıdır?

- A) Resource lock
- B) Policy initiative
- C) Management group
- D) Tag

<details><summary>Cevap</summary>

**B.** Initiative (policy set), birlikte yönetilen politika tanımları kümesidir. Management group (C) **kapsamdır** — initiative'i oraya atamak, "birçok subscription" gereksinimini karşılar; ama birim olarak gruplayan şey initiative'dir.
</details>

### 6. Tek seçim
Bir şirket, yeni politikasının mevcut dağıtımları bozup bozmayacağını görmek istiyor; uyumsuz kaynakların **engellenmesini değil**, yalnızca **raporlanmasını** istiyor. Hangi politika etkisini seçmelidir?

- A) Deny
- B) Audit
- C) Modify
- D) DeployIfNotExists

<details><summary>Cevap</summary>

**B.** Audit kaynağa izin verir ve uyumsuz olarak raporlar. Deny engeller; Modify ve DeployIfNotExists değişiklik yapar.
</details>

### 7. Eşleştirme
Her politika etkisini davranışıyla eşleştirin: **Deny · Audit · Modify · DeployIfNotExists**

1. Eksik tanılama ayarını otomatik olarak dağıtır.
2. Kurala aykırı oluşturma isteğini reddeder.
3. Oluşturma sırasında eksik bir tag'i ekler.
4. Uyumsuz kaynağa izin verir ama raporlar.

<details><summary>Cevap</summary>

**1 → DeployIfNotExists · 2 → Deny · 3 → Modify · 4 → Audit.**
</details>

### 8. Senaryo
Bir şirket, tüm subscription'larındaki (şu an 40, her ay yenileri ekleniyor) kaynakların zorunlu `costCenter` tag'ine sahip olmasını istiyor. Yönetim yükünü **en aza** indirmek için politikayı nereye atamalıdır?

- A) Her resource group'a ayrı ayrı
- B) Her subscription'a ayrı ayrı
- C) Subscription'ları içeren management group'a
- D) Her kaynağa ayrı ayrı

<details><summary>Cevap</summary>

**C.** Management group'a yapılan atama altındaki **tüm mevcut ve yeni** subscription'lara miras kalır; tek noktadan yönetilir.
</details>

### 9. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Deny etkili bir politika atandığında, kurala aykırı **mevcut** kaynaklar otomatik olarak silinir. | ☐ | ☐ |
| b) Azure Policy, mevcut kaynakların uyumluluk durumunu bir panoda gösterir. | ☐ | ☐ |
| c) Azure Policy'yi management group, subscription veya resource group kapsamına atayabilirsiniz. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Mevcut uyumsuz kaynaklar silinmez, "non-compliant" olarak raporlanır; düzeltme için remediation task veya elle müdahale gerekir.
</details>

### 10. Tek seçim
Bir yönetici, kritik bir storage account'a **ReadOnly** lock uyguladıktan sonra bir uygulamanın hesabın erişim anahtarlarını listeleyemediğini fark ediyor. Bunun sebebi nedir?

- A) Lock, storage account'taki tüm blob'ları şifreledi
- B) ReadOnly lock, anahtar listeleme gibi Resource Manager üzerinden yapılan bazı POST işlemlerini de engeller
- C) Uygulamanın rolü otomatik olarak Reader'a düşürüldü
- D) Lock, storage account'u başka bir bölgeye taşıdı

<details><summary>Cevap</summary>

**B.** ReadOnly lock yalnızca "değiştirmeyi" değil, `listKeys` gibi POST işlemlerini de engeller. Bu yüzden ReadOnly beklenmedik yan etkiler doğurabilir; çoğu durumda CanNotDelete yeterlidir. Lock rolleri değiştirmez (C).
</details>

### 11. Tek seçim
Bir şirketin veri ekibi; Azure SQL Database, Azure Data Lake, on-prem SQL Server ve Amazon S3'teki verilerin **tek bir haritasını** çıkarmak, hassas verileri **otomatik sınıflandırmak** ve verinin hangi kaynaktan geldiğini (soy ağacı) görmek istiyor. Hangi çözümü kullanmalıdır?

- A) Azure Policy
- B) Microsoft Purview
- C) Azure Monitor
- D) Microsoft Defender for Cloud

<details><summary>Cevap</summary>

**B.** Veri haritası, katalog, sınıflandırma ve lineage Microsoft Purview'un veri yönetişimi yetenekleridir. Policy kaynak yapılandırmasını, Monitor metrik/log'ları, Defender güvenlik duruşunu yönetir.
</details>

### 12. Çoklu seçim (iki doğru cevap)
Microsoft Purview ile **hangi ikisi** yapılabilir?

- A) Kredi kartı numarası gibi hassas verileri otomatik keşfedip sınıflandırmak
- B) Kullanıcılara MFA zorunluluğu getirmek
- C) Hassas verinin kuruluş dışına gönderilmesini veri kaybı önleme (DLP) politikalarıyla engellemek
- D) Sanal makinelerin boyutunu otomatik büyütmek

<details><summary>Cevap</summary>

**A ve C.** Purview'un risk ve uyumluluk tarafı sınıflandırma, bilgi koruma ve DLP'yi kapsar. MFA Entra ID'nin (→ C1.9), ölçekleme autoscale'in işidir.
</details>

### 13. Tek seçim
Bir denetçi, **Microsoft'un** Azure için aldığı ISO 27001 sertifikasını ve SOC denetim raporlarını görmek istiyor. Bu belgeler nereden alınır?

- A) Azure Policy compliance panosu
- B) Microsoft Service Trust Portal
- C) Azure Advisor
- D) Microsoft Purview Data Map

<details><summary>Cevap</summary>

**B.** Service Trust Portal, Microsoft'un bağımsız denetim raporlarını ve uyumluluk belgelerini yayımlar. Policy compliance panosu (A) **senin** kaynaklarının kurallarına uyumunu gösterir.
</details>

### 14. Senaryo
Bir ekip, her gece otomatik olarak bir test resource group'unu silip yeniden oluşturan bir pipeline çalıştırıyor. Bir yönetici bu RG'ye CanNotDelete lock koyduktan sonra pipeline hata vermeye başladı. **En doğru** çözüm hangisidir?

- A) Pipeline'ın kimliğine Owner rolü vermek
- B) Lock'ı test RG'sinden kaldırmak (lock'ı üretim kaynaklarında tutmak)
- C) Lock'ı ReadOnly'ye çevirmek
- D) Bir Audit politikası atamak

<details><summary>Cevap</summary>

**B.** Lock rol yetkisinden bağımsızdır; Owner olmak (A) silmeyi sağlamaz. ReadOnly (C) durumu kötüleştirir. Düzenli silinen test kaynakları için lock uygun değildir; lock kalıcı/üretim kaynakları içindir.
</details>

### 15. Cümle tamamlama
"Azure Policy'de, ilişkili bir kaynak veya ayar mevcut değilse onu **otomatik olarak dağıtan** etkiye \_\_\_\_\_\_\_\_\_\_ denir."

- A) Append
- B) AuditIfNotExists
- C) DeployIfNotExists
- D) Disabled

<details><summary>Cevap</summary>

**C.** DeployIfNotExists eksik ilişkili kaynağı dağıtır; AuditIfNotExists (B) yalnızca raporlar.
</details>

### 16. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Resource lock'lar subscription, resource group veya tek kaynak düzeyinde uygulanabilir. | ☐ | ☐ |
| b) CanNotDelete lock'lı bir storage account'taki blob'lar da silinemez. | ☐ | ☐ |
| c) Bir lock'ı kaldırmak için de uygun yetki (ör. Owner veya User Access Administrator) gerekir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Lock yönetim düzlemini (hesabın kendisini) korur, içindeki veriyi değil; blob'ları korumak için soft delete/versioning gibi veri koruma özellikleri gerekir. Lock kaldırmak `Microsoft.Authorization/locks/*` yetkisi ister.
</details>

### 17. Eşleştirme
Her gereksinimi doğru araçla eşleştirin: **Azure Policy · RBAC · Resource lock · Microsoft Purview**

1. Pazarlama ekibi yalnızca kendi resource group'undaki kaynakları yönetebilsin.
2. Yalnızca onaylı VM boyutları kullanılabilsin.
3. Ağ omurgası kaynakları kazara silinmesin.
4. Hangi veri kaynaklarında kişisel veri olduğu bilinsin.

<details><summary>Cevap</summary>

**1 → RBAC** (kim ne yapabilir, RG kapsamında Contributor) · **2 → Azure Policy** (izin verilen SKU'lar) · **3 → Resource lock** (CanNotDelete) · **4 → Microsoft Purview** (veri keşfi ve sınıflandırma).
</details>

### 18. Tek seçim
Bir şirket, kaynaklara `env` tag'inin **resource group'tan otomatik olarak kopyalanmasını** ve mevcut tag'siz kaynakların da düzeltilmesini istiyor. Ne yapmalıdır?

- A) Resource group'a tag vermek yeterlidir; tag'ler otomatik miras alınır
- B) *Inherit a tag from the resource group* gibi **Modify** etkili bir politika atamak ve mevcut kaynaklar için **remediation task** çalıştırmak
- C) Resource group'a ReadOnly lock uygulamak
- D) Tüm kullanıcılara Contributor rolü vermek

<details><summary>Cevap</summary>

**B.** Tag'ler varsayılan olarak miras **alınmaz** (A yanlış; → C1.11). Modify etkili politika yeni/güncellenen kaynaklara tag'i ekler; mevcut kaynakları remediation task düzeltir.
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README 3.2'deki etkiler tablosunu ve 3.3'teki lock türlerini tekrar et · < 12 → konuyu baştan çalış, 3.5'teki laboratuvarı uygula.
