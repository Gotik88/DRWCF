
namespace DR.WCF.Server
{
    using System;

    public class ConfigurationService : IConfigurationService
    {
        public string GetSection(string key)
        {
            return string.Format("You entered: {0}", key);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
