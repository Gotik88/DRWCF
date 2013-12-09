

namespace DR.WCF.Client.Host
{
    using System;
    using DR.WCF.Proxy.WCFClient;

    class Program
    {
        static void Main(string[] args)
        {
            var value = String.Empty;
            new LamdaProxyHelper<Config.IConfigurationService>().Use(serviceProxy =>
            {
                value = serviceProxy.GetSection("test");
            }, "BasicHttpBinding_IConfigurationService");
        }
    }
}
