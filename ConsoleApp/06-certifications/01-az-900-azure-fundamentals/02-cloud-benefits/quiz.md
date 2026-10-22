# C1.2 Cloud'un Faydaları — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe cloud concepts* (%25-30) · 20 soru
>
> Sorular **özgündür** ve sınav formatındadır; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. SLA hesapları için: `dotnet run -- C1.2 serial …` / `parallel …` / `downtime …`. Hedef: **≥ 16/20**.

---

### 1. Tek seçim
Bir e-ticaret sitesinin çalıştığı datacenter'lardan birinde elektrik kesintisi oldu; site, başka bir availability zone'daki kopyası sayesinde kullanıcılar fark etmeden yayında kaldı. Bu, bulutun hangi faydasını gösterir?

- A) Scalability
- B) High availability
- C) Predictability
- D) Governance

<details><summary>Cevap</summary>

**B.** Arızaya rağmen hizmetin **kesintisiz sürmesi** high availability'dir. Scalability kapasite değişimi, predictability öngörülebilirlik, governance uyumdur.
</details>

### 2. Tek seçim
Bir uygulama, bütün bir Azure region'ını etkileyen bir felaketten sonra eşleşik bölgede yeniden ayağa kaldırıldı ve verilerin çoğu korunmuş olarak çalışmaya devam etti. Bu en iyi hangi faydayla açıklanır?

- A) Reliability
- B) Elasticity
- C) Manageability
- D) Security

<details><summary>Cevap</summary>

**A.** Arızadan/felaketten **toparlanıp** çalışmaya devam etmek reliability'dir (felaket kurtarma bunun bir parçasıdır).
</details>

### 3. Tek seçim
Bir yönetici, kampanya öncesinde uygulamanın çalıştığı VM'in boyutunu 2 vCPU'dan 8 vCPU'ya yükseltti. Bu işlemin adı nedir?

- A) Scale out
- B) Scale in
- C) Scale up
- D) Failover

<details><summary>Cevap</summary>

**C.** Aynı kaynağı **daha güçlü** hale getirmek vertical scaling, yani scale up'tır. Scale out instance **sayısını** artırmaktır.
</details>

### 4. Tek seçim
Bir uygulamanın instance sayısı, CPU kullanımı %70'i geçtiğinde **otomatik olarak** 2'den 6'ya çıkıyor ve yük azalınca tekrar 2'ye iniyor. Bu, bulutun hangi faydasıdır?

- A) Elasticity
- B) High availability
- C) Reliability
- D) Predictability

<details><summary>Cevap</summary>

**A.** Ölçeklemenin yüke göre **otomatik** yapılması elasticity'dir. (Scalability bu yeteneğin kendisidir; "otomatik" kelimesi elasticity'yi işaret eder.)
</details>

### 5. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Scale out, mevcut sunucuya daha fazla bellek eklemek demektir. | ☐ | ☐ |
| b) Horizontal scaling, kaynak sayısını artırmak veya azaltmaktır. | ☐ | ☐ |
| c) Vertical scaling'in, kullanılabilecek en büyük makine boyutuyla sınırlı bir üst limiti vardır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Bellek/CPU eklemek scale **up**'tır; scale out instance sayısını artırır. Vertical scaling en büyük boyutla sınırlıdır.
</details>

### 6. Cümle tamamlama
"Bir sistemin talebe göre kaynak ekleme veya çıkarma **yeteneğine** \_\_\_\_\_\_\_\_\_\_ denir; bunun yüke göre **otomatik** yapılmasına ise \_\_\_\_\_\_\_\_\_\_ denir."

- A) elasticity · scalability
- B) scalability · elasticity
- C) availability · reliability
- D) reliability · scalability

<details><summary>Cevap</summary>

**B.** Scalability yetenek; elasticity onun otomatik kullanımı.
</details>

### 7. Tek seçim
Bir finans ekibi, önümüzdeki çeyreğin bulut harcamasını gerçek kullanım verilerine dayanarak tahmin edebiliyor ve bütçe aşımı olmadan planlama yapıyor. Bu hangi faydadır?

- A) Governance
- B) Predictability
- C) Security
- D) Elasticity

<details><summary>Cevap</summary>

**B.** Maliyetin öngörülebilmesi **cost predictability**'dir. Predictability'nin diğer yüzü performans öngörülebilirliğidir.
</details>

### 8. Tek seçim
Bir web sitesinin kullanıcı sayısı gün içinde 10 kat dalgalanmasına rağmen, load balancer ve autoscale sayesinde sayfa yanıt süresi sürekli aynı aralıkta kalıyor. Bu en iyi hangi faydayla açıklanır?

- A) Performance predictability
- B) Cost predictability
- C) Reliability
- D) Governance

<details><summary>Cevap</summary>

**A.** Yük değişse de performansın **öngörülebilir** kalması. (Autoscale aynı zamanda elasticity'dir; ama sorunun vurgusu "yanıt süresinin aynı kalması" yani performans öngörülebilirliğidir.)
</details>

### 9. Tek seçim
Bir kuruluş, tüm aboneliklerinde yalnızca Avrupa Birliği içindeki region'larda kaynak oluşturulmasına izin veren ve uyumsuz kaynakları raporlayan kurallar uyguluyor. Bu hangi faydadır?

- A) Security
- B) Governance
- C) High availability
- D) Scalability

<details><summary>Cevap</summary>

**B.** Kurumsal standartlara ve regülasyonlara **uyum**, uyumsuzluğun tespiti ve raporlanması governance'tır (araç: Azure Policy → C1.12).
</details>

### 10. Tek seçim
Bir şirket PaaS'a geçtikten sonra işletim sistemi güvenlik yamalarının bulut sağlayıcısı tarafından otomatik uygulanmasından ve altyapının DDoS saldırılarına karşı korunmasından faydalanıyor. Bu hangi faydadır?

- A) Security
- B) Governance
- C) Manageability
- D) Predictability

<details><summary>Cevap</summary>

**A.** Tehditlere karşı koruma ve yamaların sağlayıcıda olması security faydasıdır. Bulut, ihtiyaca göre kontrol düzeyi seçtirir: IaaS'ta tam kontrol, PaaS/SaaS'ta bakım sağlayıcıda.
</details>

### 11. Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** *management of the cloud* (bulutun yönetimi) örneğidir?

- A) Kaynakların talebe göre otomatik ölçeklenmesi
- B) Azure portal üzerinden kaynak oluşturulması
- C) Bir kaynak sorun yaşadığında uyarı gönderilmesi
- D) Azure CLI komutlarının kullanılması

<details><summary>Cevap</summary>

**A ve C.** Bulutun yönetimi: otomatik ölçekleme, şablonla dağıtım, sağlık izleme ve uyarı. Portal ve CLI (B, D) *management in the cloud* — yani yönetim **arayüzleridir**.
</details>

### 12. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure'daki her servis, preview aşamasında da bir SLA ile sunulur. | ☐ | ☐ |
| b) SLA tutturulamadığında müşteri servis kredisi talep edebilir. | ☐ | ☐ |
| c) Bir servisin SLA'sı, nasıl yapılandırıldığına (ör. tek VM mi, birden çok zone'a yayılmış VM'ler mi) göre değişebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Preview servislerin SLA'sı yoktur. SLA ihlalinde faturadan indirim (servis kredisi) talep edilir. Tek VM ile zone'lara yayılmış VM'lerin SLA'sı farklıdır.
</details>

### 13. Tek seçim
%99,9 SLA'lı bir servis için bir ayda (730 saat) izin verilen **en fazla** kesinti yaklaşık ne kadardır?

- A) ~4 dakika
- B) ~22 dakika
- C) ~44 dakika
- D) ~7 saat

<details><summary>Cevap</summary>

**C.** `730 sa × 0,001 ≈ 0,73 sa ≈ 43,8 dk`. ~4 dk %99,99'a, ~22 dk %99,95'e, ~7 saat %99'a karşılık gelir. (`dotnet run -- C1.2 downtime 99.9`)
</details>

### 14. Tek seçim
Bir uygulama, SLA'sı %99,95 olan bir web servisi ile SLA'sı %99,99 olan bir veritabanından oluşuyor; çalışması için **ikisinin de** ayakta olması gerekiyor. Uygulamanın composite SLA'sı yaklaşık nedir?

- A) %99,99
- B) %99,97
- C) %99,95
- D) %99,94

<details><summary>Cevap</summary>

**D.** Seri bağlı bileşenlerde SLA'lar çarpılır: `0,9995 × 0,9999 ≈ 0,9994`. Sonuç en zayıf bileşenden (%99,95) bile düşüktür. (`dotnet run -- C1.2 serial 99.95 99.99`)
</details>

### 15. Senaryo
Bir şirket, kritik uygulamasının SLA'sını **yükseltmek** istiyor. Hangi değişiklik bunu **en etkili** biçimde sağlar?

- A) Uygulamanın çalıştığı tek VM'i daha büyük bir boyuta yükseltmek
- B) Uygulamayı iki farklı availability zone'daki birden çok VM'e dağıtmak
- C) VM'i spot fiyatlandırmaya geçirmek
- D) VM'e daha fazla disk eklemek

<details><summary>Cevap</summary>

**B.** SLA'yı yükselten şey **yedekliliktir**: zone'lara yayılmış birden çok VM (%99,99), tek VM'den (%99,9) yüksek SLA'ya sahiptir. Daha büyük VM (A) hâlâ tek arıza noktasıdır; spot (C) kesilebilir olduğundan HA'yı düşürür.
</details>

### 16. Tek seçim (hesap)
Composite SLA'sı %99,9 olan aynı uygulama yığını, birbirinden bağımsız iki region'a kuruluyor ve biri çökerse diğeri tüm trafiği alıyor (yönlendirme katmanını yok sayın). Toplam SLA yaklaşık nedir?

- A) %99,8
- B) %99,9
- C) %99,95
- D) %99,9999

<details><summary>Cevap</summary>

**D.** Paralel (yedekli) bileşenlerde: `1 − (1 − 0,999)² = 1 − 0,000001 = %99,9999`. Seri bağlantıda çarpım SLA'yı düşürürdü (A); yedeklilik yükseltir. (`dotnet run -- C1.2 parallel 99.9 99.9`)
</details>

### 17. Eşleştirme
Her senaryoyu doğru faydayla eşleştirin: **High availability · Scalability · Reliability · Manageability**

1. Geliştiriciler kaynakları Azure CLI ve REST API ile yönetiyor.
2. Kampanya için uygulamaya elle 10 yeni instance eklendi.
3. Datacenter arızasında uygulama başka zone'dan yayında kaldı.
4. Bölgesel felaketten sonra sistem yedek bölgede yeniden çalıştırıldı.

<details><summary>Cevap</summary>

**1 → Manageability** (*in* the cloud) · **2 → Scalability** (elle olduğu için elasticity değil) · **3 → High availability** (kesintisiz devam) · **4 → Reliability** (toparlanma).
</details>

### 18. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Elasticity, kaynakların iş yüküne göre otomatik olarak artırılıp azaltılmasıdır. | ☐ | ☐ |
| b) High availability, bir felaketten sonra verilerin yedekten geri yüklenmesi anlamına gelir. | ☐ | ☐ |
| c) Governance, kuruluşun kurallarına ve regülasyonlara uyumu sağlamaya yardımcı olur. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Felaketten sonra geri dönüş reliability/disaster recovery'dir; HA, kesinti **olmadan** çalışmayı sürdürmektir.
</details>

### 19. Tek seçim
Bir ekip, uygulamanın altyapısını her ortam için aynı **şablondan** dağıtıyor ve kaynakların sağlığını izleyip sorun olduğunda uyarı alıyor. Bu, bulutun hangi faydasının örneğidir?

- A) Manageability
- B) Elasticity
- C) Predictability
- D) High availability

<details><summary>Cevap</summary>

**A.** Şablonla dağıtım ve izleme/uyarı, *management of the cloud* yani manageability'dir (araçlar: ARM/Bicep, Azure Monitor → C1.13, C1.14).
</details>

### 20. Senaryo
Bir haber sitesi, büyük bir olay olduğunda **önceden tahmin edilemeyen** trafik sıçramaları yaşıyor. Hem sıçrama anında kesinti yaşamamak hem de sakin zamanlarda boşta kapasite için ödeme yapmamak istiyor. Hangi yaklaşım **en uygundur**?

- A) Zirve trafiğe göre boyutlanmış sabit sayıda sunucu çalıştırmak
- B) Her sabah elle sunucu sayısını artırmak
- C) Metrik tabanlı autoscale kuralları ile yatay ölçekleme (elasticity)
- D) Tek bir çok büyük VM kullanmak

<details><summary>Cevap</summary>

**C.** Öngörülemeyen yükte maliyeti talebe bağlayan ve insan müdahalesi gerektirmeyen yaklaşım elasticity'dir. A boşta kapasite için öder; B tahmine dayalıdır ve öngörülemeyen sıçramayı kaçırır; D tek arıza noktasıdır ve üst sınırı vardır.
</details>

---

**Puanlama:** 18-20 → hazırsın · 14-17 → README 3.1'deki "karıştırılan çiftler"i tekrar et · < 14 → konuyu baştan çalış, `dotnet run -- C1.2 all` çıktısını incele.
