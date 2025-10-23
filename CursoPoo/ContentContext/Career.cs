using Balta.ContentContext.Enums;

namespace Balta.ContentContext
{
    public class Career : Content
    {
        public Career(string title, string url) : base(title, url)
        {
            Items = new List<CareerItem>();
        }
        public IList<CareerItem> Items { get; set; }
        public int TotalCourses => Items.Count; //{ get { return Items.Count; } } -> quando for uma lógica mais complexa
    }
}
