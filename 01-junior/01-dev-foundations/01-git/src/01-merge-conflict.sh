#!/usr/bin/env bash
# Senaryo 1 — MERGE conflict
# İki geliştirici aynı metodun aynı satırını farklı şekilde değiştiriyor; merge'de çakışma çıkıyor.
#
# Kullanım:
#   ./01-merge-conflict.sh           # çakışmalı durumda bırakır, çözmeyi siz yaparsınız
#   ./01-merge-conflict.sh --solve   # çözümü de uygulayıp merge'ü tamamlar (cevap anahtarı)
source "$(dirname "$0")/lib.sh"

new_repo merge-conflict

say "1) main: başlangıç — Calculator.cs"
cat > Calculator.cs <<'CS'
namespace Demo;

public static class Calculator
{
    public static decimal ApplyDiscount(decimal price, decimal percent)
    {
        return price - price * percent / 100;
    }

    public static decimal AddVat(decimal price)
    {
        return price * 1.18m;
    }
}
CS
printf 'bin/\nobj/\n*.user\n.vs/\n' > .gitignore
git add . && git commit --quiet -m "chore: Calculator ve .gitignore eklendi"
run git log --oneline

say "2) feature/validation: ApplyDiscount'a doğrulama ekler"
git switch --quiet -c feature/validation
cat > Calculator.cs <<'CS'
namespace Demo;

public static class Calculator
{
    public static decimal ApplyDiscount(decimal price, decimal percent)
    {
        if (percent is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(percent));
        return price - price * percent / 100;
    }

    public static decimal AddVat(decimal price)
    {
        return price * 1.18m;
    }
}
CS
commit_file Calculator.cs "feat: indirim yüzdesi için doğrulama"

say "3) main: başka bir geliştirici AYNI metodu yuvarlayacak şekilde değiştirir"
git switch --quiet main
cat > Calculator.cs <<'CS'
namespace Demo;

public static class Calculator
{
    public static decimal ApplyDiscount(decimal price, decimal percent)
    {
        return Math.Round(price - price * percent / 100, 2);
    }

    public static decimal AddVat(decimal price)
    {
        return price * 1.18m;
    }
}
CS
commit_file Calculator.cs "feat: indirimli fiyat 2 basamağa yuvarlanır"
run git log --oneline --graph --all

say "4) feature/validation → main merge (çakışma bekleniyor)"
git merge feature/validation || true

say "Durum:"
run git status --short
echo
echo "Çakışan dosya (diff3 stiliyle: <<<<<<< bizim | ||||||| ortak ata | ======= onların >>>>>>>):"
cat Calculator.cs

if [[ "${1:-}" == "--solve" ]]; then
  say "Çözüm: iki değişikliği birleştir (doğrulama + yuvarlama)"
  cat > Calculator.cs <<'CS'
namespace Demo;

public static class Calculator
{
    public static decimal ApplyDiscount(decimal price, decimal percent)
    {
        if (percent is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(percent));
        return Math.Round(price - price * percent / 100, 2);
    }

    public static decimal AddVat(decimal price)
    {
        return price * 1.18m;
    }
}
CS
  git add Calculator.cs
  git commit --quiet --no-edit
  run git log --oneline --graph --all
else
  cat <<MSG

──────────────────────────────────────────────────────────────
Şimdi sıra sizde:
  cd $SANDBOX_DIR/merge-conflict
  1. Calculator.cs'i açın, işaretleri kaldırıp İKİ değişikliği de koruyun
  2. git add Calculator.cs
  3. git commit            (merge commit mesajı hazır gelir)
  Vazgeçmek için:  git merge --abort
Ayrıca deneyin:  git log --merge   ·   git diff --name-only --diff-filter=U
──────────────────────────────────────────────────────────────
MSG
fi
