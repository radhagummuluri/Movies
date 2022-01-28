namespace Movies.RestApi.Data.ReadModel
{
    public class MovieReadModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        public string Genres { get; set; }
        public double CalculatedAverageRating { get; set; }
    }
}
