using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace SvelteTemplate
{

    public class ServiceAction {
        public Guid Id { get; set; }
    }


    public static class ServiceActionExtensions {
        public static T Reply<T>(this ServiceAction request, T response) where T : ServiceAction {
            response.Id = request.Id;
            return response;
        }
    }


    public class ActionClientMessage : ServiceAction {
        public string Text { get; set; }
    }


    public class InnerBus: IDisposable
    {
        private Subject<ServiceAction> subject;

        public InnerBus()
        {
            subject = new Subject<ServiceAction>();
        }

        public void Dispose()
        {
            subject.Dispose();
        }

        public async Task SendAsync(ServiceAction action) {
            subject.OnNext(action);
            await Task.CompletedTask;
        }

        public IDisposable Subscribe<T>(Action<T> action) where T:ServiceAction {
            return subject.OfType<T>().Subscribe(action);
        }



    }
}
