namespace GoldenRaspberry.Api.Models
{
    public class MovieProducer
    {
        public int Id { get; set; } 
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
    }
}