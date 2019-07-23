using System;

namespace ArtistManagement.WebApi.Infrastructure
{
    public class PageDto
    {
        public PageDto(int current, int total, int size)
        {
            Current = current;
            Total = (int)Math.Ceiling((double)total / (double)size);
            Next = Current < Total && Current >= 1;
            Previous = Current > 1 && Current <= Total;
        }

        public int Current { get; private set; }
        public int Total { get; private set; }
        public bool Next { get; private set; }
        public bool Previous { get; private set; }
    }
}