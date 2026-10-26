# C1.19 Yedekleme, Felaket Kurtarma ve Taşıma — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *genişletilmiş kapsam (bağlam)* · 14 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, eşleştirme, senaryo). Gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ %80** (14 soruda ≥ 12).

---

### 1. Tek seçim
Bir geliştirici, üretim veritabanı VM'ini yanlışlıkla sildi. Şirket, VM'i **dünkü haline** geri getirebilmek istiyor. Hangi servis uygundur?

- A) Azure Site Recovery
- B) Azure Backup
- C) Azure Data Box
- D) Azure Load Balancer

<details><summary>Cevap</summary>

**B.** Geçmiş bir kurtarma noktasına dönmek **yedekleme** işidir (Azure Backup). Site Recovery sürekli replikasyonla çalışır; silme işlemi de karşıya yansıyacağından geçmişe dönüş sağlamaz.
</details>

### 2. Tek seçim
West Europe bölgesinin tamamen kullanılamaz hale gelmesi durumunda VM'lerin **North Europe'ta** çalıştırılabilmesi için hangi servis kullanılır?

- A) Azure Backup
- B) Azure Migrate
- C) Azure Site Recovery
- D) Azure Advisor

<details><summary>Cevap</summary>

**C.** Site Recovery, VM'leri ikincil bölgeye sürekli replike eder ve felakette **failover** sağlar.
</details>

### 3. Eşleştirme
Her ifadeyi **RPO** veya **RTO** ile eşleştirin.

1. "Felakette en fazla 10 dakikalık veri kaybını kabul edebiliriz."
2. "Hizmet felaketten sonra en geç 2 saatte yeniden çalışıyor olmalı."
3. "Son iyi yedek ile felaket anı arasındaki süre."
4. "Hedeflenen kesinti süresi."

<details><summary>Cevap</summary>

**1 → RPO · 2 → RTO · 3 → RPO · 4 → RTO.** RPO = kabul edilebilir **veri kaybı** (zaman cinsinden), RTO = kabul edilebilir **kesinti süresi**.
</details>

### 4. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir storage account GRS ile yapılandırıldıysa, bir blob'un yanlışlıkla silinmesi ikincil bölgedeki kopyadan geri alınabilir. | ☐ | ☐ |
| b) Site Recovery replikasyonu, bozulmuş verinin hedef bölgeye de kopyalanmasına yol açabilir. | ☐ | ☐ |
| c) Azure Backup yedekleri, kaynağından ayrı bir vault'ta saklanır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Silme ve bozulma işlemleri redundancy kopyalarına da uygulanır; redundancy yedek değildir. Replikasyon da bozulmayı taşıyabilir. Backup, ayrı bir vault'ta zaman içinde kurtarma noktaları tutar.
</details>

### 5. Tek seçim
Bir şirket kurum içi VMware sunucularını önce **keşfetmek**, Azure'a uygunluğunu ve maliyetini **değerlendirmek**, sonra da **taşımak** istiyor. Hangi servis bu üç adım için merkezi hub'dır?

- A) Azure Data Box
- B) Azure Site Recovery
- C) Azure Migrate
- D) Azure Arc

<details><summary>Cevap</summary>

**C.** Azure Migrate: discover → assess → migrate. Data Box yalnızca veri taşır; Site Recovery DR içindir; Arc taşımadan yönetim sağlar.
</details>

### 6. Tek seçim
Bir kurumun ofis hattı 100 Mbps. 500 TB'lık arşivi Azure'a **çevrimdışı** taşımak istiyor. En uygun seçenek hangisidir?

- A) AzCopy ile ağdan kopyalama
- B) Azure Data Box ailesi
- C) Azure Site Recovery
- D) Azure Backup

<details><summary>Cevap</summary>

**B.** Ağ yetersiz ve veri çok büyük: fiziksel cihaz (Data Box). 500 TB'ı 100 Mbps ile göndermek yıl mertebesinde sürer (→ C1.8).
</details>

### 7. Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Azure Backup'ın yedekleyebildiği iş yüklerindendir?

- A) Azure VM'ler
- B) Azure Files paylaşımları
- C) Azure DNS bölge kayıtlarının canlı trafik yönlendirmesi
- D) Bir Application Gateway'in WAF kuralları (canlı trafikle birlikte)

<details><summary>Cevap</summary>

**A ve B.** Azure Backup VM'leri, VM içindeki SQL Server/SAP HANA'yı, Azure Files'ı, blob'ları, diskleri ve kurum içi dosyaları yedekler. C ve D yedekleme iş yükü değildir.
</details>

### 8. Eşleştirme
Her senaryoyu doğru servisle eşleştirin: **Azure Backup · Azure Site Recovery · Azure Migrate · Database Migration Service · Azure Data Box**

1. Kurum içi SQL Server veritabanını minimum kesintiyle Azure SQL Managed Instance'a taşımak
2. VM'lerin dünkü, geçen haftaki ve geçen ayki sürümlerine dönebilmek
3. Bir bölgedeki VM'leri sürekli başka bölgeye replike edip felakette failover yapmak
4. Yüzlerce sunucunun Azure'a uygunluğunu ve tahmini maliyetini çıkarmak
5. Ağ üzerinden aylar sürecek büyük veriyi kargo ile taşımak

<details><summary>Cevap</summary>

**1 → Database Migration Service · 2 → Azure Backup · 3 → Azure Site Recovery · 4 → Azure Migrate · 5 → Azure Data Box.**
</details>

### 9. Sıralama
Azure Migrate ile tipik bir taşıma akışını doğru sıraya koyun: **Taşı (migrate) · Keşfet (discover) · Değerlendir (assess)**

<details><summary>Cevap</summary>

**Keşfet → Değerlendir → Taşı.** Önce ortamın envanteri, sonra boyutlandırma/maliyet/uygunluk analizi, en son taşıma.
</details>

### 10. Tek seçim
"Uygulamayı kodunu değiştirmeden, olduğu gibi Azure VM'lere taşıyoruz." Bu taşıma stratejisinin adı nedir?

- A) Rebuild
- B) Replace
- C) Rearchitect
- D) Rehost (lift and shift)

<details><summary>Cevap</summary>

**D.** Rehost, değişiklik yapmadan taşımadır. Rebuild sıfırdan yazmak, Replace hazır SaaS'a geçmek, Rearchitect mimariyi değiştirmektir.
</details>

### 11. Senaryo
Bir finans şirketi, bir yöneticinin hesabı ele geçirilse bile saldırganın **yedeklerini silemeyeceği** (fidye yazılımı senaryosu) bir yapı istiyor. Aşağıdakilerden hangisi bu hedefe **en çok** hizmet eder?

- A) Storage account'ı LRS'ten GRS'e çevirmek
- B) Azure Backup vault'unda **soft delete** ve **immutable vault** özelliklerini etkinleştirmek
- C) VM'leri bir availability set'e koymak
- D) Azure Advisor önerilerini uygulamak

<details><summary>Cevap</summary>

**B.** Soft delete silinen yedeği süreli geri alınabilir tutar; immutable vault kurtarma noktalarının erken silinmesini/değiştirilmesini engeller. GRS (A) silmeyi de kopyalar; availability set (C) HA içindir.
</details>

### 12. Tek seçim
Bir ekip, Site Recovery için **DR tatbikatı** yapmak istiyor ama üretim iş yüklerini ve replikasyonu **etkilememesini** şart koşuyor. Hangi özellik uygundur?

- A) Failover'ı üretimde çalıştırmak
- B) Test failover
- C) Reprotect
- D) Soft delete

<details><summary>Cevap</summary>

**B.** Test failover, yalıtılmış bir ağda replika VM'i ayağa kaldırır; üretimi ve replikasyonu etkilemez.
</details>

### 13. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Azure Arc, kurum içi sunucuları Azure'a taşımadan Azure'dan yönetmeyi sağlar. | ☐ | ☐ |
| b) RTO, felaket sonrası kaybedilmesine izin verilen veri miktarını belirtir. | ☐ | ☐ |
| c) Site Recovery, kurum içi Hyper-V VM'lerini Azure'a replike edebilir. | ☐ | ☐ |
| d) Bir Recovery Services vault'ta korunan VM'in yedek verileri silinmeden vault silinebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet · d) Hayır.** b RPO'nun tanımıdır (RTO kesinti süresidir). Vault'u silmeden önce korumayı durdurup yedek verisini silmek (ve soft delete'i hesaba katmak) gerekir.
</details>

### 14. Senaryo
Bir şirketin iş birimi şu hedefleri verdi: **RPO = 24 saat, RTO = 1 gün** olan bir raporlama VM'i ve **RPO = 15 dakika, RTO = 1 saat** olan bir sipariş sistemi. Her iş yükü için **en uygun** koruma yaklaşımı hangisidir?

- A) Her ikisi için de yalnızca GRS storage
- B) Raporlama VM'i → günlük Azure Backup; sipariş sistemi → Azure Site Recovery (artı yedek)
- C) Raporlama VM'i → Azure Site Recovery; sipariş sistemi → yalnızca günlük yedek
- D) Her ikisi için de Azure Data Box

<details><summary>Cevap</summary>

**B.** 24 saatlik RPO günlük yedekle karşılanır. 15 dakikalık RPO ve 1 saatlik RTO sürekli replikasyon ve hızlı failover (Site Recovery) gerektirir; geçmişe dönüş için yedek de eklenir. GRS yedek değildir; Data Box taşıma aracıdır.
</details>

---

**Puanlama:** 13-14 → hazırsın · 10-12 → README 3.1 (HA/Backup/DR tablosu) ve 3.4 (redundancy yedek değildir) bölümlerini tekrar et · < 10 → konuyu baştan çalış ve 3.6'daki laboratuvarı uygula.
