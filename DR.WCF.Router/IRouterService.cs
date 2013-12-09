
using System;

namespace DR.WCF.Router
{
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    [ServiceContract(Namespace = "http://www.thatindigogirl.com/samples/2008/01")]
    public interface IRouterService : IDisposable
    {
        [OperationContract(Action = "*", ReplyAction = "*")]
        Message ProcessMessage(Message requestMessage);
    }
}
