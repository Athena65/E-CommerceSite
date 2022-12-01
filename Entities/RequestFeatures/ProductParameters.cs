namespace Entities.RequestFeatures
{
    public class ProductParameters
    {
        const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _PageSize=4;  
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize =(value>MaxPageSize) ? MaxPageSize:value;
            }
        }
        public string? SearchTerm { get; set; }
        public string? OrderBy { get; set; } = "name"; //deafult
    }
}
