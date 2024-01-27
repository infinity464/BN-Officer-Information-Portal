using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.Api.Web
{ 
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User'
    public class User : IdentityUser
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User'
    {
        [Required]
        [MaxLength(50)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User.FirstName'
        public string FirstName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User.FirstName'

        [Required]
        [MaxLength(50)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User.LastName'
        public string LastName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User.LastName'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User.IsActive'
        public bool IsActive { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User.IsActive'

        [Required]
        [MaxLength(10)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User.CultureCode'
        public string CultureCode { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User.CultureCode'

        [MaxLength(36)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User.CompanyId'
        public string CompanyId { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User.CompanyId'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User.CreatedBy'
        public string CreatedBy  { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User.CreatedBy'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User.CreatedDate'
        public DateTime? CreatedDate { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User.CreatedDate'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User.InActiveBy'
        public string InActiveBy { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User.InActiveBy'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User.InActiveDate'
        public DateTime? InActiveDate { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User.InActiveDate'


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'User.InActiveDate'
        public string PNo { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'User.PNo'

    }
}