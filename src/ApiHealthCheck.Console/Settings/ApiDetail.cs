using ApiHealthCheck.Lib;

namespace ApiHealthCheck.Console.Settings
{
    internal record ApiDetail(string Name, string Url, ApiCredential? ApiCredential, bool IsEnable);
}
