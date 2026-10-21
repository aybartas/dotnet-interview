# C1.13 Azure Kaynaklarını Yönetme ve Dağıtma Araçları

> 📁 `06-certifications/01-az-900-azure-fundamentals/13-management-deployment-tools/` · Sınav alanı: **Describe Azure management and governance (%30-35)** · Önkoşul: [C1.4](../04-core-architecture/README.md) · Quiz: [`quiz.md`](quiz.md)
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe features and tools for managing and deploying Azure resources* — **Azure portal** · **Azure Cloud Shell** (Azure CLI ve Azure PowerShell dahil) · **Azure Arc** · **infrastructure as code** · **Azure Resource Manager** ve **ARM template**'leri. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

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

Azure'da bir kaynağı oluşturmanın, değiştirmenin ve silmenin birden çok yolu vardır; hepsi aynı kapıdan — **Azure Resource Manager (ARM)** — geçer:

```
 Azure portal ─┐
 Azure CLI ────┤
 PowerShell ───┼──►  Azure Resource Manager  ──►  kimlik doğrulama + RBAC + Policy + lock + tag
 REST API/SDK ─┤          (tek yönetim katmanı)        ──►  resource provider'lar (Compute, Storage, …)
 ARM/Bicep ────┘
```

| Araç | Ne? | En iyi olduğu iş |
|---|---|---|
| **Azure portal** | Web tabanlı grafik konsol | Keşif, tek seferlik işler, dashboard'lar, öğrenme |
| **Azure Cloud Shell** | Tarayıcıdan açılan, kimliği hazır Bash/PowerShell kabuğu | Kurulumsuz komut satırı, hızlı betikler |
| **Azure CLI** (`az …`) | Platformlar arası komut satırı aracı | Komut satırı otomasyonu (Bash/PowerShell/cmd) |
| **Azure PowerShell** (`*-Az*` cmdlet'leri) | PowerShell modülü | PowerShell ekosisteminde otomasyon |
| **ARM template / Bicep** | Deklaratif **infrastructure as code** | Tekrarlanabilir, tutarlı ortam kurulumu |
| **Azure Arc** | Azure dışındaki kaynakları ARM'a yansıtma | Hybrid ve multicloud yönetişimi |

Konunun kodu yoktur; pratik, README'deki `az` CLI ve Bicep laboratuvarıdır (3.7).

---

## 2. Neden?

- **Sınav:** "Bu iş için hangi araç?" soruları (kurulumsuz komut satırı → Cloud Shell; aynı ortamı tekrar tekrar kur → ARM template; başka buluttaki sunucu → Arc) bu alanın sabitleridir.
- **Gerçek işte:** Portalda tıklanarak kurulan ortam **tekrarlanamaz** ve **denetlenemez**: "Test'te çalışıyordu, üretimde neden farklı?" sorusunun çoğu zaman cevabı elle yapılmış bir ayardır. IaC ortamı kod olarak saklar, gözden geçirir ve aynen tekrarlar.
- **ARM'ı anlamak**, Policy, lock ve RBAC'ın neden **her araçta** aynı şekilde uygulandığını açıklar (→ C1.9, C1.12).

---

## 3. Nasıl?

### 3.1 Azure portal

- Web tabanlı, birleşik konsol (`portal.azure.com`): kaynak oluşturma ve yönetme, maliyet ve izleme ekranları, **özelleştirilebilir dashboard**'lar (→ C1.14), erişilebilirlik desteği.
- Microsoft tarafından sürekli güncellenir; bakım için kesinti gerektirmez. Ayrıca mobil cihazlar için **Azure mobile app** vardır *(bağlam)*.
- Sınır: grafik arayüz **tekrarlanabilir ve otomatik** işler için uygun değildir; yüz ortamı aynı şekilde kurmak için betik veya IaC gerekir.

### 3.2 Azure Cloud Shell, Azure CLI ve Azure PowerShell

**Cloud Shell**, portaldan (üst çubuktaki `>_` simgesi) veya `shell.azure.com`'dan açılan, **tarayıcı tabanlı** ve **kimliği zaten doğrulanmış** etkileşimli bir kabuktur:

- İki deneyim: **Bash** (Azure CLI hazır) ve **PowerShell** (Azure PowerShell hazır); ikisinde de `az` ve `Az` modülü, git, editörler gibi araçlar güncel olarak kurulu gelir.
- **Kurulum gerektirmez**, her cihazdan çalışır; `az login` gerekmez.
- Dosya kalıcılığı isteğe bağlıdır: bir storage account (Azure Files paylaşımı) bağlanırsa `$HOME` oturumlar arasında korunur; **ephemeral session** (storage account olmadan) seçilirse oturum kapanınca dosyalar silinir.

**Azure CLI ↔ Azure PowerShell** — aynı yetenekler, farklı ekosistem; ikisi de Windows, Linux ve macOS'a yerel olarak da kurulabilir:

| İş | Azure CLI | Azure PowerShell |
|---|---|---|
| Giriş | `az login` | `Connect-AzAccount` |
| Resource group oluştur | `az group create --name rg-demo --location westeurope` | `New-AzResourceGroup -Name rg-demo -Location westeurope` |
| Listele | `az group list -o table` | `Get-AzResourceGroup \| Format-Table` |
| Şablon dağıt | `az deployment group create -g rg-demo --template-file main.bicep` | `New-AzResourceGroupDeployment -ResourceGroupName rg-demo -TemplateFile main.bicep` |
| Sil | `az group delete --name rg-demo --yes` | `Remove-AzResourceGroup -Name rg-demo -Force` |

Çıktıyı süzmek: Azure CLI'da `--query` (JMESPath) ve `-o table|tsv|json`; PowerShell'de nesne boru hattı (`Where-Object`, `Select-Object`):

```bash
az vm list --query "[?location=='westeurope'].{ad:name, rg:resourceGroup}" -o table
az group show --name rg-demo --query id -o tsv     # tek değeri değişkene almak için
```

### 3.3 Azure Arc

**Azure Arc**, Azure **dışındaki** kaynakları Azure Resource Manager'a **yansıtır** (her biri Azure'da bir kaynak olarak görünür) ve onları Azure'dan yönetmeyi sağlar.

| Arc ile yönetilebilenler | Örnek |
|---|---|
| **Sunucular** (Windows/Linux, fiziksel veya sanal) | On-prem datacenter'daki sunucular, AWS/GCP'deki VM'ler |
| **Kubernetes kümeleri** | On-prem veya başka buluttaki Kubernetes |
| **SQL Server** ve Azure veri servisleri | On-prem SQL Server envanteri ve lisans yönetimi; Arc-enabled SQL Managed Instance |
| **VM'ler (VMware vSphere, System Center VMM, Azure Local)** | Mevcut sanallaştırma ortamındaki VM'lerin yaşam döngüsü |

Sağladıkları: tek panelde **envanter**, **Azure Policy** ve **RBAC**, tag'ler, **Defender for Cloud** (→ C1.10), **Azure Monitor** (→ C1.14), güncelleme yönetimi. Yani Azure'un yönetişim araçları, kaynak **nerede çalışırsa çalışsın** uygulanabilir hale gelir (hybrid → C1.1).

### 3.4 Azure Resource Manager (ARM) ⭐

**ARM**, Azure'un **dağıtım ve yönetim servisidir**. Portal, CLI, PowerShell, REST API ve SDK'lardan gelen **tüm** istekler ARM'dan geçer; ARM isteği doğrular, **yetkilendirir** (RBAC), **Policy** ve **lock**'ları uygular, sonra ilgili resource provider'a iletir. Bu yüzden hangi araçla yapılırsa yapılsın sonuç ve kurallar **tutarlıdır**.

ARM'ın faydaları:

- Kaynakları tek tek değil, **grup halinde** (resource group) yönetmek, dağıtmak ve silmek.
- **Deklaratif şablonlarla** dağıtım; bağımlılıkları doğru sırayla kurmak (önce VNet, sonra VM).
- Aynı şablonla **tekrar tekrar tutarlı** dağıtım.
- Erişim kontrolü (RBAC), tag, lock ve Policy'nin tek noktadan uygulanması.

### 3.5 Infrastructure as Code (IaC)

**IaC**, altyapıyı **kodla** (metin dosyalarıyla) tanımlayıp dağıtmaktır.

| Fayda | Açıklama |
|---|---|
| **Tekrarlanabilirlik** | Dev, test ve prod aynı şablondan kurulur; "bende çalışıyordu" farkı kalkar |
| **Sürüm kontrolü ve inceleme** | Altyapı değişikliği git'te, pull request ile gözden geçirilir; geçmiş ve geri alma |
| **İdempotentlik** | Aynı şablon tekrar uygulanınca aynı sonuç: eksik olan oluşturulur, değişen güncellenir, aynı olan dokunulmaz |
| **Otomasyon** | CI/CD pipeline'ında otomatik dağıtım (→ J10.5) |

- **Deklaratif** ("**ne** istiyorum": son durumu tanımla, nasıl kurulacağını araç çözsün) ↔ **imperatif** ("**nasıl**": adım adım komutlar — `az` betikleri).
- Azure'un yerel IaC dilleri: **ARM template** (JSON) ve **Bicep**; *(bağlam)* platformlar arası Terraform (→ S8.2).

### 3.6 ARM template'leri ve Bicep

**ARM template**, dağıtılacak kaynakları tanımlayan **JSON** biçimindeki deklaratif dosyadır. Bölümleri:

| Bölüm | Ne? |
|---|---|
| `parameters` | Dağıtımda verilen değerler (ad, bölge, SKU) |
| `variables` | Şablon içinde yeniden kullanılan hesaplanmış değerler |
| `resources` | Oluşturulacak/güncellenecek kaynaklar |
| `outputs` | Dağıtımdan dönen değerler (endpoint, id) |

Nitelikleri: ARM bağımlılıkları kendisi çözer ve bağımsız kaynakları **paralel** dağıtır; dağıtımdan önce **doğrulama** (validation) ve **what-if** (ne değişecek önizlemesi) yapılabilir; şablonlar modüler olarak birbirini çağırabilir; *(bağlam)* **Template Specs** şablonları Azure'da paylaşılabilir kaynak olarak saklar.

**Bicep**, ARM template'lerin daha okunabilir, kısa bir **alana özgü dilidir (DSL)**; **ARM JSON'a derlenir** ve aynı yeteneklere sahiptir. Microsoft yeni şablonlar için Bicep'i öneriyor. Aynı storage account iki biçimde:

```bicep
// storage.bicep
param storageName string
param location string = resourceGroup().location

var skuName = 'Standard_LRS'

resource st 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: storageName
  location: location
  sku: { name: skuName }
  kind: 'StorageV2'
  properties: {
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: false
  }
}

output blobEndpoint string = st.properties.primaryEndpoints.blob
```

```json
{
  "$schema": "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#",
  "contentVersion": "1.0.0.0",
  "parameters": {
    "storageName": { "type": "string" },
    "location": { "type": "string", "defaultValue": "[resourceGroup().location]" }
  },
  "variables": {
    "skuName": "Standard_LRS"
  },
  "resources": [
    {
      "type": "Microsoft.Storage/storageAccounts",
      "apiVersion": "2023-05-01",
      "name": "[parameters('storageName')]",
      "location": "[parameters('location')]",
      "sku": { "name": "[variables('skuName')]" },
      "kind": "StorageV2",
      "properties": {
        "minimumTlsVersion": "TLS1_2",
        "allowBlobPublicAccess": false
      }
    }
  ],
  "outputs": {
    "blobEndpoint": {
      "type": "string",
      "value": "[reference(resourceId('Microsoft.Storage/storageAccounts', parameters('storageName'))).primaryEndpoints.blob]"
    }
  }
}
```

`az bicep build --file storage.bicep` komutu, Bicep dosyasından yukarıdakine eşdeğer ARM JSON'u üretir.

**Portalda şablon üretme (teori).** Portalda tıklayarak yapılan işin ARM JSON karşılığını almak için iki yol vardır:

- **Download a template for automation:** Bir kaynak oluşturma sihirbazının son adımında (**Review + create**), oluşturulmak üzere olan kaynağın şablonunu ve parametre dosyasını verir — *dağıtımdan önce*.
- **Export template:** Var olan bir kaynağın veya resource group'un menüsünden (Automation → **Export template**), mevcut durumun şablonunu verir — *dağıtımdan sonra*.

Dışa aktarılan şablon otomasyon için bir **başlangıç noktasıdır**: sabit kodlanmış adlar ve gereksiz özellikler içerir; parametrelenip temizlenmesi gerekir (bazı kaynak türleri dışa aktarılamayabilir).

### 3.7 Pratik — imperatif CLI ve deklaratif Bicep (`az` CLI)

Maliyet: App Service planı **F1 (ücretsiz)** katmanında, storage account boş — maliyet kuruşlar düzeyinde. Bütçe alarmın kurulu olsun (→ C1.1).

**(a) İmperatif: adım adım CLI ile oluştur ve sil**

```bash
RG=rg-az900-c113
SUFFIX=$RANDOM
az group create --name $RG --location westeurope
az storage account create --name stc113$SUFFIX --resource-group $RG --sku Standard_LRS
az appservice plan create --name plan-c113 --resource-group $RG --sku F1 --is-linux
az webapp create --name app-c113-$SUFFIX --resource-group $RG --plan plan-c113 \
  --runtime "DOTNETCORE:10.0"                       # geçerli değerler: az webapp list-runtimes --os linux
az resource list --resource-group $RG --query "[].{ad:name, tur:type}" -o table
az group delete --name $RG --yes                    # dört kaynak tek komutla gider
```

**(b) Deklaratif: Bicep ile dağıt, ikinci dağıtımda değişiklik olmadığını gör**

```bash
RG=rg-az900-c113-iac
ST=stc113iac$RANDOM
az group create --name $RG --location westeurope

# 3.6'daki Bicep içeriğini dosyaya yaz (Cloud Shell'de: code storage.bicep ile de açabilirsin)
cat > storage.bicep <<'EOF'
param storageName string
param location string = resourceGroup().location
var skuName = 'Standard_LRS'
resource st 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: storageName
  location: location
  sku: { name: skuName }
  kind: 'StorageV2'
  properties: { minimumTlsVersion: 'TLS1_2', allowBlobPublicAccess: false }
}
output blobEndpoint string = st.properties.primaryEndpoints.blob
EOF

# 1) İlk dağıtımdan önce önizleme → "+ Create" (1 kaynak oluşturulacak)
az deployment group what-if --resource-group $RG --template-file storage.bicep --parameters storageName=$ST

# 2) Dağıt ve çıktıyı oku
az deployment group create --resource-group $RG --template-file storage.bicep \
  --parameters storageName=$ST --query properties.outputs.blobEndpoint.value -o tsv

# 3) Aynı şablonu tekrar önizle → "= Nochange": idempotent, hiçbir şey yeniden oluşturulmaz
az deployment group what-if --resource-group $RG --template-file storage.bicep --parameters storageName=$ST

# 4) Şablonda bir değeri değiştir (ör. allowBlobPublicAccess: true) → what-if "~ Modify" gösterir; geri al

# 5) Bicep → ARM JSON
az bicep build --file storage.bicep --stdout | head -40

# Temizlik
az group delete --name $RG --yes --no-wait
rm storage.bicep
```

> what-if çıktısında bazı salt okunur özellikler zaman zaman "değişecek" gibi görünebilir (*noise*); bu kaynakların yeniden oluşturulacağı anlamına gelmez.

> **Alıştırma.** Her ihtiyaç için aracı seç: portal · Cloud Shell · Azure CLI/PowerShell betiği · ARM template/Bicep · Azure Arc.
> 1. Yeni katılan bir stajyer Azure'u ilk kez keşfedecek.
> 2. Kişisel bilgisayarına hiçbir şey kuramayan bir yönetici, tarayıcıdan acil bir komut çalıştıracak.
> 3. Her yeni müşteri için aynı 12 kaynaklık ortam, sürüm kontrolünde ve aynı şekilde kurulacak.
> 4. Şirketin kendi datacenter'ındaki 300 Linux sunucusuna Azure Policy ve Defender for Cloud uygulanacak.
>
> <details><summary>Çözüm</summary>
>
> 1. **Azure portal** — grafik, keşfetmeye uygun.
> 2. **Cloud Shell** — kurulumsuz, tarayıcıdan, kimliği hazır.
> 3. **ARM template / Bicep** (IaC) — deklaratif, tekrarlanabilir, git'te sürümlenir; CI/CD ile otomatikleşir.
> 4. **Azure Arc** — sunucular Arc-enabled server olarak ARM'a yansıtılır; Policy ve Defender Azure kaynağıymış gibi uygulanır.
> </details>

---

## 4. Gerçek dünya senaryosu

Bir SaaS şirketi her yeni kurumsal müşteri için ayrı bir ortam (VNet, App Service, Azure SQL, Key Vault, storage, izleme) kuruyor:

1. **Başlangıç:** İlk müşteri ortamı portaldan elle kuruldu; ikincide bir ayar unutuldu ve güvenlik denetiminde açık çıktı.
2. **Şablona geçiş:** Mühendis, ilk ortamın resource group'undan **Export template** ile şablonu aldı, parametreleyip temizledi ve **Bicep**'e çevirdi; dosyalar git deposuna kondu, her değişiklik pull request ile inceleniyor.
3. **Dağıtım:** Pipeline her müşteri için `az deployment group what-if` ile değişikliği önizliyor, onaydan sonra `az deployment group create` çalıştırıyor. Mevcut ortamlara aynı şablon tekrar uygulandığında yalnızca **farklar** değişiyor (idempotent).
4. **Günlük iş:** Destek ekibi küçük sorgular için **Cloud Shell** kullanıyor; kimse dizüstüne araç kurmak zorunda değil.
5. **Hybrid:** Şirketin hâlâ on-prem'de duran lisans sunucuları **Azure Arc** ile Azure'a bağlandı; Azure'daki tag, Policy ve izleme kuralları onlara da uygulanıyor.

---

## 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** "Hangi araç?" senaryoları ve ARM/IaC nitelikleri hakkında evet/hayır.

| Senaryodaki ifade | Cevap |
|---|---|
| "Aynı ortamı **tekrar tekrar tutarlı** kur", "altyapıyı kod olarak sürümle" | **ARM template / Bicep** (IaC) |
| "**Kurulum yapmadan**, tarayıcıdan komut satırı" | **Azure Cloud Shell** |
| "Hem **Bash** hem **PowerShell** seçeneği sunan tarayıcı kabuğu" | **Cloud Shell** |
| "**Grafik** arayüz", "özelleştirilebilir dashboard" | **Azure portal** |
| "**On-prem / başka buluttaki** sunucuyu Azure'dan yönet, Policy uygula" | **Azure Arc** |
| "Tüm yönetim isteklerinin geçtiği **katman**" | **Azure Resource Manager** |
| "ARM template **hangi biçimde**?" | **JSON** (deklaratif) |
| "Bicep nedir?" | ARM JSON'a derlenen, daha okunabilir DSL |
| "Şablonu dağıtmadan **ne değişeceğini** gör" | **what-if** |

**Tuzaklar:**
- "Azure CLI yalnızca Linux'ta çalışır" → **Hayır** (Windows, macOS, Linux, Cloud Shell).
- "Azure PowerShell, Azure CLI'ın yapamadığı işleri yapar" → genellikle **Hayır**; yetenekler büyük ölçüde aynıdır, sözdizimi farklıdır.
- "ARM template imperatiftir" → **Hayır**, **deklaratiftir** (son durumu tanımlar).
- "Portal, CLI ve PowerShell farklı API'ler kullanır, kurallar farklı uygulanır" → **Hayır**; hepsi ARM'dan geçer, RBAC/Policy/lock aynı uygulanır.
- "Azure Arc, on-prem sunucuları Azure'a **taşır**" → **Hayır**; sunucu yerinde kalır, yalnızca Azure'dan **yönetilir** (taşıma → Azure Migrate, C1.8).

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Üretim ortamını portaldan elle kurmak | Elle kurulum tekrarlanamaz ve denetlenemez; IaC kullan |
| "Bicep ayrı bir servis/runtime" | Bicep yalnızca bir dildir; ARM JSON'a derlenir, dağıtımı yine ARM yapar |
| Export edilen şablonu olduğu gibi kullanmak | Sabit adlar/gereksiz özellikler içerir; parametrele ve temizle |
| CLI betiğinin idempotent olduğunu sanmak | İmperatif betik ikinci çalıştırmada "zaten var" hatası verebilir; deklaratif şablon son durumu sağlar |
| Ephemeral Cloud Shell'de dosya saklamak | Oturum kapanınca silinir; kalıcılık için storage bağla, secret'ları Key Vault'ta tut |
| Arc'ı taşıma aracı sanmak | Arc **yönetim** sağlar; taşıma için Azure Migrate |
| what-if çıktısındaki her farkı gerçek değişiklik sanmak | Bazı salt okunur özellikler "noise" olarak görünebilir |

---

## 7. Performans & trade-off

| Seçim | Kazanç | Bedel |
|---|---|---|
| Portal | Öğrenmesi en kolay, görsel | Tekrarlanamaz, ölçeklenmez, hata yapmaya açık |
| CLI/PowerShell betiği (imperatif) | Hızlı otomasyon, esnek mantık | Durum takibi ve idempotentlik betiği yazana kalır |
| ARM JSON | Tam yetenek, her araç destekler | Uzun, okunması/yazılması zor |
| Bicep | Kısa, okunabilir, modüler, aynı yetenek | Ek araç (Bicep CLI — `az` içinde gelir); yalnızca Azure |
| *(bağlam)* Terraform | Çok bulutlu, geniş ekosistem | Ayrı durum (state) dosyası yönetimi (→ S8.2) |
| Azure Arc | Azure yönetişimi her yerde | Ajan kurulumu, bağlantı gereksinimi; bazı Arc özellikleri ücretli |
| Cloud Shell | Kurulumsuz, kimlik hazır | Oturum zaman aşımı; ephemeral modda dosyalar kalıcı değil |

---

## 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe features and tools for managing and deploying Azure resources](https://learn.microsoft.com/en-us/training/modules/describe-features-tools-manage-deploy-azure-resources/)
- [What is Azure Resource Manager?](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/overview) · [ARM templates overview](https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/overview) · [What is Bicep?](https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/overview)
- [ARM template deployment what-if](https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-what-if) · [Export template in the portal](https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/export-template-portal)
- [Azure Cloud Shell features](https://learn.microsoft.com/azure/cloud-shell/features) · [Cloud Shell ephemeral sessions](https://learn.microsoft.com/azure/cloud-shell/get-started/ephemeral)
- [Azure Arc overview](https://learn.microsoft.com/en-us/azure/azure-arc/overview)
- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/) · [Azure PowerShell](https://learn.microsoft.com/en-us/powershell/azure/)
