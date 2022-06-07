using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HCI_Project.commands
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand Search = new RoutedUICommand(
            "Serach Command",
            "SearchCommand",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S, ModifierKeys.Control)
            });
    }
}
