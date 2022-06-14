using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HCI_Project.commands
{
    public static class RoutedCommands
    {
        //clients
        public static readonly ICommand FormatHello;
        public static readonly ICommand FormatSearchTrainLine;
        public static readonly ICommand FormatSearchTrainTable;
        public static readonly ICommand FormatLogOut;
        public static readonly ICommand FormatHistoryTickets;
        public static readonly ICommand FormatHistoryReservations;
        public static readonly ICommand FormatPurchaseTicket;
        public static readonly ICommand FormatReserveTicket;

        //managers
        public static readonly ICommand FormatCreateTrain;
        public static readonly ICommand FormatCreateTrainLine;
        public static readonly ICommand FormatCreateTrainTable;

        public static readonly ICommand FormatEditTrain;
        public static readonly ICommand FormatEditTrainLine;
        public static readonly ICommand FormatEditTrainTable;

        public static readonly ICommand FormatMonthlyReports;
        public static readonly ICommand FormatPerTableReports;

        //help
        public static readonly ICommand FormatManagerHelp;
        public static readonly ICommand FormatClientHelp;

        //full screen
        public static readonly ICommand FormatFullScreen;

        //tutorial
        public static readonly ICommand FormatCancelTutorial;

        public static readonly ICommand FormatTutorial; // serach train lines tutorial
        public static readonly ICommand FormatCreateTrainTutorial;
        public static readonly ICommand FormatCreateTrainLineTutorial;
        public static readonly ICommand FormatCreateTrainTableTutorial;

        //public static readonly ICommand FormatLogOut;

        static RoutedCommands()
        {
            // clients
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

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.P }, ModifierKeys.Control, "Ctrl+P"));
            FormatPurchaseTicket = new RoutedUICommand("Format PurchaseTicket", "FormatPurchaseTicket", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.R }, ModifierKeys.Control, "Ctrl+R"));
            FormatReserveTicket = new RoutedUICommand("Format ReserveTicket", "FormatReserveTicket", typeof(RoutedCommands), inputGestures);

            //managers
            //create
            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.Q }, ModifierKeys.Control | ModifierKeys.Shift, "Ctrl+Shift+Q"));
            FormatCreateTrain = new RoutedUICommand("Format CreateTrain", "FormatCreateTrain", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.W }, ModifierKeys.Control | ModifierKeys.Shift, "Ctrl+Shift+W"));
            FormatCreateTrainLine = new RoutedUICommand("Format CreateTrainLine", "FormatCreateTrainLine", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.E }, ModifierKeys.Control | ModifierKeys.Shift, "Ctrl+Shift+E"));
            FormatCreateTrainTable = new RoutedUICommand("Format CreateTrainTable", "FormatCreateTrainTable", typeof(RoutedCommands), inputGestures);

            //edit
            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.F, Key.Q }, ModifierKeys.Control, "Ctrl+F+Q"));
            FormatEditTrain = new RoutedUICommand("Format EditTrain", "FormatEditTrain", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.F, Key.W }, ModifierKeys.Control, "Ctrl+F+W"));
            FormatEditTrainLine = new RoutedUICommand("Format EditTrainLine", "FormatEditTrainLine", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.F, Key.E}, ModifierKeys.Control, "Ctrl+F+E"));
            FormatEditTrainTable = new RoutedUICommand("Format EditTrainTable", "FormatEditTrainTable", typeof(RoutedCommands), inputGestures);

            //reports
            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.R, Key.M }, ModifierKeys.Control, "Ctrl+R, M"));
            FormatMonthlyReports = new RoutedUICommand("Format MonthlyReports", "FormatMonthlyReports", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.R, Key.T }, ModifierKeys.Control, "Ctrl+R, T"));
            FormatPerTableReports = new RoutedUICommand("Format PerTableReports", "FormatPerTableReports", typeof(RoutedCommands), inputGestures);

            // help
            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.F1 } , ModifierKeys.Control, "Ctrl+F1"));
            FormatManagerHelp = new RoutedUICommand("Format Help", "Help", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.F1 }, ModifierKeys.Control, "Ctrl+F1"));
            FormatClientHelp = new RoutedUICommand("Format ClientHelp", "ClientHelp", typeof(RoutedCommands), inputGestures);

            //fullscreen
            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.F }, ModifierKeys.Control, "Ctrl+F"));
            FormatFullScreen = new RoutedUICommand("Format FullScreen", "FormatFullScreen", typeof(RoutedCommands), inputGestures);

            //tutorial
            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.T, Key.U }, ModifierKeys.Control, "Ctrl+T, U"));
            FormatTutorial = new RoutedUICommand("Format Tutorial", "FormatTutorial", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.T, Key.C }, ModifierKeys.Control, "Ctrl+T, C"));
            FormatCreateTrainTutorial = new RoutedUICommand("Format CreateTrainTutorial", "FormatCreateTrainTutorial", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.T, Key.L }, ModifierKeys.Control, "Ctrl+T, L"));
            FormatCreateTrainLineTutorial = new RoutedUICommand("Format CreateTrainLineTutorial", "FormatCreateTrainLineTutorial", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.T, Key.E }, ModifierKeys.Control, "Ctrl+T, E"));
            FormatCreateTrainTableTutorial = new RoutedUICommand("Format CreateTrainTableTutorial", "FormatCreateTrainTableTutorial", typeof(RoutedCommands), inputGestures);

            inputGestures = new InputGestureCollection();
            inputGestures.Add(new MultiKey.MultiKeyGesture(new Key[] { Key.T, Key.X }, ModifierKeys.Control, "Ctrl+T, X"));
            FormatCancelTutorial = new RoutedUICommand("Format CancelTutorial", "FormatCancelTutorial", typeof(RoutedCommands), inputGestures);

        }
    }
}