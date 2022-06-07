using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HCI_Project.commands
{
    public static class RoutedCommands
    {
        public static readonly ICommand FormatHello;
        public static readonly ICommand FormatSearchTrainLine;
        public static readonly ICommand FormatSearchTrainTable;
        public static readonly ICommand FormatLogOut;
        public static readonly ICommand FormatHistoryTickets;
        public static readonly ICommand FormatHistoryReservations;

        static RoutedCommands()
        {
            var inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.M, Key.B }, ModifierKeys.Control, "Ctrl+M, B"));
            FormatHello = new RoutedUICommand("Format Hello", "FormatHello", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.F, Key.L }, ModifierKeys.Control, "Ctrl+F, L"));
            FormatSearchTrainLine = new RoutedUICommand("Format SearchTrainLine", "FormatSearchTrainLine", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.F, Key.T }, ModifierKeys.Control, "Ctrl+F, T"));
            FormatSearchTrainTable = new RoutedUICommand("Format SearchTrainTable", "FormatSearchTrainTable", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.L, Key.O }, ModifierKeys.Control, "Ctrl+L, O"));
            FormatLogOut = new RoutedUICommand("Format LogOut", "FormatLogOut", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.H, Key.T }, ModifierKeys.Control, "Ctrl+H, T"));
            FormatHistoryTickets = new RoutedUICommand("Format HistoryTickets", "FormatHistoryTickets", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.H, Key.R }, ModifierKeys.Control, "Ctrl+H, R"));
            FormatHistoryReservations = new RoutedUICommand("Format HistoryReservations", "FormatHistoryReservations", typeof(RoutedCommands), inputGestures);


        }
    }
}