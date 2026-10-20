#!/usr/bin/env bash
# Aynı uygulamayı (c) varyantındaki yapıyı SIFIRDAN, yalnızca dotnet CLI ile kurar:
#   solution (.slnx) + class library + console + xunit test projesi + referanslar + build + test.
#
# Önemli: Bu script repoDAN BAĞIMSIZ geçici bir klasörde çalışır. Çünkü repo kökünde Directory.Packages.props
# (Central Package Management) var ve `dotnet add package` onu DEĞİŞTİRİRDİ.
#
# Kullanım:  ./cli-walkthrough.sh           (yalnızca dotnet SDK gerekir; paket indirmek için internet)
set -euo pipefail

say() { printf '\n\033[1;36m==> %s\033[0m\n' "$*"; }
run() { printf '\033[0;33m$ %s\033[0m\n' "$*"; "$@"; }

WORK="$(mktemp -d)"
trap 'echo; echo "(geçici klasör: $WORK — silebilirsiniz)"' EXIT
cd "$WORK"

say "0) Ortam: hangi SDK ve runtime'lar kurulu?"
run dotnet --info | sed -n '1,12p'
run dotnet --list-sdks
run dotnet --list-runtimes

say "1) Solution (yeni XML tabanlı .slnx biçimi — .NET 10'da varsayılan)"
run dotnet new sln -n VatCalculator
ls

say "2) Class library, console ve test projesi"
run dotnet new classlib -n VatCalculator.Core    -o src/VatCalculator.Core
run dotnet new console  -n VatCalculator.Cli     -o src/VatCalculator.Cli
run dotnet new classlib -n VatCalculator.Tests   -o tests/VatCalculator.Tests   # test projesi: classlib şablonu + xunit paketi (adım 4-5)

say "3) Solution'a ekle (dotnet sln add) ve proje referanslarını kur (dotnet add reference)"
run dotnet sln add src/VatCalculator.Core src/VatCalculator.Cli tests/VatCalculator.Tests
run dotnet add src/VatCalculator.Cli   reference src/VatCalculator.Core
run dotnet add tests/VatCalculator.Tests reference src/VatCalculator.Core

say "4) NuGet paketi ekle (dotnet add package) — csproj'a <PackageReference> yazılır; transitive bağımlılıkları görmek için:"
run dotnet add tests/VatCalculator.Tests package xunit.v3
run dotnet list tests/VatCalculator.Tests package --include-transitive | head -20

say "5) Test projesini çalıştırılabilir yap ve bir test yaz"
sed -i 's#<PropertyGroup>#<PropertyGroup>\n    <OutputType>Exe</OutputType>#' tests/VatCalculator.Tests/VatCalculator.Tests.csproj
rm tests/VatCalculator.Tests/Class1.cs
cat > tests/VatCalculator.Tests/SanityTests.cs <<'CS'
using Xunit;
public class SanityTests { [Fact] public void TwoPlusTwo() => Assert.Equal(4, 2 + 2); }
CS
# .NET 10'da `dotnet test`'in yeni (Microsoft.Testing.Platform) modunu açan global.json:
cat > global.json <<'JSON'
{ "test": { "runner": "Microsoft.Testing.Platform" } }
JSON

say "6) build · test · run"
run dotnet build
run dotnet test
run dotnet run --project src/VatCalculator.Cli

say "7) Oluşan yapı"
find . -name '*.csproj' -o -name '*.slnx' -o -name global.json | grep -v '/obj/' | sort

say "8) .csproj'un içi nasıl? (şablonun ürettiği çıplak hali)"
cat src/VatCalculator.Cli/VatCalculator.Cli.csproj
