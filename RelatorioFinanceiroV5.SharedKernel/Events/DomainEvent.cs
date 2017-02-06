using RelatorioFinanceiroV5.SharedKernel.Events;
using RelatorioFinanceiroV5.SharedKernel.Events.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.SharedKernel.Events
{
    public class DomainEvent
    {
        public static IContainer Container { get; set; }
        public static void Raise<T>(T args) where T : IDomainEvent
        {
            try
            {
                if (Container != null)
                {
                    var obj = Container.GetService(typeof(IHandler<T>));
                    ((IHandler<T>)obj).Handle(args);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
