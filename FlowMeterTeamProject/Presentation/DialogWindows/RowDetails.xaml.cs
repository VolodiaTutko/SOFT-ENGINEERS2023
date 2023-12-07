using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlowMeterTeamProject.Presentation.DialogWindows
{
    /// <summary>
    /// Interaction logic for RowDetails.xaml
    /// </summary>
    public partial class RowDetails : Window
    {
        private readonly Dictionary<string, string> rowData;

        public RowDetails(Dictionary<string, string> rowData)
        {
            InitializeComponent();
            this.rowData = rowData;

            foreach (var pair in rowData)
            {
                detailsTextBlock.Text += $"{pair.Key}: {pair.Value}\n";
            }
        }
    }
}