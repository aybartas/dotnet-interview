# C1.20 Azure AI, Machine Learning ve IoT Servisleri

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/20-ai-iot-services/` · Sınav alanı: **Describe Azure architecture and services — genişletilmiş kapsam (bağlam)** · Önkoşul: [C1.3](../03-cloud-service-types/README.md), [C1.10](../10-security/README.md) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Bağlantı:** AZ-900'ün eski sürümlerinde ve birçok kursta "popular Azure services" bölümü; **AI-200 (C2) ve AI-102/AI-900 yoluna zemin**.
>
> Bu konu resmî study guide'ın (July 20, 2026) doğrudan hedefi değildir; kurs müfredatlarında ve senaryo sorularının seçeneklerinde geçer.

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

Bu konu iki servis ailesini tanıtır; ikisi de "kendin kurmadan hazır kullan" mantığındadır:

| Aile | Soru | Servisler (özet) |
|---|---|---|
| **AI ve makine öğrenmesi** | "Metni/görüntüyü/sesi anlamak ya da kendi modelimi eğitmek istiyorum" | **Microsoft Foundry** (platform), **Foundry Tools / Azure AI services** (hazır API'ler), **Azure OpenAI / Foundry Models**, **Azure Machine Learning**, **Azure AI Search**, bot/agent servisleri |
| **IoT** | "Binlerce cihazı bağlayıp telemetri toplamak ve onlara komut göndermek istiyorum" | **IoT Hub**, **DPS**, **IoT Central**, **Digital Twins**, **IoT Edge** |

Ek olarak "diğer popüler servisler" listesinde **Azure Batch** (HPC), **Azure Local / Azure Stack** (hibrit) ve **Azure Communication Services** vardır (3.5).

> **Adlandırma uyarısı (Ekim 2026 itibarıyla):** Azure AI dünyasında adlar hızlı değişir. *Azure AI Studio* → *Azure AI Foundry* (Kasım 2024) → **Microsoft Foundry** (Kasım 2025, Ignite). Hazır API'ler *Cognitive Services* → *Azure AI services* → **Foundry Tools** olarak yeniden markalandı; Azure kaynak türü hâlâ `Microsoft.CognitiveServices/accounts`, CLI hâlâ `az cognitiveservices`. Eski sınav soruları ve kurslar eski adları kullanır. Sınavdan/mülakattan önce güncel adı Learn'den kontrol et.

Bu konunun C# kodu yoktur. Pratik, ücretsiz katmanda bir Language kaynağı ve REST çağrısıdır; isteğe bağlı olarak IoT Hub F1 (3.6).

---

## 2. Neden?

- **Sınav/kurs:** Eski AZ-900 sürümleri "popular products" olarak AI, IoT, Batch gibi servisleri sorardı; bugün bu servisler **seçeneklerde (distractor)** ve senaryo sorularında geçer: "hazır API mi, özel model mi?" ve "IoT Hub mı, Event Hubs mı?" klasiktir.
- **Gerçek işte:** AI/IoT projelerinin en sık hatası yanlış katmandan başlamaktır: metin dil tespiti için model eğitmek (gereksiz), ya da cihaz yönetimi için salt olay alımı servisi seçmek (yetersiz).
- **Kariyer yolu:** AI-900 → AI-102 → **AI-200 (C2)**. Burada ad dağarcığını edinirsin; geliştirici derinliği C2'dedir ve .NET tarafı M15'tedir (→ M15.4 embeddings & vector search).
- **Güvenlik (→ C1.18):** AI servis anahtarları ve cihaz bağlantı dizeleri **sırdır**; koda gömülmez.

---

## 3. Nasıl?

### 3.1 AI servis katmanları ⭐

Üç katmanı ayır; çoğu soru "hangi katman?" sorusudur:

| Katman | Ne | Kimin işi | Örnek |
|---|---|---|---|
| **1. Hazır API'ler** (prebuilt) | Önceden eğitilmiş modeller, REST/SDK ile çağır | Model eğitmezsin; veri gönderir, sonuç alırsın | Vision, Speech, Language, Translator, Document Intelligence |
| **2. Temel/üretken modeller** | LLM'ler ve diğer foundation model'ler; prompt, RAG, agent | Model seçer, prompt/veri bağlarsın | **Azure OpenAI / Foundry Models** |
| **3. Özel ML** | Kendi verinle model **eğit**, dağıt, yönet (MLOps) | Veri bilimci/ML mühendisi | **Azure Machine Learning** |

**Microsoft Foundry** — AI uygulama ve agent geliştirme **platformu**: model kataloğu (OpenAI ve diğer sağlayıcıların modelleri), dağıtım, değerlendirme, agent'lar, güvenlik/içerik filtreleri ve hazır AI servislerine tek yerden erişim. (Eski adlarıyla Azure AI Foundry / AI Studio.)

**Hazır API'ler (Foundry Tools / Azure AI services):**

| Servis | Ne yapar | Tipik iş |
|---|---|---|
| **Vision** (Image Analysis, OCR) | Görüntüde nesne/metin/etiket bulma | Ürün fotoğrafı etiketleme, OCR |
| **Face** *(bağlam; erişim sınırlı)* | Yüz algılama | Kimlik doğrulama senaryoları — sorumlu AI kısıtları var |
| **Speech** | Ses ↔ metin, konuşma sentezi, çeviri | Çağrı merkezi transkripsiyonu |
| **Language** | Duygu analizi, dil tespiti, varlık çıkarma, özetleme, soru-cevap | Yorum analizi |
| **Translator** | Metin çevirisi | Çok dilli içerik |
| **Document Intelligence** (eski adı Form Recognizer) | Fatura/makbuz/form alanlarını çıkarma | Fatura işleme |
| **Content Safety** | Zararlı içerik tespiti | Kullanıcı içeriği moderasyonu |
| **Azure AI Search** (eski adı Cognitive Search) | Arama + vektör/semantik arama + enrichment; RAG'ın "R"si | Kurumsal doküman arama (→ M15.4) |

**Azure OpenAI / Foundry Models:** OpenAI modellerini (ve katalogdaki diğer modelleri) Azure'ın güvenlik, ağ (private endpoint) ve uyumluluk sınırları içinde kullanma; GPT sohbet, embeddings, görüntü/ses modelleri. Model adları/sürümleri sık değiştiği için sınav sürüm değil **"ne işe yarar"** sorar.

**Azure Machine Learning (Azure ML):** Kendi modelini **eğitmek** ve yönetmek: workspace, **AutoML** (algoritma/hiperparametre aramasını otomatikleştirir), **designer** (sürükle-bırak pipeline), notebook'lar, model kaydı, **managed endpoint**'ler, **MLOps** (versiyonlama, izleme, yeniden eğitim).

**Bot/agent servisleri (özet):** Konuşmalı uygulamalar için *Azure AI Bot Service* + *Bot Framework SDK* geleneksel yoldu. Bot Framework SDK **archive** edildi ve Aralık 2025 sonrası destek bileti kapsamı dışına çıktı *(doğrula)*; Microsoft yeni işler için **Microsoft 365 Agents SDK**, **Copilot Studio** (low-code) ve **Foundry Agent Service**'i öneriyor. QnA Maker 31 Mart 2025'te emekliye ayrıldı. Sınavda "chatbot" cevabı için eski ad "Azure Bot Service" hâlâ görülebilir.

**Hazır API ↔ özel model karar tablosu:**

| İhtiyaç | Seçim | Gerekçe |
|---|---|---|
| Fatura PDF'inden toplam ve tarihi çek | **Document Intelligence** | Hazır model var; eğitim gereksiz |
| Müşteri yorumlarında duygu analizi | **Language** | Hazır |
| Çağrı kayıtlarını yazıya dök | **Speech** | Hazır |
| Kendi şirket verinle, sektörüne özgü bir tahmin modeli (ör. arıza tahmini) | **Azure Machine Learning** | Özel veri + özel hedef |
| Şirket dokümanları üzerinde soru-cevap yapan sohbet asistanı | **Foundry Models/Azure OpenAI + AI Search** (RAG) | Üretken model + kendi verin |
| Binlerce görüntüde şirkete özgü kusur tespiti (hazır etiketler yetmiyor) | **Azure ML** veya özel görüntü modeli eğitimi | Özel sınıflar |

> Kural: **Hazır bir API sorunu çözüyorsa model eğitme.** Eğitim; veri, zaman, MLOps ve maliyet demektir. Özel model yalnızca hazır API'nin kapsamadığı yerde.

### 3.2 Sorumlu AI ilkeleri *(bağlam — AI-900'de ağırlıklıdır)*

Microsoft'un altı ilkesi:

| İlke | Soru | Örnek |
|---|---|---|
| **Fairness** (adalet) | Sistem herkese eşit davranıyor mu? | Kredi modeli cinsiyete göre sapmıyor |
| **Reliability & safety** (güvenilirlik ve güvenlik) | Beklenen koşullarda ve uç durumlarda güvenle çalışıyor mu? | Otonom sistemde test, güvenli hata modu |
| **Privacy & security** (gizlilik ve güvenlik) | Kişisel veriyi koruyor mu? | Eğitim verisinde anonimleştirme, şifreleme |
| **Inclusiveness** (kapsayıcılık) | Engelli dahil herkesi dikkate alıyor mu? | Konuşma/altyazı, erişilebilirlik |
| **Transparency** (şeffaflık) | Sistemin nasıl karar verdiği anlaşılabilir mi? | "Bu karar şu faktörlere dayandı" açıklaması |
| **Accountability** (hesap verebilirlik) | Kim sorumlu? İnsan denetimi var mı? | Karar sürecinde insan onayı |

### 3.3 Azure IoT servisleri ⭐

**Azure IoT Hub** — bulutta barındırılan, IoT cihazları ile bulut arasında **güvenli, çift yönlü** iletişim sağlayan **PaaS** servis.

| Özellik | Ne |
|---|---|
| **Device-to-cloud (D2C)** | Cihazdan bulutta telemetri alımı (MQTT, AMQP, HTTPS) |
| **Cloud-to-device (C2D)** | Buluttan cihaza mesaj, **direct method** (anlık komut), **configuration** |
| **Device identity registry** | Her cihazın kimliği ve anahtarı/sertifikası; tek tek iptal edilebilir |
| **Device twin** | Cihazın bulutta tutulan **JSON durum kopyası** (desired/reported properties, tag'ler); cihaz çevrimdışıyken bile istenen durum bırakılır |
| **Message routing** | Mesajları koşula göre Storage, Event Hubs, Service Bus'a yönlendirme; yerleşik Event Hubs uyumlu uç nokta |
| **Device Provisioning Service (DPS)** | Binlerce cihazı **sıfır dokunuşla** (zero-touch) doğru IoT Hub'a kaydeder, yük dağıtır |

**Diğer IoT servisleri:**

| Servis | Konum | Tek cümle |
|---|---|---|
| **IoT Central** | **SaaS** | Kod azıyla cihaz yönetimi + panolar + kurallar; IoT Hub'ın üstünde hazır uygulama. *(Şubat 2024'te yanlışlıkla "emekli oluyor" bildirimi çıktı; Microsoft bunu hatalı sistem mesajı olarak düzeltti. Güncel durumu Azure Updates'ten doğrula.)* |
| **Azure Digital Twins** | PaaS | Fiziksel ortamın (bina, fabrika) **dijital ikizini** grafik modelle (DTDL) kurma |
| **Azure IoT Edge** | Cihaz üstü çalışma zamanı | Bulut mantığını (container'lar) **cihazda/uçta** çalıştırma; çevrimdışı çalışma, gecikme azaltma |
| **Azure Sphere** *(bağlam)* | Güvenli MCU platformu | Duyuruya göre **31 Temmuz 2031'de** tüm güncelleme desteği biter (Azure Updates AZ557123 — doğrula); yeni tasarım için önerilmez |
| **Stream Analytics / Event Hubs** | Akan veri | Telemetriyi gerçek zamanlı işleme/alım (→ C1.16, 3.4) |

**Uçtan uca akış:**

```
 Device ──D2C──▶ IoT Hub ──routing──▶ Stream Analytics ──▶ Storage / Cosmos DB ──▶ Power BI
   ▲               │ ▲                                             (hot/cold path)
   │               │ └── device twin (desired/reported), identity registry
   └──C2D/direct───┘
 New devices ──▶ DPS ──▶ (assigns) IoT Hub          Edge devices ◀── IoT Edge modules
```

**IoT Hub ↔ Event Hubs** ⭐ (sık tuzak):

| | **IoT Hub** | **Event Hubs** |
|---|---|---|
| Yön | **Çift yönlü** (D2C + C2D) | Tek yönlü: yüksek hacimli **alım** (ingest) |
| Cihaz kimliği | **Cihaz başına** kimlik, iptal | Yok (paylaşımlı erişim anahtarları/SAS) |
| Cihaz yönetimi | Twin, direct methods, config | Yok |
| Kullanım | IoT cihaz filosu | Uygulama log'u/telemetrisi, clickstream, Kafka uyumlu alım (→ C1.21) |

> Pratik karar: cihazlara **komut gönderiyor / cihazları yönetiyor / cihaz kimliği** gerekiyorsa **IoT Hub**; yalnızca çok yüksek hacimli olay akışı alıyorsan **Event Hubs**. Gerçekte ikisi birlikte de kullanılır (IoT Hub'ın yerleşik uç noktası Event Hubs uyumludur).

### 3.4 Akan veri ve analitik bağlantısı

IoT/uygulama telemetrisi **hot path** (anlık: Stream Analytics → alarm) ve **cold path** (toplu: Storage/Data Lake → analiz → Power BI) olarak ikiye ayrılır. Veri/analitik servisleri C1.16'dadır (Stream Analytics, Event Hubs, Data Lake, Synapse/Fabric).

### 3.5 Diğer popüler servisler *(bağlam)*

| Servis | Ne | Tek cümle |
|---|---|---|
| **Azure Batch** | HPC / büyük ölçekli paralel iş | VM havuzlarını otomatik oluşturup işleri dağıtır; **Batch servisinin kendisi için ücret yok, kullanılan compute/depolama ücretli** *(doğrula)*; render, simülasyon, Monte Carlo |
| **Azure Local** (eski adı Azure Stack HCI) | Hibrit | Kurum içi donanımda Azure destekli altyapı (→ C1.1) |
| **Azure Stack Hub / Edge** | Hibrit/uç | Kurum içinde Azure benzeri hizmetler / uçta (edge) donanım cihazı. *Azure Stack ailesinin adları ve ürün durumu değişti; güncel kataloğu kontrol et* |
| **Azure Communication Services** | CPaaS | Uygulamaya **SMS, sesli/görüntülü arama, sohbet, e-posta** ekleme |
| **Azure Arc** | Hibrit yönetim | Kurum içi/başka bulut kaynaklarını Azure'dan yönetme (→ [C1.13](../13-management-deployment-tools/README.md)) |

### 3.6 Pratik — Language kaynağı (F0) ve tek REST çağrısı (`az` CLI)

Ücretsiz katman (F0), dil tespiti için fazlasıyla yeterlidir; F0 sınırlıdır (sınırlı aylık işlem, abonelik/bölge başına sınır) *(güncel kotayı doğrula)*. **Anahtarı koda veya depoya yazma** (→ C1.18); kabuk değişkeninde tut ve işin sonunda kaynağı sil. Bütçe alarmın (→ [C1.11](../11-cost-management/README.md)) kurulu olsun.

```bash
RG=rg-az900-c120
LOC=westeurope
LANG_NAME=lang-az900-c120-$RANDOM
az group create --name $RG --location $LOC

# 1) Language resource, free tier (kind "TextAnalytics" = Language; F0 = free).
#    If a free resource of this kind already exists in the subscription the command fails - then use sku S0 briefly or skip.
az cognitiveservices account create --resource-group $RG --name $LANG_NAME \
  --kind TextAnalytics --sku F0 --location $LOC --yes

# 2) Endpoint and key (secret: keep it in a shell variable, never in source code)
ENDPOINT=$(az cognitiveservices account show --resource-group $RG --name $LANG_NAME \
  --query properties.endpoint --output tsv)
KEY=$(az cognitiveservices account keys list --resource-group $RG --name $LANG_NAME \
  --query key1 --output tsv)

# 3) One REST call: language detection (the exact API version/path may change; see the REST reference)
curl -s -X POST "${ENDPOINT}language/:analyze-text?api-version=2023-04-01" \
  -H "Ocp-Apim-Subscription-Key: $KEY" -H "Content-Type: application/json" \
  -d '{
        "kind": "LanguageDetection",
        "parameters": { "modelVersion": "latest" },
        "analysisInput": { "documents": [
          { "id": "1", "text": "Hello, how are you?" },
          { "id": "2", "text": "Bugun hava cok guzel." } ] }
      }'

# 4) Cleanup
az group delete --name $RG --yes --no-wait
```

Beklenen gözlem: Her doküman için `detectedLanguage` alanında `name`, `iso6391Name` (`en`, `tr`) ve bir `confidenceScore` döner. Uç nokta bölgesel veya özel alt alan adıyla farklı gelebilir; 401/404 alırsan Learn'deki güncel *Language REST* başlangıç sayfasına bak. **Gözlem:** hiç model eğitmedin; bu, "hazır API" katmanıdır.

**İsteğe bağlı — IoT Hub F1 + simüle cihaz** *(F1: abonelik başına **1 tane**; günlük mesaj kotası küçüktür, üçüncü taraf kaynaklar 8.000 mesaj/gün der — portaldaki Overview'dan doğrula; `azure-iot` CLI uzantısı gerekir)*:

```bash
RG=rg-az900-c120b
az group create --name $RG --location westeurope
az extension add --name azure-iot
HUB=hub-az900-c120-$RANDOM
az iot hub create --resource-group $RG --name $HUB --sku F1 --partition-count 2
az iot hub device-identity create --hub-name $HUB --device-id sim-device-01
az iot device simulate --hub-name $HUB --device-id sim-device-01 --msg-count 5   # device-to-cloud telemetry
az iot hub monitor-events --hub-name $HUB --timeout 30                           # read what the hub received
az group delete --name $RG --yes --no-wait
```

> `monitor-events` ve `device simulate` uzantı sürümüne göre değişebilir; çalışmazsa portalda *IoT Hub → Devices → Test* veya Device Explorer yolu aynı fikri gösterir.

---

## 4. Gerçek dünya senaryosu

Bir gıda üreticisi, üç fabrikadaki 4.000 soğutucu ve konveyör sensörüyle üretimi izlemek ve müşteri destek sürecini otomatikleştirmek istiyor.

1. **Cihaz tarafı:** Sensörler **IoT Hub**'a bağlanır; her birinin kimliği vardır. Yeni fabrika açılınca cihazlar **DPS** ile otomatik kayıt olur. Fabrikada internet kesilse bile **IoT Edge** modülleri yerelde eşik alarmını çalıştırır.
2. **Veri akışı:** Telemetri IoT Hub'dan **Stream Analytics**'e yönlenir; sıcaklık aşımı **hot path**'te saniyeler içinde alarm üretir, ham veri **Data Lake**'e (cold path) yazılır, **Power BI** panosunda üretim kalitesi izlenir. Bir soğutucu sorunlu çıkınca bulut, **direct method** ile cihaza "kompresör yeniden başlat" komutu yollar; **device twin**'de istenen eşik değeri güncellenir.
3. **Neden Event Hubs değil?** Cihaz kimliği, komut gönderme ve filo yönetimi gerektiğinden IoT Hub. (Aynı şirketin web sitesi click-stream'i ise Event Hubs'a gider.)
4. **Destek tarafı:** Müşteri e-postaları **Language** servisiyle duygu ve konu analizine girer; sipariş faturaları **Document Intelligence** ile okunur — **hiç model eğitilmeden**. Çağrı merkezi kayıtları **Speech** ile yazıya dökülür.
5. **Özel model:** Soğutucu arızalarını **7 gün önceden tahmin eden** model şirketin kendi geçmiş sensör verisine dayanır; hazır bir API bunu bilemez → **Azure Machine Learning** ile eğitilir, managed endpoint'ten sunulur, **MLOps** ile aylık yeniden eğitilir.
6. **Üretken AI:** Teknisyenlerin bakım kılavuzlarında soru sorabildiği asistan için **Foundry Models/Azure OpenAI + AI Search** (RAG) kullanılır; modele özel veri verilirken private endpoint ve Entra kimliği tercih edilir, anahtarlar **Key Vault**'ta tutulur.
7. **Sorumlu AI:** Arıza tahmin modelinin bir fabrikaya sistematik olarak daha çok yanlış alarm verdiği görülür (**fairness/reliability**); sonuçlar teknisyene **açıklanır** (**transparency**) ve üretim durdurma kararı **insan onayına** bağlanır (**accountability**).

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → servis eşleştirmesi (hazır API ↔ Azure ML; IoT Hub ↔ Event Hubs ↔ IoT Central) ve sorumlu AI ilkelerini tanımla eşleştirme.

| Soru kalıbı | Doğru cevap | Tuzak |
|---|---|---|
| "Önceden eğitilmiş görüntü/metin/konuşma API'si" | **Azure AI services (Foundry Tools)** | Azure Machine Learning |
| "**Kendi verinle** özel model eğit, dağıt, MLOps" | **Azure Machine Learning** | Language, Vision |
| "Fatura/formdan alan çıkar" | **Document Intelligence** | Vision (genel OCR) |
| "Konuşmayı yazıya çevir / sesi sentezle" | **Speech** | Language |
| "Metni başka dile çevir" | **Translator** | Language |
| "Dokümanlarda arama + vektör arama" | **Azure AI Search** | Cosmos DB |
| "Üretken AI modelleri (GPT), güvenli kurumsal kullanım" | **Azure OpenAI / Foundry Models** | Azure ML |
| "Cihaz-bulut **çift yönlü** iletişim, cihaz kimliği, device twin" | **IoT Hub** | Event Hubs |
| "Yüksek hacimli olay **alımı**, Kafka uyumu, cihaz yönetimi yok" | **Event Hubs** | IoT Hub |
| "Binlerce cihazı **otomatik** kaydet (zero-touch)" | **IoT Hub Device Provisioning Service** | IoT Central |
| "Kodsuz/low-code IoT **SaaS** uygulaması" | **IoT Central** | IoT Hub (PaaS) |
| "Fiziksel ortamın dijital ikizi/modeli" | **Azure Digital Twins** | IoT Hub |
| "Bulut mantığını cihazda/uçta çalıştır" | **IoT Edge** | Azure Arc |
| "Büyük ölçekli paralel/HPC iş" | **Azure Batch** | VM Scale Set |
| "Uygulamaya SMS/arama/sohbet ekle" | **Azure Communication Services** | Logic Apps |
| "Modelin kararlarını açıklayabilmek" | **Transparency** | Accountability |
| "Verilerin korunması" | **Privacy & security** | Fairness |
| "Herkes, engelliler dahil kullanabilsin" | **Inclusiveness** | Fairness |

**Anahtar kelimeler:** *prebuilt, pretrained, cognitive* → AI services · *train, custom model, MLOps, AutoML, designer* → Azure ML · *telemetry, bi-directional, device twin, provisioning* → IoT Hub/DPS · *ingest millions of events/s* → Event Hubs · *digital twin graph* → Digital Twins · *HPC* → Batch.

**Evet/hayır tuzakları:**
- "Dil tespiti için önce bir model eğitmen gerekir" → **Hayır** (hazır API).
- "Event Hubs ile cihaza komut gönderilebilir" → **Hayır** (C2D/direct method IoT Hub'da).
- "IoT Hub PaaS'tır; IoT Central SaaS'tır" → **Evet**.
- "Azure ML, yalnızca hazır eğitilmiş API'leri çağırmak içindir" → **Hayır** (kendi modelini eğitme/yönetme).
- "Sorumlu AI'da 'accountability', sistem sahiplerinin sonuçlardan sorumlu olması demektir" → **Evet**.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Hazır API yeterken model eğitmeye kalkmak | Önce hazır API (Language/Vision/Speech/Document Intelligence); özel model son çare |
| "Azure OpenAI = Azure ML" | Biri **hazır/foundation model kullanımı**, diğeri **kendi modelini eğitme ve yönetme**; karıştırılır |
| Servis adlarını ezberleyip güncel adı kontrol etmemek | Cognitive Services → AI services → Foundry Tools; AI Studio → AI Foundry → Microsoft Foundry. Eski kurslarda eski ad |
| IoT filosunu Event Hubs ile yönetmeye çalışmak | Cihaz kimliği, twin ve komut için IoT Hub |
| IoT Hub'ı cihaz kimlik bilgisini kodda/depoda tutarak kullanmak | DPS + sertifika/TPM; anahtarlar Key Vault'ta; bağlantı dizesi depoya girmez (→ C1.18) |
| AI servis anahtarını istemci tarafına (tarayıcı/mobil) gömmek | Anahtar sunucuda, tercihen **Entra kimliği/managed identity** ile; istemci doğrudan çağırmaz |
| F0/F1 ücretsiz katmanını üretimde kullanmak | Sınırlı kota, SLA yok/sınırlı; yalnızca deneme (→ [C1.22](../22-support-sla-lifecycle/README.md)) |
| Sorumlu AI'yı "sadece hukuk işi" saymak | Tasarım ve test sürecine girer: veri dengesi, açıklanabilirlik, insan denetimi |
| Edge/Arc/Azure Stack'i aynı şey sanmak | Arc = yönetim düzlemi; IoT Edge = cihazda container çalıştırma; Azure Local/Stack = kurum içi altyapı |
| Deneme kaynağını silmeyi unutmak | Kaynak grubunu sil; IoT Hub'ın F1 kotası aboneliğe bir tane! |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Hazır API (AI services) | Dakikalar içinde sonuç, eğitim yok, sürekli iyileşen model | Genel amaçlı; alana özgü doğruluk sınırlı; çağrı başına ücret, veri bölgesi/gizlilik değerlendirmesi |
| Özel model (Azure ML) | Alana özgü doğruluk, tam kontrol | Veri toplama/etiketleme, MLOps, eğitim/çıkarım maliyeti, uzman gerekir |
| Foundation model + RAG | Yeniden eğitim olmadan şirket verisine dayalı yanıt | Token maliyeti, gecikme, halüsinasyon riski; içerik filtresi ve değerlendirme gerekir |
| IoT Hub (PaaS) | Güvenlik, ölçek, cihaz yönetimi hazır | Kurulum/tasarım, birim ve mesaj sayısına göre ücret |
| IoT Central (SaaS) | Hızlı başlangıç, kodsuz pano | Esneklik ve özelleştirme sınırlı; platform bağımlılığı |
| Edge işleme | Düşük gecikme, çevrimdışı çalışma, bant genişliği tasarrufu | Cihaz yönetimi/güncelleme karmaşıklığı |
| Hot path (Stream Analytics) | Saniyelik alarm | Sürekli çalışan işlem maliyeti |
| Cold path (Data Lake) | Ucuz uzun süreli depolama, geriye dönük analiz | Anlık değil |
| Ücretsiz katman | Maliyetsiz öğrenme | Kota, bölge/abonelik sınırı, üretime uygun değil |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Foundry documentation](https://learn.microsoft.com/en-us/azure/ai-foundry/) *(URL yapısı rebrand ile değişebilir; "Microsoft Foundry" araması)* · [Foundry Tools (Azure AI services) overview](https://learn.microsoft.com/en-us/azure/ai-services/what-are-ai-services)
- [Azure Machine Learning](https://learn.microsoft.com/en-us/azure/machine-learning/overview-what-is-azure-machine-learning) · [Azure AI Search](https://learn.microsoft.com/en-us/azure/search/search-what-is-azure-search)
- [Responsible AI principles](https://www.microsoft.com/en-us/ai/responsible-ai) · [Microsoft Learn — Fundamental principles of responsible AI](https://learn.microsoft.com/en-us/training/modules/get-started-ai-fundamentals/)
- [What is Azure IoT Hub?](https://learn.microsoft.com/en-us/azure/iot-hub/iot-concepts-and-iot-hub) · [IoT Hub quotas and throttling](https://learn.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-quotas-throttling) · [Device Provisioning Service](https://learn.microsoft.com/en-us/azure/iot-dps/about-iot-dps)
- [Azure IoT Central](https://learn.microsoft.com/en-us/azure/iot-central/core/overview-iot-central) · [Azure Digital Twins](https://learn.microsoft.com/en-us/azure/digital-twins/overview) · [Azure IoT Edge](https://learn.microsoft.com/en-us/azure/iot-edge/about-iot-edge)
- [Azure Updates — retirements (Azure Sphere, Bot Framework SDK vb.)](https://azure.microsoft.com/updates/) · [Azure Batch](https://learn.microsoft.com/en-us/azure/batch/batch-technical-overview) · [Azure Communication Services](https://learn.microsoft.com/en-us/azure/communication-services/overview)
- [`az cognitiveservices`](https://learn.microsoft.com/en-us/cli/azure/cognitiveservices) · [`az iot`](https://learn.microsoft.com/en-us/cli/azure/iot)
