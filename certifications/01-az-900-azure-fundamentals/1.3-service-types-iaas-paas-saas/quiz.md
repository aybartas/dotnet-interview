[← Konu anlatımı](README.md) · [AZ-900 İçindekiler](../README.md)

### 📝 Bölüm 1.3 Quiz Soruları

> Teori: [README.md](README.md) · Sınav alanı: *Describe cloud concepts* (%25-30) · 20 soru
>
> Sorular **özgündür** ve sınav formatındadır; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 16/20**.

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir şirket, kurum içindeki bir uygulamayı **hiçbir kod değişikliği yapmadan** ve işletim sistemi üzerinde tam kontrolü koruyarak buluta taşımak istiyor. Hangi hizmet türü **en uygundur**?

- A) SaaS
- B) PaaS
- C) IaaS
- D) Serverless

<details><summary>Cevap</summary>

**C.** Lift-and-shift ve OS üzerinde tam kontrol = IaaS (Azure Virtual Machines). PaaS ve serverless OS erişimi vermez; SaaS hazır uygulamadır.
</details>

#### Soru 2: Eşleştirme
Her ürünü hizmet türüyle eşleştirin: **IaaS · PaaS · SaaS**

1. Azure Virtual Machines
2. Azure SQL Database
3. Microsoft 365
4. Azure App Service
5. Dynamics 365

<details><summary>Cevap</summary>

**1 → IaaS · 2 → PaaS · 3 → SaaS · 4 → PaaS · 5 → SaaS.**
</details>

#### Soru 3: Tek seçim
Bir geliştirme ekibi web API'sini yayınlamak istiyor. Ekip işletim sistemi yaması, sunucu yapılandırması veya runtime kurulumu ile uğraşmak istemiyor; yalnızca kodlarına odaklanmak istiyor. Hangi hizmet türü **en uygundur**?

- A) IaaS
- B) PaaS
- C) SaaS
- D) On-premises

<details><summary>Cevap</summary>

**B.** Kendi kodunu çalıştırıp OS/runtime yönetmemek PaaS'tır (ör. App Service). SaaS hazır uygulama olduğundan kendi API'lerini yayınlamaya uygun değildir.
</details>

#### Soru 4: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) PaaS'ta işletim sistemi güncellemeleri müşterinin sorumluluğundadır. | ☐ | ☐ |
| b) IaaS, üç hizmet türü arasında müşteriye en fazla kontrolü verir. | ☐ | ☐ |
| c) SaaS'ta müşteri, uygulamanın yeni sürümlerini kendisi kurmak zorundadır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** PaaS'ta OS Microsoft'ta; IaaS en fazla kontrolü verir; SaaS'ta uygulama güncellemeleri sağlayıcıdadır.
</details>

#### Soru 5: Tek seçim
Bir şirket SQL Server'ı bir Azure sanal makinesine kurup çalıştırıyor. Bu dağıtım hangi hizmet türüdür?

- A) IaaS
- B) PaaS
- C) SaaS
- D) Serverless

<details><summary>Cevap</summary>

**A.** VM üzerinde SQL Server = IaaS: OS ve SQL Server yamaları müşteride. Yönetilen bir Azure SQL Database olsaydı PaaS olurdu.
</details>

#### Soru 6: Tek seçim
Bir kuruluş, 300 çalışanının **hemen** e-posta, takvim ve ofis uygulamalarını kullanmasını istiyor; hiçbir sunucu veya uygulama yönetmek istemiyor. Hangi hizmet türü **en uygundur**?

- A) IaaS
- B) PaaS
- C) SaaS
- D) Hybrid cloud

<details><summary>Cevap</summary>

**C.** Hazır uygulamayı abonelikle kullanmak SaaS'tır (ör. Microsoft 365). D bir dağıtım modelidir, hizmet türü değildir.
</details>

#### Soru 7: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** serverless bilgi işlemin niteliğidir?

- A) Müşteri, kodun çalıştığı sanal makinelerin boyutunu seçer ve yamalar
- B) Kod, bir olay (HTTP isteği, kuyruk mesajı, dosya yükleme) ile tetiklenir
- C) Kaynaklar yüke göre otomatik ölçeklenir ve boşta kaldığında ücret oluşmaz
- D) Kaynaklar 7/24 ayrılmış kapasiteyle sabit ücretle çalışır

<details><summary>Cevap</summary>

**B ve C.** Serverless: olay tetiklemeli, otomatik ölçekleme (sıfıra kadar), kullanım kadar ücret, altyapı yönetimi yok. A IaaS'ı, D sabit kapasiteli planları anlatır.
</details>

#### Soru 8: Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Serverless'te kodun çalıştığı fiziksel sunucular yoktur. | ☐ | ☐ |
| b) Azure Functions bir serverless bilgi işlem hizmetidir. | ☐ | ☐ |
| c) Serverless'te genellikle yalnızca kodun çalıştığı süre/çağrı kadar ücret ödenir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Sunucular vardır; sağlanmaları ve yönetimleri sağlayıcıdadır. Functions Azure'un serverless bilgi işlem hizmetidir; serverless'te ücret genellikle çağrı ve çalışma süresine göre ölçülür, boşta ücret yoktur.
</details>

#### Soru 9: Senaryo
Bir uygulama, kullanıcılar bir Blob Storage hesabına fotoğraf yüklediğinde her fotoğrafın küçük bir kopyasını oluşturmalı. Yüklemeler gün içinde düzensiz geliyor, gece neredeyse hiç yok. Şirket yalnızca işlem yapıldığında ödeme yapmak istiyor. Hangi çözüm **en uygundur**?

- A) 7/24 çalışan bir sanal makine
- B) Azure Functions
- C) Microsoft 365
- D) Kurum içi bir sunucu

<details><summary>Cevap</summary>

**B.** Olay (blob yükleme) ile tetiklenen, düzensiz yük ve "yalnızca çalışırken ödeme" = serverless, Azure Functions. VM (A) boşta da ücretlidir.
</details>

#### Soru 10: Sıralama
Aşağıdaki hizmet türlerini, **müşterinin yönetim yükü en fazladan en aza** doğru sıralayın: SaaS · IaaS · PaaS

<details><summary>Cevap</summary>

**IaaS → PaaS → SaaS.** Kontrol de aynı sırayla azalır.
</details>

#### Soru 11: Tek seçim
Aşağıdaki senaryolardan hangisi **en tipik** IaaS kullanım senaryosudur?

- A) Çalışanlara hazır bir CRM uygulaması sunmak
- B) Geliştiricilerin, gerektiğinde dakikalar içinde açıp işi bitince silebilecekleri, özel yazılım kurulu test ortamları oluşturmak
- C) Hazır bir e-posta ve ofis paketi kiralamak
- D) Bir web API'sini OS yönetmeden yayınlamak

<details><summary>Cevap</summary>

**B.** Hızla açılıp kapatılan, özel yapılandırmalı test/geliştirme ortamları IaaS'ın klasik senaryosudur. A → SaaS, C → SaaS, D → PaaS.
</details>

#### Soru 12: Tek seçim
Bir şirket, olay olduğunda çalışan küçük bir kod parçasını **sunucu yönetmeden** ve yalnızca kod çalıştığı süre için ödeme yaparak çalıştırmak istiyor. Hangisini seçmelidir ve bu hangi hizmet türüdür?

- A) Azure Virtual Machines — IaaS
- B) Azure Functions — serverless (PaaS)
- C) Microsoft 365 — SaaS
- D) Şirket içi sunucu — on-premises

<details><summary>Cevap</summary>

**B.** Olay tetiklemeli kodu sunucu yönetmeden ve kullanım kadar ücretle çalıştırmak **Azure Functions**'tır (serverless). VM'ler (A) 7/24 açıkken boşta da ücretlenir ve OS yönetimi gerektirir.
</details>

#### Soru 13: Cümle tamamlama
"\_\_\_\_\_\_\_\_\_\_ modelinde bulut sağlayıcısı donanımı, işletim sistemini ve runtime'ı yönetir; müşteri ise uygulamasını ve verisini yönetir."

- A) IaaS
- B) PaaS
- C) SaaS
- D) On-premises

<details><summary>Cevap</summary>

**B.** PaaS'ın tanımı: platform (OS, runtime, ara katman) sağlayıcıda, uygulama ve veri müşteride.
</details>

#### Soru 14: Senaryo
Bir yazılım ekibi kendi geliştirdiği uygulamayı çalıştıracak ve **en az yönetim yükünü** istiyor. Şıklarda şunlar var: (A) Azure Virtual Machines, (B) Azure App Service, (C) Microsoft 365, (D) kendi datacenter'ları. Hangisini seçmelidir?

<details><summary>Cevap</summary>

**B.** "En az yönetim yükü" sorusunda SaaS (C) ancak ihtiyaç **hazır bir uygulamayla** karşılanıyorsa doğrudur; burada ekip **kendi uygulamasını** çalıştıracak, bu yüzden en az yükü PaaS (App Service) verir.
</details>

#### Soru 15: Evet / Hayır

- a) Bir iş yükü önce lift-and-shift ile IaaS'a taşınıp sonra PaaS'a dönüştürülebilir. — Evet: ☐ · Hayır: ☐
- b) SaaS'ta, kullanıcı hesaplarının ve erişim izinlerinin yönetimi sağlayıcının sorumluluğundadır. — Evet: ☐ · Hayır: ☐
- c) Çok yüksek ve sürekli yükte, 7/24 açık bir kaynak serverless'ten daha ekonomik olabilir. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Kademeli taşıma (önce rehost, sonra refactor) yaygın bir stratejidir. Hesaplar ve kimlikler her modelde müşteridedir (→ C1.1). Serverless düşük/düzensiz yükte ucuzdur; sürekli yüksek yükte çağrı + GB-saniye toplamı sabit bir plandan fazla olabilir.
</details>

#### Soru 16: Eşleştirme
Her senaryoyu en uygun hizmet türüyle eşleştirin: **IaaS · PaaS · SaaS**

1. Bir ekip, kendi yazdığı web API'sini işletim sistemi yönetmeden yayınlamak istiyor.
2. Bir şirket, özel bir sürücü gerektiren eski bir uygulamayı değiştirmeden taşımak istiyor.
3. Çalışanlara hazır bir e-posta ve takvim uygulaması sunulacak.
4. Geliştiriciler dakikalar içinde açıp işi bitince silecekleri özel yapılandırmalı test sunucularına ihtiyaç duyuyor.

<details><summary>Cevap</summary>

**1 → PaaS** (App Service) · **2 → IaaS** (VM, lift-and-shift) · **3 → SaaS** (Microsoft 365) · **4 → IaaS** (test/geliştirme ortamı VM'leri).
</details>

#### Soru 17: Tek seçim
Bir uygulama **Azure App Service** üzerinde çalışıyor. Aşağıdakilerden hangisi **müşterinin** sorumluluğunda kalır?

- A) App Service'in çalıştığı sunucuların işletim sistemi yamaları
- B) Uygulamanın kodu ve uygulamanın sakladığı veri
- C) Fiziksel sunucuların donanım arızaları
- D) Runtime'ın (ör. .NET) platform düzeyindeki kurulumu ve bakımı

<details><summary>Cevap</summary>

**B.** PaaS'ta Microsoft altyapıyı, işletim sistemini ve runtime'ı yönetir; müşteri **kodunu, verisini** ve uygulama yapılandırmasını yönetir. A, C ve D Microsoft'tadır.
</details>

#### Soru 18: Evet / Hayır

- a) Azure Virtual Machines, müşteriye işletim sistemi üzerinde tam kontrol veren bir IaaS hizmetidir. — Evet: ☐ · Hayır: ☐
- b) Microsoft 365 kullanan bir şirket, kullanıcı hesaplarının ve verinin güvenliğinden sorumlu değildir. — Evet: ☐ · Hayır: ☐
- c) Serverless bir fonksiyon çalışmıyorken genellikle compute ücreti doğmaz. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** SaaS'ta bile hesaplar, kimlikler ve veri müşteride kalır (→ C1.1). Serverless'te ücret çağrı ve çalışma süresine göredir; boşta ücret yoktur.
</details>

#### Soru 19: Tek seçim
Aşağıdakilerden hangisi **PaaS**'ın IaaS'a göre bir **dezavantajıdır**?

- A) İşletim sistemi yamalarının müşteri tarafından yapılması gerekir
- B) Platformun desteklemediği özel bir bileşen veya işletim sistemi ayarı kurulamaz
- C) Uygulama ölçeklenemez
- D) Fiziksel sunucuları müşteri satın almak zorundadır

<details><summary>Cevap</summary>

**B.** PaaS daha az yönetim yükü getirir ama **daha az kontrol** demektir: OS'a erişim yoktur, platformun izin vermediği bileşen kurulamaz. A IaaS'ın yüküdür; PaaS yerleşik ölçekleme sunar (C yanlış); fiziksel sunucular hiçbir bulut modelinde müşteride değildir (D yanlış).
</details>

#### Soru 20: Senaryo
Bir ekip; olay tetiklemeli kısa bir kodu **sunucu sağlamadan**, yük artınca **otomatik ölçeklenen** ve **yalnızca çalıştığı süre için ücretlenen** bir hizmette çalıştırmak istiyor. Bu nitelikler hangi modeli tanımlar ve Azure'daki örneği nedir?

- A) IaaS — Azure Virtual Machines
- B) Serverless — Azure Functions
- C) SaaS — Microsoft 365
- D) Private cloud — kurum içi sunucu

<details><summary>Cevap</summary>

**B.** Olay tetiklemeli çalışma, otomatik ölçekleme (sıfıra kadar), kullanım kadar ücret ve altyapı yönetiminin olmaması **serverless**'in nitelikleridir; Azure'daki örneği **Azure Functions**'tır. Serverless, PaaS'ın en "yönetimsiz" biçimidir.
</details>

---

**Puanlama:** 18-20 → hazırsın · 14-17 → README 3.6'daki karar sırasını ve "aynı teknoloji, farklı tür" tablosunu tekrar et · < 14 → konuyu baştan çalış, sonra bu quiz'i yeniden çöz.

---

➡️ **Sonraki konu:** [2.1 Azure Temel Mimari Bileşenleri](../2.1-core-architecture/README.md) · [AZ-900 İçindekiler](../README.md)
