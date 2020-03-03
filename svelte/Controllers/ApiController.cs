using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapper;

namespace svelte.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly SqlConnection dapper;

        public ApiController(ILogger<ApiController> logger, SqlConnection dapper)
        {
            _logger = logger;
            this.dapper = dapper;
        }

        [HttpGet]
        public object Get()
        {
            return dapper.Query("select @@servername as name;").Single();
        }
    }
}
