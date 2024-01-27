
using Infinity.Bnois.Configuration.Models;
namespace Infinity.Bnois.Configuration.Data
{
    public class CompanyConfiguration
    {
       
        public ILoggedInUser LoggedInUser
        {
            get; set;
        }
        public string SignatureUploadFolder
        {
            get
            {
                return string.Format("{0}/{1}", PortalConfig.BaseImageUploadFolder, "Signature");
            }
        }
        public string PictureUploadFolder
        {
            get
            {
                return string.Format("{0}/{1}", PortalConfig.BaseDocumentUploadFolder, "File");
            }
        }
        public string DocumentUploadFolder
        {
            get
            {
                return string.Format("{0}/{1}", PortalConfig.BaseDocumentUploadFolder, "File");
            }
        }


    }
}