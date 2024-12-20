namespace GoldenRaspberry.Api.Models
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieStudio> MovieStudios { get; set; } = new List<MovieStudio>();
    }

}
