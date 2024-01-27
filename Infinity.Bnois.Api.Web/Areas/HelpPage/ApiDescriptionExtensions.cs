using System;
using System.Text;
using System.Web;
using System.Web.Http.Description;

namespace Infinity.Bnois.Api.Web.Areas.HelpPage
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ApiDescriptionExtensions'
    public static class ApiDescriptionExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ApiDescriptionExtensions'
    {
#pragma warning disable CS0419 // Ambiguous reference in cref attribute: 'ApiDescription'. Assuming 'ApiDescription', but could have also matched other overloads including 'ApiDescription'.
#pragma warning disable CS0419 // Ambiguous reference in cref attribute: 'ApiDescription'. Assuming 'ApiDescription', but could have also matched other overloads including 'ApiDescription'.
        /// <summary>
        /// Generates an URI-friendly ID for the <see cref="ApiDescription"/>. E.g. "Get-Values-id_name" instead of "GetValues/{id}?name={name}"
        /// </summary>
        /// <param name="description">The <see cref="ApiDescription"/>.</param>
        /// <returns>The ID as a string.</returns>
        public static string GetFriendlyId(this ApiDescription description)
#pragma warning restore CS0419 // Ambiguous reference in cref attribute: 'ApiDescription'. Assuming 'ApiDescription', but could have also matched other overloads including 'ApiDescription'.
#pragma warning restore CS0419 // Ambiguous reference in cref attribute: 'ApiDescription'. Assuming 'ApiDescription', but could have also matched other overloads including 'ApiDescription'.
        {
            string path = description.RelativePath;
            string[] urlParts = path.Split('?');
            string localPath = urlParts[0];
            string queryKeyString = null;
            if (urlParts.Length > 1)
            {
                string query = urlParts[1];
                string[] queryKeys = HttpUtility.ParseQueryString(query).AllKeys;
                queryKeyString = String.Join("_", queryKeys);
            }

            StringBuilder friendlyPath = new StringBuilder();
            friendlyPath.AppendFormat("{0}-{1}",
                description.HttpMethod.Method,
                localPath.Replace("/", "-").Replace("{", String.Empty).Replace("}", String.Empty));
            if (queryKeyString != null)
            {
                friendlyPath.AppendFormat("_{0}", queryKeyString.Replace('.', '-'));
            }
            return friendlyPath.ToString();
        }
    }
}