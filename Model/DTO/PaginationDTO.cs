namespace BillManager.Model.DTO
{
    public class PaginationDTO<T>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public List<T> Items { get; set; }
    }
}
