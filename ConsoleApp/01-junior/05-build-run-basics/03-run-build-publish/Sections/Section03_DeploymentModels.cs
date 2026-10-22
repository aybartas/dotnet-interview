namespace Junior.BuildRunBasics.RunBuildPublish;

public static class Section03_DeploymentModels
{
    public static void Run()
    {
        Console.WriteLine("""
        Deployment modellerine bir paragraflık giriş (derin hali → M2.5):

        1) Framework-dependent (varsayılan)  Runtime: MAKİNEDE kurulu .NET   · Boyut: küçük (yalnızca uygulama)
              dotnet publish -c Release
        2) Self-contained                    Runtime: UYGULAMAYLA birlikte gelir · Boyut: büyük (~70+ MB)
              dotnet publish -c Release -r linux-x64 --self-contained
        3) Native AOT                        Runtime: yok, kod önceden native derlenir · Boyut: küçük, hızlı başlangıç
              dotnet publish -c Release -r linux-x64 -p:PublishAot=true

        • Framework-dependent: sunucuda/container'da (mcr.microsoft.com/dotnet/aspnet) .NET runtime kurulu olmalı; güvenlik yamaları runtime güncellemesiyle gelir.
          Eksikse: "You must install or update .NET..." (runtimeconfig.json'daki framework sürümü bulunamaz → 5.2 Bölüm 2).
        • Self-contained: hedef makinede .NET gerekmez; runtime sürümü uygulamaya kilitlidir (yama için yeniden yayımlamak gerekir).
          runtimeconfig.json'da `includedFrameworks` görünür; çıktı yüzlerce dosya olur (tek dosya: -p:PublishSingleFile=true).
        • Native AOT: IL, yayın sırasında makine koduna derlenir; JIT yoktur → çok hızlı başlangıç, küçük bellek; kısıtları (yansıma, dinamik kod) vardır.
        • Ek seçenekler: -p:PublishTrimmed=true (kullanılmayan kodu at), -p:PublishReadyToRun=true (JIT maliyetini azalt).
        Seçim: kurumsal sunucu/container + .NET kurulu → framework-dependent (varsayılan); izole/istemci makinesi → self-contained; serverless/CLI/soğuk başlangıç → AOT.
        """);

        Console.WriteLine($"Bu program: {(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, typeof(Section03_DeploymentModels).Assembly.GetName().Name + ".runtimeconfig.json")).Contains("includedFrameworks") ? "self-contained" : "framework-dependent")} (runtimeconfig.json'da 'includedFrameworks' var mı diye bakarak anlıyoruz)");
    }
}
