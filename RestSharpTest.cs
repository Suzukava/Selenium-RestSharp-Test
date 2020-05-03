using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace selenium_tests
{
    [TestFixture]
    public class RestSharpTest
    {
        private string getUrl = "https://docs.microsoft.com/api/search?search=LINQ&locale=ru-ru&scoringprofile=search_for_en_us_a_b_test&facet=category&%24skip=0&%24top=10";

        [OneTimeSetUp] // вызывается перед началом запуска всех тестов
        public void OneTimeSetUp()
        {
            
        }

        [OneTimeTearDown] //вызывается после завершения всех тестов
        public void OneTimeTearDown()
        {
            
        }

        [SetUp] // вызывается перед каждым тестом
        public void SetUp()
        {

        }

        [TearDown] // вызывается после каждого теста
        public void TearDown()
        {

        }

        [Test]
        public void TEST_1()
        {
            IRestClient restClien = new RestClient();
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine(i);
                string getUrl1 = "https://docs.microsoft.com/api/search?search=LINQ&locale=ru-ru&scoringprofile=search_for_en_us_a_b_test&facet=category&%24skip=" + i + "0&%24top=10";

                IRestRequest restReaquest = new RestRequest(getUrl1);
                restReaquest.AddHeader("Accept", "application/json");

                IRestResponse restResponse = restClien.Get(restReaquest);
                if (restResponse.IsSuccessful)
                {
                    Console.WriteLine(restResponse.StatusCode);
                    var o = JObject.Parse(restResponse.Content);

                    var result = o["results"];
                    foreach (var res in result)
                    {
                        StringBuilder sb = new StringBuilder((string)res["title"]);
                        foreach (var desc in res["descriptions"])
                        {
                            sb.Append(" ; ");
                            sb.Append((string)desc["content"]);
                        }
                        string s1 = sb.ToString();
                        string s2 = "linq";
                        bool b = s1.ToLower().Contains(s2);                       

                        if (b == true)
                        {
                            Console.WriteLine($"Result: {b} \n Search result contains sought-for word");
                        }
                        else
                        {
                            Console.WriteLine($"Result: {b} \n Search result not contains sought-for word");
                        }
                        Assert.IsTrue(b);
                    }

                }
            }

        }
    }
}
