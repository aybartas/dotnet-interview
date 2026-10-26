# C2.12 Azure App Configuration — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Secure, monitor, and troubleshoot Azure solutions* (%20-25) · 13 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, senaryo, kod/CLI tamamlama, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 11/13** (≥ %80).

---

### 1. Tek seçim
Bir uygulamanın şu ayarlarını merkezi olarak saklaman gerekiyor: log seviyesi, sayfa boyutu ve bir üçüncü taraf API'nin **anahtarı**. En doğru depolama dağılımı hangisidir?

- A) Üçünü de App Configuration'da düz key-value olarak
- B) Log seviyesi ve sayfa boyutu App Configuration'da; API anahtarı Key Vault'ta (App Configuration'dan Key Vault referansıyla)
- C) Üçünü de Key Vault secret olarak
- D) Üçünü de `appsettings.json`'da

<details><summary>Cevap</summary>

**B.** Gizli olmayan ayarlar App Configuration'da, secret Key Vault'ta tutulur; App Configuration'daki Key Vault referansı uygulamaya tek bir yapılandırma kaynağı sunar. A secret'ı düz metin yapar; C gizli olmayan ayarları gereksiz yere vault işlemi/yetkisiyle yönetir; D merkezi değildir ve değişiklik için yeniden dağıtım gerektirir.
</details>

### 2. Tek seçim
Aynı `Api:BaseUrl` anahtarının Dev ve Prod ortamlarında farklı değeri olacak ve ikisi aynı store'da duracak. Hangisi bunu sağlar?

- A) İki ayrı key adı (`Api:BaseUrlDev`) kullanmak zorunludur
- B) Anahtara **label** vermek (`Dev`, `Prod`)
- C) Content type değiştirmek
- D) Snapshot oluşturmak

<details><summary>Cevap</summary>

**B.** Label aynı anahtarın varyantlarını ayırır; sağlayıcıda `Select("Api:*", labelFilter: env)` ile ortam seçilir. Content type değerin türüdür (C); snapshot bir kümenin değiştirilemez kopyasıdır, ortam ayrımı için değildir (D). Ayrı anahtar adı çalışır ama label'ın tam amacıdır ve kod değişikliği gerektirir (A zorunlu değildir).
</details>

### 3. Tek seçim
Bir ASP.NET Core uygulaması App Configuration'dan ayar yüklüyor. Ayarlar **birlikte, tutarlı** güncellenmeli ve uygulama yeniden başlatılmamalı. Hangi kalıp önerilir?

- A) Her anahtarı ayrı ayrı `Register` edip kısa aralıkla izlemek
- B) Tek bir **sentinel key** kaydetmek ve `refreshAll: true` kullanmak; sentinel'i değerlerden **sonra** güncellemek
- C) `IOptions<T>` kullanıp her gün restart planlamak
- D) Access key'i döndürmek

<details><summary>Cevap</summary>

**B.** Tek anahtar izlendiği için istek sayısı düşük kalır; sentinel en son yükseltilince uygulama tutarlı bir kümeyi bir arada yükler. A pahalıdır ve yarı güncel küme riski taşır; C yenileme sağlamaz; D ilgisizdir.
</details>

### 4. Kod tamamlama (C#)
Boşluğu doldur; sentinel değişince **tüm** seçili yapılandırma yeniden yüklensin:

```csharp
options.Connect(endpoint, new DefaultAzureCredential())
       .Select("App:*", env)
       .ConfigureRefresh(refresh => refresh
           .Register("App:Sentinel", env, ______: true)
           .SetRefreshInterval(TimeSpan.FromSeconds(30)));
```

- A) `refreshAll`
- B) `reloadAll`
- C) `forceRefresh`
- D) `cacheExpiration`

<details><summary>Cevap</summary>

**A.** `Register(key, label, refreshAll: true)` — `refreshAll` false (varsayılan) ise yalnızca kayıtlı anahtarın kendisi yenilenir. Diğerleri API'de bu anlamda yoktur; yenileme aralığı `SetRefreshInterval` ile verilir.
</details>

### 5. Evet / Hayır

Bir ASP.NET Core uygulaması `AddAzureAppConfiguration` ile dinamik yenileme kurmuş.

| İfade | Evet | Hayır |
|---|---|---|
| a) `app.UseAzureAppConfiguration()` eklenmezse yenileme kontrolleri tetiklenmez. | ☐ | ☐ |
| b) Yenileme kontrolü, aralık dolduktan sonra gelen bir istekle tetiklenir; uygulama tamamen boştaysa kontrol yapılmaz. | ☐ | ☐ |
| c) `IOptions<T>` (singleton) ile okunan değer yenilemeden sonra otomatik güncellenir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** ASP.NET Core'da yenileme middleware tarafından istek hattında tetiklenir (poll tabanlı, yenileme aralığına bağlı). `IOptions<T>` ilk değeri tutar; `IOptionsSnapshot<T>` veya `IOptionsMonitor<T>` gerekir.
</details>

### 6. Tek seçim
Bir App Configuration anahtarı Key Vault referansıdır. Uygulama değeri okurken **403** alıyor; uygulamanın MI'ı App Configuration'da `App Configuration Data Reader` rolüne sahip. Eksik olan nedir?

- A) `App Configuration Data Owner`
- B) Referans verilen vault'ta uygulama kimliği için **Key Vault Secrets User**
- C) Vault için `Contributor`
- D) Store'da soft delete

<details><summary>Cevap</summary>

**B.** Sağlayıcı referansı uygulamanın kimliğiyle **vault'tan** çözer; vault'ta okuma rolü gerekir. Data Owner (A) yazma yetkisidir, sorunu çözmez; Contributor (C) data plane değildir; soft delete (D) erişimle ilgisizdir.
</details>

### 7. Tek seçim
Yeni bir özelliği **kullanıcıların kabaca %20'sine** açmak ve kodu yeniden dağıtmadan oranı artırmak istiyorsun. Kullanıcı başına tutarlılık gerekmiyor. Hangi filtre en basit çözümdür?

- A) TimeWindow
- B) Percentage
- C) Targeting ile gruplar
- D) Özel `IFeatureFilter`

<details><summary>Cevap</summary>

**B.** `Microsoft.Percentage` filtresi isteklerin yüzde N'i için flag'i açar; oran App Configuration'dan değiştirilir. Tutarlı kullanıcı deneyimi gerekseydi **Targeting** (C) seçilirdi. TimeWindow zaman aralığıdır (A); özel filtre gereksiz karmaşıklıktır (D).
</details>

### 8. Tek seçim
Pazarlama, kampanya özelliğinin **1 Kasım 09:00 – 7 Kasım 23:59 (UTC)** arasında otomatik açık kalmasını istiyor; sonrasında kod/ayar müdahalesi olmamalı. Hangi feature flag filtresi?

- A) Percentage
- B) Targeting
- C) TimeWindow
- D) Flag'i `enabled=false` yapıp elle açmak

<details><summary>Cevap</summary>

**C.** `Microsoft.TimeWindow` başlangıç/bitiş tarihleriyle flag'i otomatik açıp kapatır. Diğerleri zamandan bağımsızdır; D elle işlem gerektirir.
</details>

### 9. Çoklu seçim (iki doğru cevap)
200 instance'lı bir serviste 5 saniyelik yenileme aralığı **429** hatalarına yol açıyor. **Hangi iki** değişiklik sorunu çözer?

- A) Yenileme aralığını uzatmak
- B) `Microsoft.AppConfiguration.KeyValueModified` olayını Event Grid ile uygulamaya iletip **push** yenileme kullanmak
- C) `App Configuration Data Owner` rolü vermek
- D) Tüm anahtarları tek bir Key Vault secret'ına taşımak

<details><summary>Cevap</summary>

**A ve B.** Polling istekleri store kotasını tüketir; aralığı uzatmak ve değişiklikleri push ile bildirmek istek sayısını düşürür. Rol, kotayı etkilemez (C); D mimariyi bozar ve vault'un kendi throttling'i vardır.
</details>

### 10. CLI tamamlama
Bir feature flag'e **%20** yüzde filtresi eklemek için komutu tamamla:

```bash
az appconfig feature filter add -n appcs-demo --auth-mode login --feature NewCheckout --label Prod \
  --filter-name ______ --filter-parameters Value=20
```

- A) `Microsoft.Percentage`
- B) `Microsoft.Targeting`
- C) `Microsoft.TimeWindow`
- D) `Microsoft.Sentinel`

<details><summary>Cevap</summary>

**A.** Percentage filtresi `Value` parametresini alır. Targeting `Audience` nesnesi (kullanıcı/grup/varsayılan yüzde), TimeWindow `Start`/`End` ister. "Sentinel" bir filtre değil, yenileme anahtarı kalıbıdır.
</details>

### 11. Sıralama
Dinamik yenilemeyi **tutarlı** biçimde tetiklemek için yönetici adımlarını sırala:

1. `App:Sentinel` değerini yükselt
2. `Db:Pool:MaxSize` değerini güncelle
3. `App:Greeting` değerini güncelle
4. Uygulama bir sonraki yenileme aralığında sentinel değişimini görür ve tüm yapılandırmayı yeniden yükler

<details><summary>Cevap</summary>

**2 → 3 → 1 → 4** (2 ve 3 kendi aralarında yer değiştirebilir). Önce asıl değerler, **sentinel en son**; aksi hâlde uygulama sentinel'i görüp yenilerken eski değerleri okuyabilir ve sonraki değişikliklere kadar tutarsız kalır.
</details>

### 12. Senaryo
Bir release öncesinde Prod ayarlarının (`App:*`, label `Prod`) **değiştirilemez** bir kopyasını dondurmak ve gerekirse ona dönmek istiyorsun. Ne kullanırsın?

- A) Yeni bir label
- B) Snapshot
- C) Key Vault soft delete
- D) Kilitli bir kaynak grubu

<details><summary>Cevap</summary>

**B.** Snapshot, filtre ile seçilen key-value kümesinin değiştirilemez anlık görüntüsüdür; sağlayıcı `SelectSnapshot(...)` ile bu kümeden yükleyebilir. Label (A) değiştirilebilir değerler arasında ayrım yapar; C ve D yapılandırma sürümlemesi değildir.
</details>

### 13. Vaka çalışması

**Durum.** *Fabrikam Shop*, 8 mikroservisi Container Apps'te çalıştırıyor. Gereksinimler:

1. Servis ayarları (sayfa boyutu, zaman aşımı) kod dağıtılmadan değiştirilebilmeli; yeniden başlatma olmamalı.
2. Prod ayarlarını yalnızca platform ekibi değiştirebilmeli; geliştiriciler Dev ayarlarını yönetebilmeli.
3. Ödeme sağlayıcısının API anahtarı App Configuration'dan okunur görünmeli ama düz metin olarak saklanmamalı.
4. Yeni "tek tıkla ödeme" özelliği önce çalışanlara, sonra müşterilerin %10'una, sonra %50'sine açılmalı; aynı müşteri her ziyaretinde aynı deneyimi görmeli.
5. Servisler yalnızca okuma yetkisiyle çalışmalı ve parola/anahtar taşımamalı.

**Soru A.** Ortam ayrımını nasıl kurarsın?
**Soru B.** Gereksinim 3 ve 5'i hangi bileşenlerle karşılarsın?
**Soru C.** Gereksinim 4 için hangi feature flag filtresi ve neden?

<details><summary>Cevap</summary>

**A.** Yetki ayrımı gerektiği için **ortam başına ayrı store** (Dev ve Prod) ve ayrı RBAC: Prod store'unda yalnızca platform ekibine `App Configuration Data Owner`, Dev store'unda geliştiricilere. Label ile tek store basit olurdu ama yetki store düzeyinde olduğundan Prod/Dev ayrımı sağlanamaz. Yenileme için **sentinel key** (`refreshAll: true`) + `IOptionsMonitor`.

**B.** API anahtarı **Key Vault**'ta secret olarak durur; App Configuration'da **Key Vault referansı** (uri) tutulur → düz metin yok. Servisler **managed identity** kullanır: store'da **App Configuration Data Reader**, vault'ta **Key Vault Secrets User** (okuma, en az yetki); parola/connection string yok (`DefaultAzureCredential`/`ManagedIdentityCredential`).

**C.** **Targeting** filtresi: önce grup (`Employees`) %100, sonra varsayılan yüzde %10 → %50. Karar kullanıcı kimliğine göre tutarlı verilir; Percentage filtresi istek başına rastgele olduğundan aynı müşteri farklı sonuç görebilir ve gereksinimi karşılamaz.
</details>

---

**Puanlama:** 12-13 → hazırsın · 9-11 → README 3.4 (yenileme) ve 3.5 (filtre tablosu) bölümlerini tekrar et · < 9 → konuyu baştan çalış ve lab'ı (3.7) uygula.
