#!/usr/bin/env bash
# Senaryo 3 — git bisect
# 40 commit'lik bir geçmişte bir noktada "KDV hesabı" bozuluyor. Hangi commit?
# İkili arama: 40 commit için en fazla ~6 adım (log2 40).
#
# Kullanım:
#   ./03-bisect.sh            # repoyu hazırlar, bisect'i SİZ yürütürsünüz (manuel)
#   ./03-bisect.sh --auto     # `git bisect run` ile otomatik bulur
source "$(dirname "$0")/lib.sh"

TOTAL=40
# Hatalı commit'in numarası her çalıştırmada değişir ki ezberlenemesin (sabit için BUG_AT=17 ./03-bisect.sh)
BUG_AT="${BUG_AT:-$(( (RANDOM % 30) + 6 ))}"

new_repo bisect

say "$TOTAL commit üretiliyor (hata bunlardan birinde saklı)…"
# Test script'i: KDV'li toplam 118 değilse başarısız (exit 1). `git bisect run` bunu kullanır.
cat > check.sh <<'SH'
#!/usr/bin/env bash
# exit 0 = iyi · exit 1 = kötü · exit 125 = test edilemez (skip)
source ./vat.env
result=$(( 100 * (100 + VAT_PERCENT) / 100 ))
[[ "$result" -eq 118 ]]
SH
chmod +x check.sh
echo "VAT_PERCENT=18" > vat.env
echo "# Değişiklik günlüğü" > CHANGELOG.md
git add . && git commit --quiet -m "chore: ilk commit"

for i in $(seq 1 $TOTAL); do
  echo "- değişiklik $i" >> CHANGELOG.md
  if [[ "$i" -eq "$BUG_AT" ]]; then
    echo "VAT_PERCENT=8" > vat.env          # SESSİZCE bozulur: sadece commit mesajı masum görünür
    git add . && git commit --quiet -m "refactor: konfigürasyon temizliği (#$i)"
  else
    git add . && git commit --quiet -m "docs: changelog güncellemesi (#$i)"
  fi
done
echo "Hazır. Toplam commit: $(git rev-list --count HEAD)"
run git log --oneline -5

if [[ "${1:-}" == "--auto" ]]; then
  say "Otomatik bisect: kötü = HEAD, iyi = ilk commit"
  FIRST=$(git rev-list --max-parents=0 HEAD)
  git bisect start HEAD "$FIRST" >/dev/null
  git bisect run ./check.sh | grep -E 'running|is the first bad commit|^[0-9a-f]{40}|refactor' || true
  echo
  say "Suçlu commit:"
  run git show --stat --format='%h %s' refs/bisect/bad
  git bisect reset >/dev/null 2>&1
  echo
  echo "(Gerçek hatalı commit numarası: #$BUG_AT — bisect'in bulduğuyla aynı olmalı)"
else
  cat <<MSG

──────────────────────────────────────────────────────────────
Manuel bisect:
  cd $SANDBOX_DIR/bisect
  ./check.sh; echo \$?              # HEAD'de 1 dönmeli (hata var)
  git bisect start
  git bisect bad                    # HEAD kötü
  git bisect good \$(git rev-list --max-parents=0 HEAD)   # ilk commit iyi
  # git ortadaki bir commit'e geçer → ./check.sh çalıştırın → 'git bisect good' / 'git bisect bad'
  # …"<hash> is the first bad commit" yazana kadar tekrarlayın
  git bisect reset                  # başlangıç noktasına dön

Otomatik hali:
  git bisect start HEAD \$(git rev-list --max-parents=0 HEAD)
  git bisect run ./check.sh
──────────────────────────────────────────────────────────────
MSG
fi
