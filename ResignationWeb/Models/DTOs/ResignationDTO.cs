namespace ResignationWeb.Models.DTOs
{
    public class ResignationDTO
    {
        public string id { get; set; }
        public string userId { get; set; }
        public int status { get; set; }
        public DateTime resignationDate { get; set; }
        public DateTime relievingDate { get; set; }
        public string reason { get; set; }
        public string comments { get; set; } = null!;
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string? approvedBy { get; set; }
    }
}
