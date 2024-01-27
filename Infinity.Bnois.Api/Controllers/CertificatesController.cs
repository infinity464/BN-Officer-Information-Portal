using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Infinity.Bnois.Api.Core;
using Infinity.Bnois.Api.Right;
using Infinity.Bnois.ApplicationService.Interface;
using Infinity.Bnois.ApplicationService.Models;
using Infinity.Bnois.Configuration;
using Infinity.Bnois.Configuration.Models;

namespace Infinity.Bnois.Api.Controllers
{
    [RoutePrefix(BnoisRoutePrefix.Certificates)]
    [EnableCors("*", "*", "*")]
    [ActionAuthorize(Feature = MASTER_SETUP.CERTIFICATES)]

    public class CertificatesController : PermissionController
    {
        private readonly ICertificateService certificateService;

        public CertificatesController(ICertificateService certificateService, IRoleFeatureService roleFeatureService) : base(roleFeatureService)
        {
            this.certificateService = certificateService;
        }

        [HttpGet]
        [Route("get-certificates")]
        public IHttpActionResult GetCertificates(int ps, int pn, string qs)
        {
            int total = 0;
            List<CertificateModel> models = certificateService.GetCertificates(ps, pn, qs, out total);
            RoleFeature permission = base.GetFeature(MASTER_SETUP.CERTIFICATES);
            return Ok(new ResponseMessage<List<CertificateModel>>()
            {
                Result = models,
                Total = total, Permission=permission
            });
        }

        [HttpGet]
        [Route("get-certificate")]
        public async Task<IHttpActionResult> GetCertificate(int id)
        {
            CertificateModel model = await certificateService.GetCertificate(id);
            return Ok(new ResponseMessage<CertificateModel>
            {
                Result = model
            });
        }


        [HttpPost]
        [ModelValidation]
        [Route("save-certificate")]
        public async Task<IHttpActionResult> SaveCertificate([FromBody] CertificateModel model)
        {
            return Ok(new ResponseMessage<CertificateModel>
            {
                Result = await certificateService.SaveCertificate(0, model)
            });
        }



        [HttpPut]
        [Route("update-certificate/{id}")]
        public async Task<IHttpActionResult> UpdateCertificate(int id, [FromBody] CertificateModel model)
        {
            return Ok(new ResponseMessage<CertificateModel>
            {
                Result = await certificateService.SaveCertificate(id, model)
            });
        }


        [HttpDelete]
        [Route("delete-certificate/{id}")]
        public async Task<IHttpActionResult> DeleteCertificate(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await certificateService.DeleteCertificate(id)
            });
        }


    }
}