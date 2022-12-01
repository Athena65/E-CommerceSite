using Entities.RequestFeatures;

namespace ClientSide.Features
{
    public class PagingResponse<T> where T : class
    {
        public List<T>? Items { get; set; }
        public MetaData? MetaData { get; set; }
    }
}
