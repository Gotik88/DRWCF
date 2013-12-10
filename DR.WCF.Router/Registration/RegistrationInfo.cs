
namespace DR.WCF.Router.Registration
{
    using System.Runtime.Serialization;

    [DataContract(Namespace = "http://schemas.thatindigogirl.com/samples/2008/01")]
    public class RegistrationInfo
    {
        [DataMember(IsRequired = true, Order = 1)]
        public string Address { get; set; }

        [DataMember(IsRequired = true, Order = 2)]
        public string ContractName { get; set; }

        [DataMember(IsRequired = true, Order = 3)]
        public string ContractNamespace { get; set; }

        public override int GetHashCode()
        {
            return this.Address.GetHashCode() +
            this.ContractName.GetHashCode() +
            this.ContractNamespace.GetHashCode();
        }
    }
}
