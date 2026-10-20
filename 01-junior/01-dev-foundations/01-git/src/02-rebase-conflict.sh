#!/usr/bin/env bash
# Senaryo 2 — REBASE conflict
# Aynı çakışmanın rebase ile yaşanışı: commit'ler tek tek yeniden oynatılır, çakışma commit başına çözülür.
# Merge'den farkı: geçmiş doğrusal kalır ama commit'ler YENİ hash alır (→ paylaşılmış branch'te rebase yapılmaz).
#
# Kullanım:
#   ./02-rebase-conflict.sh           # rebase ortasında çakışmalı bırakır
#   ./02-rebase-conflict.sh --solve   # çözüp rebase'i tamamlar ve hash değişimini gösterir
source "$(dirname "$0")/lib.sh"

new_repo rebase-conflict

say "1) main: başlangıç"
cat > Pricing.cs <<'CS'
namespace Demo;

public static class Pricing
{
    public static decimal Total(decimal net) => net * 1.18m;
}
CS
commit_file Pricing.cs "chore: Pricing eklendi"

say "2) feature/currency: 2 commit — (a) Total'i yuvarlar, (b) yeni metot ekler"
git switch --quiet -c feature/currency
cat > Pricing.cs <<'CS'
namespace Demo;

public static class Pricing
{
    public static decimal Total(decimal net) => Math.Round(net * 1.18m, 2);
}
CS
commit_file Pricing.cs "feat: toplam yuvarlanır"
cat >> Pricing.cs <<'CS'

public static class Currency
{
    public static string Format(decimal value) => value.ToString("C2");
}
CS
commit_file Pricing.cs "feat: para birimi formatlayıcı"
FEATURE_BEFORE=$(git rev-parse --short HEAD)

say "3) main: KDV oranı konfigüre edilebilir yapılır (aynı satır!)"
git switch --quiet main
cat > Pricing.cs <<'CS'
namespace Demo;

public static class Pricing
{
    public const decimal VatRate = 1.20m;
    public static decimal Total(decimal net) => net * VatRate;
}
CS
commit_file Pricing.cs "refactor: KDV oranı sabite taşındı"
run git log --oneline --graph --all

say "4) feature/currency üzerinden: git rebase main (çakışma bekleniyor)"
git switch --quiet feature/currency
git rebase main || true
run git status --short
echo
cat Pricing.cs

if [[ "${1:-}" == "--solve" ]]; then
  say "Çözüm: sabiti kullan + yuvarla"
  cat > Pricing.cs <<'CS'
namespace Demo;

public static class Pricing
{
    public const decimal VatRate = 1.20m;
    public static decimal Total(decimal net) => Math.Round(net * VatRate, 2);
}
CS
  git add Pricing.cs
  GIT_EDITOR=true git rebase --continue || true
  # 2. commit de aynı dosyayı etkiliyorsa tekrar çakışabilir; çözümü uygula
  if git status --short | grep -q '^UU'; then
    cat > Pricing.cs <<'CS'
namespace Demo;

public static class Pricing
{
    public const decimal VatRate = 1.20m;
    public static decimal Total(decimal net) => Math.Round(net * VatRate, 2);
}

public static class Currency
{
    public static string Format(decimal value) => value.ToString("C2");
}
CS
    git add Pricing.cs
    GIT_EDITOR=true git rebase --continue
  fi
  say "Sonuç: doğrusal geçmiş — ve feature commit'leri YENİ hash aldı (önce: $FEATURE_BEFORE)"
  run git log --oneline --graph --all
else
  cat <<MSG

──────────────────────────────────────────────────────────────
Şimdi sıra sizde (rebase ORTASINDASINIZ — prompt'ta (feature/currency|REBASE 1/2) görünür):
  cd $SANDBOX_DIR/rebase-conflict
  1. Pricing.cs'teki çakışmayı çözün
  2. git add Pricing.cs && git rebase --continue
  3. 2. commit de çakışırsa aynısını tekrarlayın
  Vazgeçmek için:  git rebase --abort
  Sonra:  git log --oneline --graph --all   →   merge senaryosuyla geçmişin şeklini karşılaştırın
──────────────────────────────────────────────────────────────
MSG
fi
