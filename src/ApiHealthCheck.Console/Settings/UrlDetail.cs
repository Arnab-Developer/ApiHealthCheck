using ApiHealthCheck.Lib;

namespace ApiHealthCheck.Console.Settings
{
    internal record ApiDetail
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public ApiCredential? ApiCredential { get; set; }
        public bool IsEnable { get; set; }

        public ApiDetail()
        {
            Name = string.Empty;
            Url = string.Empty;
            IsEnable = false;
        }
    }
}
