using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureAndHumidityLogger.Core.Interfaces.Common
{
    public interface ISoftDelete
    {
         DateTime? DeletedAt { get; set; }
    }
}
