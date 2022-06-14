using HCI_Project.utils;
using HCI_Project.utils.display;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project.clientPages
{
    public partial class SoldTicketsByMonthPage : Page
    {
        public SoldTicketsByMonthPage()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));

            DateTime now = DateTime.Now;
            Year.Text = now.Year.ToString();
            Month.Text = now.Month.ToString();
        }

        private void GetTicketsByMonth(object sender, RoutedEventArgs e)
        {
            try
            {
                int year = Int16.Parse(Year.Text);
                int month = Int16.Parse(Month.Text);

                if (month < 1 || month > 12 || year < 1)
                {
                    MessageBox.Show("Invalid year or month!");
                    return;
                }

                List<Ticket> tickets = Database.GetSoldTicketsByMonth(Int16.Parse(Year.Text), Int16.Parse(Month.Text));
                List<SoldTicketDisplay> ticketsDisplay = tickets.Select(x => new SoldTicketDisplay(x)).ToList();

                TicketsTable.ItemsSource = ticketsDisplay;
                TicketsTable.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid year or month!");
                return;
            }
        }
    }
}
