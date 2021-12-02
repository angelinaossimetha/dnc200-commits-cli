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
    public static class Program
    {


        public  static int GetCommits(String username, String repoName = "")
        { 
            if (username == "" && repoName == "") { return 0; }
            int count = 0;
            string url = "";
            string responseBody = "";
        
            
            if (repoName != "")
            {
                url = $"https://api.github.com/repos/{username}/{repoName}/events";
            } else
            {
                url = $"https://api.github.com/users/{username}/events/public";
            }
          
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
                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
           
            List<Event> myDeserializedClass = JsonConvert.DeserializeObject<List<Event>>(responseBody);
            foreach (Event e in myDeserializedClass)
            {
             if (e.type == "PushEvent")
               {
                    count += e.payload.commits.Count;
              }
            }
            return count;
          
        }
        public static void Main(string[] args)
        {
            //hint: this is the base of the github API url "https://api.github.com/"

            //capture user input to determine if they want to search a user or a repository
            Console.WriteLine("Welcome to the Commit Count App");
            Console.WriteLine("Would you like to search for a repository or a single user commits");
            Console.WriteLine("Type U for user or R for repository");

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
                    numCommits = Program.GetCommits(username);
                    Console.WriteLine($"Number of commits for {username} in the past 90 days: {numCommits}");
                    
                } else {
                    Console.WriteLine("Type you Github repository:");
                    String repoName = Console.ReadLine().ToLower();
                    numCommits = Program.GetCommits(username, repoName);
                    Console.WriteLine($"Number of commits for {repoName} in the past 90 days: {numCommits}");
                } 
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        
        }

    }
}
