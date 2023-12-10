using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;

namespace BLL.Utils.DataGrid
{
    public static class DataGridSearch
    {
        public static void PerformSearch(System.Windows.Controls.DataGrid dataGrid, TextBox searchBox)
        {
            string searchText = searchBox.Text.ToLower();

            foreach (var item in dataGrid.Items)
            {
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);

                if (row != null)
                {
                    bool rowContainsSearchText = false;

                    foreach (DataGridColumn column in dataGrid.Columns)
                    {
                        var cellContent = column.GetCellContent(item);
                        if (cellContent is TextBlock textBlock)
                        {
                            string cellText = textBlock.Text;

                            int index = cellText.IndexOf(searchText, StringComparison.OrdinalIgnoreCase);
                            if (index >= 0)
                            {
                                textBlock.Inlines.Clear();
                                textBlock.Inlines.Add(new Run(cellText.Substring(0, index)));
                                textBlock.Inlines.Add(new Run(cellText.Substring(index, searchText.Length))
                                {
                                    Background = Brushes.Orange,
                                    FontWeight = FontWeights.Bold
                                });
                                textBlock.Inlines.Add(new Run(cellText.Substring(index + searchText.Length)));
                                rowContainsSearchText = true;
                            }
                            else
                            {
                                textBlock.Inlines.Clear();
                                textBlock.Inlines.Add(cellText);
                            }
                        }
                    }

                    row.Visibility = rowContainsSearchText ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }
    }
}
