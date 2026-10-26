# C2.3 AKS'e Manifest ile Dağıtım ve Container Teşhisi — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Develop containerized solutions on Azure* (%20-25) · 15 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, eşleştirme, manifest okuma/tamamlama, kubectl/CLI seçimi, senaryo, vaka); gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 12/15** (%80).

---

### 1. Manifest okuma
Aşağıdaki Deployment uygulanıyor.

```yaml
apiVersion: apps/v1
kind: Deployment
metadata: { name: todoapi }
spec:
  replicas: 2
  selector:
    matchLabels:
      app: todoapi
  template:
    metadata:
      labels:
        app: todo-api
    spec:
      containers:
        - name: todoapi
          image: myacr.azurecr.io/todoapi:1.0.0
```

Ne olur?

- A) Pod'lar oluşur; Service'ler çalışmaz
- B) `kubectl apply` selector, şablon label'larıyla eşleşmediği için hata verir
- C) Pod'lar `CrashLoopBackOff` olur
- D) Pod'lar `ImagePullBackOff` olur

<details><summary>Cevap</summary>

**B.** `spec.selector.matchLabels` pod şablonunun label'larıyla eşleşmelidir; burada `todoapi` ≠ `todo-api`, API server Deployment'ı doğrulamada reddeder (`selector does not match template labels`). Hiç pod oluşmaz, dolayısıyla C ve D de mümkün değildir.
</details>

### 2. Tek seçim
AKS kümesindeki bir pod'un durumu `ImagePullBackOff`; `kubectl describe pod` olayları `Failed to pull image … 401 Unauthorized` diyor. Image adı ve tag doğru. En olası kök neden ve çözüm?

- A) Bellek limiti düşük; limit artırılmalı
- B) Kubelet kimliğinde ACR üzerinde `AcrPull` yok; `az aks update --attach-acr` ile bağlanmalı
- C) Readiness probe yanlış; port düzeltilmeli
- D) Service selector yanlış

<details><summary>Cevap</summary>

**B.** `401/unauthorized` kimlik/yetki sorunudur. AKS image'ı kubelet managed identity ile çeker; ona `AcrPull` verilmelidir (`--attach-acr`). "manifest unknown/not found" olsaydı kök neden yanlış tag/digest olurdu. A, C, D farklı belirtilerin nedenleridir.
</details>

### 3. Eşleştirme
Belirtileri en olası kök nedenlerle eşleştir.

| Belirti | Kök neden |
|---|---|
| 1) `OOMKilled`, Exit Code 137 | a) Readiness probe yanlış port |
| 2) Pod `Running`, `READY 0/1`, endpoint yok | b) Bellek limiti aşıldı |
| 3) `CrashLoopBackOff`, `logs --previous`'ta "missing configuration" | c) Eksik env var/secret nedeniyle uygulama başlarken çöküyor |
| 4) `ErrImagePull`, `manifest unknown` | d) Registry'de olmayan tag |

<details><summary>Cevap</summary>

**1-b · 2-a · 3-c · 4-d.** Bellek limit aşımı çekirdeğin container'ı SIGKILL (137) ile öldürmesidir; readiness başarısızlığı pod'u endpoint'lerden çıkarır ama yeniden başlatmaz; çökme döngüsünün nedeni uygulama logunda; `manifest unknown` etiketin registry'de bulunmadığını söyler.
</details>

### 4. Tek seçim
Bir container birkaç saniye çalışıp çöküyor ve pod `CrashLoopBackOff`. Çökmenin **nedenini** görmek için hangi komut en doğrudur?

- A) `kubectl logs <pod>` (her zaman yeterlidir)
- B) `kubectl logs <pod> --previous`
- C) `kubectl scale deploy/x --replicas=0`
- D) `kubectl rollout restart deploy/x`

<details><summary>Cevap</summary>

**B.** Çöken container yeniden başlatıldığı için `kubectl logs` çoğu zaman yeni (henüz log üretmemiş) örneği gösterir; önceki çalışmanın logu `--previous` ile okunur. C ve D teşhis değil, müdahaledir.
</details>

### 5. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Memory limit aşıldığında container OOMKilled olur. | ☐ | ☐ |
| b) CPU limit aşıldığında container öldürülür. | ☐ | ☐ |
| c) Readiness probe başarısız olursa container yeniden başlatılır. | ☐ | ☐ |
| d) Liveness probe sürekli başarısız olursa container yeniden başlatılır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Hayır · d) Evet.** Bellek sıkıştırılamaz kaynaktır (aşım = öldürülme); CPU limit aşımı yalnızca kısıtlamaya (throttle) yol açar. Readiness başarısızlığı pod'u Service endpoint'lerinden çıkarır, yeniden başlatmaz; liveness başarısızlığı container'ı yeniden başlatır.
</details>

### 6. kubectl seçimi
Yeni sürüm rollout'ı sonrası hata oranı arttı. **Önceki çalışan sürüme** hızla dönmek için hangi komut kullanılır?

- A) `kubectl delete deploy todoapi`
- B) `kubectl rollout undo deployment/todoapi`
- C) `kubectl scale deployment/todoapi --replicas=0`
- D) `kubectl rollout pause deployment/todoapi`

<details><summary>Cevap</summary>

**B.** `rollout undo` Deployment'ı bir önceki ReplicaSet revision'ına döndürür (`--to-revision=N` ile belirli bir sürüme de). Diğerleri geri alma yapmaz (`pause` rollout'u yalnızca durdurur).
</details>

### 7. Manifest tamamlama
Service, `app: todoapi` etiketli pod'ların container'ındaki **8080** portuna, dışarıya **80** portundan açılacak.

```yaml
apiVersion: v1
kind: Service
metadata: { name: todoapi }
spec:
  type: LoadBalancer
  selector:
    app: todoapi
  ports:
    - port: 80
      ____: 8080
```

- A) `containerPort`
- B) `targetPort`
- C) `nodePort`
- D) `hostPort`

<details><summary>Cevap</summary>

**B.** `targetPort` trafiğin pod'da gönderileceği porttur; `port` Service'in sunduğu porttur. `containerPort` Pod şablonunda, `nodePort` NodePort türünde node portu için kullanılır.
</details>

### 8. Tek seçim
Bir Service oluşturuldu, ancak istemciler zaman aşımı alıyor. `kubectl get pods` tüm pod'ları `Running 1/1` gösteriyor. `kubectl get endpoints todoapi` **`<none>`** dönüyor. En olası neden?

- A) Bellek limiti
- B) Service selector'ı pod label'larıyla eşleşmiyor
- C) ACR kimlik hatası
- D) Liveness probe

<details><summary>Cevap</summary>

**B.** Pod'lar sağlıklı ve hazır, ama Service hiçbirini seçmiyor: selector–label uyuşmazlığı. Hazır olmayan pod'lar (readiness) da endpoint'ten çıkardı, ancak burada hepsi 1/1 hazır. ACR ve bellek sorunları pod'u hiç `Running 1/1` bırakmazdı.
</details>

### 9. CLI tamamlama
Mevcut AKS kümesinin ACR'dan image çekebilmesi için:

```bash
az aks ____ --resource-group rg --name aks --attach-acr myacr
```

- A) `update`
- B) `scale`
- C) `get-credentials`
- D) `nodepool add`

<details><summary>Cevap</summary>

**A.** `az aks update --attach-acr` kubelet kimliğine registry'de `AcrPull` atar (yeni kümede `az aks create --attach-acr`). `get-credentials` kubeconfig yazar, `scale` node sayısını değiştirir.
</details>

### 10. Çoklu seçim (iki doğru cevap)
Bir pod `Pending` durumunda kalıyor. `describe` olayı: `FailedScheduling: 0/2 nodes are available: 2 Insufficient memory`. **Hangi iki** eylem sorunu giderir?

- A) Container'ın memory **request**'ini düşürmek
- B) Node pool'a node eklemek (veya daha büyük VM)
- C) Liveness probe'u kaldırmak
- D) Service türünü ClusterIP yapmak

<details><summary>Cevap</summary>

**A ve B.** Zamanlayıcı **request** değerlerine bakar; request node'larda boş bellekten büyükse pod yerleşemez. Ya request gerçek ihtiyaca göre düşürülür ya da kapasite artırılır. Probe ve Service türü zamanlamayı etkilemez.
</details>

### 11. Senaryo
Ekip, bir Todo API'yi Kubernetes API'sine, CRD'lere veya özel bir service mesh'e **ihtiyaç duymadan**, kuyruk mesajına göre sıfıra kadar ölçekleyerek çalıştırmak ve küme operasyonuyla uğraşmamak istiyor. En uygun seçenek hangisidir?

- A) AKS
- B) Azure Container Apps
- C) Azure Virtual Machines
- D) App Service (Windows)

<details><summary>Cevap</summary>

**B.** Container Apps, yerleşik KEDA ile scale-to-zero ve küme yönetimi gerektirmeyen serverless container sunar. AKS bu ihtiyaç için gereğinden fazla operasyon yüküdür; VM ve klasik App Service KEDA tabanlı sıfıra ölçeklemeyi sunmaz.
</details>

### 12. Manifest okuma (iki doğru cevap)
Aşağıdaki Pod bölümünü incele.

```yaml
containers:
  - name: api
    image: myacr.azurecr.io/api:2.0.0
    ports:
      - containerPort: 8080
    resources:
      limits: { memory: 32Mi }
    readinessProbe:
      httpGet: { path: /healthz, port: 80 }
```

Uygulama 8080 portunu dinliyor ve çalışırken ~120 MiB bellek kullanıyor. **Hangi iki sorun** vardır?

- A) Bellek limiti gerçek kullanımın altında (OOMKilled riski)
- B) Readiness probe yanlış porta bakıyor
- C) `containerPort` yazılması hatadır
- D) `image` alanı tag içeremez

<details><summary>Cevap</summary>

**A ve B.** 32 MiB limit, ~120 MiB kullanımla çatışır: container OOMKilled olur. Probe 80 portunu dener ama uygulama 8080'dedir → readiness başarısız, pod hiç hazır olmaz. `containerPort` geçerli ve bilgilendiricidir; image tag içerebilir.
</details>

### 13. Tek seçim
Bir ekip dağıtım sonrası pod'ları şöyle gözlemliyor: `STATUS Running`, `READY 0/1`, `RESTARTS 0`. `kubectl describe` çıktısında `Readiness probe failed: Get "http://10.0.0.5:8080/health": … 404`. Uygulamanın sağlık ucu `/healthz`. Çözüm?

- A) Memory limit artırmak
- B) Probe path'ini `/healthz` yapmak
- C) `imagePullPolicy: Always`
- D) `kubectl rollout undo`'yu her zaman kullanmak

<details><summary>Cevap</summary>

**B.** 404, probe'un yanlış yola gittiğini gösterir; pod çalışıyor ama hazır işaretlenmiyor (restart yok, çünkü liveness değil readiness başarısız). `rollout undo` geçici çözümdür, kök nedeni düzeltmez; A ve C alakasızdır.
</details>

### 14. Sıralama
Bir pod'un çalışmadığı bildirildi. Sistematik teşhis sırasını diz: (1) `kubectl logs <pod> --previous`, (2) `kubectl get pods -o wide`, (3) `kubectl describe pod <pod>`, (4) Kök nedeni gidermek için manifest/yapılandırmayı düzeltip `kubectl apply`.

<details><summary>Cevap</summary>

**2 → 3 → 1 → 4.** Önce durum (`STATUS`, `READY`, `RESTARTS`), sonra `describe` (Events, `Reason`, `Exit Code`), ardından log (çökme nedeni), son olarak düzeltme. Önce müdahale etmek yerine kanıt toplanır.
</details>

### 15. Vaka çalışması
**Northwind Bank** ödeme API'sini AKS'e dağıttı. Pipeline yeni sürümü `kubectl set image` ile güncelledi. Gözlemler:

- Yeni pod'lar: `ImagePullBackOff`; `describe` olayı: `manifest for myacr.azurecr.io/payments:1.4.1 not found`.
- Eski pod'lar hâlâ çalışıyor ve trafiği karşılıyor (`maxUnavailable: 0`).
- Bir sonraki denemede image doğru ama pod'lar `CrashLoopBackOff`; `kubectl logs --previous`: `Unhandled exception: ConnectionStrings:Db is missing`.
- Üçüncü denemede uygulama ayakta, ancak `READY 0/1` ve Service `endpoints` boş; `describe`: `Readiness probe failed … connection refused (port 8081)`; uygulama 8080'de dinliyor.

**a)** İlk sorunun kök nedeni ve düzeltmesi?
- A) Kubelet'te `AcrPull` yok; rol ver
- B) CI, registry'de bulunmayan bir tag yazdı; doğru tag/digest ile güncelle (veya `rollout undo`)

**b)** İkinci sorunun nedeni?
- A) Bellek limiti
- B) Ortam değişkeni/secret'ta `ConnectionStrings__Db` eksik

**c)** Üçüncü sorunun düzeltmesi?
- A) `readinessProbe.httpGet.port` değerini 8080 yapmak
- B) Replica sayısını artırmak

**d)** Müşteri etkisi neden yaşanmadı?
- A) `RollingUpdate` + `maxUnavailable: 0`: eski pod'lar yenileri hazır olana kadar kapatılmadı
- B) Service tipi LoadBalancer olduğu için

<details><summary>Cevap</summary>

**a) B · b) B · c) A · d) A.**
(a) "not found" tag/manifest'in bulunmadığını söyler; yetki sorunu olsaydı `unauthorized` görülürdü. (b) `logs --previous` doğrudan eksik yapılandırmayı gösteriyor. (c) Probe portu uygulamanın portuyla uyuşmalı; replica artırmak hazır olmayan pod'ları hazır yapmaz. (d) Rolling update, yeni pod'lar readiness'i geçmeden eski pod'ları kapatmaz.
</details>
