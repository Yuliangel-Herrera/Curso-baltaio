namespace Fina.Core.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty; //inicializa a propriedade com o valor vazio, ou seja, pode ser null
        public string? Description { get; set; }
        public string UserId { get; set; } = string.Empty; //Inicializa a propriedade com uma string vazia ("").
    }
}
