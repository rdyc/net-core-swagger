using Newtonsoft.Json.Converters;

namespace ArtistManagement.WebApi.Infrastructure
{
    /// <summary>
    /// Date fotmat converter
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.Converters.IsoDateTimeConverter" />
    public class DateFormatConverter : IsoDateTimeConverter
    {
        private readonly string _format = "yyyy-MM-dd";

        /// <summary>
        /// Initializes a new instance of the <see cref="DateFormatConverter"/> class.
        /// </summary>
        public DateFormatConverter()
        {
            DateTimeFormat = _format;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateFormatConverter"/> class.
        /// </summary>
        /// <param name="format">The format.</param>
        public DateFormatConverter(string format)
        {
            DateTimeFormat = string.IsNullOrEmpty(format) ? _format : format;
        }
    }
}