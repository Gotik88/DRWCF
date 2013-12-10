using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DR.WCF.Service.Duplex;

namespace DR.WCF.Client.Host
{
    public class MyClientCallback : ICallbackDuplexService
    {
        public string Reply(string message)
        {
            return "He says " + message;
        }
    }
}
