using HCI_Project.utils;
using HCI_Project.utils.display;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project.managerPages
{
    public partial class SoldTicketsByTrainLine : Page
    {
        public SoldTicketsByTrainLine()
        {
            InitializeComponent();
            TrainLineComboBox.ItemsSource = Database.GetTrainLinesStringWithID();
        }

        private int getTrainLineID()
        {
            string trainLineString = TrainLineComboBox.SelectedValue.ToString();
            string trainLineID_String = trainLineString.Split('>')[0];
            trainLineID_String = trainLineID_String.Substring(0, trainLineID_String.Length - 2);
            int trainLineID = Convert.ToInt32(trainLineID_String.Trim());
            return trainLineID;
        }

        private void TrainLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int trainLineID = getTrainLineID();
                var trainLine = Database.GetTrainLineByID(trainLineID);

                List<Ticket> tickets = Database.GetSoldTicketsByRide(trainLine);
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
