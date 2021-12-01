using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace dnc200_commits_cli
{
    public class Program
    {


        public void  GetCommits(String username, String repoName = "")
        {
            int count = 0;
            string url = "";
            string responseBody = "";
            //HttpClient client = new HttpClient();
            
            if (repoName != "")
            {
                url = $"https://api.github.com/repos/{username}/{repoName}/events/public";
            } else
            {
                url = $"https://api.github.com/users/{username}/events/public";
            }
            //Console.WriteLine(url);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");//Set the User Agent to "request"

                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        responseBody = response.Content.ReadAsStringAsync().Result;
                    }
                }
                //HttpResponseMessage response = client.GetAsync(url).Result;
               // Console.WriteLine(response.EnsureSuccessStatusCode());
                //responseBody = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            Console.WriteLine("YES!!");
           List<Event> myDeserializedClass = JsonConvert.DeserializeObject<List<Event>>(responseBody);
            foreach (Event e in myDeserializedClass)
            {
             if (e.type == "PushEvent")
               {
                    count += e.payload.commits.Count;
              }
            }
            Console.WriteLine(count);
          
        }
        public static void Main(string[] args)
        {
            //hint: this is the base of the github API url "https://api.github.com/"

            //capture user input to determine if they want to search a user or a repository
            Console.WriteLine("Welcome to the Commit Count App");
            Console.WriteLine("Would you like to search for a repository or a single user commits");
            Console.WriteLine("Type U for user or R for repository");

            Program program = new Program();
            string search = Console.ReadLine().ToUpper();
            int numCommits;

            try
            {
                if (search != "U" && search != "R")
                {
                    Console.WriteLine("Please provide a valid input : U or R");
                }

                Console.WriteLine("Type you Github username:");
                string username = Console.ReadLine().ToLower(); 

                if (search == "U") {
                    Console.WriteLine("Hello 1");
                    //numCommits = program.GetCommits(username);
                    program.GetCommits(username);
                    //Console.WriteLine($"Number of commits for {username}: {numCommits}");
                    Console.WriteLine("Hello 2");
                } else {
                    Console.WriteLine("Hola 1");
                    Console.WriteLine("Type you Github repository:");
                    String repoName = Console.ReadLine().ToLower();
                    //numCommits = program.GetCommits(username, repoName);
                    program.GetCommits(username, repoName);
                    // Console.WriteLine($"Number of commits for {repoName}: {numCommits}");
                    Console.WriteLine("Hola 2");
                } 
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        
        }

    }
}
