using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace dnc200_commits_cli
{
    public class Program
    { 
    public int GetCommits(String username, String repoName = "")
        {
            int count = 0;
            string url = "";
            WebClient client = new WebClient();
            if (repoName != "")
            {
                url = string.Format($"https://api.github.com/repos/{username}/{repoName}/events");
            } else
            {
                url = string.Format($"https://api.github.com/users/{username}/events");
            }
            string responseString = client.DownloadString(url);
            List<Event> myDeserializedClass = JsonConvert.DeserializeObject<List<Event>>(responseString);
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

            Program program = new Program();
            string search = Console.ReadLine().ToUpper();
            int numCommits; 

            try
            {
                if (search != "U" || search != "R")
                {
                    Console.WriteLine("Please provide a valid input : U or R");
                }

                Console.WriteLine("Type you Github username:");
                string username = Console.ReadLine().ToLower(); 

                if (search == "U") {
                    Console.WriteLine("Hello 1");
                    numCommits = program.GetCommits(username);
                    Console.WriteLine($"Number of commits for {username}: {numCommits}");
                    Console.WriteLine("Hello 2");
                } else {
                    Console.WriteLine("Hola 1");
                    Console.WriteLine("Type you Github repository:");
                    String repoName = Console.ReadLine().ToLower();
                    numCommits = program.GetCommits(username, repoName);
                    Console.WriteLine($"Number of commits for {repoName}: {numCommits}");
                    Console.WriteLine("Hola 2");
                } 
            } catch (Exception ex)
            {
                Console.WriteLine("Please provide a valid input : U or R");
            }
        }

    }
}
