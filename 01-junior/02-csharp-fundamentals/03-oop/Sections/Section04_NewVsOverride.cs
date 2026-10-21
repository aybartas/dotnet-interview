namespace Junior.CSharpFundamentals.Oop;

public static class Section04_NewVsOverride
{
    public static void Run()
    {
        Console.WriteLine("Aynı metot, iki farklı türetme biçimi. Base REFERANSI üzerinden çağırınca ne olur?\n");

        Base viaOverride = new DerivedOverride();
        Base viaNew = new DerivedNew();

        Console.WriteLine("                                   Base referansı ile    Derived referansı ile");
        Console.WriteLine($"override (virtual dispatch)      : {viaOverride.Hello(),-20}  {((DerivedOverride)viaOverride).Hello()}");
        Console.WriteLine($"new      (method HIDING)         : {viaNew.Hello(),-20}  {((DerivedNew)viaNew).Hello()}");
        Console.WriteLine($"non-virtual (Base'te virtual yok) : {viaNew.Plain(),-20}  {((DerivedNew)viaNew).Plain()}");

        Console.WriteLine("""

        override: Base'in sanal (virtual/abstract) metodunu DEĞİŞTİRİR → çağrı, nesnenin GERÇEK tipine göre çözülür (runtime polimorfizm).
        new     : Base'teki metodu GİZLER → çağrı, DEĞİŞKENİN (derleme zamanı) tipine göre çözülür. Polimorfizm YOKTUR.
                  Aynı nesne, hangi tipte tutulduğuna göre farklı davranır → şaşırtıcı hata kaynağı.
        Derleyici 'new' yazmazsanız CS0108 uyarısı verir ("gizlemek mi istediniz?"). 'new' neredeyse hiçbir zaman istenen şey DEĞİLDİR;
        meşru kullanım: sahibi olmadığınız bir taban sınıfa sonradan eklenen aynı adlı üyeyle çakışmayı çözmek.
        sealed override: bu noktadan sonra daha fazla override'a izin vermez (kalıtım zincirini kapatır, JIT devirtualization yapabilir).
        """);

        Console.WriteLine("Koleksiyon içinde fark daha da kötü: Base listesi, new ile gizlenen metodu HİÇ görmez:");
        List<Base> list = [new DerivedOverride(), new DerivedNew()];
        foreach (var item in list) Console.WriteLine($"   {item.GetType().Name,-16} → {item.Hello()}");
    }

    private class Base
    {
        public virtual string Hello() => "Base.Hello";
        public string Plain() => "Base.Plain";
    }

    private class DerivedOverride : Base
    {
        public override string Hello() => "DerivedOverride.Hello";
    }

    private class DerivedNew : Base
    {
        public new string Hello() => "DerivedNew.Hello";       // GİZLER (base.virtual olsa bile)
        public new string Plain() => "DerivedNew.Plain";
    }
}
