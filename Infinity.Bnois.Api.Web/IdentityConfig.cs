using System;
using System.Configuration;

namespace Infinity.Bnois.Api.Web
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig'
    public class IdentityConfig
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.Authority'
        public static string Authority
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.Authority'
        {
            get { return ConfigurationManager.AppSettings["Authority"]; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.IssuerUri'
        public static string IssuerUri
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.IssuerUri'
        {
            get { return ConfigurationManager.AppSettings["IssuerUri"]; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.PublicOrigin'
        public static string PublicOrigin
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.PublicOrigin'
        {
            get { return ConfigurationManager.AppSettings["PublicOrigin"]; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.IsMemoryUse'
        public static bool IsMemoryUse
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.IsMemoryUse'
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["IsMemoryUse"]); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.ApplicantUri'
        public static string ApplicantUri
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.ApplicantUri'
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["ApplicantUri"]); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.IsEnableTwoFactor'
        public static bool IsEnableTwoFactor
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.IsEnableTwoFactor'
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["IsEnableTwoFactor"]); }
        }
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.IsEnableRegFee'
        public static bool IsEnableRegFee
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'IdentityConfig.IsEnableRegFee'
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["IsEnableRegFee"]); }
        }

    }
}