#!/usr/bin/env bash
# TCP three-way handshake ve bağlantı kapanışını tcpdump ile yakalar (loopback üzerinde).
#
# Gereksinim: tcpdump + nc (netcat), root yetkisi (sudo). Linux/macOS.
#   ./capture-handshake.sh        # .NET gerekmez; nc ile sunucu/istemci kurar
#
# Wireshark ile yapmak isterseniz: Loopback arayüzünü seçin, filtre: tcp.port == 5000
# Aranacak: [S] SYN → [S.] SYN-ACK → [.] ACK   (bağlanma)   ·   [F.] FIN-ACK, [.] ACK ... (kapanış)
set -euo pipefail

PORT="${1:-5000}"
IFACE="lo"
[[ "$(uname)" == "Darwin" ]] && IFACE="lo0"

if [[ $EUID -ne 0 ]]; then
  echo "tcpdump root ister:  sudo $0 $PORT" >&2
  exit 1
fi
command -v tcpdump >/dev/null || { echo "tcpdump kurulu değil (apt install tcpdump)" >&2; exit 1; }
command -v nc >/dev/null      || { echo "nc kurulu değil (apt install netcat-openbsd)" >&2; exit 1; }

OUT="$(mktemp)"
trap 'kill $(jobs -p) 2>/dev/null || true; rm -f "$OUT"' EXIT

echo "==> tcpdump başlıyor (arayüz: $IFACE, port: $PORT)"
# -n: isim çözme yok · -S: mutlak sequence number göster
tcpdump -i "$IFACE" -n -S "tcp port $PORT" > "$OUT" 2>/dev/null &
sleep 1

echo "==> sunucu: nc dinliyor"
nc -l 127.0.0.1 "$PORT" >/dev/null &
sleep 0.5

echo "==> istemci: bağlan, 1 mesaj gönder, kapat"
printf 'merhaba\n' | nc -q 1 127.0.0.1 "$PORT" 2>/dev/null || printf 'merhaba\n' | nc 127.0.0.1 "$PORT"
sleep 1
kill %1 2>/dev/null || true
sleep 0.5

echo
echo "==> Yakalanan paketler  (Flags: S=SYN, S.=SYN-ACK, .=ACK, P.=PSH-ACK (veri), F.=FIN-ACK)"
cat "$OUT"
cat <<'MSG'

Nasıl okunur:
  1) [S]    istemci → sunucu : "bağlanmak istiyorum"      (seq = x)
  2) [S.]   sunucu  → istemci: "tamam, ben de hazırım"     (seq = y, ack = x+1)
  3) [.]    istemci → sunucu : "aldım"                     (ack = y+1)     → bağlantı kuruldu, 1 RTT
  4) [P.]   veri paketi ("merhaba\n" = 8 byte; length 8)
  5) [F.] ... [F.] ... [.]  kapanış: her yön ayrı FIN ile kapanır (dörtlü); ilk FIN'i gönderen TIME_WAIT'e girer
MSG
