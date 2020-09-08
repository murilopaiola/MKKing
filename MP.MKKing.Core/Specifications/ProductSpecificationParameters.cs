namespace MP.MKKing.Core.Specifications
{
    /// <summary>
    /// An object that contains parameters for sorting, filtering and paging
    /// </summary>
    public class ProductSpecificationParameters
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize 
        { 
            get => _pageSize; 
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }
        private string _search;
        public string Search 
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}