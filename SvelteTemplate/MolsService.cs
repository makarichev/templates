﻿using Dapper;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using static svelte.Controllers.MolsController;

namespace SvelteTemplate
{



    public class MolsService : IHostedService
    {
        private readonly InnerBus innerBus;
        private readonly SqlConnection repo;

        public MolsService(InnerBus innerBus, SqlConnection repo)
        {
            this.innerBus = innerBus;
            this.repo = repo;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {


            innerBus.Subscribe<ActionUnworkRequest>(async x => await UnworkAsync(x) );
            innerBus.Subscribe<ActionNewPost>(async x => await NewPostAsync(x));

            return Task.CompletedTask;
        }





        public async Task UnworkAsync(ActionUnworkRequest request) {
            await Task.Delay(4000);
            var mol = (await repo.QueryAsync(@"select * from mols where mol_id = @molId", request)).Single();
            await innerBus.SendAsync(request.Reply(new ActionUnworkResponse()));
            await innerBus.SendAsync(new ActionClientMessage { Text = $"Сотрудник {mol.NAME} уволен" });
        }

        public async Task NewPostAsync(ActionNewPost request)
        {

            await repo.ExecuteAsync(@"
                    update mols set
                        post_id = @postId,
                        ddept_id = @deptId
                    where mol_id = @molId;

                    insert into MOLS_POSTS_HIST(MOL_ID, D_FROM, POST_ID, DEPT_ID)
                    select @molId, @dFrom, @PostId, @deptId;
                ", request);


            await innerBus.SendAsync(new ActionClientMessage { Text = $"Запрос принят" });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;

        }
    }




    public class ActionUnworkRequest: ServiceAction { public int MolId { get; set; } }
    public class ActionUnworkResponse: ServiceAction
    { public int MolId { get; set; } }

    public class ActionNewPost : ServiceAction
    {
        public int MolId { get; set; }
        public int PostId { get; set; }
        public int DeptId { get; set; }
        public DateTime DFrom { get; set; }
    }


}
