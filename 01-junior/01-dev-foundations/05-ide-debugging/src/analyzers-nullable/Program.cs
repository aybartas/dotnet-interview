using Junior.DevFoundations.IdeDebugging.AnalyzersNullable;

// Uyarıları üretmek için tipler yeterli; çalıştırınca da kısa bir çıktı verir.
// Not: GetUserName(2) çalışma zamanında null döndürür — derleyicinin CS8603 ile uyardığı tam olarak bu.
var service = new UserService { Name = "demo" };
Console.WriteLine($"1 → {service.GetUserName(1)}");
Console.WriteLine($"2 → {service.GetUserName(2) ?? "(null döndü)"}");
Console.WriteLine($"Style demo: IsAllowed(admin) = {new StyleViolations().IsAllowed("admin")}");
