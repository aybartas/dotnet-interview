# C2.1 Azure Container Registry ve ACR Tasks — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Develop containerized solutions on Azure* (%20-25) · 15 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, kod/CLI tamamlama, senaryo, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 12/15** (%80).

---

### 1. Tek seçim
Bir ekip registry'yi **yalnızca VNet içinden** erişilebilir yapmak (private endpoint) ve image'ları iki bölgeye replike etmek istiyor. Hangi SKU gerekir?

- A) Basic
- B) Standard
- C) Premium
- D) Standard + geo-replication eklentisi

<details><summary>Cevap</summary>

**C.** Private endpoint ve geo-replication yalnızca **Premium**'dadır. Basic/Standard'a sonradan eklenemez; D diye bir eklenti yoktur, önce SKU Premium'a yükseltilir.
</details>

### 2. Tek seçim
Geliştiricinin makinesinde Docker yok. Todo API'nin Dockerfile'ından image'ı üretip doğrudan registry'ye göndermek için hangi komut kullanılır?

- A) `docker build` ardından `docker push`
- B) `az acr build --registry myacr --image todoapi:1.0.0 .`
- C) `az acr run --registry myacr --cmd 'docker build .' /dev/null`
- D) `az acr import --name myacr --source ./Dockerfile`

<details><summary>Cevap</summary>

**B.** `az acr build` kaynağı ACR Tasks'a yükler; build ve push bulutta olur, yerel Docker gerekmez. A Docker ister; C'de `docker build` ACR Tasks ortamında çalıştırılacak bir komut değildir; `az acr import` hazır bir image'ı başka registry'den kopyalar, Dockerfile derlemez.
</details>

### 3. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Aynı tag'e farklı içerikli bir image yeniden push edilebilir; digest ise değişmez. | ☐ | ☐ |
| b) Bir manifest'e birden fazla tag işaret edebilir. | ☐ | ☐ |
| c) `todoapi:1.0.0` tag'i, kilitlenmediği sürece her zaman aynı digest'e işaret eder. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Tag değişebilir bir işaretçidir; üzerine yazılabilir (kilit `--write-enabled false` ile konur). Digest içeriğin SHA-256 kimliğidir. Aynı digest'e `1.0.0`, `1.0` ve `sha-abc123` gibi birden çok tag bağlanabilir. Bu yüzden üretimde digest ile deploy edilir.
</details>

### 4. Tek seçim
Container Apps uygulamasının managed identity'si ACR'dan image çekecek. **En az yetki** ilkesine göre hangi rol atanmalıdır?

- A) `Owner`
- B) `Contributor`
- C) `AcrPush`
- D) `AcrPull`

<details><summary>Cevap</summary>

**D.** Yalnızca çekme gerekiyor: `AcrPull`. `AcrPush` fazladan yazma yetkisi verir; `Contributor`/`Owner` registry'yi yönetme yetkisidir ve image çekmek için gerekmez. (ABAC repository permissions modunda eşdeğer rol *Container Registry Repository Reader*'dır.)
</details>

### 5. CLI tamamlama
Bir quick task ile registry bağlamında, kaynak kod olmadan `todoapi:1.0.0` image'ını çalıştırmak istiyorsun. Boşluğu doldur.

```bash
az acr ____ --registry myacr --cmd '$Registry/todoapi:1.0.0 --version' /dev/null
```

- A) `build`
- B) `run`
- C) `task create`
- D) `import`

<details><summary>Cevap</summary>

**B.** `az acr run` bir container'ı (veya `acr purge` gibi bir komutu) registry bağlamında çalıştırır; `/dev/null` "kaynak yok" anlamına gelir. `build` image üretir, `task create` kalıcı task tanımlar, `import` image kopyalar.
</details>

### 6. Çoklu seçim (iki doğru cevap)
ACR Task için **hangi iki** tetikleyici desteklenir?

- A) Base image güncellemesi
- B) Storage account'a blob yüklenmesi
- C) Zamanlayıcı (cron)
- D) Container Apps'te revision oluşması

<details><summary>Cevap</summary>

**A ve C.** ACR Tasks tetikleyicileri: kaynak kod güncellemesi (commit/PR), base image güncellemesi ve timer. Blob olayı (B) ve revision oluşumu (D) ACR Task tetikleyicisi değildir.
</details>

### 7. Evet / Hayır
Bir task `FROM mcr.microsoft.com/dotnet/aspnet:10.0` kullanıyor ve base image trigger açık.

| İfade | Evet | Hayır |
|---|---|---|
| a) Task en az bir kez çalıştırılmadan base image bağımlılıkları izlenmez. | ☐ | ☐ |
| b) Base image sürümleri her seferinde yeni, benzersiz bir tag ile yayınlanırsa (ör. `10.0.1`, `10.0.2`) tetikleme güvenilir çalışır. | ☐ | ☐ |
| c) Çok aşamalı Dockerfile'da varsayılan olarak yalnızca runtime image'ın güncellemesi izlenir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Bağımlılıklar ilk build'den sonra bilinir. Tetikleme için base image'ın **sabit bir tag** üzerinden güncellenmesi gerekir; yeni tag çıkarsa task bunu "güncelleme" saymaz. `--base-image-trigger-type` varsayılanı `Runtime`'dır (build-time SDK image'ı izlenmez).
</details>

### 8. Sıralama
Yeni bir servis için güvenli yayın akışının adımlarını doğru sıraya diz: (1) Digest'i oku ve deploy tanımına yaz, (2) Image'ı `az acr build` ile derle ve semver + SHA tag'le, (3) Registry'yi oluştur (admin user kapalı), (4) Uygulamayı deploy et.

<details><summary>Cevap</summary>

**3 → 2 → 1 → 4.** Önce registry, sonra build; digest ancak image push edildikten sonra bilinir; deploy tanımı digest'le yazıldıktan sonra uygulama çalıştırılır. (Çalışma zamanı kimliğine `AcrPull` ataması, deploy'dan önce herhangi bir noktada yapılabilir.)
</details>

### 9. Kod tamamlama (YAML)
Multi-step task, image'ı derleyip **test ettikten sonra** push etmeli. Boş bırakılan adım türü hangisidir?

```yaml
version: v1.1.0
steps:
  - build: -t $Registry/todoapi:{{.Run.ID}} -f Dockerfile .
  - ____: $Registry/todoapi:{{.Run.ID}} --selftest
  - push:
      - $Registry/todoapi:{{.Run.ID}}
```

- A) `run`
- B) `cmd`
- C) `test`
- D) `exec`

<details><summary>Cevap</summary>

**B.** Multi-step task'ta `build`, `push` ve `cmd` adım türleri vardır; `cmd` bir container'ı çalıştırır (burada yeni derlenen image). `run`, `test`, `exec` geçerli adım türleri değildir. Adım başarısız olursa sonraki `push` çalışmaz.
</details>

### 10. Tek seçim
Production'da aşağıdaki deploy referanslarından hangisi **en güvenli ve tekrarlanabilirdir**?

- A) `myacr.azurecr.io/todoapi:latest`
- B) `myacr.azurecr.io/todoapi:1.0`
- C) `myacr.azurecr.io/todoapi@sha256:9f2c…`
- D) `myacr.azurecr.io/todoapi`

<details><summary>Cevap</summary>

**C.** Digest içeriği değişmez biçimde tanımlar. `:latest` ve etiketsiz referans (D, aslında `:latest` demektir) ile `1.0` gibi hareketli tag'ler, aynı referansın zamanla farklı içeriğe işaret etmesine izin verir.
</details>

### 11. Çoklu seçim (iki doğru cevap)
Registry'de birikmiş **untagged manifest**'leri temizlemek için hangi iki yaklaşım doğrudur?

- A) `acr purge --untagged` komutunu ACR Task (timer) ile zamanlamak
- B) Premium registry'de retention policy etkinleştirmek
- C) Basic registry'de soft-delete'i kapatmak
- D) `az acr update --sku Free`

<details><summary>Cevap</summary>

**A ve B.** `acr purge` (`--filter`, `--ago`, `--keep`, `--untagged`, `--dry-run`) timer task'a bağlanabilir; retention policy (Premium, preview) untagged manifest'leri N gün sonra siler. C ve D uydurmadır (Free SKU yoktur).
</details>

### 12. CLI tamamlama
`todoapi:1.0.0` image'ının digest'ini yalnızca değer olarak almak istiyorsun.

```bash
az acr repository show --name myacr --image todoapi:1.0.0 --query ____ -o tsv
```

- A) `tag`
- B) `digest`
- C) `sha`
- D) `manifest.id`

<details><summary>Cevap</summary>

**B.** `az acr repository show --image …` çıktısında `digest` alanı bulunur; `--query digest -o tsv` düz metin döndürür. `az acr manifest list-metadata` de digest'leri ve tag'leri listeler.
</details>

### 13. Tek seçim
Bir GitHub Actions iş akışı, Entra'da tanımlı bir federated credential (OIDC) ile oturum açıp registry'ye image **push** edecek ve başka bir şey yapmayacak. Hangisi doğrudur?

- A) Admin user'ı etkinleştirip parolayı secret olarak saklamak
- B) Federe kimliğe registry kapsamında `AcrPush` atamak
- C) Federe kimliğe abonelik kapsamında `Contributor` atamak
- D) Anonim pull'u açmak

<details><summary>Cevap</summary>

**B.** Parolasız (OIDC) kimlik + yalnızca gereken rol + dar kapsam (registry). A paylaşılan parolaya döner; C gereğinden geniştir ve push için yetmeyebilir (data-plane rolü ister); D push'u hiç etkilemez.
</details>

### 14. Tek seçim
`az acr build` şu hatayla başarısız oluyor: `TasksOperationsNotAllowed`. Abonelik, **Azure ücretsiz deneme kredisi** kullanıyor. En olası neden ve çözüm hangisidir?

- A) Registry Basic SKU; Premium'a yükseltmek gerekir
- B) ACR Tasks çalıştırmaları bu abonelik türünde kısıtlanmış olabilir; Pay-As-You-Go'ya geçmek veya yerelde `docker build` + `docker push` kullanmak çözümdür
- C) `AcrPull` rolü eksiktir
- D) Dockerfile'da `ENTRYPOINT` yoktur

<details><summary>Cevap</summary>

**B.** Microsoft, kredi/deneme aboneliklerinde ACR Tasks çalıştırmalarını geçici olarak kısıtlayabilmektedir (durumu güncel dokümandan doğrula). SKU, `AcrPull` veya `ENTRYPOINT` bu hata kodunu üretmez; push için gereken rol ise `AcrPush`'tur.
</details>

### 15. Vaka çalışması
**Contoso Retail** 12 .NET mikroservisini ACR'a koyuyor. Gereksinimler: (1) Production'a giden image'lar tam olarak QA'da test edilen image olmalı; (2) .NET runtime güvenlik yaması çıkınca tüm servis image'ları kendiliğinden yeniden derlenmeli; (3) Avrupa ve ABD'deki AKS kümeleri hızlı pull yapmalı; (4) Registry'ye internetten erişim kapalı olmalı; (5) Pipeline'da kalıcı parola bulunmamalı; (6) Registry maliyeti kontrol altında kalmalı.

**a)** Gereksinim (1) için ne yapılır?
- A) `:latest` tag'iyle deploy
- B) Digest ile deploy (`@sha256:…`)

**b)** Gereksinim (2) için hangi mekanizma?
- A) Her servis için ACR Task + base image update trigger
- B) AKS'te cron job

**c)** Gereksinimler (3) ve (4) için hangi SKU/özellikler?
- A) Standard + Azure Firewall
- B) Premium + geo-replication + private endpoint

**d)** Gereksinim (5) için?
- A) Admin user ve paylaşılan parola
- B) Managed identity / OIDC + `AcrPull` / `AcrPush`

**e)** Gereksinim (6) için?
- A) `acr purge --untagged --ago … --keep …` timer task'ı (veya Premium'da retention policy)
- B) Hiçbir şey; ACR eski image'ları kendi siler

<details><summary>Cevap</summary>

**a) B · b) A · c) B · d) B · e) A.**
(a) Digest değişmezdir. (b) Base image trigger yama zincirini otomatikleştirir (base image sabit tag + ilk build şartı). (c) Geo-replication ve private endpoint Premium'dur; Firewall registry'ye private erişim sağlamaz. (d) Secret'sız kimlik, en az yetki. (e) ACR eski image'ları kendiliğinden silmez; yetim manifest'ler depolama ücreti yazar.
</details>
