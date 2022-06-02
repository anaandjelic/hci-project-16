using HCI_Project.utils;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project.clientPages
{
    /// <summary>
    /// Interaction logic for SearchContent.xaml
    /// </summary>
    public partial class SearchContent : Page
    {
        public SearchContent()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            foreach (TrainTimeTable x in Database.GetTimeTables())
            {
                DataTable.Items.Add(x);
            }
        }
    }
}
