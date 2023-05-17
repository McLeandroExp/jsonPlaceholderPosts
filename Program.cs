using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    public static async Task getPosts(int start, int limit)
    {
        string API_URL = $"https://jsonplaceholder.typicode.com/posts?_start={start}&_limit={limit}";
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(API_URL);
        string content = await response.Content.ReadAsStringAsync();
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Post>();

        foreach (Post post in posts)
        {
            Console.WriteLine($"ID: {post.Id}, Title: {post.Title}");
        }
    }

    static async Task Main(string[] args)
    {
        Boolean continuar = true;
        int start = 0;
        int limit = 5;
        do
        {
            Console.WriteLine("Escoja una opcion :");
            Console.WriteLine($"1. Ver los post desde {start} hasta {limit}:");
            Console.WriteLine($"2. Terminar la ejecucion :");
            string entrada = Console.ReadLine() ?? "2";
            switch (entrada)
            {
                case "1":
                    await getPosts(start, 5);
                    start += 5;
                    limit += 5;
                    break;
                case "2":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("No ha seleccionado ninguna de las opciones");
                    break;
            }
        } while (continuar);
    }
}


class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}
