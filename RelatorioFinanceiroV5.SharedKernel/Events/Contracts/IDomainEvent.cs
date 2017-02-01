using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.SharedKernel.Events.Contracts
{
    public interface IDomainEvent
    {
        DateTime DateOcurred { get; } 
    }
}
