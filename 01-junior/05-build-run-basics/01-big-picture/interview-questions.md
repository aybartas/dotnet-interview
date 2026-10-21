# J5.1 C# → IL → Native: Büyük Resim — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan. Modül (J5) sorularının tamamı için J5.2 ve J5.3 dosyalarına da bakın.

### 1. ⭐ C# kodu nasıl `.exe` oluyor?
Olmuyor: derleyici (Roslyn) C# kodunu **IL + metadata içeren bir `.dll`**'e (assembly) çevirir; senin kodun her zaman `.dll`'dedir. `.exe` (apphost), içinde C# kodu olmayan küçük, native bir launcher'dır: `runtimeconfig.json`'dan hangi runtime'ın gerektiğini öğrenir, .NET'i bulur ve `.dll`'i başlatır. Çalışma zamanında **JIT** IL'i metotlar ilk çağrıldıkça makine koduna çevirir. (`dotnet MyApp.dll` ile apphost'a hiç gerek kalmadan da çalışır.)

### 2. ⭐ IL nedir, neden vardır?
IL (Intermediate Language / CIL), derleyicinin ürettiği yığın (stack) tabanlı, platform ve işlemciden bağımsız ara dildir. Neden: dil bağımsızlığı (C#, F#, VB aynı IL'e derlenir) ve platform bağımsızlığı (aynı `.dll` x64/ARM64, Windows/Linux'ta çalışır); platforma özel makine kodunu JIT hedef makinede üretir. Örnek: `a + b` → `ldarg.0, ldarg.1, add, ret`.

### 3. JIT nedir? (bir cümleyle)
JIT (Just-In-Time) derleyicisi, IL'i bir metot ilk çağrıldığında çalışma zamanında makine koduna çevirir ve sonraki çağrılarda o kodu kullanır. Tiered compilation ile önce hızlı/az optimize kod üretir, sık çağrılan metotları sonradan daha iyi optimize eder.

### 4. ⭐ Debug ile Release farkı nedir?
Debug'da derleyici/JIT optimizasyonu kapalıdır (IL'de `nop`'lar, gereksiz yerel değişkenler), `DEBUG` sembolü tanımlıdır, `Debug.Assert`/`[Conditional("DEBUG")]` çağrıları çalışır ve debugger her değişkeni satır satır gösterir. Release'de optimizasyon açıktır (daha küçük/hızlı kod), `DEBUG` yoktur, `Debug.*` çağrıları ve **argüman ifadeleri derlemeden silinir**, debugger'da değişkenler "optimized away" olabilir. `build/run` varsayılanı Debug, `publish` varsayılanı Release'dir (.NET 8+).

### 5. "Debug'da çalışıyor, Release'de çalışmıyor" neden olabilir?
Yan etkili ifadenin `Debug.Assert` içinde olması (Release'de hiç çalışmaz), `#if DEBUG` ile farklılaşan kod yolları, optimizasyona duyarlı/zamanlamaya bağlı kod (race condition, başlatılmamış durum), değişkenin ömrünün Release'de kısalması. Çözüm: iki yapılandırmayı da test etmek, yan etkiyi assert dışına almak.

### 6. Compile-time error ile runtime exception farkı nedir?
Compile-time hatada derleyici kodu reddeder; `.dll` üretilmez, program hiç çalışmaz (örn. `CS0029` tür uyuşmazlığı). Runtime exception kod derlenip çalıştıktan sonra o satır işlenince fırlar (`NullReferenceException`, `InvalidCastException`). Derleme zamanı hatası ucuzdur (geliştirici anında görür); runtime hatası pahalıdır (üretimde, belki veriye bağlı). Strateji hataları tipler, nullable reference types, analyzer'larla derleme zamanına çekmektir.

### 7. Assembly nedir?
.NET'in dağıtım/derleme birimidir: IL kodu, metadata (tip ve metot tabloları, referanslar) ve manifest içeren `.dll` (veya `.exe`). Bir proje bir assembly üretir; `internal` erişim assembly sınırıdır (→ J2.1).

### 8. Managed ve native kod farkı nedir?
Managed kod, CLR tarafından yönetilen koddur (IL olarak derlenir; GC, tip güvenliği, JIT); `.dll` yönetilen bir PE dosyasıdır (CLR başlığı + metadata vardır). Native kod doğrudan işlemci makine koduna derlenmiş koddur (C/C++ ve apphost gibi); CLR metadata'sı yoktur.

### 9. `foreach` derleyicide nasıl çalışır?
Koleksiyon bir diziyse indeksli `for` döngüsüne iner. Aksi halde `GetEnumerator()` çağrılır; `try { while (e.MoveNext()) { var x = e.Current; ... } } finally { e.Dispose(); }` biçiminde açılır. `List<T>.Enumerator` struct olduğu için kutulanmaz. `foreach` içinde koleksiyonu değiştirmek `InvalidOperationException` verir (enumerator sürüm kontrolü).

### 10. `using` ifadesi derleyicide neye açılır?
`try/finally` bloğuna: `var r = new X(); try { ... } finally { if (r != null) r.Dispose(); }`. Bu yüzden istisna olsa bile `Dispose` çağrılır. `await using` aynı kalıbın `DisposeAsync` ile async halidir.

### 11. Uygulama başlarken apphost, hostfxr ve hostpolicy ne yapar?
apphost `runtimeconfig.json`'u okuyup hangi runtime/framework sürümünün gerektiğini öğrenir. **hostfxr** makinede uygun .NET sürümünü seçer (roll-forward kuralları). **hostpolicy** `deps.json`'a göre bağımlılık assembly yollarını çözer. Ardından **CoreCLR** `.dll`'i yükler ve JIT çalışır. Runtime eksikse "You must install or update .NET" / "Framework ... was not found" hatası alınır.

### 12. Native AOT ve ReadyToRun nedir? JIT'ten farkı?
Her ikisi de IL'i çalışma zamanından **önce** makine koduna çevirir (başlangıç gecikmesini azaltır). ReadyToRun hâlâ IL'i ve JIT'i taşır (önceden derlenmiş kod başlangıçta kullanılır, sonra tiered JIT devralır). Native AOT yalnızca native çıktı üretir, JIT/IL yoktur (çok hızlı başlangıç, küçük bellek; yansıma/dinamik kod kısıtları). Derin hali → M2.5.

### 13. Aynı `.dll` neden Windows ve Linux'ta çalışabilir?
Çünkü içinde platforma özel makine kodu değil IL vardır; her platformdaki runtime IL'i o platformun işlemcisi için JIT ile derler. Platforma özel olan yalnızca apphost (ve self-contained yayında runtime) dosyalarıdır.
