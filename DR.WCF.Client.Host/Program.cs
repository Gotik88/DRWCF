

using DR.WCF.Service.Duplex;
using DR.WCF.Service.OneWay;

namespace DR.WCF.Client.Host
{
    using System;
    using DR.WCF.Proxy.WCFClient;
    using System.ServiceModel;

    class Program
    {
        static void Main(string[] args)
        {
            /*var value = String.Empty;
            new LamdaProxyHelper<Config.IConfigurationService>().Use(serviceProxy =>
            {
                value = serviceProxy.GetSection("test");
            }, "BasicHttpBinding_IConfigurationService");*/


            /*Please Comment out this code portion if u want to use Self Hosting...that is if u run the project Rashim.RND.WCFRouting.Router
           */


            Console.WriteLine("************ Self Hosting**************");

            Console.WriteLine("\n\n------------------------------------------------------");
            Console.WriteLine("\n\n Binding = WSHttpBinding");
            Console.WriteLine("\n\n endpoint = http://localhost:9000/HelloServiceRouter/hello");

            var binding = new WSHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:9000/HelloServiceRouter/hello");
            var proxy = ChannelFactory<IOneWayService>.CreateChannel(binding, endpoint);

            ////Console.WriteLine("\n\n Reply from Server :" + proxy.ProcessMessage("Hi"));
            Console.WriteLine("\n\n------------------------------------------------------");

            Console.WriteLine("\n\n------------------------------------------------------");
            Console.WriteLine("\n\n Binding = NetTcpBinding");
            Console.WriteLine("\n\n endpoint = net.tcp://localhost:8733/HelloServiceRouter/duplex");
            var callback = new MyClientCallback();
            InstanceContext ctx = new InstanceContext(callback);

            var netbinding = new NetTcpBinding();
            var netendpoint = new EndpointAddress("net.tcp://localhost:8733/HelloServiceRouter/duplex");
            var netproxy = DuplexChannelFactory<IDuplexService>.CreateChannel(ctx, netbinding, netendpoint);

            Console.WriteLine("\n\n Reply from the Server:" + netproxy.ProcessMessage("Hi"));
            Console.WriteLine("\n\n------------------------------------------------------");

            Console.WriteLine("\n\n------------------------------------------------------");
            Console.WriteLine("\n\n Binding = BasicHttpBinding");
            Console.WriteLine("\n\n endpoint = http://localhost:9000/HelloServiceRouter/oneway");
            var onewaybinding = new BasicHttpBinding();
            var onewaynetendpoint = new EndpointAddress("http://localhost:9000/HelloServiceRouter/oneway");
            var onewaynetproxy = ChannelFactory<IOneWayService>.CreateChannel(onewaybinding, onewaynetendpoint);
            onewaynetproxy.ProcessMessage("Hi");

            Console.WriteLine("\n\n oneway binding is working");
            Console.WriteLine("\n\n------------------------------------------------------");
            Console.WriteLine("************ ++++++++++++++++**************");


            /*Please Comment out this code portion if u want to use IIS Hosting...that is if u run the project RouterHostingInIIS
            */

            /*
            Console.WriteLine("************ IIS Hosting**************");
            
            Console.WriteLine("\n\n------------------------------------------------------");
            Console.WriteLine("\n\n Binding = WSHttpBinding");
            Console.WriteLine("\n\n endpoint = http://localhost:5725/RouterService.svc/hello");

            var binding = new WSHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:5725/RouterService.svc/hello");
            var proxy = ChannelFactory<IHelloService>.CreateChannel(binding, endpoint);
            Console.WriteLine("\n\n Reply from Server :" + proxy.SayHi("Hi"));
            Console.WriteLine("\n\n------------------------------------------------------");



            Console.WriteLine("\n\n------------------------------------------------------");
            Console.WriteLine("\n\n Binding = WSDualHttpBinding");
            Console.WriteLine("\n\n endpoint = http://localhost:5725/RouterService.svc/duplex");          

            var callback = new MyClientCallback();
            InstanceContext ctx = new InstanceContext(callback);

            var netbinding = new WSDualHttpBinding();
            var netendpoint = new EndpointAddress("http://localhost:5725/RouterService.svc/duplex");
            var netproxy = DuplexChannelFactory<Rashim.RND.WCFRouting.DuplexService.IHelloService>.CreateChannel(ctx, netbinding, netendpoint);

            Console.WriteLine("\n\n Reply from the Server:" + netproxy.SayHi("Hi"));
            Console.WriteLine("\n\n------------------------------------------------------");

            Console.WriteLine("\n\n------------------------------------------------------");
            Console.WriteLine("\n\n Binding = BasicHttpBinding");
            Console.WriteLine("\n\n endpoint = http://localhost:9000/HelloServiceRouter/oneway");

            var onewaybinding = new BasicHttpBinding();
            var onewaynetendpoint = new EndpointAddress("http://localhost:5725/RouterService.svc/oneway");
            var onewaynetproxy = ChannelFactory<Rashim.RND.WCFRouting.OneWayService.IHelloService>.CreateChannel(onewaybinding, onewaynetendpoint);
            Console.WriteLine("\n\n oneway binding is working");
            Console.WriteLine("\n\n------------------------------------------------------");
            Console.WriteLine("************ ++++++++++++++++**************");     
             */

            Console.ReadLine();
        }
    }
}
