# C1.15 Deneme Sınavı ve Sınav Stratejisi

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/15-practice-exam/` · Tüm sınav alanları (C1.1–C1.14) · Deneme sınavı: [`quiz.md`](quiz.md) (50 soru)
>
> 📌 Bu konu yeni bir resmî hedef eklemez; **skills measured as of July 20, 2026** müfredatının üç alanını tek sınavda ölçer. Sınav formatı, süre, ücret ve dil seçenekleri değişebilir — sınavdan önce resmî [AZ-900 sertifika sayfası](https://learn.microsoft.com/en-us/credentials/certifications/azure-fundamentals/) ve [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

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

AZ-900 bir **genişlik** sınavıdır: derin yapılandırma değil, kavramların ve Azure servislerinin **doğru senaryoya eşleştirilmesi** ölçülür. Bu konu üç parçadan oluşur:

| Parça | Dosya | Amaç |
|---|---|---|
| **Deneme sınavı** | [`quiz.md`](quiz.md) | Alan ağırlıklarına göre dağıtılmış **50 özgün soru**; her soru bir konu koduna (C1.x) bağlı |
| **Puanlama ve tekrar planı** | `quiz.md` sonu | Alan bazlı puan tablosu; yanlışları C1.x'e eşleyip zayıf konuları tekrar etme |
| **Sınav stratejisi** | Bu README | Soru türleri, eleme yöntemi, anahtar kelimeler, zaman yönetimi, sınav günü |

**Sınavın genel çerçevesi** (ayrıntılar değişebilir, resmî sayfadan doğrula):

| | |
|---|---|
| Alanlar ve ağırlıklar | *Describe cloud concepts* %25-30 · *Describe Azure architecture and services* %35-40 · *Describe Azure management and governance* %30-35 |
| Geçme puanı | **700 / 1000** (ölçeklenmiş puan; "doğru yüzdesi" ile birebir aynı değildir) |
| Soru sayısı ve süre | Fundamentals sınavlarında genellikle birkaç düzine soru ve kısa bir süre (yaklaşık 45 dakika civarı) bulunur; kesin değerler sınav sayfasında ve sınav başında gösterilir |
| Soru türleri | Tek seçim, çoklu seçim ("iki/üç doğru cevap seçin"), **evet/hayır** tabloları, eşleştirme/sürükle-bırak, cümle tamamlama (açılır liste), kısa senaryolar |
| Nerede | **Pearson VUE** test merkezi veya **çevrimiçi gözetimli** (online proctored, evden/ofisten) |
| Geçerlilik | Fundamentals sertifikalarının **süresi dolmaz** (yenileme gerekmez) |

---

## 2. Neden?

- **Bilmek ile sınavda göstermek farklıdır.** Konuları tek tek çalışırken her soru bağlamıyla gelir; gerçek sınavda C1.12'den bir soru, C1.4'ten bir soruyla yan yana gelir ve benzer şıklar (Policy/RBAC/lock, zone/pair, Advisor/Service Health) birbirine karışır. Karışık bir deneme bunu ortaya çıkarır.
- **Zayıf alanı ölçmek:** Alan bazlı puan, "hangi C1.x'i tekrar edeyim?" sorusunu veriyle cevaplar. Ağırlığı en büyük alan (%35-40, mimari ve servisler) genellikle en çok yanlışın çıktığı alandır.
- **Format alışkanlığı:** Evet/hayır tabloları ve "NOT/EXCEPT" soruları, bilgiden çok **okuma disiplini** ister; denemede yakalanan dikkat hatası sınavda tekrarlanmaz.

---

## 3. Nasıl?

### 3.1 Deneme sınavını kullanma (önerilen akış)

1. **Hazırlık:** C1.1–C1.14 konularının quiz'lerinde ≥ %80 al. Eksik konu varken denemeye girme; deneme bir ölçüm aracıdır, ders değil.
2. **Zamanlı ve kapalı kitap çöz:** 50 soru için **45 dakika** ayır (soru başına ~50 saniye). Not alma; kodu ve README'leri açma. Cevaplarını bir kâğıda/dosyaya yaz, `<details>` bloklarını açma.
3. **Puanla:** Sonda cevap anahtarı yok — her sorunun cevabı kendi `<details>` bloğundadır; tek tek kontrol et ve **alan bazlı puan tablosunu** doldur.
4. **Yanlışları sınıflandır:** Her yanlış için (a) hangi C1.x, (b) **bilgi eksiği mi, dikkat hatası mı** ("NOT"u kaçırdım, "otomatik"i görmedim). Bilgi eksiği → konu README'si ve quiz'i; dikkat hatası → 3.2'deki teknikler.
5. **Tekrar et ve resmî testi çöz:** Zayıf konuları tekrar ettikten sonra Microsoft Learn'deki **ücretsiz resmî Practice Assessment**'ı kapalı kitap çöz. Hem bu denemede hem resmî testte istikrarlı biçimde **%80+** alıyorsan sınava hazırsın.
6. Bir hafta sonra denemeyi **sırayı değiştirerek** tekrar çöz; ezber değil anlama ölçülsün diye gerekçeleri kendi cümlelerinle açıkla.

### 3.2 Soru çözme teknikleri

| Teknik | Nasıl? | Örnek |
|---|---|---|
| **Önce soru kökünü oku** | Senaryonun son cümlesi asıl soruyu taşır: "en düşük maliyet", "en az yönetim yükü", "en yüksek erişilebilirlik" | "…**en az yönetim yükü** ile web uygulaması" → PaaS (App Service) |
| **NOT / EXCEPT soruları** | Soruyu olumluya çevir, her şıkkı "bu doğru mu?" diye işaretle; tek "yanlış" olanı seç | "Aşağıdakilerden hangisi resource group için **doğru değildir**?" |
| **Eleme yöntemi** | Kesin yanlışları at (kategori dışı, farklı katman, mutlak ifade); kalan iki şık arasında anahtar kelimeye bak | Zone ↔ pair: "datacenter arızası" mı "bölgesel felaket" mi? |
| **Anahtar kelime avı** | Kelime → kavram eşlemesi (aşağıdaki tablo) | "internetten geçmeden" → ExpressRoute |
| **Evet/hayır tablosu** | Her satır **bağımsızdır**; bir satırın cevabı diğerini etkilemez. "Hepsi evet olamaz" gibi bir kural yoktur | — |
| **Mutlak ifadeler** | "Her zaman", "hiçbir zaman", "yalnızca", "tüm" — ifade tek bir istisnayla çöker mi? | "SaaS'ta **tüm** sorumluluk Microsoft'tadır" → Hayır |
| **"En iyi" cevap** | Birden çok şık teknik olarak çalışabilir; senaryonun kısıtını (maliyet, yönetim, zaman) en iyi karşılayanı seç | VM de web uygulaması çalıştırır ama "OS yönetmeden" denmişse App Service |
| **Çoklu seçim** | Kaç cevap istendiğini oku ("iki doğru cevap"); kısmi puan olabilir ama varsayma | — |
| **Tahmin** | Boş bırakılan soru puan getirmez; yanlış cevap için ek ceza yoktur — her soruyu cevapla | — |
| **İşaretle ve geç** | 60 saniyeyi aşan soruyu işaretle (*mark for review*), sona bırak | — |

**Anahtar kelime → cevap tablosu** (tüm konulardan):

| Kelime/ifade | Büyük olasılıkla | Konu |
|---|---|---|
| "her zaman müşteride" | veri, cihazlar, hesaplar/kimlikler | C1.1 |
| "ön yatırım yok", "kullandıkça öde" | OpEx / consumption-based | C1.1 |
| "otomatik olarak büyü/küçül" | elasticity / autoscale | C1.2 |
| "kesintisiz devam etti" ↔ "felaketten toparlandı" | high availability ↔ reliability | C1.2 |
| "OS'a tam erişim", "lift-and-shift" | IaaS (VM) | C1.3, C1.5 |
| "yalnızca kod çalışırken ödeme", "olay tetiklemeli" | serverless (Functions) | C1.3, C1.6 |
| "datacenter arızası" ↔ "bölgesel felaket" | availability zone ↔ region pair | C1.4 |
| "politikayı tüm subscription'lara" | management group | C1.4, C1.12 |
| "update/fault domain" | availability set | C1.5 |
| "tek container, orkestrasyon yok" | Container Instances | C1.6 |
| "uzaktan Windows masaüstü" | Azure Virtual Desktop | C1.6 |
| "internetten geçmeden, özel bağlantı" | ExpressRoute | C1.7 |
| "iki VNet'i bağla" | VNet peering | C1.7 |
| "bölgesel felakette de okunabilir" | RA-GRS / RA-GZRS | C1.8 |
| "terabaytlarca veri, ağ yetersiz" | Data Box | C1.8 |
| "klasik domain join, LDAP, Kerberos" | Entra Domain Services | C1.9 |
| "koşula göre MFA iste" | Conditional Access | C1.9 |
| "asla güvenme, her zaman doğrula" | Zero Trust | C1.10 |
| "henüz kurulmamış iş yükünün maliyeti" | Pricing Calculator | C1.11 |
| "maliyeti departmana göre raporla" | tag | C1.11 |
| "izin verilmeyen bölgede kaynak oluşturmayı engelle" | Azure Policy (Deny) | C1.12 |
| "kazara silinmeyi önle" | resource lock (CanNotDelete) | C1.12 |
| "aynı ortamı tekrar tekrar tutarlı kur" | ARM template / Bicep (IaC) | C1.13 |
| "başka buluttaki sunucuyu Azure'dan yönet" | Azure Arc | C1.13 |
| "kişiselleştirilmiş öneri" | Azure Advisor | C1.14 |
| "benim bölgemi etkileyen Azure olayı" | Service Health | C1.14 |

### 3.3 Zaman yönetimi

- Sınav başlamadan önce gösterilen **soru sayısı ve süreyi** not et; soru başına düşen süreyi hesapla (≈ süre / soru sayısı).
- İlk geçişte bildiklerini hızla cevapla, emin olmadıklarını **işaretle**; ikinci geçişe en az %15 süre bırak.
- Uzun senaryolu sorularda önce son cümleyi (asıl soruyu), sonra senaryoyu oku.
- Bazı soru grupları (ör. aynı senaryoya bağlı ardışık sorular) cevaplandıktan sonra **geri dönülemeyebilir**; sınav ekranındaki uyarıyı oku.

### 3.4 Sınav günü

| | Test merkezi (Pearson VUE) | Çevrimiçi gözetimli (online proctored) |
|---|---|---|
| Ortam | Hazır bilgisayar, gözetmen yerinde | Kendi bilgisayarın; webcam ve mikrofon zorunlu |
| Hazırlık | Kimlik belgesi, randevudan önce varış | **Sistem testi** (önceden), sessiz ve boş bir oda, masada hiçbir şey olmaması, check-in fotoğrafları |
| Risk | Ulaşım | İnternet kopması, ortam kurallarının ihlali (birinin odaya girmesi, ekrandan uzaklaşmak) |
| Öneri | İlk sınavında stres azaltır | Esnek saat; ama check-in için 15-30 dakika erken başla |

- Sınav arayüzünü önceden görmek için Microsoft'un **exam sandbox** (sınav deneyimi demosu) sayfasını dene.
- Kimlikteki ad, sınav profilindeki adla **aynı** olmalıdır.
- Sonuç genellikle sınav bitiminde ekranda görünür; başarısızlıkta alan bazlı performans raporu verilir — o rapor, bu repodaki C1.x tekrar planına doğrudan eşlenir. Yeniden girme bekleme süreleri için resmî **retake policy**'ye bak.
- Microsoft zaman zaman ücretsiz eğitim etkinlikleri (ör. *Virtual Training Days*) ve öğrenci programlarıyla sınav indirimi/ücretsiz hak sunar; güncel durumu resmî sayfalardan kontrol et.

---

## 4. Gerçek dünya senaryosu

Bir geliştirici AZ-900'e beş haftalık plana göre hazırlanıyor (→ C1 modül süresi):

- **Hafta 1-4:** Her hafta 3-4 konu; her konunun sonunda `quiz.md`'yi çözüp ≥ %80 alana kadar tekrar. Laboratuvarları kendi aboneliğinde, bütçe alarmı kurulu olarak yapıyor ve her laboratuvarı `az group delete` ile kapatıyor.
- **Hafta 5, gün 1:** Bu deneme sınavı zamanlı: **38/50**. Alan tablosu: cloud concepts 13/14, mimari ve servisler 12/19, yönetim ve yönetişim 13/17. Yanlışların 5'i C1.7 ve C1.8'den (ağ, storage redundancy), 3'ü C1.12'den (Policy/RBAC/lock).
- **Gün 2-3:** C1.7, C1.8, C1.12 README'lerini ve quiz'lerini tekrar; anahtar kelime tablosunu kendi el yazısıyla yeniden yazıyor.
- **Gün 4:** Resmî Practice Assessment: %84. Deneme sınavını farklı sırayla tekrar: 46/50.
- **Gün 6:** Çevrimiçi gözetimli sınav; önceki gün sistem testini yapmış, odayı hazırlamış.

---

## 5. Sınav perspektifi

**Microsoft'un sevdiği kalıplar ve tuzaklar** (tüm modülden en sık karışanlar):

| Karışan çift | Ayırt eden soru |
|---|---|
| Availability zone ↔ region pair | Arıza datacenter düzeyinde mi, bölge düzeyinde mi? |
| Availability set ↔ zone ↔ scale set | Datacenter içi donanım/bakım mı, datacenter arızası mı, otomatik ölçek mi? |
| Scalability ↔ elasticity | "Otomatik" var mı? |
| HA ↔ reliability | "Devam etti" mi, "toparlandı" mı? |
| VPN Gateway ↔ ExpressRoute | İnternet üzerinden şifreli mi, özel hat mı? |
| LRS ↔ ZRS ↔ GRS ↔ GZRS ↔ RA-* | Hangi arızaya dayanmalı, ikincilden okuma gerekli mi? |
| Entra ID ↔ Entra Domain Services | Modern kimlik mi, klasik AD özellikleri (domain join, LDAP, Kerberos) mi? |
| B2B ↔ B2C (External ID) | İş ortağı mı, müşteri mi? |
| RBAC ↔ Policy ↔ lock | Kim ne yapabilir mi, kaynak nasıl olmalı mı, silinmesin/değişmesin mi? |
| Pricing Calculator ↔ TCO | Yeni iş yükünün tahmini mi, on-prem ile karşılaştırma mı? |
| Advisor ↔ Service Health ↔ Monitor | Öneri mi, Azure olayı mı, benim telemetrim mi? |
| Defender for Cloud ↔ Advisor | Güvenlik duruşu ve tehdit koruması mı, beş kategoride genel öneri mi? |

**Sınavda olmayanlar:** komut sözdizimi ezberi, fiyat rakamları, hesaplama ağırlıklı sorular (SLA hesabı nadiren ve basit), portal ekranlarının yeri. Laboratuvar (hands-on lab) bölümü fundamentals sınavlarında genellikle yoktur.

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Braindump/"çıkmış soru" ezberlemek | Microsoft'un sınav sözleşmesine aykırıdır, sertifika iptaline yol açabilir; sorular değişir — kavramı öğren |
| Yalnızca deneme sınavı çözerek çalışmak | Önce konu (README + laboratuvar), sonra quiz, en son deneme |
| Evet/hayır satırlarını birbirine bağlamak | Her satır bağımsız bir doğru/yanlış sorusudur |
| "NOT"u kaçırmak | Soru kökündeki olumsuzluğu altını çizerek (zihnen) oku |
| Bir soruda 3 dakika takılmak | İşaretle, geç, sona dön |
| Online sınavda ortam kurallarını hafife almak | Masada telefon/kâğıt, odaya giren biri veya ekrandan uzun süre uzaklaşmak sınavın iptaline yol açabilir |
| Müfredat tarihini kontrol etmemek | Study guide'daki "skills measured as of" tarihini ve değişiklik kaydını sınavdan önce kontrol et |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Erken sınav tarihi almak | Motivasyon ve odak | Hazır değilsen tekrar ücreti ve bekleme süresi |
| Test merkezi | Teknik risk yok, sakin ortam | Ulaşım, sınırlı saat |
| Online proctored | Esnek saat ve yer | Bağlantı/ortam riski, sıkı kurallar |
| Çok sayıda deneme sınavı | Format rahatlığı | Ezber riski: aynı soruları tanımaya başlarsın; gerekçeyi açıklayamıyorsan bilmiyorsundur |
| Laboratuvarları atlamak | Zaman kazancı | Kavramlar soyut kalır; senaryo sorularında "hangisi gerçekten böyle çalışır?" sezgisi zayıflar |

---

## 8. Kaynaklar

- [Microsoft Certified: Azure Fundamentals (AZ-900)](https://learn.microsoft.com/en-us/credentials/certifications/azure-fundamentals/) — sınav planlama, Practice Assessment, randevu
- [AZ-900 study guide (skills measured + değişiklik kaydı)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Azure Fundamentals learning path'leri](https://learn.microsoft.com/en-us/training/courses/az-900t00)
- [Exam duration and question types](https://learn.microsoft.com/en-us/credentials/support/exam-duration-exam-experience) · [Exam sandbox](https://aka.ms/examdemo)
- [Online exams with Pearson VUE](https://learn.microsoft.com/en-us/credentials/certifications/online-exams) · [Retake policy](https://learn.microsoft.com/en-us/credentials/support/retake-policy)
- [Credentials support (sınav politikaları, gizlilik sözleşmesi, sınav bütünlüğü)](https://learn.microsoft.com/en-us/credentials/support/)
