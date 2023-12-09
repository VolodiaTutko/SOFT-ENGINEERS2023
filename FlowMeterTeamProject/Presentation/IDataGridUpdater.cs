using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterTeamProject.Presentation
{
    public  interface IDataGridUpdater
    {
        event EventHandler DataGridUpdated;
        void UpdateDataGrid();
    }
}
