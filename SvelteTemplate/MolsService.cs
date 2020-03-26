using Dapper;
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

            return Task.CompletedTask;
        }





        public async Task UnworkAsync(ActionUnworkRequest request) {
            await Task.Delay(10000);
            var mol = (await repo.QueryAsync(@"select * from mols where mol_id = @molId", request)).Single();
            await innerBus.SendAsync(request.Reply(new ActionUnworkResponse()));
            await innerBus.SendAsync(new ActionClientMessage { Text = $"Сотрудник {mol.NAME} уволен" });
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;

        }
    }




    public class ActionUnworkRequest: ServiceAction { public int MolId { get; set; } }
    public class ActionUnworkResponse: ServiceAction
    { public int MolId { get; set; } }



}
