using System.Collections.Generic;

namespace ArtistManagement.WebApi.Infrastructure
{
    public class CollectionDto<T> where T : Entity
    {
        public IEnumerable<T> Data { get; set; }
        public int Total { get; set; }
        public int Size { get; set; }
        public PageDto Page { get; private set; }
        public CollectionDto<T> WithPage (int page) 
        {
            Page = new PageDto(page, Total, Size);

            return this;
        }
    }
}