using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetriNetApp
{
    public class DataController
    {
        public DataTable table { get; set; }

        public DataController()
        { table = new DataTable("table");
            table.Columns.Add("time");
            table.Columns.Add("transition");
        }

        public void time_tick(int time, int transition)
        {
            DataRow dr = table.NewRow();
            dr["time"] = time;
            dr["transition"] = transition;
            table.Rows.Add(dr);
        }

    }
}
