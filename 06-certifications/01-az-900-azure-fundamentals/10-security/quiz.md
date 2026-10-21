# C1.10 Güvenlik: Zero Trust, Defense in Depth, Microsoft Defender for Cloud — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure architecture and services* (%35-40) · 15 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, eşleştirme, senaryo). Gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 12/15**.

---

### 1. Tek seçim
"Asla güvenme, her zaman doğrula" ifadesi hangi güvenlik modelini tanımlar?

- A) Defense in depth
- B) Zero Trust
- C) Shared responsibility model
- D) Perimeter (kale ve hendek) modeli

<details><summary>Cevap</summary>

**B.** Zero Trust, ağın içi dahil hiçbir isteğe otomatik güvenmez ve her isteği doğrular. Perimeter modeli (D) tam tersine iç ağa güvenir.
</details>

### 2. Eşleştirme
Her uygulamayı Zero Trust ilkesiyle eşleştirin: **Verify explicitly · Use least privilege access · Assume breach**

1. Yöneticiye üretim ortamında yalnızca talep ettiğinde ve 2 saatliğine yetki vermek
2. Her girişte kullanıcı kimliğini, konumunu ve cihazın uyumlu olup olmadığını değerlendirmek
3. Ağı segmentlere ayırıp bir sunucunun ele geçirilmesinin diğerlerine yayılmasını engellemek
4. Trafiği uçtan uca şifrelemek ve analitikle anormal davranışı tespit etmek

<details><summary>Cevap</summary>

**1 → Least privilege** (just-in-time) · **2 → Verify explicitly** (tüm sinyallerle doğrula) · **3 → Assume breach** (etki alanını küçült) · **4 → Assume breach** (şifrele, tespit et).
</details>

### 3. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Zero Trust modelinde, şirket ağından gelen isteklere otomatik olarak güvenilir. | ☐ | ☐ |
| b) Zero Trust, satın alınıp kurulan tek bir Microsoft ürünüdür. | ☐ | ☐ |
| c) Conditional Access, Zero Trust'ın "verify explicitly" ilkesini uygulamaya yardımcı olur. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Zero Trust konuma göre güven vermez; bir model/stratejidir, ürün değildir. Conditional Access kimlik, konum, cihaz ve risk sinyallerini değerlendirir.
</details>

### 4. Tek seçim
Defense in depth stratejisinin **temel amacı** nedir?

- A) Tüm güvenliği tek ve çok güçlü bir güvenlik duvarında toplamak
- B) Birden çok güvenlik katmanı kullanarak bir katman aşıldığında diğerlerinin saldırıyı yavaşlatmasını veya durdurmasını sağlamak
- C) Güvenlik sorumluluğunu tamamen bulut sağlayıcısına devretmek
- D) Kullanıcıların tek girişle birden çok uygulamaya erişmesini sağlamak

<details><summary>Cevap</summary>

**B.** Katmanlı savunmada hiçbir kontrol tek arıza noktası değildir. A tam tersidir; D SSO'dur.
</details>

### 5. Sıralama
Defense in depth katmanlarını **dıştan içe** doğru sıralayın: Ağ · Veri · Fiziksel güvenlik · Uygulama · Kimlik ve erişim · Compute · Çevre (perimeter)

<details><summary>Cevap</summary>

**Fiziksel güvenlik → Kimlik ve erişim → Çevre → Ağ → Compute → Uygulama → Veri.** En içte, saldırganın asıl hedefi olan veri bulunur.
</details>

### 6. Tek seçim
Azure DDoS Protection, defense in depth modelinin hangi katmanında yer alır?

- A) Veri
- B) Uygulama
- C) Çevre (perimeter)
- D) Compute

<details><summary>Cevap</summary>

**C.** Çevre katmanı ağın dış sınırını büyük ölçekli saldırılardan (DDoS) ve çevre güvenlik duvarlarıyla korur.
</details>

### 7. Eşleştirme
Her kontrolü defense in depth katmanıyla eşleştirin: **Kimlik ve erişim · Ağ · Compute · Uygulama · Veri**

1. VM'lerde işletim sistemi yamalarını güncel tutmak
2. Connection string'leri kodda değil Azure Key Vault'ta saklamak
3. Subnet'ler arasında NSG ile varsayılan olarak trafiği engellemek
4. Tüm yöneticiler için MFA zorunluluğu
5. Veritabanını durağan halde (at rest) şifrelemek

<details><summary>Cevap</summary>

**1 → Compute · 2 → Uygulama · 3 → Ağ · 4 → Kimlik ve erişim · 5 → Veri.**
</details>

### 8. Tek seçim
Defense in depth modelinde, saldırganların **asıl hedefi** genellikle hangi katmandaki varlıktır ve bu katmanın korunması bulutta kimin sorumluluğundadır?

- A) Fiziksel güvenlik — Microsoft
- B) Veri — her zaman müşteri
- C) Çevre — Microsoft
- D) Compute — her zaman Microsoft

<details><summary>Cevap</summary>

**B.** En içteki veri katmanı asıl hedeftir. Shared responsibility'ye göre bilgi ve veri her modelde müşteridedir (→ C1.1).
</details>

### 9. Tek seçim
Microsoft Defender for Cloud'un amacını **en iyi** hangisi tanımlar?

- A) Kaynakların hangi bölgede oluşturulabileceğini zorlamak
- B) Kaynakların güvenlik duruşunu sürekli değerlendirmek, öneriler sunmak ve iş yüklerine yönelik tehditleri tespit etmek
- C) Bulut harcamasını izlemek ve bütçe uyarısı göndermek
- D) Kullanıcıların uygulamalara tek girişle erişmesini sağlamak

<details><summary>Cevap</summary>

**B.** Defender for Cloud = CSPM (duruş yönetimi, öneriler, secure score) + CWP (iş yükü koruması, tehdit uyarıları). A Azure Policy, C Cost Management, D SSO'dur.
</details>

### 10. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Defender for Cloud yalnızca Azure'da çalışan kaynakları izleyebilir. | ☐ | ☐ |
| b) Kurum içi sunucular Azure Arc aracılığıyla Defender for Cloud'a bağlanabilir. | ☐ | ☐ |
| c) Defender for Cloud, AWS ve Google Cloud kaynaklarının güvenlik duruşunu da değerlendirebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Defender for Cloud hybrid ve multicloud'dur: on-prem için Azure Arc, AWS/GCP için connector'lar.
</details>

### 11. Tek seçim
Defender for Cloud'da, önerilerin uygulanma durumuna göre bir subscription'ın güvenlik duruşunu **yüzde** olarak gösteren ölçü hangisidir?

- A) Advisor score
- B) Secure score
- C) SLA
- D) Compliance score

<details><summary>Cevap</summary>

**B.** Secure score, güvenlik kontrolleri altındaki önerilerin ne kadarının uygulandığını gösterir. Yükseldikçe duruş iyileşir.
</details>

### 12. Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Microsoft Defender for Cloud'un yeteneklerindendir?

- A) Bir VM'e yönelik kaba kuvvet (brute-force) giriş denemelerinde güvenlik uyarısı üretmek
- B) Kaynakları kazara silinmeye karşı kilitlemek
- C) Yanlış yapılandırılmış kaynaklar için güvenlik önerileri sunmak
- D) Kurum içi AD kullanıcılarını Entra ID'ye senkronize etmek

<details><summary>Cevap</summary>

**A ve C.** A tehdit tespiti (Defend/CWP), C duruş önerisidir (Secure/CSPM). B resource lock (→ C1.12), D Entra Connect'tir (→ C1.9).
</details>

### 13. Senaryo
Bir şirket Azure'da, kendi datacenter'ında ve AWS'te sunucular çalıştırıyor. Güvenlik ekibi bu ortamların **hepsinin** güvenlik açıklarını ve tehditlerini **tek bir panelden** görmek istiyor. Hangi çözüm uygundur?

- A) Azure Advisor
- B) Azure Policy
- C) Microsoft Defender for Cloud (kurum içi sunucular için Azure Arc ve AWS connector'ı ile)
- D) Azure Service Health

<details><summary>Cevap</summary>

**C.** Defender for Cloud; Azure, kurum içi (Arc) ve AWS/GCP (connector) kaynaklarını tek panelde değerlendirir ve korur. Advisor yalnızca Azure kaynakları için genel öneri verir; Service Health Azure platform olaylarını bildirir.
</details>

### 14. Tek seçim
Azure Advisor'ın **Security** kategorisinde gösterilen öneriler nereden gelir?

- A) Azure Service Health
- B) Microsoft Defender for Cloud
- C) Azure Cost Management
- D) Microsoft Purview

<details><summary>Cevap</summary>

**B.** Advisor'ın güvenlik önerileri Defender for Cloud değerlendirmelerinden beslenir; Advisor bunları diğer dört kategoriyle (reliability, performance, cost, operational excellence) birlikte gösterir (→ C1.14).
</details>

### 15. Senaryo
Bir uygulamada SQL injection zafiyeti bulundu. Ancak saldırganın simülasyonu, veritabanının private endpoint arkasında olması, uygulamanın yalnızca tek tabloya okuma yetkisi olan bir kimlik kullanması ve verilerin şifreli olması nedeniyle sınırlı veriye ulaşabildi. Bu durum en iyi hangi kavramı örnekler?

- A) Single sign-on
- B) Defense in depth
- C) Consumption-based model
- D) High availability

<details><summary>Cevap</summary>

**B.** Uygulama katmanı aşılsa bile ağ (private endpoint), kimlik (en az yetki) ve veri (şifreleme) katmanları hasarı sınırladı. Katmanlı savunmanın amacı budur (bu kontrollerin bir kısmı Zero Trust'ın "least privilege" ve "assume breach" ilkeleriyle de örtüşür).
</details>

---

**Puanlama:** 14-15 → hazırsın · 11-13 → README 3.1 (üç ilke) ve 3.2 (katman tablosu) bölümlerini tekrar et · < 11 → konuyu baştan çalış ve 3.2'deki alıştırmayı yap.
