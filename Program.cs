using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string API_URL = "https://jsonplaceholder.typicode.com/posts?_limit=5";
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(API_URL);
        string content = await response.Content.ReadAsStringAsync();
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Post>();

        foreach (Post post in posts)
        {
            Console.WriteLine($"ID: {post.Id}, Title: {post.Title}");
        }
    }
}

class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}
