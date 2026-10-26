# C2.11 Managed Identity ve Azure Key Vault — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Secure, monitor, and troubleshoot Azure solutions* (%20-25) · 16 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, senaryo, kod/CLI tamamlama, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 13/16** (≥ %80).

---

### 1. Tek seçim
Üç Azure Function uygulaması ve bir API aynı Key Vault'tan secret okuyacak. Kaynaklar CI/CD ile sık sık silinip yeniden oluşturuluyor. Rol atamalarının **bir kez** yapılıp yeniden oluşturmalarda korunmasını istiyorsun. Ne kullanmalısın?

- A) Her kaynakta system-assigned managed identity
- B) Tek bir user-assigned managed identity'yi dört kaynağa atamak
- C) Vault'a bir client secret ile bağlanmak
- D) Key Vault access policy'ye her kaynak için ayrı Entra kullanıcısı eklemek

<details><summary>Cevap</summary>

**B.** User-assigned MI bağımsız bir kaynaktır; kaynaklar silinse de principal ID aynı kalır ve rol ataması korunur; ayrıca birden çok kaynağa atanabilir. System-assigned (A) kaynakla birlikte silinir, yeniden oluşturulunca **yeni principal** gelir ve roller kaybolur. C ve D secret/kullanıcı yönetimi getirir ve "secret'sız" hedefine aykırıdır.
</details>

### 2. Tek seçim
Bir API'nin managed identity'si, RBAC izin modelindeki vault'tan yalnızca secret **değerlerini okuyacak**. **En az yetkiyi** hangi rol verir?

- A) Contributor
- B) Key Vault Administrator
- C) Key Vault Secrets User
- D) Key Vault Reader

<details><summary>Cevap</summary>

**C.** *Key Vault Secrets User* secret değerini okur (get/list). *Contributor* control plane rolüdür ve değer okutmaz. *Administrator* gereğinden geniştir. *Key Vault Reader* yalnızca meta veriyi (ad, öznitelik) gösterir, **değeri vermez**.
</details>

### 3. Tek seçim
Rotation Function'ı vault'a **yeni secret sürümü yazacak**. Function'ın MI'ına hangi rol verilmelidir (en az yetki)?

- A) Key Vault Secrets User
- B) Key Vault Secrets Officer
- C) Key Vault Crypto Officer
- D) Owner

<details><summary>Cevap</summary>

**B.** Secret oluşturma/güncelleme data plane'de *Secrets Officer* gerektirir. *Secrets User* salt okunurdur (A). *Crypto Officer* key'ler içindir, secret'lar için değil (C). *Owner* control plane'dir ve fazlasıyla geniştir (D).
</details>

### 4. Evet / Hayır

Vault, **Azure RBAC** izin modelini kullanıyor.

| İfade | Evet | Hayır |
|---|---|---|
| a) Vault üzerinde `Owner` olan bir kullanıcı, ek rol almadan secret değerini okuyabilir. | ☐ | ☐ |
| b) Rol atamasını tek bir secret kapsamında yapmak mümkündür. | ☐ | ☐ |
| c) Access policy girdileri RBAC rolleriyle birlikte etkin sayılır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Hayır.** `Owner` control plane'dir; data plane (secret okuma) için ayrı bir rol gerekir. RBAC modelinde kapsam vault, kaynak grubu ve **tek nesne** düzeyinde olabilir (access policy yalnızca vault düzeyidir). Bir vault'ta aynı anda tek model geçerlidir; RBAC açıkken access policy'ler **yok sayılır**.
</details>

### 5. Tek seçim
Bir App Service uygulaması şu app setting'i kullanıyor:
`@Microsoft.KeyVault(SecretUri=https://kv-demo.vault.azure.net/secrets/db-conn/3f1a9c…)`
Secret'ın yeni sürümü yazıldı ama uygulama eski değeri kullanmaya devam ediyor. Neden?

- A) Key Vault referansları rotation'ı desteklemez
- B) Referans belirli bir **sürüme sabitlenmiş**; sürümsüz URI gerekir
- C) Managed identity'nin Secrets Officer rolü yok
- D) Event Grid aboneliği eksik

<details><summary>Cevap</summary>

**B.** URI'daki sürüm kimliği referansı o sürüme sabitler; en son sürümü almak için `.../secrets/db-conn/` (sürümsüz) kullanılır. Sürümsüz referanslar platform tarafından belirli aralıklarla yenilenir (App Service'te yaklaşık günlük, kesin SLA yok; app setting değişimi/restart anında yeniler). Okuma için Secrets User yeterlidir (C); Event Grid referansın yenilenmesi için gerekmez (D).
</details>

### 6. Çoklu seçim (iki doğru cevap)
Secret'lar için "süresi dolmak üzere" olayıyla otomatik rotation kurmak istiyorsun. **Hangi iki** bileşen akışın parçasıdır?

- A) Secret'a bir expiry date (`--expires`) atanması
- B) Key Vault'un `Microsoft.KeyVault.SecretNearExpiry` olayına Event Grid aboneliği
- C) Azure Policy'nin secret'ı otomatik yeniden oluşturması
- D) Application Insights availability test

<details><summary>Cevap</summary>

**A ve B.** Near-expiry olayı yalnızca expiry tarihi olan secret'lar için doğar; Event Grid olayı Function/Logic App'e iletir, yeni değeri üreten ve vault'a yazan o handler'dır. Azure Policy secret üretmez (C); availability test ilgisizdir (D).
</details>

### 7. Sıralama
Tek anahtarlı bir hedef servis yerine **iki anahtarlı** (primary/secondary) bir servis için kesintisiz rotation adımlarını doğru sıraya koy:

1. Uygulama vault'tan yeni değeri yenileyip secondary anahtara geçer
2. Event Grid `SecretNearExpiry` olayı Function'ı tetikler
3. Function, **kullanılmayan** anahtarı hedef serviste yeniden üretir
4. Function yeni anahtarı vault'a yeni secret sürümü olarak yazar
5. Bir sonraki turda eski (artık kullanılmayan) anahtar yenilenir

<details><summary>Cevap</summary>

**2 → 3 → 4 → 1 → 5.** Olay tetikler; Function kullanılmayan anahtarı üretir; vault'a yeni sürüm yazar; uygulama yenilemeyle yeni değere geçer; eski anahtar sonraki rotasyonda döndürülür. Aktif anahtarı önce değiştirmek, uygulamayı vault'u okumadan kıracağı için yanlıştır.
</details>

### 8. Kod tamamlama (C#)
Yerelde `az login` ile, Azure'da **belirli bir user-assigned MI** ile çalışacak şekilde boşluğu doldur:

```csharp
var client = new SecretClient(
    new Uri("https://kv-demo.vault.azure.net/"),
    new DefaultAzureCredential(new DefaultAzureCredentialOptions
    {
        ______ = "11111111-2222-3333-4444-555555555555"   // user-assigned identity client id
    }));
```

- A) `TenantId`
- B) `ManagedIdentityClientId`
- C) `VisualStudioTenantId`
- D) `AuthorityHost`

<details><summary>Cevap</summary>

**B.** `ManagedIdentityClientId` (veya `AZURE_CLIENT_ID` ortam değişkeni) zincirdeki managed identity adımının hangi user-assigned kimliği kullanacağını belirler. Yerelde MI adımı atlanır, `az login` kimliği devreye girer. Diğerleri tenant/authority ayarlarıdır.
</details>

### 9. Kod tamamlama (Python)
Aşağıdaki kod secret'ın **yeni bir sürümünü** oluşturmalı ve 90 gün sonra sona erecek şekilde işaretlemelidir:

```python
from datetime import datetime, timedelta, timezone
from azure.identity import DefaultAzureCredential
from azure.keyvault.secrets import SecretClient

client = SecretClient("https://kv-demo.vault.azure.net/", DefaultAzureCredential())
client.______("Db--ConnectionString", new_value,
              expires_on=datetime.now(timezone.utc) + timedelta(days=90))
```

- A) `update_secret_properties`
- B) `set_secret`
- C) `begin_recover_deleted_secret`
- D) `backup_secret`

<details><summary>Cevap</summary>

**B.** `set_secret` aynı adla çağrılınca yeni sürüm üretir. `update_secret_properties` **mevcut** sürümün özniteliklerini değiştirir, değer/sürüm üretmez. C silinmiş secret'ı kurtarır, D yedek alır.
</details>

### 10. CLI tamamlama
Yeni bir vault'u **RBAC modeliyle** ve kalıcı silmeye karşı korumalı oluşturmak istiyorsun:

```bash
az keyvault create --resource-group rg-demo --name kv-demo-123 --location westeurope \
  --______ true --enable-purge-protection true
```

- A) `--enable-rbac-authorization`
- B) `--enable-soft-delete`
- C) `--public-network-access`
- D) `--sku premium`

<details><summary>Cevap</summary>

**A.** `--enable-rbac-authorization true` izin modelini Azure RBAC yapar. Soft delete yeni vault'larda zaten **her zaman açıktır** (B gereksiz); C ağ erişimidir; D HSM destekli key içindir, izin modeliyle ilgisizdir.
</details>

### 11. Tek seçim
Bir ekip, silinen bir vault'un ve içeriğinin **retention süresi dolmadan hiç kimse (yöneticiler dahil) tarafından** kalıcı silinememesini istiyor. Ne etkinleştirilmelidir?

- A) Soft delete
- B) Purge protection
- C) Private endpoint
- D) Resource lock `CanNotDelete`

<details><summary>Cevap</summary>

**B.** Purge protection açıkken soft-deleted nesne retention bitene kadar kalıcı silinemez; bir kez açılınca kapatılamaz. Soft delete (A) kurtarma penceresi sağlar ama purge'ü engellemez. Private endpoint ağ erişimidir (C); resource lock silme işlemini bloklar ama purge protection'ın yerini tutmaz (D).
</details>

### 12. Senaryo
Bir ASP.NET Core API `AddAzureKeyVault(...)` ile yapılandırılmış ve `ReloadInterval` ayarlanmamış. Secret `Api--Key` rotation ile yeni sürüm aldı; yeni değerin uygulamada görünmesi için **en az değişiklikle** ne yapılmalı? (İki doğru cevap)

- A) `AzureKeyVaultConfigurationOptions.ReloadInterval` ayarlamak
- B) Secret'ı options'a `IOptions<T>` singleton'ı üzerinden okumak
- C) Secret'ı `IOptionsMonitor<T>` (veya `IOptionsSnapshot<T>`) ile tüketmek
- D) Uygulamayı Premium Key Vault'a taşımak

<details><summary>Cevap</summary>

**A ve C.** Sağlayıcı varsayılan olarak yalnızca başlangıçta yükler; `ReloadInterval` periyodik yeniden okuma ekler. Değerin tüketiciye yansıması için options'ın değişikliği izlemesi gerekir (`IOptionsMonitor`/`IOptionsSnapshot`). `IOptions<T>` singleton'dır, ilk değeri tutar (B). Premium SKU HSM key içindir (D).
</details>

### 13. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) System-assigned MI, ait olduğu kaynak silinince otomatik silinir. | ☐ | ☐ |
| b) Bir kaynağa birden çok user-assigned MI atanabilir. | ☐ | ☐ |
| c) Key Vault, secret'lar için yerleşik otomatik rotation politikası sunar (key'lerdeki gibi). | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Otomatik rotation policy **key'ler** (ve sertifikalarda auto-renew) için vardır; secret'ların değeri hedef serviste de değişmesi gerektiğinden rotation Event Grid + Function ile uygulanır. Birden çok user-assigned atandığında `client_id` ile seçim yapılmalıdır.
</details>

### 14. Tek seçim
Bir Container Apps uygulaması secret'ı vault'tan referansla almak istiyor. Aşağıdaki komutta boşluğa ne yazılmalıdır?

```bash
az containerapp secret set -g rg-demo -n api-demo \
  --secrets "db-conn=______:https://kv-demo.vault.azure.net/secrets/db-conn,identityref:system"
```

- A) `secretref`
- B) `keyvaultref`
- C) `kvuri`
- D) `vaultref`

<details><summary>Cevap</summary>

**B.** Söz dizimi `<ad>=keyvaultref:<secret-uri>,identityref:<system | kullanıcı-atanmış MI kaynak ID'si>`. `secretref` (A) ise **env var'dan** uygulama secret'ına başvurmak içindir (`DB_CONN=secretref:db-conn`). Sürümsüz URI yeni sürümlerin platform tarafından alınmasını sağlar.
</details>

### 15. Çoklu seçim (iki doğru cevap)
Üretim API'si `new DefaultAzureCredential()` kullanıyor ve zaman zaman beklenmedik bir kimlikle 403 alıyor. Eski bir CI ajanında `AZURE_CLIENT_ID/SECRET` ortam değişkenleri tanımlı. **Hangi iki** düzeltme doğrudur?

- A) Üretimde belirli credential kullanmak (`ManagedIdentityCredential`)
- B) Credential zincirini `ChainedTokenCredential` ile deterministik hale getirmek
- C) Key Vault'a `Contributor` rolü eklemek
- D) Vault'un soft delete süresini artırmak

<details><summary>Cevap</summary>

**A ve B.** `DefaultAzureCredential` ortam değişkenlerini zincirin başında dener; beklenmeyen bir kimlik seçilebilir. Üretimde belirli credential veya sıralı/özel bir zincir kullanmak belirsizliği ve zincir tarama gecikmesini kaldırır. `Contributor` data plane izni vermez (C); soft delete ilgisizdir (D).
</details>

### 16. Vaka çalışması

**Durum.** *Contoso Orders* şirketi, Container Apps üzerinde çalışan `orders-api`, iki Azure Function (`invoice-fn`, `rotate-fn`) ve PostgreSQL kullanıyor. Gereksinimler:

1. Uygulamalar PostgreSQL parolasını yalnızca **Key Vault**'tan okuyacak; kodda/pipeline'da secret olmayacak.
2. Parola **90 günde bir** kesintisiz değişecek; değişim insan müdahalesi olmadan yapılacak.
3. `orders-api` ve `invoice-fn` yalnızca okuyacak; yazma yetkisi **yalnızca** `rotate-fn`'de olacak.
4. Yanlışlıkla veya kötü niyetle silinen vault geri getirilebilmeli; kalıcı silme engellenmeli.
5. Vault'a yalnızca şirket VNet'inden erişilmeli.

**Soru A.** Rol atamalarını nasıl yaparsın?
**Soru B.** Rotation'ı hangi bileşenlerle kurarsın?
**Soru C.** Gereksinim 4 ve 5 için hangi ayarlar?

<details><summary>Cevap</summary>

**A.** Vault'u **RBAC** modelinde oluştur. `orders-api` ve `invoice-fn` için tek bir **user-assigned MI** (veya her biri için ayrı MI) → vault/secret kapsamında **Key Vault Secrets User**. `rotate-fn`'ın MI'ı → **Key Vault Secrets Officer** (secret yazar) + PostgreSQL'de yeni rol/parola oluşturma yetkisi. Okuyan kimliklere yazma yetkisi verme (gereksinim 3).

**B.** Secret'a `--expires` ver (≈ 90 gün). Vault'tan **Event Grid** `Microsoft.KeyVault.SecretNearExpiry` olayına abone ol → `rotate-fn` (Event Grid trigger). Function **iki rol/parola kalıbıyla** kullanılmayan PostgreSQL parolasını yenileyip vault'a **yeni sürüm** yazar (idempotent). Uygulamalar sürümsüz referans/`AddAzureKeyVault` + `ReloadInterval` + `IOptionsMonitor` (veya Container Apps `keyvaultref`) ile yeni sürümü alır. Yalnızca `SecretNearExpiry`/`SecretExpired`'a abone ol ki `SecretNewVersionCreated` döngüsü oluşmasın.

**C.** **Soft delete** (yeni vault'larda zaten açık) kurtarma sağlar; kalıcı silmeyi engellemek için **purge protection**'ı etkinleştir (kapatılamaz). Ağ için **private endpoint** oluştur (`privatelink.vaultcore.azure.net` DNS zone) ve public network access'i kapat; Container Apps/Functions aynı VNet'e entegre olmalı.
</details>

---

**Puanlama:** 14-16 → hazırsın · 11-13 → README 3.3 (rol tablosu) ve 3.5 (rotation akışı) bölümlerini tekrar et · < 11 → konuyu baştan çalış ve lab'ı (3.7) uygula.
