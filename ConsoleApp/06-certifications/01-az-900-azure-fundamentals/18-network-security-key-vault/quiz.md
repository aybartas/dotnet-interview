# C1.18 Ağ Güvenliği, Bastion ve Key Vault — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *genişletilmiş kapsam (bağlam)* · 16 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, eşleştirme, senaryo). Gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ %80** (≥ 13/16).

---

### 1. Tek seçim
Bir subnet'teki VM'lere gelen trafiği **IP, port ve protokole** göre filtrelemek ve bunu **ek ücret ödemeden** yapmak istiyorsunuz. Hangisini kullanırsınız?

- A) Azure Firewall Premium
- B) Network security group (NSG)
- C) DDoS Network Protection
- D) Azure Bastion

<details><summary>Cevap</summary>

**B.** NSG, subnet/NIC düzeyinde ücretsiz L3/L4 paket filtresidir. Azure Firewall ücretli ve merkezidir; DDoS hacimsel saldırı korumasıdır; Bastion yönetim erişimi içindir.
</details>

### 2. Tek seçim
Bir NSG'de aşağıdaki iki inbound kural var. `443` portuna internetten gelen bir istek ne olur?

| Öncelik | Ad | Port | Kaynak | Eylem |
|---|---|---|---|---|
| 200 | deny-all-web | 443 | Internet | Deny |
| 300 | allow-web | 443 | Internet | Allow |

- A) İzin verilir, çünkü Allow kuralı sonradan tanımlanmıştır
- B) Engellenir, çünkü düşük numaralı (200) kural önce işlenir ve ilk eşleşme uygulanır
- C) Rastgele biri uygulanır
- D) Her iki kural da uygulanır; sonuç Allow olur

<details><summary>Cevap</summary>

**B.** NSG kuralları öncelik numarasına göre küçükten büyüğe işlenir; ilk eşleşen kural uygulanır ve sonrakilere bakılmaz.
</details>

### 3. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) NSG stateful'dur; izin verilen bir inbound bağlantının dönüş trafiği için ayrı kural gerekmez. | ☐ | ☐ |
| b) Varsayılan olarak NSG, internete giden (outbound) trafiği engeller. | ☐ | ☐ |
| c) Varsayılan olarak NSG, internetten gelen (inbound) trafiği engeller. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Varsayılan kurallar: `AllowInternetOutBound` outbound'u açık tutar; `DenyAllInBound` internetten gelen trafiği engeller (VNet ve Azure Load Balancer trafiği hariç).
</details>

### 4. Tek seçim
Bir NSG kuralında VM'lerin IP adresleri yerine **"web sunucuları"** ve **"veritabanı sunucuları"** gibi mantıksal gruplar kullanılmak isteniyor. Hangi kaynak bu amaca hizmet eder?

- A) Application security group (ASG)
- B) Route table
- C) Azure DNS
- D) Resource lock

<details><summary>Cevap</summary>

**A.** ASG, VM'leri uygulama rolüne göre gruplar ve NSG kurallarında kaynak/hedef olarak kullanılır; VM eklendikçe kural değişmez.
</details>

### 5. Tek seçim
Bir şirket, VNet'lerinden internete **yalnızca belirli FQDN'lere** (ör. `*.windows.net`) çıkışa izin vermek ve bilinen kötü domain'leri **threat intelligence** ile engellemek istiyor; politikanın **merkezi** yönetilmesini istiyor. Hangisi **en uygundur**?

- A) NSG
- B) Azure Firewall
- C) Application security group
- D) Service endpoint

<details><summary>Cevap</summary>

**B.** FQDN filtreleme, threat intelligence ve merkezi politika Azure Firewall'ın yetenekleridir; NSG yalnızca IP/port/protokol bilir.
</details>

### 6. Eşleştirme
Her ihtiyacı servisle eşleştirin: **NSG · Azure Firewall Premium · Azure DDoS Protection · WAF**

1. Subnet'te basit, ücretsiz port/IP filtresi
2. Giden TLS trafiğinin incelenmesi ve IDPS
3. Hacimsel (L3/L4) saldırı koruması
4. SQL injection ve XSS gibi web uygulaması saldırılarına karşı koruma

<details><summary>Cevap</summary>

**1 → NSG · 2 → Azure Firewall Premium · 3 → Azure DDoS Protection · 4 → WAF.**
</details>

### 7. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure'daki tüm public IP'ler için altyapı düzeyinde temel DDoS koruması varsayılan ve ücretsiz olarak açıktır. | ☐ | ☐ |
| b) DDoS Protection, uygulama katmanı (L7) SQL injection saldırılarını durdurmak için tek başına yeterlidir. | ☐ | ☐ |
| c) DDoS IP Protection, tek tek public IP'ler için seçilebilen bir korumadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** DDoS L3/L4 hacimsel/protokol saldırılarına karşıdır; L7 web saldırıları için WAF gerekir. Network Protection VNet'i, IP Protection tek tek IP'leri kapsar.
</details>

### 8. Tek seçim
Bir ekip, sanal makinelerinde **public IP olmadan**, Azure portalından tarayıcı üzerinden RDP/SSH bağlantısı kurmak ve 3389/22 portlarını internete **açmamak** istiyor. Hangisi **en uygundur**?

- A) Azure Bastion
- B) Her VM'e public IP + `Any` kaynağa 3389 izni
- C) Traffic Manager
- D) Azure CDN

<details><summary>Cevap</summary>

**A.** Bastion, VNet'teki `AzureBastionSubnet` üzerinden TLS ile VM'lere özel IP'yle bağlanır; VM'de public IP ve internete açık yönetim portu gerekmez.
</details>

### 9. Tek seçim
Yönetim portlarının **normalde kapalı** olup yalnızca kullanıcı **istek yaptığında, belirli IP'den ve sınırlı süre** için açılmasını sağlayan özellik hangisidir?

- A) Just-in-time (JIT) VM access
- B) Resource lock
- C) Azure Policy
- D) Availability set

<details><summary>Cevap</summary>

**A.** JIT, istek üzerine NSG kuralını süreli olarak açar ve süre bitince kapatır (Defender for Cloud özelliği).
</details>

### 10. Senaryo
Bir geliştirici, veritabanı parolasını `appsettings.json` içine yazıp repoya göndermiş. **En iyi** düzeltme hangisidir?

- A) Dosyayı özel repoya taşımak
- B) Parolayı Azure Key Vault'a koymak ve uygulamayı managed identity ile kasadan okutmak
- C) Parolayı Base64 ile kodlamak
- D) Parolayı ortam değişkenine düz metin yazıp aynı VM'de tutmak

<details><summary>Cevap</summary>

**B.** Key Vault secret'ları merkezi ve denetimli saklar; managed identity ile uygulama **kodda parola olmadan** erişir. Base64 şifreleme değildir. (Ayrıca sızdırılmış parola hemen **döndürülmelidir** — rotate.)
</details>

### 11. Çoklu seçim (iki doğru cevap)
Azure Key Vault'ta **hangi iki** nesne türü saklanabilir?

- A) Secret
- B) Sertifika
- C) Virtual machine görüntüsü
- D) Blob container

<details><summary>Cevap</summary>

**A ve B.** Key Vault secret, key (kriptografik anahtar) ve certificate saklar. VM görüntüsü ve blob container başka servislerin nesneleridir.
</details>

### 12. Tek seçim
Yeni bir Key Vault için veri düzlemi (secret/key/certificate) erişimi hangi model **önerilir**?

- A) Yalnızca vault access policy
- B) Azure RBAC
- C) Paylaşılan hesap anahtarı (shared key)
- D) Anonim erişim

<details><summary>Cevap</summary>

**B.** Azure RBAC önerilir; tutarlı denetim, daha ince kapsam (tek nesneye kadar) sağlar. Access policy eski modeldir.
</details>

### 13. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Key Vault'ta soft delete, silinen nesnelerin bekleme süresi boyunca geri alınmasını sağlar. | ☐ | ☐ |
| b) Purge protection açıldığında silinmiş bir nesne bekleme süresi dolmadan kalıcı olarak silinemez. | ☐ | ☐ |
| c) Purge protection bir kez açıldıktan sonra istenirse kapatılabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Purge protection kapatılamaz; bu yüzden lab ortamlarında açılmamalıdır.
</details>

### 14. Sıralama
RBAC modelindeki bir Key Vault'ta **soft delete** davranışını göstermek için aşağıdaki adımları doğru sıraya koyun: **(a) Secret'ı sil · (b) Kendine veri düzlemi yazma rolü ata (ör. Secrets Officer) · (c) Secret'ı `recover` ile geri al · (d) RBAC authorization etkin kasayı oluştur · (e) Secret'ı oluştur**

<details><summary>Cevap</summary>

**d → b → e → a → c.** Önce kasa, sonra veri düzlemi rolü (Owner/Contributor gibi yönetim düzlemi rolleri secret yazmak için yeterli değildir), ardından secret oluşturma; silinen secret bekleme süresi içinde `recover` ile geri alınabilir.
</details>

### 15. Eşleştirme
Her ifadeyi doğru şifreleme kavramıyla eşleştirin: **Platform-managed key · Customer-managed key · TLS · Managed HSM**

1. Hizmetlerin durağan veriyi, müşteri ek yapılandırma yapmadan varsayılan olarak şifrelediği anahtar türü
2. Anahtarın müşteri tarafından Key Vault'ta tutulduğu ve rotasyonunun müşteride olduğu model
3. Aktarım halindeki verinin korunması
4. Tek kiracılı, ayrılmış HSM üzerinde yüksek güvenceli anahtar saklama

<details><summary>Cevap</summary>

**1 → Platform-managed key · 2 → Customer-managed key · 3 → TLS · 4 → Managed HSM.**
</details>

### 16. Senaryo
Bir şirketin güvenlik ekibi, birçok kaynaktan log toplayıp olayları **korele etmek** ve **otomatik müdahale playbook'ları** çalıştırmak istiyor. Hangi servis **en uygundur**?

- A) Azure Bastion
- B) Microsoft Sentinel
- C) Azure DNS
- D) Network security group

<details><summary>Cevap</summary>

**B.** Sentinel, bulut-yerel SIEM/SOAR'dır. Defender for Cloud ise kaynakların güvenlik duruşunu (posture) ve iş yükü korumasını sağlar; uyarıları Sentinel'e akabilir.
</details>
