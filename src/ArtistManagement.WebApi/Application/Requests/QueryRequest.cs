using System;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Application.Requests
{
    public class QueryRequest<T> where T : struct, IConvertible
    {
        private int page = 0;

        public QueryRequest()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
        }

        public string Find { get; set; }
        public T? FindBy { get; set; }
        public T? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
        public int Page
        {
            get { return page; }
            set
            {
                int i = value - 1;

                page = (i < 0 ? 0 : i);
            }
        }
        public int Size { get; set; } = 10;
    }
}