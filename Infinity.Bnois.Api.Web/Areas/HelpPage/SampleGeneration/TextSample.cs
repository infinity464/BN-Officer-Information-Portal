using System;

namespace Infinity.Bnois.Api.Web.Areas.HelpPage
{
    /// <summary>
    /// This represents a preformatted text sample on the help page. There's a display template named TextSample associated with this class.
    /// </summary>
    public class TextSample
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'TextSample.TextSample(string)'
        public TextSample(string text)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'TextSample.TextSample(string)'
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            Text = text;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'TextSample.Text'
        public string Text { get; private set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'TextSample.Text'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'TextSample.Equals(object)'
        public override bool Equals(object obj)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'TextSample.Equals(object)'
        {
            TextSample other = obj as TextSample;
            return other != null && Text == other.Text;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'TextSample.GetHashCode()'
        public override int GetHashCode()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'TextSample.GetHashCode()'
        {
            return Text.GetHashCode();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'TextSample.ToString()'
        public override string ToString()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'TextSample.ToString()'
        {
            return Text;
        }
    }
}