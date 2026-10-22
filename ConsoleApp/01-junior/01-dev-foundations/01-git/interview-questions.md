# J1.1 Git — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan.

### 1. ⭐ `merge` ile `rebase` arasındaki fark nedir?
`merge` iki branch'in uçlarını birleştiren yeni bir **merge commit** üretir; mevcut commit'lere dokunmaz, gerçek (doğrusal olmayan) geçmişi korur. `rebase` ise feature branch'indeki commit'leri hedef branch'in ucuna **yeniden oynatır**; içerik aynı kalsa da commit'ler yeni hash alır, yani geçmiş yeniden yazılır ve sonuç doğrusal olur. Bu yüzden rebase **yalnızca henüz paylaşılmamış (push edilmemiş veya sadece benim kullandığım) branch'lerde** güvenlidir. Pratik kural: feature branch'imi `main`'e güncel tutmak için rebase, feature'ı `main`'e almak için PR + (squash) merge.

### 2. Fast-forward merge nedir? `--no-ff` ne yapar?
Hedef branch, birleştirilen branch'in çıkış noktasından ileri gitmediyse Git yeni commit üretmeden sadece branch işaretçisini ileri kaydırır — buna fast-forward denir. `--no-ff`, fast-forward mümkün olsa bile merge commit üretir (feature'ın tek bir birim olarak izi kalır). `--ff-only` ise yalnızca fast-forward mümkünse birleştirir, değilse hata verir.

### 3. ⭐ `git reset` ile `git revert` farkı nedir?
`reset` branch işaretçisini **geriye** taşır — commit'ler geçmişten "düşer" (`--soft`: stage ve dosyalar kalır; `--mixed`: dosyalar kalır, stage sıfırlanır; `--hard`: ikisi de sıfırlanır). `revert` geçmişi değiştirmez; eski bir commit'in etkisini tersine çeviren **yeni bir commit** ekler. Paylaşılmış branch'te `revert` kullanılır, çünkü `reset` + `force push` başkalarının elindeki geçmişle ıraksama yaratır.

### 4. `git reset --hard` ile yanlışlıkla commit kaybettim. Kurtarabilir miyim?
Commit'lenmiş işler genellikle evet: `git reflog` HEAD'in son hareketlerini listeler (varsayılan ~90 gün); kaybolan commit'in hash'ini bulup `git reset --hard <hash>` veya `git switch -c rescue <hash>` yaparım. **Commit'lenmemiş** working tree değişiklikleri `reset --hard` ile gitmişse genellikle kurtarılamaz — bu yüzden riskli işlemden önce `git stash` veya geçici commit.

### 5. Merge conflict nasıl çıkar, nasıl çözersin?
İki branch aynı dosyanın aynı bölgesini farklı değiştirdiğinde (veya biri silip diğeri değiştirdiğinde) Git otomatik birleştiremez ve dosyaya `<<<<<<<`, `=======`, `>>>>>>>` işaretleri koyar. Çözüm: iki tarafın **niyetini** anlamak (gerekirse yazarla konuşmak), kodu birleştirip işaretleri kaldırmak, `git add`, ardından `git commit` (merge) ya da `git rebase --continue`. Sonunda **derleyip testleri çalıştırırım**, çünkü metinsel çakışma olmasa da semantik çakışma olabilir. Önlemek için küçük, kısa ömürlü branch'ler kullanılır.

### 6. Rebase sırasında çakışma çıktı. Merge'den farkı ne?
Rebase commit'leri tek tek yeniden oynattığı için çakışma **commit başına** çıkabilir ve her biri ayrı çözülür (`git add` → `git rebase --continue`). Vazgeçmek için `git rebase --abort`. Merge'de tüm çakışmalar tek seferde, tek merge commit içinde çözülür.

### 7. Paylaşılmış geçmişi neden yeniden yazmamalıyız?
Rebase/amend/reset sonrası commit'ler yeni hash alır. Eski commit'leri zaten çekmiş takım arkadaşlarının yerel geçmişi ile sunucudaki geçmiş ıraksar; ikisini birleştirmeye çalışınca aynı değişiklikler çift commit olarak görünür ya da "silinen" commit'ler geri gelir. Güvenli yol `revert`. Kendi feature branch'imi rebase edip itmem gerekiyorsa `--force-with-lease` kullanırım (başkası araya push ettiyse reddeder; düz `--force` bunu kontrol etmez).

### 8. `git stash` ne zaman kullanılır?
Yarım kalmış, commit'lemek istemediğim değişikliklerle başka bir branch'e (ör. acil hotfix için) geçmem gerektiğinde. `git stash push -m "description"` ile kaldırır, işim bitince `git stash pop` ile geri getiririm. İzlenmeyen (untracked) dosyalar için `-u` gerekir.

### 9. `git cherry-pick` nedir, ne zaman kullanılır?
Başka bir branch'teki **belirli bir commit'in** değişikliğini bulunduğum branch'e yeni bir commit olarak kopyalar. Tipik senaryo: `main`'de yapılan hata düzeltmesini eski bir `release/x` hattına taşımak (backport). Dezavantajı: aynı değişiklik iki farklı hash'le yaşar; sık kullanmak ileride merge gürültüsü yaratır.

### 10. ⭐ `git bisect` nasıl çalışır?
Bir hatanın hangi commit'te girdiğini **ikili aramayla** bulur. Bilinen bir kötü (genelde HEAD) ve bilinen bir iyi commit verilir; Git aradaki commit'e geçer, ben test edip `good`/`bad` derim, aralık her adımda yarıya iner — N commit için ~log₂N adım. `git bisect run <command>` ile otomatikleştirilir (exit code 0 = iyi, 1–127 = kötü, 125 = atla). Küçük ve derlenebilir commit'ler bisect'i çok daha verimli kılar.

### 11. Pull request akışını anlat. İyi bir PR'ın özellikleri neler?
`main`'den kısa ömürlü bir branch aç → küçük commit'ler → PR aç (ne ve neden açıklaması) → CI (build/test/lint) yeşil → review → düzeltmeler → onay → merge. İyi PR: **küçük** (kabaca 200–400 satır altı), **tek amaçlı**, açıklaması ve testi olan, refactoring ile özellik karışmamış, kendi kendimin önce review ettiği PR'dır. Küçük PR daha hızlı ve daha kaliteli review alır, çakışma riski düşer, geri alması kolaydır.

### 12. Code review'da neye bakarsın, nasıl geri bildirim verirsin?
Doğruluk (edge case, null, hata yolları), test kapsamı, okunabilirlik, güvenlik (girdi doğrulama, secret), performans ve mimariyle uyum. Stil tartışmasını formatter/analyzer'a bırakırım. Yorumları "blocker" ve "nit/öneri" diye ayırır, emir yerine soru ve gerekçe kullanırım. Review aynı zamanda bilgi paylaşımıdır.

### 13. ⭐ GitHub Flow, Trunk-based ve Git Flow arasındaki farklar? Hangisini ne zaman seçersin?
- **GitHub Flow:** `main` her zaman deploy edilebilir; kısa ömürlü feature branch + PR; merge ≈ deploy. Çoğu web/API ekibi için varsayılan.
- **Trunk-based:** herkes çok sık (günde en az bir kez) küçük değişikliği `main`'e gönderir; bitmemiş iş **feature flag** arkasında gizlenir. Güçlü CI/test ve olgun ekip ister; en yüksek teslimat hızı.
- **Git Flow:** `main` + `develop` + `feature/release/hotfix` branch'leri. Birden çok sürümü paralel destekleyen, sürümlü ürünler (mobil, masaüstü, kütüphane) için; sürekli teslimat için ağır ve merge acısı yüksek.
Seçim: deploy sıklığı ve aynı anda desteklenen sürüm sayısı.

### 14. Conventional Commits nedir, SemVer ile nasıl ilişkilidir?
Commit mesajına `type(scope): description` yapısı verir (`feat`, `fix`, `docs`, `refactor`, `test`, `chore`…). Araçlar bunu okuyup changelog ve sürüm numarası üretebilir: `fix` → PATCH, `feat` → MINOR, `feat!:`/`BREAKING CHANGE:` → MAJOR. SemVer `MAJOR.MINOR.PATCH`'tir: MAJOR geriye uyumsuz, MINOR geriye uyumlu yeni özellik, PATCH geriye uyumlu hata düzeltmesi.

### 15. `1.4.2` sürümünde geriye uyumsuz bir API değişikliği yaptım. Yeni sürüm ne olmalı?
`2.0.0` — MAJOR artar, MINOR ve PATCH sıfırlanır. (Yeni geriye uyumlu özellik olsaydı `1.5.0`, sadece hata düzeltmesi olsaydı `1.4.3`.) Not: `0.x.y` sürümlerinde API'nin kararsız olduğu kabul edilir.

### 16. .NET projesinde `.gitignore`'a neler girer? Yanlışlıkla commit'lenen bir dosyayı nasıl çıkarırsın?
`bin/`, `obj/`, `.vs/`, `*.user`, `.idea/`, `TestResults/` ve yerel gizli yapılandırma dosyaları. `dotnet new gitignore` hazır şablonu üretir. Zaten izlenen dosya için `.gitignore` tek başına yetmez: `git rm --cached <file>` ile izlemeden çıkarırım. Eğer dosya bir **secret** içeriyorsa geçmişten silmek yetmez, secret'ı **iptal edip yenilerim**.

### 17. Detached HEAD nedir?
`HEAD`'in bir branch yerine doğrudan bir commit'i (ör. `git checkout <hash>` veya etiket) göstermesi. Bu durumda atılan commit'leri hiçbir branch göstermez; ayrılınca "kaybolur" (reflog'da bir süre durur). Kalıcı kılmak için `git switch -c new-branch` ile branch açılır.

### 18. `git fetch` ile `git pull` farkı?
`fetch` uzaktaki değişiklikleri indirir ama çalışma dizinine/branch'e **dokunmaz** (`origin/main` güncellenir). `pull` = `fetch` + (merge veya `--rebase`). Ne geldiğini önce görmek için `fetch` + `git log main..origin/main`; pull davranışını `pull.rebase`/`--ff-only` ile belirlerim.

### 19. Yanlış branch'e commit attım. Ne yaparım?
Henüz push edilmediyse: yeni branch'i oluştururum (`git branch correct-branch`), yanlış branch'i `git reset --hard origin/<branch>` ile uzaktakine eşitlerim (commit correct-branch'te güvende kalır) ve doğru branch'e geçerim. Alternatif: doğru branch'te `cherry-pick`, yanlış branch'te `reset`.

### 20. Bir bug'ın hangi commit'te girdiğini bulmak için `bisect` dışında hangi araçları kullanırsın?
`git blame <file>` (satır satır son değiştiren commit), `git log -S "searched code"` (pickaxe: bu string'in sayısını değiştiren commit'ler), `git log -L :Method:file.cs` (bir metodun geçmişi), `git log -p -- path`. Hatanın yeniden üretilebilir bir testi varsa `bisect run` en hızlısıdır.
