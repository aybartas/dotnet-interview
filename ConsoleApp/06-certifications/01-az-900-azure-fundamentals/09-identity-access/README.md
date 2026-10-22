# C1.9 Kimlik ve Erişim: Microsoft Entra ID, Kimlik Doğrulama, RBAC

> 📁 `ConsoleApp/06-certifications/01-az-900-azure-fundamentals/09-identity-access/` · Sınav alanı: **Describe Azure architecture and services (%35-40)** · Önkoşul: [C1.4](../04-core-architecture/README.md) (kapsam hiyerarşisi) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe Azure identity, access, and security* — directory servisleri (**Microsoft Entra ID**, **Microsoft Entra Domain Services**) · kimlik doğrulama yöntemleri (**SSO, MFA, passwordless**) · **external identities** (B2B, B2C) · **Conditional Access** · **Azure role-based access control (RBAC)**. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et. Geliştirici derinliği (OAuth 2.0/OIDC akışları, token, managed identity) → M9, C2.11.

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

Bulutta kaynaklar herhangi bir yerden, herhangi bir cihazdan erişilebilir olduğu için güvenliğin sınırı artık şirket ağının duvarı değil, **kimliktir** ("identity is the new perimeter"). Bu konu iki soruyu cevaplar:

| Soru | Kavram | Azure aracı |
|---|---|---|
| **Sen kimsin?** (authentication, AuthN) | Kimlik doğrulama: şifre, **MFA**, **passwordless**, **SSO** | **Microsoft Entra ID**, Conditional Access |
| **Ne yapabilirsin?** (authorization, AuthZ) | Yetkilendirme: hangi kaynakta hangi işlem | **Azure RBAC** (rol atamaları) |

Çevresindeki parçalar:

- **Directory servisleri:** Microsoft Entra ID (bulut tabanlı kimlik) ve Microsoft Entra Domain Services (yönetilen klasik AD özellikleri).
- **External identities:** iş ortakları (**B2B**) ve müşteriler (**B2C** / Entra External ID) için kimlik.
- **Conditional Access:** "kim, nereden, hangi cihazla?" sinyallerine göre erişime karar veren politika motoru.

Bu konunun C# kodu yoktur; pratik, `az` CLI laboratuvarıdır (3.11).

---

## 2. Neden?

- **Sınav:** Kimlik/erişim/güvenlik maddeleri "Describe Azure architecture and services" alanının (%35-40) büyük parçasıdır; Entra ID ↔ Domain Services, B2B ↔ B2C, AuthN ↔ AuthZ, Owner ↔ Contributor ↔ Reader eşleştirmeleri neredeyse her sınavda çıkar.
- **Gerçek işte:** Gerçek ihlallerin çoğu çalınmış veya zayıf kimlik bilgisiyle başlar. Microsoft'un kendi açıklamalarına göre MFA, hesap ele geçirme saldırılarının çok büyük kısmını durdurur; Azure bu yüzden yönetim işlemlerinde MFA'yı **zorunlu** hale getirdi (3.6).
- **Shared responsibility (→ C1.1):** Hesaplar ve kimlikler **her modelde müşterinin** sorumluluğundadır. Entra ID altyapıyı sağlar; kimin hangi role sahip olduğu, MFA'nın açık olup olmadığı senin kararındır.

---

## 3. Nasıl?

### 3.1 Microsoft Entra ID (eski adı Azure Active Directory)

**Microsoft Entra ID**, Microsoft'un **bulut tabanlı kimlik ve erişim yönetimi** servisidir. Kullanıcıların, grupların, uygulamaların ve cihazların kimliklerini tutar; Microsoft 365, Azure portal, Dynamics 365 ve binlerce SaaS/özel uygulamaya girişi yönetir. (Azure AD, 2023'te **Microsoft Entra ID** adını aldı; sınav ve dokümanlar yeni adı kullanır.)

- **Tenant:** bir kuruluşun Entra ID örneği (ör. `contoso.onmicrosoft.com`). Her Azure subscription **tek bir tenant'a** güvenir; bir tenant'a birden çok subscription bağlı olabilir (→ C1.4 hiyerarşisinin en üstü).
- **Kimler kullanır?**
  - **BT yöneticileri:** kullanıcı/grup yönetimi, erişim politikaları, MFA.
  - **Geliştiriciler:** uygulamalarına SSO ve kimlik eklemek (app registration, OAuth 2.0/OIDC → M9).
  - **Kullanıcılar:** tek hesapla giriş, **self-service password reset** (SSPR).
  - **Online servis aboneleri:** Microsoft 365, Azure ve Dynamics 365 zaten Entra ID kullanır.

**On-prem Active Directory (AD DS) ile farkı:**

| | Active Directory Domain Services (on-prem) | Microsoft Entra ID |
|---|---|---|
| Nerede | Kendi domain controller sunucuların | Microsoft'un yönettiği küresel bulut servisi |
| Protokoller | **Kerberos, NTLM, LDAP**, Group Policy (GPO) | **HTTP tabanlı modern protokoller:** OAuth 2.0, OpenID Connect, SAML, WS-Federation |
| Yapı | Forest, domain, OU, domain join | Düz yapı: tenant, kullanıcı, grup, administrative unit |
| Tipik kullanım | Kurum içi Windows sunucu/istemci yönetimi | Bulut ve SaaS uygulamalarına giriş, internet üzerinden erişim |

**Hibrit kimlik:** Kurum içi AD kullanıcılarını Entra ID'ye senkronize etmek için **Microsoft Entra Connect** (Sync) veya daha hafif ajan tabanlı **Microsoft Entra Cloud Sync** kullanılır. Böylece kullanıcı aynı kimlikle hem kurum içi kaynaklara hem bulut uygulamalarına girer. Senkronizasyon seçenekleri (password hash sync, pass-through authentication, federation) *(bağlam)* sınavda ad olarak geçebilir.

### 3.2 Microsoft Entra Domain Services

Bazı eski (legacy) uygulamalar modern protokolleri konuşamaz; **domain join**, **Group Policy**, **LDAP** veya **Kerberos/NTLM** ister. Bunları buluta taşırken iki seçenek vardır:

1. Azure VM'lere kendi domain controller'larını kurup yönetmek (IaaS — yama, yedek, replikasyon senin işin).
2. **Microsoft Entra Domain Services:** Bu klasik AD özelliklerini **yönetilen** (managed) bir hizmet olarak sunar. Domain controller'ları Microsoft kurar, yamalar ve yedekler; sen yalnızca VM'leri bu yönetilen domain'e katarsın.

- Yönetilen domain, Entra ID'den **tek yönlü** senkronize olur: kullanıcı ve gruplar Entra ID'den gelir. Domain Services'ta oluşturulan nesneler Entra ID'ye geri yazılmaz.
- Sınav kalıbı: "Eski uygulama LDAP/Kerberos/domain join istiyor, **domain controller yönetmek istemiyoruz**" → **Entra Domain Services**.

### 3.3 Benefits of Microsoft Entra ID

| Fayda | Ne sağlar |
|---|---|
| **Authentication** | Kimlik doğrulama, **self-service password reset**, **MFA**, özel yasaklı şifre listesi, **smart lockout** (kaba kuvvet denemelerini kilitleme) |
| **Single sign-on (SSO)** | Tek kimlikle birçok uygulamaya tek giriş (3.5) |
| **Application management** | Bulut (SaaS) ve kurum içi uygulamaları tek yerden yönetme; **My Apps** portalı; **Application Proxy** ile kurum içi web uygulamalarına VPN'siz güvenli erişim |
| **Device management** | Cihazları Entra ID'ye kaydetme/katma; **Microsoft Intune** ile cihaz politikaları; cihaz durumunu Conditional Access sinyali olarak kullanma |
| **Identity protection** *(P2)* | Riskli oturum ve riskli kullanıcı tespiti (sızdırılmış kimlik bilgisi, olağandışı konum) |
| **Conditional Access** *(P1)* | Sinyale göre erişim kararı (3.9) |
| **Denetim ve raporlama** | Giriş (sign-in) ve denetim (audit) log'ları, merkezi görünürlük |

> *(bağlam)* Lisans katmanları: **Free** (Azure/Microsoft 365 aboneliğiyle gelen temel kimlik, security defaults), **P1** (Conditional Access, gelişmiş grup yönetimi, hibrit özellikler), **P2** (Identity Protection, Privileged Identity Management). Sınav fiyat sormaz; "Conditional Access lisans ister" bilgisi yeterlidir.

### 3.4 Authentication vs authorization ⭐

| | **Authentication (AuthN)** | **Authorization (AuthZ)** |
|---|---|---|
| Soru | "**Sen kimsin?**" — iddia ettiğin kişi misin? | "**Ne yapabilirsin?**" — hangi kaynağa hangi erişimin var? |
| Benzetme | Havalimanında **pasaport** kontrolü | **Biniş kartı**: hangi uçağa, hangi koltuğa |
| Araç | Şifre, MFA, passwordless, sertifika | Azure RBAC rolleri, Entra rolleri, uygulama izinleri |
| Sıra | **Önce** | **Sonra** (kimliği doğrulanmış kişinin yetkisi) |

AuthN başarılı olsa bile AuthZ reddedebilir: kullanıcı portala girer (AuthN ✓) ama başka bir resource group'u göremez (AuthZ ✗).

### 3.5 Single sign-on (SSO)

**SSO**, kullanıcının **tek kimlik ve tek giriş** ile birden çok bağımsız uygulamaya erişmesidir.

- **Kullanıcı için:** tek şifre (veya passwordless), tekrar tekrar giriş yok.
- **Güvenlik için:** her ek kimlik = ek şifre = ek risk. Kimlikler tek yerde olunca **politika tek yerde** uygulanır (MFA, Conditional Access) ve çalışan ayrıldığında **tek hesabı kapatmak** tüm erişimi bitirir.
- **Uygulama için:** Entra ID, SAML/OIDC ile binlerce SaaS uygulamasına hazır SSO entegrasyonu sunar (uygulama galerisi).

> SSO **kimliğin kaç kez sorulduğunu** azaltır; MFA ise kimliğin **ne kadar güçlü** kanıtlandığını artırır. İkisi birlikte kullanılır ve birbirinin alternatifi değildir.

### 3.6 Multi-factor authentication (MFA / 2FA)

**MFA**, kimliği **iki veya daha fazla farklı türde faktörle** kanıtlamaktır:

| Faktör türü | Örnek |
|---|---|
| **Bildiğin bir şey** (knowledge) | Şifre, PIN, güvenlik sorusu |
| **Sahip olduğun bir şey** (possession) | Telefon (Authenticator bildirimi/kodu), donanım anahtarı, akıllı kart |
| **Olduğun bir şey** (inherence) | Parmak izi, yüz tanıma |

- **2FA**, MFA'nın iki faktörlü halidir. İki şifre istemek MFA **değildir**; ikisi de aynı türdendir (bildiğin).
- **Microsoft Entra MFA yöntemleri:** Microsoft Authenticator (bildirim veya tek kullanımlık kod), OATH donanım/yazılım token'ları, SMS ve sesli arama (en zayıfları), FIDO2 güvenlik anahtarları, Windows Hello for Business.
- **Şifre çalınsa bile** saldırgan ikinci faktöre sahip olmadığı için giremez; MFA'nın değeri buradadır.
- **Security defaults:** Entra ID'nin **ücretsiz**, tek anahtarla açılan temel güvenlik ayarları. Tüm kullanıcılara MFA kaydı yaptırır, yöneticilere ve riskli işlemlere MFA ister, MFA desteklemeyen **eski (legacy) kimlik doğrulama protokollerini** engeller. Yeni tenant'larda varsayılan olarak açıktır. Ayrıntılı ve koşullu kurallar gerekiyorsa yerine Conditional Access kullanılır.
- **Azure'da zorunlu MFA:** Microsoft, Azure yönetim işlemleri için MFA'yı kademeli olarak **zorunlu** kıldı: **1. aşama** Azure portal, Microsoft Entra admin center ve Intune admin center girişleri (2025 Mart itibarıyla tüm tenant'lara yayıldı); **2. aşama** 1 Ekim 2025'ten itibaren kademeli olarak Azure CLI, Azure PowerShell, IaC araçları ve REST API üzerinden yapılan **oluşturma/güncelleme/silme** işlemleri. Managed identity ve service principal gibi **iş yükü kimlikleri** etkilenmez. (Güncel durum için Microsoft Learn'deki "mandatory MFA" sayfasını kontrol et.)

### 3.7 Passwordless authentication

Şifre hem en zayıf halkadır (tahmin, tekrar kullanım, phishing) hem de kullanıcı için zahmetlidir. **Passwordless** yöntemler şifreyi tamamen kaldırır ve "**sahip olduğun** cihaz + **olduğun** (biyometri) veya cihaza bağlı **bildiğin** (yerel PIN)" ile giriş yapar. Bu yöntemler zaten çok faktörlüdür. Microsoft'un üç ana seçeneği:

| Yöntem | Nasıl çalışır | Tipik kullanım |
|---|---|---|
| **Windows Hello for Business** | Kullanıcının **kendi Windows cihazına** bağlı biyometri veya PIN; kimlik bilgisi cihazdan çıkmaz | Atanmış bir Windows bilgisayarı kullanan bilgi çalışanları |
| **Microsoft Authenticator** | Telefon, kimlik doğrulayıcı olur: phone sign-in (eşleşen numara + biyometri/PIN) veya Authenticator'da saklanan **passkey** | Mobil çalışanlar, her cihazdan giriş |
| **FIDO2 güvenlik anahtarı / passkey** | Açık standart (FIDO2/WebAuthn) donanım anahtarı (USB, NFC) veya cihazdaki passkey; **phishing'e dayanıklı** | Paylaşılan cihazlar, yüksek ayrıcalıklı hesaplar, telefon kullanılamayan ortamlar |

> Passwordless hem **daha güvenli** (phishing'e dayanıklı, çalınacak şifre yok) hem **daha kolaydır**. Sınav bu iki faydayı birlikte sorar.

### 3.8 External identities: B2B ve B2C

Kuruluş dışındaki kişilerin (iş ortakları, tedarikçiler, müşteriler) **kendi kimlikleriyle** uygulamalarına güvenli erişimi:

| | **B2B collaboration** | **B2C** / **Microsoft Entra External ID** (müşteri) |
|---|---|---|
| Kim | **İş ortakları**, tedarikçiler, danışmanlar | Uygulamanı kullanan **müşteriler** (tüketiciler) |
| Nasıl | Kendi kimlikleriyle (kendi Entra ID'leri, Microsoft hesabı, Google, e-posta tek kullanımlık kod) **misafir (guest)** olarak senin tenant'ına davet edilirler | Uygulamana **kayıt olur/giriş yaparlar**: e-posta/şifre, sosyal hesaplar (Google, Facebook…), özelleştirilebilir marka sayfaları |
| Nerede yönetilir | Kendi (workforce) tenant'ında, diğer kullanıcılar gibi gruplara ve rollere atanır | Ayrı bir müşteri (external) tenant'ında, milyonlarca kullanıcıya ölçeklenir |
| Örnek | Ajans çalışanına SharePoint sitesi ve Azure resource group'unda Reader yetkisi | E-ticaret sitesinde "Google ile giriş yap" |

- **B2B direct connect** *(bağlam)*: iki kuruluş arasında karşılıklı güven; ör. Teams **paylaşılan kanallar**, misafir hesabı oluşturmadan.
- **Azure AD B2C** 1 Mayıs 2025'ten itibaren **yeni müşterilere satılmıyor**; mevcut müşteriler kullanmaya devam ediyor ve Microsoft en az 2030'a kadar desteği taahhüt ediyor. Yeni müşteri kimliği (CIAM) projeleri için önerilen ürün **Microsoft Entra External ID**'dir. Sınavda "B2C" terimi kavram olarak geçmeye devam edebilir: **müşteriye dönük kimlik**.

### 3.9 Conditional Access

**Conditional Access**, Entra ID'nin **politika motorudur**: her giriş isteğinde sinyalleri toplar, bir karar verir ve uygular. Yapısı **"if-then"**dir: *eğer* (bu sinyaller) *ise* (bu kararı uygula).

| 1. Sinyaller (if) | 2. Karar | 3. Uygulama (then) |
|---|---|---|
| **Kullanıcı / grup** (yönetici mi, misafir mi?) | Değerlendirme | **Erişime izin ver** |
| **Konum** (IP aralığı, ülke, bilinen ofis ağı) | | **MFA iste** |
| **Cihaz** (uyumlu mu, kurumsal mı, platform) | | **Uyumlu/kurumsal cihaz** zorunlu tut |
| **Uygulama** (hangi uygulamaya erişiliyor) | | Şifre değişikliği iste, oturum süresini kısalt |
| **Risk** (oturum/kullanıcı riski, P2 ile) | | **Erişimi engelle** |

Örnek politikalar:

- "**Yönetici rolündeki** herkes, her girişte MFA kullanır."
- "**Bilinen ofis IP'leri dışından** giren kullanıcıdan MFA iste."
- "**Uyumsuz** (yönetilmeyen) cihazdan e-postaya erişim yok."
- "Belirli **ülkelerden** gelen tüm girişleri engelle."
- "Riskli oturumda (sızdırılmış kimlik bilgisi şüphesi) şifre değişikliği zorunlu."

> Conditional Access, MFA'yı **her zaman** değil **koşula bağlı** ister: güvenilir durumda kullanıcıyı rahat bırakır, riskli durumda ek kanıt ister. **Microsoft Entra ID P1** (risk tabanlı politikalar için P2) lisansı gerektirir. Ücretsiz alternatif, daha az esnek olan **security defaults**'tur.

Portalda politika oluşturma ekranları ve hazır şablonlar (ör. *Require MFA for administrators*) bu konunun teorisinin birebir karşılığıdır; yukarıdaki sinyal → karar → uygulama tablosunu bilmek sınav için yeterlidir.

### 3.10 Azure role-based access control (RBAC) ⭐

**Azure RBAC**, Azure kaynaklarına **kimin, nerede, ne yapabileceğini** belirler. Bir **rol ataması** (role assignment) üç parçadan oluşur:

```
Rol ataması = Güvenlik sorumlusu (KİM)  +  Rol tanımı (NE)  +  Kapsam (NEREDE)
              kullanıcı, grup,             izin kümesi:          management group,
              service principal,           Owner, Contributor,   subscription,
              managed identity             Reader, …             resource group, kaynak
```

**Built-in roller (sınavın sorduğu dört temel rol):**

| Rol | Kaynakları görüntüleme | Kaynak oluşturma/değiştirme/silme | Erişim (rol atamalarını) yönetme |
|---|---|---|---|
| **Owner** | ✓ | ✓ | ✓ |
| **Contributor** | ✓ | ✓ | ✗ |
| **Reader** | ✓ | ✗ | ✗ |
| **User Access Administrator** | ✓ (yalnızca erişimle ilgili okuma) | ✗ | ✓ |

- Ayrıca **servise özel** built-in roller vardır (Virtual Machine Contributor, Storage Blob Data Reader…) ve ihtiyaca göre **custom roller** tanımlanabilir.
- **Kapsam ve miras:** Bir kapsamda verilen rol, **altındaki tüm kapsamlara miras** kalır. Subscription'da Reader olan kullanıcı, o subscription'daki tüm resource group'ları ve kaynakları okuyabilir (→ C1.4 hiyerarşisi).
- **İzinler toplanır (additive):** Bir kullanıcının subscription'da Reader, bir resource group'ta Contributor rolü varsa, o resource group'ta **Contributor** gibi davranır. *(bağlam)* Açıkça yasaklamak için ayrı bir mekanizma olan **deny assignment** vardır; bunları genellikle platform (ör. managed application) oluşturur.
- **En az yetki ilkesi (least privilege):** İşi yapmaya yetecek **en düşük** rolü, **en dar** kapsamda ver; kişiler yerine **gruplara** ata.
- **Nasıl uygulanır:** Portal, CLI, PowerShell, SDK veya REST'ten gelen her yönetim isteği **Azure Resource Manager**'dan geçer (→ C1.13). ARM, isteği yapanın o kapsamda gereken izne sahip olup olmadığını kontrol eder.

**Karıştırılan üç kavram:**

| | **Azure RBAC** | **Microsoft Entra rolleri** | **Azure Policy** (→ C1.12) |
|---|---|---|---|
| Neyi kontrol eder | **Azure kaynakları** üzerindeki işlemler (VM oluştur, storage sil) | **Directory** üzerindeki işlemler (kullanıcı oluştur, şifre sıfırla, lisans ata) | **Kaynağın özellikleri** (hangi bölge, hangi SKU, zorunlu tag) |
| Örnek rol/kural | Owner, Contributor, Reader | Global Administrator, User Administrator | "Yalnızca West Europe", "tag zorunlu" |
| Kapsam | Management group → subscription → RG → kaynak | Tenant (veya administrative unit) | Management group → subscription → RG |
| Soru | "**Kullanıcı** ne yapabilir?" | "**Directory'de** kim ne yapabilir?" | "**Kaynak** nasıl olmalı?" |

### 3.11 Pratik — kullanıcı, grup ve kapsamlı rol ataması (`az` CLI)

Bu laboratuvar **ücretsizdir**: Entra ID'de kullanıcı/grup oluşturmak ve boş bir resource group'a rol atamak ücret üretmez. Yine de bütçe alarmın kurulu olsun (→ C1.1). Kullanıcı oluşturmak için tenant'ta **User Administrator** (veya Global Administrator), rol atamak için hedef kapsamda **Owner** veya **User Access Administrator** gerekir. En temizi, kendi oluşturduğun bir deneme tenant'ı/aboneliğidir.

```bash
az login
TENANT_DOMAIN=$(az ad signed-in-user show --query userPrincipalName -o tsv | cut -d@ -f2)
echo "$TENANT_DOMAIN"          # ör. contoso.onmicrosoft.com

# 1) İki resource group: yetki yalnızca birinde olacak
az group create --name rg-az900-c19-a --location westeurope
az group create --name rg-az900-c19-b --location westeurope

# 2) Entra ID'de bir kullanıcı ve bir grup
az ad user create \
  --display-name "AZ900 Okuyucu" \
  --user-principal-name "az900reader@$TENANT_DOMAIN" \
  --password "Gecici-Sifre-$RANDOM!aA" \
  --force-change-password-next-sign-in true
az ad group create --display-name grp-az900-readers --mail-nickname grp-az900-readers

USER_ID=$(az ad user show --id "az900reader@$TENANT_DOMAIN" --query id -o tsv)
GROUP_ID=$(az ad group show --group grp-az900-readers --query id -o tsv)
az ad group member add --group grp-az900-readers --member-id "$USER_ID"
az ad group member list --group grp-az900-readers --query "[].userPrincipalName" -o tsv

# 3) Reader rolünü GRUBA, yalnızca rg-az900-c19-a kapsamında ata (en az yetki + gruba atama)
SCOPE_A=$(az group show --name rg-az900-c19-a --query id -o tsv)
az role assignment create --assignee-object-id "$GROUP_ID" --assignee-principal-type Group \
  --role Reader --scope "$SCOPE_A"

# 4) Doğrula: atama yalnızca A kapsamında var, B'de yok
az role assignment list --scope "$SCOPE_A" --output table
az role assignment list --scope "$(az group show --name rg-az900-c19-b --query id -o tsv)" --output table
az role assignment list --all --assignee "$GROUP_ID" --query "[].{rol:roleDefinitionName, kapsam:scope}" -o table

# 5) Rollerin içine bak: Reader yalnızca */read; Contributor her şey ama yetki yönetimi hariç
az role definition list --name Reader      --query "[].permissions[0].actions"    -o tsv
az role definition list --name Contributor --query "[].permissions[0].notActions" -o tsv
az role definition list --name Owner       --query "[].permissions[0].actions"    -o tsv
```

Beklenen gözlem: Kullanıcı (portalda veya `az login` ile, ilk girişte şifre değiştirip MFA kaydı yaparak) `rg-az900-c19-a`'yı ve içini **görür** ama içinde kaynak **oluşturamaz**; `rg-az900-c19-b`'yi ise **hiç görmez**. Contributor'ın `notActions` listesinde `Microsoft.Authorization/*/Write` ve `*/Delete` bulunur: Contributor her şeyi yapar ama **başkasına rol atayamaz**.

**B2B misafir daveti** (isteğe bağlı): `az` CLI'nin doğrudan bir "davet" komutu yoktur. Microsoft Graph'ın `invitations` API'si `az rest` ile çağrılabilir. Bunun için oturumdaki hesabın davet yetkisi (ör. Guest Inviter veya User Administrator rolü) olmalıdır:

```bash
az rest --method POST --url "https://graph.microsoft.com/v1.0/invitations" \
  --headers "Content-Type=application/json" \
  --body '{"invitedUserEmailAddress":"ortak@example.com","inviteRedirectUrl":"https://myapps.microsoft.com","sendInvitationMessage":true}'
# Davet edilen kullanıcı tenant'ta userType=Guest olarak görünür:
az ad user list --filter "userType eq 'Guest'" --query "[].{ad:displayName, upn:userPrincipalName}" -o table
```

**Temizlik:**

```bash
az role assignment delete --assignee "$GROUP_ID" --role Reader --scope "$SCOPE_A"
az ad group delete --group grp-az900-readers
az ad user delete --id "az900reader@$TENANT_DOMAIN"
# (davet ettiysen) az ad user delete --id <misafir kullanıcının id'si>
az group delete --name rg-az900-c19-a --yes --no-wait
az group delete --name rg-az900-c19-b --yes --no-wait
```

> **Alıştırma.** Bir şirkette üç ekip var: **Platform** (tüm subscription'ları yönetir, başkalarına yetki verir), **Geliştiriciler** (yalnızca `rg-app-dev` içinde kaynak açıp siler), **Denetçiler** (tüm subscription'ları yalnızca görüntüler). Her ekip için rol ve kapsamı yaz.
>
> <details><summary>Çözüm</summary>
>
> | Ekip | Rol | Kapsam | Neden |
> |---|---|---|---|
> | Platform | **Owner** | Management group (veya her subscription) | Kaynak yönetimi + rol atama; management group'tan tüm subscription'lara miras |
> | Geliştiriciler | **Contributor** | `rg-app-dev` resource group | Kaynak oluştur/sil, ama yetki dağıtamazlar; kapsam yalnızca o RG |
> | Denetçiler | **Reader** | Management group | Yalnızca görüntüleme, tüm subscription'lara miras |
>
> Hepsi **gruplara** atanır (kişiye değil); ekip değişince yalnızca grup üyeliği değişir.
> </details>

---

## 4. Gerçek dünya senaryosu

Bir sigorta şirketi kimlik altyapısını modernize ediyor:

1. **Hibrit kimlik:** Kurum içindeki AD kullanıcıları **Entra Connect** ile Entra ID'ye senkronize ediliyor. Çalışanlar aynı hesapla hem dosya sunucusuna hem Microsoft 365'e giriyor. Microsoft 365, Salesforce ve iç uygulamalar Entra ID üzerinden **SSO** ile bağlanıyor.
2. **Eski poliçe uygulaması** LDAP ve domain join istiyor. Azure'a taşınırken VM'lere domain controller kurmak yerine **Entra Domain Services** kullanılıyor.
3. **MFA ve passwordless:** Önce tüm çalışanlar Authenticator ile **MFA**'ya kaydediliyor. Yöneticiler ve çağrı merkezinin paylaşılan cihazları için **FIDO2 anahtarları**, ofis çalışanları için **Windows Hello for Business** devreye alınıyor.
4. **Conditional Access (P1):** "Yöneticiler her zaman MFA", "ofis dışından girişte MFA", "yönetilmeyen cihazdan poliçe sistemine erişim yok", "şirketin faaliyet göstermediği ülkelerden giriş engelli" politikaları tanımlanıyor.
5. **External identities:** Acente çalışanları **B2B misafir** olarak davet edilip yalnızca acente portalına yetkilendiriliyor. Sigortalıların kullandığı mobil uygulama için ayrı bir **Entra External ID** (müşteri) tenant'ı kuruluyor ve "Google/Apple ile giriş" ekleniyor.
6. **RBAC:** Azure'da Platform ekibi management group'ta **Owner**, uygulama ekipleri kendi resource group'larında **Contributor**, iç denetim **Reader**. Tüm atamalar gruplara yapılıyor ve üç ayda bir gözden geçiriliyor.

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Bu konu çoğunlukla **senaryo → servis/yöntem** eşleştirmesi ve **evet/hayır** tablolarıyla sorulur. RBAC'ta rol ve kapsam seçimi, Conditional Access'te sinyal/karar mantığı beklenir.

| Soru kalıbı | Doğru cevap | Tuzak |
|---|---|---|
| "Bulut tabanlı kimlik ve erişim yönetimi servisi" | **Microsoft Entra ID** | Entra Domain Services |
| "Eski uygulama domain join / LDAP / Kerberos / Group Policy istiyor, DC yönetmek istemiyoruz" | **Entra Domain Services** | Entra ID (bu protokolleri sunmaz) |
| "Kurum içi AD kullanıcılarını Entra ID'ye senkronize et" | **Entra Connect** (veya Cloud Sync) | Domain Services |
| "Kimliği doğrulama" / "kimliğini kanıtla" | **Authentication** | Authorization |
| "Hangi kaynağa erişebileceğini belirleme" | **Authorization** (RBAC) | Authentication |
| "Tek girişle birçok uygulama" | **SSO** | MFA |
| "Şifre + telefona gelen bildirim" | **MFA** | SSO |
| "Şifre olmadan, biyometri/güvenlik anahtarıyla giriş" | **Passwordless** (Windows Hello, Authenticator, FIDO2) | MFA (passwordless zaten çok faktörlüdür) |
| "İş ortağını kendi hesabıyla misafir olarak davet et" | **B2B** | B2C |
| "Müşterilerin sosyal hesapla uygulamaya kaydolması" | **B2C / Entra External ID** | B2B |
| "Konuma/cihaza/riske göre MFA iste veya engelle" | **Conditional Access** | Security defaults (koşulsuz), RBAC |
| "Kaynakları yalnızca görüntüleme" | **Reader** | Contributor |
| "Her şeyi yönet ama başkasına erişim veremesin" | **Contributor** | Owner |
| "Kaynakları ve erişimi tam yönet" | **Owner** | User Access Administrator (kaynakları yönetemez) |
| "Yalnızca erişim (rol atamaları) yönetsin" | **User Access Administrator** | Owner |
| "Rolü birçok subscription'a birden uygula" | **Management group** kapsamında rol ataması | Her subscription'a tek tek |

**Anahtar kelimeler:** *identity as the primary security perimeter*, *tenant*, *managed domain*, *guest user*, *social identities*, *signals → decisions → enforcement*, *security principal + role definition + scope*, *inherited*, *least privilege*.

**Evet/hayır tuzakları:**
- "RBAC atamaları alt kapsamlara miras kalır" → **Evet**.
- "Entra ID, Kerberos ve LDAP ile domain join sağlar" → **Hayır** (bu Domain Services'tır).
- "Conditional Access, kullanıcının bir resource group'ta hangi işlemleri yapabileceğini belirler" → **Hayır** (bu RBAC'tır; Conditional Access **girişe/erişime** karar verir).
- "Contributor rolü başka kullanıcılara rol atayabilir" → **Hayır**.
- "MFA, iki farklı şifre istemektir" → **Hayır** (iki farklı **faktör türü**).

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "Entra ID = bulutta çalışan Active Directory" | Entra ID modern HTTP protokolleri (OAuth/OIDC/SAML) kullanır; domain join, GPO, Kerberos/LDAP sunmaz. Bunlar için Entra Domain Services |
| "SSO açtık, MFA'ya gerek yok" | SSO giriş sayısını azaltır, MFA kimliği güçlendirir; ikisi birlikte |
| "SMS ile kod = en güvenli MFA" | SMS en zayıf yöntemlerdendir (SIM swap); Authenticator, FIDO2 ve passkey daha güçlüdür |
| "B2B müşteriler içindir" | B2B **iş ortakları**, B2C/External ID **müşteriler** içindir |
| Rolleri kişilere tek tek atamak | Gruplara ata; ayrılan/katılan kişide yalnızca üyelik değişsin |
| Herkese subscription'da Owner vermek | En az yetki: işi yapan en düşük rol, en dar kapsam |
| "Global Administrator otomatik olarak tüm Azure kaynaklarına erişir" | Entra rolleri ile Azure RBAC ayrıdır. Global Admin varsayılan olarak Azure kaynaklarına erişemez; ancak erişimi kendine yükseltebilir (*elevate access*) *(bağlam)* |
| "Conditional Access ücretsiz" | P1 lisansı gerekir; ücretsiz seçenek security defaults |
| "Yeni bir müşteri uygulaması için Azure AD B2C tenant'ı açarım" | Yeni müşterilere B2C satılmıyor (Mayıs 2025); Entra External ID kullanılır |

---

## 7. Performans & trade-off

| Karar | Kazanç | Bedel |
|---|---|---|
| Security defaults | Ücretsiz, tek tıkla temel MFA ve eski protokol engeli | Esneklik yok: istisna, konum/cihaz koşulu tanımlanamaz |
| Conditional Access | Riske göre ayarlı güvenlik; güvenilir durumda kullanıcı rahat | P1/P2 lisansı; yanlış politika herkesi (yöneticiler dahil) dışarıda bırakabilir. Bu yüzden **break-glass** acil durum hesabı tutulur ve politika önce "report-only" modda denenir *(bağlam)* |
| MFA zorunluluğu | Hesap ele geçirmeye karşı çok güçlü koruma | Kullanıcı sürtünmesi, kayıt süreci, yardım masası yükü |
| Passwordless | Daha güvenli ve daha hızlı giriş, şifre sıfırlama talepleri azalır | Cihaz/anahtar dağıtımı ve kayıp anahtar süreci |
| Entra Domain Services | Eski uygulamalar DC yönetmeden buluta | Ücretli servis; tam AD DS esnekliği yok (ör. şema genişletme kısıtlı) |
| Kendi DC'lerin (IaaS VM) | Tam AD kontrolü | Yama, yedek, replikasyon ve güvenlik yükü sende |
| Geniş kapsamlı, yüksek roller | Az sayıda atama, kolay yönetim | Büyük saldırı yüzeyi; tek hesap ele geçirilince tüm ortam risk altında |
| Dar kapsamlı, en az yetkili roller | Etki alanı küçük | Daha çok atama ve gözden geçirme |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe Azure identity, access, and security](https://learn.microsoft.com/en-us/training/modules/describe-azure-identity-access-security/)
- [What is Microsoft Entra ID?](https://learn.microsoft.com/en-us/entra/fundamentals/whatis) · [Microsoft Entra Domain Services overview](https://learn.microsoft.com/en-us/entra/identity/domain-services/overview)
- [Passwordless authentication options](https://learn.microsoft.com/en-us/entra/identity/authentication/concept-authentication-passwordless) · [Security defaults](https://learn.microsoft.com/en-us/entra/fundamentals/security-defaults)
- [Mandatory MFA for Azure](https://learn.microsoft.com/en-us/entra/identity/authentication/concept-mandatory-multifactor-authentication) · [Azure mandatory MFA phase 2 (Azure blog)](https://azure.microsoft.com/en-us/blog/azure-mandatory-multifactor-authentication-phase-2-starting-in-october-2025/)
- [What is Conditional Access?](https://learn.microsoft.com/en-us/entra/identity/conditional-access/overview)
- [External ID overview (B2B, External ID for customers)](https://learn.microsoft.com/en-us/entra/external-id/external-identities-overview) · [Azure AD B2C FAQ (end of sale)](https://learn.microsoft.com/en-us/azure/active-directory-b2c/faq)
- [What is Azure RBAC?](https://learn.microsoft.com/en-us/azure/role-based-access-control/overview) · [Azure built-in roles](https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles) · [Azure roles vs Microsoft Entra roles](https://learn.microsoft.com/en-us/azure/role-based-access-control/rbac-and-directory-admin-roles)
- [`az ad`](https://learn.microsoft.com/en-us/cli/azure/ad) · [`az role assignment`](https://learn.microsoft.com/en-us/cli/azure/role/assignment) · [Microsoft Graph: create invitation](https://learn.microsoft.com/en-us/graph/api/invitation-post)
