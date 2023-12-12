namespace FlowMeterTeamProject.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDataGridUpdater
    {
        event EventHandler DataGridUpdated;

        void UpdateDataGrid();
    }
}
