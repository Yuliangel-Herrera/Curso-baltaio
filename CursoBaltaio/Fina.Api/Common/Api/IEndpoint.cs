namespace Fina.Api.Common.Api
{
    public interface IEndpoint
    {
        //substituição do app.MapGet no program
        static abstract void Map(IEndpointRouteBuilder app);
    }
}
