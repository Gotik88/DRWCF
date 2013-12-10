
namespace DR.WCF.Router.Registration
{
    using System.ServiceModel;

    [ServiceContract(Namespace = "http://www.thatindigogirl.com/samples/2008/01")]
    public interface IRegistrationService
    {
        [OperationContract]
        void Register(RegistrationInfo regInfo);

        [OperationContract]
        void Unregister(RegistrationInfo regInfo);
    }
}
