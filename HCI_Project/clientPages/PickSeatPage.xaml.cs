using System.Windows.Controls;
using HCI_Project.utils.display;

namespace HCI_Project.clientPages
{
    public partial class PickSeatPage : Page
    {
        TimeTableDisplay Selection;

        public PickSeatPage(TimeTableDisplay selection)
        {
            InitializeComponent();
            Selection = selection;
        }
    }
}
