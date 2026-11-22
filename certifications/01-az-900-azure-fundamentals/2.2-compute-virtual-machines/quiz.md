[← Konu anlatımı](README.md) · [AZ-900 İçindekiler](../README.md)

### 📝 Bölüm 2.2 Quiz Soruları

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure architecture and services* (%35-40) · 21 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, eşleştirme, senaryo); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 17/21**.

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir şirket, kurum içindeki bir uygulamayı özel bir sürücüye ihtiyaç duyduğu için işletim sistemi üzerinde tam kontrolle Azure'a taşımak istiyor. Hangi compute seçeneği **en uygundur**?

- A) Azure Functions
- B) Azure App Service
- C) Azure Virtual Machines
- D) Azure Container Apps

<details><summary>Cevap</summary>

**C.** OS üzerinde tam kontrol ve özel sürücü = IaaS, Azure VM. Diğerleri OS erişimi vermez.
</details>

#### Soru 2: Çoklu seçim (iki doğru cevap)
Bir Azure VM oluşturulurken **hangi ikisi** VM'in çalışması için **gerekli** kaynaklardır?

- A) Network interface (NIC)
- B) Public IP adresi
- C) Virtual network / subnet
- D) App Service plan

<details><summary>Cevap</summary>

**A ve C.** VM bir NIC ile bir VNet'teki subnet'e bağlanır (ayrıca boyut, image ve OS diski gerekir). Public IP isteğe bağlıdır (prod'da genellikle verilmez); App Service plan VM ile ilgisizdir.
</details>

#### Soru 3: Tek seçim
Aşağıdakilerden hangisi bir Azure VM için **isteğe bağlıdır**?

- A) OS diski
- B) VM boyutu
- C) Public IP adresi
- D) Image

<details><summary>Cevap</summary>

**C.** Public IP yalnızca VM'e internetten doğrudan erişim gerekiyorsa eklenir; prod'da çoğu zaman verilmez.
</details>

#### Soru 4: Tek seçim
Bir geliştirici, gece kullanılmayan test VM'leri için **compute ücreti ödememek** ama diskleri ve yapılandırmayı korumak istiyor. Ne yapmalıdır?

- A) VM'in içinden işletim sistemini kapatmak
- B) VM'i deallocate etmek
- C) VM'i silmek
- D) VM boyutunu küçültmek

<details><summary>Cevap</summary>

**B.** Deallocate edilen VM'in donanımı serbest bırakılır ve compute ücreti durur; diskler korunur (ve disk ücreti sürer). OS'tan kapatmak VM'i *Stopped* yapar ama donanım ayrılmış kaldığı için ücret sürer. Silmek yapılandırmayı korumaz.
</details>

#### Soru 5: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Deallocate edilmiş bir VM'in managed diskleri için ücret ödenmeye devam edilir. | ☐ | ☐ |
| b) İşletim sisteminden kapatılan (Stopped) bir VM için compute ücreti alınmaz. | ☐ | ☐ |
| c) Bir VM'in boyutu oluşturulduktan sonra değiştirilebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Diskler deallocate sonrası da depolama ücreti üretir. *Stopped* durumunda donanım ayrılmış kaldığından compute ücreti sürer. Boyut değiştirilebilir (genellikle yeniden başlatma ile).
</details>

#### Soru 6: Tek seçim
Bir web uygulamasının trafiği gün içinde çok dalgalanıyor. Şirket, **aynı yapılandırmadaki** VM'lerin sayısının CPU kullanımına göre **otomatik** artıp azalmasını istiyor. Ne kullanmalıdır?

- A) Availability set
- B) Availability zone
- C) Virtual Machine Scale Set
- D) Tek, büyük bir VM

<details><summary>Cevap</summary>

**C.** Scale set aynı VM'lerden oluşan grubu tek kaynak gibi yönetir ve autoscale kurallarıyla instance sayısını otomatik değiştirir.
</details>

#### Soru 7: Tek seçim
Bir availability set'te **fault domain** neyi temsil eder?

- A) Planlı bakım sırasında aynı anda yeniden başlatılan VM grubunu
- B) Ortak güç kaynağını ve ağ anahtarını paylaşan donanım (raf) grubunu
- C) Bir region içindeki ayrı bir datacenter'ı
- D) Bir subscription'daki resource group'ları

<details><summary>Cevap</summary>

**B.** Fault domain fiziksel donanım arızasının etki alanıdır. A update domain'in tanımıdır; C availability zone'dur.
</details>

#### Soru 8: Tek seçim
Azure'un planlı bakım sırasında bir uygulamanın tüm VM'lerini **aynı anda** yeniden başlatmasını önleyen availability set kavramı hangisidir?

- A) Fault domain
- B) Update domain
- C) Availability zone
- D) Scale set

<details><summary>Cevap</summary>

**B.** Update domain'ler planlı bakımda **sırayla** güncellenir; VM'ler farklı UD'lerde olduğu için aynı anda yeniden başlamaz.
</details>

#### Soru 9: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Availability set kullanmak için VM ücretine ek bir ücret ödenir. | ☐ | ☐ |
| b) Bir availability set, bir datacenter'ın tamamen çökmesine karşı koruma sağlar. | ☐ | ☐ |
| c) Bir availability set en fazla 3 fault domain içerebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Availability set'in kendisi ücretsizdir. Set tek bir datacenter içindedir; datacenter arızası için availability zone gerekir. Fault domain sayısı en çok 3'tür (update domain varsayılan 5, en çok 20).
</details>

#### Soru 10: Senaryo
Bir şirket, iki VM'li uygulamasının **datacenter düzeyinde** bir arızada da çalışmaya devam etmesini istiyor; uygulama tek bir region'da kalacak. Hangi seçenek **en uygundur**?

- A) VM'leri aynı availability set'e koymak
- B) VM'leri iki farklı availability zone'a dağıtmak
- C) VM'leri aynı resource group'a koymak
- D) VM'leri daha büyük bir boyuta yükseltmek

<details><summary>Cevap</summary>

**B.** Availability zone'lar bir region içinde fiziksel olarak ayrı datacenter gruplarıdır. Availability set tek datacenter içinde korur; resource group yalnızca mantıksal bir kaptır, fiziksel yerleşimi değiştirmez.
</details>

#### Soru 11: Tek seçim
Bir VM oluşturulurken seçilen **image** neyi belirler?

- A) VM'in bağlanacağı subnet'i
- B) VM'in kurulacağı işletim sistemini ve (varsa) hazır yazılımı
- C) VM'in aylık bütçe sınırını
- D) VM'in bulunacağı availability zone'u

<details><summary>Cevap</summary>

**B.** Image, VM'in kurulacağı işletim sistemini (Windows Server, Ubuntu…) ve varsa hazır yazılımı belirler; Azure Marketplace'ten veya kendi özel image'ından seçilebilir. Subnet NIC/VNet ile, zone ise yerleşim ayarıyla belirlenir.
</details>

#### Soru 12: Eşleştirme
Her gereksinimi doğru seçenekle eşleştirin: **Availability set · Availability zone · VM Scale Set · Deallocate**

1. "Yük artınca yeni VM'ler otomatik eklensin."
2. "Aynı datacenter'da kalsınlar ama aynı raf arızasından etkilenmesinler."
3. "Bir datacenter yansa da uygulama çalışsın."
4. "Gece kullanılmayan VM için compute ücreti ödenmesin."

<details><summary>Cevap</summary>

**1 → VM Scale Set · 2 → Availability set · 3 → Availability zone · 4 → Deallocate** (yalnızca işletim sisteminden kapatmak yetmez; donanım ayrılmış kalır).
</details>

#### Soru 13: Tek seçim
Bir VM'i bir VNet içindeki subnet'e bağlayan ve VM'in **özel IP adresini** taşıyan sanal ağ bileşeni hangisidir?

- A) Network interface (NIC)
- B) Managed disk
- C) Availability set
- D) Resource group

<details><summary>Cevap</summary>

**A.** **NIC**, VM'i subnet'e bağlar ve özel IP'yi taşır; bir VM'de birden çok NIC olabilir. Managed disk depolama, availability set yerleşim, resource group ise mantıksal kap bileşenidir.
</details>

#### Soru 14: Çoklu seçim (iki doğru cevap)
VM Scale Sets ile ilgili **hangi iki** ifade doğrudur?

- A) Scale set'teki VM'ler aynı yapılandırmayla oluşturulur ve merkezi olarak yönetilir
- B) Scale set kullanmak, VM ücretlerine ek olarak ayrı bir lisans ücreti gerektirir
- C) Scale set instance'ları availability zone'lara yayılabilir
- D) Scale set yalnızca Windows VM'leri destekler

<details><summary>Cevap</summary>

**A ve C.** Scale set'in kendisi ek ücretli değildir (B yanlış); Windows ve Linux'u destekler (D yanlış).
</details>

#### Soru 15: Tek seçim
Bir yönetici bir VM'i siliyor ama ay sonunda faturada hâlâ o VM'le ilgili disk ve public IP ücretleri görüyor. En olası neden nedir?

- A) Deallocate edilen VM'ler faturalanmaya devam eder
- B) VM silinse de OS diski ve public IP gibi ilişkili kaynaklar ayrıca silinmedi
- C) Azure silme işlemini 30 gün geciktirir
- D) Availability set ücreti ödenir

<details><summary>Cevap</summary>

**B.** VM'in diskleri, NIC ve public IP ayrı kaynaklardır; VM'i silmek onları her zaman silmez. Bütün ortamı silmenin temiz yolu resource group'u silmektir.
</details>

#### Soru 16: Evet / Hayır

- a) VM Scale Set, ölçekleme kurallarına göre instance sayısını otomatik değiştirebilir. — Evet: ☐ · Hayır: ☐
- b) Availability set, bir datacenter'ın tamamen çökmesine karşı da korur. — Evet: ☐ · Hayır: ☐
- c) Var olan, çalışan bir VM sonradan bir availability set'e eklenebilir. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Hayır.** Availability set tek datacenter içindedir; datacenter arızası için availability zone gerekir. VM availability set'e oluşturulurken yerleştirilir.
</details>

#### Soru 17: Tek seçim
Bir şirketin veritabanı sunucusu yüksek disk performansı (IOPS) ve düşük gecikme gerektiriyor. VM için hangi disk türü **en uygundur**?

- A) Standard HDD
- B) Standard SSD
- C) Premium SSD
- D) Disk gerekmez

<details><summary>Cevap</summary>

**C.** Premium SSD (veya daha uç ihtiyaçlarda Premium SSD v2 / Ultra Disk) üretim veritabanları için yüksek IOPS ve düşük gecikme sağlar.
</details>

#### Soru 18: Senaryo
Bir e-ticaret sitesinin web katmanı için şu gereksinimler var: (1) trafik artınca VM sayısı otomatik artsın, (2) bir datacenter arızasında site ayakta kalsın. Hangi tasarım **her iki** gereksinimi de karşılar?

- A) Tek bir availability set'te iki VM
- B) Availability zone'lara yayılmış ve autoscale kuralı tanımlı bir VM Scale Set
- C) Aynı VM'e bağlı dört veri diski
- D) Tek bir büyük VM ve günlük yedek

<details><summary>Cevap</summary>

**B.** Scale set + autoscale otomatik ölçeği, zone'lara yayılma datacenter arızasına dayanıklılığı sağlar. A otomatik ölçek ve datacenter koruması sunmaz; C yalnızca depolama kapasitesini artırır; D tek arıza noktasıdır.
</details>

#### Soru 19: Eşleştirme
Her VM bileşenini, sağladığı yetenekle eşleştirin: **VM boyutu (size) · Image · Managed disk · NIC**

1. VM'in kurulacağı işletim sistemi ve hazır yazılım
2. vCPU, bellek ve ağ bant genişliği miktarı
3. İşletim sistemi ve verinin kalıcı olarak saklandığı blok depolama
4. VM'i subnet'e bağlayan ve özel IP'yi taşıyan sanal ağ kartı

<details><summary>Cevap</summary>

**1 → Image · 2 → VM boyutu · 3 → Managed disk · 4 → NIC.** Boyut **compute**, disk **storage**, NIC/VNet **networking** bileşenidir; image ise VM'in başlangıç işletim sistemini belirler.
</details>

#### Soru 20: Evet / Hayır

- a) Bir availability set'teki VM'ler, planlı bakım sırasında **update domain**'lere göre sırayla yeniden başlatılır. — Evet: ☐ · Hayır: ☐
- b) Farklı **fault domain**'lerdeki VM'ler aynı güç kaynağını ve ağ anahtarını paylaşır. — Evet: ☐ · Hayır: ☐
- c) Bir VM'in boyutu, ihtiyaç değiştiğinde sonradan büyütülebilir veya küçültülebilir. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Fault domain, ortak güç ve ağ donanımını paylaşan raf grubudur; VM'leri **farklı** fault domain'lere koymak, bir raf arızasının hepsini etkilemesini önler. Boyut değişikliği genellikle yeniden başlatma gerektirir.
</details>

#### Soru 21: Tek seçim
Bir şirket, tüm VM'lerinin kurumun onayladığı güvenlik ayarları ve yazılımlarla **aynı başlangıç durumundan** kurulmasını istiyor. Ne kullanmalıdır?

- A) Her VM için Azure Marketplace'ten farklı bir image seçmek
- B) Onaylı yapılandırmayı içeren **özel (custom) bir VM image**'ı oluşturup tüm VM'leri bundan dağıtmak
- C) VM'leri farklı region'lara dağıtmak
- D) VM'lere public IP atamak

<details><summary>Cevap</summary>

**B.** Image, VM'in işletim sistemini ve kurulu yazılımını belirler; kurumsal bir **custom image**, her VM'in aynı onaylı durumla başlamasını sağlar. Marketplace image'ları hazır işletim sistemleri/ürünler sunar ama kurumun özel ayarlarını içermez.
</details>

---

**Puanlama:** 19-21 → hazırsın · 15-18 → README 3.2 (VM kaynakları ve ücretlendirme) ile 3.4'teki karşılaştırma tablosunu tekrar et · < 15 → konuyu baştan çalış ve 3.5 laboratuvarını yap.

---

➡️ **Sonraki konu:** [2.3 Uygulama Barındırma: App Service, Container'lar, Functions, AVD](../2.3-app-hosting-containers-functions-avd/README.md) · [AZ-900 İçindekiler](../README.md)
