
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Xml;

namespace DR.WCF.Router
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     AddressFilterMode = AddressFilterMode.Any,
                     ValidateMustUnderstand = false)] // solve problem with request messages contains security headers
    public class RouterService : IRouterService
    {
        public static IDictionary<string, string> RegistrationList = new Dictionary<string, string>();

        public Message ProcessMessage(Message requestMessage)
        {
            RegistrationList.Add("http://www.thatindigogirl.com/samples/2008/01/IServiceA", "ServiceA");  // services registration
            RegistrationList.Add("http://www.thatindigogirl.com/samples/2008/01/IServiceB", "ServiceB");

            /*Uri httpBase = new Uri(string.Format("http://localhost:{0}",FindFreePort()));
            Uri tcpBase = new Uri(string.Format("net.tcp://localhost:{0}",FindFreePort()));
            Uri netPipeBase = new Uri(string.Format("net.pipe://localhost/{0}",Guid.NewGuid().ToString()));

            ServiceHost host = new ServiceHost(typeof(ServiceA), httpBase, tcpBase, netPipeBase);*/

            //header
            string contractNamespace = requestMessage.Headers.Action.Substring(0, requestMessage.Headers.Action.LastIndexOf("/"));
            string configurationName = RouterService.RegistrationList[contractNamespace];

            var contractNamespaceHeaders = requestMessage.Headers.GetHeader<string>("Route", "http://www.thatindigogirl.com/samples/2008/01");

            //body
            //После прочтения сообщение невозможно перенаправить службам приложений, 
            //поскольку сообщение может быть только один раз прочитано, записано или скопировано.
            MessageBuffer messageBuffer = requestMessage.CreateBufferedCopy(int.MaxValue);
            Message messageCopy = messageBuffer.CreateMessage(); // create new instance of message base on buffered copy
            XmlDictionaryReader bodyReader = messageCopy.GetReaderAtBodyContents();

            XmlDocument doc = new XmlDocument();
            doc.Load(bodyReader);
            XmlNodeList elements = doc.GetElementsByTagName("LicenseKey");
            string licenseKey = elements[0].InnerText;


            using (var factory = new ChannelFactory<IRouterService>(configurationName))
            {
                factory.Endpoint.Behaviors.Add(new MustUnderstandBehavior(false)); // solve problem with response messages contains security headers
                using (IRouterService proxy = factory.CreateChannel())
                {
                    return proxy.ProcessMessage(requestMessage);
                }
            }
        }

        public void Dispose()
        {
        }
    }

}
