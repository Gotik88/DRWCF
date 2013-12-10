using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DR.WCF.Service.Duplex
{
    [ServiceContract(CallbackContract = typeof(ICallbackDuplexService), SessionMode = SessionMode.Required)]
    public interface IDuplexService
    {
        [OperationContract]
        string ProcessMessage(string message);
    }
}
