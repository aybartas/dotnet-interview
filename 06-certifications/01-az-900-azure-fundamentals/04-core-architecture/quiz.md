# C1.4 Azure Temel Mimari Bileşenleri — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure architecture and services* (%35-40) · 20 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, eşleştirme, senaryo); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 16/20**.

---

### 1. Tek seçim
Bir şirket, uygulamasının **aynı region içindeki** tek bir datacenter'ın elektrik kesintisinden etkilenmemesini istiyor. Ne kullanmalıdır?

- A) Region pair
- B) Availability zone
- C) Sovereign region
- D) Management group

<details><summary>Cevap</summary>

**B.** Availability zone'lar bir region içinde bağımsız güç, soğutma ve ağa sahip fiziksel olarak ayrı datacenter gruplarıdır; kaynakları zone'lara yaymak datacenter arızasına karşı korur. Region pair bölgesel felaket içindir.
</details>

### 2. Tek seçim
Bir şirket, **bütün bir Azure region'ını** etkileyen bir felakette bile verilerinin başka bir bölgede kopyasının bulunmasını istiyor ve verinin aynı coğrafya içinde kalması gerekiyor. Hangi kavram bunu destekler?

- A) Availability zone
- B) Region pair
- C) Resource group
- D) Availability set

<details><summary>Cevap</summary>

**B.** Region pair'ler aynı coğrafyada, birbirinden uzak iki bölgedir; GRS gibi servisler ikincil kopyayı eşleşik bölgede tutar ve veri coğrafya içinde kalır.
</details>

### 3. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Availability zone destekleyen bir region'da en az üç zone bulunur. | ☐ | ☐ |
| b) Her Azure region'ı availability zone destekler. | ☐ | ☐ |
| c) Availability zone'ların her birinin bağımsız gücü, soğutması ve ağı vardır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Zone'lar yalnızca zone destekleyen region'larda vardır; orada en az üç tanedir ve her biri bağımsız altyapıya sahiptir.
</details>

### 4. Çoklu seçim (iki doğru cevap)
Region pair'lerin **hangi ikisi** faydasıdır?

- A) Planlı Azure güncellemeleri eşleşik bölgelere sırayla uygulanır
- B) Eşleşik bölgeler arasındaki tüm trafik ücretsizdir
- C) Geniş çaplı bir kesintide eşleşik bölgelerden birinin kurtarılmasına öncelik verilir
- D) Eşleşik bölgeler aynı datacenter'ı paylaşır

<details><summary>Cevap</summary>

**A ve C.** Sıralı güncelleme ve kurtarma önceliği region pair'in temel faydalarıdır. Bölgeler arası trafik ücretlidir (B); eşleşik bölgeler mümkün olduğunca ~300 mil uzaktadır, datacenter paylaşmaz (D).
</details>

### 5. Tek seçim
ABD'deki bir kamu kurumu, fiziksel ve ağ olarak ana Azure'dan ayrılmış, yalnızca taranmış ABD personelinin işlettiği ve kamu uyumluluk gereksinimlerini karşılayan bir bulut istiyor. Hangisini kullanmalıdır?

- A) Azure Government
- B) Azure China (21Vianet)
- C) Bir availability zone
- D) Ayrı bir subscription

<details><summary>Cevap</summary>

**A.** Azure Government, ABD kamu kurumları için bir **sovereign region**'dır. Ayrı bir subscription (D) yalnızca mantıksal ayrımdır; fiziksel ve yasal ayrım sağlamaz.
</details>

### 6. Tek seçim
Aşağıdaki servislerden hangisi **global** bir servistir, yani belirli bir region seçilmeden kullanılır?

- A) Azure Virtual Machines
- B) Azure SQL Database
- C) Microsoft Entra ID
- D) Azure Storage account

<details><summary>Cevap</summary>

**C.** Entra ID (ve Front Door, Traffic Manager, Azure DNS) global servislerdir. VM, SQL Database ve storage account bir region'da oluşturulur.
</details>

### 7. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir kaynak aynı anda iki resource group'a ait olabilir. | ☐ | ☐ |
| b) Bir resource group başka bir resource group'un içine yerleştirilebilir. | ☐ | ☐ |
| c) Bir resource group'taki kaynaklar farklı region'larda olabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Her kaynak tam olarak bir gruptadır; gruplar iç içe olmaz; RG'nin konumu yalnızca meta veri içindir, kaynaklar farklı region'larda olabilir.
</details>

### 8. Tek seçim
Bir yönetici, içinde bir VM, bir VNet ve bir storage account bulunan `rg-test` resource group'unu siliyor. Ne olur?

- A) Yalnızca resource group silinir, kaynaklar varsayılan gruba taşınır
- B) Resource group ve içindeki tüm kaynaklar silinir
- C) İçinde kaynak olan bir resource group silinemez
- D) Yalnızca VM silinir

<details><summary>Cevap</summary>

**B.** Bir resource group silindiğinde içindeki tüm kaynaklar da silinir. Bu yüzden önemli gruplara resource lock konur (→ C1.12).
</details>

### 9. Tek seçim
Bir şirketin 30 subscription'ı var. Güvenlik ekibi, **tüm subscription'larda** yalnızca belirli region'larda kaynak oluşturulmasına izin veren bir politikayı **tek bir atamayla** uygulamak istiyor. Politika nereye atanmalıdır?

- A) Her resource group'a
- B) Bir management group'a
- C) Her kaynağa
- D) Bir availability zone'a

<details><summary>Cevap</summary>

**B.** Management group'a atanan Policy ve RBAC, altındaki tüm subscription'lara ve onların kaynaklarına miras kalır.
</details>

### 10. Sıralama
Azure yönetim hiyerarşisini **en geniş kapsamdan en dara** doğru sıralayın: Resource group · Resource · Management group · Subscription

<details><summary>Cevap</summary>

**Management group → Subscription → Resource group → Resource.** (En üstte Entra tenant ve onun root management group'u bulunur.)
</details>

### 11. Tek seçim
Bir şirket, departmanlarının Azure maliyetlerini **ayrı faturalarla** takip etmek ve her departmanın erişimini birbirinden ayırmak istiyor. En uygun yapı hangisidir?

- A) Her departman için ayrı bir availability zone
- B) Her departman için ayrı bir subscription
- C) Tüm kaynakları tek bir resource group'ta toplamak
- D) Her departman için ayrı bir region

<details><summary>Cevap</summary>

**B.** Subscription hem **billing boundary** hem **access control boundary**'dir. (Maliyeti tek subscription içinde de tag'lerle raporlamak mümkündür → C1.11, ama ayrı fatura ve erişim sınırı subscription ile sağlanır.)
</details>

### 12. Çoklu seçim (iki doğru cevap)
Bir kuruluşun **birden çok subscription** kullanmasının **hangi ikisi** geçerli bir nedendir?

- A) Dev, test ve prod ortamlarını faturalama ve erişim açısından ayırmak
- B) Bir resource group'u başka bir resource group'un içine koyabilmek
- C) Subscription başına kaynak limitlerine (kota) takılmak
- D) Availability zone'ları etkinleştirmek

<details><summary>Cevap</summary>

**A ve C.** Ortam/departman ayrımı, faturalama ve kota/limitler birden çok subscription'ın klasik nedenleridir. RG'ler iç içe olmaz (B); zone kullanımı subscription sayısından bağımsızdır (D).
</details>

### 13. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Her Entra tenant'ında tek bir root management group bulunur. | ☐ | ☐ |
| b) Bir subscription aynı anda iki management group'un altında olabilir. | ☐ | ☐ |
| c) Management group hiyerarşisi (root ve subscription seviyesi hariç) 6 seviyeye kadar derinleşebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Her management group ve subscription tek bir ebeveyne bağlıdır; derinlik root ve subscription hariç 6 seviyedir.
</details>

### 14. Tek seçim
Bir resource group üzerinde bir kullanıcıya **Reader** rolü atanıyor. Bu atama nereyi etkiler?

- A) Yalnızca resource group nesnesinin kendisini
- B) Resource group'u ve içindeki tüm kaynakları
- C) Subscription'daki tüm resource group'ları
- D) Hiçbir kaynağı; rol yalnızca kaynak düzeyinde atanabilir

<details><summary>Cevap</summary>

**B.** RBAC atamaları kapsamdan **aşağı** miras alınır: resource group'a verilen rol içindeki tüm kaynaklarda geçerlidir, ama üstteki subscription'a yayılmaz (→ C1.9).
</details>

### 15. Senaryo
Bir şirket "datacenter seçerek" kaynak oluşturmak istiyor. Bu Azure'da nasıl karşılanır?

- A) Kaynak oluştururken datacenter adını seçerek
- B) Müşteri datacenter seçemez; region ve (destekleniyorsa) availability zone seçer
- C) Yalnızca Azure Government'ta datacenter seçilebilir
- D) Management group ile datacenter atanır

<details><summary>Cevap</summary>

**B.** Datacenter'lar müşteriye doğrudan açılmaz; region'lar ve availability zone'lar halinde gruplanır.
</details>

### 16. Eşleştirme
Her ihtiyacı doğru kavramla eşleştirin: **Availability zone · Region pair · Resource group · Management group · Subscription**

1. Bir uygulamanın test ortamındaki tüm kaynakları tek komutla silmek
2. Şirket genelindeki 25 subscription'a aynı RBAC atamasını uygulamak
3. Aynı region içinde datacenter arızasına dayanmak
4. Pazarlama departmanı için ayrı fatura
5. Bölgesel felakette ikinci bölgeden devam etmek

<details><summary>Cevap</summary>

**1 → Resource group · 2 → Management group · 3 → Availability zone · 4 → Subscription · 5 → Region pair.**
</details>

### 17. Tek seçim
**Zone-redundant** bir servis ile **zonal** bir servis arasındaki fark nedir?

- A) Zone-redundant servisi müşteri tek bir zone'a sabitler; zonal servisi platform otomatik çoğaltır
- B) Zonal servisi müşteri belirli bir zone'a sabitler; zone-redundant servisi platform zone'lar arasında otomatik çoğaltır
- C) İkisi aynı şeydir
- D) Zonal servisler yalnızca sovereign region'larda bulunur

<details><summary>Cevap</summary>

**B.** Zonal: VM, disk, public IP gibi kaynakları sen bir zone'a yerleştirirsin (dayanıklılık için birden çok zone'a ayrı kopyalar koyarsın). Zone-redundant: ZRS storage veya zone-redundant SQL Database gibi servislerde platform kopyaları zone'lara kendisi dağıtır.
</details>

### 18. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Her Azure region'ının bir eşleşik bölgesi vardır. | ☐ | ☐ |
| b) Bir resource group'taki kaynak başka bir resource group'a taşınabilir. | ☐ | ☐ |
| c) Resource group'a verilen tag'ler içindeki kaynaklara otomatik olarak miras kalır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Bazı yeni region'ların eşi yoktur. Kaynaklar RG ve subscription'lar arasında taşınabilir (bazı türlerde kısıtlarla). Tag'ler varsayılan olarak miras alınmaz; bunun için Azure Policy kullanılır (→ C1.11, C1.12).
</details>

### 19. Çoklu seçim (iki doğru cevap)
Bir şirket yeni uygulaması için region seçiyor. **Hangi ikisi** region seçiminde dikkate alınması gereken kriterdir?

- A) Kullanıcılara olan ağ gecikmesi
- B) Subscription'ın adı
- C) Verinin belirli bir ülkede kalmasını gerektiren regülasyonlar
- D) Resource group sayısı

<details><summary>Cevap</summary>

**A ve C.** Gecikme ve veri ikametgâhı/regülasyon temel kriterlerdir; servis uygunluğu, fiyat ve zone/pair durumu da dikkate alınır. Subscription adı ve RG sayısı region seçimini etkilemez.
</details>

### 20. Senaryo
Bir şirketin kökte "Allowed locations" politikası atanmış bir management group'u, onun altında `MG-Prod`, onun altında `sub-prod` subscription'ı ve içinde `rg-api` resource group'u var. `rg-api`'de izin verilmeyen bir region'da VM oluşturmaya çalışan bir kullanıcının subscription'da **Owner** rolü var. Ne olur?

- A) Owner olduğu için VM oluşturulur
- B) Politika üst kapsamdan miras alındığı için istek reddedilir
- C) Politika yalnızca management group'un kendisini etkiler
- D) VM oluşturulur ama otomatik olarak izinli region'a taşınır

<details><summary>Cevap</summary>

**B.** Management group'a atanan Policy, tüm alt subscription, RG ve kaynaklara miras kalır. Policy **kaynağın** nasıl olabileceğini belirler; RBAC'taki Owner rolü onu aşmaz (→ C1.12).
</details>

---

**Puanlama:** 18-20 → hazırsın · 14-17 → README 3.3 (zone vs pair) ve 3.5 (RG kuralları) tablolarını tekrar et · < 14 → konuyu baştan çalış ve 3.7 laboratuvarını yap.
