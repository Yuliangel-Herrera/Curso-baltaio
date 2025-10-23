using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Extensions
{
    public static class ModelStateExtension
    {
        //This: Adiciona o método de extensão criado (GerErrors) a todos os tipos ModelStateDictionary (no caso ModelState)
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach(var item in modelState.Values)
            {
                foreach(var error in item.Errors)
                {
                    result.Add(error.ErrorMessage);
                }
            }
            return result;
        }
    }
}
