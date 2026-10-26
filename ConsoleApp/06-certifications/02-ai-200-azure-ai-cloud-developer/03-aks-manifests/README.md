# C2.3 AKS'e Manifest ile Dağıtım ve Container Teşhisi

> 📁 `ConsoleApp/06-certifications/02-ai-200-azure-ai-cloud-developer/03-aks-manifests/` · Sınav alanı: **Develop containerized solutions on Azure (%20-25)** · Önkoşul: [C2.1](../01-container-registry/README.md) (ACR), Kubernetes temelleri (→ M14.3–M14.4) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of October 7, 2026):** *Implement container-orchestrated solutions* — uygulamaları manifest dosyaları kullanarak **Azure Kubernetes Service**'e dağıt ve yönet. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)'ı kontrol et.

## İçindekiler

1. [Ne?](#1-ne)
2. [Neden?](#2-neden)
3. [Nasıl?](#3-nasıl)
4. [Gerçek dünya senaryosu](#4-gerçek-dünya-senaryosu)
5. [Sınav perspektifi](#5-sınav-perspektifi)
6. [Yaygın hatalar](#6-yaygın-hatalar)
7. [Performans & trade-off](#7-performans--trade-off)
8. [Kaynaklar](#8-kaynaklar)

---

## 1. Ne?

**Azure Kubernetes Service (AKS)**, yönetilen Kubernetes'tir: **control plane'i (API server, etcd, scheduler) Azure yönetir**, sen **node pool**'ları (worker VM'leri) ve üzerlerinde çalışan iş yüklerini yönetirsin. Kubernetes'te her şey **bildirimsel manifest**'tir (YAML): "şu image'dan 3 kopya istiyorum" dersin, controller'lar gerçek durumu istenen duruma getirir. Bu konu Kubernetes temellerini (→ M14.3–M14.4) AI-200 sınavının bakış açısıyla birleştirir: **ACR'daki image'ı AKS'e manifest ile dağıtmak ve bozulunca teşhis etmek**.

| Manifest türü | Ne yapar? |
|---|---|
| **Deployment** | N kopya (replica) pod'u ve **rolling update** stratejisini yönetir (ReplicaSet üzerinden) |
| **Service** | Pod'lara sabit sanal IP/DNS: `ClusterIP` (küme içi), `LoadBalancer` (Azure Load Balancer ile dış IP), `NodePort` |
| **Ingress** | Host/yol bazlı HTTP yönlendirme (bir ingress controller gerektirir) |
| **ConfigMap** | Gizli olmayan yapılandırma |
| **Secret** | Gizli veri (base64 **kodlama**, şifreleme değil — varsayılan olarak) |
| **Resource request/limit** | Zamanlama (request) ve tavan (limit) |
| **Probe** | `liveness` (yeniden başlat), `readiness` (trafik al/alma), `startup` (yavaş açılış) |

```text
kubectl apply -f ──▶ API server ──▶ Deployment ──▶ ReplicaSet ──▶ Pod(lar) ◀── Service (selector: app=todoapi)
                                                                     ▲                  ▲
                                                       ConfigMap/Secret (env)        Ingress / LoadBalancer
```

---

## 2. Neden?

- **Sınav:** Hedef "manifest dosyalarıyla dağıt ve yönet" der; sorular genellikle **verilen manifest'teki hatayı bulma** (selector–label uyuşmazlığı, port, image), **hata mesajından kök nedene gitme** (`ImagePullBackOff`, `CrashLoopBackOff`, `OOMKilled`) ve `kubectl` komutu seçme biçimindedir.
- **Gerçek işte:** Kubernetes'te "pod çalışmıyor" sorununun %90'ı dört kalıba iner; sistematik bir teşhis sırası (`get → describe → logs → events`) saatler kazandırır.
- **Karar:** AKS en esnek ama en fazla operasyon yükü olan seçenektir; ne zaman Container Apps veya App Service'in yeterli olduğunu bilmek mimari kararın parçasıdır.

---

## 3. Nasıl?

### 3.1 AKS, ACR entegrasyonu ve temel kubectl ⭐

| Adım | Komut | Not |
|---|---|---|
| Küme oluştur (ACR bağla) | `az aks create -g RG -n aks --attach-acr $ACR --node-count 2 --generate-ssh-keys` | Kubelet kimliğine registry'de **`AcrPull`** atar |
| Var olan kümeye ACR bağla | `az aks update -g RG -n aks --attach-acr $ACR` | Komutu çalıştıran kullanıcının rol atama yetkisi (Owner/User Access Administrator) gerekir |
| Kubeconfig al | `az aks get-credentials -g RG -n aks` | `~/.kube/config`'e context ekler (Entra entegre kümelerde `kubelogin` gerekebilir) |
| Uygula | `kubectl apply -f k8s/` | Bildirimsel; tekrar çalıştırılabilir (idempotent) |
| Rollout takibi | `kubectl rollout status deploy/todoapi` | Başarı/zaman aşımı |
| Geçmiş / geri al | `kubectl rollout history deploy/todoapi` · `kubectl rollout undo deploy/todoapi [--to-revision=N]` | Önceki ReplicaSet'e döner |
| Ölçekle | `kubectl scale deploy/todoapi --replicas=5` | Elle; otomatik için HPA (→ M14.4) |
| Image güncelle | `kubectl set image deploy/todoapi todoapi=<image>` | Yeni rollout başlatır |

- **AKS ↔ ACR:** `--attach-acr` ile image çekme **kubelet managed identity** üzerinden olur; pod'a `imagePullSecrets` gerekmez. Hata durumunda ilk bakılacak yer: kubelet kimliğinde `AcrPull` var mı, ACR adı/tag doğru mu?
- ABAC repository permissions modundaki registry'lerde `--attach-acr` desteklenmez; *Container Registry Repository Reader* rolünü elle ata (→ C2.1).
- AKS **Free** tier control plane ücretsizdir; ücreti **node VM'leri** oluşturur — laboratuvarın en pahalı kalemi budur, işin bitince kümeyi sil.
- *(Bağlam)* **AKS Automatic**, node ve ağ yönetimini daha çok Azure'a devreden, güvenlik/ölçek varsayılanları hazır bir mod; **Standard** tam kontrol sunar.

### 3.2 Manifest'ler: Todo API

```yaml
# k8s/00-namespace.yaml
apiVersion: v1
kind: Namespace
metadata:
  name: todo
---
# k8s/10-config.yaml
apiVersion: v1
kind: ConfigMap
metadata:
  name: todoapi-config
  namespace: todo
data:
  ASPNETCORE_ENVIRONMENT: Production
  Logging__LogLevel__Default: Information
---
apiVersion: v1
kind: Secret
metadata:
  name: todoapi-secrets
  namespace: todo
type: Opaque
stringData:                       # plain text here; the API server stores it base64-encoded
  ConnectionStrings__Db: "Host=db;Database=todo;Username=app;Password=change-me"
```

```yaml
# k8s/20-deployment.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: todoapi
  namespace: todo
spec:
  replicas: 2
  selector:
    matchLabels:
      app: todoapi                # MUST match the pod template labels below
  strategy:
    type: RollingUpdate
    rollingUpdate: { maxSurge: 1, maxUnavailable: 0 }
  template:
    metadata:
      labels:
        app: todoapi
    spec:
      containers:
        - name: todoapi
          image: myacr.azurecr.io/todoapi@sha256:REPLACE_WITH_DIGEST
          ports:
            - containerPort: 8080
          envFrom:
            - configMapRef: { name: todoapi-config }
          env:
            - name: ConnectionStrings__Db
              valueFrom:
                secretKeyRef: { name: todoapi-secrets, key: ConnectionStrings__Db }
          resources:
            requests: { cpu: 100m, memory: 128Mi }
            limits:   { cpu: 500m, memory: 256Mi }
          readinessProbe:
            httpGet: { path: /healthz, port: 8080 }
            initialDelaySeconds: 3
            periodSeconds: 5
          livenessProbe:
            httpGet: { path: /healthz, port: 8080 }
            initialDelaySeconds: 10
            periodSeconds: 10
```

```yaml
# k8s/30-service.yaml
apiVersion: v1
kind: Service
metadata:
  name: todoapi
  namespace: todo
spec:
  type: LoadBalancer              # ClusterIP for in-cluster only; use Ingress for host/path routing
  selector:
    app: todoapi                  # MUST match pod labels, otherwise the Service has no endpoints
  ports:
    - port: 80                    # Service port
      targetPort: 8080            # container port
```

Anahtar kurallar:
- `Deployment.spec.selector.matchLabels` ⊆ `template.metadata.labels`; uyuşmazsa `kubectl apply` **reddedilir** (`selector does not match template labels`). `selector` oluşturulduktan sonra **değiştirilemez**.
- `Service.spec.selector` pod label'larıyla eşleşmezse Service **endpoint'siz** kalır (bağlantı zaman aşımı).
- `targetPort` = container'ın gerçekte dinlediği port (burada 8080); `port` = Service'in dışarıya sunduğu port.
- **Requests** zamanlayıcının node seçerken baktığı garanti; **limits** tavan: bellek limitini aşan container **OOMKilled** olur, CPU limitini aşan **kısılır** (throttle, öldürülmez).
- **Readiness** başarısız olursa pod Service endpoint'lerinden çıkar (yeniden başlatılmaz); **liveness** başarısız olursa container **yeniden başlatılır**.
- **Secret** varsayılan olarak yalnızca base64'tür; gerçek koruma için RBAC, etcd şifreleme ve tercihen **Key Vault'tan Secrets Store CSI driver** (→ [C2.11](../11-identity-key-vault/README.md)). Manifest'i Git'e secret değerleriyle koyma.

**Ingress** *(isteğe bağlı)*: `ingressClassName` ile bir controller seçilir.

```yaml
# k8s/40-ingress.yaml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: todoapi
  namespace: todo
spec:
  ingressClassName: webapprouting.kubernetes.io   # AKS application routing add-on (NGINX-based class)
  rules:
    - host: todo.example.com
      http:
        paths:
          - path: /
            pathType: Prefix
            backend:
              service: { name: todoapi, port: { number: 80 } }
```

> **2026 notu:** Upstream **ingress-nginx** projesi Mart 2026'da bakım sonuna girdi; Microsoft, application routing add-on'undaki NGINX Ingress için **Kasım 2026'ya kadar** kritik güvenlik yamalarını desteklediğini duyurdu ve uzun vadeli yön olarak **Gateway API** (application routing'in Gateway API uygulaması veya **Application Gateway for Containers**) önerdi. Sınav hedefi Ingress kaynağını (ve genel yönlendirme fikrini) sorar; yeni projelerde Gateway API'yi düşün. Güncel durumu doğrula.

### 3.3 Teşhis ⭐

**Sistematik sıra:** durum → ayrıntı → log → olay.

| Komut | Ne gösterir? |
|---|---|
| `kubectl get pods -n todo -o wide` | `STATUS`, `READY` (0/1), `RESTARTS`, hangi node |
| `kubectl describe pod <pod> -n todo` | **Events** (image çekme, probe, schedule), `State/Last State` (**Reason**, **Exit Code**), env, mount |
| `kubectl logs <pod> -n todo` · `--previous` · `-f` · `-c <container>` | Container logu; **çöken container'ın önceki çalışması için `--previous`** |
| `kubectl get events -n todo --sort-by=.lastTimestamp` | Namespace olayları (zamansal sıra) |
| `kubectl get endpoints todoapi -n todo` | Service'in arkasındaki **hazır pod IP'leri** (boşsa selector/readiness sorunu) |
| `kubectl exec -it <pod> -n todo -- sh` | Container içinde komut/ağ testi (shell yoksa `kubectl debug`) |
| `kubectl top pod -n todo` | CPU/bellek kullanımı (metrics-server ile) |

**Hata kalıbı → kök neden:**

| Gördüğün | Anlamı / olası kök neden | Kanıt | Çözüm |
|---|---|---|---|
| **`ErrImagePull` → `ImagePullBackOff`** | Image çekilemiyor: **yanlış repository/tag/digest**, **registry yetkisi yok** (kubelet kimliğinde `AcrPull` eksik), registry'ye ağ erişimi yok | `describe` → Events: `manifest unknown / not found` (tag yok) veya `unauthorized` / `401` (yetki) | Tag'i/digest'i düzelt; `az aks update --attach-acr`; ağ kuralları |
| **`CrashLoopBackOff`** | Container başlıyor ve **çöküyor**; kubelet artan beklemeyle yeniden başlatıyor. Neden: uygulama hatası, **eksik env var / secret**, yanlış bağlantı dizesi, yanlış komut, liveness probe'un sürekli başarısız olması | `logs --previous` (çökme nedeni), `describe` → `Last State: Terminated`, `Exit Code` | Config/secret'ı düzelt; kodu düzelt; probe'u gevşet |
| **`OOMKilled`** (Exit Code **137**) | Container **bellek limitini** aştı ve çekirdek tarafından öldürüldü | `describe` → `Reason: OOMKilled`; tekrar eden restart | Limit'i artır **veya** bellek sızıntısını düzelt; request/limit'i ölçülen kullanıma göre ayarla |
| **Pod `Running` ama `READY 0/1`** | **Readiness** başarısız: yanlış probe yolu/portu, uygulama hazır değil | `describe` → `Readiness probe failed: … connection refused / 404`; `get endpoints` boş | Probe path/port'u düzelt; `initialDelaySeconds`/startup probe |
| **`Pending`** | Zamanlanamıyor: yetersiz CPU/bellek (request çok yüksek), node selector/taint, PVC | `describe` → `FailedScheduling` (Insufficient cpu) | Request'i düşür, node ekle |
| **Service'e bağlanılamıyor** | Selector–label uyuşmazlığı, `targetPort` yanlış, hazır pod yok | `get endpoints` boş; `describe svc` | Selector/port düzelt |

> `CrashLoopBackOff` bir **belirti**dir, neden değil: asıl nedeni `logs --previous` ve `describe`'ın `Exit Code`'u söyler (1 = uygulama hatası, 137 = OOM/SIGKILL, 139 = segfault).

**Container Apps karşılığı:** `az containerapp logs show --type console|system`, portalda Log stream; Log Analytics'te `ContainerAppConsoleLogs_CL` (uygulama çıktısı) ve `ContainerAppSystemLogs_CL` (image çekme, probe, restart) (→ [C2.2](../02-container-apps/README.md)). AKS'te karşılığı: Container Insights / `ContainerLogV2` tablosu (→ [C2.13](../13-opentelemetry-kql/README.md)).

### 3.4 Container Apps mı, AKS mi, App Service mi? ⭐

| Kriter | **Container Apps** | **AKS** | **App Service for Containers** |
|---|---|---|---|
| Soyutlama | Serverless container; Kubernetes gizli | **Tam Kubernetes** API'si | PaaS web uygulaması (tek/çoklu container) |
| Operasyon yükü | **Düşük** | **Yüksek** (node, upgrade, ağ, güvenlik) | **Çok düşük** |
| Ölçekleme | HTTP + **KEDA** (scale-to-zero), HPA yok | HPA/KEDA/Cluster Autoscaler (**sen** kurarsın) | Plan düzeyinde scale out; sıfıra inmez |
| Özel Kubernetes (CRD, operator, DaemonSet, service mesh seçimi) | ✘ | ✔ | ✘ |
| Kullanım | Mikroservis, event-driven worker, API | Karmaşık/çok takımlı platform, özel ağ/GPU/stateful | Klasik web/API; basit container dağıtımı |
| Maliyet modeli | Kullanım bazlı (tüketim) | Node VM'leri (kullanılmasa da ödenir) | App Service planı (sabit) |

Kural: **Kubernetes API'sine/ekosistemine ihtiyaç yoksa Container Apps; hiç orkestrasyon gerekmiyorsa App Service; tam kontrol gerekiyorsa AKS.** (Tek container, orkestrasyon yok → Container Instances; → C1.5.)

### 3.5 Lab — Dağıt, bilerek boz, teşhis et, geri al

Maliyet: 2 × `Standard_B2s` node birkaç saatlik laboratuvar için düşük maliyetlidir (güncel fiyatı Pricing Calculator'dan kontrol et); **işin bitince kümeyi sil**. Bütçe alarmı şart (→ C1.1). Yerel alternatif: `kind`/`k3d` kümesi (ACR'dan çekmek için `imagePullSecrets` veya `kind load docker-image` gerekir; bu yüzden bu lab AKS ile anlatılır).

```bash
RG=rg-ai200-c23; LOC=westeurope; AKS=aks-ai200
az group create --name $RG --location $LOC

# 1) Cluster with ACR attached (kubelet identity gets AcrPull). ACR + image come from C2.1.
az aks create --resource-group $RG --name $AKS --node-count 2 --node-vm-size Standard_B2s \
  --generate-ssh-keys --attach-acr $ACR
az aks get-credentials --resource-group $RG --name $AKS --overwrite-existing
kubectl get nodes

# 2) Deploy the good version
sed -e "s|myacr|$ACR|" -e "s|REPLACE_WITH_DIGEST|${DIGEST#sha256:}|" k8s/20-deployment.yaml > /tmp/20-deployment.yaml   # or edit by hand
kubectl apply -f k8s/00-namespace.yaml -f k8s/10-config.yaml
kubectl apply -f /tmp/20-deployment.yaml -f k8s/30-service.yaml
kubectl rollout status deploy/todoapi -n todo
kubectl get svc,pods,endpoints -n todo           # wait for EXTERNAL-IP, then: curl http://<ip>/healthz

# 3) BROKEN #1 — wrong image tag -> ErrImagePull / ImagePullBackOff
kubectl set image deploy/todoapi todoapi=$ACR.azurecr.io/todoapi:9.9.9 -n todo
kubectl get pods -n todo                          # new pod: ImagePullBackOff, old pods still serve (maxUnavailable: 0)
kubectl describe pod -n todo -l app=todoapi | grep -A6 "Events:"      # 'manifest ... not found'
kubectl rollout undo deploy/todoapi -n todo       # back to the previous ReplicaSet
kubectl rollout status deploy/todoapi -n todo

# 4) BROKEN #2 — missing secret key / bad env -> CrashLoopBackOff
kubectl set env deploy/todoapi -n todo ConnectionStrings__Db-        # remove the env var (trailing dash deletes it); assumes the API validates its config at startup
kubectl get pods -n todo -w                       # Error -> CrashLoopBackOff, RESTARTS increasing
POD=$(kubectl get pods -n todo -l app=todoapi --sort-by=.metadata.creationTimestamp -o name | tail -1)   # newest pod = the crashing one
kubectl logs $POD -n todo --previous              # app log: "ConnectionStrings:Db is not configured"
kubectl describe $POD -n todo | grep -E "Reason|Exit Code|Restart Count"
kubectl rollout undo deploy/todoapi -n todo

# 5) BROKEN #3 — wrong readiness port -> Running but 0/1 READY, no endpoints
kubectl patch deploy/todoapi -n todo --type=json \
  -p='[{"op":"replace","path":"/spec/template/spec/containers/0/readinessProbe/httpGet/port","value":8081}]'
kubectl get pods,endpoints -n todo                # READY 0/1, endpoints <none>
kubectl describe pod -n todo -l app=todoapi | grep -i "Readiness probe failed"
kubectl rollout undo deploy/todoapi -n todo

# 6) BROKEN #4 — memory limit too low -> OOMKilled (Exit Code 137)
kubectl set resources deploy/todoapi -n todo --limits=memory=16Mi
kubectl describe pod -n todo -l app=todoapi | grep -E "OOMKilled|Exit Code"
kubectl rollout undo deploy/todoapi -n todo
kubectl rollout history deploy/todoapi -n todo

# 7) Cleanup: delete the whole resource group (cluster + node resource group)
az group delete --name $RG --yes --no-wait
```

> **Alıştırma.** `selector.matchLabels: {app: todoapi}` iken pod şablonu `labels: {app: todo-api}` ise ne olur? Şablon doğru, **Service selector**'ı `app: todo-api` ise ne olur?
>
> <details><summary>Çözüm</summary>
>
> Birincisinde `kubectl apply` Deployment'ı **doğrulamada reddeder** (`selector does not match template labels`); hiçbir pod oluşmaz. İkincisinde Deployment ve pod'lar sağlıklı çalışır ama Service selector'ı hiçbir pod'la eşleşmez: `kubectl get endpoints todoapi` **boş** döner ve istemci bağlantıları zaman aşımına uğrar.
> </details>

---

## 4. Gerçek dünya senaryosu

Bir fintech, Todo/ödeme mikroservislerini AKS'te çalıştırıyor ve cuma akşamı yapılan dağıtımdan sonra alarm geliyor:

| Belirti | Teşhis | Kök neden | Çözüm |
|---|---|---|---|
| `kubectl get pods`: yeni pod'lar `ImagePullBackOff` | `describe` → `manifest ... not found` | CI yanlış tag'i manifest'e yazdı | `rollout undo`; pipeline'da digest kullan (→ C2.1) |
| Pod'lar `CrashLoopBackOff` | `logs --previous`: "Db connection string missing" | Yeni sürüm eklediği `Db__Password` secret'ta yok | Secret'ı ekle; eksik yapılandırmada hızlı fail; `rollout undo` |
| Pod'lar `OOMKilled`, 137 | `describe` → `Reason: OOMKilled` | Limit 256Mi, yeni önbellek ~400Mi | Ölçerek limit'i artır; önbellek boyutunu sınırla |
| Pod `Running` ama trafik gelmiyor | `describe` readiness 404; `get endpoints` boş | Probe `/health`, uygulama `/healthz` | Probe yolunu düzelt |
| Eski sürüm çalışmaya devam ediyor | `maxUnavailable: 0` + bozuk yeni pod | Rolling update eski pod'ları korudu | `rollout undo` ile temizle; kesinti oluşmamıştı |

Ek kararlar: yapılandırma ConfigMap, parola Key Vault + CSI driver; pod başına request/limit zorunlu; üretim image'ları digest ile; rollout `kubectl rollout status` ile pipeline'da doğrulanır, başarısızsa otomatik `rollout undo`.

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Kısa bir manifest veya `kubectl` çıktısı verir; hatayı/kök nedeni/komutu seçtirir. Çoğu soru "ne yapılmalı?" yerine **"ilk önce hangi komut?"** veya **"hangi alan yanlış?"** biçimindedir.

| Senaryodaki ifade | Cevap |
|---|---|
| "Pod `ImagePullBackOff`; mesaj `manifest unknown`" | Yanlış tag/digest/repository |
| "Pod `ImagePullBackOff`; mesaj `unauthorized`" | Kubelet kimliğinde **`AcrPull`** yok → `--attach-acr` |
| "Pod sürekli yeniden başlıyor, `Exit Code 1`" | `CrashLoopBackOff` → `kubectl logs --previous` |
| "`Reason: OOMKilled`, Exit Code 137" | **Bellek limiti** aşıldı |
| "Pod Running ama READY 0/1" | **Readiness probe** (yol/port) |
| "Service'e istek zaman aşımı; endpoint yok" | **Selector–label** uyuşmazlığı veya hazır pod yok |
| "Dağıtım sonrası sorun, hemen önceki sürüme dön" | `kubectl rollout undo deployment/<ad>` |
| "Çöken container'ın önceki logu" | `kubectl logs <pod> --previous` |
| "Pod içinde komut çalıştır" | `kubectl exec -it <pod> -- sh` |
| "AKS'in ACR'dan image çekmesi" | `az aks update --attach-acr` (kubelet identity + `AcrPull`) |
| "Kubeconfig'e küme ekle" | `az aks get-credentials` |
| "Kubernetes API/CRD gerekmiyor, scale-to-zero" | **Container Apps** |

**Tuzaklar:**
- `Deployment.selector` ile `template.labels` eşleşmeli; `Service.selector` da pod label'larıyla eşleşmeli — ikisi farklı hatalardır (apply reddi vs endpoint'siz Service).
- `containerPort` **bilgilendiricidir**; trafiği yönlendiren `Service.targetPort` ve uygulamanın gerçek dinlediği porttur.
- **Liveness** ≠ **readiness**: biri yeniden başlatır, öteki trafikten çıkarır.
- Bellek limit aşımı öldürür (**OOMKilled**), CPU limit aşımı yalnızca yavaşlatır (**throttle**).
- `kubectl logs` çöken container için çoğu zaman yeni (boş) instance'ı gösterir; **`--previous`** gerekir.
- `Secret` şifreli değil **base64 kodlu**dur.
- `rollout undo` yalnızca **Deployment** revision geçmişi üzerinden çalışır (`revisionHistoryLimit` ile sınırlı).

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| `:latest` tag'i ile dağıtıp `imagePullPolicy` yüzünden sürpriz yaşamak | Digest veya benzersiz tag; politika bilinçli seçilir |
| Request/limit vermemek | Zamanlama ve OOM/throttle öngörülebilir olsun; her container için ver |
| Probe yok / yanlış | Readiness yoksa hazır olmayan pod trafik alır; liveness çok agresifse sağlıklı pod öldürülür |
| `kubectl describe`'a bakmadan yeniden dağıtmak | Events çoğu zaman kök nedeni açıkça yazar |
| Secret'ı düz YAML olarak Git'e koymak | Key Vault + CSI driver / sealed secret / CI'dan enjeksiyon |
| Her şeyi `default` namespace'e koymak | Namespace + RBAC + ResourceQuota ile izole et |
| Sadece 1 replica ve `maxUnavailable` varsayılanı | Kesintisiz güncelleme için ≥2 replica; `maxUnavailable: 0`, `maxSurge: 1` |
| `--attach-acr`'yi unutmak | Kubelet kimliğine `AcrPull` olmadan image çekilemez |
| Basit bir API için AKS seçmek | Container Apps / App Service operasyon maliyetini düşürür |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| AKS vs Container Apps | Tam Kubernetes kontrolü, ekosistem | Node/upgrade/güvenlik operasyonu, boşta node maliyeti |
| Yüksek memory limit | OOM riski azalır | Node'da daha az pod; maliyet |
| Düşük CPU limit | Komşu pod'ları korur | Throttle → gecikme artışı |
| `maxUnavailable: 0, maxSurge: 1` | Kesintisiz güncelleme | Güncelleme sırasında geçici ek kaynak ve daha yavaş rollout |
| Agresif liveness probe | Takılan pod'u hızlı yeniden başlatır | Yavaş açılan uygulamada gereksiz restart döngüsü → **startup probe** kullan |
| `LoadBalancer` Service per app | Basitlik | Her biri ayrı public IP/maliyet; **Ingress/Gateway** ile birleştir |
| Pod başına küçük request | Yoğun paketleme, ucuz | Aşırı taahhüt (overcommit) ve gürültülü komşu riski |

---

## 8. Kaynaklar

- [AI-200 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/ai-200)
- [AKS overview](https://learn.microsoft.com/en-us/azure/aks/what-is-aks) · [Deploy an AKS cluster (CLI)](https://learn.microsoft.com/en-us/azure/aks/learn/quick-kubernetes-deploy-cli)
- [Authenticate with ACR from AKS (`--attach-acr`)](https://learn.microsoft.com/en-us/azure/aks/cluster-container-registry-integration)
- [Kubernetes: Deployments](https://kubernetes.io/docs/concepts/workloads/controllers/deployment/) · [Services](https://kubernetes.io/docs/concepts/services-networking/service/) · [Probes](https://kubernetes.io/docs/tasks/configure-pod-container/configure-liveness-readiness-startup-probes/) · [Debug Pods](https://kubernetes.io/docs/tasks/debug/debug-application/debug-pods/)
- [Update on the AKS application routing add-on and Ingress-NGINX](https://blog.aks.azure.com/2025/11/13/ingress-nginx-update) · [Application routing](https://learn.microsoft.com/en-us/azure/aks/app-routing)
- [Choose an Azure container service](https://learn.microsoft.com/en-us/azure/architecture/guide/choose-azure-container-service)
- [Monitor Container Apps logs](https://learn.microsoft.com/en-us/azure/container-apps/log-monitoring)
