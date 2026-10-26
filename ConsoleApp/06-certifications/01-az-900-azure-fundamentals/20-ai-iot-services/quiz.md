# C1.20 Azure AI, Machine Learning ve IoT Servisleri — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *genişletilmiş kapsam (bağlam)* · 13 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, sıralama, eşleştirme, senaryo). Gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ %80** (13 soruda ≥ 11).

---

### 1. Tek seçim
Bir e-ticaret şirketi, müşteri yorumlarının olumlu mu olumsuz mu olduğunu **kendi model eğitmeden** otomatik belirlemek istiyor. Hangi servis ailesi en uygundur?

- A) Azure Machine Learning ile sıfırdan model eğitimi
- B) Azure AI services / Foundry Tools içindeki Language servisi
- C) Azure IoT Hub
- D) Azure Batch

<details><summary>Cevap</summary>

**B.** Duygu analizi, Language servisinin hazır yeteneğidir. Hazır API sorunu çözüyorsa model eğitmek gereksizdir.
</details>

### 2. Tek seçim
Bir enerji şirketi, **kendi geçmiş sensör verisiyle** trafo arızalarını önceden tahmin eden özel bir model **eğitmek, dağıtmak ve izlemek** istiyor. Hangi servis uygundur?

- A) Azure Machine Learning
- B) Translator
- C) Azure Communication Services
- D) Event Hubs

<details><summary>Cevap</summary>

**A.** Özel model eğitimi ve MLOps Azure Machine Learning'in alanıdır; hazır API şirkete özgü arıza tahminini bilmez.
</details>

### 3. Eşleştirme
Her ihtiyacı servisle eşleştirin: **Speech · Translator · Document Intelligence · Azure AI Search · Vision**

1. Faturalardan toplam tutar ve tarih alanlarını çıkarmak
2. Çağrı merkezi kayıtlarını metne dökmek
3. Ürün fotoğraflarındaki nesneleri etiketlemek
4. Web sitesi metnini 20 dile çevirmek
5. Kurumsal dokümanlarda tam metin ve vektör araması yapmak

<details><summary>Cevap</summary>

**1 → Document Intelligence · 2 → Speech · 3 → Vision · 4 → Translator · 5 → Azure AI Search.**
</details>

### 4. Tek seçim
Microsoft'un AI geliştirme platformu için doğru ad ve rebrand sırası hangisidir (2026)?

- A) Azure Cognitive Studio → Azure Studio → Foundry
- B) Azure AI Studio → Azure AI Foundry → Microsoft Foundry
- C) Azure ML Studio → Azure AI Hub → Fabric AI
- D) Bot Studio → Copilot Studio → Foundry

<details><summary>Cevap</summary>

**B.** Azure AI Studio (2023) → Azure AI Foundry (Kasım 2024) → Microsoft Foundry (Kasım 2025). Hazır API'ler de Cognitive Services → Azure AI services → Foundry Tools olarak yeniden markalandı. Adlar sık değiştiği için güncel olanı Learn'den doğrula.
</details>

### 5. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Metin dilini tespit etmek için önce kendi eğitim verinle bir model eğitmen gerekir. | ☐ | ☐ |
| b) Azure OpenAI/Foundry Models, kendi modelini sıfırdan eğitme platformu olan Azure Machine Learning ile aynı şeydir. | ☐ | ☐ |
| c) AI servis anahtarı kaynak kodunda ve sürüm kontrolünde tutulmamalıdır. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Hayır · b) Hayır · c) Evet.** Dil tespiti hazır API'dir. Foundation model kullanımı ile özel model eğitimi farklı katmanlardır. Anahtarlar sırdır (Key Vault / managed identity — → C1.18).
</details>

### 6. Eşleştirme
Her tanımı sorumlu AI ilkesiyle eşleştirin: **Fairness · Reliability & safety · Privacy & security · Inclusiveness · Transparency · Accountability**

1. Modelin bir karara nasıl vardığı kullanıcılara açıklanabilir
2. Bir kredi modeli, benzer profildeki başvuranlara cinsiyetten bağımsız davranır
3. Sistemin sahipleri, sonuçlardan ve insan denetiminden sorumludur
4. Kişisel veriler eğitimde ve çıkarımda korunur
5. Sistem, engelli kullanıcılar dahil geniş bir kitle için tasarlanmıştır
6. Sistem, beklenmedik girdilerde bile güvenli ve tutarlı çalışır

<details><summary>Cevap</summary>

**1 → Transparency · 2 → Fairness · 3 → Accountability · 4 → Privacy & security · 5 → Inclusiveness · 6 → Reliability & safety.**
</details>

### 7. Tek seçim
Bir üretici, 10.000 sensörle **iki yönlü** iletişim kurmak (telemetri toplamak ve cihazlara komut göndermek), her cihaza **ayrı kimlik** vermek ve cihazın son durumunu bulutta bir JSON kopyası olarak tutmak istiyor. Hangi servis?

- A) Azure Event Hubs
- B) Azure Service Bus
- C) Azure IoT Hub
- D) Azure Stream Analytics

<details><summary>Cevap</summary>

**C.** IoT Hub: D2C + C2D, cihaz başına kimlik ve device twin. Event Hubs yüksek hacimli tek yönlü alımdır; cihaz yönetimi yoktur.
</details>

### 8. Eşleştirme
Her tanımı IoT servisiyle eşleştirin: **Device Provisioning Service · IoT Central · Azure Digital Twins · IoT Edge · Device twin**

1. Bir fabrikanın dijital ikizini grafik modelle kurmak
2. Bulut mantığını (container'lar) cihaz üzerinde, çevrimdışı çalışabilecek şekilde yürütmek
3. Binlerce yeni cihazı sıfır dokunuşla doğru IoT Hub'a kaydetmek
4. Cihazın desired/reported özelliklerini bulutta JSON olarak tutmak
5. IoT çözümünü SaaS olarak, az kodla panolar ve kurallarla işletmek

<details><summary>Cevap</summary>

**1 → Digital Twins · 2 → IoT Edge · 3 → Device Provisioning Service · 4 → Device twin · 5 → IoT Central.**
</details>

### 9. Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** IoT Hub ile Event Hubs arasındaki doğru farklardır?

- A) IoT Hub cihaza komut gönderebilir; Event Hubs gönderemez.
- B) Event Hubs cihaz başına kimlik ve device twin sunar; IoT Hub sunmaz.
- C) IoT Hub cihaz başına kimlik yönetimi sunar; Event Hubs yüksek hacimli olay alımına odaklanır.
- D) İkisi de yalnızca SaaS'tır.

<details><summary>Cevap</summary>

**A ve C.** B tam tersidir; D yanlıştır (ikisi de PaaS'tır; IoT Central SaaS'tır).
</details>

### 10. Senaryo
Bir şirket, telemetriyi iki yoldan işlemek istiyor: sıcaklık eşiği aşıldığında **saniyeler içinde alarm** ve aynı verinin **ucuz uzun süreli depolanıp** sonradan Power BI'da incelenmesi. Aşağıdaki akıştan hangisi doğrudur?

- A) Cihaz → IoT Hub → Stream Analytics (anlık alarm) + Data Lake (uzun süreli depolama) → Power BI
- B) Cihaz → Azure Batch → Active Directory
- C) Cihaz → Azure Backup → Site Recovery
- D) Cihaz → Azure DNS → Azure Policy

<details><summary>Cevap</summary>

**A.** Hot path (Stream Analytics) anlık işler; cold path (Data Lake) toplu analiz içindir; Power BI raporlar.
</details>

### 11. Tek seçim
Bir geliştirme ekibi, mobil uygulamalarına **SMS, görüntülü arama ve sohbet** özelliği eklemek istiyor. Hangi Azure servisi bunu sağlar?

- A) Azure Communication Services
- B) Azure Batch
- C) Azure Digital Twins
- D) Azure Machine Learning

<details><summary>Cevap</summary>

**A.** Communication Services; SMS, sesli/görüntülü arama, sohbet ve e-posta için CPaaS.
</details>

### 12. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) IoT Hub bir PaaS servistir; IoT Central ise SaaS yaklaşımıdır. | ☐ | ☐ |
| b) Azure Batch, büyük ölçekli paralel/HPC işleri çalıştırmak için kullanılır. | ☐ | ☐ |
| c) IoT Hub'ın F1 (ücretsiz) katmanı üretim için önerilir. | ☐ | ☐ |
| d) Event Hubs ile bir cihazın **desired** özelliğini güncelleyebilirsin. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır · d) Hayır.** Ücretsiz katman deneme içindir (abonelik başına bir tane, küçük kota). Desired property'ler device twin'in parçasıdır; IoT Hub'ın özelliğidir.
</details>

### 13. Senaryo
Bir bankanın veri bilimi ekibi, kendi iç dokümanları üzerinde soru-cevap yapan bir sohbet asistanı geliştiriyor. Modeli **sıfırdan eğitmek istemiyorlar**; hazır bir üretken modeli kendi verileriyle beslemek (arama sonuçlarını yanıta eklemek) istiyorlar. Aşağıdakilerden hangi kombinasyon bu yaklaşıma (RAG) en uygundur?

- A) Azure OpenAI / Foundry Models + Azure AI Search
- B) Azure Batch + Azure Local
- C) IoT Hub + Digital Twins
- D) Azure Backup + Site Recovery

<details><summary>Cevap</summary>

**A.** RAG'de arama servisi (AI Search) ilgili doküman parçalarını bulur; üretken model bunlarla yanıt üretir. Model eğitmeye gerek yoktur (→ M15.4).
</details>

---

**Puanlama:** 12-13 → hazırsın · 9-11 → README 3.1 (üç katman, karar tablosu) ve 3.3 (IoT Hub ↔ Event Hubs) bölümlerini tekrar et · < 9 → konuyu baştan çalış ve 3.6'daki laboratuvarı uygula.
