namespace ArtistManagement.WebApi.Infrastructure
{
    public class Page
    {
        public int Current { get; private set; }
        public int Total { get; private set; }
        public bool Next { get; private set; }
        public bool Previous { get; private set; }
    }
}