using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DR.WCF.Service.Duplex
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class DuplexService : IDuplexService
    {
        public string ProcessMessage(string message)
        {
            var callback = OperationContext.Current.GetCallbackChannel<ICallbackDuplexService>();
            return callback.Reply(message);
        }
    }
}
