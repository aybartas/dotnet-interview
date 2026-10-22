# C1.1 Cloud Computing, Cloud Modelleri ve Fiyatlandırma — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe cloud concepts* (%25-30) · 18 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, senaryo); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Önce cevabını ver, sonra `<details>` içindeki gerekçeyi oku. Hedef: **≥ 15/18**.

---

### 1. Tek seçim
Aşağıdakilerden hangisi cloud computing'in tanımını **en iyi** ifade eder?

- A) Şirketin kendi sunucularını sanallaştırarak tek bir fiziksel makinede toplaması
- B) Bilgi işlem hizmetlerinin internet üzerinden, ihtiyaç anında ve kullanım kadar ücretle sunulması
- C) Uygulamaların yalnızca web tarayıcısı üzerinden çalıştırılması
- D) Verilerin birden fazla datacenter'a yedeklenmesi

<details><summary>Cevap</summary>

**B.** Tanımın üç ayağı: internet üzerinden, ihtiyaç anında (on-demand), kullanım kadar ücret (measured service). A sanallaştırmadır (bulutun bir teknolojisi, kendisi değil); C yalnızca SaaS'ın bir özelliği; D yedeklemedir.
</details>

### 2. Çoklu seçim (iki doğru cevap)
NIST tanımına göre aşağıdakilerden **hangi ikisi** cloud computing'in temel niteliklerindendir?

- A) On-demand self-service
- B) Tek kiracılı (single-tenant) donanım
- C) Measured service
- D) Uzun dönemli donanım sözleşmesi

<details><summary>Cevap</summary>

**A ve C.** Beş nitelik: on-demand self-service, broad network access, resource pooling (çok kiracılı havuz — B'nin tersi), rapid elasticity, measured service. D, bulutun ortadan kaldırdığı şeydir.
</details>

### 3. Evet / Hayır
Her ifade için Evet veya Hayır seçin.

| İfade | Evet | Hayır |
|---|---|---|
| a) SaaS modelinde kullanıcı hesaplarının ve erişim izinlerinin yönetimi müşterinin sorumluluğundadır. | ☐ | ☐ |
| b) IaaS modelinde fiziksel sunucuların bakımı müşterinin sorumluluğundadır. | ☐ | ☐ |
| c) PaaS modelinde işletim sistemi yamaları Microsoft'un sorumluluğundadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Hesaplar ve kimlikler her modelde müşteride; fiziksel katmanlar bulutun her modelinde Microsoft'ta; işletim sistemi IaaS'ta müşteride, PaaS'ta Microsoft'ta.
</details>

### 4. Tek seçim
Bir şirket Azure'da bir sanal makine (IaaS) çalıştırıyor. Aşağıdakilerden hangisi **Microsoft'un** sorumluluğundadır?

- A) VM'deki işletim sisteminin güvenlik yamaları
- B) VM'e erişen kullanıcıların MFA kullanması
- C) VM'in çalıştığı fiziksel host'un donanım arızası
- D) VM üzerindeki uygulamanın güvenlik duvarı kuralları

<details><summary>Cevap</summary>

**C.** IaaS'ta Microsoft fiziksel host, fiziksel ağ ve datacenter'dan sorumludur. OS (A), kimlik/MFA (B) ve uygulama/ağ kuralları (D) müşteridedir.
</details>

### 5. Tek seçim
Shared responsibility model'de aşağıdakilerden hangisi **tüm dağıtım modellerinde** (on-prem, IaaS, PaaS, SaaS) müşteride kalır?

- A) İşletim sistemi
- B) Ağ kontrolleri
- C) Bilgi ve veri
- D) Uygulamalar

<details><summary>Cevap</summary>

**C.** Her zaman müşteride kalan üçlü: bilgi ve veri; cihazlar (mobil ve PC); hesaplar ve kimlikler. OS PaaS'ta, ağ kontrolleri ve uygulamalar SaaS'ta Microsoft'a geçer.
</details>

### 6. Sıralama
Aşağıdaki dağıtım modellerini, **müşterinin sorumluluğu en fazladan en aza** doğru sıralayın: PaaS · On-prem · SaaS · IaaS

<details><summary>Cevap</summary>

**On-prem → IaaS → PaaS → SaaS.** Her adımda bir katman grubu Microsoft'a geçer: IaaS'ta fiziksel katman, PaaS'ta işletim sistemi (ve kısmen ağ/uygulama), SaaS'ta uygulama ve ağ kontrolleri.
</details>

### 7. Cümle tamamlama
"PaaS modelinde kimlik ve directory altyapısı \_\_\_\_\_\_\_\_\_\_."

- A) tamamen müşterinin sorumluluğundadır
- B) müşteri ile Microsoft arasında paylaşılır
- C) tamamen Microsoft'un sorumluluğundadır
- D) hiçbir tarafın sorumluluğunda değildir

<details><summary>Cevap</summary>

**B.** Kimlik altyapısı IaaS'ta müşteride, PaaS ve SaaS'ta **paylaşımlıdır**: altyapıyı (ör. Entra ID) Microsoft işletir, doğru yapılandırmayı (politikalar, roller) müşteri yapar.
</details>

### 8. Senaryo
Bir banka, regülasyon gereği müşteri verisini kendi datacenter'ında tutmak zorunda. Aynı zamanda mobil bankacılık uygulamasının ön yüzünün, kampanya dönemlerinde hızla ölçeklenmesini istiyor. Hangi cloud modeli **en uygundur**?

- A) Public cloud
- B) Private cloud
- C) Hybrid cloud
- D) Multicloud

<details><summary>Cevap</summary>

**C.** Hassas veri kurum içinde (private/on-prem), ölçeklenmesi gereken ön yüz public cloud'da → ikisinin birlikte kullanımı hybrid cloud'dur. B ölçek ihtiyacını karşılamaz; A veri yerleşimi gereksinimini karşılamaz; D birden çok public sağlayıcı demektir.
</details>

### 9. Tek seçim
Bir şirket iş yüklerini hem Azure'da hem de başka bir public cloud sağlayıcısında çalıştırıyor. Kendi datacenter'ı yok. Bu yaklaşımın adı nedir?

- A) Hybrid cloud
- B) Private cloud
- C) Multicloud
- D) Community cloud

<details><summary>Cevap</summary>

**C.** Birden çok public cloud sağlayıcısı = multicloud. Hybrid, kurum içi (private) altyapı ile public cloud'un birlikte kullanımıdır; burada kurum içi altyapı yok.
</details>

### 10. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Private cloud, tek bir kuruluşun kullanımına ayrılmış bulut altyapısıdır. | ☐ | ☐ |
| b) Public cloud kullanmak için önce sunucu donanımı satın almak gerekir. | ☐ | ☐ |
| c) Hybrid cloud, yerel kapasite yetmediğinde fazla yükün public cloud'a taşınmasına (cloud bursting) olanak tanır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Public cloud'da ön yatırım yoktur; donanım sağlayıcınındır. Cloud bursting, hybrid'in klasik kullanım senaryosudur.
</details>

### 11. Tek seçim
Aşağıdakilerden hangisi bir **CapEx** (capital expenditure) örneğidir?

- A) Aylık Azure faturası
- B) Kendi datacenter'ı için sunucu ve depolama ünitesi satın almak
- C) Kullanıcı başına aylık Microsoft 365 aboneliği
- D) Saatlik ücretlendirilen bir sanal makine

<details><summary>Cevap</summary>

**B.** CapEx, fiziksel altyapıya baştan yapılan, amortismanla yıllara yayılan yatırımdır. A, C, D dönemsel, kullanıma bağlı giderlerdir → OpEx.
</details>

### 12. Çoklu seçim (iki doğru cevap)
Consumption-based (tüketim bazlı) modelin **hangi ikisi** faydasıdır?

- A) Ön yatırım (upfront cost) gerekmez
- B) Fatura her ay sabittir
- C) Kullanılmayan kapasite için ödeme yapılmaz
- D) Donanım müşterinin mülkiyetine geçer

<details><summary>Cevap</summary>

**A ve C.** Tüketim bazlı modelde maliyet kullanımla orantılıdır, dolayısıyla fatura **değişkendir** (B yanlış); donanım sağlayıcıda kalır (D yanlış).
</details>

### 13. Cümle tamamlama
"Bir kuruluşun kendi datacenter'ından public cloud'a geçmesi, BT harcamalarını genellikle \_\_\_\_\_\_\_\_\_\_ kaydırır."

- A) OpEx'ten CapEx'e
- B) CapEx'ten OpEx'e
- C) CapEx'ten CapEx'e
- D) Hiçbir değişiklik olmaz

<details><summary>Cevap</summary>

**B.** Donanım yatırımı (CapEx) yerine dönemsel hizmet bedeli (OpEx) ödenir.
</details>

### 14. Senaryo
Bir şirketin muhasebe uygulaması, önümüzdeki üç yıl boyunca 7/24, yükü neredeyse hiç değişmeden çalışacak. Maliyeti **en aza** indirmek için hangi fiyatlandırma modeli **en uygundur**?

- A) Pay-as-you-go
- B) Spot VM
- C) 3 yıllık reserved instance (veya savings plan)
- D) Ücretsiz katman

<details><summary>Cevap</summary>

**C.** Sabit ve uzun süreli yükte taahhüt karşılığı indirim en ucuzudur. Spot (B) kesilebildiği için 7/24 kritik iş için uygun değildir; pay-as-you-go (A) esneklik için ödenen primdir; ücretsiz katman (D) üretim yükü için değildir.
</details>

### 15. Senaryo
Bir film stüdyosu, her gece binlerce kareyi işleyen bir render işi çalıştırıyor. İş kesilirse kaldığı yerden devam edebiliyor ve maliyet en önemli kriter. Hangi seçenek **en uygundur**?

- A) Spot VM
- B) 3 yıllık reserved instance
- C) Pay-as-you-go standart VM
- D) Kendi datacenter'ına sunucu almak

<details><summary>Cevap</summary>

**A.** Kesintiye dayanıklı, esnek zamanlı toplu iş = spot VM'in tipik kullanımı; Azure'un boşta kapasitesi çok düşük fiyata kullanılır, karşılığında VM geri alınabilir (eviction).
</details>

### 16. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Reserved instance alındığında, VM kapalı kalsa bile taahhüt bedeli ödenir. | ☐ | ☐ |
| b) Spot VM'ler, Azure kapasiteye ihtiyaç duyduğunda kesilebilir. | ☐ | ☐ |
| c) Pay-as-you-go modelinde uzun dönem taahhüt gerekir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Reservation bir taahhüttür (kullanım değil, rezerve edilen kapasite ödenir); spot kesilebilir kapasitedir; pay-as-you-go'nun tanımı taahhütsüzlüktür.
</details>

### 17. Tek seçim
Bir geliştirici Azure'u öğrenmeye başlıyor ve beklenmedik bir faturayla karşılaşmamak istiyor. **İlk** yapması gereken nedir?

- A) Reserved instance satın almak
- B) Aboneliğe bir bütçe (budget) ve uyarı eşiği tanımlamak
- C) Tüm kaynakları tek bir region'da açmak
- D) Spot VM kullanmak

<details><summary>Cevap</summary>

**B.** Cost Management bütçesi, harcama eşiğe yaklaşınca uyarı gönderir. Dikkat: bütçe harcamayı **durdurmaz**, yalnızca bildirir. Her laboratuvarı kaynakları silerek (`az group delete`) bitirmek ikinci kuraldır.
</details>

### 18. Senaryo
Bir şirket e-posta altyapısını kendi Exchange sunucularından Microsoft 365'e (SaaS) taşıdı. Taşımadan sonra aşağıdakilerden hangisi **hâlâ şirketin** sorumluluğundadır?

- A) E-posta sunucularının işletim sistemi yamaları
- B) Datacenter'daki sunucu donanımının yenilenmesi
- C) Çalışanların hesaplarında MFA'nın zorunlu tutulması ve posta kutusu verisinin doğru sınıflandırılması
- D) E-posta uygulamasının yeni sürüme yükseltilmesi

<details><summary>Cevap</summary>

**C.** SaaS'ta uygulama, OS ve fiziksel katman Microsoft'tadır (A, B, D); hesaplar/kimlikler ve veri her modelde müşteride kalır.
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README'nin 3.2 ve 3.4 bölümlerini tekrar et · < 12 → konuyu baştan çalış, `dotnet run -- C1.1 all` çıktısını incele.
