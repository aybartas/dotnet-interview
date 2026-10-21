namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section12_XmlDocs
{
    public static void Run()
    {
        // `///` ile yazılan XML yorumları: (1) IntelliSense'te açıklama olarak görünür, (2) csproj'daki
        // <GenerateDocumentationFile> ile .xml dosyası üretilir, (3) Swashbuckle/OpenAPI bu dosyayı okuyup
        // endpoint açıklamalarını Swagger UI'a taşır (→ J7.5), (4) docfx gibi araçlar dokümantasyon sitesi üretir.
        var xmlPath = Path.Combine(AppContext.BaseDirectory, "ProgramAnatomy.xml");
        Console.WriteLine($"Üretilen XML dosyası: {xmlPath}  (var mı: {File.Exists(xmlPath)})");

        if (File.Exists(xmlPath))
        {
            var lines = File.ReadLines(xmlPath).SkipWhile(l => !l.Contains("Section12_XmlDocs.Divide")).Take(12);
            Console.WriteLine("\n--- Divide metodu için üretilen kayıt ---");
            foreach (var l in lines) Console.WriteLine(l);
        }

        Console.WriteLine($"\nDivide(10, 4) = {Divide(10, 4)}");
    }

    /// <summary>
    /// İki sayıyı böler ve <b>tamsayı bölme</b> sonucunu döner.
    /// </summary>
    /// <param name="dividend">Bölünen sayı.</param>
    /// <param name="divisor">Bölen. Sıfır olamaz.</param>
    /// <returns>Bölümün tam kısmı (ondalık atılır).</returns>
    /// <exception cref="DivideByZeroException"><paramref name="divisor"/> sıfırsa fırlatılır.</exception>
    /// <remarks>Ondalık sonuç gerekiyorsa <see cref="decimal"/> overload'ını kullanın.</remarks>
    /// <example>
    /// <code>var r = Section12_XmlDocs.Divide(10, 4); // 2</code>
    /// </example>
    public static int Divide(int dividend, int divisor) => dividend / divisor;
}
