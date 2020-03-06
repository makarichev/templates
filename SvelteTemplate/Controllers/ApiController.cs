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


        [HttpGet("mols")]
        public object Mols(string search, int page = 1, int limit = 15)
        {
            
            return dapper.Query(@"
                ;with s as (
	                select *
	                from mols with (nolock)
	                where is_working = 1 and name_logon is not null
                    and @search is null or name like @search + '%'
                )
                , c as (select count(*) as count from s)

	                select 
		                c.count, s.*, d.name as DEPT_NAME
	                from s
                        inner join depts d on d.dept_id = s.dept_id    
                        cross join c
	                order by mol_id
	                offset @start rows fetch next @limit rows only option (recompile)

                ;
            ", new { search, start = limit * (page - 1), limit }).AsQueryable();


        }


    }
}
