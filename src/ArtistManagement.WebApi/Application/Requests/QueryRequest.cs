using System;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Application.Requests
{
    /// <summary>
    /// Query Request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryRequest<T> where T : struct, IConvertible
    {
        private int page = 0;

        /// <summary>
        /// Construct
        /// </summary>
        public QueryRequest()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
        }

        /// <summary>
        /// A keyword to find
        /// </summary>
        public string Find { get; set; }

        /// <summary>
        /// Find in specific field
        /// </summary>
        /// <value></value>
        public T? FindBy { get; set; }

        /// <summary>
        /// Sort the results by specific field
        /// </summary>
        /// <value></value>
        public T? SortBy { get; set; }

        /// <summary>
        /// Sorting results direction
        /// </summary>
        /// <value></value>
        public SortDirection? SortDirection { get; set; }
        
        /// <summary>
        /// The requested paged data (default 1)
        /// </summary>
        /// <value></value>
        public int Page
        {
            get { return page; }
            set
            {
                int i = value - 1;

                page = (i < 0 ? 0 : i);
            }
        }

        /// <summary>
        /// The requested data size per page (default 10)
        /// </summary>
        /// <value></value>
        public int Size { get; set; } = 10;
    }
}