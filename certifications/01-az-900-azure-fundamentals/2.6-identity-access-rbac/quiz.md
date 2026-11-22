[← Konu anlatımı](README.md) · [AZ-900 İçindekiler](../README.md)

### 📝 Bölüm 2.6 Quiz Soruları

> Teori: [README.md](README.md) · Sınav alanı: *Describe Azure architecture and services* (%35-40) · 25 soru
>
> Sorular **özgündür** ve sınav formatındadır (tek seçim, çoklu seçim, evet/hayır, eşleştirme, senaryo). Gerçek sınav soruları gizlilik sözleşmesi (NDA) kapsamındadır ve burada kullanılmaz. Hedef: **≥ 20/25**.

> 💡 Soruları çözdükten sonra alttaki **Cevap** butonuna tıklayarak gerekçeli çözümü inceleyebilirsiniz. Hedef: ≥ %80 başarı.

---

#### Soru 1: Tek seçim
Microsoft'un bulut tabanlı kimlik ve erişim yönetimi servisi hangisidir?

- A) Microsoft Entra Domain Services
- B) Microsoft Entra ID
- C) Azure Policy
- D) Microsoft Defender for Cloud

<details><summary>Cevap</summary>

**B.** Entra ID (eski adı Azure AD), kullanıcı, grup, uygulama ve cihaz kimliklerini yöneten bulut kimlik servisidir. Domain Services klasik AD özelliklerini yönetilen olarak sunar; Policy kaynak kurallarıdır; Defender güvenlik duruşudur.
</details>

#### Soru 2: Senaryo
Bir şirket, **LDAP ve Kerberos** kullanan ve sunucularının **domain'e katılmasını** gerektiren eski bir uygulamayı Azure VM'lerine taşıyacak. Şirket domain controller kurmak, yamalamak ve yedeklemek istemiyor. Hangi servisi kullanmalıdır?

- A) Microsoft Entra ID
- B) Microsoft Entra Domain Services
- C) Microsoft Entra External ID
- D) Azure RBAC

<details><summary>Cevap</summary>

**B.** Entra Domain Services; domain join, Group Policy, LDAP ve Kerberos/NTLM'i domain controller yönetimi olmadan, **yönetilen** olarak sunar. Entra ID bu klasik protokolleri sunmaz.
</details>

#### Soru 3: Evet / Hayır

- a) Microsoft Entra ID, OAuth 2.0, OpenID Connect ve SAML gibi modern protokolleri kullanır. — Evet: ☐ · Hayır: ☐
- b) Microsoft Entra ID, sanal makinelerin geleneksel domain join işlemini doğrudan sağlar. — Evet: ☐ · Hayır: ☐
- c) Microsoft Entra ID, Microsoft 365 ve Azure'un kullandığı bulut tabanlı kimlik servisidir. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Hayır · c) Evet.** Geleneksel domain join (Kerberos/LDAP/GPO) Entra **Domain Services**'tır. Entra ID, Microsoft'un bulut tabanlı kimlik ve erişim yönetimi servisidir.
</details>

#### Soru 4: Tek seçim
Aşağıdaki ifadelerden hangisi **Microsoft Entra ID ile kurum içi Active Directory (AD DS)** arasındaki farkı doğru anlatır?

- A) Entra ID Kerberos ve LDAP ile domain join sağlar; AD DS yalnızca bulutta çalışır
- B) Entra ID, HTTP/HTTPS tabanlı modern protokolleri (SAML, OAuth 2.0, OpenID Connect) kullanan bulut kimlik servisidir; AD DS ise Kerberos/LDAP kullanan, kendi domain controller'larınızda çalışan klasik dizin servisidir
- C) İkisi de tamamen aynı servisin farklı adlarıdır
- D) Entra ID yalnızca fiziksel sunucuları yönetir

<details><summary>Cevap</summary>

**B.** Entra ID bulutta çalışan, modern web protokolleriyle SSO/MFA sağlayan kimlik servisidir; AD DS ise kurum içindeki domain controller'larda çalışan, Kerberos/LDAP/Group Policy kullanan dizin servisidir. Klasik protokollere ihtiyaç duyan uygulamalar için bulutta **Entra Domain Services** vardır.
</details>

#### Soru 5: Çoklu seçim (iki doğru cevap)
Aşağıdakilerden **hangi ikisi** Microsoft Entra ID'nin faydalarındandır?

- A) Self-service password reset
- B) Sanal makine yedekleme
- C) SaaS uygulamalarına single sign-on
- D) Storage account redundancy

<details><summary>Cevap</summary>

**A ve C.** Entra ID'nin faydaları: authentication (self-service password reset, MFA), SSO, uygulama ve cihaz yönetimi, Conditional Access. B ve D kimlik özelliği değildir.
</details>

#### Soru 6: Eşleştirme
Her ifadeyi **Authentication** veya **Authorization** ile eşleştirin.

1. Kullanıcının iddia ettiği kişi olduğunu kanıtlaması
2. Kullanıcının bir resource group'ta Reader olması
3. Şifre ve Authenticator bildirimiyle giriş
4. Kullanıcının bir VM'i silip silemeyeceğine karar verilmesi

<details><summary>Cevap</summary>

**1 → Authentication · 2 → Authorization · 3 → Authentication · 4 → Authorization.** AuthN "sen kimsin?", AuthZ "ne yapabilirsin?" sorusunu cevaplar. AuthN önce gelir.
</details>

#### Soru 7: Tek seçim
Kullanıcıların **bir kez** giriş yaparak Microsoft 365, Salesforce ve şirket içi web uygulamalarına tekrar şifre girmeden erişmesini sağlayan özellik hangisidir?

- A) Multi-factor authentication
- B) Conditional Access
- C) Single sign-on
- D) Passwordless authentication

<details><summary>Cevap</summary>

**C.** SSO, tek kimlikle birçok uygulamaya tek giriştir. MFA kimliğin gücünü artırır, girişlerin sayısını azaltmaz.
</details>

#### Soru 8: Tek seçim
Aşağıdakilerden hangisi **multi-factor authentication** örneğidir?

- A) Kullanıcının şifresini ve ardından güvenlik sorusunun cevabını girmesi
- B) Kullanıcının şifresini girmesi ve telefonundaki Microsoft Authenticator bildirimini onaylaması
- C) Kullanıcının iki farklı şifre girmesi
- D) Kullanıcının tek bir uzun parola kullanması

<details><summary>Cevap</summary>

**B.** MFA **farklı türde** faktör ister: şifre (bildiğin) + telefon (sahip olduğun). A ve C'deki iki faktör de "bildiğin" türündendir; bu yüzden MFA değildir.
</details>

#### Soru 9: Eşleştirme
Her kimlik doğrulama faktörünü türüyle eşleştirin: **Bildiğin · Sahip olduğun · Olduğun**

1. Parmak izi
2. PIN
3. FIDO2 güvenlik anahtarı
4. Yüz tanıma
5. Şifre

<details><summary>Cevap</summary>

**1 → Olduğun · 2 → Bildiğin · 3 → Sahip olduğun · 4 → Olduğun · 5 → Bildiğin.**
</details>

#### Soru 10: Çoklu seçim (üç doğru cevap)
Microsoft'un passwordless kimlik doğrulama seçenekleri hangileridir?

- A) Windows Hello for Business
- B) SMS ile tek kullanımlık kod + şifre
- C) Microsoft Authenticator (phone sign-in / passkey)
- D) FIDO2 güvenlik anahtarları
- E) Güvenlik soruları

<details><summary>Cevap</summary>

**A, C ve D.** B şifre kullanır (passwordless değildir, klasik MFA'dır); E zayıf bir "bildiğin" faktörüdür.
</details>

#### Soru 11: Evet / Hayır

- a) Passwordless kimlik doğrulama, şifreye göre phishing saldırılarına daha dayanıklıdır. — Evet: ☐ · Hayır: ☐
- b) Windows Hello for Business, kullanıcının cihazına bağlı biyometri veya PIN kullanır. — Evet: ☐ · Hayır: ☐
- c) Passwordless yöntemler, kullanıcı deneyimini zorlaştırdığı için yalnızca yöneticilere önerilir. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Passwordless hem daha güvenli hem daha kolaydır; tüm kullanıcılar için önerilir.
</details>

#### Soru 12: Senaryo
Bir şirket, dış danışmanlık firmasının çalışanlarının **kendi şirket hesaplarıyla** giriş yaparak şirketin SharePoint sitesine ve bir Azure resource group'una erişmesini istiyor. Danışmanlara yeni şifre verilmeyecek. Ne kullanılmalıdır?

- A) Microsoft Entra External ID — B2B collaboration (guest kullanıcı daveti)
- B) Azure Policy
- C) Microsoft Entra Domain Services
- D) Her danışmana şirket tenant'ında yeni bir kullanıcı hesabı oluşturmak

<details><summary>Cevap</summary>

**A.** B2B collaboration, iş ortaklarını kendi kimlikleriyle **misafir (guest)** olarak davet eder. Erişimleri senin tenant'ında grup ve rollerle yönetilir; ayrı şifre vermen gerekmez. Azure Policy kaynak kurallarıdır, Domain Services klasik AD protokolleridir.
</details>

#### Soru 13: Tek seçim
Microsoft Entra External ID kapsamındaki **guest user (misafir kullanıcı)** için aşağıdakilerden hangisi doğrudur?

- A) Misafir kullanıcı, kendi kimliğiyle (kendi kurumunun hesabı, Microsoft hesabı vb.) giriş yapar ve davet eden tenant'ta grup/rol atamalarıyla yönetilir
- B) Misafir kullanıcıya her zaman davet eden kurumun yeni bir şifresi verilmelidir
- C) Misafir kullanıcılar Azure RBAC rolü alamaz
- D) Misafir kullanıcı yalnızca fiziksel datacenter'lara erişebilir

<details><summary>Cevap</summary>

**A.** B2B'de dış kullanıcı **kendi kimliğiyle** gelir; davet eden tenant'ta diğer kullanıcılar gibi gruplara ve rollere (ör. Reader) atanabilir ve Conditional Access/MFA ile sınırlanabilir.
</details>

#### Soru 14: Tek seçim
Microsoft Entra Conditional Access'in işlevini **en iyi** hangisi tanımlar?

- A) Kaynakların hangi Azure bölgelerinde oluşturulabileceğini kısıtlar
- B) Kullanıcı, konum, cihaz ve risk gibi sinyalleri değerlendirerek erişime izin verir, MFA ister veya erişimi engeller
- C) Kaynakların kazara silinmesini engeller
- D) Kullanıcıların şifrelerini Azure Storage'a yedekler

<details><summary>Cevap</summary>

**B.** Conditional Access, sinyal → karar → uygulama mantığıyla çalışan if-then politika motorudur. A Azure Policy, C resource lock, D ise bir senkronizasyon aracıdır.
</details>

#### Soru 15: Senaryo
Bir şirket, kullanıcıların **ofis ağından** girişte yalnızca şifre kullanmasını, ama **ofis dışından** girişte ek olarak MFA yapmasını istiyor. Ne yapılandırılmalıdır?

- A) Her kullanıcıya Owner rolü vermek
- B) Bir Conditional Access politikası (konum koşulu → MFA iste)
- C) Azure Policy ile *Allowed locations*
- D) Resource group üzerinde Reader rol ataması

<details><summary>Cevap</summary>

**B.** Konuma bağlı, koşullu MFA Conditional Access'tir. Azure Policy'nin *Allowed locations* kuralı kullanıcı konumunu değil, **kaynakların** bölgesini kısıtlar.
</details>

#### Soru 16: Evet / Hayır

- a) Conditional Access, cihazın kurumsal politikalara uyumlu olup olmadığını bir sinyal olarak kullanabilir. — Evet: ☐ · Hayır: ☐
- b) Conditional Access, konuma göre MFA isteyebilir veya erişimi engelleyebilir. — Evet: ☐ · Hayır: ☐
- c) Conditional Access, bir kullanıcının bir VM'i yeniden başlatma iznini belirler. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Kaynak üzerindeki işlem izni Azure RBAC'ın işidir; Conditional Access girişe ve uygulamaya erişime karar verir.
</details>

#### Soru 17: Tek seçim
Bir kullanıcının bir resource group'taki tüm kaynakları **görüntüleyebilmesi** ama hiçbir şeyi değiştirememesi gerekiyor. Hangi built-in rol atanmalıdır?

- A) Owner
- B) Contributor
- C) Reader
- D) Bir Entra ID Global Administrator rolü

<details><summary>Cevap</summary>

**C.** Reader yalnızca okuma (`*/read`) izni verir.
</details>

#### Soru 18: Tek seçim
Bir geliştirici ekibi bir resource group'ta kaynak oluşturup silebilmeli, ancak **başka kullanıcılara rol atayamamalıdır**. En az yetki ilkesine göre hangi rol uygundur?

- A) Owner
- B) Contributor
- C) Reader
- D) Global Administrator

<details><summary>Cevap</summary>

**B.** Contributor, erişim yönetimi (`Microsoft.Authorization/*/Write`, `*/Delete`) dışında her şeyi yapar. Owner erişim de yönetebilir (fazla yetki). Global Administrator bir Entra (directory) rolüdür.
</details>

#### Soru 19: Evet / Hayır

- a) Bir subscription kapsamında atanan rol, o subscription'daki tüm resource group'lara ve kaynaklara miras kalır. — Evet: ☐ · Hayır: ☐
- b) Bir rol ataması; bir güvenlik sorumlusu, bir rol tanımı ve bir kapsamdan oluşur. — Evet: ☐ · Hayır: ☐
- c) Azure RBAC rolleri yalnızca tek tek kullanıcılara atanabilir, gruplara atanamaz. — Evet: ☐ · Hayır: ☐

<details><summary>Cevap</summary>

**a) Evet · b) Evet · c) Hayır.** Roller kullanıcı, grup, service principal ve managed identity'ye atanabilir. En iyi uygulama gruplara atamaktır.
</details>

#### Soru 20: Senaryo
Bir şirketin 40 subscription'ı var. Güvenlik denetim ekibinin **tüm subscription'lardaki** kaynakları görüntüleyebilmesi gerekiyor ve ileride eklenecek subscription'lara da erişimi otomatik olarak olmalı. En verimli çözüm nedir?

- A) Her subscription'da denetim ekibine ayrı ayrı Reader rolü atamak
- B) Subscription'ları bir management group altında toplayıp denetim ekibinin grubuna o management group kapsamında Reader rolü atamak
- C) Denetim ekibine tenant'ta Global Administrator rolü vermek
- D) Her resource group'a Reader rolü atamak

<details><summary>Cevap</summary>

**B.** Management group kapsamındaki atama, altındaki tüm subscription'lara (yenileri dahil) miras kalır. C hem yanlış türde (Entra rolü) hem en az yetki ilkesine aykırıdır.
</details>

#### Soru 21: Tek seçim
Bir kullanıcının subscription kapsamında **Reader**, aynı subscription'daki `rg-web` resource group'unda ise **Contributor** rolü var. Kullanıcı `rg-web` içinde ne yapabilir?

- A) Yalnızca kaynakları görüntüleyebilir; daha kısıtlayıcı rol geçerlidir
- B) Kaynak oluşturabilir, değiştirebilir ve silebilir; izinler toplanır
- C) Hiçbir şey yapamaz; çakışan roller birbirini iptal eder
- D) Rol atayabilir

<details><summary>Cevap</summary>

**B.** Azure RBAC **additive**'dir: etkin izin, atamaların toplamıdır. `rg-web`'de Contributor geçerlidir. Contributor rol atayamaz (D yanlış).
</details>

#### Soru 22: Tek seçim
Aşağıdaki gereksinimlerden hangisi Azure RBAC ile **değil**, Azure Policy ile karşılanır?

- A) Geliştiricilerin yalnızca `rg-dev` içinde kaynak oluşturabilmesi
- B) Denetçilerin kaynakları yalnızca görüntüleyebilmesi
- C) Hiç kimsenin (Owner dahil) West Europe dışındaki bir bölgede kaynak oluşturamaması
- D) Bir ekibin yalnızca rol atamalarını yönetebilmesi

<details><summary>Cevap</summary>

**C.** RBAC **kullanıcının** ne yapabileceğini, Policy **kaynağın** nasıl olabileceğini (bölge, SKU, tag) belirler. Policy'nin Deny etkisi Owner'ı da durdurur (→ C1.12). A, B ve D rol atamalarıyla çözülür.
</details>

#### Soru 23: Sıralama
Aşağıdaki kimlik doğrulama yaklaşımlarını **güvenlik düzeyi en düşükten en yükseğe** doğru sıralayın: Passwordless (FIDO2 anahtarı / Windows Hello) · Yalnızca parola · Parola + MFA

<details><summary>Cevap</summary>

**Yalnızca parola → Parola + MFA → Passwordless.** Parola tek başına tahmin edilir, tekrar kullanılır ve phishing ile çalınır; MFA ikinci bir faktör ekler; passwordless parolayı tamamen kaldırır ve phishing'e dayanıklı, zaten çok faktörlü yöntemler kullanır.
</details>

#### Soru 24: Tek seçim
Bir şirket, kullanıcının oturumu **riskli** görüldüğünde (ör. sızdırılmış kimlik bilgisi şüphesi, olağandışı konumdan giriş) ek doğrulama istemek, risk çok yüksekse erişimi **engellemek** istiyor. Hangi özellik kullanılır?

- A) Azure Policy
- B) Risk sinyalini kullanan bir Microsoft Entra Conditional Access politikası
- C) Resource lock
- D) Azure RBAC'ta Reader rolü

<details><summary>Cevap</summary>

**B.** Conditional Access; kullanıcı/grup, konum, cihaz, uygulama ve **risk** sinyallerine göre erişime izin verir, **MFA ister** veya **engeller**. Policy kaynak kurallarıdır, lock silme korumasıdır, RBAC kaynak işlemlerini yetkilendirir.
</details>

#### Soru 25: Eşleştirme
Bir rol atamasının her parçasını örneğiyle eşleştirin: **Security principal · Role definition · Scope**

1. `rg-web-prod` resource group'u
2. "Denetçiler" Entra ID grubu
3. *Reader* rolünün izin listesi (yalnızca okuma)

<details><summary>Cevap</summary>

**1 → Scope** (NEREDE) · **2 → Security principal** (KİM — kullanıcı, grup, service principal veya managed identity) · **3 → Role definition** (NE — izinlerin listesi). Üçü birlikte bir **role assignment** oluşturur.
</details>

---

**Puanlama:** 23-25 → hazırsın · 18-22 → README 3.4–3.10'daki karşılaştırma tablolarını tekrar et · < 18 → konuyu baştan çalış ve 3.11 laboratuvarını yap.

---

➡️ **Sonraki konu:** [2.7 Güvenlik: Zero Trust, Defense in Depth, Defender for Cloud](../2.7-security-zero-trust-defender/README.md) · [AZ-900 İçindekiler](../README.md)
