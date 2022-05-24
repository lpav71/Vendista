#nullable disable
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace Vendista.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TerminalController : ControllerBase
    {
        [HttpPost]
        public ActionResult SendToTerminal([FromForm]string command_id, [FromForm]string parameter_1, [FromForm]string parameter_2, [FromForm]string parameter_3, [FromForm]string parameter_4, [FromForm]string terminal, [FromForm]string token)
        {
            string url = "http://178.57.218.210:198/terminals/" + terminal + "/commands?token=" + token;
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            string body = "{\"command_id\":" + command_id;
            if (parameter_1 != "0") 
                body += ",\"parameter1\":" + parameter_1;
            if (parameter_2 != "0") 
                body += ",\"parameter2\":" + parameter_2;
            if (parameter_3 != "0") 
                body += ",\"parameter3\":" + parameter_3;
            if (parameter_4 != "0") 
                body += ",\"parameter4\":" + parameter_4;
            body += "}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return new OkObjectResult(response.Content);
        }
    }    
}
