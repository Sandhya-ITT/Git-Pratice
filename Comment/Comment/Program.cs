using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Comments
{
    class DisplayBlog
    {
  
        public void Details(dynamic jsonObject)
        {
            try
            {
                Console.WriteLine("title :" + jsonObject["tumblelog"]["title"].ToString());
                Console.WriteLine("name :" + jsonObject["tumblelog"]["name"].ToString());
                Console.WriteLine("description :" + jsonObject["tumblelog"]["description"].ToString());
                Console.WriteLine("number of posts :" + jsonObject["posts-total"].ToString());
                JArray postsLength = (JArray)jsonObject["posts"];
                for (int index = 0; index < postsLength.Count; index++)
                {
                    Console.WriteLine(jsonObject["posts"][index]["photo-url-1280"].ToString());
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        //<summary>Main class to execute all the functions</summary>
        static void Main(string[] args)
        {
            var client = new WebClient();
            Console.WriteLine("Enter the blog name");
            string tumblrBlogName = Console.ReadLine();
            Console.WriteLine("Enter Min range");
            string minRange = Console.ReadLine();
            Console.WriteLine("Enter the max range");
            string maxRange = Console.ReadLine();
            string tumblrUrl = "https://" + tumblrBlogName + ".tumblr.com/api/read/json?photo&num=" + maxRange + "&start" + minRange;
            string response = client.DownloadString(tumblrUrl);
            while (response.StartsWith("var tumblr_api_read = "))
            {
                response = response.Substring("var tumblr_api_read = ".Length);    //Removing the unwanted characters to make it a valid json
            }
            response = response.Replace(";", "");
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response);
            DisplayBlog blog = new DisplayBlog();
            blog.Details(jsonObject);
        }
    }
}
