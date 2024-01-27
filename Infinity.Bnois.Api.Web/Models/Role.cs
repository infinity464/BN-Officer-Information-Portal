using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.Api.Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Role'
    public class Role : IdentityRole
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Role'
    {
        [MaxLength(36)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Role.CompanyId'
        public string CompanyId { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Role.CompanyId'
    }
}