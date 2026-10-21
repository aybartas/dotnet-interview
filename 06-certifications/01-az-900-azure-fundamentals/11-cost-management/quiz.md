# C1.11 Azure'da Maliyet Yönetimi — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure management and governance* (%30-35) · 18 soru
>
> Sorular **özgündür** ve sınav formatındadır; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 15/18**.

---

### 1. Tek seçim
Bir ekip, **henüz kurmadığı** bir web uygulamasının (App Service + Azure SQL Database + Storage) aylık Azure maliyetini tahmin etmek istiyor. Hangi aracı kullanmalıdır?

- A) Microsoft Cost Management — cost analysis
- B) Azure Pricing Calculator
- C) Azure Advisor
- D) Azure Service Health

<details><summary>Cevap</summary>

**B.** Pricing Calculator, kurulmamış bir mimarinin tahmini maliyetini hesaplar. Cost analysis gerçekleşen maliyeti gösterir; Advisor mevcut kaynaklar için öneri verir; Service Health olayları bildirir.
</details>

### 2. Tek seçim
Bir CFO, şirketin kendi datacenter'ını işletmenin 3 yıllık toplam maliyetini (donanım, enerji, alan, işçilik dahil) aynı iş yükünün Azure'daki maliyetiyle **karşılaştırmak** istiyor. Hangi yaklaşım bu ihtiyaca yöneliktir?

- A) Pricing Calculator
- B) TCO karşılaştırması (TCO Calculator / Azure Migrate business case)
- C) Azure Policy
- D) Resource tags

<details><summary>Cevap</summary>

**B.** On-prem ↔ Azure toplam sahip olma maliyeti karşılaştırması TCO'nun işidir. Klasik TCO Calculator'ın yerini bazı kaynaklara göre Azure Migrate business case almıştır; sınav mantığı aynıdır: Pricing Calculator yalnızca Azure tarafını tahmin eder.
</details>

### 3. Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** bir Azure kaynağının maliyetini **doğrudan** etkiler?

- A) Kaynağın dağıtıldığı bölge
- B) Kaynağın adı
- C) Kaynağın SKU'su / boyutu
- D) Kaynağı oluşturan kullanıcının rolü

<details><summary>Cevap</summary>

**A ve C.** Coğrafya (bölge) ve kaynak türü/ayarları (SKU, boyut, tier) maliyet faktörleridir. Ad ve oluşturanın rolü fiyatı değiştirmez.
</details>

### 4. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure'a gelen (inbound) veri trafiği genellikle ücretsizdir. | ☐ | ☐ |
| b) Bir Azure bölgesinden internete çıkan (egress) veri trafiği ücretlendirilebilir. | ☐ | ☐ |
| c) Aynı VM boyutunun fiyatı tüm Azure bölgelerinde aynıdır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Ingress genellikle ücretsiz, egress ücretlidir; fiyatlar bölgeye göre değişir.
</details>

### 5. Senaryo
Bir şirket, aylık Azure harcaması belirlediği limitin **%80'ine ulaştığında** finans ekibine otomatik e-posta gönderilmesini istiyor. Ne yapılandırmalıdır?

- A) Azure Policy ile Deny politikası
- B) Cost Management'ta bir budget ve uyarı eşiği
- C) Resource lock
- D) Pricing Calculator tahmini

<details><summary>Cevap</summary>

**B.** Budget + eşik uyarısı (e-posta veya action group). Policy ve lock maliyet uyarısı göndermez; Pricing Calculator tahmin aracıdır.
</details>

### 6. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Cost Management'ta bir budget aşıldığında Azure kaynakları otomatik olarak durdurulur. | ☐ | ☐ |
| b) Budget uyarısı bir action group'a bağlanarak otomasyon tetiklenebilir. | ☐ | ☐ |
| c) Cost analysis ile maliyetler resource group veya tag'e göre gruplanabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Budget yalnızca uyarır; kapatma gibi eylemler action group üzerinden kurulan otomasyonla yapılır.
</details>

### 7. Tek seçim
Bir şirket, maliyetleri **departmanlara göre** raporlamak istiyor. Kaynaklara hangi bilgi eklenmelidir?

- A) Resource lock
- B) `costCenter` gibi bir **tag**
- C) Ek bir subscription rolü
- D) Daha yüksek bir SLA

<details><summary>Cevap</summary>

**B.** Tag'ler (anahtar-değer meta veri) cost analysis'te gruplama ve filtreleme için kullanılır. (Ayrı subscription'lar da bir yöntemdir, ama şıklardaki "rol" yöntem değildir.)
</details>

### 8. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir resource group'a eklenen tag'ler, gruptaki kaynaklara varsayılan olarak otomatik uygulanır. | ☐ | ☐ |
| b) Azure Policy, eksik tag'lerin resource group'tan kaynaklara kopyalanması için kullanılabilir. | ☐ | ☐ |
| c) Tag'ler anahtar-değer çiftlerinden oluşur. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Tag'ler miras alınmaz; Policy (Modify etkisi, ör. *Inherit a tag from the resource group if missing*) bunu sağlar.
</details>

### 9. Tek seçim
Bir şirket, Software Assurance kapsamındaki mevcut **Windows Server ve SQL Server lisanslarını** Azure'daki VM'lerde kullanarak maliyetini düşürmek istiyor. Hangi seçenek bunu sağlar?

- A) Spot VM
- B) Azure Hybrid Benefit
- C) Dev/test fiyatlandırması
- D) Azure Marketplace

<details><summary>Cevap</summary>

**B.** Azure Hybrid Benefit, mevcut lisansların Azure'da kullanılmasıyla lisans maliyetini düşürür.
</details>

### 10. Senaryo
Bir uygulamanın veritabanı sunucusu önümüzdeki üç yıl 7/24 aynı boyutta çalışacak. **En büyük indirimi** hangi seçenek sağlar?

- A) Pay-as-you-go
- B) 3 yıllık reservation
- C) Spot VM
- D) Ücretsiz hesap

<details><summary>Cevap</summary>

**B.** Sabit, uzun süreli yükte taahhütlü indirim en ucuzudur. Spot kesilebilir olduğu için 7/24 kritik veritabanına uygun değildir.
</details>

### 11. Tek seçim
Bir geliştirici bir VM'i sildi, ancak ertesi ay faturada hâlâ o VM'e ait **disk ve public IP** ücretleri görülüyor. Bu hangi maliyet faktörüne örnektir?

- A) Coğrafya
- B) Bakım (unutulan/kullanılmayan kaynaklar)
- C) Azure Marketplace
- D) Subscription türü

<details><summary>Cevap</summary>

**B.** VM'i silmek bağlı kaynakları her zaman silmez; kullanılmayan kaynaklar faturalanmaya devam eder. Çözüm: resource group düzeyinde temizlik, Advisor önerileri.
</details>

### 12. Eşleştirme
Her ihtiyacı araçla eşleştirin: **Pricing Calculator · Cost Management (cost analysis) · Cost Management (budgets) · Azure Advisor**

1. Geçen ayın gerçekleşen maliyetini servis bazında görmek
2. Az kullanılan VM'leri küçültme önerisi almak
3. Yeni bir projenin tahmini aylık maliyetini çıkarmak
4. Harcama eşiği aşılınca uyarı almak

<details><summary>Cevap</summary>

**1 → Cost analysis · 2 → Advisor · 3 → Pricing Calculator · 4 → Budgets.**
</details>

### 13. Tek seçim
Bir şirket Azure Marketplace'ten üçüncü taraf bir güvenlik duvarı imajıyla VM dağıtıyor. Fatura hakkında hangisi doğrudur?

- A) Yalnızca Microsoft'un VM ücreti ödenir
- B) VM ücretine ek olarak satıcının yazılım ücreti de ödenebilir
- C) Marketplace ürünleri her zaman ücretsizdir
- D) Marketplace ürünleri yalnızca Enterprise Agreement ile alınabilir

<details><summary>Cevap</summary>

**B.** Marketplace ürünlerinde Azure altyapı ücreti + yayıncının (satıcının) ürün ücreti olabilir.
</details>

### 14. Senaryo
Bir şirketin geliştirme ortamındaki VM'ler yalnızca hafta içi mesai saatlerinde kullanılıyor. Maliyeti düşürmek için **en uygun** adım hangisidir?

- A) VM'leri 3 yıllık reservation'a almak
- B) Mesai dışında VM'leri otomatik kapatmak (deallocate) ve dev/test fiyatlandırmasını kullanmak
- C) VM'leri başka bir bölgeye taşımak
- D) VM'lere daha çok tag eklemek

<details><summary>Cevap</summary>

**B.** Haftada ~60 saat kullanılan VM'i kalan ~108 saat kapatmak compute ücretini yarıdan fazla düşürür; dev/test fiyatlandırması üretim dışı ortamlarda ek indirim sağlar. Reservation, çoğu zaman kapalı duran bir VM için boşa ödeme olur.
</details>

### 15. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Pricing Calculator'ın sonucu bir tahmindir; gerçek fatura kullanıma göre farklı olabilir. | ☐ | ☐ |
| b) Pricing Calculator'ı kullanmak için bir Azure aboneliği gerekir. | ☐ | ☐ |
| c) Pricing Calculator'da reservation ve Azure Hybrid Benefit gibi seçeneklerin etkisi görülebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Calculator herkese açıktır; satın alma seçeneklerini karşılaştırmaya izin verir; sonuç tahmindir.
</details>

### 16. Tek seçim
Bir kuruluş, kimsenin `costCenter` tag'i olmadan kaynak oluşturamamasını istiyor. Ne kullanmalıdır?

- A) Cost Management budget
- B) Azure Policy (*Require a tag* — Deny etkisi)
- C) Resource lock (ReadOnly)
- D) Azure Advisor

<details><summary>Cevap</summary>

**B.** Tag'i zorunlu kılmak Azure Policy'nin işidir (→ C1.12). Budget uyarır; lock silme/değiştirmeyi engeller; Advisor öneri verir.
</details>

### 17. Senaryo (hesap)
Kendi datacenter'ında 3 yıllık maliyet: 1. yıl 380.000 $ (donanım peşin dahil), 2. ve 3. yıllar 100.000'er $. Azure'da aynı iş yükü her yıl 90.000 $. Aşağıdakilerden hangisi doğrudur?

- A) On-prem 3 yılda daha ucuzdur
- B) Azure 3 yılda yaklaşık 310.000 $ daha ucuzdur ve harcama yıllara eşit yayılır
- C) Azure'da 1. yıl donanım için peşin ödeme gerekir
- D) İki seçenek de aynı maliyettedir

<details><summary>Cevap</summary>

**B.** On-prem: 380.000 + 100.000 + 100.000 = 580.000 $; Azure: 3 × 90.000 = 270.000 $; fark 310.000 $. On-prem'de harcama 1. yılda yığılır (CapEx), Azure'da dönemseldir (OpEx). Kendi rakamlarınla yapıyı görmek için: `dotnet run -- 1.11 tco 20`.
</details>

### 18. Tek seçim
Bir şirket kesintiye dayanıklı, gece çalışan toplu veri işleme işlerinin maliyetini **en aza** indirmek istiyor; işler kesilirse yeniden başlatılabiliyor. Hangi seçenek **en uygundur**?

- A) Spot VM
- B) 3 yıllık reservation
- C) Azure Hybrid Benefit
- D) Premium SSD diskler

<details><summary>Cevap</summary>

**A.** Spot VM, Azure'un boşta kapasitesini büyük indirimle sunar; karşılığında VM geri alınabilir — kesintiye dayanıklı toplu işler için idealdir.
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README 3.2 (araç ayrımı) ve 3.4 (tag kuralları) bölümlerini tekrar et · < 12 → konuyu baştan çalış, `dotnet run -- 1.11 all` çıktısını incele.
