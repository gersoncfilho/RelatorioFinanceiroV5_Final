using RelatorioFinanceiroV5.SharedKernel.Events.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.SharedKernel
{
    public interface IHandler<T> : IDisposable where T : IDomainEvent
    {
        void Handle(Task args);
        IEnumerable<T> notify();
        bool hasNotifications();
        void Hanfle<T>(T args) where T : IDomainEvent;
    }
}
