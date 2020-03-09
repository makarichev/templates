using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SvelteTemplate.Controllers
{
    [ApiController]
    [Route("api")]
    public class AppController: ControllerBase
    {
        private readonly ILogger<AppController> logger;
        private readonly SqlConnection dapper;

        public AppController(ILogger<AppController> logger, SqlConnection dapper)
        {
            this.logger = logger;
            this.dapper = dapper;
        }


        [HttpGet("reglament")]
        public ActionResult Reglament() {
            return Ok(new { allowEdit = true});
        }

    }
}
