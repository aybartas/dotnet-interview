# J6.3 Configuration & Options Temelleri — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ ASP.NET Core'da configuration kaynaklarının öncelik sırası nedir?
Varsayılan sıra (düşükten yükseğe): `appsettings.json` → `appsettings.{Environment}.json` → **User Secrets** (yalnızca Development) → **environment variables** → **komut satırı argümanları**. Kural **"sonra eklenen kazanır"**: aynı key birden çok kaynakta varsa sonuncunun değeri okunur, diğerleri ezilir. Bu sayede JSON'daki varsayılan, ortam dosyasıyla; o da env var ile; o da gerektiğinde komut satırıyla değiştirilebilir. Sırayı `builder.Configuration.AddXxx(...)` çağrılarının sırası belirler; kendi kaynağınızı sona eklerseniz o kazanır.

### 2. ⭐ Secret'ları (şifre, API anahtarı, connection string) nerede saklarsın?
**Kaynak kodda ve git'te asla.** `appsettings.json` commit edilir, yani herkese açık kabul edilir. Development'ta **User Secrets** (`dotnet user-secrets`, kullanıcı profilinde repo dışında bir JSON), CI/container/Kubernetes'te **environment variable** (K8s `Secret`, CI secret store), Production'da **Azure Key Vault** (Managed Identity ile şifresiz erişim) veya platformun secret mekanizması. Yanlışlıkla commit edilen secret silinmekle kurtulmaz — git geçmişinde durur — **hemen rotate edilir**. Ayrıca `.gitignore`'a yerel secret dosyaları eklenir ve secret scanning/push protection açılır.

### 3. ⭐ `Logging:LogLevel:Default` gibi hiyerarşik bir key'i environment variable olarak nasıl verirsin?
`:` yerine **çift alt çizgi**: `Logging__LogLevel__Default=Warning`. Nedeni, bazı işletim sistemleri/shell'lerde env var adında `:` kullanılamamasıdır; `__` her yerde çalışır ve environment variables provider'ı onu otomatik `:`'ya çevirir. Dizi elemanları indeksle gider: `Endpoints__1__Name`. Komut satırında ise `--Logging:LogLevel:Default=Warning` yazılır.

### 4. ⭐ User Secrets nedir? Production'da kullanılır mı?
Geliştirme makinesinde, proje dışında (`%APPDATA%\Microsoft\UserSecrets\<id>\secrets.json` veya `~/.microsoft/usersecrets/<id>/secrets.json`) tutulan key/value dosyasıdır; projede `<UserSecretsId>` ile eşlenir (`dotnet user-secrets init`). Secret'ın repoya girmesini önler. **Şifreli değildir** ve **yalnızca Development ortamında** otomatik yüklenir; Production'da kullanılmaz (orada env var / Key Vault). Bir güvenlik sınırı değil, "yanlışlıkla commit etme" korumasıdır.

### 5. ⭐ `IOptions<T>`, `IOptionsSnapshot<T>` ve `IOptionsMonitor<T>` farkı nedir?
`IOptions<T>`: **Singleton**, değer bir kez okunur, çalışırken değişiklikleri görmez; en basit ve varsayılan seçim. `IOptionsSnapshot<T>`: **Scoped**, her request başında yeniden hesaplanır (dosya değiştiyse yeni değeri görür); singleton'a enjekte edilemez. `IOptionsMonitor<T>`: **Singleton**, her zaman güncel değeri verir (`CurrentValue`) ve `OnChange` ile bildirim yollar; singleton servislerde canlı ayar için kullanılır. Canlı yeniden yükleme için kaynağın da `reloadOnChange` desteklemesi gerekir (JSON dosyaları destekler). Derinlemesine → M3.

### 6. ⭐ Options pattern nedir, `config["Smtp:Port"]` yerine neden tercih edilir?
Configuration'ın bir bölümünü **tip-güvenli bir sınıfa** (`SmtpOptions`) bind edip DI ile `IOptions<SmtpOptions>` olarak enjekte etme desenidir. Avantajları: derleme zamanında tip/yazım denetimi (string key typo'su yok), tür dönüşümü (`int`, `bool`), tek yerde toplanmış ayar, **validation** (DataAnnotations), test edilebilirlik (`Options.Create(new SmtpOptions { ... })`) ve ilgili olmayan ayarlardan izolasyon (interface segregation). Servisler `IConfiguration`'ın tamamını görmek yerine yalnızca ihtiyaç duyduğu ayarı alır.

### 7. `AddOptions<T>().Bind(...)` ile `Configure<T>(...)` farkı nedir?
İkisi de bir section'ı options'a bağlar. `services.Configure<T>(section)` kısa yoldur. `AddOptions<T>()` bir **`OptionsBuilder<T>`** döner ve akıcı biçimde `.Bind(section)`, `.ValidateDataAnnotations()`, `.Validate(predicate, mesaj)`, `.ValidateOnStart()` zincirlemenizi sağlar; doğrulama gerekiyorsa tercih edilir. Ayrıca `.BindConfiguration("Smtp")` ile section yolu doğrudan verilebilir.

### 8. ⭐ `ValidateOnStart` ne yapar? Fail-fast neden önemli?
Varsayılan olarak options doğrulaması **ilk kullanımda** (`options.Value` okunduğunda) çalışır; yani hatalı ayar uygulama açılır, ilk istek o servise gelince patlar. `ValidateOnStart()` doğrulamayı **host başlarken** çalıştırır: ayar geçersizse `OptionsValidationException` ile uygulama **hiç ayağa kalkmaz**. Üretimde bu, hatalı deploy'un trafik almadan fark edilmesi (Kubernetes rollout'u durur, eski sürüm çalışmaya devam eder) demektir. Gece 3'te kullanıcıya 500 dönmektense açılışta net bir mesajla durmak tercih edilir.

### 9. `GetValue<int>("Smtp:Port")` key yoksa ne döner? `Get<T>()` ve `config["x"]`?
`config["x"]` yoksa **`null`** döner (exception yok). `GetValue<int>(...)` yoksa **`default(int)` = 0** döner — sessiz hata riski; `GetValue("Smtp:Timeout", 30)` ile varsayılan verilebilir. `GetSection("X").Get<T>()` section hiç yoksa **`null`** döner. `GetSection(...)` ise hiçbir zaman null dönmez (boş section verir); zorunlu section için `GetRequiredSection` exception fırlatır. Bu sessizlik yüzünden kritik ayarlarda `[Required]` + `ValidateOnStart` kullanılır.

### 10. Configuration key'leri büyük/küçük harfe duyarlı mıdır?
Hayır. `Smtp:Host`, `smtp:host` ve `SMTP:HOST` aynı key'dir (ordinal-ignore-case karşılaştırma). Bu yüzden Linux'ta büyük/küçük harfe duyarlı env var adı sorun çıkarmaz. Ama **dosya adları** farklıdır: Linux'ta `appsettings.production.json` ile `appsettings.Production.json` farklı dosyadır.

### 11. ⭐ Aynı ayarı appsettings.json'a, env var'a ve komut satırına verdim; hangisi kazanır?
Komut satırı. Sıra: JSON < ortam JSON'u < User Secrets < env var < komut satırı. Aynı anda `Smtp__Port=2525` env var ve `--Smtp:Port=25` verilirse `25` okunur. Hangi provider'ın kazandığını görmek için `((IConfigurationRoot)config).Providers` listesini sondan başa tarayıp key'i veren ilk provider'a bakılabilir (`GetDebugView()` de her değerin hangi provider'dan geldiğini gösterir).

### 12. `appsettings.Production.json` ne zaman yüklenir? Dosya yoksa ne olur?
Dosya adındaki `{Environment}`, `ASPNETCORE_ENVIRONMENT`/`DOTNET_ENVIRONMENT` (veya `--environment`) değeriyle eşleşir; varsayılan ortam `Production`'dır. Dosya **opsiyonel** eklendiği için yoksa hata verilmez, sessizce atlanır — bu yüzden ortam adındaki yazım hatası (özellikle Linux'ta büyük/küçük harf) fark edilmeden ayarların yüklenmemesine yol açar. (`optional: false` ile eklenen bir dosya yoksa açılışta `FileNotFoundException` alırsınız; bu demoda `appsettings.json` böyle eklenir.)

### 13. Connection string'i nereye koyarsın?
`ConnectionStrings` bölümüne (`ConnectionStrings:Default`) koyup `config.GetConnectionString("Default")` ile okurum. Dev'de yerel DB için User Secrets, üretimde şifreyi içeren değer env var (`ConnectionStrings__Default`) veya Key Vault'tan gelir; repodaki `appsettings.json`'da şifre içermeyen bir şablon bile tutulabilir (kullanıcı/şifre olmadan). Azure'da şifre yerine Managed Identity ile bağlanmak en iyisidir.

### 14. Production'da bir ayarı kod/deploy değişmeden nasıl değiştirirsin? Uygulama yeniden başlar mı?
Ayarı environment variable olarak (ya da Key Vault/App Configuration'da) değiştiririm. Env var'lar process başlarken okunduğu için genellikle **yeniden başlatma** gerekir (container yeniden oluşturulur; K8s'te rollout). Dosya tabanlı kaynaklar `reloadOnChange` ile canlı yenilenebilir ve `IOptionsMonitor<T>` / `IOptionsSnapshot<T>` değişikliği görür; ama `IOptions<T>` görmez. Hot-reload istenen ayarlar için merkezi bir servis (Azure App Configuration) tercih edilir (→ M3).

### 15. `appsettings.json` içine şifre koydum ve commit ettim; ne yaparım?
1) Secret'ı **hemen rotate/iptal ederim** (yeni şifre üretirim) — silmek yetmez, git geçmişinde ve fork/clone'larda kalır. 2) Değeri koddan çıkarıp User Secrets/env var/Key Vault'a taşırım. 3) Gerekirse geçmişi temizlerim (`git filter-repo`/BFG), ama bunun tek başına yeterli olduğuna güvenmem. 4) Erişim loglarında kötüye kullanım olup olmadığına bakarım. 5) Tekrarını önlemek için `.gitignore`, pre-commit secret taraması ve GitHub secret scanning/push protection açarım.

### 16. Birim testinde options nasıl verilir?
`IOptions<T>` sadece `Value` taşıyan basit bir arayüzdür; test için `Options.Create(new SmtpOptions { Host = "x", Port = 25 })` yeterlidir, `IConfiguration` kurmak gerekmez. Bu, options pattern'in test edilebilirlik avantajıdır. Configuration'ı da test etmek gerekirse bellek içi kaynak kullanılır: `new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string,string?> { ["Smtp:Port"] = "25" }).Build()`.

### 17. Bir options sınıfındaki property neden dolmuyor (hep varsayılan değer)?
Sık nedenler: section adı yanlış (`"Smpt"` — hata vermez, sessizce boş kalır); property'nin public `set`'i yok (binder varsayılanda yalnızca public setter'lı property'leri doldurur); tür dönüşümü başarısız (`"abc"` → `int` genelde `InvalidOperationException` fırlatır); key başka bir kaynak tarafından ezilmiş; yanlış ortam dosyası yüklenmiş. Teşhis: `GetSection("Smtp").GetChildren()` ile görünen key'lere bakmak ve `[Required]` + `ValidateOnStart` eklemek.
