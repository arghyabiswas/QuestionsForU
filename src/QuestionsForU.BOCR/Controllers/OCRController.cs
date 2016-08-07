using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestionsForU.OCREngine;
using  System.Threading;

namespace QuestionsForU.BOCR.Controllers
{
    [Route("api/[controller]")]
    public class OCRController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get(CancellationToken cancellationToken)
        {
            return new string[] { "Bengali OCR", "V1.0" };
        }


        // POST api/values
        [HttpPost]
        public async Task<int[]> Post([FromBody]int[] value,CancellationToken cancellationToken)
        {
            var eye = new Retina();
            eye.Input = value;
            return await eye.Recognise();
        }
    }
}
