using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace DR.WCF.Router.Registration
{
    public static class RegistrationHelper
    {
        static public IDictionary<int, RegistrationInfo> RegistrationList = new Dictionary<int, RegistrationInfo>();

        public static void Register(RegistrationInfo regInfo)
        {
            if (!RegistrationList.ContainsKey(regInfo.GetHashCode()))
            {
                RegistrationList.Add(regInfo.GetHashCode(), regInfo);
            }
        }

        public static void UnRegister(RegistrationInfo regInfo)
        {
            if (RegistrationList.ContainsKey(regInfo.GetHashCode()))
            {
                RegistrationList.Remove(regInfo.GetHashCode());
            }
        }

        ////string contractNamespace =requestMessage.Headers.Action.Substring(0,requestMessage.Headers.Action.LastIndexOf("/"));

        // get a list of all registered service entries for 
        // the specified contract
        /*var results = from item in RouterService.RegistrationList
                      where item.Value.ContractNamespace.Contains(contractNamespace)
                      select item;*/

        ////int index = 0;
        // find the next address used ...

        // create the channel 
        ////RegistrationInfo regInfo = results.ElementAt<KeyValuePair<int,RegistrationInfo>>(index).Value;

        /*Uri addressUri = new Uri(regInfo.Address);
        Binding binding = ConfigurationUtility.GetRouterBinding(addressUri.Scheme);
        EndpointAddress endpointAddress = new EndpointAddress(regInfo.Address);*/

    }
}
