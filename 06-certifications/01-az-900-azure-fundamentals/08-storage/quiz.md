# C1.8 Azure Storage: Blob, Files, Taşıma ve Migration — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure architecture and services* (%35-40) · 20 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, eşleştirme, senaryo); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 16/20**.

---

### 1. Tek seçim
Bir uygulama kullanıcıların yüklediği fotoğraf ve videoları saklayacak. Hangi Azure Storage servisi **en uygundur**?

- A) Azure Files
- B) Blob Storage
- C) Queue Storage
- D) Table Storage

<details><summary>Cevap</summary>

**B.** Görsel, video, yedek gibi yapılandırılmamış veri (nesne) Blob Storage'ın işidir. Files dosya paylaşımı, Queue mesaj kuyruğu, Table anahtar-değer verisidir.
</details>

### 2. Tek seçim
Bir şirket, birden çok Windows ve Linux VM'in **SMB protokolüyle aynı anda bağlanabileceği** yönetilen bir dosya paylaşımına ihtiyaç duyuyor. Hangi servisi kullanmalıdır?

- A) Azure Files
- B) Blob Storage
- C) Azure Managed Disks
- D) Queue Storage

<details><summary>Cevap</summary>

**A.** Azure Files, SMB (ve NFS) ile bağlanan tam yönetilen dosya paylaşımıdır. Managed disk tek bir VM'e bağlı blok depolamadır.
</details>

### 3. Eşleştirme
Her ihtiyacı storage servisiyle eşleştirin: **Blob · Files · Queue · Table · Disks**

1. VM'in işletim sistemi diski
2. Siparişleri işleyen arka plan servisine iş öğeleri iletmek
3. Şemasız, anahtar-değer biçiminde cihaz meta verisi
4. Log ve yedek dosyalarını saklamak
5. Ağ sürücüsü olarak bağlanan paylaşılan klasör

<details><summary>Cevap</summary>

**1 → Disks · 2 → Queue · 3 → Table · 4 → Blob · 5 → Files.**
</details>

### 4. Tek seçim
Aşağıdaki redundancy seçeneklerinden hangisi verinin **üç kopyasını tek bir datacenter** içinde tutar?

- A) ZRS
- B) GRS
- C) LRS
- D) GZRS

<details><summary>Cevap</summary>

**C.** LRS (locally redundant storage) birincil bölgede tek datacenter içinde 3 kopya tutar; datacenter felaketine dayanmaz.
</details>

### 5. Senaryo
Bir şirketin verisi, regülasyon gereği **yalnızca bir bölgede** kalmalı; ancak bölgedeki bir datacenter (availability zone) tamamen çökerse de veriye **kesintisiz** erişilebilmeli. Hangi seçenek **en uygundur**?

- A) LRS
- B) ZRS
- C) GRS
- D) RA-GZRS

<details><summary>Cevap</summary>

**B.** ZRS veriyi tek bölgedeki üç availability zone'a yayar: zone arızasında erişim sürer ve veri bölgeden çıkmaz. GRS/RA-GZRS veriyi ikinci bir bölgeye kopyalar (gereksinime aykırı); LRS zone arızasına dayanmaz.
</details>

### 6. Senaryo
Bir şirket, birincil Azure bölgesi tamamen kullanılamaz hale gelse bile, **failover beklemeden** verisini okumaya devam edebilmek istiyor. Hangi seçeneği seçmelidir?

- A) GRS
- B) ZRS
- C) RA-GRS
- D) LRS

<details><summary>Cevap</summary>

**C.** RA-GRS (read-access geo-redundant), ikincil bölgeden sürekli okuma izni verir. GRS'te ikincil bölge ancak failover sonrası erişilebilir olur.
</details>

### 7. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) GRS ile ikincil bölgedeki veri, failover yapılmadan her zaman okunabilir. | ☐ | ☐ |
| b) GZRS, birincil bölgede veriyi availability zone'lara yayar ve ikincil bölgeye de kopyalar. | ☐ | ☐ |
| c) ZRS, bölgesel bir felakete karşı koruma sağlar. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** Sürekli okuma yalnızca RA- seçeneklerinde vardır; GZRS = ZRS (birincil) + ikincil bölgede LRS; ZRS tek bölgededir.
</details>

### 8. Tek seçim
GRS ve GZRS'te ikincil bölge nasıl belirlenir?

- A) Müşteri dünyadaki herhangi bir bölgeyi seçer
- B) Birincil bölgenin region pair'i kullanılır
- C) Her zaman aynı bölgedeki başka bir zone kullanılır
- D) Azure her ay farklı bir bölge seçer

<details><summary>Cevap</summary>

**B.** Coğrafi redundancy'de ikincil bölge, birincil bölgenin eşleşik bölgesidir (region pair → C1.4).
</details>

### 9. Tek seçim
Yıllarca saklanması gereken ve **yılda bir kez bile okunmayabilecek** uyumluluk kayıtları için **en düşük depolama maliyetini** hangi access tier sağlar?

- A) Hot
- B) Cool
- C) Cold
- D) Archive

<details><summary>Cevap</summary>

**D.** Archive en ucuz depolama tier'ıdır; karşılığında veri çevrimdışıdır ve okumadan önce rehydrate gerekir — nadiren okunan uyumluluk kaydı için uygundur.
</details>

### 10. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Archive tier'daki bir blob, Hot tier'daki gibi anında indirilebilir. | ☐ | ☐ |
| b) Archive'daki bir blob'u okumak için önce Hot, Cool veya Cold tier'a rehydrate etmek gerekir ve bu saatler sürebilir. | ☐ | ☐ |
| c) Cool tier'daki bir blob'u 30 günden önce silmek ek ücret doğurabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Archive çevrimdışıdır; rehydrate standart öncelikte saatler sürebilir; Cool (30), Cold (90), Archive (180 gün) için erken silme ücreti vardır.
</details>

### 11. Sıralama
Aşağıdaki blob access tier'larını **depolama maliyeti en yüksekten en düşüğe** sıralayın: Archive · Hot · Cold · Cool

<details><summary>Cevap</summary>

**Hot → Cool → Cold → Archive.** Erişim (okuma) maliyeti ise ters yönde artar.
</details>

### 12. Senaryo
Bir web sitesinin ana sayfa görselleri günde on binlerce kez okunuyor. Bu görseller hangi tier'da tutulmalıdır?

- A) Hot
- B) Cool
- C) Cold
- D) Archive

<details><summary>Cevap</summary>

**A.** Sık erişilen veri için erişim maliyeti en düşük olan Hot tier doğrudur; Cool/Cold/Archive'da okuma ücretleri yüksek olur ve Archive'da veri anında okunamaz.
</details>

### 13. Tek seçim
Bir yönetici, yerel bir klasördeki binlerce dosyayı **komut satırından, betikle** bir blob container'a kopyalamak istiyor. Hangi aracı kullanmalıdır?

- A) Azure Storage Explorer
- B) AzCopy
- C) Azure File Sync
- D) Azure Data Box

<details><summary>Cevap</summary>

**B.** AzCopy, blob/dosya kopyalamak için komut satırı aracıdır (`azcopy copy … --recursive`). Storage Explorer grafik araçtır; File Sync Windows Server ↔ Azure Files senkronudur; Data Box fiziksel cihazdır.
</details>

### 14. Tek seçim
Bir kullanıcı, blob ve dosya paylaşımlarına **grafik bir masaüstü uygulamasıyla** göz atmak, dosya yükleyip indirmek istiyor. Hangi araç **en uygundur**?

- A) AzCopy
- B) Azure Storage Explorer
- C) Azure Migrate
- D) Azure Cloud Shell

<details><summary>Cevap</summary>

**B.** Azure Storage Explorer; Windows, macOS ve Linux'ta çalışan grafik yönetim aracıdır (arka planda AzCopy kullanır).
</details>

### 15. Senaryo
Bir şirketin şubelerinde Windows Server dosya sunucuları var. Şirket, dosyaların merkezi olarak Azure'da tutulmasını, ama şubelerdeki kullanıcıların **sık kullanılan dosyalara yerel sunucudan hızlı erişmesini** ve seyrek dosyaların yerel diskte yer kaplamamasını istiyor. Ne kullanmalıdır?

- A) AzCopy ile her gece kopyalama
- B) Azure File Sync (cloud tiering ile)
- C) Azure Data Box
- D) Blob Storage Archive tier

<details><summary>Cevap</summary>

**B.** Azure File Sync, Windows Server'ı Azure Files'ın önbelleği yapar; iki yönlü senkron ve cloud tiering (seyrek dosyalar yalnızca bulutta) sağlar.
</details>

### 16. Senaryo
Bir şirket 250 TB veriyi Azure'a taşımak istiyor; internet bağlantısı yavaş ve taşıma ağ üzerinden aylar sürecek. **En uygun** çözüm nedir?

- A) AzCopy
- B) Azure Storage Explorer
- C) Azure Data Box
- D) Azure File Sync

<details><summary>Cevap</summary>

**C.** Büyük veri + yetersiz ağ = Azure Data Box: veri fiziksel cihaza kopyalanır, cihaz Azure datacenter'ına gönderilir ve storage account'a yüklenir.
</details>

### 17. Tek seçim
Bir şirket, datacenter'ındaki yüzlerce sunucuyu **keşfetmek**, Azure'da uygun **boyutlarını ve tahmini maliyetini değerlendirmek** ve ardından taşımak için merkezi bir araç arıyor. Hangisi?

- A) Azure Data Box
- B) Azure Migrate
- C) AzCopy
- D) Azure Advisor

<details><summary>Cevap</summary>

**B.** Azure Migrate, keşif (discovery), değerlendirme (assessment) ve taşıma için merkezi hub'dır. Data Box veri taşıma cihazıdır; Advisor zaten Azure'da olan kaynaklar için öneri verir.
</details>

### 18. Çoklu seçim (iki doğru cevap)
Azure Data Box hakkında **hangi iki** ifade doğrudur?

- A) Veriyi hem Azure'a aktarmak hem de Azure'dan dışarı almak için kullanılabilir.
- B) Sunucuları keşfedip boyutlandırma önerisi üretir.
- C) Ağ bağlantısının yetersiz veya pahalı olduğu büyük veri taşımaları için uygundur.
- D) Windows Server ile Azure Files arasında sürekli senkron sağlar.

<details><summary>Cevap</summary>

**A ve C.** Data Box içe ve dışa aktarmayı destekler ve büyük, ağdan taşınması zor veri için tasarlanmıştır. B Azure Migrate'i, D Azure File Sync'i anlatır.
</details>

### 19. Tek seçim
Bir ekip; Blob, Files, Queue ve Table servislerini **tek bir hesapta** kullanmak ve ileride GRS'e geçebilmek istiyor. Hangi storage account türünü seçmelidir?

- A) Premium block blobs
- B) Premium file shares
- C) Standard general-purpose v2
- D) Premium page blobs

<details><summary>Cevap</summary>

**C.** Standard general-purpose v2; tüm servisleri ve tüm redundancy seçeneklerini (LRS'ten RA-GZRS'e) destekleyen, çoğu senaryo için önerilen türdür. Premium türler belirli bir servise özeldir ve yalnızca LRS/ZRS sunar.
</details>

### 20. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Storage account adı Azure genelinde benzersiz olmalıdır. | ☐ | ☐ |
| b) Premium storage account'lar GRS redundancy'yi destekler. | ☐ | ☐ |
| c) Bir blob'un access tier'ı blob düzeyinde değiştirilebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Ad, endpoint URL'sinin parçasıdır (`<ad>.blob.core.windows.net`); Premium türler yalnızca LRS/ZRS sunar; tier blob düzeyinde ayarlanabilir (`az storage blob set-tier`, `SetAccessTier`).
</details>

---

**Puanlama:** 18-20 → hazırsın · 14-17 → README 3.2 (redundancy) ve 3.3 (tier) tablolarını tekrar et · < 14 → konuyu baştan çalış, `dotnet run -- 1.8 all` çıktısını incele.
