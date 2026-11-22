[← Konu anlatımı](README.md) · [AZ-900 İçindekiler](../README.md)

### 📝 Bölüm 1.2 Quiz Soruları

> Teori: [README.md](README.md) · Sınav alanı: *Describe cloud concepts* (%25-30) · 22 soru
>
> Sorular **özgündür** ve sınav formatındadır; gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 18/22**.

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Bir e-ticaret sitesinin çalıştığı datacenter'lardan birinde elektrik kesintisi oldu; site, başka bir availability zone'daki kopyası sayesinde kullanıcılar fark etmeden yayında kaldı. Bu, bulutun hangi faydasını gösterir?

- A) Scalability
- B) High availability
- C) Predictability
- D) Governance

<details><summary>Cevap</summary>

**B.** Arızaya rağmen hizmetin **kesintisiz sürmesi** high availability'dir. Scalability kapasite değişimi, predictability öngörülebilirlik, governance uyumdur.
</details>

#### Soru 2: Tek seçim
Bir uygulama, bütün bir Azure region'ını etkileyen bir felaketten sonra eşleşik bölgede yeniden ayağa kaldırıldı ve verilerin çoğu korunmuş olarak çalışmaya devam etti. Bu en iyi hangi faydayla açıklanır?

- A) Reliability
- B) Elasticity
- C) Manageability
- D) Security

<details><summary>Cevap</summary>

**A.** Arızadan/felaketten **toparlanıp** çalışmaya devam etmek reliability'dir (felaket kurtarma bunun bir parçasıdır).
</details>

#### Soru 3: Tek seçim
Bir yönetici, kampanya öncesinde uygulamanın çalıştığı VM'in boyutunu 2 vCPU'dan 8 vCPU'ya yükseltti. Bu işlemin adı nedir?

- A) Scale out
- B) Scale in
- C) Scale up
- D) Failover

<details><summary>Cevap</summary>

**C.** Aynı kaynağı **daha güçlü** hale getirmek vertical scaling, yani scale up'tır. Scale out instance **sayısını** artırmaktır.
</details>

#### Soru 4: Tek seçim
Bir uygulamanın instance sayısı, CPU kullanımı %70'i geçtiğinde **otomatik olarak** 2'den 6'ya çıkıyor ve yük azalınca tekrar 2'ye iniyor. Bu, bulutun hangi faydasıdır?

- A) Elasticity
- B) High availability
- C) Reliability
- D) Predictability

<details><summary>Cevap</summary>

**A.** Ölçeklemenin yüke göre **otomatik** yapılması elasticity'dir. (Scalability bu yeteneğin kendisidir; "otomatik" kelimesi elasticity'yi işaret eder.)
</details>

#### Soru 5: Evet / Hayır

- **a) Scale out, mevcut sunucuya daha fazla bellek eklemek demektir.** — Evet: ☐ · Hayır: ☐
- **b) Horizontal scaling, kaynak sayısını artırmak veya azaltmaktır.** — Evet: ☐ · Hayır: ☐
- c) Vertical scaling'in, kullanılabilecek en büyük makine boyutuyla sınırlı bir üst limiti vardır. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Bellek/CPU eklemek scale **up**'tır; scale out instance sayısını artırır. Vertical scaling en büyük boyutla sınırlıdır.
</details>

#### Soru 6: Cümle tamamlama
"Bir sistemin talebe göre kaynak ekleme veya çıkarma **yeteneğine** \_\_\_\_\_\_\_\_\_\_ denir; bunun yüke göre **otomatik** yapılmasına ise \_\_\_\_\_\_\_\_\_\_ denir."

- A) elasticity · scalability
- B) scalability · elasticity
- C) availability · reliability
- D) reliability · scalability

<details><summary>Cevap</summary>

**B.** Scalability yetenek; elasticity onun otomatik kullanımı.
</details>

#### Soru 7: Tek seçim
Bir finans ekibi, önümüzdeki çeyreğin bulut harcamasını gerçek kullanım verilerine dayanarak tahmin edebiliyor ve bütçe aşımı olmadan planlama yapıyor. Bu hangi faydadır?

- A) Governance
- B) Predictability
- C) Security
- D) Elasticity

<details><summary>Cevap</summary>

**B.** Maliyetin öngörülebilmesi **cost predictability**'dir. Predictability'nin diğer yüzü performans öngörülebilirliğidir.
</details>

#### Soru 8: Tek seçim
Bir web sitesinin kullanıcı sayısı gün içinde 10 kat dalgalanmasına rağmen, autoscale ve yeterli kapasite sayesinde sayfa yanıt süresi sürekli aynı aralıkta kalıyor. Bu en iyi hangi faydayla açıklanır?

- A) Performance predictability
- B) Cost predictability
- C) Reliability
- D) Governance

<details><summary>Cevap</summary>

**A.** Yük değişse de performansın **öngörülebilir** kalması. (Autoscale aynı zamanda elasticity'dir; ama sorunun vurgusu "yanıt süresinin aynı kalması" yani performans öngörülebilirliğidir.)
</details>

#### Soru 9: Tek seçim
Bir kuruluş, tüm aboneliklerinde yalnızca Avrupa Birliği içindeki region'larda kaynak oluşturulmasına izin veren ve uyumsuz kaynakları raporlayan kurallar uyguluyor. Bu hangi faydadır?

- A) Security
- B) Governance
- C) High availability
- D) Scalability

<details><summary>Cevap</summary>

**B.** Kurumsal standartlara ve regülasyonlara **uyum**, uyumsuzluğun tespiti ve raporlanması governance'tır (araç: Azure Policy → C1.12).
</details>

#### Soru 10: Tek seçim
Bir şirket PaaS'a geçtikten sonra işletim sistemi güvenlik yamalarının bulut sağlayıcısı tarafından otomatik uygulanmasından ve altyapının DDoS saldırılarına karşı korunmasından faydalanıyor. Bu hangi faydadır?

- A) Security
- B) Governance
- C) Manageability
- D) Predictability

<details><summary>Cevap</summary>

**A.** Tehditlere karşı koruma ve yamaların sağlayıcıda olması security faydasıdır. Bulut, ihtiyaca göre kontrol düzeyi seçtirir: IaaS'ta tam kontrol, PaaS/SaaS'ta bakım sağlayıcıda.
</details>

#### Soru 11: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** *management of the cloud* (bulutun yönetimi) örneğidir?

- A) Kaynakların talebe göre otomatik ölçeklenmesi
- B) Azure portal üzerinden kaynak oluşturulması
- C) Bir kaynak sorun yaşadığında uyarı gönderilmesi
- D) Azure CLI komutlarının kullanılması

<details><summary>Cevap</summary>

**A ve C.** Bulutun yönetimi: otomatik ölçekleme, şablonla dağıtım, sağlık izleme ve uyarı. Portal ve CLI (B, D) *management in the cloud* — yani yönetim **arayüzleridir**.
</details>

#### Soru 12: Evet / Hayır

- a) Bir VM'in boyutunu 2 vCPU'dan 8 vCPU'ya çıkarmak **horizontal scaling**'dir. — Evet: ☐ · Hayır: ☐
- b) VM sayısını 2'den 6'ya çıkarmak **scale out**'tur. — Evet: ☐ · Hayır: ☐
- c) Horizontal scaling'de uygulamanın durumsuz (stateless) tasarlanması işi kolaylaştırır. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Hayır · b) Evet · c) Evet.** Aynı kaynağı güçlendirmek **vertical** (scale up) ölçeklemedir. Kaynak **sayısını** artırmak horizontal (scale out) ölçeklemedir; instance'lar arasında oturum durumu tutulmazsa yeni instance'lar yükü sorunsuz paylaşır.
</details>

#### Soru 13: Tek seçim
Bir şirket, gelecek ayın bulut faturasını Cost Management ile tahmin edebiliyor ve uygulamanın yanıt süresi kullanıcı sayısı artsa da hedefte kalıyor. Bu iki durum bulutun hangi faydasını gösterir?

- A) Governance
- B) Predictability
- C) Reliability
- D) Manageability

<details><summary>Cevap</summary>

**B.** Predictability'nin iki yüzü vardır: **performans** (yük değişse de yanıt süresi hedefte) ve **maliyet** (kullanımı izleyip faturayı öngörebilmek).
</details>

#### Soru 14: Tek seçim
Bir kuruluş, kaynakların yalnızca onaylı region'larda oluşturulmasını zorunlu kılmak ve uyumsuz kaynakları raporlamak istiyor. Bu ihtiyaç bulutun hangi faydasıyla ilgilidir?

- A) Security
- B) Scalability
- C) Governance
- D) High availability

<details><summary>Cevap</summary>

**C.** Kurumsal standartlara ve regülasyonlara **uyum** (kural, denetim, uyumsuzluk tespiti) governance'tır. Security ise saldırı/tehdit/koruma kontrolleriyle ilgilidir. Bu amaç için Azure'da **Azure Policy** kullanılır (→ C1.12).
</details>

#### Soru 15: Tek seçim
Bir ekip uygulamasının **yüksek erişilebilirliğini** artırmak istiyor. Hangi değişiklik bunu **en etkili** biçimde sağlar?

- A) Tek VM'i daha büyük bir boyuta yükseltmek
- B) Aynı VM'in iki kopyasını farklı availability zone'lara dağıtmak
- C) VM'i her gece elle yeniden başlatmak
- D) VM'in diskini küçültmek

<details><summary>Cevap</summary>

**B.** Erişilebilirliği yükselten şey **yedekliliktir**: birden çok instance'ı farklı zone'lara yaymak tek arıza noktasını kaldırır. Daha büyük VM (A) hâlâ tek arıza noktasıdır.
</details>

#### Soru 16: Tek seçim
Aşağıdakilerden hangisi bulutta **"management in the cloud"** (bulutta yönetim) kapsamında yer alır?

- A) Azure portal, Azure CLI ve REST API ile kaynakları yönetmek
- B) Yük artınca kaynakların otomatik ölçeklenmesi
- C) Arızalı kaynağın otomatik değiştirilmesi
- D) Şablonla otomatik ve tekrarlanabilir dağıtım

<details><summary>Cevap</summary>

**A.** *Management **in** the cloud*, yönetim **arayüzleridir**: portal, CLI, PowerShell, REST API. B, C ve D (otomatik ölçek, otomatik düzeltme, şablonla dağıtım) *management **of** the cloud* örnekleridir.
</details>

#### Soru 17: Eşleştirme
Her senaryoyu doğru faydayla eşleştirin: **High availability · Scalability · Reliability · Manageability**

1. Geliştiriciler kaynakları Azure CLI ve REST API ile yönetiyor.
2. Kampanya için uygulamaya elle 10 yeni instance eklendi.
3. Datacenter arızasında uygulama başka zone'dan yayında kaldı.
4. Bölgesel felaketten sonra sistem yedek bölgede yeniden çalıştırıldı.

<details><summary>Cevap</summary>

**1 → Manageability** (*in* the cloud) · **2 → Scalability** (elle olduğu için elasticity değil) · **3 → High availability** (kesintisiz devam) · **4 → Reliability** (toparlanma).
</details>

#### Soru 18: Evet / Hayır

- a) Elasticity, kaynakların iş yüküne göre otomatik olarak artırılıp azaltılmasıdır. — Evet: ☐ · Hayır: ☐
- b) High availability, bir felaketten sonra verilerin yedekten geri yüklenmesi anlamına gelir. — Evet: ☐ · Hayır: ☐
- c) Governance, kuruluşun kurallarına ve regülasyonlara uyumu sağlamaya yardımcı olur. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Felaketten sonra geri dönüş reliability/disaster recovery'dir; HA, kesinti **olmadan** çalışmayı sürdürmektir.
</details>

#### Soru 19: Tek seçim
Bir ekip, uygulamanın altyapısını her ortam için aynı **şablondan** dağıtıyor ve kaynakların sağlığını izleyip sorun olduğunda uyarı alıyor. Bu, bulutun hangi faydasının örneğidir?

- A) Manageability
- B) Elasticity
- C) Predictability
- D) High availability

<details><summary>Cevap</summary>

**A.** Şablonla dağıtım ve izleme/uyarı, *management of the cloud* yani manageability'dir (araçlar: ARM template'leri, Azure Monitor → C1.13, C1.14).
</details>

#### Soru 20: Senaryo
Bir haber sitesi, büyük bir olay olduğunda **önceden tahmin edilemeyen** trafik sıçramaları yaşıyor. Hem sıçrama anında kesinti yaşamamak hem de sakin zamanlarda boşta kapasite için ödeme yapmamak istiyor. Hangi yaklaşım **en uygundur**?

- A) Zirve trafiğe göre boyutlanmış sabit sayıda sunucu çalıştırmak
- B) Her sabah elle sunucu sayısını artırmak
- C) Metrik tabanlı autoscale kuralları ile yatay ölçekleme (elasticity)
- D) Tek bir çok büyük VM kullanmak

<details><summary>Cevap</summary>

**C.** Öngörülemeyen yükte maliyeti talebe bağlayan ve insan müdahalesi gerektirmeyen yaklaşım elasticity'dir. A boşta kapasite için öder; B tahmine dayalıdır ve öngörülemeyen sıçramayı kaçırır; D tek arıza noktasıdır ve üst sınırı vardır.
</details>

#### Soru 21: Eşleştirme
Her senaryoyu doğru faydayla eşleştirin: **Scalability · Predictability · Security · Governance**

1. Kuruluş, tüm kaynakların kurumsal adlandırma ve bölge kurallarına uyduğunu otomatik denetliyor.
2. Ekip, yük artmadan önce kaynak sayısını elle 4'ten 10'a çıkarabiliyor.
3. Sağlayıcı, işletim sistemi yamalarını PaaS'ta otomatik uyguluyor ve altyapıyı tehditlere karşı koruyor.
4. Finans ekibi, gelecek çeyreğin bulut harcamasını kullanım verisine göre güvenle tahmin ediyor.

<details><summary>Cevap</summary>

**1 → Governance** (kurallara uyum, denetim) · **2 → Scalability** (elle olduğu için elasticity değil) · **3 → Security** · **4 → Predictability** (maliyet).
</details>

#### Soru 22: Evet / Hayır

- a) Vertical scaling'in (scale up) bir üst sınırı vardır: en büyük VM boyutundan daha büyüğüne çıkılamaz. — Evet: ☐ · Hayır: ☐
- b) Horizontal scaling (scale out), birden çok instance kullandığı için yüksek erişilebilirliğe de katkı sağlar. — Evet: ☐ · Hayır: ☐
- c) Reliability, hizmetin hiçbir zaman kesintiye uğramayacağının garantisidir. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Scale up tek kaynağın sınırına takılır; scale out instance sayısını artırdığı için tek arıza noktasını da kaldırır. Reliability "hiç kesinti olmaz" değil, arızadan **toparlanıp** çalışmaya devam edebilmektir.
</details>

---

**Puanlama:** 20-22 → hazırsın · 16-19 → README 3.1'deki "karıştırılan çiftler"i tekrar et · < 16 → konuyu baştan çalış, sonra bu quiz'i yeniden çöz.

---

➡️ **Sonraki konu:** [1.3 Bulut Hizmet Türleri: IaaS, PaaS, SaaS, Serverless](../1.3-service-types-iaas-paas-saas/README.md) · [AZ-900 İçindekiler](../README.md)
