#nullable disable
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using Vendista.ViewModels;

namespace Vendista.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var client = new RestClient("http://178.57.218.210:198/token?login=part&password=part");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Auth auth = JsonSerializer.Deserialize<Auth>(response.Content);
            string token = auth.token;

            client = new RestClient("http://178.57.218.210:198/commands/types?token="+token);
            request = new RestRequest(Method.GET);
            response = client.Execute(request);
            Command command = JsonSerializer.Deserialize<Command>(response.Content);
            List<Data> items = new();
            foreach (var item in command.items)
            {
                Data data = new();
                data.id = item.id;
                data.name = item.name;
                data.RawData = JsonSerializer.Serialize(item);
                data.Token = token;
                items.Add(data);
            }

            return View(items);
        }
    }

    internal class Auth
    {
        public string token { get; set; }
        public int owner_id { get; set; }
        public int role_id { get; set; }
        public string name { get; set; }
    }

    internal class Command
    {
        public int page_number { get; set; }
        public int items_per_page { get; set; }
        public int items_count { get; set; }
        public List<Item> items { get; set; }
        public bool success { get; set; }
    }

    internal class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string parameter_name1 { get; set; }
        public string parameter_name2 { get; set; }
        public string parameter_name3 { get; set; }
        public string parameter_name4 { get; set; }
        public string str_parameter_name1 { get; set; }
        public string str_parameter_name2 { get; set; }
        public int? parameter_default_value1 { get; set; }
        public int? parameter_default_value2 { get; set; }
        public int? parameter_default_value3 { get; set; }
        public int? parameter_default_value4 { get; set; }
        public string str_parameter_default_value1 { get; set; }
        public string str_parameter_default_value2 { get; set; }
        public bool visible { get; set; }
    }
}