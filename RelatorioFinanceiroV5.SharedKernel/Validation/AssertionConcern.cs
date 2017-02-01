using RelatorioFinanceiroV5.SharedKernel.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.SharedKernel.Validation
{
    public static class AssertionConcern
    {
        public static bool IsSatisfiedBy(DomainNotification[] validations)
        {
            var notificationssNotNull = validations.Where(validation => validation != null);
            NotifyAll(notificationssNotNull);

            return notificationssNotNull.Count().Equals(0);
        }

        private static void NotifyAll(IEnumerable<DomainNotification> notifications)
        {
            //notifications.ToList().ForEach(validation => { DomainEvent.Raise(validation); });
        }
    }
}
