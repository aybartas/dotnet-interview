#!/usr/bin/env bash
# const'ın çağıran assembly'ye GÖMÜLDÜĞÜNÜ kanıtlar.
#   1) App'i Lib v1 ile derle ve çalıştır           → MaxRetries=3, Timeout=30
#   2) SADECE Lib'i v2 olarak yeniden derle, yeni Lib.dll'i App'in yanına kopyala (App'i YENİDEN DERLEME!)
#   3) App'i tekrar çalıştır                        → MaxRetries HÂLÂ 3 (gömülü), Timeout=60 (çalışırken okundu)
#   4) App'i yeniden derle                          → MaxRetries artık 5
set -euo pipefail
cd "$(dirname "$0")"

say() { printf '\n\033[1;36m==> %s\033[0m\n' "$*"; }
APP_OUT="App/bin/Debug/net10.0"

say "1) Lib v1 ile derle ve çalıştır"
dotnet build App -nologo -v q >/dev/null
dotnet "$APP_OUT/App.dll"

say "2) SADECE Lib'i v2 olarak derle ve Lib.dll'i App'in klasörüne kopyala (App yeniden derlenmiyor)"
dotnet build Lib -nologo -v q -p:DefineConstants=V2 -o /tmp/const-embedding-lib-v2 >/dev/null
cp /tmp/const-embedding-lib-v2/Lib.dll "$APP_OUT/Lib.dll"

say "3) App'i (eski derlemesiyle) tekrar çalıştır — Lib.dll artık v2"
dotnet "$APP_OUT/App.dll"
echo "   → Version v2 görünüyor (Lib gerçekten değişti) ama MaxRetries hâlâ 3: const App'in IL'ine gömülmüştü."

say "4) App'i yeniden derle (Lib v2 ile) — const değeri güncellenir"
dotnet build App -nologo -v q -p:DefineConstants=V2 >/dev/null
dotnet "$APP_OUT/App.dll"

rm -rf /tmp/const-embedding-lib-v2
