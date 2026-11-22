[← AZ-900 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../2.5-storage/README.md)

## 2.6 Kimlik ve Erişim: Entra ID, MFA, Conditional Access, RBAC

> **Sınav Alanı:** Describe Azure architecture and services (%35-40) · **Konu Kodu:** C1.9
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe Azure identity, access, and security* — directory servisleri (**Microsoft Entra ID**, **Microsoft Entra Domain Services**) · kimlik doğrulama yöntemleri (**SSO, MFA, passwordless**) · **external identities** (Microsoft Entra External ID, B2B collaboration, guest users) · **Conditional Access** · **Azure role-based access control (RBAC)**. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

### 1. Ne?

Bulutta kaynaklar herhangi bir yerden, herhangi bir cihazdan erişilebilir olduğu için güvenliğin sınırı artık şirket ağının duvarı değil, **kimliktir** ("identity is the new perimeter"). Bu konu iki soruyu cevaplar:

- **Sen kimsin?** (authentication, AuthN)
  - Kavram: Kimlik doğrulama: şifre, **MFA**, **passwordless**, **SSO**
  - Azure aracı: **Microsoft Entra ID**, Conditional Access
- **Ne yapabilirsin?** (authorization, AuthZ)
  - Kavram: Yetkilendirme: hangi kaynakta hangi işlem
  - Azure aracı: **Azure RBAC** (rol atamaları)

Çevresindeki parçalar:

- **Directory servisleri:** Microsoft Entra ID (bulut tabanlı kimlik) ve Microsoft Entra Domain Services (yönetilen klasik AD özellikleri).
- **External identities:** kuruluş dışındaki kişiler (iş ortakları, tedarikçiler — **B2B collaboration**) için **Microsoft Entra External ID** ile **guest (misafir)** kimlikler.
- **Conditional Access:** "kim, nereden, hangi cihazla?" sinyallerine göre erişime karar veren politika motoru.

Bu konunun kodu yok; pratik, `az` CLI laboratuvarıdır (3.11).

---

### 2. Neden?

- **Sınav:** Kimlik/erişim/güvenlik maddeleri "Describe Azure architecture and services" alanının (%35-40) büyük parçasıdır; Entra ID ↔ Domain Services, AuthN ↔ AuthZ, Owner ↔ Contributor ↔ Reader eşleştirmeleri neredeyse her sınavda çıkar.
- **Gerçek işte:** Gerçek ihlallerin çoğu çalınmış veya zayıf kimlik bilgisiyle başlar. Microsoft'un kendi açıklamalarına göre MFA, hesap ele geçirme saldırılarının çok büyük kısmını durdurur; MFA bu yüzden en temel kimlik koruması sayılır (3.6).
- **Shared responsibility (→ C1.1):** Hesaplar ve kimlikler **her modelde müşterinin** sorumluluğundadır. Entra ID altyapıyı sağlar; kimin hangi role sahip olduğu, MFA'nın açık olup olmadığı senin kararındır.

---

### 3. Nasıl?

#### 3.1 Microsoft Entra ID (eski adı Azure Active Directory)

**Microsoft Entra ID**, Microsoft'un **bulut tabanlı kimlik ve erişim yönetimi** servisidir. Kullanıcıların, grupların, uygulamaların ve cihazların kimliklerini tutar; Microsoft 365, Azure portal, Dynamics 365 ve binlerce SaaS/özel uygulamaya girişi yönetir. (Azure AD, 2023'te **Microsoft Entra ID** adını aldı; sınav ve dokümanlar yeni adı kullanır.)

- **Tenant:** bir kuruluşun Entra ID örneği (ör. `contoso.onmicrosoft.com`). Her Azure subscription **tek bir tenant'a** güvenir; bir tenant'a birden çok subscription bağlı olabilir (→ C1.4 hiyerarşisinin en üstü).
- **Kimler kullanır?**
  - **BT yöneticileri:** kullanıcı/grup yönetimi, erişim politikaları, MFA.
  - **Geliştiriciler:** uygulamalarına SSO ve kimlik doğrulama eklemek.
  - **Kullanıcılar:** tek hesapla giriş, **self-service password reset** (SSPR).
  - **Online servis aboneleri:** Microsoft 365, Azure ve Dynamics 365 zaten Entra ID kullanır.

**On-prem Active Directory (AD DS) ile farkı:**

- **Nerede**
  - Active Directory Domain Services (on-prem): Kendi domain controller sunucuların
  - Microsoft Entra ID: Microsoft'un yönettiği küresel bulut servisi
- **Protokoller**
  - Active Directory Domain Services (on-prem): **Kerberos, NTLM, LDAP**, Group Policy (GPO)
  - Microsoft Entra ID: **HTTP/HTTPS tabanlı modern protokoller** (ör. SAML, OAuth 2.0, OpenID Connect)
- **Yapı**
  - Active Directory Domain Services (on-prem): Forest, domain, OU, domain join
  - Microsoft Entra ID: Düz yapı: tenant, kullanıcı, grup, administrative unit
- **Tipik kullanım**
  - Active Directory Domain Services (on-prem): Kurum içi Windows sunucu/istemci yönetimi
  - Microsoft Entra ID: Bulut ve SaaS uygulamalarına giriş, internet üzerinden erişim

**Hibrit kimlik:** Kurum içi AD kullanıcıları Entra ID'ye **senkronize** edilebilir; böylece kullanıcı aynı kimlikle hem kurum içi kaynaklara hem bulut uygulamalarına girer.

#### 3.2 Microsoft Entra Domain Services

Bazı eski (legacy) uygulamalar modern protokolleri konuşamaz; **domain join**, **Group Policy**, **LDAP** veya **Kerberos/NTLM** ister. Bunları buluta taşırken iki seçenek vardır:

1. Azure VM'lere kendi domain controller'larını kurup yönetmek (IaaS — yama, yedek, replikasyon senin işin).
2. **Microsoft Entra Domain Services:** Bu klasik AD özelliklerini **yönetilen** (managed) bir hizmet olarak sunar. Domain controller'ları Microsoft kurar, yamalar ve yedekler; sen yalnızca VM'leri bu yönetilen domain'e katarsın.

- Yönetilen domain, Entra ID'den **tek yönlü** senkronize olur: kullanıcı ve gruplar Entra ID'den gelir. Domain Services'ta oluşturulan nesneler Entra ID'ye geri yazılmaz.
- Sınav kalıbı: "Eski uygulama LDAP/Kerberos/domain join istiyor, **domain controller yönetmek istemiyoruz**" → **Entra Domain Services**.

#### 3.3 Benefits of Microsoft Entra ID

| Fayda | Ne sağlar |
|---|---|
| **Authentication** | Kimlik doğrulama, **self-service password reset**, **MFA**, özel yasaklı şifre listesi, **smart lockout** (kaba kuvvet denemelerini kilitleme) |
| **Single sign-on (SSO)** | Tek kimlikle birçok uygulamaya tek giriş (3.5) |
| **Application management** | Bulut (SaaS) ve kurum içi uygulamaları tek yerden yönetme |
| **Device management** | Cihazları Entra ID'ye kaydetme/katma; cihaz durumunu Conditional Access sinyali olarak kullanma |
| **Conditional Access** | Sinyale göre erişim kararı (3.9) |
| **Denetim ve raporlama** | Giriş (sign-in) ve denetim (audit) log'ları, merkezi görünürlük |

#### 3.4 Authentication vs authorization ⭐

- **Soru**
  - Authentication (AuthN): "**Sen kimsin?**" — iddia ettiğin kişi misin?
  - Authorization (AuthZ): "**Ne yapabilirsin?**" — hangi kaynağa hangi erişimin var?
- **Benzetme**
  - Authentication (AuthN): Havalimanında **pasaport** kontrolü
  - Authorization (AuthZ): **Biniş kartı**: hangi uçağa, hangi koltuğa
- **Araç**
  - Authentication (AuthN): Şifre, MFA, passwordless, sertifika
  - Authorization (AuthZ): Azure RBAC rolleri, Entra rolleri, uygulama izinleri
- **Sıra**
  - Authentication (AuthN): **Önce**
  - Authorization (AuthZ): **Sonra** (kimliği doğrulanmış kişinin yetkisi)

AuthN başarılı olsa bile AuthZ reddedebilir: kullanıcı portala girer (AuthN ✓) ama başka bir resource group'u göremez (AuthZ ✗).

**Kimlik doğrulama yöntemleri — özet:**

- **Şifre tabanlı (password-based):** en yaygın ama en zayıf yöntem — tahmin edilir, tekrar kullanılır, phishing ile çalınır. Tek başına yeterli kabul edilmez.
- **SSO:** tek giriş ile birden çok uygulama (3.5).
- **MFA:** şifreye ek ikinci (üçüncü) faktör (3.6).
- **Passwordless:** şifreyi tamamen kaldıran, biyometri/cihaz/anahtar tabanlı yöntemler (3.7). Güvenlik sırası kabaca: şifre < şifre + MFA < passwordless.

#### 3.5 Single sign-on (SSO)

**SSO**, kullanıcının **tek kimlik ve tek giriş** ile birden çok bağımsız uygulamaya erişmesidir.

- **Kullanıcı için:** tek şifre (veya passwordless), tekrar tekrar giriş yok.
- **Güvenlik için:** her ek kimlik = ek şifre = ek risk. Kimlikler tek yerde olunca **politika tek yerde** uygulanır (MFA, Conditional Access) ve çalışan ayrıldığında **tek hesabı kapatmak** tüm erişimi bitirir.
- **Uygulama için:** Entra ID, SAML/OIDC ile binlerce SaaS uygulamasına hazır SSO entegrasyonu sunar (uygulama galerisi).

> SSO **kimliğin kaç kez sorulduğunu** azaltır; MFA ise kimliğin **ne kadar güçlü** kanıtlandığını artırır. İkisi birlikte kullanılır ve birbirinin alternatifi değildir.

#### 3.6 Multi-factor authentication (MFA / 2FA)

**MFA**, kimliği **iki veya daha fazla farklı türde faktörle** kanıtlamaktır:

| Faktör türü | Örnek |
|---|---|
| **Bildiğin bir şey** (knowledge) | Şifre, PIN, güvenlik sorusu |
| **Sahip olduğun bir şey** (possession) | Telefon (Authenticator bildirimi/kodu), donanım anahtarı, akıllı kart |
| **Olduğun bir şey** (inherence) | Parmak izi, yüz tanıma |

- **2FA**, MFA'nın iki faktörlü halidir. İki şifre istemek MFA **değildir**; ikisi de aynı türdendir (bildiğin).
- **Microsoft Entra MFA yöntemleri:** Microsoft Authenticator (bildirim veya tek kullanımlık kod), OATH donanım/yazılım token'ları, SMS ve sesli arama (en zayıfları), FIDO2 güvenlik anahtarları, Windows Hello for Business.
- **Şifre çalınsa bile** saldırgan ikinci faktöre sahip olmadığı için giremez; MFA'nın değeri buradadır.

#### 3.7 Passwordless authentication

Şifre hem en zayıf halkadır (tahmin, tekrar kullanım, phishing) hem de kullanıcı için zahmetlidir. **Passwordless** yöntemler şifreyi tamamen kaldırır ve "**sahip olduğun** cihaz + **olduğun** (biyometri) veya cihaza bağlı **bildiğin** (yerel PIN)" ile giriş yapar. Bu yöntemler zaten çok faktörlüdür. Microsoft'un üç ana seçeneği:

- **Windows Hello for Business**
  - Nasıl çalışır: Kullanıcının **kendi Windows cihazına** bağlı biyometri veya PIN; kimlik bilgisi cihazdan çıkmaz
  - Tipik kullanım: Atanmış bir Windows bilgisayarı kullanan bilgi çalışanları
- **Microsoft Authenticator**
  - Nasıl çalışır: Telefon, kimlik doğrulayıcı olur: phone sign-in (eşleşen numara + biyometri/PIN) veya Authenticator'da saklanan **passkey**
  - Tipik kullanım: Mobil çalışanlar, her cihazdan giriş
- **FIDO2 güvenlik anahtarı / passkey**
  - Nasıl çalışır: Açık standart (FIDO2/WebAuthn) donanım anahtarı (USB, NFC) veya cihazdaki passkey; **phishing'e dayanıklı**
  - Tipik kullanım: Paylaşılan cihazlar, yüksek ayrıcalıklı hesaplar, telefon kullanılamayan ortamlar

> Passwordless hem **daha güvenli** (phishing'e dayanıklı, çalınacak şifre yok) hem **daha kolaydır**. Sınav bu iki faydayı birlikte sorar.

#### 3.8 External identities: Microsoft Entra External ID ve B2B

Kuruluş dışındaki kişilerin (iş ortakları, tedarikçiler, danışmanlar) **kendi kimlikleriyle** kaynaklara güvenli erişimini sağlayan yetenek kümesine **Microsoft Entra External ID** denir. Bu kişiler **external user / guest user** (dış kullanıcı / misafir) olarak adlandırılır.

- **B2B collaboration (business-to-business):** dış kullanıcı **kendi kimliğiyle** (kendi kurumunun Entra ID hesabı, Microsoft hesabı, Google hesabı veya e-posta tek kullanımlık kodu) senin tenant'ına **misafir (guest)** olarak davet edilir.
  - Parola yönetimi **davet edenin değil**, kullanıcının kendi kimlik sağlayıcısının işidir; kullanıcı için ayrı bir kurumsal hesap açılmaz.
  - Misafir, tenant'ta diğer kullanıcılar gibi **gruplara ve rollere atanabilir**; erişimi Conditional Access, MFA ve RBAC ile sınırlanır.
  - Örnek: Bir ajans çalışanına SharePoint sitesi ve Azure resource group'unda **Reader** yetkisi vermek.
- **Yönetim:** misafir hesabı kendi Entra ID'ni kullanır; çalışan ayrıldığında **tek bir yerden** (davet eden tenant'taki guest hesabı) erişimi kaldırabilirsin.

> Kural: **"Kurum dışındaki kişiye, kendi kimliğiyle, kaynaklarımıza erişim ver"** → **External ID / B2B collaboration (guest user)**. Kendi çalışanların → Entra ID kullanıcısı.

#### 3.9 Conditional Access

**Conditional Access**, Entra ID'nin **politika motorudur**: her giriş isteğinde sinyalleri toplar, bir karar verir ve uygular. Yapısı **"if-then"**dir: *eğer* (bu sinyaller) *ise* (bu kararı uygula).

| 1. Sinyaller (if) | 2. Karar | 3. Uygulama (then) |
|---|---|---|
| **Kullanıcı / grup** (yönetici mi, misafir mi?) | Değerlendirme | **Erişime izin ver** |
| **Konum** (IP aralığı, ülke, bilinen ofis ağı) | | **MFA iste** |
| **Cihaz** (uyumlu mu, kurumsal mı, platform) | | **Uyumlu/kurumsal cihaz** zorunlu tut |
| **Uygulama** (hangi uygulamaya erişiliyor) | | Şifre değişikliği iste, oturum süresini kısalt |
| **Risk** (oturum/kullanıcı riski) | | **Erişimi engelle** |

Örnek politikalar:

- "**Yönetici rolündeki** herkes, her girişte MFA kullanır."
- "**Bilinen ofis IP'leri dışından** giren kullanıcıdan MFA iste."
- "**Uyumsuz** (yönetilmeyen) cihazdan e-postaya erişim yok."
- "Belirli **ülkelerden** gelen tüm girişleri engelle."
- "Riskli oturumda (sızdırılmış kimlik bilgisi şüphesi) şifre değişikliği zorunlu."

> Conditional Access, MFA'yı **her zaman** değil **koşula bağlı** ister: güvenilir durumda kullanıcıyı rahat bırakır, riskli durumda ek kanıt ister. Conditional Access bir **Microsoft Entra ID Premium** özelliğidir.

#### 3.10 Azure role-based access control (RBAC) ⭐

**Azure RBAC**, Azure kaynaklarına **kimin, nerede, ne yapabileceğini** belirler. Bir **rol ataması** (role assignment) üç parçadan oluşur:

```
Role assignment = Security principal (WHO)  +  Role definition (WHAT)  +  Scope (WHERE)
                  user, group,                 permission set:            management group,
                  service principal,           Owner, Contributor,        subscription,
                  managed identity             Reader, …                  resource group, resource
```

**Rol ataması bileşenleri:**

- **Security principal (güvenlik sorumlusu) — KİM?** Rol alacak kimlik: **kullanıcı** (user), **grup** (group), **service principal** (bir uygulamanın/servisin kimliği) veya **managed identity** (Azure'un otomatik yönettiği uygulama kimliği).
- **Role definition (rol tanımı) — NE?** İzinlerin listesi: hangi işlemlere izin verildiği (ör. *Reader* yalnızca okur).
- **Scope (kapsam) — NEREDE?** Rolün geçerli olduğu seviye: management group, subscription, resource group veya tek kaynak.

**Built-in roller (sınavın sorduğu üç temel rol):**

- **Owner** — Kaynakları görüntüleme: ✓ · Kaynak oluşturma/değiştirme/silme: ✓ · Erişim (rol atamalarını) yönetme: ✓
- **Contributor** — Kaynakları görüntüleme: ✓ · Kaynak oluşturma/değiştirme/silme: ✓ · Erişim (rol atamalarını) yönetme: ✗
- **Reader** — Kaynakları görüntüleme: ✓ · Kaynak oluşturma/değiştirme/silme: ✗ · Erişim (rol atamalarını) yönetme: ✗
- Ayrıca **servise özel** built-in roller vardır (ör. *Virtual Machine Contributor*); Azure yüzlerce built-in rol sunar, gerekirse özel (custom) rol da tanımlanabilir.
- **Kapsam ve miras:** Bir kapsamda verilen rol, **altındaki tüm kapsamlara miras** kalır. Subscription'da Reader olan kullanıcı, o subscription'daki tüm resource group'ları ve kaynakları okuyabilir (→ C1.4 hiyerarşisi).
- **İzinler toplanır (additive):** Bir kullanıcının subscription'da Reader, bir resource group'ta Contributor rolü varsa, o resource group'ta **Contributor** gibi davranır.
- **En az yetki ilkesi (least privilege):** İşi yapmaya yetecek **en düşük** rolü, **en dar** kapsamda ver; kişiler yerine **gruplara** ata.
- **Nasıl uygulanır:** Portal, CLI, PowerShell, SDK veya REST'ten gelen her yönetim isteği **Azure Resource Manager**'dan geçer (→ C1.4, C1.13). ARM, isteği yapanın o kapsamda gereken izne sahip olup olmadığını kontrol eder.

**Karıştırılan üç kavram:**

- **Neyi kontrol eder**
  - Azure RBAC: **Azure kaynakları** üzerindeki işlemler (VM oluştur, storage sil)
  - Microsoft Entra rolleri: **Directory** üzerindeki işlemler (kullanıcı oluştur, şifre sıfırla, lisans ata)
  - Azure Policy (→ C1.12): **Kaynağın özellikleri** (hangi bölge, hangi SKU, zorunlu tag)
- **Örnek rol/kural**
  - Azure RBAC: Owner, Contributor, Reader
  - Microsoft Entra rolleri: Global Administrator, User Administrator
  - Azure Policy (→ C1.12): "Yalnızca West Europe", "tag zorunlu"
- **Kapsam**
  - Azure RBAC: Management group → subscription → RG → kaynak
  - Microsoft Entra rolleri: Tenant (veya administrative unit)
  - Azure Policy (→ C1.12): Management group → subscription → RG
- **Soru**
  - Azure RBAC: "**Kullanıcı** ne yapabilir?"
  - Microsoft Entra rolleri: "**Directory'de** kim ne yapabilir?"
  - Azure Policy (→ C1.12): "**Kaynak** nasıl olmalı?"

#### 3.11 Pratik — kullanıcı, grup ve kapsamlı rol ataması (`az` CLI)

Bu laboratuvar **ücretsizdir**: Entra ID'de kullanıcı/grup oluşturmak ve boş bir resource group'a rol atamak ücret üretmez. Yine de bütçe alarmın kurulu olsun (→ C1.1). Kullanıcı oluşturmak için tenant'ta **User Administrator** (veya Global Administrator), rol atamak için hedef kapsamda **Owner** veya **User Access Administrator** gerekir. En temizi, kendi oluşturduğun bir deneme tenant'ı/aboneliğidir.

```bash
az login
TENANT_DOMAIN=$(az ad signed-in-user show --query userPrincipalName -o tsv | cut -d@ -f2)
echo "$TENANT_DOMAIN"          # e.g. contoso.onmicrosoft.com

# 1) Two resource groups: the permission will exist on only one
az group create --name rg-az900-c19-a --location westeurope
az group create --name rg-az900-c19-b --location westeurope

# 2) A user and a group in Entra ID
az ad user create \
  --display-name "AZ900 Reader" \
  --user-principal-name "az900reader@$TENANT_DOMAIN" \
  --password "Temp-Password-$RANDOM!aA" \
  --force-change-password-next-sign-in true
az ad group create --display-name grp-az900-readers --mail-nickname grp-az900-readers

USER_ID=$(az ad user show --id "az900reader@$TENANT_DOMAIN" --query id -o tsv)
GROUP_ID=$(az ad group show --group grp-az900-readers --query id -o tsv)
az ad group member add --group grp-az900-readers --member-id "$USER_ID"
az ad group member list --group grp-az900-readers --query "[].userPrincipalName" -o tsv

# 3) Assign the Reader role to the GROUP, only at the rg-az900-c19-a scope (least privilege + assign to groups)
SCOPE_A=$(az group show --name rg-az900-c19-a --query id -o tsv)
az role assignment create --assignee-object-id "$GROUP_ID" --assignee-principal-type Group \
  --role Reader --scope "$SCOPE_A"

# 4) Verify: the assignment exists only at scope A, not at B
az role assignment list --scope "$SCOPE_A" --output table
az role assignment list --scope "$(az group show --name rg-az900-c19-b --query id -o tsv)" --output table
az role assignment list --all --assignee "$GROUP_ID" --query "[].{role:roleDefinitionName, scope:scope}" -o table

# 5) Look inside the roles: Reader is only */read; Contributor can do everything except manage permissions
az role definition list --name Reader      --query "[].permissions[0].actions"    -o tsv
az role definition list --name Contributor --query "[].permissions[0].notActions" -o tsv
az role definition list --name Owner       --query "[].permissions[0].actions"    -o tsv
```

Beklenen gözlem: Kullanıcı (portalda veya `az login` ile, ilk girişte şifre değiştirip MFA kaydı yaparak) `rg-az900-c19-a`'yı ve içini **görür** ama içinde kaynak **oluşturamaz**; `rg-az900-c19-b`'yi ise **hiç görmez**. Contributor'ın `notActions` listesinde `Microsoft.Authorization/*/Write` ve `*/Delete` bulunur: Contributor her şeyi yapar ama **başkasına rol atayamaz**.

**Misafir (guest) daveti — B2B collaboration** (isteğe bağlı): `az` CLI'nin doğrudan bir "davet" komutu yoktur. Microsoft Graph'ın `invitations` API'si `az rest` ile çağrılabilir. Bunun için oturumdaki hesabın davet yetkisi (ör. Guest Inviter veya User Administrator rolü) olmalıdır:

```bash
az rest --method POST --url "https://graph.microsoft.com/v1.0/invitations" \
  --headers "Content-Type=application/json" \
  --body '{"invitedUserEmailAddress":"partner@example.com","inviteRedirectUrl":"https://myapps.microsoft.com","sendInvitationMessage":true}'
# The invited user appears in the tenant as userType=Guest:
az ad user list --filter "userType eq 'Guest'" --query "[].{name:displayName, upn:userPrincipalName}" -o table
```

**Temizlik:**

```bash
az role assignment delete --assignee "$GROUP_ID" --role Reader --scope "$SCOPE_A"
az ad group delete --group grp-az900-readers
az ad user delete --id "az900reader@$TENANT_DOMAIN"
# (if you sent an invitation) az ad user delete --id <guest user's id>
az group delete --name rg-az900-c19-a --yes --no-wait
az group delete --name rg-az900-c19-b --yes --no-wait
```

> **Alıştırma.** Bir şirkette üç ekip var: **Platform** (tüm subscription'ları yönetir, başkalarına yetki verir), **Geliştiriciler** (yalnızca `rg-app-dev` içinde kaynak açıp siler), **Denetçiler** (tüm subscription'ları yalnızca görüntüler). Her ekip için rol ve kapsamı yaz.
>
> <details><summary>Çözüm</summary>
>
> - **Platform**
>   - Rol: **Owner**
>   - Kapsam: Management group (veya her subscription)
>   - Neden: Kaynak yönetimi + rol atama; management group'tan tüm subscription'lara miras
> - **Geliştiriciler**
>   - Rol: **Contributor** · Kapsam: `rg-app-dev` resource group
>   - Neden: Kaynak oluştur/sil, ama yetki dağıtamazlar; kapsam yalnızca o RG
> - **Denetçiler**
>   - Rol: **Reader** · Kapsam: Management group
>   - Neden: Yalnızca görüntüleme, tüm subscription'lara miras
>
> Hepsi **gruplara** atanır (kişiye değil); ekip değişince yalnızca grup üyeliği değişir.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir sigorta şirketi kimlik altyapısını modernize ediyor:

1. **Hibrit kimlik:** Kurum içindeki AD kullanıcıları Entra ID'ye senkronize ediliyor. Çalışanlar aynı hesapla hem dosya sunucusuna hem Microsoft 365'e giriyor. Microsoft 365, Salesforce ve iç uygulamalar Entra ID üzerinden **SSO** ile bağlanıyor.
2. **Eski poliçe uygulaması** LDAP ve domain join istiyor. Azure'a taşınırken VM'lere domain controller kurmak yerine **Entra Domain Services** kullanılıyor.
3. **MFA ve passwordless:** Önce tüm çalışanlar Authenticator ile **MFA**'ya kaydediliyor. Yöneticiler ve çağrı merkezinin paylaşılan cihazları için **FIDO2 anahtarları**, ofis çalışanları için **Windows Hello for Business** devreye alınıyor.
4. **Conditional Access:** "Yöneticiler her zaman MFA", "ofis dışından girişte MFA", "yönetilmeyen cihazdan poliçe sistemine erişim yok", "şirketin faaliyet göstermediği ülkelerden giriş engelli" politikaları tanımlanıyor.
5. **External identities:** Acente çalışanları kendi kurumsal kimlikleriyle **B2B guest (misafir)** olarak davet edilip yalnızca acente portalına yetkilendiriliyor; acente ayrıldığında erişim tek yerden kapatılıyor.
6. **RBAC:** Azure'da Platform ekibi management group'ta **Owner**, uygulama ekipleri kendi resource group'larında **Contributor**, iç denetim **Reader**. Tüm atamalar gruplara yapılıyor ve üç ayda bir gözden geçiriliyor.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Bu konu çoğunlukla **senaryo → servis/yöntem** eşleştirmesi ve **evet/hayır** tablolarıyla sorulur. RBAC'ta rol ve kapsam seçimi, Conditional Access'te sinyal/karar mantığı beklenir.

- **"Bulut tabanlı kimlik ve erişim yönetimi servisi"** — Doğru cevap: **Microsoft Entra ID** · Tuzak: Entra Domain Services
- "Eski uygulama domain join / LDAP / Kerberos / Group Policy istiyor, DC yönetmek istemiyoruz"
  - Doğru cevap: **Entra Domain Services**
  - Tuzak: Entra ID (bu protokolleri sunmaz)
- **"Kimliği doğrulama" / "kimliğini kanıtla"** — Doğru cevap: **Authentication** · Tuzak: Authorization
- **"Hangi kaynağa erişebileceğini belirleme"** — Doğru cevap: **Authorization** (RBAC) · Tuzak: Authentication
- **"Tek girişle birçok uygulama"** — Doğru cevap: **SSO** · Tuzak: MFA
- **"Şifre + telefona gelen bildirim"** — Doğru cevap: **MFA** · Tuzak: SSO
- **"Şifre olmadan, biyometri/güvenlik anahtarıyla giriş"**
  - Doğru cevap: **Passwordless** (Windows Hello, Authenticator, FIDO2)
  - Tuzak: MFA (passwordless zaten çok faktörlüdür)
- **"İş ortağını kendi hesabıyla misafir (guest) olarak davet et"** — Doğru cevap: **Entra External ID / B2B collaboration** · Tuzak: Yeni kullanıcı hesabı açmak
- **"Kendi kimliğiyle dış kullanıcıya erişim ver"** — Doğru cevap: **Guest user (B2B)** · Tuzak: Entra Domain Services
- **"Konuma/cihaza/riske göre MFA iste veya engelle"**
  - Doğru cevap: **Conditional Access**
  - Tuzak: RBAC (Conditional Access girişe, RBAC kaynak işlemlerine karar verir)
- **"Kaynakları yalnızca görüntüleme"** — Doğru cevap: **Reader** · Tuzak: Contributor
- **"Her şeyi yönet ama başkasına erişim veremesin"** — Doğru cevap: **Contributor** · Tuzak: Owner
- **"Kaynakları ve erişimi tam yönet"**
  - Doğru cevap: **Owner**
  - Tuzak: Contributor (rol ataması yapamaz)
- **"Rolü birçok subscription'a birden uygula"**
  - Doğru cevap: **Management group** kapsamında rol ataması
  - Tuzak: Her subscription'a tek tek

**Anahtar kelimeler:** *identity as the primary security perimeter*, *tenant*, *managed domain*, *guest user*, *social identities*, *signals → decisions → enforcement*, *security principal + role definition + scope*, *inherited*, *least privilege*.

**Evet/hayır tuzakları:**
- "RBAC atamaları alt kapsamlara miras kalır" → **Evet**.
- "Entra ID, Kerberos ve LDAP ile domain join sağlar" → **Hayır** (bu Domain Services'tır).
- "Conditional Access, kullanıcının bir resource group'ta hangi işlemleri yapabileceğini belirler" → **Hayır** (bu RBAC'tır; Conditional Access **girişe/erişime** karar verir).
- "Contributor rolü başka kullanıcılara rol atayabilir" → **Hayır**.
- "MFA, iki farklı şifre istemektir" → **Hayır** (iki farklı **faktör türü**).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Entra ID = bulutta çalışan Active Directory" | Entra ID modern HTTP protokolleri (OAuth/OIDC/SAML) kullanır; domain join, GPO, Kerberos/LDAP sunmaz. Bunlar için Entra Domain Services |
| "SSO açtık, MFA'ya gerek yok" | SSO giriş sayısını azaltır, MFA kimliği güçlendirir; ikisi birlikte |
| "SMS ile kod = en güvenli MFA" | SMS en zayıf yöntemlerdendir (SIM swap); Authenticator, FIDO2 ve passkey daha güçlüdür |
| "Misafir kullanıcı için ayrı kurumsal hesap açılır" | B2B'de misafir **kendi kimliğiyle** gelir; parola yönetimi kendi kimlik sağlayıcısındadır |
| Rolleri kişilere tek tek atamak | Gruplara ata; ayrılan/katılan kişide yalnızca üyelik değişsin |
| Herkese subscription'da Owner vermek | En az yetki: işi yapan en düşük rol, en dar kapsam |
| "Entra rolü olan Azure kaynaklarını da yönetir" | Entra rolleri (directory) ile Azure RBAC rolleri (kaynaklar) ayrıdır |
| "Conditional Access, kaynaklarda hangi işlemlerin yapılabileceğini belirler" | Hayır — o RBAC'tır; Conditional Access **girişe/erişime** koşullara göre karar verir |

---

### 7. Performans & trade-off

- **Conditional Access**
  - Kazanç: Riske göre ayarlı güvenlik; güvenilir durumda kullanıcı rahat
  - Bedel: Premium lisans gerektirir; yanlış yapılandırılmış politika kullanıcıları (yöneticiler dahil) dışarıda bırakabilir
- **MFA zorunluluğu**
  - Kazanç: Hesap ele geçirmeye karşı çok güçlü koruma
  - Bedel: Kullanıcı sürtünmesi, kayıt süreci, yardım masası yükü
- **Passwordless**
  - Kazanç: Daha güvenli ve daha hızlı giriş, şifre sıfırlama talepleri azalır
  - Bedel: Cihaz/anahtar dağıtımı ve kayıp anahtar süreci
- **Entra Domain Services**
  - Kazanç: Eski uygulamalar DC yönetmeden buluta
  - Bedel: Ücretli servis; tam AD DS esnekliği yok (ör. şema genişletme kısıtlı)
- **Kendi DC'lerin (IaaS VM)**
  - Kazanç: Tam AD kontrolü
  - Bedel: Yama, yedek, replikasyon ve güvenlik yükü sende
- **Geniş kapsamlı, yüksek roller**
  - Kazanç: Az sayıda atama, kolay yönetim
  - Bedel: Büyük saldırı yüzeyi; tek hesap ele geçirilince tüm ortam risk altında
- **Dar kapsamlı, en az yetkili roller** — Kazanç: Etki alanı küçük · Bedel: Daha çok atama ve gözden geçirme

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure identity, access, and security](https://learn.microsoft.com/en-us/training/modules/describe-azure-identity-access-security/)
- [What is Microsoft Entra ID?](https://learn.microsoft.com/en-us/entra/fundamentals/whatis) · [Microsoft Entra Domain Services overview](https://learn.microsoft.com/en-us/entra/identity/domain-services/overview)
- [Passwordless authentication options](https://learn.microsoft.com/en-us/entra/identity/authentication/concept-authentication-passwordless) · [Microsoft Entra multifactor authentication](https://learn.microsoft.com/en-us/entra/identity/authentication/concept-mfa-howitworks)
- [What is Conditional Access?](https://learn.microsoft.com/en-us/entra/identity/conditional-access/overview)
- [External ID overview (B2B collaboration)](https://learn.microsoft.com/en-us/entra/external-id/external-identities-overview)
- [What is Azure RBAC?](https://learn.microsoft.com/en-us/azure/role-based-access-control/overview) · [Azure built-in roles](https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles) · [Azure roles vs Microsoft Entra roles](https://learn.microsoft.com/en-us/azure/role-based-access-control/rbac-and-directory-admin-roles)
- [`az ad`](https://learn.microsoft.com/en-us/cli/azure/ad) · [`az role assignment`](https://learn.microsoft.com/en-us/cli/azure/role/assignment) · [Microsoft Graph: create invitation](https://learn.microsoft.com/en-us/graph/api/invitation-post)

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
