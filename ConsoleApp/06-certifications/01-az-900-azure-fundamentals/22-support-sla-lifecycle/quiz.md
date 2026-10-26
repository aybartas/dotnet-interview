# C1.22 Destek Planları, SLA'lar, Servis Yaşam Döngüsü ve Benimseme Çerçeveleri — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *genişletilmiş kapsam (bağlam)* · 15 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, eşleştirme, senaryo). Gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ %80** (15 soruda ≥ 12).

---

### 1. Tek seçim
Her Azure aboneliğinde **varsayılan** olarak bulunan ve **ücretsiz** olan destek planı hangisidir?

- A) Developer
- B) Standard
- C) Basic
- D) Professional Direct

<details><summary>Cevap</summary>

**C.** Basic ücretsizdir; faturalama/abonelik desteği ve self-help kaynakları (Learn, Q&A, Advisor, Service Health) içerir, teknik destek içermez.
</details>

### 2. Eşleştirme
Her ihtiyacı destek planıyla eşleştirin: **Basic · Developer · Standard · Professional Direct · Unified**

1. Yalnızca öğrenme amaçlı abonelik; sadece fatura sorusu ve dokümantasyon yeter
2. Test ortamı; iş saatlerinde e-posta ile teknik destek yeterli
3. Üretim iş yükü; gece de telefonla teknik destek gerekli
4. İş-kritik iş yükü; hızlı yanıt ve mimari/operasyon rehberliği isteniyor
5. Bankanın Azure, Microsoft 365 ve Dynamics için tek sözleşmesi ve atanmış Technical Account Manager'ı

<details><summary>Cevap</summary>

**1 → Basic · 2 → Developer · 3 → Standard · 4 → Professional Direct · 5 → Unified** (eski Premier'in yerini aldı). Ücret ve yanıt süresi sayıları değişebilir; güncel plan sayfasını kontrol et.
</details>

### 3. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Basic plan 7/24 telefonla teknik sorun giderme desteği içerir. | ☐ | ☐ |
| b) Destek talebinde "yanıt süresi", sorunun çözüldüğü süreyi ifade eder. | ☐ | ☐ |
| c) Azure Status ve Service Health, Basic planda da kullanılabilen self-service kaynaklardır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Basic teknik destek içermez. Yanıt süresi, bir mühendisin ilk dönüşüne kadar geçen süredir; çözüm taahhüdü değildir.
</details>

### 4. Tek seçim
Bir ekip yeni bir Azure servisini kullanmak istiyor; servis **public preview** aşamasında. Aşağıdakilerden hangisi **doğrudur**?

- A) Microsoft, preview servisler için GA ile aynı SLA'yı verir.
- B) Preview servisler için SLA yoktur ve destek/özellik garantisi sınırlıdır; üretim için önerilmez.
- C) Preview servisler yalnızca Azure Government'ta bulunur.
- D) Preview servisler her zaman ücretsizdir ve kalıcıdır.

<details><summary>Cevap</summary>

**B.** Preview: SLA yok, özellik/API değişebilir, destek kapsamı sınırlı olabilir. GA aşamasında SLA ve tam destek gelir.
</details>

### 5. Sıralama
Bir servisin yaşam döngüsü aşamalarını **erken aşamadan olgun aşamaya** doğru sıralayın: **General Availability (GA) · Public preview · Private preview**

<details><summary>Cevap</summary>

**Private preview → Public preview → General Availability (GA).** Private: davetle, sınırlı; Public: herkese açık ama SLA'sız; GA: SLA ve tam destek.
</details>

### 6. Tek seçim
Bir servisin SLA'sı **%99,9**'dur. Bir ayda (730 saat) izin verilen toplam kesinti yaklaşık ne kadardır?

- A) ≈ 4,4 dakika
- B) ≈ 21,9 dakika
- C) ≈ 43,8 dakika
- D) ≈ 7,3 saat

<details><summary>Cevap</summary>

**C.** 730 saat = 43.800 dk; %0,1 × 43.800 ≈ 43,8 dk. (%99,99 ≈ 4,4 dk, %99,95 ≈ 21,9 dk, %99 ≈ 7,3 saat.)
</details>

### 7. Tek seçim
Bir uygulama, **seri bağlı** iki bileşenden oluşuyor: web katmanı (SLA %99,9) ve veritabanı (SLA %99,9). Uygulamanın toplam (composite) SLA'sı yaklaşık nedir?

- A) %99,9'dan yüksek
- B) Yaklaşık %99,8
- C) Tam olarak %99,9
- D) Yaklaşık %100

<details><summary>Cevap</summary>

**B.** Seri bileşenlerin SLA'ları çarpılır: 0,999 × 0,999 ≈ 0,998 → %99,8. Toplam, en zayıf bileşenden de düşüktür.
</details>

### 8. Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** bir uygulamanın SLA'sını **artırmaya** yönelik doğru yaklaşımlardır?

- A) VM'leri birden fazla availability zone'a yaymak
- B) Mevcut tek VM'i daha büyük bir boyuta çıkarmak (yedeklilik eklemeden)
- C) Uygulamayı birden çok bölgede çalıştırıp trafiği yönlendirmek
- D) Resource group'a tag eklemek

<details><summary>Cevap</summary>

**A ve C.** SLA'yı yükseltmenin yolu yedekliliktir (zone, region) veya daha yüksek SLA'lı katman seçmektir. Daha büyük VM tek arıza noktasını kaldırmaz; tag SLA'yı etkilemez.
</details>

### 9. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) SLA tutturulamadığında, müşteri faturasından servis kredisi talep edebilir. | ☐ | ☐ |
| b) SLA, servisin hiçbir zaman kesintiye uğramayacağını garanti eder. | ☐ | ☐ |
| c) Ücretsiz katmanlar (ör. App Service Free) genellikle SLA kapsamında değildir. | ☐ | ☐ |
| d) Seri bağlı bileşenlerin toplam SLA'sı, en düşük bileşenin SLA'sından yüksektir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet · d) Hayır.** SLA bir mali taahhüttür, kesintisizlik garantisi değildir. Seri toplam en düşük bileşenin altına iner.
</details>

### 10. Eşleştirme
Her Well-Architected sütununu tanımıyla eşleştirin: **Reliability · Security · Cost Optimization · Operational Excellence · Performance Efficiency**

1. Arıza anında çalışmaya devam etme ve toparlanma
2. Veriyi ve sistemi koruma (en az yetki, şifreleme)
3. Gereksiz harcamayı azaltma (doğru boyut, kullanılmayanı kapatma)
4. IaC, CI/CD ve izleme ile sağlıklı dağıtım ve operasyon
5. Talebe göre ölçekleme ve kaynakları verimli kullanma

<details><summary>Cevap</summary>

**1 → Reliability · 2 → Security · 3 → Cost Optimization · 4 → Operational Excellence · 5 → Performance Efficiency.** Azure Advisor'ın beş kategorisi (Reliability, Security, Cost, Operational Excellence, Performance) bu sütunlarla örtüşür (→ C1.14).
</details>

### 11. Tek seçim
Bir kurum, buluta geçiş yolculuğunu **strateji, plan, hazırlık, benimseme, yönetişim ve yönetim** aşamalarıyla yönlendirecek bir **rehber** arıyor. Hangi çerçeve uygundur?

- A) Azure Well-Architected Framework
- B) Microsoft Cloud Adoption Framework
- C) Azure Policy
- D) Azure Advisor

<details><summary>Cevap</summary>

**B.** CAF, kurumun buluta geçiş yolculuğunu yönlendirir. WAF tek bir iş yükünün mimari kalitesini değerlendirir; Policy ve Advisor araçlardır.
</details>

### 12. Tek seçim
Bir dokümanda "WAF'ın beş sütunu" ifadesi geçiyor ve bağlam bir **iş yükü mimarisinin kalitesini** değerlendirmekten söz ediyor. Buradaki WAF nedir?

- A) Web Application Firewall
- B) Well-Architected Framework
- C) Windows Admin Framework
- D) Workload Assessment Feature

<details><summary>Cevap</summary>

**B.** Mimari kalite ve beş sütun bağlamında WAF = Well-Architected Framework. Web Application Firewall bir ağ güvenliği servisidir (→ C1.17/C1.18).
</details>

### 13. Tek seçim
Bir ekip, buluta taşınan iş yüklerinin güvenle yerleşeceği, **management group'lar, Policy atamaları, ağ topolojisi ve kimlik tabanı** önceden hazır bir ortam kurmak istiyor. Bu kavramın adı nedir?

- A) Availability zone
- B) Azure landing zone
- C) Proximity placement group
- D) Resource lock

<details><summary>Cevap</summary>

**B.** Azure landing zone, CAF'nin "Ready" aşamasında hazırlanan, yönetişim ve ağ tabanıyla önceden yapılandırılmış ortamdır.
</details>

### 14. Senaryo
Bir ekip, üretimde kullandığı bir servisin bir özelliğinin **emekliye ayrılacağını** son anda öğrenip acil geçiş yapmak zorunda kaldı. Gelecekte bu tür duyuruları **zamanında** fark etmek için aşağıdakilerden **hangisi en uygundur**?

- A) Azure Updates'i ve Service Health'teki *health advisories* bildirimlerini takip etmek
- B) Yalnızca Basic destek planına güvenmek
- C) Resource group'lara lock koymak
- D) Cost Management bütçesi oluşturmak

<details><summary>Cevap</summary>

**A.** Retirement duyuruları Azure Updates'te yayımlanır ve Service Health'te aksiyon gerektiren değişiklikler (health advisories) olarak gelir; uyarı kuralları ve e-posta ile izlenir (→ C1.14).
</details>

### 15. Senaryo
Bir e-ticaret şirketi, **üretim** iş yükleri için **7/24 teknik destek** istiyor ve bütçesi sınırlı. Aynı şirket dev/test aboneliği için yalnızca iş saatinde destek yeterli buluyor. Hangi kombinasyon en makul olur?

- A) Her iki abonelik için Basic
- B) Üretim → Standard (veya üstü), dev/test → Developer
- C) Üretim → Developer, dev/test → Unified
- D) Her iki abonelik için Unified

<details><summary>Cevap</summary>

**B.** Üretim için 7/24 teknik destek veren plan (Standard ve üstü), üretim dışı ortam için Developer yeterlidir. Basic teknik destek vermez; Unified büyük kuruma yöneliktir ve bu senaryo için gereksiz pahalıdır. Güncel plan özelliklerini sayfadan doğrula.
</details>

---

**Puanlama:** 14-15 → hazırsın · 11-13 → README 3.1 (plan ağacı), 3.2 (SLA) ve 3.4 (CAF/WAF) bölümlerini tekrar et · < 11 → konuyu baştan çalış; 3.5'teki karar tablosunu ve 3.6'daki composite SLA hesabını yap.
