namespace Fina.Core.Requests
{
    public abstract class Request //classe base (padroniza as request)
    {
        public string UserId { get; set; } = string.Empty;
    }
}
