namespace Fina.Api
{
    public static class ApiConfiguration
    {
        public const string UserId = "UserId";
        public static string ConnectionString { get; set; } = string.Empty;
        public static string CorsPolicyName = "wasm";
    }
}
