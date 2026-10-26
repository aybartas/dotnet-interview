# C2.2 Azure Container Apps: Ortam, Revision ve KEDA — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Develop containerized solutions on Azure* (%20-25) · 18 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, kod/CLI tamamlama, senaryo, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 15/18** (%80).

---

### 1. Tek seçim
Bir ekip, yeni bir sürümü önce trafiğin %10'una göstermek, sorun yoksa %100'e çıkarmak istiyor. Container app şu an varsayılan revision mode'da. İlk adım ne olmalıdır?

- A) `az containerapp ingress traffic set --revision-weight latest=10`
- B) `az containerapp revision set-mode --mode multiple`
- C) `az containerapp update --min-replicas 0`
- D) `az containerapp secret set`

<details><summary>Cevap</summary>

**B.** Varsayılan mod **single**'dır ve trafik bölmeyi desteklemez. Önce **multiple** moda geçilir; sonra trafik ağırlıkları verilir (A bu adımdan sonra gelir).
</details>

### 2. Evet / Hayır
Aşağıdaki değişikliklerden hangisi **yeni bir revision** oluşturur?

| Değişiklik | Yeni revision |
|---|---|
| a) Container image'ının değiştirilmesi | ☐ Evet ☐ Hayır |
| b) Secret değerinin güncellenmesi | ☐ Evet ☐ Hayır |
| c) Scale kuralının (ör. `messageCount`) değiştirilmesi | ☐ Evet ☐ Hayır |
| d) Trafik ağırlıklarının değiştirilmesi | ☐ Evet ☐ Hayır |
| e) Environment variable eklenmesi | ☐ Evet ☐ Hayır |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet · d) Hayır · e) Evet.** `template` bölümündeki (revision-scope) değişiklikler — image, env var, kaynaklar, scale kuralları, suffix — yeni revision üretir. Secret, ingress ve trafik bölme kuralları application-scope'tur; tüm revision'lara uygulanır, yeni revision doğurmaz (secret değişince çalışan replica'lar için restart gerekir).
</details>

### 3. Tek seçim
Multiple revision mode'da `todoapi--v1` %90, `todoapi--v2` %20 verildi. Ne olur?

- A) Trafik 90/20 oranında bölünür
- B) Komut, toplam ağırlık %100 olmadığı için geçersizdir
- C) v2 otomatik %10'a düşürülür
- D) v1 devre dışı kalır

<details><summary>Cevap</summary>

**B.** Trafik kurallarının toplamı **%100** olmalıdır. Sistem değerleri kendiliğinden düzeltmez.
</details>

### 4. Tek seçim
Bir order worker'ı Service Bus kuyruğundaki mesajlara göre ölçeklenmeli ve kuyruk boşken **hiç replica çalışmamalı**. Hangi yapılandırma doğrudur?

- A) CPU scale rule (%70), `min-replicas 0`
- B) `azure-servicebus` KEDA kuralı, `min-replicas 0`
- C) HTTP concurrency kuralı, `min-replicas 1`
- D) Memory kuralı, `min-replicas 0`

<details><summary>Cevap</summary>

**B.** Kuyruk uzunluğu KEDA `azure-servicebus` scaler'ı ile ölçülür ve scale-to-zero (`min-replicas 0`) mümkündür. **CPU ve bellek kuralları sıfıra inemez** (A, D); HTTP kuralı kuyruğu görmez ve min 1 sıfıra izin vermez (C).
</details>

### 5. CLI tamamlama
Service Bus kuyruğu `orders` için, **replica başına 20 mesaj** hedefleyen bir kuralı tamamla.

```bash
az containerapp create --name orderworker --resource-group rg --environment cae \
  --image myacr.azurecr.io/orderworker:1.0.0 --min-replicas 0 --max-replicas 10 \
  --secrets sb-conn="$CONN" \
  --scale-rule-name orders-queue --scale-rule-type azure-servicebus \
  --scale-rule-metadata queueName=orders namespace=mysb ____=20 \
  --scale-rule-auth connection=sb-conn
```

- A) `queueLength`
- B) `messageCount`
- C) `concurrentRequests`
- D) `targetValue`

<details><summary>Cevap</summary>

**B.** KEDA Service Bus scaler'ında replica başına hedef mesaj sayısı `messageCount`'tur. `queueLength` Storage Queue scaler'ının, `concurrentRequests` HTTP kuralının parametresidir.
</details>

### 6. Hesaplama
Kuyrukta 500 mesaj var, `messageCount=20`, `maxReplicas=10`. KEDA kuralı kaç replica'ya ölçekler?

- A) 5
- B) 10
- C) 25
- D) 500

<details><summary>Cevap</summary>

**B.** İstenen ≈ ceil(500/20) = 25; ancak `maxReplicas=10` üst sınırdır, dolayısıyla 10 replica çalışır.
</details>

### 7. Tek seçim
Bir container app `ACR`'dan image'ı managed identity ile çekecek. **En az yetki** ve ilk revision'da çalışması için hangisi doğrudur?

- A) System-assigned identity'ye `Contributor`, uygulama oluşturulduktan sonra
- B) Önceden oluşturulmuş user-assigned identity'ye registry kapsamında `AcrPull`, uygulama bu identity ve `--registry-identity` ile oluşturulur
- C) Registry'de admin user'ı açıp parolayı env var'a koymak
- D) Identity'ye `AcrPush`

<details><summary>Cevap</summary>

**B.** System-assigned identity uygulama oluşturulduktan sonra var olur; ilk revision'ın image'ı çekmesi için user-assigned identity önceden hazırlanır. `Contributor` (A) ve `AcrPush` (D) gereğinden fazla yetkidir; admin user (C) paylaşılan parolaya dayanır.
</details>

### 8. Çoklu seçim (iki doğru cevap)
Bir revision label'ı (`canary`) için **hangi iki** ifade doğrudur?

- A) Revision'a kalıcı bir URL verir
- B) Aynı label aynı anda birden çok revision'da bulunabilir
- C) Label'ı başka bir revision'a taşıyarak trafik yönlendirmesi değiştirilebilir
- D) Label, uygulamayı single moda zorlar

<details><summary>Cevap</summary>

**A ve C.** Label revision'a sabit bir URL tanımlar ve `--label-weight` ile trafik kuralında kullanılabilir. Bir label tek revision'dadır (B yanlış); label'lar multiple mod ile kullanışlıdır (D yanlış).
</details>

### 9. Senaryo — canary sonrası geri alma
`todoapi--v1` (%90) ve `todoapi--v2` (%10) aktif. v2'de 5xx oranı arttı. **En hızlı** ve güvenli eylem hangisidir?

- A) `todoapi--v1`'i deactivate etmek
- B) Trafiği `todoapi--v1=100` olarak ayarlamak
- C) Uygulamayı silip yeniden oluşturmak
- D) `max-replicas`'ı 0 yapmak

<details><summary>Cevap</summary>

**B.** Geri alma = trafik kuralını eski aktif revision'a çevirmektir (`az containerapp ingress traffic set --revision-weight todoapi--v1=100`). A tersine, sağlıklı revision'ı kapatır. C ve D gereksiz yıkıcıdır.
</details>

### 10. Sıralama
Güvenli bir canary yayınının adımlarını doğru sıraya diz: (1) Trafiği v2'ye %100 ver, (2) Multiple mode'a geç, (3) v2'yi oluştur ve %10 trafik ver, (4) v1'i deactivate et, (5) v2 metriklerini/loglarını izle.

<details><summary>Cevap</summary>

**2 → 3 → 5 → 1 → 4.** Multiple mod; v2 revision'ı ve %10 trafik; izleme; sağlıklıysa %100; en son v1 kapatılır (geri alma imkânı o ana kadar korunur).
</details>

### 11. Tek seçim
Bir HTTP API'nin sabah ilk isteğinde birkaç saniyelik gecikme yaşanıyor; uygulama gece sıfır replica'ya iniyor. En uygun çözüm?

- A) Revision mode'u multiple yapmak
- B) `min-replicas`'ı 1 yapmak
- C) Scale kuralını CPU'ya çevirmek
- D) Ingress'i internal yapmak

<details><summary>Cevap</summary>

**B.** Gecikme **scale-to-zero soğuk başlangıcıdır**; en az bir sıcak replica tutmak bunu giderir (karşılığında sürekli, azaltılmış idle ücreti). Diğer seçenekler soğuk başlangıcı etkilemez; CPU kuralı ise zaten sıfıra inemez, yani sorunu çözmek yerine maliyeti değiştirir.
</details>

### 12. Kod tamamlama (YAML)
Scale kuralı **secret yerine managed identity** ile kimlik doğrulamalı. Boşluğu doldur.

```yaml
rules:
  - name: orders-queue
    custom:
      type: azure-servicebus
      metadata:
        queueName: orders
        namespace: mysb
        messageCount: "20"
      ____: system
```

- A) `auth`
- B) `identity`
- C) `credential`
- D) `principal`

<details><summary>Cevap</summary>

**B.** Kural şemasında `auth` (secret referansı) yerine `identity` kullanılır; değeri `system` veya user-assigned identity'nin resource ID'sidir. Kimliğe hedefte *Azure Service Bus Data Receiver* gibi bir rol verilmelidir.
</details>

### 13. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Consumption-only environment'ta GPU kullanılabilir. | ☐ | ☐ |
| b) Aynı environment'taki uygulamalar aynı Log Analytics çalışma alanına log gönderir (varsayılan). | ☐ | ☐ |
| c) Container Apps Job, cron ile zamanlanabilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** GPU ve büyük kaynak boyutları için workload profiles ortamında Dedicated profil gerekir. Environment ortak log hedefini paylaşır; Job'lar `Schedule`, `Event` veya `Manual` tetiklenebilir.
</details>

### 14. CLI tamamlama
`todoapi--v2` revision'ını kalıcı olarak trafik dışı bırakmak ve kaynak tüketmesini durdurmak için:

```bash
az containerapp revision ____ --name todoapi --resource-group rg --revision todoapi--v2
```

- A) `delete`
- B) `deactivate`
- C) `suspend`
- D) `stop`

<details><summary>Cevap</summary>

**B.** `az containerapp revision deactivate` revision'ı kapatır (replica'ları durur; gerekirse `activate` ile geri açılır). Trafik kuralından çıkarmak tek başına revision'ı kapatmaz.
</details>

### 15. Tek seçim
Bir geliştirici `az containerapp secret set` ile bir secret'ın değerini güncelledi, ama uygulama hâlâ eski değeri kullanıyor. Neden ve çözüm?

- A) Secret değişikliği yeni revision üretir; eski revision'ın kapanması beklenmeli
- B) Secret application-scope bir değişikliktir ve yeni revision oluşturmaz; çalışan replica'lar için revision restart gerekir
- C) Secret'lar yalnızca Key Vault'tan okunabilir
- D) Değişiklik yalnızca multiple modda geçerlidir

<details><summary>Cevap</summary>

**B.** Secret değişikliği yeni revision doğurmaz; çalışan replica'ların güncel değeri alması için revision restart (veya yeni revision) gerekir.
</details>

### 16. Tek seçim
Uygulama `8080` portunu dinliyor, ingress `--target-port 80` ile açıldı ve external. Dış istekler hata veriyor; loglar uygulamanın sağlıklı çalıştığını gösteriyor. En olası neden?

- A) Registry kimliği eksik
- B) Target port, container'ın dinlediği portla eşleşmiyor
- C) Scale kuralı yok
- D) Revision mode single

<details><summary>Cevap</summary>

**B.** Ingress'in `target-port`'u container'ın gerçekten dinlediği port olmalıdır. Registry kimliği sorunu image'ın hiç çalışmamasına yol açardı (loglar sağlıklı), diğerleri alakasızdır.
</details>

### 17. KQL tamamlama
Bir container app'in son 30 dakikadaki konsol loglarından hata içerenleri görmek istiyorsun.

```kusto
____
| where ContainerAppName_s == "todoapi" and TimeGenerated > ago(30m)
| where Log_s contains "error"
| project TimeGenerated, RevisionName_s, Log_s
```

- A) `ContainerAppSystemLogs_CL`
- B) `ContainerAppConsoleLogs_CL`
- C) `AzureActivity`
- D) `AppTraces`

<details><summary>Cevap</summary>

**B.** stdout/stderr çıktısı `ContainerAppConsoleLogs_CL` tablosundadır (`Log_s` mesaj kolonudur). Sistem tablosu platform olaylarını (image çekme, probe, restart) tutar; `AzureActivity` kontrol düzlemi işlemleridir.
</details>

### 18. Vaka çalışması
**Fabrikam Lojistik**, sipariş API'sini ve sipariş-işleme worker'ını Container Apps'e taşıyor. Gereksinimler: (1) API'nin yeni sürümleri önce %10 trafikle denenmeli ve gerektiğinde saniyeler içinde geri alınabilmeli; (2) worker `orders` kuyruğundaki mesaj sayısına göre ölçeklenmeli, boşken sıfıra inmeli ve Service Bus connection string'i saklanmamalı; (3) image'lar ACR'dan parolasız çekilmeli; (4) API'nin ilk isteği yavaş olmamalı.

**a)** (1) için doğru mod ve eylem?
- A) Single mode, her dağıtımda yeni revision
- B) Multiple mode, yeni revision'a `%10` ağırlık, eski revision aktif

**b)** (2) için doğru ölçekleme yapılandırması?
- A) `azure-servicebus` KEDA kuralı, `messageCount` hedefi, managed identity (`identity`) ile kimlik doğrulama, `min-replicas 0`
- B) CPU kuralı, `min-replicas 0`

**c)** (3) için?
- A) User-assigned identity + registry kapsamında `AcrPull` + `--registry-identity`
- B) ACR admin user

**d)** (4) için?
- A) API'de `min-replicas 1`
- B) API'de `min-replicas 0`

<details><summary>Cevap</summary>

**a) B · b) A · c) A · d) A.**
(a) Trafik bölme yalnızca multiple modda; geri alma, eski revision aktif olduğu sürece trafik kuralı değişikliğidir. (b) Kuyruk uzunluğu KEDA Service Bus scaler'ı ile ölçülür; CPU kuralı sıfıra inemez; `identity` ile connection string saklanmaz. (c) Parolasız ve en az yetkili çekme. (d) Soğuk başlangıcı önlemek için API'de en az bir sıcak replica; worker ise sıfıra inebilir.
</details>
