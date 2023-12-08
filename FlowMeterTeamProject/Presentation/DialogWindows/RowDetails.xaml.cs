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
        private readonly List<Dictionary<string, string>> rowsData;

        public RowDetails(List<Dictionary<string, string>> rowsData)
        {
            InitializeComponent();
            this.rowsData = rowsData;

            foreach (var rowData in rowsData)
            {
                foreach (var pair in rowData)
                {
                    detailsTextBlock.Text += $"{pair.Key}: {pair.Value}\n";
                }

                detailsTextBlock.Text += "------------------------\n";
            }
        }
    }
}