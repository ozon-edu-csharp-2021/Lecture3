using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HttpAndMvc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly AppOptions _appOptions;

        public TestController(IOptionsSnapshot<AppOptions> options)
        {
            _appOptions = options.Value;
        }

        [HttpGet]
        public string Get()
        {
            return _appOptions.ElasticApiKey;
        }
        
        [HttpGet("time")]
        public string Get(int time)
        {
            return DateTime.Now.ToString("f");
        }
    }
}