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


        private void SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            TicketDisplay selection = (TicketDisplay)TicketsTable.SelectedItem;
            if (selection == null || selection.OriginalTicket.Purchased)
            {
                ButtonCancel.IsEnabled = false;
                ButtonPurchase.IsEnabled = false;
            } 
            else
            {
                ButtonCancel.IsEnabled = true;
                ButtonPurchase.IsEnabled = true;
            }
        }

        private void CancelTicket(object sender, RoutedEventArgs e)
        {
            TicketDisplay selection = (TicketDisplay)TicketsTable.SelectedItem;
            if (MessageBox.Show("Are you sure you want to cancel ticket? This action cannot be undone.", "Confirm cancellation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                selection.OriginalTicket.Deleted = true;
                GetTickets();
            }
        }

        private void PurchaseTicket(object sender, RoutedEventArgs e)
        {
            TicketDisplay selection = (TicketDisplay)TicketsTable.SelectedItem;
            if (MessageBox.Show("Purchasing a reserved ticket cannot be refunded or undone. Do you confirm that you want to purchase the ticket?", "Confirm purchase", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                selection.OriginalTicket.Purchased = true;
                GetTickets();
            }
        }
    }
}
