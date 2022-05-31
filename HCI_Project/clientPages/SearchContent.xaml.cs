using HCI_Project.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
