using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapper;
using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Z.Dapper.Plus;
using System.Text;
using System.IO;
using System.Threading;

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
        public ActionResult Filter()
        {
            return Ok(new MolsFilter());
        }



        [HttpGet]
        public async Task<ActionResult<object>> Mols([FromQuery] MolsFilter filter = default(MolsFilter))
        {



            var res = await dapper.QueryAsync($@"
                ;with s as (
	                select 
                        s.MOL_ID, s.NAME, s.NAME_LOGON, s.EMAIL, s.PHONE_LOCAL, s.DATE_HIRE
                        , s.IS_WORKING
                        , d.name as DEPT_NAME
                        , p.name as POST_NAME
	                from mols s (nolock)
                        left join depts d on d.dept_id = s.ddept_id    
                        left join mols_posts p on p.post_id = s.post_id
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
                JsonConvert.SerializeObject(new
                {
                    offset = filter.Limit * (filter.Page - 1),
                    limit = filter.Limit,
                    total = res.Select(x => x.count).FirstOrDefault(),

                })
            );

            return Ok(res);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> MolAsync(int id)
        {

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
                    AND EXISTS(SELECT 1 FROM MOLS WHERE DDEPT_ID = DEPTS.DEPT_ID)
                    order by name;
                "))
                );
        }

        [HttpGet("posts")]
        public async Task<object> PostsAsync()
        {
            return await dapper.QueryAsync(@"
                    SELECT POST_ID, NAME FROM MOLS_POSTS ORDER BY NAME
                ");
        }


        [HttpGet("{id}/hist")]
        public async Task<object> HistAsync(int id)
        {
            return await dapper.QueryAsync(@"
                    select m.*, p.name as POST_NAME, d.name as DEPT_NAME
                    from MOLS_POSTS_HIST m
                        inner join mols_posts p on p.post_id = m.post_id
                        inner join depts d on d.dept_id = m.dept_id
                    where m.mol_id = @id
                ", new { id })
                ;
        }


        [HttpGet("{id}/audiences")]
        public async Task<object> AudiencesAsync(int id)
        {

            return await dapper.QueryAsync(@"
                select 
	                a.AUDIENCE_ID, a.NAME, a.DESCRIPTION
	                , cast(case when m.audience_id is null then 0 else 1 end as bit) as IS_CHECKED

                from sys_audiences a
	                left join sys_audience_mols m on m.audience_id = a.audience_id and m.mol_id = @id
                where a.is_deleted = 0
                order by description
                ", new { id })
                ;
        }

        [HttpPost("{id}/audiences")]
        public async Task<ActionResult<object>> SaveAudiencesAsync(int id, [FromBody] IEnumerable<MolAudience> audiences)
        {
            foreach (var a in audiences) a.D_ADD = DateTime.Today;

            await dapper.ExecuteAsync(@"delete sys_audience_mols where mol_id = @id", new { id });

            dapper
                .UseBulkOptions(x =>
                {
                    x.DestinationTableName = "SYS_AUDIENCE_MOLS";
                    x.ColumnPrimaryKeyNames = new List<string>() { "MOL_ID", "AUDIENCE_ID" };
                })
                .BulkInsert(audiences);

            return Ok();
        }


        [HttpPost("{id}")]
        public async Task<ActionResult<object>> Save(int id, [FromBody] MOL mol)
        {


            dapper
                .UseBulkOptions(x =>
                {
                    x.ColumnPrimaryKeyNames = new List<string>() { "MOL_ID" };
                    x.DestinationTableName = "MOLS";
                })
                .BulkUpdate(mol);

            await Task.Delay(1000);

            return Ok((await dapper.QueryAsync(@"select * from mols where mol_id = @id", new { id })).Single());


        }


        [HttpPost("insert")]
        public async Task<ActionResult<object>> InsertAsync([FromBody] MOL mol)
        {

            mol.MOL_ID = (new System.Random()).Next();

            dapper
                .UseBulkOptions(x =>
                {
                    x.ColumnPrimaryKeyNames = new List<string>() { "MOL_ID" };
                    x.DestinationTableName = "MOLS";
                })
                .BulkInsert(mol);


            await Task.Delay(4000);
            return Ok((await dapper.QueryAsync(@"select * from mols where mol_id = @mol_id", new { mol.MOL_ID })).Single());


        }


        [HttpGet("{id}/verify")]
        public async Task<ActionResult<object>> VerifyAsync(int id, string login) 
        {
            return Ok((await dapper.QueryAsync<bool>(@"
                select cast(case when exists(select 1 from mols where name_logon = @login and (mol_id != @id)) then 0 else 1 end as bit)  as checked
            ", new { id, login})).First());

        }


        [HttpGet("password")]
        public ActionResult<object> GetPasswordAsync(string password)
        {
            return Ok(dapper.Query<string>(@"
                SELECT convert(varchar(34), HASHBYTES('MD5',@password),1) as SQL_PASSWORD
            ", new { password }).First());

        }




        [HttpGet("connect")]
        public async Task ConnectAsync()
        {

            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var ws = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _logger.LogWarning("socket.open");
                await ws.SendAsync(Encoding.UTF8.GetBytes("Привет"), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);



                var buffer = new byte[2048 * 2];
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                _logger.LogWarning($"received {Encoding.UTF8.GetString(buffer)}");

                while (!ws.CloseStatus.HasValue)
                {
                    await ws.SendAsync(Encoding.UTF8.GetBytes("Привет"), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);
                    result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    _logger.LogWarning($"received {Encoding.UTF8.GetString(buffer)}");
                }

                _logger.LogWarning("socket.close");
                await ws.CloseAsync(ws.CloseStatus.Value, ws.CloseStatusDescription, CancellationToken.None);
            }

        }


    }






    public class MolsFilter
    {
        public string Search { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 15;

        public DateTime? Date { get; set; }
        public string Sort { get; set; } = "NAME ASC";
        //public int Offset { get; set; } = 0;

        public int? DeptId { get; set; }
        public int? PostId { get; set; }
    }



    public class MOL
    {
        public int MOL_ID { get; set; }
        public string SURNAME { get; set; }
        public string NAME1 { get; set; }
        public string NAME2 { get; set; }
        public string ADDRESS { get; set; }
        public string PASSPORT { get; set; }
        public int? DDEPT_ID { get; set; }
        public int? POST_ID { get; set; }

        public DateTime? BIRTHDAY { get; set; }
        public string PHOTO_TYPE { get; set; }
        public Byte[] PHOTO_IMAGE { get; set; }

        public string NAME_LOGON { get; set; }
        //public string SQL_PASSWORD { get; set; }
        public DateTime? D_PWD_EXPIRED { get; set; }

    }

    public class MolAudience
    {
        public int MOL_ID { get; set; }
        public int AUDIENCE_ID { get; set; }
        public DateTime D_ADD { get; set; }
    }

}

