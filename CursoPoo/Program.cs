using System;
using System.Linq;
using Balta.ContentContext;

namespace Balta
{
    class Program
    {
        static void Main(string[] args)
        {
            var articles = new List<Article>();
            articles.Add(new Article("Artigo sobre OPP", "orientacao-objetos"));
            articles.Add(new Article("Artigo sobre C#", "csharp"));
            articles.Add(new Article("Artigo sobre .NET", "dotnet"));

            foreach (var article in articles)
            {
                Console.WriteLine(article.Id);
                Console.WriteLine(article.Title);
                Console.WriteLine(article.Url);
            }

            var courses = new List<Course>();
            var courseC = new Course("fundamentos C#", "fundamentos-csharp");
            var courseOPP = new Course("fundamentos OPP", "fundamentos-opp");

            courses.Add(courseC);
            courses.Add(courseOPP);

            var careers = new List<Career>();
            var careerDotnet = new Career("Especialista .NET", "especialista-dotnet");
            var careerItem2 = new CareerItem(2, "Aprenda Dotnet..", "", courseC);
            var careerItem = new CareerItem(1, "Comece por aqui", "", courseOPP);

            careerDotnet.Items.Add(careerItem2);
            careerDotnet.Items.Add(careerItem);
            careers.Add(careerDotnet);

            foreach(var c in careers)
            {
                Console.WriteLine(careerDotnet.Title);
                foreach(var item in careerDotnet.Items.OrderBy(c => c.Order))
                {
                    Console.WriteLine($"{item.Order} - {item.Title}");
                    Console.WriteLine(item.Course?.Title);

                    foreach(var notification in item.Notifications)
                    {
                        Console.WriteLine($"{notification.Property} - {notification.Message}");
                    }
                }
            }
        }
    }
}