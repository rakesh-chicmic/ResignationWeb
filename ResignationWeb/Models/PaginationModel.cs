namespace ResignationWeb.Models
{
    public class PaginationModel
    {     
            public int CurrentPage { get; set; } = 1;
            public int ItemsPerPage { get; set; } = 2;
            public int TotalItems { get; set; }
            public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
    }
}
