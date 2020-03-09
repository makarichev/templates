using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapper;
using Newtonsoft.Json;

namespace svelte.Controllers
{
    [ApiController]
    [Route("api/mols")]
    public class MolsController : ControllerBase
    {
        private readonly ILogger<MolsController> _logger;
        private readonly SqlConnection dapper;

        public MolsController(ILogger<MolsController> logger, SqlConnection dapper)
        {
            _logger = logger;
            this.dapper = dapper;
        }


        [HttpGet("filter")]
        public ActionResult Filter() {
            return Ok(new MolsFilter());
        }



        [HttpGet]
        public async Task<ActionResult<object>> Mols([FromQuery] MolsFilter filter = default(MolsFilter))
        {

            

            var res = await dapper.QueryAsync($@"
                ;with s as (
	                select 
                        s.MOL_ID, s.NAME, s.NAME_LOGON, s.EMAIL, s.PHONE_LOCAL, s.DATE_HIRE
                        , d.name as DEPT_NAME
                        , p.name as POST_NAME
	                from mols s (nolock)
                        inner join depts d on d.dept_id = s.ddept_id    
                        inner join mols_posts p on p.post_id = s.post_id
	                where s.is_working = 1
                        and (@search is null or s.name like @search + '%')
                        and (@date is null or s.DATE_HIRE <= @date)
                        and (@deptid is null or s.ddept_id = @deptid)
                        and (@postid is null or s.post_id = @postid)
                )
                , c as (select count(*) as count from s)

	                select 
		                c.count, s.*
	                from s
                        cross join c
	                order by {filter.Sort}
	                offset (@page - 1) * @limit rows fetch next @limit rows only option (recompile)

                ;
            ", filter);

            //var totals = res.FirstOrDefault()?.count;


            HttpContext.Response.Headers.Add("pager", 
                JsonConvert.SerializeObject(new {
                    offset = filter.Limit * (filter.Page - 1), 
                    limit = filter.Limit, 
                    total = res.Select(x => x.count).FirstOrDefault(),

                })
            );

            return Ok(res);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> MolAsync(int id) {
            return Ok(
                (await dapper.QueryAsync(@"
                    select * from mols where mol_id = @id;
                ", new { id })).Single()
                );
        }

        [HttpGet("depts")]
        public async Task<ActionResult<object>> DeptsAsync()
        {
            return Ok(
                (await dapper.QueryAsync(@"
                    select DEPT_ID, NAME from depts 
                    WHERE IS_DELETED = 0
                    AND EXISTS(SELECT 1 FROM MOLS WHERE IS_WORKING = 1 AND DDEPT_ID = DEPTS.DEPT_ID)
                    order by name;
                "))
                );
        }

        [HttpGet("posts")]
        public async Task<ActionResult<object>> PostsAsync()
        {
            return Ok(
                (await dapper.QueryAsync(@"
                    SELECT POST_ID, NAME FROM MOLS_POSTS ORDER BY NAME
                "))
                );
        }


        [HttpGet("{id}/hist")]
        public async Task<ActionResult<object>> HistAsync(int id)
        {
            return Ok(
                (await dapper.QueryAsync(@"
                    select m.*, p.name as POST_NAME, d.name as DEPT_NAME
                    from MOLS_POSTS_HIST m
                        inner join mols_posts p on p.post_id = m.post_id
                        inner join depts d on d.dept_id = m.dept_id
                    where m.mol_id = @id
                ", new { id}))
                );
        }
    }


    public class MolsFilter {
        public string Search { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 15;

        public DateTime? Date { get; set; }
        public string Sort { get; set; } = "NAME ASC";
        //public int Offset { get; set; } = 0;

        public int? DeptId { get; set; }
        public int? PostId { get; set; }
    }

}

