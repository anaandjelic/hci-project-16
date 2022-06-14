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
    public partial class TicketsPage : Page
    {
        private Frame ClientFrame;

        public TicketsPage(Frame clientFrame)
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            ClientFrame = clientFrame;

            GetTickets();
        }

        private void GetTickets()
        {
            List<Ticket> tickets = Database.GetUserTickets();
            List<TicketDisplay> ticketsDisplay = tickets.Select(x => new TicketDisplay(x)).ToList();

            TicketsTable.ItemsSource = ticketsDisplay;
            TicketsTable.Items.Refresh();
        }
    }
}
