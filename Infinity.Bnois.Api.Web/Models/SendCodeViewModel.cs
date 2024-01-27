using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infinity.Bnois.Api.Web.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SendCodeViewModel'
    public class SendCodeViewModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SendCodeViewModel'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SendCodeViewModel.SelectedProvider'
        public string SelectedProvider { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SendCodeViewModel.SelectedProvider'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SendCodeViewModel.Providers'
        public ICollection<SelectListItem> Providers { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SendCodeViewModel.Providers'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SendCodeViewModel.ReturnUrl'
        public string ReturnUrl { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SendCodeViewModel.ReturnUrl'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SendCodeViewModel.RememberMe'
        public bool RememberMe { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SendCodeViewModel.RememberMe'
    }
}