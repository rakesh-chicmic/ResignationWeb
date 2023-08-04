namespace ResignationWeb.Models
{
    public class ResignationWithUser 
    {
        public string? id { get; set; }
        public string? userId { get; set; }
        public object? userDetails { get; set; }
        public int status { get; set; }
        public DateTime resignationDate { get; set; }
        public DateTime relievingDate { get; set; }
        public string? reason { get; set; }
        public string comments { get; set; } = null!;
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string? approvedBy { get; set; }
        public object? approverDetails { get; set; }

    }
}
