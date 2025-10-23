namespace Fina.Core.Requests
{
    public abstract class PagedRequest : Request //classe para paginação
    {
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
        public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
    }
}
