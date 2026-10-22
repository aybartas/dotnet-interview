namespace Junior.CSharpFundamentals.ProgramAnatomy;

public static class Section12_XmlDocs
{
    public static void Run()
    {
        // XML comments written with `///`: (1) show up as descriptions in IntelliSense, (2) <GenerateDocumentationFile>
        // in the csproj produces a .xml file, (3) Swashbuckle/OpenAPI reads this file and carries the
        // endpoint descriptions into Swagger UI (→ J7.5), (4) tools like docfx generate a documentation site.
        var xmlPath = Path.Combine(AppContext.BaseDirectory, "ProgramAnatomy.xml");
        Console.WriteLine($"Generated XML file: {xmlPath}  (exists: {File.Exists(xmlPath)})");

        if (File.Exists(xmlPath))
        {
            var lines = File.ReadLines(xmlPath).SkipWhile(l => !l.Contains("Section12_XmlDocs.Divide")).Take(12);
            Console.WriteLine("\n--- Generated entry for the Divide method ---");
            foreach (var l in lines) Console.WriteLine(l);
        }

        Console.WriteLine($"\nDivide(10, 4) = {Divide(10, 4)}");
    }

    /// <summary>
    /// Divides two numbers and returns the result of the <b>integer division</b>.
    /// </summary>
    /// <param name="dividend">The number to be divided.</param>
    /// <param name="divisor">The divisor. Cannot be zero.</param>
    /// <returns>The whole part of the quotient (the fraction is discarded).</returns>
    /// <exception cref="DivideByZeroException">Thrown if <paramref name="divisor"/> is zero.</exception>
    /// <remarks>If you need a fractional result, use the <see cref="decimal"/> overload.</remarks>
    /// <example>
    /// <code>var r = Section12_XmlDocs.Divide(10, 4); // 2</code>
    /// </example>
    public static int Divide(int dividend, int divisor) => dividend / divisor;
}
