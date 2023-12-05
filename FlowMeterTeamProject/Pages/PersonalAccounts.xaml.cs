﻿using FlowMeterTeamProject.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using FlowMeterTeamProject.Utils.DataGrid;
using FlowMeterTeamProject.Data.DataMock;
using FlowMeterTeamProject.PersonalAccountDialogWindow;

namespace FlowMeterTeamProject.Pages
{
    /// <summary>
    /// Interaction logic for PersonalAccounts.xaml
    /// </summary>
    public partial class PersonalAccounts : Page
    {
        public PersonalAccounts()
        {
            InitializeComponent();

            FillDataGrid();

            //Account[] accountData = GenerateRandomAccounts(20);


            //using (var context = new AppDbContext())
            //{
            //    foreach (var data in accountData)
            //    {
            //        context.accounts.Add(data);
            //    }
            //    context.SaveChanges();
            //}

        }

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            XlsxExporter.ExportToExcelButton_Click(sender, e, dataGrid);
        }

        private void ExportToPdfButton_Click(object sender, RoutedEventArgs e)
        {
            PdfExporter.ExportToPdfButton_Click(sender, e, dataGrid);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridSearch.PerformSearch(dataGrid, searchBox);
        }


        public void FillDataGrid()
        {
            using (var context = new AppDbContext())
            {
                List<Account> accounts = context.accounts.ToList();

                DataTable dt = new DataTable("Account");
                dt.Columns.Add("№", typeof(int));
                dt.Columns.Add("PersonalAccount", typeof(string));
                dt.Columns.Add("HotWater", typeof(decimal));
                dt.Columns.Add("ColdWater", typeof(decimal));
                dt.Columns.Add("Heating", typeof(decimal));
                dt.Columns.Add("Electricity", typeof(decimal));
                dt.Columns.Add("PublicService", typeof(decimal));

                for (int i = 0; i < accounts.Count; i++)
                {
                    dt.Rows.Add(
                        i + 1,
                        accounts[i].PersonalAccount,
                        accounts[i].HotWater,
                        accounts[i].ColdWater,
                        accounts[i].Heating,
                        accounts[i].Electricity,
                        accounts[i].PublicService
                    );
                }

                dt.Columns["№"].SetOrdinal(0);

                dataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void b1_Click(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
