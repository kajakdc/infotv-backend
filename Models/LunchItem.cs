namespace infotv.Models
{
    public class  LunchItem
    {
        public long ID { get; set; }
        public string Food { get; set; }
        public long CreatedAt { get; set; }  //DateTimeOffset.Now.ToUnixTimeSeconds();
    }
}