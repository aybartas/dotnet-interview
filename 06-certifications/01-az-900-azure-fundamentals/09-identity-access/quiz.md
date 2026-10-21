# C1.9 Kimlik ve Erişim: Microsoft Entra ID, Kimlik Doğrulama, RBAC — Quiz

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure architecture and services* (%35-40) · 22 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, eşleştirme, senaryo). Gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 18/22**.

---

### 1. Tek seçim
Microsoft'un bulut tabanlı kimlik ve erişim yönetimi servisi hangisidir?

- A) Microsoft Entra Domain Services
- B) Microsoft Entra ID
- C) Azure Policy
- D) Microsoft Defender for Cloud

<details><summary>Cevap</summary>

**B.** Entra ID (eski adı Azure AD), kullanıcı, grup, uygulama ve cihaz kimliklerini yöneten bulut kimlik servisidir. Domain Services klasik AD özelliklerini yönetilen olarak sunar; Policy kaynak kurallarıdır; Defender güvenlik duruşudur.
</details>

### 2. Senaryo
Bir şirket, **LDAP ve Kerberos** kullanan ve sunucularının **domain'e katılmasını** gerektiren eski bir uygulamayı Azure VM'lerine taşıyacak. Şirket domain controller kurmak, yamalamak ve yedeklemek istemiyor. Hangi servisi kullanmalıdır?

- A) Microsoft Entra ID
- B) Microsoft Entra Domain Services
- C) Microsoft Entra External ID
- D) Azure RBAC

<details><summary>Cevap</summary>

**B.** Entra Domain Services; domain join, Group Policy, LDAP ve Kerberos/NTLM'i domain controller yönetimi olmadan, **yönetilen** olarak sunar. Entra ID bu klasik protokolleri sunmaz.
</details>

### 3. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Microsoft Entra ID, OAuth 2.0, OpenID Connect ve SAML gibi modern protokolleri kullanır. | ☐ | ☐ |
| b) Microsoft Entra ID, sanal makinelerin geleneksel domain join işlemini doğrudan sağlar. | ☐ | ☐ |
| c) Kurum içi Active Directory kullanıcıları Microsoft Entra ID'ye senkronize edilebilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Geleneksel domain join (Kerberos/LDAP/GPO) Entra **Domain Services**'tır. Senkronizasyon Entra Connect veya Cloud Sync ile yapılır.
</details>

### 4. Tek seçim
Kurum içindeki Active Directory kullanıcı hesaplarını Microsoft Entra ID ile senkronize etmek için hangi araç kullanılır?

- A) Azure Migrate
- B) Microsoft Entra Connect
- C) Azure Arc
- D) Microsoft Entra Domain Services

<details><summary>Cevap</summary>

**B.** Entra Connect (Sync) ve daha hafif Entra Cloud Sync, on-prem AD → Entra ID senkronizasyonunu yapar. Domain Services senkronizasyonun yönü tersidir (Entra ID'den yönetilen domain'e).
</details>

### 5. Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Microsoft Entra ID'nin faydalarındandır?

- A) Self-service password reset
- B) Sanal makine yedekleme
- C) SaaS uygulamalarına single sign-on
- D) Storage account redundancy

<details><summary>Cevap</summary>

**A ve C.** Entra ID'nin faydaları: authentication (SSPR, MFA, smart lockout), SSO, uygulama ve cihaz yönetimi, identity protection, Conditional Access. B ve D kimlik özelliği değildir.
</details>

### 6. Eşleştirme
Her ifadeyi **Authentication** veya **Authorization** ile eşleştirin.

1. Kullanıcının iddia ettiği kişi olduğunu kanıtlaması
2. Kullanıcının bir resource group'ta Reader olması
3. Şifre ve Authenticator bildirimiyle giriş
4. Kullanıcının bir VM'i silip silemeyeceğine karar verilmesi

<details><summary>Cevap</summary>

**1 → Authentication · 2 → Authorization · 3 → Authentication · 4 → Authorization.** AuthN "sen kimsin?", AuthZ "ne yapabilirsin?" sorusunu cevaplar. AuthN önce gelir.
</details>

### 7. Tek seçim
Kullanıcıların **bir kez** giriş yaparak Microsoft 365, Salesforce ve şirket içi web uygulamalarına tekrar şifre girmeden erişmesini sağlayan özellik hangisidir?

- A) Multi-factor authentication
- B) Conditional Access
- C) Single sign-on
- D) Passwordless authentication

<details><summary>Cevap</summary>

**C.** SSO, tek kimlikle birçok uygulamaya tek giriştir. MFA kimliğin gücünü artırır, girişlerin sayısını azaltmaz.
</details>

### 8. Tek seçim
Aşağıdakilerden hangisi **multi-factor authentication** örneğidir?

- A) Kullanıcının şifresini ve ardından güvenlik sorusunun cevabını girmesi
- B) Kullanıcının şifresini girmesi ve telefonundaki Microsoft Authenticator bildirimini onaylaması
- C) Kullanıcının iki farklı şifre girmesi
- D) Kullanıcının tek bir uzun parola kullanması

<details><summary>Cevap</summary>

**B.** MFA **farklı türde** faktör ister: şifre (bildiğin) + telefon (sahip olduğun). A ve C'deki iki faktör de "bildiğin" türündendir; bu yüzden MFA değildir.
</details>

### 9. Eşleştirme
Her kimlik doğrulama faktörünü türüyle eşleştirin: **Bildiğin · Sahip olduğun · Olduğun**

1. Parmak izi
2. PIN
3. FIDO2 güvenlik anahtarı
4. Yüz tanıma
5. Şifre

<details><summary>Cevap</summary>

**1 → Olduğun · 2 → Bildiğin · 3 → Sahip olduğun · 4 → Olduğun · 5 → Bildiğin.**
</details>

### 10. Çoklu seçim (üç doğru cevap)
Microsoft'un passwordless kimlik doğrulama seçenekleri hangileridir?

- A) Windows Hello for Business
- B) SMS ile tek kullanımlık kod + şifre
- C) Microsoft Authenticator (phone sign-in / passkey)
- D) FIDO2 güvenlik anahtarları
- E) Güvenlik soruları

<details><summary>Cevap</summary>

**A, C ve D.** B şifre kullanır (passwordless değildir, klasik MFA'dır); E zayıf bir "bildiğin" faktörüdür.
</details>

### 11. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Passwordless kimlik doğrulama, şifreye göre phishing saldırılarına daha dayanıklıdır. | ☐ | ☐ |
| b) Windows Hello for Business, kullanıcının cihazına bağlı biyometri veya PIN kullanır. | ☐ | ☐ |
| c) Passwordless yöntemler, kullanıcı deneyimini zorlaştırdığı için yalnızca yöneticilere önerilir. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Passwordless hem daha güvenli hem daha kolaydır; tüm kullanıcılar için önerilir.
</details>

### 12. Senaryo
Bir şirket, dış danışmanlık firmasının çalışanlarının **kendi şirket hesaplarıyla** giriş yaparak şirketin SharePoint sitesine ve bir Azure resource group'una erişmesini istiyor. Danışmanlara yeni şifre verilmeyecek. Ne kullanılmalıdır?

- A) Microsoft Entra B2B collaboration
- B) Azure AD B2C / Entra External ID (müşteri)
- C) Microsoft Entra Domain Services
- D) Her danışmana şirket tenant'ında yeni bir kullanıcı hesabı oluşturmak

<details><summary>Cevap</summary>

**A.** B2B, iş ortaklarını kendi kimlikleriyle **misafir (guest)** olarak davet eder. Erişimleri senin tenant'ında grup ve rollerle yönetilir. B müşteriye dönük uygulamalar içindir.
</details>

### 13. Senaryo
Bir e-ticaret şirketi, mobil uygulamasının **müşterilerinin** Google veya Facebook hesaplarıyla kayıt olup giriş yapabilmesini ve giriş sayfalarının şirket markasıyla özelleştirilmesini istiyor. Hangi çözüm uygundur?

- A) Microsoft Entra B2B collaboration
- B) Müşteri kimliği için Microsoft Entra External ID (eski adıyla B2C senaryosu)
- C) Azure RBAC
- D) Microsoft Entra Connect

<details><summary>Cevap</summary>

**B.** Müşteriye dönük kimlik (CIAM): kayıt, sosyal hesaplar, markalı sayfalar. Azure AD B2C Mayıs 2025'ten beri yeni müşterilere satılmıyor; yeni projeler için Microsoft Entra External ID önerilir. Kavram sınavda "B2C" olarak geçebilir.
</details>

### 14. Tek seçim
Microsoft Entra Conditional Access'in işlevini **en iyi** hangisi tanımlar?

- A) Kaynakların hangi Azure bölgelerinde oluşturulabileceğini kısıtlar
- B) Kullanıcı, konum, cihaz ve risk gibi sinyalleri değerlendirerek erişime izin verir, MFA ister veya erişimi engeller
- C) Kaynakların kazara silinmesini engeller
- D) Kurum içi AD'yi Entra ID'ye senkronize eder

<details><summary>Cevap</summary>

**B.** Conditional Access, sinyal → karar → uygulama mantığıyla çalışan if-then politika motorudur. A Azure Policy, C resource lock, D Entra Connect'tir.
</details>

### 15. Senaryo
Bir şirket, kullanıcıların **ofis ağından** girişte yalnızca şifre kullanmasını, ama **ofis dışından** girişte ek olarak MFA yapmasını istiyor. Ne yapılandırılmalıdır?

- A) Security defaults
- B) Bir Conditional Access politikası (konum koşulu → MFA iste)
- C) Azure Policy ile *Allowed locations*
- D) Resource group üzerinde Reader rol ataması

<details><summary>Cevap</summary>

**B.** Konuma bağlı, koşullu MFA Conditional Access'tir (Entra ID P1). Security defaults koşul tanımlamaz. Azure Policy'nin *Allowed locations* kuralı kullanıcı konumunu değil, **kaynakların** bölgesini kısıtlar.
</details>

### 16. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Conditional Access, cihazın kurumsal politikalara uyumlu olup olmadığını bir sinyal olarak kullanabilir. | ☐ | ☐ |
| b) Security defaults, tüm kullanıcılara MFA kaydı yaptırır ve eski kimlik doğrulama protokollerini engeller. | ☐ | ☐ |
| c) Conditional Access, bir kullanıcının bir VM'i yeniden başlatma iznini belirler. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Kaynak üzerindeki işlem izni Azure RBAC'ın işidir; Conditional Access girişe ve uygulamaya erişime karar verir.
</details>

### 17. Tek seçim
Bir kullanıcının bir resource group'taki tüm kaynakları **görüntüleyebilmesi** ama hiçbir şeyi değiştirememesi gerekiyor. Hangi built-in rol atanmalıdır?

- A) Owner
- B) Contributor
- C) Reader
- D) User Access Administrator

<details><summary>Cevap</summary>

**C.** Reader yalnızca okuma (`*/read`) izni verir.
</details>

### 18. Tek seçim
Bir geliştirici ekibi bir resource group'ta kaynak oluşturup silebilmeli, ancak **başka kullanıcılara rol atayamamalıdır**. En az yetki ilkesine göre hangi rol uygundur?

- A) Owner
- B) Contributor
- C) Reader
- D) Global Administrator

<details><summary>Cevap</summary>

**B.** Contributor, erişim yönetimi (`Microsoft.Authorization/*/Write`, `*/Delete`) dışında her şeyi yapar. Owner erişim de yönetebilir (fazla yetki). Global Administrator bir Entra (directory) rolüdür.
</details>

### 19. Evet / Hayır

| İfade | Evet | Hayır |
|---|---|---|
| a) Bir subscription kapsamında atanan rol, o subscription'daki tüm resource group'lara ve kaynaklara miras kalır. | ☐ | ☐ |
| b) Bir rol ataması; bir güvenlik sorumlusu, bir rol tanımı ve bir kapsamdan oluşur. | ☐ | ☐ |
| c) Azure RBAC rolleri yalnızca tek tek kullanıcılara atanabilir, gruplara atanamaz. | ☐ | ☐ |

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Roller kullanıcı, grup, service principal ve managed identity'ye atanabilir. En iyi uygulama gruplara atamaktır.
</details>

### 20. Senaryo
Bir şirketin 40 subscription'ı var. Güvenlik denetim ekibinin **tüm subscription'lardaki** kaynakları görüntüleyebilmesi gerekiyor ve ileride eklenecek subscription'lara da erişimi otomatik olarak olmalı. En verimli çözüm nedir?

- A) Her subscription'da denetim ekibine ayrı ayrı Reader rolü atamak
- B) Subscription'ları bir management group altında toplayıp denetim ekibinin grubuna o management group kapsamında Reader rolü atamak
- C) Denetim ekibine tenant'ta Global Administrator rolü vermek
- D) Her resource group'a Reader rolü atamak

<details><summary>Cevap</summary>

**B.** Management group kapsamındaki atama, altındaki tüm subscription'lara (yenileri dahil) miras kalır. C hem yanlış türde (Entra rolü) hem en az yetki ilkesine aykırıdır.
</details>

### 21. Tek seçim
Bir kullanıcının subscription kapsamında **Reader**, aynı subscription'daki `rg-web` resource group'unda ise **Contributor** rolü var. Kullanıcı `rg-web` içinde ne yapabilir?

- A) Yalnızca kaynakları görüntüleyebilir; daha kısıtlayıcı rol geçerlidir
- B) Kaynak oluşturabilir, değiştirebilir ve silebilir; izinler toplanır
- C) Hiçbir şey yapamaz; çakışan roller birbirini iptal eder
- D) Rol atayabilir

<details><summary>Cevap</summary>

**B.** Azure RBAC **additive**'dir: etkin izin, atamaların toplamıdır. `rg-web`'de Contributor geçerlidir. Contributor rol atayamaz (D yanlış).
</details>

### 22. Tek seçim
Aşağıdaki gereksinimlerden hangisi Azure RBAC ile **değil**, Azure Policy ile karşılanır?

- A) Geliştiricilerin yalnızca `rg-dev` içinde kaynak oluşturabilmesi
- B) Denetçilerin kaynakları yalnızca görüntüleyebilmesi
- C) Hiç kimsenin (Owner dahil) West Europe dışındaki bir bölgede kaynak oluşturamaması
- D) Bir ekibin yalnızca rol atamalarını yönetebilmesi

<details><summary>Cevap</summary>

**C.** RBAC **kullanıcının** ne yapabileceğini, Policy **kaynağın** nasıl olabileceğini (bölge, SKU, tag) belirler. Policy'nin Deny etkisi Owner'ı da durdurur (→ C1.12). A, B ve D rol atamalarıyla çözülür.
</details>

---

**Puanlama:** 20-22 → hazırsın · 15-19 → README 3.4–3.10'daki karşılaştırma tablolarını tekrar et · < 15 → konuyu baştan çalış ve 3.11 laboratuvarını yap.
