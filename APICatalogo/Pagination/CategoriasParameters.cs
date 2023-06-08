namespace APICatalogo.Pagination
{
    public class CategoriasParameters
    {
        public int PageNumber { get; set; } = 1;   
        private int _pageSize = 10;
        private int maxPageSize = 50;
        public int PageSize { get => _pageSize; set {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;             
            } }

    }
}
