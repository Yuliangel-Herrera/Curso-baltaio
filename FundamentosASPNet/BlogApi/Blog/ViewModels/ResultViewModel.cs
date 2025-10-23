namespace Blog.ViewModels
{
    public class ResultViewModel<T>
    {
        public ResultViewModel(T data, List<string> errors) 
        { 
            Data = data;
            Errors = errors;
        }
        public ResultViewModel(T data) // O que recebo se de certo
        {
            Data = data;
        }
        public ResultViewModel(List<string> errors) // O que recebo se de errado
        {
            Errors = errors;
        }
        public ResultViewModel(string error) // Quando recebo só um erro
        {
            Errors.Add(error);
        }
        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new();
    }
}
