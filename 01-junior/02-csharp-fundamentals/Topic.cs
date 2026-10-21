namespace Junior.CSharpFundamentals;

/// <summary>Modüldeki bir konu: Id (ör. "2.3"), başlık ve giriş noktası (kalan argümanları alır, exit code döner).</summary>
public sealed record Topic(string Id, string Title, Func<string[], int> Run);
