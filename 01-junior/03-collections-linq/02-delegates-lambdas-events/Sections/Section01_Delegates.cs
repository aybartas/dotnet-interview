namespace Junior.CollectionsLinq.DelegatesLambdasEvents;

public static class Section01_Delegates
{
    // 1) Delegate TİPİ: "şu imzaya sahip metotları tutabilen bir tip" (tip güvenli fonksiyon işaretçisi)
    private delegate int BinaryOperation(int a, int b);

    public static void Run()
    {
        Console.WriteLine("1) delegate = imza tipi; bir metoda (veya birkaçına) REFERANS tutar. Delegate bir NESNE'dir (System.MulticastDelegate).");
        BinaryOperation op = Add;                                      // method group → delegate dönüşümü (new BinaryOperation(Add) kısaltması)
        Console.WriteLine($"   BinaryOperation op = Add;  op(2, 3) = {op(2, 3)}  ·  op.Method.Name = {op.Method.Name}  ·  tip: {op.GetType().BaseType!.Name}");

        Console.WriteLine("\n2) Hazır generic delegate'ler (kendi delegate tipini nadiren yazarsın):");
        Func<int, int, int> add = Add;                                 // girdi(ler) + DÖNÜŞ değeri: Func<T1, T2, TResult>
        Action<string> print = Console.WriteLine;                      // girdi, dönüş YOK (void): Action<T>
        Predicate<int> isEven = n => n % 2 == 0;                       // bool dönen tek girdili: Predicate<T> (= Func<T,bool>)
        Console.WriteLine($"   Func: add(4,5) = {add(4, 5)}  ·  Predicate: isEven(4) = {isEven(4)}");
        print("   Action<string> ile yazdırıldı (dönüş değeri yok)");
        Console.WriteLine("   Func<> son tip parametresi DÖNÜŞ tipidir; Action<> void döner; Predicate<T> ≡ Func<T,bool> (eski API'lerde: List.FindAll)");

        Console.WriteLine("\n3) Method group: metot adını (parantezsiz) delegate beklenen yere vermek:");
        var words = new List<string> { "kedi", "köpek", "kuş" };
        Console.WriteLine($"   words.ForEach(Console.WriteLine) →");
        words.ForEach(Console.WriteLine);
        Console.WriteLine("   Method group, imzası uyuşan overload'a derleme zamanında bağlanır; belirsizse (birden çok overload) lambda ile açıkça yazın.");

        Console.WriteLine("\n4) MULTICAST: delegate birden çok metodu zincirleyebilir (+=, -=); çağrıldığında SIRAYLA çalışır");
        Action<string> chain = s => Console.WriteLine($"      1. abone: {s}");
        chain += s => Console.WriteLine($"      2. abone: {s}");
        chain += s => Console.WriteLine($"      3. abone: {s}");
        Console.WriteLine($"   InvocationList uzunluğu = {chain.GetInvocationList().Length}");
        chain("merhaba");

        Func<int> values = () => 1;
        values += () => 2;
        values += () => 3;
        Console.WriteLine($"   Dönüş değerli multicast: Func<int> (1,2,3 zinciri) çağrısı → {values()}   ← YALNIZCA SON metodun dönüşü alınır (diğerleri atılır)");

        Console.WriteLine("\n5) Delegate'ler IMMUTABLE'dır: += yeni bir delegate nesnesi üretir ve değişkene atar.");
        Action a = () => { };
        var before = a;
        a += () => { };
        Console.WriteLine($"   += sonrası aynı nesne mi? {ReferenceEquals(before, a)}   (false → yeni zincir)");

        Console.WriteLine("\n6) Null güvenliği: hiç abone yoksa delegate NULL'dır → çağrı NullReferenceException; ?.Invoke() güvenli:");
        Action<string>? none = null;
        none?.Invoke("kimse yok");                                     // sessizce atlanır
        try { none!("x"); }
        catch (NullReferenceException) { Console.WriteLine("   null delegate'i doğrudan çağırmak → NullReferenceException; `?.Invoke(...)` kullanın"); }

        Console.WriteLine("""

        Delegate nedir, ne işe yarar?  Davranışı DEĞER gibi taşımak: parametre olarak geçmek (strateji), geri çağrı (callback), olay (event), LINQ (Where(predicate)).
        Delegate vs interface: tek metotluk davranış için delegate hafif; birden çok ilişkili metot/durum için interface.
        """);
    }

    private static int Add(int a, int b) => a + b;
}
