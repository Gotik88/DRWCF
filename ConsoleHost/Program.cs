
namespace DR.WCF.Server.Host
{
    using System;
    using System.ServiceModel;

    class Program
    {
        static void Main(string[] args)
        {
            Type serviceType = typeof(ConfigurationService);
            var serviceUri = new Uri("http://localhost:8080/ConfigurationService");
            var host = new ServiceHost(serviceType, serviceUri);
            host.Open();
            Console.Read();
        }
    }
}
