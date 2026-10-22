# .NET Junior → Mid → Senior Mülakat Hazırlığı

Türkçe, uygulamalı bir .NET mülakat hazırlık reposu. Hedef platform **.NET 10 (LTS) / C# 14**. Konu planının tamamı [`ROADMAP.md`](ROADMAP.md) içindedir (6 pist, 51 modül, ~300 konu); bu repo konuları teker teker doldurur.

## Başlamak

1. .NET 10 SDK'yı kurun (`dotnet --version` → `10.x`).
2. **`DotnetInterview.slnx`** dosyasını Visual Studio'da açın. Solution'da yalnızca **iki proje** var:
   - **`ConsoleApp`** — konuların çoğu. **F5** → konu menüsü açılır: bir konu yazın (ör. `J3.2`), sonra bölüm numarası ya da `all`.
   - **`WebApp`** — HTTP sunucusu gerektiren konular (J1.3, J6). `WebApp`'e sağ tık → *Debug → Start New Instance* → bir konu seçin (ör. `J6.2`); sunucu `http://localhost:5080`'de açılır, `Ctrl+C` ile durur. Aynı anda tek konu çalışır.
3. Konunun anlatımı, konu klasöründeki `README.md`'dir; mülakat soruları aynı klasördeki `interview-questions.md` (sertifika pistinde `quiz.md`).

Terminalden de aynı şekilde:

```bash
cd ConsoleApp
dotnet run                          # menu
dotnet run -- J3.2 4                # without the menu: section 4 of J3.2  (all: every section)
cd ../WebApp && dotnet run -- J6.2  # web topics: one topic at a time (menu without the argument)
```

## Klasör yapısı

**Bir console uygulaması + bir web uygulaması; konu = uygulamanın içinde bir klasör** (anlatım + varsa kod aynı yerde). Her konunun pratiği yoktur: kodsuz konularda yalnızca `README.md` ve sorular vardır.

```
DotnetInterview.slnx
ConsoleApp/
├── Program.cs                          # menu for every console topic
├── 01-junior/
│   ├── 01-dev-foundations/             # module
│   │   ├── 01-git/                     # topic
│   │   │   ├── README.md               # explanation (Turkish)
│   │   │   └── interview-questions.md  # 10-20 questions + answers
│   │   └── 02-network-fundamentals/    # README + code + questions
│   └── 02-csharp-fundamentals/ … 05-build-run-basics/
├── 02-mid/  03-senior/  04-dsa/  05-interview/   # filled in order
└── 06-certifications/                  # Azure certification track (AZ-900, AI-200)
WebApp/
├── Program.cs                          # menu; runs ONE web topic at a time
└── 01-junior/01-dev-foundations/03-how-the-web-works/
```

ROADMAP'teki `📁` yolları uygulama klasörüne göredir (ör. `01-junior/03-collections-linq/` → `ConsoleApp/01-junior/03-collections-linq/`). Konu ID'leri `J` (Junior), `M`, `S`, `D`, `C` (sertifika) ile başlar: `J2.3`, `C1.8`.

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
| Altyapı (veritabanı, broker…) | ilk altyapı isteyen konu geldiğinde kökte `docker-compose.yml` |

Repo bilerek yalındır: **test projesi yalnızca konusu test olan modüllerde** (J10, M10…) bulunur, komut satırı alıştırmaları script değil README'deki kod bloklarıdır.

## Nasıl çalışılır?

1. Konunun `README.md`'sini okuyun.
2. Kodu çalıştırın, çıktıyı **önce tahmin edip** sonra karşılaştırın; sonra konunun kodunu kendiniz yazmayı deneyin — kopyalamayın.
3. `interview-questions.md`'yi **kapalı kitap** cevaplayın, sonra kontrol edin.
4. Haftada bir tekrar; her seviyenin sonunda capstone ve mock mülakat.

Yeni konu/modül yazma kuralları için [`CLAUDE.md`](CLAUDE.md), planın kendisi için [`ROADMAP.md`](ROADMAP.md).
