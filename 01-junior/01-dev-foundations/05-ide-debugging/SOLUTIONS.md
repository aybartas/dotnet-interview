# J1.5 — Debugger Egzersiz Çözümleri

> ⚠️ **Spoiler!** Önce [`src/debugging-exercises`](src/debugging-exercises/) içindeki egzersizleri debugger ile kendiniz çözmeye çalışın.
> Bir egzersiz için ipucu: `dotnet run -- <numara> --hint`

Her çözümde üç şey var: **kök neden**, **hangi debugger özelliğiyle bulunur**, **düzeltme**.

---

## 1. Döngü: 1'den n'e toplam
- **Kök neden:** `for (var i = 1; i < n; i++)` — `n` dahil edilmiyor (**off-by-one**). `SumUpTo(1)` döngüye hiç girmez → 0.
- **Debugger ile:** `sum += i` satırına breakpoint → F5/F10 ile ilerleyin; **Watch**'a `i` ve `sum` ekleyin. Son iterasyonda `i == n - 1` olduğunu görürsünüz.
- **Düzeltme:** `i <= n`. (Kalıcı çözüm: sınır değerleri — `n = 0, 1, 10` — için birim testi yazmak, → J10.1.)

## 2. NullReferenceException zinciri
- **Kök neden:** "Zeynep"in `Address`'i `null`; `CityOf` içinde `c.Address!.City` — `!` operatörü derleyiciye "eminim, null değil" der ve uyarıyı susturur, ama null'ı ortadan kaldırmaz.
- **Debugger ile:** **Exception Settings** (Ctrl+Alt+E) → `System.NullReferenceException`'ı işaretleyin; istisna fırlatıldığı anda durur. **Locals**'ta `c.Address == null` görülür; **Call Stack**'te `ShippingLabel → CityOf` zincirini izlersiniz.
- **Düzeltme:** `c.Address?.City.ToUpperInvariant() ?? "adres yok"` — `!` yerine gerçek null kontrolü. Genel kural: `!`'i, null olamayacağını kanıtlayabildiğiniz yerlerde kullanın; "uyarıyı sustur" aracı değildir.

## 3. foreach sırasında koleksiyonu değiştirmek
- **Kök neden:** `List<T>` her değişiklikte iç `_version` sayacını artırır; enumerator `MoveNext`'te bunu kontrol eder ve değişmişse `InvalidOperationException` fırlatır. Belirti `foreach`'te çıkar, ama kök neden döngü gövdesindeki `Remove`'dur.
- **Debugger ile:** Exception Settings'te `InvalidOperationException`'ı işaretleyin; **Call Stack**'te `List<T>.Enumerator.MoveNext` görünür; bir önceki çerçeveye geçince hangi `Remove` çağrısından sonra olduğu anlaşılır.
- **Düzeltme:** `orders.RemoveAll(o => o.IsCancelled);` ya da `foreach (var o in orders.ToList())` (kopya üzerinde dön).

## 4. Döngüde yakalanan değişken (closure)
- **Kök neden:** `for` döngüsünde `i` **tek bir değişkendir**; lambda'lar değeri değil **değişkenin kendisini** yakalar. Lambda'lar döngü bittikten sonra çalışınca `i == 3` olduğu için hepsi `30` döner. (`foreach` değişkeni C# 5'ten beri her iterasyonda yenidir; `for` değişkeni değildir.)
- **Debugger ile:** Lambda içine breakpoint → **Locals**'ta `this` altında derleyicinin ürettiği closure sınıfını (`<>c__DisplayClass…`) ve tek bir `i` alanını görürsünüz.
- **Düzeltme:** Döngü içinde kopya al: `var copy = i; actions.Add(() => copy * 10);`.

## 5. Tamsayı bölme ve kayan nokta tuzakları
- **Kök neden:** (a) `percent / 100` → `int / int` = `0` (tamsayı bölme), indirim hiç uygulanmaz. (b) `values.Sum() / values.Length` → `int / int`; sonuç `double`'a sonradan çevrilse bile kesir kaybolmuştur. (c) `0.1 + 0.2 == 0.3` → ikili kayan noktada tam gösterilemeyen değerler; eşitlik karşılaştırması güvenilmez.
- **Debugger ile:** **Immediate window**: `percent / 100` → `0`, `(decimal)percent / 100` → `0.10`, `0.1 + 0.2` → `0.30000000000000004`. İfadeleri çalışırken deneyip doğrulayın.
- **Düzeltme:** `price - price * percent / 100m` (ya da `decimal` tipine çevirin); `values.Average()` / `(double)values.Sum() / values.Length`; para için `decimal`, kayan nokta karşılaştırmasında tolerans (`Math.Abs(a - b) < 1e-9`) kullanın (→ J2.1, J2.2).

## 6. 10.000 kayıttan birindeki bozuk veri
- **Kök neden:** `Id == 7345` olan kaydın `Quantity` değeri `-3` (veri kaynağı hatası).
- **Debugger ile:** Döngüdeki `actual += …` satırına **koşullu breakpoint**: `order.Quantity < 0`. 10.000 iterasyon yerine tam o kayıtta durur. Alternatif: **tracepoint** (sağ tık → "Log a message": `{order.Id}: {order.Quantity}`) — durmadan Output'a yazar, `grep` benzeri filtreyle bakılır. Üçüncü yol: **Hit count** (örn. "7345. iterasyonda dur").
- **Düzeltme:** Hata kaynağı **veri girişidir**: yükleme/doğrulama katmanında `Quantity > 0` kontrolü (veri sınırında doğrula) ve bu kontrolü kapsayan bir birim testi.

## 7. Unutulan await
- **Kök neden:** `SaveAsync` çağrılır ama beklenmez (`await` yok); `Count` kontrolü kayıt işlemi bitmeden yapılır. Sonucu bir değişkene atamak derleyici uyarısını (CS4014) susturur — yani bu hatanın bu biçimi derleyiciden kaçar.
- **Debugger ile:** `Report` satırında **Watch**'a `task.Status` ekleyin → `WaitingForActivation`. **Parallel Tasks / Tasks** penceresi (VS: Debug → Windows) bekleyen task'ı gösterir. F10 ile `await` satırını geçerken kontrolün başka bir yere atlayıp geri döndüğünü gözlemleyin.
- **Düzeltme:** `await repo.SaveAsync("kayıt-1");`. Önleme: `CS4014`'ü uyarı/hata yapmak (`.editorconfig`'te yapıldı), `Task` döndüren çağrıların sonucunu asla yok saymamak (→ J4).

## 8. Aynı nesneyi paylaşan iki "farklı" kullanıcı
- **Kök neden:** `"guest"` rolü için her çağrıda **aynı** `GuestDefaults` listesi döner; `aliGuest.Add("write")` paylaşılan listeyi değiştirir, Veli de etkilenir (referans tipi + paylaşılan değişken durum).
- **Debugger ile:** **Watch** penceresinde `aliGuest` → sağ tık → **Make Object ID** → `{1#}`; `veliGuest` için de `{1#}` gelir → aynı nesne.
- **Düzeltme:** Her çağrıda yeni liste ya da değiştirilemez görünüm: `=> [.. GuestDefaults]` veya `IReadOnlyList<string>` döndürmek (tasarım olarak "dışarıya değiştirilebilir iç durum sızdırmamak").

## 9. Debug'da doğru, Release'de farklı sonuç
- **Kök neden:** `Debug.Assert(NextId() > 0)` — `Debug.*` çağrıları `[Conditional("DEBUG")]` ile işaretlidir; **Release derlemesinde çağrı ve argüman ifadeleri tamamen silinir**. İfadenin yan etkisi (`NextId()` sayacı artırır) yalnızca Debug'da gerçekleşir.
- **Debugger ile:** İki yapılandırmayı karşılaştırın: `dotnet run -- 9` ve `dotnet run -c Release -- 9`. IDE'de Release'de breakpoint'ler/Locals optimize edilmiş kodda eksik görünebilir.
- **Düzeltme:** Assert/log ifadelerinde **yan etki olmasın**: `var id = NextId(); Debug.Assert(id > 0);`. Prodüksiyonda gerekli doğrulamalar `Debug.Assert` değil gerçek kontrol/istisna olmalı.
