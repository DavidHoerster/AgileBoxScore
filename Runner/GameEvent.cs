using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    public class GameEvent
    {
        public String Id { get; set; }
        public Int32 Inning { get; set; }
        public Boolean IsTop { get; set; }
        public Int32 Hit { get; set; }
        public Int32 Run { get; set; }
        public Int32 Error { get; set; }
        public Int32 Out { get; set; }

    }
}
