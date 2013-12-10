using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace DR.WCF.Router
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     AddressFilterMode = AddressFilterMode.Any,
                     ValidateMustUnderstand = false)]
    public class DuplexRouterService : IDuplexRouterService, IDisposable
    {
        object m_duplexSessionLock = new object();
        IDuplexRouterService m_duplexSession;

        public void ProcessMessage(Message requestMessage)
        {
            lock (this.m_duplexSessionLock)
            {
                if (this.m_duplexSession == null)
                {
                    IDuplexRouterCallback callback =
                      OperationContext.Current.GetCallbackChannel
                      <IDuplexRouterCallback>();

                    DuplexChannelFactory<IDuplexRouterService> factory =
                      new DuplexChannelFactory<IDuplexRouterService>
                      (new InstanceContext(null,
                      new DuplexRouterCallback(callback)), "serviceEndpoint");
                    factory.Endpoint.Behaviors.Add(new MustUnderstandBehavior(false));
                    this.m_duplexSession = factory.CreateChannel();
                }
            }

            this.m_duplexSession.ProcessMessage(requestMessage);
        }
        public void Dispose()
        {
            if (this.m_duplexSession != null)
            {
                try
                {
                    ICommunicationObject obj = this.m_duplexSession as ICommunicationObject;
                    if (obj.State == CommunicationState.Faulted)
                        obj.Abort();
                    else
                        obj.Close();
                }
                catch { }
            }
        }
    }

    public class DuplexRouterCallback : IDuplexRouterCallback
    {

        private IDuplexRouterCallback m_clientCallback;

        public DuplexRouterCallback(IDuplexRouterCallback clientCallback)
        {
            m_clientCallback = clientCallback;
        }

        public void ProcessMessage(Message requestMessage)
        {
            this.m_clientCallback.ProcessMessage(requestMessage);
        }
    }
}
