namespace Junior.CSharpFundamentals.Oop;

public static class Section04_NewVsOverride
{
    public static void Run()
    {
        Console.WriteLine("Same method, two different ways to derive. What happens when you call through a Base REFERENCE?\n");

        Base viaOverride = new DerivedOverride();
        Base viaNew = new DerivedNew();

        Console.WriteLine("                                   Via Base reference    Via Derived reference");
        Console.WriteLine($"override (virtual dispatch)      : {viaOverride.Hello(),-20}  {((DerivedOverride)viaOverride).Hello()}");
        Console.WriteLine($"new      (method HIDING)         : {viaNew.Hello(),-20}  {((DerivedNew)viaNew).Hello()}");
        Console.WriteLine($"non-virtual (Base has no virtual) : {viaNew.Plain(),-20}  {((DerivedNew)viaNew).Plain()}");

        Console.WriteLine("""

        override: CHANGES Base's virtual (virtual/abstract) method → the call is resolved by the object's ACTUAL type (runtime polymorphism).
        new     : HIDES the method in Base → the call is resolved by the VARIABLE's (compile-time) type. There is NO polymorphism.
                  The same object behaves differently depending on the type it is held as → a surprising source of bugs.
        If you omit 'new' the compiler gives warning CS0108 ("did you mean to hide?"). 'new' is almost never what you want;
        legitimate use: resolving a clash with a same-named member added later to a base class you don't own.
        sealed override: allows no further overrides from this point (closes the inheritance chain, the JIT can devirtualize).
        """);

        Console.WriteLine("Inside a collection the difference is even worse: a Base list NEVER sees the method hidden with new:");
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
        public new string Hello() => "DerivedNew.Hello";       // HIDES (even if base is virtual)
        public new string Plain() => "DerivedNew.Plain";
    }
}
