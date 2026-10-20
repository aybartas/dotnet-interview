#!/usr/bin/env bash
# curl -v ile TLS, header ve HTTP sürümü incelemesi. Önce uygulamayı başlatın:  dotnet run -- --https
# Kullanım:  ./curl-examples.sh            (hepsini sırayla çalıştırır, çıktıyı açıklar)
set -uo pipefail

HTTP=http://localhost:5080
HTTPS=https://localhost:5443
# -k: Geliştirme sertifikası güvenilir değilse doğrulamayı atla (SADECE yerel deney için; prod'da ASLA)
CURL=(curl --silent --show-error)

section() { printf '\n\033[1;36m==> %s\033[0m\n' "$*"; }

section "1) -v: istek (>) ve yanıt (<) header'ları. '*' ile başlayan satırlar bağlantı bilgisidir (TCP connect)"
"${CURL[@]}" -v "$HTTP/hello" -o /dev/null 2>&1 | grep -E '^\*   Trying|^\* Connected|^> |^< '

section "2) -I: sadece header'lar (HEAD isteği). HEAD = GET'in body'siz hali (safe + idempotent)"
"${CURL[@]}" -I "$HTTP/hello"

section "3) TLS handshake ayrıntıları (HTTPS): sürüm, cipher, ALPN, sertifika"
"${CURL[@]}" -kv "$HTTPS/hello" -o /dev/null 2>&1 | grep -E 'ALPN|SSL connection|TLSv|subject:|issuer:|expire date|start date|using HTTP|HTTP/'

section "4) HTTP/1.1 ile HTTP/2 karşılaştırması (aynı TLS bağlantısı, ALPN pazarlığı)"
echo "--http1.1:"; "${CURL[@]}" -k --http1.1 -sI "$HTTPS/hello" | head -1
echo "--http2:  "; "${CURL[@]}" -k --http2   -sI "$HTTPS/hello" | head -1

section "5) HSTS header'ı (yalnızca HTTPS yanıtında)"
"${CURL[@]}" -k -sI "$HTTPS/hello" | grep -i strict-transport || echo "(yok — --https ile başlattınız mı?)"

section "6) Redirect: -L takip eder, -i header gösterir"
echo "-- takip ETMEDEN:"; "${CURL[@]}" -si "$HTTP/redirect" | grep -E '^HTTP|^Location|^location'
echo "-- -L ile:"; "${CURL[@]}" -sL -w '  (son status: %{http_code}, toplam yönlendirme: %{num_redirects})\n' "$HTTP/redirect"

section "7) Cookie jar: -c yazar, -b gönderir. Jar olmadan sunucu seni tanımaz (stateless)"
JAR="$(mktemp)"
"${CURL[@]}" -c "$JAR" "$HTTP/cookie/set"; echo
echo -n "jar ile:   "; "${CURL[@]}" -b "$JAR" "$HTTP/cookie/read"; echo
echo -n "jar olmadan: "; "${CURL[@]}" "$HTTP/cookie/read"; echo
rm -f "$JAR"

section "8) Conditional GET: ETag → If-None-Match → 304"
ETAG=$("${CURL[@]}" -s -D - -o /dev/null "$HTTP/cache/etag" | grep -i '^etag' | awk '{print $2}' | tr -d '\r')
echo "ETag: $ETAG"
"${CURL[@]}" -si -H "If-None-Match: $ETAG" "$HTTP/cache/etag" | head -1

section "9) Gecikme dökümü: -w ile hangi aşama ne kadar sürdü? (DNS → TCP → TLS → ilk bayt)"
"${CURL[@]}" -k -o /dev/null -w \
'  dns:        %{time_namelookup}s
  tcp connect: %{time_connect}s       ← 1 RTT (handshake)
  tls bitti:   %{time_appconnect}s       ← TLS handshake eklenir (HTTP için 0)
  ilk bayt:    %{time_starttransfer}s
  toplam:      %{time_total}s
  http sürümü: %{http_version}
' "$HTTPS/hello"
