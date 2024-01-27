using System;

namespace Infinity.Bnois.Api.Web.Areas.HelpPage
{
    /// <summary>
    /// This represents an image sample on the help page. There's a display template named ImageSample associated with this class.
    /// </summary>
    public class ImageSample
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSample"/> class.
        /// </summary>
        /// <param name="src">The URL of an image.</param>
        public ImageSample(string src)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }
            Src = src;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ImageSample.Src'
        public string Src { get; private set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ImageSample.Src'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ImageSample.Equals(object)'
        public override bool Equals(object obj)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ImageSample.Equals(object)'
        {
            ImageSample other = obj as ImageSample;
            return other != null && Src == other.Src;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ImageSample.GetHashCode()'
        public override int GetHashCode()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ImageSample.GetHashCode()'
        {
            return Src.GetHashCode();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'ImageSample.ToString()'
        public override string ToString()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'ImageSample.ToString()'
        {
            return Src;
        }
    }
}