# C1.5 Azure Compute ve Virtual Machines — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure architecture and services* (%35-40) · 18 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, eşleştirme, senaryo); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 15/18**.

---

### 1. Tek seçim
Bir şirket, kurum içindeki bir uygulamayı özel bir sürücüye ihtiyaç duyduğu için işletim sistemi üzerinde tam kontrolle Azure'a taşımak istiyor. Hangi compute seçeneği **en uygundur**?

- A) Azure Functions
- B) Azure App Service
- C) Azure Virtual Machines
- D) Azure Container Apps

<details><summary>Cevap</summary>

**C.** OS üzerinde tam kontrol ve özel sürücü = IaaS, Azure VM. Diğerleri OS erişimi vermez.
</details>

### 2. Çoklu seçim (iki doğru cevap)
Bir Azure VM oluşturulurken **hangi ikisi** VM'in çalışması için **gerekli** kaynaklardır?

- A) Network interface (NIC)
- B) Load balancer
- C) Virtual network / subnet
- D) App Service plan

<details><summary>Cevap</summary>

**A ve C.** VM bir NIC ile bir VNet'teki subnet'e bağlanır (ayrıca boyut, image ve OS diski gerekir). Load balancer isteğe bağlıdır; App Service plan VM ile ilgisizdir.
</details>

### 3. Tek seçim
Aşağıdakilerden hangisi bir Azure VM için **isteğe bağlıdır**?

- A) OS diski
- B) VM boyutu
- C) Public IP adresi
- D) Image

<details><summary>Cevap</summary>

**C.** Public IP yalnızca VM'e internetten doğrudan erişim gerekiyorsa eklenir; prod'da çoğu zaman verilmez.
</details>

### 4. Tek seçim
Bir geliştirici, gece kullanılmayan test VM'leri için **compute ücreti ödememek** ama diskleri ve yapılandırmayı korumak istiyor. Ne yapmalıdır?

- A) VM'in içinden işletim sistemini kapatmak
- B) VM'i deallocate etmek
- C) VM'i silmek
- D) VM boyutunu küçültmek

<details><summary>Cevap</summary>

**B.** Deallocate edilen VM'in donanımı serbest bırakılır ve compute ücreti durur; diskler korunur (ve disk ücreti sürer). OS'tan kapatmak VM'i *Stopped* yapar ama donanım ayrılmış kaldığı için ücret sürer. Silmek yapılandırmayı korumaz.
</details>

### 5. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Deallocate edilmiş bir VM'in managed diskleri için ücret ödenmeye devam edilir. | ☐ | ☐ |
| b) İşletim sisteminden kapatılan (Stopped) bir VM için compute ücreti alınmaz. | ☐ | ☐ |
| c) Bir VM'in boyutu oluşturulduktan sonra değiştirilebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Diskler deallocate sonrası da depolama ücreti üretir. *Stopped* durumunda donanım ayrılmış kaldığından compute ücreti sürer. Boyut değiştirilebilir (genellikle yeniden başlatma ile).
</details>

### 6. Tek seçim
Bir web uygulamasının trafiği gün içinde çok dalgalanıyor. Şirket, **aynı yapılandırmadaki** VM'lerin sayısının CPU kullanımına göre **otomatik** artıp azalmasını istiyor. Ne kullanmalıdır?

- A) Availability set
- B) Proximity placement group
- C) Virtual Machine Scale Set
- D) Tek, büyük bir VM

<details><summary>Cevap</summary>

**C.** Scale set aynı VM'lerden oluşan grubu tek kaynak gibi yönetir ve autoscale kurallarıyla instance sayısını otomatik değiştirir.
</details>

### 7. Tek seçim
Bir availability set'te **fault domain** neyi temsil eder?

- A) Planlı bakım sırasında aynı anda yeniden başlatılan VM grubunu
- B) Ortak güç kaynağını ve ağ anahtarını paylaşan donanım (raf) grubunu
- C) Bir region içindeki ayrı bir datacenter'ı
- D) Bir subscription'daki resource group'ları

<details><summary>Cevap</summary>

**B.** Fault domain fiziksel donanım arızasının etki alanıdır. A update domain'in tanımıdır; C availability zone'dur.
</details>

### 8. Tek seçim
Azure'un planlı bakım sırasında bir uygulamanın tüm VM'lerini **aynı anda** yeniden başlatmasını önleyen availability set kavramı hangisidir?

- A) Fault domain
- B) Update domain
- C) Availability zone
- D) Scale set

<details><summary>Cevap</summary>

**B.** Update domain'ler planlı bakımda **sırayla** güncellenir; VM'ler farklı UD'lerde olduğu için aynı anda yeniden başlamaz.
</details>

### 9. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Availability set kullanmak için VM ücretine ek bir ücret ödenir. | ☐ | ☐ |
| b) Bir availability set, bir datacenter'ın tamamen çökmesine karşı koruma sağlar. | ☐ | ☐ |
| c) Bir availability set en fazla 3 fault domain içerebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Availability set'in kendisi ücretsizdir. Set tek bir datacenter içindedir; datacenter arızası için availability zone gerekir. Fault domain sayısı en çok 3'tür (update domain varsayılan 5, en çok 20).
</details>

### 10. Senaryo
Bir şirket, iki VM'li uygulamasının **datacenter düzeyinde** bir arızada da çalışmaya devam etmesini istiyor; uygulama tek bir region'da kalacak. Hangi seçenek **en uygundur**?

- A) VM'leri aynı availability set'e koymak
- B) VM'leri iki farklı availability zone'a dağıtmak
- C) VM'leri bir proximity placement group'a koymak
- D) VM'leri daha büyük bir boyuta yükseltmek

<details><summary>Cevap</summary>

**B.** Availability zone'lar bir region içinde fiziksel olarak ayrı datacenter gruplarıdır. Availability set tek datacenter içinde korur; PPG VM'leri bilerek yakınlaştırır.
</details>

### 11. Tek seçim
Çok katmanlı bir finansal işlem uygulamasında katmanlar arasındaki **ağ gecikmesinin mümkün olan en düşük** seviyede olması gerekiyor. Hangi özellik bunu sağlar?

- A) Availability zone
- B) Region pair
- C) Proximity placement group
- D) Update domain

<details><summary>Cevap</summary>

**C.** Proximity placement group VM'leri fiziksel olarak birbirine yakın (aynı datacenter içinde) yerleştirir. Bedeli: dayanıklılığın azalması.
</details>

### 12. Eşleştirme
Her gereksinimi doğru seçenekle eşleştirin: **Availability set · Availability zone · VM Scale Set · Proximity placement group**

1. "Yük artınca yeni VM'ler otomatik eklensin."
2. "Aynı datacenter'da kalsınlar ama aynı raf arızasından etkilenmesinler."
3. "Bir datacenter yansa da uygulama çalışsın."
4. "VM'ler arası gecikme en düşük olsun."

<details><summary>Cevap</summary>

**1 → VM Scale Set · 2 → Availability set · 3 → Availability zone · 4 → Proximity placement group.**
</details>

### 13. Tek seçim
İki veya daha fazla VM'in aynı **availability set**'e yerleştirildiği bir yapı için Microsoft'un tipik SLA'sı nedir?

- A) %99,9
- B) %99,95
- C) %99,99
- D) SLA yoktur

<details><summary>Cevap</summary>

**B.** Tek VM (Premium SSD) %99,9, availability set'te 2+ VM %99,95, iki+ zone'a yayılmış 2+ VM %99,99 (→ C1.2). Güncel değerler için SLA belgesine bakılmalıdır.
</details>

### 14. Çoklu seçim (iki doğru cevap)
VM Scale Sets ile ilgili **hangi iki** ifade doğrudur?

- A) Scale set'teki VM'ler aynı yapılandırmayla oluşturulur ve merkezi olarak yönetilir
- B) Scale set kullanmak, VM ücretlerine ek olarak ayrı bir lisans ücreti gerektirir
- C) Scale set instance'ları availability zone'lara yayılabilir
- D) Scale set yalnızca Windows VM'leri destekler

<details><summary>Cevap</summary>

**A ve C.** Scale set'in kendisi ek ücretli değildir (B yanlış); Windows ve Linux'u destekler (D yanlış).
</details>

### 15. Tek seçim
Bir yönetici bir VM'i siliyor ama ay sonunda faturada hâlâ o VM'le ilgili disk ve public IP ücretleri görüyor. En olası neden nedir?

- A) Deallocate edilen VM'ler faturalanmaya devam eder
- B) VM silinse de OS diski ve public IP gibi ilişkili kaynaklar ayrıca silinmedi
- C) Azure silme işlemini 30 gün geciktirir
- D) Availability set ücreti ödenir

<details><summary>Cevap</summary>

**B.** VM'in diskleri, NIC, public IP ve NSG ayrı kaynaklardır; VM'i silmek onları her zaman silmez. Bütün ortamı silmenin temiz yolu resource group'u silmektir.
</details>

### 16. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) VM Scale Set, ölçekleme kurallarına göre instance sayısını otomatik değiştirebilir. | ☐ | ☐ |
| b) Proximity placement group, VM'leri farklı region'lara dağıtarak dayanıklılığı artırır. | ☐ | ☐ |
| c) Var olan, çalışan bir VM sonradan bir availability set'e eklenebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Hayır.** PPG VM'leri bir araya toplar (gecikme için); VM availability set'e oluşturulurken yerleştirilir.
</details>

### 17. Tek seçim
Bir şirketin veritabanı sunucusu yüksek disk performansı (IOPS) ve düşük gecikme gerektiriyor. VM için hangi disk türü **en uygundur**?

- A) Standard HDD
- B) Standard SSD
- C) Premium SSD
- D) Disk gerekmez

<details><summary>Cevap</summary>

**C.** Premium SSD (veya daha uç ihtiyaçlarda Premium SSD v2 / Ultra Disk) üretim veritabanları için yüksek IOPS ve düşük gecikme sağlar; tek VM SLA'sını da etkiler.
</details>

### 18. Senaryo
Bir e-ticaret sitesinin web katmanı için şu gereksinimler var: (1) trafik artınca VM sayısı otomatik artsın, (2) bir datacenter arızasında site ayakta kalsın. Hangi tasarım **her iki** gereksinimi de karşılar?

- A) Tek bir availability set'te iki VM
- B) Availability zone'lara yayılmış ve autoscale kuralı tanımlı bir VM Scale Set
- C) Proximity placement group'ta dört VM
- D) Tek bir büyük VM ve günlük yedek

<details><summary>Cevap</summary>

**B.** Scale set + autoscale otomatik ölçeği, zone'lara yayılma datacenter arızasına dayanıklılığı sağlar. A otomatik ölçek ve datacenter koruması sunmaz; C dayanıklılığı azaltır; D tek arıza noktasıdır.
</details>

---

**Puanlama:** 16-18 → hazırsın · 12-15 → README 3.2 (VM kaynakları ve ücretlendirme) ile 3.4'teki karşılaştırma tablosunu tekrar et · < 12 → konuyu baştan çalış ve 3.6 laboratuvarını yap.
