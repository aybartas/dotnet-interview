# .NET Junior → Mid → Senior Mülakat Hazırlığı

Türkçe, uygulamalı bir .NET mülakat hazırlık reposu. Hedef platform **.NET 10 (LTS) / C# 14**. Konu planının tamamı [`ROADMAP.md`](ROADMAP.md) içindedir (6 pist, 51 modül, ~300 konu); bu repo konuları teker teker doldurur.

## Başlamak

1. .NET 10 SDK'yı kurun (`dotnet --version` → `10.x`).
2. **`DotnetInterview.slnx`** dosyasını Visual Studio'da açın (VS Code / Rider için de çalışır). Tek solution'da tüm modüller proje olarak, seviyelere göre klasörlenmiş görünür.
3. Bir konuyu seçin: konu klasöründeki `README.md` teoridir, yanındaki `.cs` dosyaları kodudur, `interview-questions.md` mülakat sorularıdır.

```bash
dotnet build DotnetInterview.slnx            # tüm repo

cd 01-junior/03-collections-linq             # bir modül = bir proje
dotnet run                                   # konu listesi
dotnet run -- 3.3                            # 3.3'ün bölüm listesi
dotnet run -- 3.3 2                          # 3.3'ün 2. bölümü  (all: hepsi)
```

Visual Studio'da modülün projesine sağ tık → **Set as Startup Project**, sonra proje özelliklerindeki *Debug → Application arguments* alanına `3.3 2` gibi bir değer yazıp F5 ile hata ayıklayabilirsiniz.

## Klasör yapısı

**Bir modül = bir .NET projesi, bir konu = projenin içinde bir klasör.** Ayrı `src/` veya `tests/` klasörü yoktur.

```
DotnetInterview.slnx                     # tek solution
01-junior/
├── 01-dev-foundations/                  # J01.DevFoundations (Web SDK: Kestrel + console demoları)
│   ├── J01.DevFoundations.csproj
│   ├── Program.cs                       # konu menüsü
│   ├── 01-git/                          # konu = klasör
│   │   ├── README.md                    # teori
│   │   └── interview-questions.md       # 10-20 soru + cevap
│   └── 02-network-fundamentals/ …       # README + kod dosyaları + sorular
├── 02-csharp-fundamentals/              # J02.CSharpFundamentals
├── 02-csharp-fundamentals-lib/          # J02.Lib (assembly sınırı deneyleri için küçük kütüphane)
├── 03-collections-linq/                 # J03.CollectionsLinq
├── 04-async-basics/                     # J04.AsyncBasics (Web SDK: yerel Kestrel API'ye çağrılar)
└── 05-build-run-basics/                 # J05.BuildRunBasics
02-mid/  03-senior/  04-dsa/  05-interview/   # sırayla doldurulur
06-certifications/                        # Azure sertifika pisti (AZ-900, AI-200): seviyelerden bağımsız
```

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
| Çalışan kod | konu klasöründeki `.cs` dosyaları (modül projesinin içinde) |
| Mülakat soruları | konunun `interview-questions.md`'si + ROADMAP'teki modül 🎯 satırı (sertifika pistinde: `quiz.md` + 📝 satırı) |
| Altyapı (veritabanı, broker…) | gereken modülde `docker-compose.yml` |

Repo bilerek yalındır: **test projesi yalnızca konusu test olan modüllerde** (J10, M10…) bulunur, komut satırı alıştırmaları script değil README'deki kod bloklarıdır.

## Nasıl çalışılır?

1. Konunun `README.md`'sini okuyun.
2. Kodu çalıştırın, çıktıyı **önce tahmin edip** sonra karşılaştırın; sonra konunun kodunu kendiniz yazmayı deneyin — kopyalamayın.
3. `interview-questions.md`'yi **kapalı kitap** cevaplayın, sonra kontrol edin.
4. Haftada bir tekrar; her seviyenin sonunda capstone ve mock mülakat.

Yeni konu/modül yazma kuralları için [`CLAUDE.md`](CLAUDE.md), planın kendisi için [`ROADMAP.md`](ROADMAP.md).
