namespace Fina.Core.Requests
{
    public class PagedRequest //classe para paginação
    {
        public int PageSize { get; set; } = 25;
        public int PageNumber { get; set; } = 1;
    }
}
