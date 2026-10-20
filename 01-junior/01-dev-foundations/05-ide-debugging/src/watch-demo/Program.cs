using Junior.DevFoundations.IdeDebugging.WatchDemo;

// dotnet watch + Hot Reload denemesi.
//
//   dotnet watch            → uygulamayı çalıştırır ve dosyaları İZLER
//
// Uygulama çalışırken Greeter.cs içindeki metni değiştirip kaydedin:
//   • Metot GÖVDESİ değişikliği  → Hot Reload: uygulama YENİDEN BAŞLAMADAN yeni kod devreye girer (aşağıdaki çıktı değişir)
//   • "Rude edit" (ör. metot imzasını değiştirmek, yeni tip alanı eklemek) → Hot Reload uygulayamaz;
//     dotnet watch yeniden başlatmak isteyip istemediğinizi sorar (Enter = yeniden başlat)
//   • Ctrl+R = elle yeniden başlat · Ctrl+C = çık
//
// Faydası: her küçük değişiklikte "derle → durdur → başlat → aynı duruma getir" döngüsünü ortadan kaldırır.

var counter = 0;
Console.WriteLine("Çalışıyor. Greeter.cs'i düzenleyin; Ctrl+C ile çıkın.\n");

while (true)
{
    counter++;
    Console.WriteLine($"{DateTime.Now:HH:mm:ss}  #{counter,-3} {Greeter.Message(counter)}");
    await Task.Delay(1000);
}
