using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace DR.WCF.Router
{
    [ServiceContract(Namespace = "http://www.thatindigogirl.com/samples/2008/01",
                     SessionMode = SessionMode.Required,
                     CallbackContract = typeof(IDuplexRouterCallback))]
    public interface IDuplexRouterService
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void ProcessMessage(Message requestMessage);
    }

    [ServiceContract(Namespace = "http://www.thatindigogirl.com/samples/2008/01", SessionMode = SessionMode.Allowed)]
    public interface IDuplexRouterCallback
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void ProcessMessage(Message requestMessage);
    }
}
