using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.V1.Models
{
    /// <summary>
    /// Artist
    /// </summary>
    public class ArtistModel : BaseModel
    {
        /// <summary>
        /// The artist id
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// Name of artist
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// The artist nationality
        /// </summary>
        /// <value></value>
        public string Nationality { get; set; }
    }
}