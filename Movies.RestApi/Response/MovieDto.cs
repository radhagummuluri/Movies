namespace Movies.RestApi.Response
{
    public class MovieDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        public string Genres { get; set; }
        public string AverageRating { get; set; }
    }
}