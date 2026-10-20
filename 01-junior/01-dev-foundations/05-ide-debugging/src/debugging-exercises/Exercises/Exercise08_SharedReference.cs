namespace Junior.DevFoundations.IdeDebugging.Exercises;

public sealed class Exercise08_SharedReference : Exercise
{
    public override int Number => 8;
    public override string Title => "Aynı nesneyi paylaşan iki 'farklı' kullanıcı";
    public override string Skill => "Make Object ID: iki değişkenin aynı referansı gösterdiğini kanıtlamak";
    public override string Hint => "Watch penceresinde 'aliGuest' ve 'veliGuest' → sağ tık → Make Object ID. İkisi de $1 ise aynı nesnedir. Liste nerede oluşuyor?";

    public override void Run()
    {
        var aliGuest = Permissions.For("guest");
        var veliGuest = Permissions.For("guest");

        aliGuest.Add("write");                         // sadece Ali'ye yazma yetkisi vermek istedik

        Report("Veli'nin yetkileri", expected: "read", actual: string.Join(",", veliGuest));
    }

    private static class Permissions
    {
        private static readonly List<string> GuestDefaults = ["read"];

        public static List<string> For(string role) => role switch
        {
            "admin" => ["read", "write", "delete"],     // her çağrıda YENİ liste
            _ => GuestDefaults,                         // her çağrıda AYNI liste
        };
    }
}
