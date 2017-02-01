using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.SharedKernel.Events
{
   public class DomainNotification
    {
        public string Key { get; private set; }
        public string Value { get; private set; }
        public DateTime DateOcurred { get; private set; }

        public DomainNotification(string key, string value)
        {
            this.Key = key;
            this.Value = value;
            this.DateOcurred = DateTime.Now;
        }
    }
}
