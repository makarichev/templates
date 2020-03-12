using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace svelte.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController: ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly SqlConnection dapper;

        public UserController(ILogger<UserController> logger, SqlConnection dapper)
        {
            _logger = logger;
            this.dapper = dapper;
        }


        [HttpGet]
        public object Get()
        {
            var user = dapper.Query(@"select mol_id, name, name_logon from share..mols where name_logon = @name_logon", 
                new { name_logon = HttpContext.User.Identity.Name }).SingleOrDefault();
            return user ?? new {};
        }


        [HttpGet("reglament")]
        public ActionResult Reglament()
        {
            return Ok(new { allowEdit = true });
        }

    }
}
