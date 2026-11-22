[← AZ-900 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../3.2-governance-compliance/README.md)

## 3.3 Kaynak Yönetme ve Dağıtma Araçları: Portal, Cloud Shell, CLI, PowerShell, Arc, ARM Template'leri

> **Sınav Alanı:** Describe Azure management and governance (%30-35) · **Konu Kodu:** C1.13
>
> 📌 **Resmî hedef (skills measured as of July 20, 2026):** *Describe features and tools for managing and deploying Azure resources* — **Azure portal** · **Azure Cloud Shell** (Azure CLI ve Azure PowerShell dahil) · **Azure Arc** · **infrastructure as code** · **Azure Resource Manager** ve **ARM template**'leri. Sınavdan önce resmî [study guide](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)'ı kontrol et.

### 1. Ne?

Azure'da bir kaynağı oluşturmanın, değiştirmenin ve silmenin birden çok yolu vardır; hepsi aynı kapıdan — **Azure Resource Manager (ARM)** — geçer:

```
 Azure portal ─┐
 Azure CLI ────┤
 PowerShell ───┼──►  Azure Resource Manager  ──►  authentication + RBAC + Policy + lock + tag
 REST API/SDK ─┤          (single management layer)    ──►  resource providers (Compute, Storage, …)
 ARM template ─┘
```

- **Azure portal**
  - Ne?: Web tabanlı grafik konsol
  - En iyi olduğu iş: Keşif, tek seferlik işler, izleme, öğrenme
- **Azure Cloud Shell**
  - Ne?: Tarayıcıdan açılan, kimliği hazır Bash/PowerShell kabuğu
  - En iyi olduğu iş: Kurulumsuz komut satırı, hızlı betikler
- **Azure CLI** (`az …`)
  - Ne?: Platformlar arası komut satırı aracı
  - En iyi olduğu iş: Komut satırı otomasyonu (Bash/PowerShell/cmd)
- **Azure PowerShell** (`*-Az*` cmdlet'leri)
  - Ne?: PowerShell modülü
  - En iyi olduğu iş: PowerShell ekosisteminde otomasyon
- **ARM template**
  - Ne?: JSON biçimli, deklaratif **infrastructure as code**
  - En iyi olduğu iş: Tekrarlanabilir, tutarlı ortam kurulumu
- **Azure Arc**
  - Ne?: Azure dışındaki kaynakları ARM'a yansıtma
  - En iyi olduğu iş: Hybrid ve multicloud yönetimi

Konunun kodu yok; pratik, README'deki `az` CLI ve ARM template laboratuvarıdır (3.7).

---

### 2. Neden?

- **Sınav:** "Bu iş için hangi araç?" soruları (kurulumsuz komut satırı → Cloud Shell; aynı ortamı tekrar tekrar kur → ARM template; başka buluttaki sunucu → Arc) bu alanın sabitleridir.
- **Gerçek işte:** Portalda tıklanarak kurulan ortam **tekrarlanamaz** ve **denetlenemez**: "Test'te çalışıyordu, üretimde neden farklı?" sorusunun çoğu zaman cevabı elle yapılmış bir ayardır. IaC ortamı kod olarak saklar, gözden geçirir ve aynen tekrarlar.
- **ARM'ı anlamak**, Policy, lock ve RBAC'ın neden **her araçta** aynı şekilde uygulandığını açıklar (→ C1.9, C1.12).

---

### 3. Nasıl?

#### 3.1 Azure portal

- Web tabanlı, birleşik konsol (`portal.azure.com`): kaynak oluşturma ve yönetme, dağıtım ve yapılandırma, **izleme** ve maliyet ekranları, **özelleştirilebilir dashboard**'lar, erişilebilirlik desteği.
- Microsoft tarafından sürekli güncellenir; bakım için kesinti gerektirmez.
- Sınır: grafik arayüz **tekrarlanabilir ve otomatik** işler için uygun değildir; yüz ortamı aynı şekilde kurmak için betik veya IaC gerekir.

#### 3.2 Azure Cloud Shell, Azure CLI ve Azure PowerShell

**Cloud Shell**, portaldan (üst çubuktaki `>_` simgesi) veya `shell.azure.com`'dan açılan, **tarayıcı tabanlı** ve **kimliği zaten doğrulanmış** etkileşimli bir kabuktur:

- İki deneyim: **Bash** (Azure CLI hazır) ve **PowerShell** (Azure PowerShell hazır); ikisinde de Azure CLI ve Azure PowerShell modülü, git, editörler gibi araçlar güncel olarak kurulu gelir.
- **Kurulum gerektirmez**, her cihazdan çalışır; `az login` gerekmez.
- Dosya kalıcılığı isteğe bağlıdır: bir storage account (Azure Files paylaşımı) bağlanırsa `$HOME` oturumlar arasında korunur; storage bağlanmazsa (**ephemeral**) oturum kapanınca dosyalar silinir.

**Azure CLI ↔ Azure PowerShell** — aynı yetenekler, farklı ekosistem; ikisi de Windows, Linux ve macOS'a yerel olarak da kurulabilir. CLI komutları `az` ile, PowerShell **cmdlet**'leri `Verb-AzNoun` biçiminde başlar:

- **Giriş** — Azure CLI: `az login` · Azure PowerShell: `Connect-AzAccount`
- **Resource group oluştur**
  - Azure CLI: `az group create --name rg-demo --location westeurope`
  - Azure PowerShell: `New-AzResourceGroup -Name rg-demo -Location westeurope`
- **Listele**
  - Azure CLI: `az group list -o table`
  - Azure PowerShell: `Get-AzResourceGroup | Format-Table`
- **Şablon dağıt**
  - Azure CLI: `az deployment group create -g rg-demo --template-file main.json`
  - Azure PowerShell: `New-AzResourceGroupDeployment -ResourceGroupName rg-demo -TemplateFile main.json`
- **Sil**
  - Azure CLI: `az group delete --name rg-demo --yes`
  - Azure PowerShell: `Remove-AzResourceGroup -Name rg-demo -Force`

| | Azure CLI | Azure PowerShell |
|---|---|---|
| Komut biçimi | `az <grup> <işlem>` | `Verb-AzNoun` cmdlet'leri |
| Çıktı | Varsayılan JSON (`-o table/tsv`) | **Nesne** (boru hattıyla işlenir) |
| Platform | Windows / Linux / macOS / Cloud Shell | Windows / Linux / macOS / Cloud Shell |
| Tercih edilen kitle | Bash/cmd alışkanlığı olanlar | PowerShell alışkanlığı olanlar |

Çıktıyı süzmek: Azure CLI'da `--query` (JMESPath) ve `-o table|tsv|json`; PowerShell'de nesne boru hattı (`Where-Object`, `Select-Object`):

```bash
az vm list --query "[?location=='westeurope'].{name:name, rg:resourceGroup}" -o table
az group show --name rg-demo --query id -o tsv     # to capture a single value into a variable
```

#### 3.3 Azure Arc

**Azure Arc**, Azure **dışındaki** kaynakları Azure Resource Manager'a **yansıtır** (her biri Azure'da bir kaynak olarak görünür) ve onları Azure'dan yönetmeyi sağlar.

| Arc ile yönetilebilenler | Örnek |
|---|---|
| **Sunucular** (Windows/Linux, fiziksel veya sanal) | On-prem datacenter'daki sunucular, AWS/GCP'deki VM'ler |
| **Kubernetes kümeleri** | On-prem veya başka buluttaki Kubernetes |
| **Veri servisleri** | On-prem SQL Server envanteri |

Sağladıkları: tek panelde **envanter**, **Azure Policy** ve **RBAC**, tag'ler, **Defender for Cloud** (→ C1.10), **Azure Monitor** (→ C1.14). Yani Azure'un yönetim ve yönetişim araçları, kaynak **nerede çalışırsa çalışsın** (on-prem, başka bulut) uygulanabilir hale gelir (hybrid / multicloud → C1.1).

#### 3.4 Azure Resource Manager (ARM) ⭐

**ARM**, Azure'un **dağıtım ve yönetim servisidir**. Portal, CLI, PowerShell, REST API ve SDK'lardan gelen **tüm** istekler ARM'dan geçer; ARM isteği doğrular, **yetkilendirir** (RBAC), **Policy** ve **lock**'ları uygular, sonra ilgili resource provider'a iletir. Bu yüzden hangi araçla yapılırsa yapılsın sonuç ve kurallar **tutarlıdır**.

ARM'ın faydaları:

- Kaynakları tek tek değil, **grup halinde** (resource group) yönetmek, dağıtmak ve silmek.
- **Deklaratif şablonlarla** dağıtım; bağımlılıkları doğru sırayla kurmak (önce VNet, sonra VM).
- Aynı şablonla **tekrar tekrar tutarlı** dağıtım.
- Erişim kontrolü (RBAC), tag, lock ve Policy'nin tek noktadan uygulanması.

> **ARM ↔ ARM template — sık karıştırılır:** **ARM** bir **servistir** (yönetim/dağıtım katmanı, her istek ondan geçer). **ARM template** ise ARM'a gönderilen bir **dosyadır** (JSON): hangi kaynakların kurulacağını tarif eder. Template olmadan da ARM kullanırsın (portal, CLI); ama template, ARM'ın deklaratif dağıtım yeteneğine verilen girdidir.

**Dağıtım kapsamları (deployment scopes):** Bir dağıtım (şablon) şu kapsamlardan birine uygulanır: **resource group** (en yaygın), **subscription**, **management group** veya **tenant**. Örneğin resource group oluşturmak veya subscription düzeyinde politika atamak için şablon, resource group'tan bir üst kapsama dağıtılır (→ C1.4).

#### 3.5 Infrastructure as Code (IaC)

**IaC**, altyapıyı **kodla** (metin dosyalarıyla) tanımlayıp dağıtmaktır.

| Fayda | Açıklama |
|---|---|
| **Tekrarlanabilirlik** | Dev, test ve prod aynı şablondan kurulur; "bende çalışıyordu" farkı kalkar |
| **Sürüm kontrolü ve inceleme** | Altyapı değişikliği sürüm kontrolünde tutulur ve gözden geçirilir; geçmiş ve geri alma |
| **İdempotentlik** | Aynı şablon tekrar uygulanınca aynı sonuç: eksik olan oluşturulur, değişen güncellenir, aynı olan dokunulmaz |
| **Otomasyon** | Dağıtım betiklerle veya pipeline'larla otomatik yapılır; insan hatası azalır |

- **Deklaratif** ("**ne** istiyorum": son durumu tanımla, nasıl kurulacağını araç çözsün — **ARM template**) ↔ **imperatif** ("**nasıl**": adım adım komutlar — `az` / PowerShell betikleri).
- Azure'un yerel IaC biçimi **ARM template**'tir; aynı şablon tekrar tekrar dağıtılabilir (**repeatable deployment**) ve kaynak sağlama süreci **otomatiktir** (**automated provisioning**).

#### 3.6 ARM template'leri

**ARM template**, dağıtılacak kaynakları tanımlayan **JSON** biçimindeki deklaratif dosyadır. Bölümleri:

| Bölüm | Ne? |
|---|---|
| `parameters` | Dağıtımda verilen değerler (ad, bölge, SKU) |
| `variables` | Şablon içinde yeniden kullanılan hesaplanmış değerler |
| `resources` | Oluşturulacak/güncellenecek kaynaklar |
| `outputs` | Dağıtımdan dönen değerler (endpoint, id) |

Bir storage account'u tanımlayan örnek şablon:

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

**Kaynak bağımlılıkları (resource dependencies):** Bazı kaynaklar diğerlerine bağlıdır (web uygulaması önce App Service plan'ı ister; VM önce VNet ve NIC'i ister). Şablonda bu ilişki `dependsOn` ile (veya bir kaynağın diğerine referansıyla) belirtilir; ARM bağımlılıkları **doğru sırayla** kurar, birbirinden bağımsız kaynakları ise **paralel** dağıtır:

```json
{
  "type": "Microsoft.Web/sites",
  "apiVersion": "2023-12-01",
  "name": "[parameters('appName')]",
  "location": "[parameters('location')]",
  "dependsOn": [ "[resourceId('Microsoft.Web/serverfarms', parameters('planName'))]" ],
  "properties": { "serverFarmId": "[resourceId('Microsoft.Web/serverfarms', parameters('planName'))]" }
}
```

Diğer nitelikler: dağıtımdan önce **doğrulama** (validate) ve **what-if** (ne değişecek önizlemesi) yapılabilir; aynı şablon tekrar uygulandığında yalnızca **farklar** değişir (idempotent); şablonlar aynı **deployment scope** kurallarıyla (resource group → subscription → management group) dağıtılır.

**Portalda şablon üretme (teori).** Portalda tıklayarak yapılan işin ARM JSON karşılığını almak için iki yol vardır:

- **Download a template for automation:** Bir kaynak oluşturma sihirbazının son adımında (**Review + create**), oluşturulmak üzere olan kaynağın şablonunu verir — *dağıtımdan önce*.
- **Export template:** Var olan bir kaynağın veya resource group'un menüsünden (Automation → **Export template**), mevcut durumun şablonunu verir — *dağıtımdan sonra*.

Dışa aktarılan şablon otomasyon için bir **başlangıç noktasıdır**: sabit kodlanmış adlar ve gereksiz özellikler içerir; parametrelenip temizlenmesi gerekir.

#### 3.7 Pratik — imperatif CLI ve deklaratif ARM template (`az` CLI)

Maliyet: App Service planı **F1 (ücretsiz)** katmanında, storage account boş — maliyet kuruşlar düzeyinde. Bütçe alarmın kurulu olsun (→ C1.1).

**(a) İmperatif: adım adım CLI ile oluştur ve sil**

```bash
RG=rg-az900-c113
SUFFIX=$RANDOM
az group create --name $RG --location westeurope
az storage account create --name stc113$SUFFIX --resource-group $RG --sku Standard_LRS
az appservice plan create --name plan-c113 --resource-group $RG --sku F1 --is-linux
az webapp create --name app-c113-$SUFFIX --resource-group $RG --plan plan-c113 \
  --runtime "DOTNETCORE:10.0"                       # valid values: az webapp list-runtimes --os linux
az resource list --resource-group $RG --query "[].{name:name, type:type}" -o table
az group delete --name $RG --yes                    # all three resources go with one command
```

**(b) Deklaratif: ARM template ile dağıt, ikinci dağıtımda değişiklik olmadığını gör**

```bash
RG=rg-az900-c113-iac
ST=stc113iac$RANDOM
az group create --name $RG --location westeurope

# Write the template from 3.6 to a file (in Cloud Shell you can also open it with: code storage.json)
cat > storage.json <<'EOF'
{
  "$schema": "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#",
  "contentVersion": "1.0.0.0",
  "parameters": {
    "storageName": { "type": "string" },
    "location": { "type": "string", "defaultValue": "[resourceGroup().location]" }
  },
  "variables": { "skuName": "Standard_LRS" },
  "resources": [
    {
      "type": "Microsoft.Storage/storageAccounts",
      "apiVersion": "2023-05-01",
      "name": "[parameters('storageName')]",
      "location": "[parameters('location')]",
      "sku": { "name": "[variables('skuName')]" },
      "kind": "StorageV2",
      "properties": { "minimumTlsVersion": "TLS1_2", "allowBlobPublicAccess": false }
    }
  ],
  "outputs": {
    "blobEndpoint": {
      "type": "string",
      "value": "[reference(resourceId('Microsoft.Storage/storageAccounts', parameters('storageName'))).primaryEndpoints.blob]"
    }
  }
}
EOF

# 1) Validate the template, then preview → "+ Create" (1 resource will be created)
az deployment group validate --resource-group $RG --template-file storage.json --parameters storageName=$ST
az deployment group what-if --resource-group $RG --template-file storage.json --parameters storageName=$ST

# 2) Deploy and read the output
az deployment group create --resource-group $RG --template-file storage.json \
  --parameters storageName=$ST --query properties.outputs.blobEndpoint.value -o tsv

# 3) Preview the same template again → "= Nochange": idempotent, nothing is recreated
az deployment group what-if --resource-group $RG --template-file storage.json --parameters storageName=$ST

# 4) Change a value in the template (e.g. allowBlobPublicAccess: true) → what-if shows "~ Modify"; revert it

# Cleanup
az group delete --name $RG --yes --no-wait
rm storage.json
```

> what-if çıktısında bazı salt okunur özellikler zaman zaman "değişecek" gibi görünebilir (*noise*); bu kaynakların yeniden oluşturulacağı anlamına gelmez.

> **Alıştırma.** Her ihtiyaç için aracı seç: portal · Cloud Shell · Azure CLI/PowerShell betiği · ARM template · Azure Arc.
> 1. Yeni katılan bir stajyer Azure'u ilk kez keşfedecek.
> 2. Kişisel bilgisayarına hiçbir şey kuramayan bir yönetici, tarayıcıdan acil bir komut çalıştıracak.
> 3. Her yeni müşteri için aynı 12 kaynaklık ortam, sürüm kontrolünde ve aynı şekilde kurulacak.
> 4. Şirketin kendi datacenter'ındaki 300 Linux sunucusuna Azure Policy ve Defender for Cloud uygulanacak.
>
> <details><summary>Çözüm</summary>
>
> 1. **Azure portal** — grafik, keşfetmeye uygun.
> 2. **Cloud Shell** — kurulumsuz, tarayıcıdan, kimliği hazır.
> 3. **ARM template** (IaC) — deklaratif, tekrarlanabilir, sürüm kontrolünde tutulur; otomatikleştirilebilir.
> 4. **Azure Arc** — sunucular Arc-enabled server olarak ARM'a yansıtılır; Policy ve Defender Azure kaynağıymış gibi uygulanır.
> </details>

---

### 4. Gerçek dünya senaryosu

Bir SaaS şirketi her yeni kurumsal müşteri için ayrı bir ortam (VNet, App Service, veritabanı, storage, izleme) kuruyor:

1. **Başlangıç:** İlk müşteri ortamı portaldan elle kuruldu; ikincide bir ayar unutuldu ve güvenlik denetiminde açık çıktı.
2. **Şablona geçiş:** Mühendis, ilk ortamın resource group'undan **Export template** ile şablonu aldı, parametreleyip temizledi; dosyalar sürüm kontrolüne kondu, her değişiklik gözden geçiriliyor.
3. **Dağıtım:** Her müşteri için önce `az deployment group what-if` ile değişiklik önizleniyor, onaydan sonra `az deployment group create` çalıştırılıyor. Mevcut ortamlara aynı şablon tekrar uygulandığında yalnızca **farklar** değişiyor (idempotent).
4. **Günlük iş:** Destek ekibi küçük sorgular için **Cloud Shell** kullanıyor; kimse dizüstüne araç kurmak zorunda değil.
5. **Hybrid:** Şirketin hâlâ on-prem'de duran lisans sunucuları **Azure Arc** ile Azure'a bağlandı; Azure'daki tag, Policy ve izleme kuralları onlara da uygulanıyor.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** "Hangi araç?" senaryoları ve ARM/IaC nitelikleri hakkında evet/hayır.

| Senaryodaki ifade | Cevap |
|---|---|
| "Aynı ortamı **tekrar tekrar tutarlı** kur", "altyapıyı kod olarak tanımla" | **ARM template** (IaC) |
| "**Kurulum yapmadan**, tarayıcıdan komut satırı" | **Azure Cloud Shell** |
| "Hem **Bash** hem **PowerShell** seçeneği sunan tarayıcı kabuğu" | **Cloud Shell** |
| "**Grafik** arayüz", "web tabanlı yönetim" | **Azure portal** |
| "**On-prem / başka buluttaki** sunucuyu Azure'dan yönet, Policy uygula" | **Azure Arc** |
| "Tüm yönetim isteklerinin geçtiği **katman**" | **Azure Resource Manager** |
| "ARM template **hangi biçimde**?" | **JSON** (deklaratif) |
| "Bir kaynağın başka bir kaynaktan **önce** kurulması gerekiyor" | Şablonda **dependsOn** / kaynak bağımlılığı |
| "Şablonu dağıtmadan **ne değişeceğini** gör" | **what-if** |
| "PowerShell **cmdlet**'leri" ↔ "`az` komutları" | **Azure PowerShell** ↔ **Azure CLI** |

**Tuzaklar:**
- "Azure CLI yalnızca Linux'ta çalışır" → **Hayır** (Windows, macOS, Linux, Cloud Shell).
- "Azure PowerShell, Azure CLI'ın yapamadığı işleri yapar" → genellikle **Hayır**; yetenekler büyük ölçüde aynıdır, sözdizimi farklıdır.
- "ARM template imperatiftir" → **Hayır**, **deklaratiftir** (son durumu tanımlar).
- "ARM ve ARM template aynı şeydir" → **Hayır**: ARM servis, template dosya.
- "Portal, CLI ve PowerShell farklı API'ler kullanır, kurallar farklı uygulanır" → **Hayır**; hepsi ARM'dan geçer, RBAC/Policy/lock aynı uygulanır.
- "Azure Arc, on-prem sunucuları Azure'a **taşır**" → **Hayır**; sunucu yerinde kalır, yalnızca Azure'dan **yönetilir** (taşıma → Azure Migrate, C1.8).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| Üretim ortamını portaldan elle kurmak | Elle kurulum tekrarlanamaz ve denetlenemez; IaC kullan |
| "ARM = ARM template" | ARM yönetim/dağıtım servisidir; ARM template ona verilen JSON dosyasıdır |
| Export edilen şablonu olduğu gibi kullanmak | Sabit adlar/gereksiz özellikler içerir; parametrele ve temizle |
| CLI betiğinin idempotent olduğunu sanmak | İmperatif betik ikinci çalıştırmada "zaten var" hatası verebilir; deklaratif şablon son durumu sağlar |
| Ephemeral Cloud Shell'de dosya saklamak | Oturum kapanınca silinir; kalıcılık için storage bağla |
| Arc'ı taşıma aracı sanmak | Arc **yönetim** sağlar; taşıma için Azure Migrate |
| what-if çıktısındaki her farkı gerçek değişiklik sanmak | Bazı salt okunur özellikler "noise" olarak görünebilir |

---

### 7. Performans & trade-off

- **Portal**
  - Kazanç: Öğrenmesi en kolay, görsel
  - Bedel: Tekrarlanamaz, ölçeklenmez, hata yapmaya açık
- **CLI/PowerShell betiği (imperatif)**
  - Kazanç: Hızlı otomasyon, esnek mantık
  - Bedel: Durum takibi ve idempotentlik betiği yazana kalır
- **ARM template (deklaratif IaC)**
  - Kazanç: Tekrarlanabilir, doğrulanabilir (validate/what-if), bağımlılıkları ARM çözer
  - Bedel: JSON uzun ve yazması zahmetli olabilir; yalnızca Azure kaynaklarını tanımlar
- **Azure Arc**
  - Kazanç: Azure yönetişimi her yerde
  - Bedel: Ajan kurulumu, bağlantı gereksinimi; bazı Arc özellikleri ücretli
- **Cloud Shell**
  - Kazanç: Kurulumsuz, kimlik hazır
  - Bedel: Oturum zaman aşımı; ephemeral modda dosyalar kalıcı değil

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [Microsoft Learn — Describe features and tools for managing and deploying Azure resources](https://learn.microsoft.com/en-us/training/modules/describe-features-tools-manage-deploy-azure-resources/)
- [What is Azure Resource Manager?](https://learn.microsoft.com/en-us/azure/azure-resource-manager/management/overview) · [ARM templates overview](https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/overview) · [Define the order for deploying resources (dependsOn)](https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/resource-dependency)
- [ARM template deployment what-if](https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/deploy-what-if) · [Export template in the portal](https://learn.microsoft.com/en-us/azure/azure-resource-manager/templates/export-template-portal)
- [Azure Cloud Shell features](https://learn.microsoft.com/azure/cloud-shell/features)
- [Azure Arc overview](https://learn.microsoft.com/en-us/azure/azure-arc/overview)
- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/) · [Azure PowerShell](https://learn.microsoft.com/en-us/powershell/azure/)

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
