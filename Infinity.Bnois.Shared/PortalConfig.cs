using System;
using System.Configuration;
namespace Infinity.Bnois
{
    public class PortalConfig
    {
        public static string Authority
        {
            get { return ConfigurationManager.AppSettings["Authority"]; }
        }
        public static string IssuerUri
        {
            get { return ConfigurationManager.AppSettings["IssuerUri"]; }
        }

        public static string PublicOrigin
        {
            get { return ConfigurationManager.AppSettings["PublicOrigin"]; }
        }

        public static bool IsMemoryUse
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["IsMemoryUse"]); }
        }

        public static string BaseImageUploadFolder
        {
            get { return ConfigurationManager.AppSettings["BaseImageUploadFolder"]; }
        }

        public static int YearFrom
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["YearFrom"]); }
        }
        public static string BaseDocumentUploadFolder
        {
            get { return ConfigurationManager.AppSettings["BaseDocumentUploadFolder"]; }
        }
        
    }
}
