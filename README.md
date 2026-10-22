# .NET Junior → Mid → Senior Mülakat Hazırlığı

Türkçe, uygulamalı bir .NET mülakat hazırlık reposu. Hedef platform **.NET 10 (LTS) / C# 14**. Konu planının tamamı [`ROADMAP.md`](ROADMAP.md) içindedir (6 pist, 51 modül, ~300 konu); bu repo konuları teker teker doldurur.

## Başlamak

1. .NET 10 SDK'yı kurun (`dotnet --version` → `10.x`).
2. **`DotnetInterview.slnx`** dosyasını Visual Studio'da açın (VS Code / Rider için de çalışır). Solution'da yalnızca **iki proje** var: **`ConsoleApp`** (konuların çoğu) ve **`WebApp`** (HTTP sunucusu gerektiren konular).
3. Bir konuyu seçin: konu klasöründeki `README.md` teoridir, yanındaki `.cs` dosyaları kodudur, `interview-questions.md` mülakat sorularıdır.

```bash
dotnet build DotnetInterview.slnx            # tüm repo

cd ConsoleApp
dotnet run                                   # tüm konsol konularının listesi
dotnet run -- J3.3                           # J3.3'ün bölüm listesi
dotnet run -- J3.3 2                         # J3.3'ün 2. bölümü  (all: hepsi)
dotnet run -- C1.2 seri 99.95 99.99          # bazı konuların kendi komutları vardır (konunun README'sine bakın)

cd ../WebApp && dotnet run                   # web konuları (J1.3 → http://localhost:5080)
docker compose up -d                         # (repo kökünde) yalnızca altyapı isteyen konular için, ör. C1.8 → Azurite
```

Visual Studio'da `ConsoleApp`'e sağ tık → **Set as Startup Project**, sonra proje özelliklerindeki *Debug → Application arguments* alanına `J3.3 2` gibi bir değer yazıp F5 ile hata ayıklayabilirsiniz.

## Klasör yapısı

**Tek console uygulaması + tek web uygulaması; bir konu = uygulamanın içinde bir klasör.** Modül başına proje, `src/`, `tests/`, `Directory.Build.props` yoktur. Konu ID'leri `J` (Junior), `M`, `S`, `D`, `C` (sertifika) ile başlar: `J2.3`, `C1.8`.

```
DotnetInterview.slnx                       # tek solution (ConsoleApp + WebApp)
docker-compose.yml                         # altyapı (şimdilik Azurite); konular geldikçe servis eklenir
ConsoleApp/
├── ConsoleApp.csproj
├── Program.cs                             # tüm konsol konularının menüsü
├── 01-junior/
│   ├── 01-dev-foundations/                # modül = klasör
│   │   ├── 01-git/                        # konu = klasör
│   │   │   ├── README.md                  # teori
│   │   │   └── interview-questions.md     # 10-20 soru + cevap
│   │   └── 02-network-fundamentals/ …     # README + kod dosyaları + sorular
│   ├── 02-csharp-fundamentals/ … 05-build-run-basics/
│   └── …                                  # J6'dan sonrası sırayla doldurulur
├── 02-mid/  03-senior/  04-dsa/  05-interview/   # sırayla doldurulur
└── 06-certifications/                     # Azure sertifika pisti (AZ-900, AI-200): seviyelerden bağımsız
WebApp/
├── WebApp.csproj
├── Program.cs                             # web konularının AddServices / Map metotlarını çağırır
└── 01-junior/01-dev-foundations/03-how-the-web-works/   # J1.3: Kestrel + ham HTTP isteği (curl / requests.http)
```

ROADMAP'teki `📁` yolları uygulama klasörüne göredir (ör. `01-junior/03-collections-linq/` → `ConsoleApp/01-junior/03-collections-linq/`).

| Seviye | Klasör | Durum |
|--------|--------|-------|
| [Junior](ROADMAP.md#seviye-1--junior) (J1–J13) | `01-junior/` | J1 (5 konu), J2 (6 konu), J3 (3 konu), J4 (3 konu), J5 (3 konu) yazıldı; devamı sırayla |
| [Mid](ROADMAP.md#seviye-2--mid) (M1–M15) | `02-mid/` | planlandı |
| [Senior](ROADMAP.md#seviye-3--senior) (S1–S10) | `03-senior/` | planlandı |
| [DSA](ROADMAP.md#dsa-pisti--veri-yapıları--algoritmalar) (D1–D11) | `04-dsa/` | planlandı (seviyelerle paralel; tek proje) |
| [Mülakat](ROADMAP.md#mülakat-hazırlığı) (I1–I6) | `05-interview/` | planlandı (yalnızca `.md`) |
| [Sertifika](ROADMAP.md#sertifika-pisti--azure-sertifikaları) (C1 **AZ-900**, C2 **AI-200**) | `06-certifications/` | **C1 (AZ-900) tamamlandı — 15 konu + deneme sınavı** — seviyelerden bağımsız; C1: 15 konu (AZ-900, *July 20, 2026* müfredatı), C2: 14 konu (AI-200, *October 7, 2026* müfredatı); teori + laboratuvar + sınav tarzı quiz |

Planın geri kalanı: [Genel Harita](ROADMAP.md#genel-harita) · [Çalışma Planı](ROADMAP.md#çalışma-planı) · [2026 Radarı](ROADMAP.md#2026-radarı--neler-değişti).

## Teorik ve pratik anlatımlar nerede?

| Ne | Nerede |
|----|--------|
| Ne öğrenilecek (ana hat) | [`ROADMAP.md`](ROADMAP.md) — konu maddeleri, ⭐ = sık sorulan |
| Uzun anlatım (teori) | konunun `README.md`'si: *Ne? / Neden? / Nasıl? / Gerçek dünya senaryosu / Mülakat perspektifi / Yaygın hatalar / Performans & trade-off / Kaynaklar* |
| Çalışan kod | konu klasöründeki `.cs` dosyaları (`ConsoleApp`; HTTP sunucusu gerektirenler `WebApp`) |
| Mülakat soruları | konunun `interview-questions.md`'si + ROADMAP'teki modül 🎯 satırı (sertifika pistinde: `quiz.md` + 📝 satırı) |
| Altyapı (veritabanı, broker…) | kökteki `docker-compose.yml` |

Repo bilerek yalındır: **test projesi yalnızca konusu test olan modüllerde** (J10, M10…) bulunur, komut satırı alıştırmaları script değil README'deki kod bloklarıdır.

## Nasıl çalışılır?

1. Konunun `README.md`'sini okuyun.
2. Kodu çalıştırın, çıktıyı **önce tahmin edip** sonra karşılaştırın; sonra konunun kodunu kendiniz yazmayı deneyin — kopyalamayın.
3. `interview-questions.md`'yi **kapalı kitap** cevaplayın, sonra kontrol edin.
4. Haftada bir tekrar; her seviyenin sonunda capstone ve mock mülakat.

Yeni konu/modül yazma kuralları için [`CLAUDE.md`](CLAUDE.md), planın kendisi için [`ROADMAP.md`](ROADMAP.md).
