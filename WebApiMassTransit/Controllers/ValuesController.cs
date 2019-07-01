using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace WebApiMassTransit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRequestClient<MessageText> _requestClient;

        public ValuesController(IRequestClient<MessageText> requestClient)
        {
            _requestClient = requestClient;
        }

        // GET api/values
        [HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            //return new string[] { "value1", "value2" };
            try
            {
                var request = _requestClient.Create(new { MessageText = "Hello, World." }, cancellationToken);
                var response = await request.GetResponse<MessageText>();
                return Content($"{response.Message.Text}");//, //MessageId: {response.MessageId:D}");
            }
            catch (RequestTimeoutException exception)
            {
                return StatusCode((int)HttpStatusCode.RequestTimeout);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
