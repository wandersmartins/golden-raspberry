namespace GoldenRaspberry.Api.Models
{
    public class ProducerIntervalDto
    {
        public string ProducerName { get; set; }
        public int Interval { get; set; }
        public int PreviousYear { get; set; }
        public int FollowingYear { get; set; }
    }
}
