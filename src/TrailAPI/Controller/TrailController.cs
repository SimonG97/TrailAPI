using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
namespace TrailAPI.AddControllers
{
    
    [ApiController]
    public class TrailController:ControllerBase
    {
        [HttpGet("api/trail")]
        public ActionResult<IEnumerable<string>> Get()
        {
          return new string[]{"This","is","hard","coded"};
        }
    }
}