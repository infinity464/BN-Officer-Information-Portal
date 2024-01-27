using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois
{
    public static class ExtenssionMethod
    {
        /// <summary>
        /// Async create of a System.Collections.Generic.List<T> from an 
        /// System.Collections.Generic.IQueryable<T>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="list">The System.Collections.Generic.IEnumerable<T> 
        /// to create a System.Collections.Generic.List<T> from.</param>
        /// <returns> A System.Collections.Generic.List<T> that contains elements 
        /// from the input sequence.</returns>
        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> list)
        {
            return Task.Run(() => list.ToList());
        }

        public static Guid ToGuid(this string aString)
        {
            Guid newGuid;

            if (string.IsNullOrWhiteSpace(aString))
            {
                return Guid.Empty;
            }

            if (Guid.TryParse(aString, out newGuid))
            {
                return newGuid;
            }

            return Guid.Empty;
        }

        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime dt)
        {
            return dt.FirstDayOfWeek().AddDays(6);
        }

        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1);
        }
        public static string PadingWith(this string maxRef, int length, string padChar)
        {
            string newRef = "";
            if (String.IsNullOrEmpty(maxRef) && length > 0)
            {
                long numberValue = 1;
                newRef = padChar + (numberValue.ToString().PadLeft(length, '0'));
            }
            else
            {
                long numberValue = Convert.ToInt64(maxRef) + 1;
                newRef = padChar+(numberValue.ToString().PadLeft(length,'0'));

            }
            return newRef;
        }
        public static string PadingWith(this string maxRef, int length)
        {
            string newRef = "";
            if (String.IsNullOrEmpty(maxRef) && length > 0)
            {
                long numberValue = 1;
                newRef = Convert.ToString(numberValue).PadLeft(length, '0');
            }
            else
            {
                long numberValue = Convert.ToInt64(maxRef) + 1;
                newRef = Convert.ToString(numberValue).PadLeft(length, '0');
            }
            return newRef;
        }

        private static dynamic BindDynamic(DataRow dataRow)
        {
            dynamic result = null;
            if (dataRow != null)
            {
                result = new ExpandoObject();
                var resultDictionary = (IDictionary<string, object>)result;
                foreach (DataColumn column in dataRow.Table.Columns)
                {
                    var dataValue = dataRow[column.ColumnName];
                    resultDictionary.Add(column.ColumnName, DBNull.Value.Equals(dataValue) ? null : dataValue);
                }
            }
            return result;
        }
        public static IEnumerable<dynamic> ToJson(this DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return BindDynamic(row);
            }
        }
        public static string GenerateId(this Guid guid)
        {
            long i = 1;
            foreach (byte b in guid.ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public static long GenerateLongId(this Guid guid)
        {
            byte[] buffer = guid.ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }


        public static string ToBase64ImageUrl(this byte[] file,string extension)
        {
            if (!file.Any()&& !extension.Contains("."))
            {
                return "";
            }
            string strBase64 = Convert.ToBase64String(file);
            return  String.Format("data:image/{0};base64," + strBase64, extension.Split('.')[1]);

        }

        public static Bitmap ResizeImage(this Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }



    }

}
