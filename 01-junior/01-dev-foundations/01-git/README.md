# J1.1 Git & Versiyon Kontrolü

> 📁 `01-junior/01-dev-foundations/01-git/` · Önkoşul: — · Sorular: [`interview-questions.md`](interview-questions.md)

## İçindekiler

1. [Ne?](#1-ne)
2. [Neden?](#2-neden)
3. [Nasıl?](#3-nasıl)
4. [Gerçek dünya senaryosu](#4-gerçek-dünya-senaryosu)
5. [Mülakat perspektifi](#5-mülakat-perspektifi)
6. [Yaygın hatalar](#6-yaygın-hatalar)
7. [Performans & trade-off](#7-performans--trade-off)
8. [Kaynaklar](#8-kaynaklar)

---

## 1. Ne?

**Git**, dağıtık (distributed) bir versiyon kontrol sistemidir. Her geliştiricinin makinesinde projenin **tüm geçmişi** bulunur; merkezi sunucu (GitHub, Azure DevOps, GitLab) sadece "herkesin anlaştığı" bir kopyadır.

### Git'in veri modeli (en önemli kısım)

Git komutlarını ezberlemek yerine modeli bilirseniz komutlar kendiliğinden anlam kazanır:

| Kavram | Ne? |
|--------|-----|
| **Commit** | Projenin o andaki **tam anlık görüntüsü** (snapshot) + üst commit(ler)e işaretçi + yazar + mesaj. Fark (diff) saklamaz; diff, iki snapshot'tan hesaplanır. |
| **Hash (SHA-1/SHA-256)** | Commit'in kimliği; içeriğinden türetilir. İçerik, üst commit veya mesaj değişirse hash değişir → **commit değiştirilemez, sadece yenisi üretilebilir.** |
| **Branch** | Bir commit'i gösteren **hareketli etiket** (41 byte'lık bir dosya). Branch "açmak" ucuzdur; kopyalama yoktur. |
| **`HEAD`** | "Şu an neredeyim?" işaretçisi. Genelde bir branch'i gösterir; bir commit'i doğrudan gösterirse **detached HEAD**'dir. |
| **Working tree** | Diskte gördüğünüz dosyalar. |
| **Index (staging area)** | Bir sonraki commit'e girecek değişikliklerin hazırlık alanı (`git add` buraya yazar). |
| **Remote** | Başka bir repo kopyası (`origin`). `origin/main` = "son fetch'te origin'deki main'in durumu". |

Üç alan arasındaki akış:

```
working tree ──git add──▶ index ──git commit──▶ local repo ──git push──▶ remote
      ◀──git restore──────   ◀──git reset──────       ◀──git fetch / pull──
```

Geçmiş bir **DAG**'dır (yönlü, döngüsüz graf): her commit üstünü (merge commit ise birden fazla üstünü) gösterir.

```
A ← B ← C ← D        main
         ↖
           E ← F     feature/x
```

### Günlük komut seti

```bash
git status                       # neredeyim, ne değişti?
git add -p                       # değişiklikleri parça parça stage'le (bilinçli commit için en iyi alışkanlık)
git commit -m "feat: ..."
git switch -c feature/login      # yeni branch aç ve geç (eski: git checkout -b)
git switch main
git log --oneline --graph --all  # geçmişin şekli
git diff                         # working tree ↔ index
git diff --staged                # index ↔ son commit
git restore dosya.cs             # working tree'deki değişikliği at (⚠ geri dönüşü yok)
git restore --staged dosya.cs    # stage'den çıkar (değişiklik diskte kalır)
```

---

## 2. Neden?

- **Geri dönüş güvencesi:** Hatalı değişiklik her zaman geri alınabilir; "ne zaman, kim, neden değiştirdi" (`git blame`, `git log -p`) sorusunun cevabı vardır.
- **Paralel çalışma:** Branch ucuz olduğu için herkes kendi izole kopyasında çalışır, sonra birleştirir.
- **İşbirliği altyapısı:** Pull request, code review, CI/CD tetikleme — hepsi Git'in üstüne kurulur. Git bilmeyen bir geliştirici takıma katılamaz.
- **Mülakat gerçeği:** "Git biliyorum" ile "geçmişi yeniden yazmanın başkasına ne yaptığını biliyorum" arasındaki fark junior ile mid arasındaki farktır.

---

## 3. Nasıl?

### 3.1 `merge` vs `rebase` ⭐

İkisi de "bir branch'teki işi başka branch'e al" işini yapar, **geçmişin şeklini** farklı bırakır.

```
Başlangıç:
        E ← F          feature
       /
A ← B ← C ← D          main
```

**Merge** — iki ucu birleştiren yeni bir *merge commit* (iki üstü olan commit) üretir; mevcut commit'lere dokunmaz:

```
        E ← F ─────┐
       /            ▼
A ← B ← C ← D ← ← ← M     main  (M = merge commit)
```

**Rebase** — feature'daki commit'leri `main`'in ucuna **yeniden oynatır**; E ve F yerine *yeni hash'li* E′ ve F′ oluşur:

```
A ← B ← C ← D ← E′ ← F′
```

| | `merge` | `rebase` |
|---|---------|----------|
| Geçmiş | Gerçeği korur (non-linear) | Doğrusal, temiz |
| Mevcut commit'ler | Değişmez | **Yeniden yazılır** (yeni hash) |
| Çakışma çözümü | Tek seferde | Commit başına, tek tek |
| Güvenli mi? | Paylaşılmış branch'te evet | Paylaşılmış branch'te **hayır** |
| Tipik kullanım | Feature → main birleştirme | Feature branch'i `main`'e güncel tutma |

**Altın kural:** *Başkalarının üzerine kurduğu (push edilmiş, paylaşılan) commit'leri rebase etme.* Rebase hash'leri değiştirir; sizin yeni geçmişiniz ile diğerlerinin elindeki eski geçmiş ıraksar ve herkes için çakışma/duplicate commit cehennemi başlar.

**Fast-forward vs merge commit:** Hedef branch (`main`) feature'ın çıkış noktasından ilerlememişse Git sadece `main` işaretçisini ileri kaydırır — merge commit **oluşmaz** (fast-forward). `main` ilerlemişse gerçek bir merge commit gerekir. `git merge --no-ff` fast-forward mümkün olsa bile merge commit'i zorlar (feature'ın tek bir birim olarak izini korur); `git merge --ff-only` ise yalnızca fast-forward mümkünse birleştirir, değilse hata verir.

**Squash merge:** PR'daki tüm commit'leri tek commit'e sıkıştırır. `main` geçmişi PR başına bir commit olur; geri almak (`revert`) kolaydır, ama ayrıntılı geçmiş kaybolur. GitHub'da en yaygın tercih budur.

### 3.2 Merge conflict çözümü

Aynı satırlar iki tarafta farklı değiştirildiğinde Git karar veremez ve dosyayı işaretler:

```
<<<<<<< HEAD                       ← "bizim" taraf (merge'de: bulunduğunuz branch)
        return Math.Round(...);
||||||| ortak ata                  ← (diff3 stilinde) ikisinin çıktığı orijinal
        return price - ...;
=======
        if (percent is < 0 ...) throw ...;
        return price - ...;
>>>>>>> feature/validation         ← "onların" taraf
```

Adımlar: dosyayı açın → **iki tarafın niyetini** anlayıp birleştirin (çoğu zaman biri diğerini silmez, ikisi de kalır) → işaretleri silin → `git add` → `git commit` (merge) veya `git rebase --continue` (rebase). Vazgeçmek için `git merge --abort` / `git rebase --abort`.

İpuçları:
- `git config --global merge.conflictStyle zdiff3` — ortak atayı da gösterir; çözmeyi ciddi şekilde kolaylaştırır.
- Çakışmayı çözdükten sonra **derleyin ve testleri çalıştırın**. Metinsel çakışma olmasa bile *semantik* çakışma olabilir (biri metodu yeniden adlandırdı, diğeri yeni çağrı ekledi).
- `git rerere` aynı çakışmayı tekrar tekrar çözmeyi (uzun yaşayan branch'lerde) otomatikleştirir.
- Çakışmayı önlemenin en iyi yolu: **küçük, kısa ömürlü branch'ler** ve sık `main` entegrasyonu.

**🛠 Alıştırma — merge çakışması üretin ve çözün** (boş bir klasörde; global git ayarınıza dokunmaz):

```bash
git init -b main && git config user.name Alistirma && git config user.email a@example.com
git config merge.conflictStyle diff3            # ortak atayı da gösterir

cat > Calculator.cs <<'CS'
public static class Calculator
{
    public static decimal ApplyDiscount(decimal price, decimal percent) => price - price * percent / 100;
}
CS
git add . && git commit -m "chore: Calculator"

git switch -c feature/validation                # 1. geliştirici: doğrulama ekler
sed -i 's|=> price - price \* percent / 100;|{ if (percent is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(percent)); return price - price * percent / 100; }|' Calculator.cs
git commit -am "feat: indirim yüzdesi doğrulaması"

git switch main                                 # 2. geliştirici: AYNI satırı yuvarlar
sed -i 's|=> price - price \* percent / 100;|=> Math.Round(price - price * percent / 100, 2);|' Calculator.cs
git commit -am "feat: indirimli fiyat 2 basamağa yuvarlanır"

git merge feature/validation                    # ÇAKIŞMA! Calculator.cs'i açın, iki değişikliği birleştirin
git add Calculator.cs && git commit             # merge commit
git log --oneline --graph --all
```

Vazgeçmek için `git merge --abort`. Ayrıca deneyin: `git log --merge`, `git diff --name-only --diff-filter=U`.

**🛠 Alıştırma — aynı çakışmayı rebase ile yaşayın** (yukarıdaki repoyu `git reset --hard` ile ya da yeni bir klasörde başlatın):

```bash
git init -b main && git config user.name Alistirma && git config user.email a@example.com
echo 'public static class Pricing { public static decimal Total(decimal net) => net * 1.18m; }' > Pricing.cs
git add . && git commit -m "chore: Pricing"

git switch -c feature/currency                  # 2 commit: yuvarlama + yeni sınıf
echo 'public static class Pricing { public static decimal Total(decimal net) => Math.Round(net * 1.18m, 2); }' > Pricing.cs
git commit -am "feat: toplam yuvarlanır"
echo 'public static class Currency { public static string Format(decimal v) => v.ToString("C2"); }' >> Pricing.cs
git commit -am "feat: para birimi formatlayıcı"

git switch main                                 # main: AYNI satırı değiştirir
echo 'public static class Pricing { public const decimal VatRate = 1.20m; public static decimal Total(decimal net) => net * VatRate; }' > Pricing.cs
git commit -am "refactor: KDV oranı sabite taşındı"

git switch feature/currency && git rebase main  # rebase ORTASINDASINIZ: çakışmayı çözün
git add Pricing.cs && git rebase --continue     # 2. commit de çakışabilir; aynısını tekrarlayın
git log --oneline --graph --all                 # doğrusal geçmiş — commit'ler YENİ hash aldı
```

Vazgeçmek için `git rebase --abort`. Merge senaryosuyla **geçmişin şeklini** karşılaştırın; paylaşılmış branch'te rebase yapılmaz.

### 3.3 `stash`, `cherry-pick`, `bisect`

**`git stash`** — yarım kalmış işi geçici bir rafa kaldırır (acil bir hata düzeltmesi için branch değiştirmeniz gerektiğinde):

```bash
git stash push -m "login formu yarım"    # değişiklikleri rafa kaldır (izlenmeyen dosyalar için -u)
git stash list
git stash pop                            # en üsttekini geri getir ve rafdan sil
git stash apply stash@{1}                # geri getir ama rafta bırak
```

**`git cherry-pick <hash>`** — başka bir branch'teki **tek bir commit'i** bulunduğunuz branch'e kopyalar (yeni hash ile). Tipik kullanım: `main`'deki bir hata düzeltmesini `release/1.4` branch'ine de taşımak (hotfix backport). Aşırı kullanmayın: aynı değişiklik iki farklı commit olarak iki yerde yaşar, ileride merge'de gürültü yaratabilir.

**`git bisect`** — "bu hata ne zaman girdi?" sorusunu **ikili aramayla** çözer. İyi bir commit ve kötü bir commit verirsiniz; Git ortadakine geçer, siz "iyi/kötü" dersiniz, aralık yarıya iner. 1000 commit için ~10 adım (`log₂ 1000`).

```bash
git bisect start
git bisect bad                    # şu anki HEAD hatalı
git bisect good v1.3.0            # bu etiket sağlamdı
# Git ortadaki commit'e geçti → uygulamayı/testi çalıştır → git bisect good|bad
git bisect run dotnet test        # ya da tamamını otomatikleştir: exit 0 = iyi, 1-127 (125 hariç) = kötü, 125 = atla
git bisect reset
```

Bisect'in etkili çalışması **küçük commit'lere** ve her commit'in derlenebilir olmasına bağlıdır — bir başka sebep daha, commit'leri küçük tutmak için.

**🛠 Alıştırma — bisect ile suçlu commit'i bulun** (40 commit'te biri KDV'yi sessizce bozuyor):

```bash
git init -b main && git config user.name Alistirma && git config user.email a@example.com
printf '#!/usr/bin/env bash\nsource ./vat.env\n[[ $(( 100 * (100 + VAT_PERCENT) / 100 )) -eq 118 ]]\n' > check.sh && chmod +x check.sh
echo "VAT_PERCENT=18" > vat.env && git add . && git commit -m "chore: ilk commit"

BUG_AT=$(( RANDOM % 30 + 6 ))                   # her seferinde farklı: ezberlenemesin
for i in $(seq 1 40); do
  echo "- değişiklik $i" >> CHANGELOG.md
  if [ "$i" -eq "$BUG_AT" ]; then echo "VAT_PERCENT=8" > vat.env; msg="refactor: konfigürasyon temizliği (#$i)"
  else msg="docs: changelog güncellemesi (#$i)"; fi
  git add . && git commit -qm "$msg"
done

git bisect start HEAD "$(git rev-list --max-parents=0 HEAD)"   # kötü = HEAD, iyi = ilk commit
git bisect run ./check.sh                       # otomatik; elle için her adımda ./check.sh + git bisect good|bad
git bisect reset
echo "gerçek hatalı commit: #$BUG_AT"
```

`check.sh` çıkış kodu sözleşmesi: `0` = iyi, `1` = kötü, `125` = atla. 40 commit için en fazla ~6 adım (`log₂ 40`).

### 3.4 `reset` (soft/mixed/hard) vs `revert` ⭐

İkisi de "geri al" der, ama **tamamen farklı** şeyler yapar.

| Komut | Branch işaretçisi | Index (stage) | Working tree | Geçmiş |
|-------|:-----------------:|:-------------:|:------------:|--------|
| `reset --soft HEAD~1` | geri gider | **korunur** | korunur | Commit "silinir", değişiklikler stage'de kalır |
| `reset --mixed HEAD~1` *(varsayılan)* | geri gider | sıfırlanır | **korunur** | Commit silinir, değişiklikler stage'siz diskte kalır |
| `reset --hard HEAD~1` | geri gider | sıfırlanır | **sıfırlanır** ⚠ | Commit ve değişiklikler gider (reflog dışında kurtarılamaz) |
| `revert <hash>` | **ileri** gider | — | — | Eski commit'in etkisini tersine çeviren **yeni** commit eklenir |

```bash
git reset --soft HEAD~1      # "son commit'i geri al, ama işimi stage'de bırak" → mesajı düzeltip tekrar commit'lemek için
git reset --hard origin/main # yerel main'i uzaktakine eşitle, yerel değişiklikleri ÇÖPE AT
git revert abc1234           # abc1234'ün etkisini geri alan yeni commit
git revert -m 1 <merge-hash> # bir MERGE commit'ini geri alırken hangi üstün korunacağı (-m 1 = ilk üst)
```

**Paylaşılmış geçmişi neden yeniden yazmamalı?** `reset` + `push --force` geçmişten commit'i siler. Aynı commit'i `pull` etmiş meslektaşlarınızın yerel geçmişinde o commit hâlâ vardır; sonraki push'larında "silinen" commit'i geri getirirler ya da anlaşılmaz çakışmalar yaşarlar. **`revert` geçmişi ileri taşır**, kimsenin elindeki geçmişi geçersiz kılmaz — paylaşılmış branch'te tek güvenli geri alma yoludur.

**Kurtarma ağı: `reflog`.** `git reflog` HEAD'in yakın zamanda gittiği her yeri kaydeder (yaklaşık 90 gün). `reset --hard` ile "kaybolan" commit'ler genellikle `git reflog` → `git reset --hard <hash>` ile geri getirilebilir.

`--force` gerekiyorsa (kendi feature branch'inizi rebase ettiniz): **`git push --force-with-lease`** kullanın — uzaktaki branch sizin son gördüğünüzden farklıysa (başkası push ettiyse) reddeder.

### 3.5 Pull request akışı, code review, küçük PR ⭐

```
main ──●──────────────────●── (merge)
        \                /
         feature/x ●──●─┘   ← PR: review → CI yeşil → onay → merge
```

1. `main`'den kısa ömürlü feature branch aç.
2. Küçük commit'lerle ilerle, düzenli `main`'i içeri al (rebase veya merge).
3. PR aç: **ne** değişti ve **neden** — açıklama, ilgili issue, ekran görüntüsü/test sonucu.
4. CI (build + test + lint) yeşil olmalı.
5. Review → düzeltmeler → onay → merge (genelde squash).

**Küçük PR prensibi:** İdeal PR 200–400 satırın altındadır ve **tek bir amacı** vardır. Büyük PR'ı kimse gerçekten okumaz ("LGTM" damgası alır, hatalar geçer); küçük PR hızlı gözden geçirilir, hızlı merge olur, çakışma riski düşer, geri alması kolaydır. Refactoring ile özellik ekleme aynı PR'a konmaz.

**Code review kültürü:**
- *Yazar için:* Kendi PR'ınızı önce kendiniz okuyun; neden-bağlamını açıklayın; eleştiriyi koda yönelik alın.
- *Reviewer için:* Doğruluk, okunabilirlik, test, güvenlik ve mimari uyuma bakın; stil tartışmasını araca (`.editorconfig`, formatter) bırakın. Yorumu zorunlu ("blocker") ve öneri ("nit:") olarak ayırın; **soru sorun, emretmeyin** ("Burada `null` gelirse ne olur?").
- Review bir bilgi paylaşım aracıdır, sadece hata avı değil.

### 3.6 Branching stratejileri — hangisi ne zaman

| Strateji | Nasıl çalışır | Uygun olduğu yer |
|----------|---------------|------------------|
| **GitHub Flow** | `main` her zaman deploy edilebilir; her iş kısa ömürlü branch + PR ile gelir; merge = deploy | Web uygulamaları/API'ler, sürekli teslimat; **çoğu takım için varsayılan** |
| **Trunk-based development** | Herkes `main`'e (trunk) çok sık (günde en az bir kez) küçük değişiklik gönderir; yarım işler **feature flag** arkasında gizlenir | Olgun CI/CD, güçlü test altyapısı, büyük ölçekli takımlar (Google, Microsoft) |
| **Git Flow** | `main` + `develop` + `feature/*` + `release/*` + `hotfix/*` | Sürümlü ürünler (masaüstü/mobil, aynı anda birden çok sürümü destekleme); sürekli teslimat için **ağır** |

Seçim ölçütü: *Deploy sıklığınız* ve *aynı anda kaç sürümü desteklediğiniz*. Sürekli deploy ediyorsanız Git Flow'un `develop`/`release` katmanları sadece gecikme ve merge acısı ekler.

### 3.7 Conventional Commits & Semantic Versioning

**Conventional Commits** commit mesajına makine tarafından okunabilir yapı verir:

```
<tip>(<kapsam>): <kısa açıklama>

[gövde: NEDEN değişti]

[BREAKING CHANGE: ...]
```

| Tip | Anlamı | SemVer etkisi |
|-----|--------|:-------------:|
| `feat` | Yeni özellik | MINOR |
| `fix` | Hata düzeltme | PATCH |
| `docs`, `style`, `refactor`, `perf`, `test`, `build`, `ci`, `chore` | Davranışı değiştirmeyen işler | — |
| `feat!:` veya `BREAKING CHANGE:` | Geriye uyumsuz değişiklik | **MAJOR** |

Faydası: otomatik changelog ve sürüm numarası üretimi, geçmişin taranabilirliği.

**Semantic Versioning (`MAJOR.MINOR.PATCH`)**:
- **MAJOR:** geriye uyumsuz API değişikliği (`1.4.2 → 2.0.0`)
- **MINOR:** geriye uyumlu yeni özellik (`1.4.2 → 1.5.0`)
- **PATCH:** geriye uyumlu hata düzeltmesi (`1.4.2 → 1.4.3`)
- Ön sürüm: `2.0.0-beta.1`; NuGet paketlerinde de aynı şema geçerlidir (→ J1.4).

### 3.8 .NET için `.gitignore`

Build çıktıları ve kişisel/IDE dosyaları repoya girmez:

```gitignore
bin/
obj/
*.user          # kişisel IDE ayarları
.vs/            # Visual Studio önbelleği
.idea/          # Rider
*.suo
TestResults/
appsettings.Development.json   # (projeye bağlı) yerel gizli ayarlar varsa
```

- Hazır şablon: `dotnet new gitignore` (en güncel resmî .NET şablonunu üretir).
- **Gizli bilgiler (connection string, API key) hiçbir koşulda commit'lenmez.** Yanlışlıkla commit'lendiyse anahtarı **iptal edin/yenileyin** — geçmişten silmek yetmez (başka klonlarda ve cache'lerde kalır). Yerelde `dotnet user-secrets` kullanılır (→ J6.3, J11.4).
- Zaten izlenen bir dosyayı sonradan `.gitignore`'a eklemek işe yaramaz; önce `git rm --cached dosya` gerekir.

---

## 4. Gerçek dünya senaryosu

**"Cuma akşamı prod'da fatura toplamı yanlış."**

1. Müşteri şikâyeti geldi; son iki haftada ~150 commit girmiş. Hatalı sürümü (`v2.3.1`) ve son sağlam sürümü (`v2.2.0`) biliyorsunuz.
2. `git bisect start v2.3.1 v2.2.0` ile 7-8 adımda suçlu commit'i bulursunuz: "refactor: konfigürasyon temizliği" — KDV oranı yanlışlıkla değişmiş (tam olarak yukarıdaki bisect alıştırmasının senaryosu).
3. Fix'i hemen yayınlamak için yarım işinizi `git stash` ile rafa kaldırırsınız.
4. `main`'den `hotfix/vat-rate` branch'i açar, düzeltir, PR açar, review + CI'dan sonra merge edersiniz.
5. Aynı düzeltme eski bir `release/2.2` hattında da destekleniyorsa `git cherry-pick` ile backport edilir.
6. Hatalı commit çok başka şeyle iç içeyse ve acil geri alınması gerekiyorsa: `git revert <hash>` (asla `reset --hard` + `push --force`).
7. Sonra `git stash pop` ile işinize dönersiniz. Kalıcı çözüm: aynı hatayı yakalayacak bir test eklenir ve commit mesajı `fix(billing): KDV oranı 18'e geri alındı` olur → otomatik changelog'da görünür.

---

## 5. Mülakat perspektifi

Sık sorulanlar ([`interview-questions.md`](interview-questions.md)'de cevaplarıyla):
- `merge` ile `rebase` farkı, hangisini ne zaman kullanırsın? ⭐
- `reset` ile `revert` farkı; paylaşılmış branch'te neden `reset` yok?
- Fast-forward nedir? `--no-ff` ne işe yarar?
- Conflict nasıl çözülür? `git stash`/`cherry-pick`/`bisect` ne zaman kullanılır?
- Branching stratejileri: GitHub Flow vs Trunk-based vs Git Flow.
- "İyi bir PR nasıl olmalı?" — davranışsal sorulara da bağlanır (code review deneyimi).

**Cevap verirken:** komut ezberi değil **geçmişin şekli** üzerinden anlatın (çizin!). "Rebase geçmişi yeniden yazar, çünkü commit'ler hash'iyle birlikte yeniden üretilir" cümlesi, "rebase daha temiz" cümlesinden çok daha güçlüdür.

---

## 6. Yaygın hatalar

| Hata | Sonuç / Doğrusu |
|------|-----------------|
| Paylaşılmış branch'i rebase edip `push --force` yapmak | Ekip arkadaşlarının geçmişini bozar. Gerekirse `--force-with-lease` ve **sadece kendi** feature branch'inde. |
| `git add .` ile körlemesine commit | `bin/`, `.env`, gizli dosyalar sızar. `git add -p` ve `git status` alışkanlığı. |
| Dev gün boyu yaşayan dev branch'ler | Büyük, acı veren merge'ler. Küçük PR + sık entegrasyon. |
| `git reset --hard` ile yerel değişiklikleri kaybetmek | Commit'lenmemiş iş `reflog`'da bile yoktur. Önce `git stash`. |
| "WIP", "fix", "asdf" commit mesajları | Geçmiş okunamaz, `bisect`/`blame` işe yaramaz. Conventional Commits. |
| Gizli bilgiyi commit'leyip "sonra silerim" demek | Geçmişte kalır. Anahtarı **iptal edin**. |
| Çakışmayı çözüp derlememek/test etmemek | Semantik çakışma main'i kırar. |
| `git pull` ile istemeden merge commit üretmek | `git config --global pull.rebase true` veya `git pull --ff-only` alışkanlığı. |
| Detached HEAD'de commit atıp branch açmadan ayrılmak | Commit'ler "kaybolur". `git switch -c kurtar` ya da `reflog`. |
| Binary/büyük dosyaları repoya koymak | Repo şişer, geçmişten temizlemek zordur. Git LFS veya artifact depolama. |

---

## 7. Performans & trade-off

- **Merge vs rebase vs squash:** merge = gerçek tarih, gürültülü graf; rebase = temiz doğrusal graf ama hash'ler değişir ve hata riski yüksek; squash = en sade `main`, ancak ince taneli `bisect` ve `blame` imkânı azalır (PR başına tek commit = bisect granülaritesi PR kadar kaba).
- **Commit boyutu:** küçük atomik commit'ler → `bisect`/`revert`/`cherry-pick` kolay; ama aşırı parçalamak da geçmişi gürültüye boğar. Kural: *her commit derlenebilir ve tek bir mantıksal değişiklik olmalı.*
- **Büyük repolar:** `git clone --depth 1` (shallow), `--filter=blob:none` (partial clone), `sparse-checkout` ve `git maintenance` klon/çalışma süresini düşürür. Monorepo vs polyrepo seçimi bunun ötesinde mimari bir karardır (→ S5).
- **Trunk-based hız kazandırır** ama güçlü otomasyon (hızlı CI, feature flag) ister; olmadan ana hat sık kırılır.
- **Branch koruma kuralları** (zorunlu review, zorunlu CI, force-push yasağı) güvenlik sağlar, hız maliyeti vardır — ekip büyüklüğüne göre ayarlayın.

---

## 8. Kaynaklar

- Pro Git (ücretsiz kitap, Türkçe çevirisi de var): <https://git-scm.com/book>
- Git komut referansı: <https://git-scm.com/docs>
- Interaktif görsel öğrenme: <https://learngitbranching.js.org>
- Atlassian — merge vs rebase: <https://www.atlassian.com/git/tutorials/merging-vs-rebasing>
- Conventional Commits: <https://www.conventionalcommits.org>
- Semantic Versioning: <https://semver.org/lang/tr/>
- Trunk Based Development: <https://trunkbaseddevelopment.com>
- GitHub Flow: <https://docs.github.com/en/get-started/using-github/github-flow>
- Resmî .NET `.gitignore` şablonu: <https://github.com/github/gitignore/blob/main/VisualStudio.gitignore>
