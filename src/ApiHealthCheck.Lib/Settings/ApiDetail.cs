namespace ApiHealthCheck.Lib.Settings;

public record ApiDetail(string Name, string Url, ApiCredential? ApiCredential, bool IsEnable);
