# J4.3 Cancellation Temelleri — Mülakat Soruları

> Teori: [README.md](README.md). ⭐ = çok sık sorulan. Modül (J4) sorularının tamamı için J4.1 ve J4.2 dosyalarına da bakın.

### 1. ⭐ İptal (cancellation) neden "kooperatiftir"?
`CancellationTokenSource.Cancel()` yalnızca "iptal istendi" bayrağını kaldırır; çalışan işi kimse zorla durdurmaz (`Thread.Abort` .NET Core'da yoktur). İşin token'ı **kendisinin** kontrol etmesi (`ThrowIfCancellationRequested`, token alan API'lere geçirmek) gerekir. Token'ı izlemeyen kod iptal edilemez; bu yüzden token'ı aşağı doğru geçirmek önemlidir.

### 2. `CancellationTokenSource` ile `CancellationToken` farkı nedir?
CTS iptali **tetikleyen** nesnedir (`Cancel`, `CancelAfter`), IDisposable'dır ve tek kullanımlıktır. `CancellationToken` iptali **gözleyen** küçük bir struct'tır; metotlara geçilir, kendisi iptal edemez. İşi başlatan taraf CTS'i, çalışan kod token'ı tutar.

### 3. ⭐ Neden `CancellationToken` metodun son parametresi ve neden `default` değeri var?
.NET konvansiyonudur: token isteğe bağlı bir yan kanaldır; sona koymak mevcut çağrıları bozmaz, çağıran vermek zorunda kalmaz (`= default` → `CancellationToken.None`, yani iptal edilemez). Adı `cancellationToken` (veya `ct`) olur. Token'ı sınıf alanında saklamak yerine metoda parametre olarak iletilir.

### 4. `OperationCanceledException`'ı nasıl yönetirsin?
İptal bir hata değil, beklenen bir sonuçtur: 500 dönmem, hata seviyesinde loglamam. Yakalarsam kapsamı daraltırım: `catch (OperationCanceledException) when (ct.IsCancellationRequested)`; böylece başka kaynaklı iptaller (örn. HttpClient zaman aşımı) yutulmaz. ASP.NET Core istemci koptuğunda bu exception'ı sessizce karşılar. İptal sonrası kısmi durumu (yarım dosya, transaction) temizlerim.

### 5. ⭐ ASP.NET Core'da istemci bağlantıyı keserse ne olur?
Kestrel `HttpContext.RequestAborted` token'ını iptal eder. Endpoint/controller action'ı `CancellationToken` parametresi alırsa framework onu `RequestAborted`'a bağlar. Token'ı `ToListAsync(ct)`, `HttpClient` çağrıları ve diğer I/O'ya geçirirsek iş durur; geçirmezsek sunucu işi sonuna kadar yapar (boşa CPU/DB).

### 6. Zaman aşımı (timeout) nasıl uygularsın?
`CancellationTokenSource.CancelAfter(süre)` (veya `new CancellationTokenSource(süre)`) ile token'ı işe geçiririm; süre dolunca iş iptal olur. Task üzerinde `WaitAsync(TimeSpan)` de kullanılır ama o yalnızca beklemeyi keser, işi iptal etmez. `WhenAny + Task.Delay` kalıbında zaman aşımı kazansa bile iş arkada sürer.

### 7. Kullanıcı iptali ile zaman aşımını birlikte nasıl yönetirsin?
`CancellationTokenSource.CreateLinkedTokenSource(userToken, timeoutToken)` ile birleşik token oluştururum. İptal olunca hangi kaynağın tetiklediğini iki token'ın `IsCancellationRequested` değerine bakarak ayırt ederim (kullanıcı iptali → sessiz çıkış; zaman aşımı → 504/408).

### 8. Token'ı alt çağrılara geçirmeyi unutursak ne olur?
Hiçbir uyarı olmadan iptal görmezden gelinir: işin tamamı yapılır (demoda 100 ms'de iptal edilen 900 ms'lik iş 903 ms'de bitti). Test geçer, üretimde terk edilen istekler kaynak tüketir. `CA2016` analyzer'ı ("token'ı ileri geçir") bunu yakalar; kod incelemesinde de aranmalıdır.

### 9. `HttpClient`'ta iptal ile zaman aşımını nasıl ayırt edersin?
İkisi de `TaskCanceledException` (`OperationCanceledException`'ın alt sınıfı) fırlatır. Zaman aşımında (`HttpClient.Timeout`) `InnerException` `TimeoutException`'dır; kendi token'ımızın iptalinde `ct.IsCancellationRequested` doğrudur.

### 10. Uzun süren bir CPU döngüsünü nasıl iptal edilebilir yaparsın?
Token alan bir API olmadığı için döngüde kendim kontrol ederim: `ct.ThrowIfCancellationRequested()` (çok sık olmasın diye her N iterasyonda bir). Kontrol sıklığı iptal tepki süresini belirler.

### 11. `CancellationTokenSource` neden dispose edilmeli, yeniden kullanılabilir mi?
CTS zamanlayıcı (`CancelAfter`) ve kayıtlı geri çağrılar tutar; dispose edilmezse kaynak sızar. İptal edilen CTS sıfırlanamaz (`TryReset` yalnızca iptal edilmemişte, zamanlayıcısız çalışır); her iş için yeni CTS oluşturmak en güvenlisidir.

### 12. `token.Register(callback)` ne işe yarar?
İptal istendiğinde çalışacak bir temizlik geri çağrısı kaydeder (örn. bir soketi kapatmak, iptal desteği olmayan eski bir API'yi durdurmak). Dönen `CancellationTokenRegistration` dispose edilerek kayıt kaldırılır.

### 13. İptal ettiğimiz işin yan etkileri geri alınır mı?
Hayır. İptal yalnızca devam eden işi durdurur; DB'ye yazılmış satırlar, gönderilmiş e-postalar, çağrılmış dış servisler geri alınmaz. Bu yüzden kritik adımlar (commit, ödeme) iptal edilemez bırakılır (`CancellationToken.None`) ve iptal güvenli noktalarda gerçekleştirilir; transaction'lar geri alınır.

### 14. Arka plan servisinde (`BackgroundService`) graceful shutdown için token nasıl kullanılır?
`ExecuteAsync(CancellationToken stoppingToken)` içinde `while (!stoppingToken.IsCancellationRequested)` döngüsü ve `await Task.Delay(…, stoppingToken)` kullanılır; uygulama kapanırken host token'ı iptal eder, servis mevcut işi bitirip temiz çıkar (→ M12).
