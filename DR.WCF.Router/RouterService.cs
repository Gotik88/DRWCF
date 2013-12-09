
namespace DR.WCF.Router
{
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RouterService : IRouterService
    {
        public Message ProcessMessage(Message requestMessage)
        {
            using (var factory = new ChannelFactory<IRouterService>("serviceEndpoint"))
            {
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
