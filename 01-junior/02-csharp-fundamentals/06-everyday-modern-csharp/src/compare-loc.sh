#!/usr/bin/env bash
# İki stilin domain model dosyalarının "anlamlı satır" sayısını karşılaştırır (boş satır ve yalnız-yorum satırları hariç).
# Aynı sayı: `dotnet run --project ModernCSharp -- 10`
set -euo pipefail
cd "$(dirname "$0")/ModernCSharp"

count() { grep -vE '^\s*($|//)' "$1" | wc -l | tr -d ' '; }

a=$(count Csharp7Style/Domain.cs)
b=$(count Csharp14Style/Domain.cs)
printf '%-22s %5s satır\n' "C# 7 tarzı"  "$a"
printf '%-22s %5s satır\n' "C# 14 tarzı" "$b"
awk -v a="$a" -v b="$b" 'BEGIN { printf "Fark: %d satır (%%%.0f daha az)\n", a-b, 100-100*b/a }'
