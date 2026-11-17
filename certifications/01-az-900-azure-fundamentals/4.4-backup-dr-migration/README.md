[← AZ-900 İçindekiler](../README.md) · [📝 Quiz](quiz.md) · [← Önceki konu](../4.3-network-security-bastion-key-vault/README.md)

## 4.4 Yedekleme, Felaket Kurtarma ve Taşıma

> **Sınav Alanı:** Describe Azure architecture and services — genişletilmiş kapsam · **Konu Kodu:** C1.19
>
> 📌 **Bağlantı:** *Describe Azure storage services* (Azure Migrate, Data Box → C1.8) ve *benefits of the cloud* (reliability / HA → C1.2); S8.3 (AZ-305) için zemin.
>
> Bu konu resmî study guide'ın (July 20, 2026) doğrudan hedefi değildir; kurs müfredatlarında ve senaryo sorularının seçeneklerinde geçer.

### 1. Ne?

"Verim güvende mi?" sorusunun üç ayrı cevabı vardır ve karıştırılması bu konunun en büyük tuzağıdır:

- **High availability (HA)**
  - Soru: "Küçük arızalarda hizmet **kesintisiz** sürsün"
  - Çözüm: Yedeklilik, zone'lar, load balancer
  - Azure servisi: Availability zone, zone-redundant servisler (→ C1.2, C1.4)
- **Backup** (yedekleme)
  - Soru: "Sildim / bozuldu / fidye yazılımı vurdu — **geçmiş bir noktaya dönebilir miyim?**"
  - Çözüm: Zaman içinde alınan, **ayrı** saklanan kurtarma noktaları
  - Azure servisi: **Azure Backup**
- **Disaster recovery (DR)**
  - Soru: "Bölge/datacenter **tamamen gitti** — hizmet başka yerde **ayağa kalkabilir mi?**"
  - Çözüm: İkincil konumda replika + failover
  - Azure servisi: **Azure Site Recovery**

Taşıma (migration) ise ayrı bir iştir: mevcut iş yüklerini **Azure'a** (veya Azure içinde başka bölgeye) getirmek. Araçları: **Azure Migrate**, **Database Migration Service**, **Data Box**, **Resource Mover** *(bağlam)*, hibrit yönetim için **Azure Arc**.

İki ölçü her konuşmanın merkezindedir:

- **RPO (Recovery Point Objective):** En fazla **ne kadar veri kaybı** kabul edilir? (zaman cinsinden; "son 15 dakikanın verisi gidebilir" → RPO = 15 dk)
- **RTO (Recovery Time Objective):** Hizmet **ne kadar sürede** geri gelmeli? ("en geç 2 saatte ayakta" → RTO = 2 sa)

Bu konunun C# kodu yoktur. Pratik, `az` CLI ile Recovery Services vault ve VM yedeklemesi laboratuvarıdır (3.6).

---

### 2. Neden?

- **Sınav/kurs:** "Backup mı Site Recovery mi?", "GRS yedek midir?", "Büyük veriyi çevrimdışı taşımak?" kalıpları senaryo sorularında seçenek olarak çıkar.
- **Gerçek işte:** Felaketlerin çoğu datacenter yangını değildir; **insan hatası** (yanlış `DELETE`), **kötü niyetli silme** ve **fidye yazılımıdır**. Replikasyon/redundancy bunları **de kopyalar**; yalnızca zaman içinde alınmış, değiştirilemez yedek kurtarır.
- **Shared responsibility (→ C1.1):** Microsoft platformu dayanıklı yapar (redundancy), ama **verinin yedeklenmesi müşterinin işidir**. "Buluttayım, yedeğe gerek yok" tehlikeli bir yanılgıdır.
- **Reliability (→ C1.2):** Güvenilir mimari = HA + backup + DR üçlüsünün RPO/RTO hedeflerine göre seçilmiş karışımı.

---

### 3. Nasıl?

#### 3.1 Yedekleme ↔ DR ↔ HA ayrımı ⭐

- **Koruduğu şey**
  - HA: Küçük arızalar (VM, rack, zone)
  - Backup: Silinme, bozulma, fidye yazılımı, yanlış değişiklik
  - DR (Site Recovery): Bölge/datacenter kaybı
- **Mekanizma**
  - HA: Yedeklilik, otomatik failover
  - Backup: Zaman içinde noktalar (point-in-time)
  - DR (Site Recovery): **Sürekli replikasyon** + failover
- **Tipik RPO**
  - HA: ~0 (eşzamanlı yedeklilik)
  - Backup: Saat–gün (yedekleme sıklığı kadar)
  - DR (Site Recovery): Saniye–dakika
- **Tipik RTO**
  - HA: Saniye–dakika · Backup: Saat (geri yükleme süresi)
  - DR (Site Recovery): Dakika–saat (recovery plan'a bağlı)
- **Geçmişe dönüş**
  - HA: **Yok** (bozulma da yayılır) · Backup: **Evet**
  - DR (Site Recovery): Sınırlı (kısa süreli recovery point'ler)
- **Maliyet** — HA: Ek kaynak · Backup: Depolama + korunan örnek ücreti · DR (Site Recovery): Replika + korunan örnek ücreti

> Hatırlatma: **HA arızayı sürdürür, backup geçmişi geri getirir, DR bölgeyi yedekler.** Üçü birbirinin yerine geçmez; üretim iş yükü genellikle üçüne birden ihtiyaç duyar.

**RPO / RTO yorumlama:**

```
         son iyi yedek            felaket                    hizmet geri geldi
  ───────────●─────────────────────✖───────────────────────────●──────────▶ zaman
             │◄──────  RPO  ──────►│◄───────────  RTO  ─────────►│
          (kaybolacak veri)                (kesinti süresi)
```

- **RPO = 24 sa, RTO = 1 gün**
  - Anlamı: Dün geceki yedeğe dönmek yeter, bir gün beklenebilir
  - Çözüm yönü: Günlük Azure Backup
- **RPO = 5 dk, RTO = 1 sa**
  - Anlamı: Neredeyse hiç veri kaybı, hızlı dönüş
  - Çözüm yönü: Site Recovery (sürekli replikasyon) + recovery plan
- **RPO ≈ 0, RTO ≈ 0**
  - Anlamı: Kesintisiz
  - Çözüm yönü: Aktif-aktif çok bölgeli mimari + veri tabanında senkron/yakın-senkron replikasyon (maliyetli; → C1.17, S8.3)

Kural: **RPO küçüldükçe ve RTO küçüldükçe maliyet artar.** İş birimi bu iki sayıyı verir; mimari araç seçimi onlardan çıkar.

#### 3.2 Azure Backup ⭐

**Azure Backup**, yönetilen, ölçeklenebilir yedekleme servisidir: altyapı kurmadan, politika tabanlı, kurtarma noktalarını bir **vault**'ta saklar. İki tür vault vardır:

| Vault | Neleri korur (özet — liste sürümlere göre genişler) |
|---|---|
| **Recovery Services vault** | **Azure VM**'ler, **VM içindeki SQL Server** ve **SAP HANA**, **Azure Files**, kurum içi dosya/klasör (MARS ajanı), DPM / Azure Backup Server |
| **Backup vault** | Daha yeni iş yükleri: **Azure Blobs**, **Azure Disks**, Azure Database for PostgreSQL, AKS *(bağlam)* |

> Sınav hangi iş yükünün hangi vault'ta olduğunu **sormaz**; "Azure Backup nelerin yedeğini alır?" sorusunda VM, SQL/SAP HANA (VM içinde), Azure Files, Blob, disk ve kurum içi sunucular cevaptır. Ayrıntı için güncel [desteklenen senaryolar](https://learn.microsoft.com/en-us/azure/backup/backup-support-matrix) tablosuna bak.

**Temel kavramlar:**

| Kavram | Anlamı |
|---|---|
| **Backup policy** | *Ne sıklıkla* yedek alınır (günlük/haftalık) ve *ne kadar* saklanır (retention: günlük, haftalık, aylık, yıllık) |
| **Recovery point** | Geri dönülebilecek bir zaman noktası |
| **Instant restore (VM)** | Son birkaç günün anlık görüntüleri (snapshot) VM'in diskinin yanında tutulur; hızlı geri dönüş sağlar *(varsayılan politikada kısa süre; ayrıntı sürüme göre değişir)* |
| **Vault tier** | Uzun süreli kurtarma noktaları vault'a taşınır; **maliyeti düşük**, geri yükleme biraz daha yavaş |
| **Storage redundancy (vault)** | LRS / ZRS / GRS; **varsayılan GRS**'tir *(doğrula)*. **Cross-region restore** için GRS gerekir. Redundancy, korunan ilk öğeden **önce** seçilmelidir |
| **Soft delete** | Silinen yedek verisi hemen yok edilmez; varsayılan **14 gün** geri alınabilir. *Enhanced soft delete* ile süre uzatılabilir ve "always-on" yapılırsa kapatılamaz |
| **Immutable vault** | Kurtarma noktalarının politikayla **erken silinmesini/değiştirilmesini engeller** (WORM benzeri); fidye yazılımı ve kötü niyetli yönetici senaryosuna karşı |
| **Multi-user authorization** *(bağlam)* | Kritik işlemler (ör. korumayı durdurma) için ikinci bir onay (Resource Guard) |

**Backup ↔ snapshot** ⭐:

- **Ne**
  - Snapshot: Bir diskin/blob'un o anki **salt-okunur kopyası**
  - Azure Backup: Politika, saklama, vault ve yönetim sunan **servis**
- **Nerede**
  - Snapshot: Çoğunlukla **kaynağın yanında** (aynı bölge/hesap)
  - Azure Backup: Ayrı **vault** (izole, farklı yönetim sınırı)
- **Saklama/otomasyon** — Snapshot: Elle · Azure Backup: Politika tabanlı, otomatik
- **Silme koruması** — Snapshot: Kaynakla birlikte silinebilir · Azure Backup: Soft delete, immutable vault

Azure Backup VM yedeklerken **snapshot kullanır** (anlık görüntü → vault'a aktarım); ama "snapshot almak = yedekleme stratejisi" demek değildir.

**Faturalama (özet):** korunan örnek (boyuta göre kademeli) + vault'ta tüketilen depolama. Kullanılmayan, unutulmuş korumalar para yakar → laboratuvarda temizlik adımı kritik.

#### 3.3 Azure Site Recovery (ASR) ⭐

**Azure Site Recovery**, **felaket kurtarma (DR)**: iş yüklerini ikincil bir konuma **sürekli replike eder**; felakette **failover** ile oradan çalıştırırsın.

| Senaryo | Kaynak → Hedef |
|---|---|
| Azure → Azure | Bir bölgedeki **Azure VM**'ler → başka bir bölge (veya zone) |
| Kurum içi → Azure | **VMware**, **Hyper-V** VM'leri ve **fiziksel sunucular** → Azure |

**Kavramlar:**

| Kavram | Anlamı |
|---|---|
| **Replication** | Kaynak VM'in disk değişiklikleri sürekli hedefe kopyalanır (RPO tipik olarak saniye–dakika) |
| **Recovery point** | Kısa süreli (saatler–günler) kurtarma noktaları: crash-consistent ve app-consistent |
| **Test failover** | Üretimi ve replikasyonu **etkilemeden**, **yalıtılmış ağda** DR tatbikatı; tatbikatı yapmayan DR planı güvenilmezdir |
| **Failover** | Gerçek geçiş: replika VM hedef bölgede çalışmaya başlar |
| **Recovery plan** | Birden çok VM'i **sırayla** (ör. önce veritabanı, sonra uygulama, sonra web) ve betik/runbook ile ayağa kaldırma düzeni |
| **Failback (reprotect)** | Birincil geri geldiğinde ters yönde replikasyon + geri dönüş; **otomatik değildir**, planlanır |

ASR **geçmişe dönüş aracı değildir**: replikasyon, bozulmuş/silinmiş veriyi de hızla karşıya taşır. Bu yüzden DR + backup birlikte kullanılır.

#### 3.4 Depolama redundancy'si yedek değildir ⭐

C1.8'deki LRS/ZRS/GRS/GZRS **donanım/zone/bölge arızasına** karşıdır:

| Olay | LRS/ZRS/GRS/GZRS korur mu? | Çünkü |
|---|---|---|
| Disk/rack arızası | ✔ | Birden çok kopya |
| Datacenter/zone kaybı | ✔ (ZRS/GZRS) | Zone'lara yayılmış kopyalar |
| Bölge felaketi | ✔ (GRS/GZRS, failover ile) | Eşleşik bölgede kopya |
| **Yanlışlıkla silme** | ✘ | Silme işlemi **tüm kopyalara** uygulanır |
| **Bozuk/şifrelenmiş veri** (fidye yazılımı) | ✘ | Bozulma kopyalanır |
| Yanlış uygulama sürümü veriyi bozdu | ✘ | Aynı |

Bu nedenle: **GRS "yedek" değildir.** Blob için ek koruma: *soft delete, versioning, point-in-time restore* (Storage özellikleri) ve Azure Backup (blob yedekleme). Bunlar da **veri koruma** katmanıdır; DR için ayrı değerlendirilir.

#### 3.5 Taşıma (migration) araçları ⭐

**Azure Migrate** — taşıma için **merkezi hub**: **discover → assess → migrate**.

| Aşama | Ne yapar |
|---|---|
| **Discover** | Kurum içi sunucuları/VM'leri/uygulamaları keşfeder (appliance ile) |
| **Assess** | Azure'a uygunluk, **boyutlandırma**, tahmini maliyet, bağımlılık analizi; *business case* |
| **Migrate** | Sunucuları (VMware, Hyper-V, fiziksel, diğer bulutlar), veritabanlarını, web uygulamalarını Azure'a taşır |

Taşıma stratejileri — **"R" modeli** (Cloud Adoption Framework'ün sınıflandırması; kaynağa göre 5 veya 6 R olarak anlatılır):

- **Rehost** ("lift and shift")
  - Ne yapılır: Olduğu gibi Azure VM'e · Efor: Düşük
  - Kazanç: Hızlı çıkış; bulut faydaları sınırlı
- **Replatform** *(bazı anlatımlarda)*
  - Ne yapılır: Küçük değişiklikle yönetilen servise (ör. SQL Server → Azure SQL MI)
  - Efor: Orta-düşük · Kazanç: Yönetim yükü azalır
- **Refactor**
  - Ne yapılır: Kodu bulut servislerini kullanacak şekilde düzenle (ör. App Service)
  - Efor: Orta · Kazanç: PaaS faydaları
- **Rearchitect**
  - Ne yapılır: Mimariyi değiştir (monolit → mikroservis)
  - Efor: Yüksek · Kazanç: Ölçek, çeviklik
- **Rebuild** — Ne yapılır: Bulut-yerel sıfırdan yeniden yaz · Efor: Çok yüksek · Kazanç: Tam bulut-yerel
- **Replace** — Ne yapılır: Hazır SaaS ile değiştir · Efor: Değişken · Kazanç: Yazılım bakımı biter

> Sınav ayrıntı sormaz; "olduğu gibi taşı" → **rehost / lift-and-shift**, "SaaS'a geç" → **replace** bilmek yeter.

**Diğer taşıma servisleri:**

| Servis | Rol |
|---|---|
| **Azure Database Migration Service (DMS)** | Veritabanlarını (SQL Server, PostgreSQL, MySQL, … → Azure veritabanı servisleri) **çevrimiçi/çevrimdışı** taşır; Azure Migrate içinden de erişilir (→ C1.16) |
| **Azure Data Box** ailesi | **Fiziksel cihazla** çevrimdışı büyük veri taşıma (→ C1.8) |
| **Azure Resource Mover** *(bağlam)* | Azure kaynaklarını **bölgeler arası** (ve subscription/resource group arası) taşımak için orkestrasyon. Servisin güncel durumunu *Azure Updates*'ten doğrula; sınavda nadirdir |
| **Azure Arc** | Taşımadan **hibrit yönetim**: kurum içi/diğer bulut sunucularını Azure'dan yönetmek (→ [C1.13](../3.3-management-deployment-tools/README.md)) |
| **AzCopy / Storage Explorer / File Sync** | Ağ üzerinden dosya/blob taşıma-senkron (→ C1.8) |

Taşıma, CAF'nin **Adopt → Migrate** aşamasıdır (→ [C1.22](../4.7-support-sla-lifecycle/README.md)).

#### 3.6 Pratik — Recovery Services vault, VM yedekleme, soft delete (`az` CLI)

> **Maliyet uyarısı:** Lab küçük bir VM (B serisi) ve bir yedek kurtarma noktası oluşturur. Saatlik cüzi VM ücreti + korunan örnek/depolama ücreti doğar; **aynı gün temizle**. Bütçe alarmın (→ [C1.11](../3.1-cost-management/README.md)) kurulu olsun. VM boyutu bölgede/abonelikte yoksa farklı bir küçük boyut seç.

```bash
RG=rg-az900-c119
LOC=westeurope
VAULT=rsv-az900-c119
VM=vm-az900-c119
az group create --name $RG --location $LOC

# 1) Vault: before protecting anything choose storage redundancy (default GRS; LRS is cheaper for a lab)
az backup vault create --resource-group $RG --name $VAULT --location $LOC
az backup vault backup-properties set --resource-group $RG --name $VAULT \
  --backup-storage-redundancy LocallyRedundant

# 2) A small VM to protect (use your own SSH key or --generate-ssh-keys)
az vm create --resource-group $RG --name $VM --image Ubuntu2204 --size Standard_B1s \
  --admin-username azureuser --generate-ssh-keys

# 3) Default policy: look at it (schedule + retention)
az backup policy show --resource-group $RG --vault-name $VAULT --name DefaultPolicy --output jsonc

# 4) Enable protection for the VM with the default policy
az backup protection enable-for-vm --resource-group $RG --vault-name $VAULT \
  --vm $VM --policy-name DefaultPolicy

# 5) Optional: trigger the first backup now (takes a while) and watch the job
az backup protection backup-now --resource-group $RG --vault-name $VAULT \
  --container-name $VM --item-name $VM \
  --backup-management-type AzureIaasVM --workload-type VM
az backup job list --resource-group $RG --vault-name $VAULT --output table
az backup recoverypoint list --resource-group $RG --vault-name $VAULT \
  --container-name $VM --item-name $VM \
  --backup-management-type AzureIaasVM --workload-type VM --output table

# 6) Soft delete behaviour: look at the vault's security settings (soft delete state, retention days)
az backup vault backup-properties show --resource-group $RG --name $VAULT --output jsonc
```

Beklenen gözlem: Yedek işi **Completed** olunca bir *recovery point* listelenir. Portalda *Recovery Services vault → Backup items → Azure Virtual Machine* ekranında korunan VM ve son yedek zamanı görünür; aynı ekrandan **Restore VM** (yeni VM / disk / tek tek dosya) seçilebilir. CLI parametre adları ve çıktı alanları sürümden sürüme değişebilir; takılırsan `az backup --help` ve portal aynı işi yapar.

**Temizlik — dikkat: korunan öğesi olan vault silinemez.**

```bash
# 7) Stop protection and delete the backup data (a vault with protected items or recovery points cannot be deleted)
az backup protection disable --resource-group $RG --vault-name $VAULT \
  --container-name $VM --item-name $VM \
  --backup-management-type AzureIaasVM --workload-type VM \
  --delete-backup-data true --yes

# 8) With soft delete ON the deleted backup stays in "soft deleted" state (14 days by default) and still blocks vault deletion.
#    For a throw-away lab vault: turn soft delete off BEFORE step 7 (not possible if "always-on"):
#    az backup vault backup-properties set --resource-group $RG --name $VAULT --soft-delete-feature-state Disable
#    Otherwise: portal → vault → Backup items → soft-deleted item → Undelete → Stop backup & delete data.

# 9) Cleanup (if the vault is empty the group delete succeeds)
az group delete --name $RG --yes --no-wait
```

> Bu cleanup sorunu aynı zamanda bir **özelliğin kanıtıdır**: soft delete ve "korumayı durdurmadan vault silinmez" kuralı, kazara/kötü niyetli silmeyi zorlaştırmak için vardır. Üretimde bunu **kapatmazsın**. Soft delete'in varsayılan durumu bölgeye/vault türüne göre değişebilir *(güncel durumu doğrula)*; laboratuvarda `Disable` komutu reddedilirse portaldan *Undelete → Stop backup* yolunu kullan.

---

### 4. Gerçek dünya senaryosu

Bir lojistik şirketi kurum içi iki VMware kümesinde 120 sunucu ve bir SQL Server çalıştırıyor; hedef: 9 ayda Azure'a geçmek ve felaket dayanıklılığını artırmak.

1. **Keşif ve değerlendirme:** **Azure Migrate** appliance'ı kurulur, 120 sunucu keşfedilir; bağımlılık analizi hangi sunucuların birlikte taşınması gerektiğini gösterir; boyutlandırma ve maliyet raporu (business case) yönetime sunulur.
2. **Strateji:** 90 sunucu **rehost** (lift-and-shift, Azure Migrate sunucu taşıma); SQL Server **Azure SQL Managed Instance**'a (**DMS** ile çevrimiçi taşıma, kısa kesinti); eski bir CRM **replace** edilip SaaS'a geçer; müşteri portalı **refactor** ile App Service'e alınır.
3. **Büyük arşiv:** 250 TB'lık taranmış belge arşivi 200 Mbps hatla ayları bulacağından **Data Box** ile taşınır (→ C1.8).
4. **Backup:** Taşınan VM'ler **Azure Backup** ile günlük yedeklenir; vault **immutable** ve soft delete açık; sorumlu yönetici hesapları MFA'lıdır. Bir mühendis yanlışlıkla test yerine **üretim** veritabanı VM'ini siler — yedekten saatler içinde geri döner (**RTO** ~ 3 sa, **RPO** ≤ 24 sa; iş birimi bunu onaylamıştı).
5. **DR:** Sipariş sistemi için **RPO 15 dk / RTO 1 sa** hedeflenir: West Europe'taki VM'ler **Site Recovery** ile North Europe'a replike edilir; *recovery plan* veritabanı → uygulama → web sırasını belirler; **her çeyrek test failover** yapılır. DR bölgesi, birincil bölgenin eşleşik bölgesi olarak planlanır (→ C1.4).
6. **Hibrit kalan parça:** Fabrikadaki iki yerel sunucu taşınamaz; **Azure Arc** ile Azure'a bağlanır, Policy ve Defender onlara da uygulanır (→ C1.13, C1.10).
7. **Ders:** Yönetim "storage zaten GRS, yedeğe gerek yok" demişti; tatbikatta bir blob kapsayıcısının toplu silinmesi **tüm kopyalara** yayıldı ve ancak blob soft delete / Backup ile geri alınabildi.

---

### 5. Sınav perspektifi

**Sınav neyi, nasıl sorar?** Senaryo → servis eşleştirmesi (Backup / Site Recovery / Migrate / Data Box / DMS), RPO ve RTO tanımları, "redundancy yedek midir?" ve evet/hayır tabloları.

- "Silinen/bozulan VM'i **dünkü haline** geri getir" — Doğru cevap: **Azure Backup** · Tuzak: Site Recovery, GRS
- "Bölge arızasında VM'leri **başka bölgede çalıştır**, failover" — Doğru cevap: **Azure Site Recovery** · Tuzak: Azure Backup
- "Kurum içi VMware/Hyper-V'yi Azure'a **DR için** replike et" — Doğru cevap: **Site Recovery** · Tuzak: Azure Migrate
- "Kurum içi sunucuları **keşfet, değerlendir ve taşı**" — Doğru cevap: **Azure Migrate** · Tuzak: Data Box, Site Recovery
- "Veritabanını (SQL Server → Azure SQL) **kesintiyi azaltarak** taşı" — Doğru cevap: **Database Migration Service** · Tuzak: Backup
- **"Çevrimdışı, onlarca/yüzlerce TB, ağ yetersiz"** — Doğru cevap: **Azure Data Box** · Tuzak: AzCopy, Azure Migrate
- "En fazla kabul edilen **veri kaybı** süresi" — Doğru cevap: **RPO** · Tuzak: RTO
- "Hizmetin **geri gelme** süresi hedefi" — Doğru cevap: **RTO** · Tuzak: RPO
- **"GRS açık, o halde yedeğim var"** — Doğru cevap: **Yanlış**: redundancy ≠ backup · Tuzak: —
- **"Kodu değiştirmeden olduğu gibi Azure VM'e taşı"** — Doğru cevap: **Rehost (lift-and-shift)** · Tuzak: Refactor, Rearchitect
- "Kurum içi sunucuları Azure'dan **yönet**, taşımadan" — Doğru cevap: **Azure Arc** · Tuzak: Azure Migrate
- **"Yedeklerin ransomware ile silinmesini/değiştirilmesini engelle"**
  - Doğru cevap: **Soft delete + immutable vault**
  - Tuzak: Yalnız GRS
- "Aynı tarihe **geri dönülebilen** kurtarma noktaları"
  - Doğru cevap: **Azure Backup** (recovery point)
  - Tuzak: Site Recovery

**Anahtar kelimeler:** *recovery point, retention, vault, restore* → Backup · *failover, failback, replicate, recovery plan, test failover* → Site Recovery · *discover, assess, migrate, lift and shift* → Azure Migrate · *offline, physical device, terabytes* → Data Box · *RPO = data loss, RTO = downtime*.

**Evet/hayır tuzakları:**
- "Azure Site Recovery, silinen bir dosyayı dünkü sürüme geri getirmek için en uygun araçtır" → **Hayır** (Backup).
- "GRS ile saklanan blob yanlışlıkla silinirse ikincil bölgeden geri alınabilir" → **Hayır** (silme kopyalara da uygulanır).
- "Azure Migrate yalnızca sunucuları değerlendirir, taşıma yapmaz" → **Hayır** (hub: discover + assess + migrate).
- "Azure Backup yalnızca Azure VM'lerini yedekler" → **Hayır** (SQL/SAP HANA in VM, Azure Files, Blob, disk, kurum içi dosyalar da).
- "RTO, kaybedilmesine izin verilen veri miktarıdır" → **Hayır** (o RPO).

---

### 6. Yaygın hatalar

| Hata | Doğrusu |
|---|---|
| "GRS/ZRS = yedek" | Redundancy **erişilebilirlik/dayanıklılık**; silme ve bozulmayı da kopyalar. Yedek için Backup (ve blob soft delete/versioning) |
| "Site Recovery ile yedek alırım" | ASR replikasyonu **DR içindir**; geçmişe dönüş yok. Yedek için Backup |
| Yedeği hiç **geri yüklemeyi denememek** | Test restore ve test failover tatbikatı yapmayan plan "inanç"tır |
| Vault'u **kaynakla aynı abonelik/yönetim sınırında**, ayrı koruma olmadan bırakmak | Soft delete, immutable vault, multi-user authorization, RBAC ile koru |
| Vault redundancy'sini korumaya başladıktan **sonra** değiştirmeyi denemek | Önce redundancy'yi seç; sonra kilitlenir (değişiklik koşulları sınırlıdır) |
| RPO/RTO'yu **iş birimiyle konuşmadan** teknik olarak seçmek | Hedefi iş verir, maliyet-fayda tartılır; "hepsi sıfır" çok pahalıdır |
| "Veriyi Azure'a taşıdım, iş bitti" | Taşıma + **yedek + DR + izleme** birlikte gelir |
| Büyük veriyi ağdan taşımaya inat etmek | Süreyi hesapla (C1.8: 40 TB / 100 Mbps ≈ 37 gün); gerekirse Data Box |
| Lab/deneme vault'larını **korumayı durdurmadan silmeye çalışmak** | Önce korumayı durdur ve yedek verisini sil (soft delete'e dikkat), sonra vault |
| Site Recovery'de failback'i **otomatik** sanmak | Reprotect + geri dönüş planlanan bir süreçtir |

---

### 7. Performans & trade-off

- **Daha sık yedek (günde 1 → saatlik)**
  - Kazanç: Daha küçük RPO
  - Bedel: Daha çok depolama, işlem; her iş yükü desteklemez
- **Uzun retention (yıllık noktalar)**
  - Kazanç: Uyumluluk, geriye dönük adli analiz
  - Bedel: Depolama maliyeti (vault tier ile düşürülür)
- **Vault GRS + cross-region restore**
  - Kazanç: Bölge kaybında yedekten bölgesel olmayan geri yükleme
  - Bedel: LRS'ten pahalı
- **Immutable vault / always-on soft delete**
  - Kazanç: Fidye yazılımına ve kötü niyetli silmeye karşı koruma
  - Bedel: Hatalı politikayı geri almak zorlaşır; planlı olmayan silinemez
- **Site Recovery ile hot DR**
  - Kazanç: Dakikalar mertebesinde RPO/RTO
  - Bedel: Korunan örnek ücreti + hedef bölgede depolama; test failover'ın geçici hesaplama maliyeti
- **Backup-only (DR yok)**
  - Kazanç: Ucuz; bölge felaketinde gün-hafta RTO
  - Bedel: Bölge çökerse yeniden kurulum süresi uzun
- **Rehost (lift-and-shift)**
  - Kazanç: Hızlı, düşük risk
  - Bedel: PaaS faydaları (yönetim, ölçek) alınmaz; maliyet optimizasyonu sonraya kalır
- **Refactor/Rearchitect**
  - Kazanç: Uzun vadeli maliyet ve çeviklik
  - Bedel: Yüksek mühendislik eforu, uzun süre
- **Çevrimiçi DMS (minimal kesinti)**
  - Kazanç: Kesinti dakikalar
  - Bedel: Kurulum karmaşıklığı; çevrimdışıya göre daha çok izleme
- **Data Box**
  - Kazanç: Ağ yükü yok, büyük veri hızlı
  - Bedel: Kargo süresi, cihaz ücreti, sürekli senkron değil

---

### 8. Kaynaklar

- [AZ-900 study guide (skills measured)](https://learn.microsoft.com/en-us/credentials/certifications/resources/study-guides/az-900)
- [What is the Azure Backup service?](https://learn.microsoft.com/en-us/azure/backup/backup-overview) · [Recovery Services vaults overview](https://learn.microsoft.com/en-us/azure/backup/backup-azure-recovery-services-vault-overview) · [Azure Backup support matrix](https://learn.microsoft.com/en-us/azure/backup/backup-support-matrix)
- [Soft delete for Azure Backup (enhanced soft delete)](https://learn.microsoft.com/en-us/azure/backup/backup-azure-enhanced-soft-delete-about) · [Immutable vault](https://learn.microsoft.com/en-us/azure/backup/backup-azure-immutable-vault-concept)
- [About Azure Site Recovery](https://learn.microsoft.com/en-us/azure/site-recovery/site-recovery-overview) · [Recovery plans](https://learn.microsoft.com/en-us/azure/site-recovery/recovery-plan-overview)
- [About Azure Migrate](https://learn.microsoft.com/en-us/azure/migrate/migrate-services-overview) · [Azure Database Migration Service](https://learn.microsoft.com/en-us/azure/dms/dms-overview) · [Azure Data Box overview](https://learn.microsoft.com/en-us/azure/databox/data-box-overview)
- [Cloud Adoption Framework — Migrate](https://learn.microsoft.com/en-us/azure/cloud-adoption-framework/migrate/) · [Azure Resource Mover overview](https://learn.microsoft.com/en-us/azure/resource-mover/overview) *(güncel durumu doğrula)*
- [Azure Well-Architected Framework — Reliability (RPO/RTO, backup & DR)](https://learn.microsoft.com/en-us/azure/well-architected/reliability/)
- [`az backup`](https://learn.microsoft.com/en-us/cli/azure/backup)

---

➡️ **Sıradaki:** [📝 Bu konunun quiz soruları](quiz.md)
