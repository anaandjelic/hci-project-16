using HCI_Project.utils;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace HCI_Project.clientPages
{
    public partial class SearchPage : Page
    {
        readonly ObservableCollection<TrainTimeTable> ttbs = new ObservableCollection<TrainTimeTable>(Database.GetTimeTables());
        public SearchPage()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));

            DataTable.ItemsSource = new ObservableCollection<TTT_DTO>(Database.GetTTT_DTOS());
        }
        private bool DoesItContainStations(string from, string to, TrainTimeTable ttb)
        {
            if (ttb.TrainLine.DoesItContainStationName(from) && ttb.TrainLine.DoesItContainStationName(to))
                return true;
            return false;
        }

        private bool IsItCorrectDate(TrainTimeTable ttb)
        {
            if (ttb.DepartureTime.Year.Equals(DP.SelectedDate.Value.Year) && ttb.DepartureTime.Month.Equals(DP.SelectedDate.Value.Month) && ttb.DepartureTime.Day.Equals(DP.SelectedDate.Value.Day))
                return true;
            return false;
        }

        private bool CorrectOrder(TTT_DTO dto,string from,string to)
        {
            int start = -1;
            int end = int.MaxValue;

            string[] stationOrder = dto.TrainLine.Split('-');
            for(int i=0;i<stationOrder.Length;i++)
            {
                if (stationOrder[i].Trim().Equals(from))
                {
                    if (i == stationOrder.Length - 1)
                        return false;
                    else
                        start = i;
                }
                if (stationOrder[i].Trim().Equals(to))
                {
                    if (i == 0)
                        return false;
                    else
                        end = i;

                }
            }
            if(start>end)
                return false;
            return true;
        }


        private void SearchClick(object sender, RoutedEventArgs e)
        {
            ObservableCollection<TTT_DTO> searchRes = new ObservableCollection<TTT_DTO>();

            string from = From.Text;
            string to = To.Text;

            foreach (TrainTimeTable ttb in ttbs)
            {
                if (DoesItContainStations(from,to,ttb) && IsItCorrectDate(ttb))
                {
                    TTT_DTO dto = new TTT_DTO(ttb);
                    if(CorrectOrder(dto,from,to))
                        searchRes.Add(dto);
                }
            }
            DataTable.ItemsSource = searchRes;
            DataTable.Items.Refresh();
        }
    }
}
