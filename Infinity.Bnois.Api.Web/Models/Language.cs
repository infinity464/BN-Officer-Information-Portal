using System.ComponentModel.DataAnnotations;

namespace Infinity.Bnois.Api.Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Language'
    public class Language
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Language'
    {
        [Key]
        [MaxLength(5)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Language.CultureCode'
        public string CultureCode { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Language.CultureCode'

        [MaxLength(50)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Language.DisplayName'
        public string DisplayName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Language.DisplayName'
    }
}