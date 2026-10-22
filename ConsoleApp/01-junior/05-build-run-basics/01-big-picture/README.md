# J5.1 C# → IL → Native: Büyük Resim

> 📁 `ConsoleApp/01-junior/05-build-run-basics/01-big-picture/` · Önkoşul: J1.4 · Sorular: [`interview-questions.md`](interview-questions.md)

## İçindekiler

1. [Ne?](#1-ne)
2. [Neden?](#2-neden)
3. [Nasıl?](#3-nasıl)
4. [Gerçek dünya senaryosu](#4-gerçek-dünya-senaryosu)
5. [Mülakat perspektifi](#5-mülakat-perspektifi)
6. [Yaygın hatalar](#6-yaygın-hatalar)
7. [Performans & trade-off](#7-performans--trade-off)
8. [Kaynaklar](#8-kaynaklar)

---

## 1. Ne?

"Kod derlenince ne oluyor?" sorusunun junior düzeyi cevabı **tek bir hat**tır:

```
C# source code
  → [Roslyn compiler]   → .dll (assembly) containing IL + metadata
  → [MSBuild]           → .dll + apphost(.exe) + .deps.json + .runtimeconfig.json into the bin/ folder
  → [execution]         → apphost → hostfxr → hostpolicy → CoreCLR (runtime)
  → [JIT]               → turns IL into native machine code on a method's first call
  → CPU runs it
```

Roslyn, IL, apphost ve deployment modellerinin derin hali → M2. Konunun kodu [`Sections/`](Sections/) altında 6 bölümdür (`dotnet run -- J5.1 <number>`, `ConsoleApp` klasöründe); kod, **kendi .dll'ini ve IL'ini inceleyen** bir programdır.

---

## 2. Neden?

- "C# kodu nasıl `.exe` oluyor?" klasik bir mülakat sorusudur; cevap, .NET'in neden platform bağımsız olduğunu ve "Debug'da çalışıyor, Release'de çalışmıyor" gibi sorunların nereden geldiğini de açıklar.
- Hata ayıklarken ve deployment yaparken (`FileNotFoundException`, "runtime bulunamadı", yavaş ilk istek) bu hattın hangi halkasında olduğunuzu bilmek zaman kazandırır.
- IL'e bakabilmek (ILSpy, sharplab.io), dilin "şeker"lerinin (`foreach`, `using`, `async`) aslında ne olduğunu gösterir.

---

## 3. Nasıl?

### 3.1 "C# kodu nasıl `.exe` oluyor?" ⭐ ([`Section01`](Sections/Section01_DllAndApphost.cs))

**Olmuyor.** Senin kodun **her zaman bir `.dll`**'dir: IL + metadata içeren yönetilen (managed) bir assembly. `.exe` (Linux/macOS'ta uzantısız dosya) **apphost**'tur: içinde C# kodu olmayan, küçük, **native** bir launcher; tek işi .NET runtime'ı bulup `.dll`'i başlatmaktır. `dotnet MyApp.dll` ile apphost'a hiç gerek kalmadan da çalıştırılır.

| Dosya | Türü | Ne? |
|-------|------|-----|
| `ConsoleApp.dll` (~80 KB) | **Yönetilen PE**, `ILOnly=True` | Senin kodun: IL + metadata; işlemciye özel makine kodu yok |
| `ConsoleApp` (apphost, ~78 KB) | **Native** (Linux'ta ELF, Windows'ta PE `.exe`) | Runtime'ı bulup `.dll`'i başlatan launcher |
| `System.Private.CoreLib.dll` | Yönetilen | `object`, `string`, `List<T>` — BCL de IL'dir |

**Başlatma zinciri:** (1) apphost `.runtimeconfig.json`'u okuyup hangi runtime gerektiğini öğrenir → (2) **hostfxr** uygun .NET sürümünü seçer (roll-forward) → (3) **hostpolicy** `.deps.json`'a göre bağımlılıkları çözer → (4) **CoreCLR** `.dll`'i yükler, GC'yi başlatır → (5) **JIT** IL'i metotlar ilk çağrıldıkça makine koduna çevirir.

> `makine etiketi=I386` AnyCPU IL için normaldir; 32-bit anlamına gelmez.

### 3.2 IL nedir, neden var? 🛠 ([`Section02`](Sections/Section02_ReadingIl.cs))

**IL (Intermediate Language, CIL)**, derleyicinin ürettiği **yığın (stack) tabanlı, platformdan bağımsız ara dildir.** `a + b`:

```
IL_0000: ldarg.0     ; push the first argument onto the stack
IL_0001: ldarg.1     ; push the second one onto the stack
IL_0002: add         ; pop both, add them, push the result
IL_0003: ret         ; return the value on the stack
```

- **Neden var?** Dil ve platform bağımsızlığı: C#, F#, VB aynı IL'e derlenir; aynı `.dll` x64, ARM64, Windows, Linux'ta çalışır. Platforma özel kısmı (makine kodu) JIT hedef makinede üretir.
- `Section02`, `MethodBody.GetILAsByteArray()` + küçük bir disassembler ile IL'i yazdırır. **Gerçek araçlar:**
  - **ILSpy** (açık kaynak): `bin/Debug/net10.0/ConsoleApp.dll` dosyasını açın → `BigPicture` → `Samples` → sağ üstten "IL" / "C#" seçin.
  - **sharplab.io**: tarayıcıda C# yazın; sağ panelde **IL** ve **Lowered C#** (derleyicinin açtığı hâl) görün.
  - Komut satırı: `dotnet ildasm` / `ildasm` (araç kurulumu gerekir).

### 3.3 JIT ([`Section03`](Sections/Section03_Jit.cs))

**JIT (Just-In-Time)**: bir metot **ilk çağrıldığında** IL'i makine koduna çevirir; sonraki çağrılar çevrilmiş kodu kullanır. Demoda `JitInfo.GetCompiledMethodCount()`: ilk çağrıda +1 metot (~60 µs), ikinci çağrıda +0.

- IL taşınabilir; JIT çalıştığı işlemciye en uygun kodu üretir.
- Maliyet: ilk çağrılarda gecikme (startup). **Tiered compilation** (varsayılan): önce hızlı/az optimize kod (tier-0), sık çağrılan metotlar sonra yeniden daha iyi derlenir (tier-1). Önceden derleme seçenekleri (ReadyToRun, Native AOT) → M2.5.

### 3.4 Debug vs Release ([`Section04`](Sections/Section04_DebugVsRelease.cs))

İki kez çalıştırıp karşılaştırın: `dotnet run -- J5.1 4` ve `dotnet run -c Release -- J5.1 4`.

| | Debug | Release |
|---|-------|---------|
| Derleyici optimizasyonu | **Kapalı** (`nop`'lar, gereksiz geçici değişkenler) | **Açık** (IL sadeleşir) |
| `AddWithLocal` IL'i | 10 komut, 1 `nop` | **4 komut** (`ldarg, ldarg, add, ret`) |
| JIT optimizasyonu | Kapalı (`IsJITOptimizerDisabled = True`) | Açık |
| `DEBUG` sembolü | Tanımlı (`#if DEBUG` çalışır) | Tanımlı değil |
| `Debug.Assert`, `[Conditional("DEBUG")]` | Çalışır | Çağrı **ve argüman ifadeleri silinir** |
| Debugger | Satır satır, her değişken görünür | Değişkenler "optimized away", satırlar atlar |

`dotnet build`/`run` varsayılanı **Debug**, `dotnet publish` varsayılanı **Release**'dir (.NET 8+). Üretime Debug çıktısı gitmemelidir. "Debug'da çalışıyor, Release'de çalışmıyor" gerçek bir sorun sınıfıdır: yan etkili `Debug.Assert` (demo: Release'de ifade hiç çalışmaz), optimizasyona duyarlı/zamanlamaya bağlı kod, `#if DEBUG` yol farkı → ikisini de test edin (→ J1.5).

### 3.5 Compile-time error vs runtime exception ([`Section05`](Sections/Section05_CompileVsRuntime.cs))

| | Compile-time error | Runtime exception |
|---|--------------------|-------------------|
| Ne zaman? | Derleyici kodu analiz ederken | Program çalışırken, o kod yolu işlenince |
| Sonuç | `.dll` **üretilmez**; hiçbir şey çalışmaz | `.dll` var; hata kullanıcıya/log'a yansır |
| Maliyet | Ucuz: geliştirici hemen görür | Pahalı: üretimde, belki yalnızca belirli veride |
| Örnek | `CS0029` (tür uyuşmazlığı), `CS0165` (atanmamış değişken) | `InvalidCastException`, `NullReferenceException`, `DivideByZeroException` |

Strateji: hatayı mümkün olduğunca **derleme zamanına çekin**: tipler, nullable reference types, analyzer'lar, `required`, `enum`/`record` ile geçersiz durumu ifade edilemez kılmak; kalanını testle yakalayın (→ J10).

### 3.6 🛠 `foreach` ve `using`'in "lowered" hali ([`Section06`](Sections/Section06_Lowering.cs))

Derleyici dil "şeker"lerini daha basit koda açar; IL'de `foreach`/`using` diye bir komut yoktur. Demo, aynı mantığı elle yazıp sonucu karşılaştırır:

```csharp
foreach (var item in list) sum += item;
// ≈ what the compiler expands it to:
var e = list.GetEnumerator();           // List<T>.Enumerator: a struct (not boxed)
try { while (e.MoveNext()) { var item = e.Current; sum += item; } }
finally { e.Dispose(); }                // foreach ALWAYS disposes

using var r = new Resource();
// ≈  var r = new Resource(); try { ... } finally { if (r != null) r.Dispose(); }
```

Dizide `foreach`, IL'de indeksli `for`'a iner (enumerator yok). **sharplab.io'da deneyin:**

```csharp
using System; using System.Collections.Generic;
public class C { public int M(List<int> l) { var s = 0; foreach (var x in l) s += x; return s; } }
```

Sağ üstten "Results → Lowered C#" / "IL" seçin. Diğer şekerler: `lock` → `Monitor`, `async/await` → state machine (→ M4.2), interpolation → `DefaultInterpolatedStringHandler`, lambda/closure → display class (→ J3.2).

---

## 4. Gerçek dünya senaryosu

**"Sunucuda `Could not load file or assembly` / `You must install .NET`."** Uygulama framework-dependent yayınlanmış ve sunucuda doğru runtime yok. Zincir bilinince teşhis kolaydır: apphost → `runtimeconfig.json` (hangi framework?) → hostfxr (kurulu mu?) → `deps.json` (hangi assembly?). Çözüm: runtime kurmak ya da self-contained yayınlamak.

**"Canlıda `Debug.Assert` içinde yan etki vardı, test ortamında çalışıyordu."** `Debug.Assert(Initialize())` Release'de **hiç çağrılmıyor** (çağrı ve argüman ifadesi silinir). Yan etkili çağrılar assert dışına alındı.

**"İlk istek 3 sn, sonrakiler 20 ms."** Soğuk başlangıçta JIT ve tip yükleme maliyeti. Çözüm: ısındırma (warm-up) isteği, ReadyToRun/tiered ayarları ya da Native AOT (→ M2.5).

---

## 5. Mülakat perspektifi

Sık sorulanlar: **C# kodu nasıl `.exe` oluyor?** ⭐ · **IL nedir?** · JIT nedir (bir cümleyle)? · Debug ile Release farkı? · Compile-time ile runtime hata farkı? · `foreach` aslında nasıl çalışır? · Assembly nedir?

**İpucu:** Önce hattı **tek cümleyle** söyleyin ("C# → Roslyn → IL `.dll` → çalışırken JIT → native"), sonra yanılgıyı düzeltin: "`.exe` kodun kendisi değil, `.dll`'i başlatan native launcher'dır."

---

## 6. Yaygın hatalar

| Hata | Doğrusu |
|------|---------|
| ".exe içinde benim C# kodum var" | Kodun `.dll`'dedir; `.exe` apphost (native launcher) |
| "C# doğrudan makine koduna derlenir" | IL'e derlenir; makine koduna JIT (veya AOT) çevirir |
| Debug çıktısını production'a koymak | `dotnet publish` (Release) |
| `Debug.Assert` içinde yan etkili ifade | İfadeyi assert dışında çalıştır, sonucu assert et |
| `#if DEBUG` ile davranışı değiştirmek | Yapılandırma farkı hata sınıfı yaratır; ayarla (appsettings/env) |
| Compile-time hatayı try/catch ile çözmeye çalışmak | Derleme hatası düzeltilir; exception çalışma zamanı içindir |
| Çalışma zamanı hatalarını testsiz bırakmak | Tipler/nullable ile derleme zamanına çek, kalanını test et |

---

## 7. Performans & trade-off

- **JIT maliyeti:** İlk çağrılarda gecikme; sıcak yolda JIT optimize kod (tier-1) genelde statik derlemeye yakın hızdadır. Soğuk başlangıç kritikse ReadyToRun/Native AOT (→ M2.5, S1).
- **Release** çoğu zaman belirgin biçimde daha hızlıdır; benchmark'ı **daima Release**'te ölçün (→ J11).
- **IL taşınabilirliği ↔ JIT gecikmesi** temel takastır; AOT, taşınabilirlikten (ve bazı dil özelliklerinden) vazgeçip başlangıç hızı kazanır.
- **Assembly boyutu:** Debug sembolleri (`.pdb`) ayrı dosyadır; ihtiyaç halinde yayın paketinden ayrılabilir ama hata ayıklama/satır numaraları için saklanmalıdır.

---

## 8. Kaynaklar

- .NET'in yönetilen yürütme süreci (CLR): <https://learn.microsoft.com/dotnet/standard/managed-execution-process>
- Ortak Ara Dil (CIL) ve JIT: <https://learn.microsoft.com/dotnet/standard/managed-code>
- Assembly'ler: <https://learn.microsoft.com/dotnet/standard/assembly/>
- .NET uygulama başlatma (host): <https://learn.microsoft.com/dotnet/core/dependency-loading/overview>
- ILSpy: <https://github.com/icsharpcode/ILSpy>
- SharpLab: <https://sharplab.io>
- Tiered compilation: <https://learn.microsoft.com/dotnet/core/runtime-config/compilation>
- Derleme yapılandırmaları (Debug/Release): <https://learn.microsoft.com/visualstudio/debugger/how-to-set-debug-and-release-configurations>
