#!/usr/bin/env bash
# Ortak yardımcılar: alıştırma repolarını src/sandbox/ altında, deterministik ayarlarla oluşturur.
# Doğrudan çalıştırılmaz; diğer script'ler `source lib.sh` ile kullanır.

set -euo pipefail

SRC_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
SANDBOX_DIR="$SRC_DIR/sandbox"

# Çıktıyı okunur kılan küçük yardımcılar
say()  { printf '\n\033[1;36m==> %s\033[0m\n' "$*"; }
run()  { printf '\033[0;33m$ %s\033[0m\n' "$*"; "$@"; }

# new_repo <ad>  → sandbox/<ad> altında sıfır bir repo açar ve içine girer
new_repo() {
  local name="$1"
  local dir="$SANDBOX_DIR/$name"
  rm -rf "$dir"
  mkdir -p "$dir"
  cd "$dir"
  git init --quiet --initial-branch=main
  # Global git ayarınıza dokunmamak için sadece bu repoya yazılır
  git config user.name  "Alistirma Gelistirici"
  git config user.email "alistirma@example.com"
  git config commit.gpgsign false
  # Çakışma işaretlerinde ortak atayı da göster (diff3): çözmeyi çok kolaylaştırır
  git config merge.conflictStyle diff3
}

# commit_file <dosya> <mesaj>  → dosyayı stage'ler ve commit'ler
commit_file() {
  git add "$1"
  git commit --quiet -m "$2"
}
