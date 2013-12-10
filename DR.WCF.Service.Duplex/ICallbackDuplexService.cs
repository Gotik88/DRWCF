using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DR.WCF.Service.Duplex
{
    public interface ICallbackDuplexService
    {
        [OperationContract]
        string Reply(string message);
    }
}
