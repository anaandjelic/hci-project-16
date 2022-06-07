using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HCI_Project.commands
{
    public class RoutedCommands
    {
        public readonly RoutedUICommand Search = new RoutedUICommand(
            "Serach Command",
            "SearchCommand",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S,ModifierKeys.Shift)
            });
    }
}
