namespace Junior.AsyncBasics;

/// <summary>Modüldeki bir konu: Id (ör. "4.2"), başlık ve giriş noktası (kalan argümanları alır, exit code döner). Bu modül async olduğu için giriş noktası da async.</summary>
public sealed record Topic(string Id, string Title, Func<string[], Task<int>> Run);
