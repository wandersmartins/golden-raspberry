namespace GoldenRaspberry.Api.Models
{
    public class MovieStudio
    {
        public int Id { get; set; }
        public int MovieId { get; set; } 
        public Movie Movie { get; set; }
        public int StudioId { get; set; }
        public Studio Studio { get; set; }
    }
}
