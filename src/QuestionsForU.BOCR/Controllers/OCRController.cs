using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestionsForU.OCREngine;

namespace QuestionsForU.BOCR.Controllers
{
    [Route("api/[controller]")]
    public class OCRController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return new string[] { "Bengali OCR", "V1.0" };
        }


        // POST api/values
        [HttpPost]
        public async Task<IEnumerable<int>> Post([FromBody]int[] value)
        {
            var eye = new Retina();
            eye.Input = value;
            eye.Narrow();
            return eye.Output;
        }
    }
}
