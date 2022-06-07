using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace HCI_Project.commands.MultiKey
{
    public class MultiKeyBinding : InputBinding
	{
		[TypeConverter(typeof(MultiKeyGestureConverter))]
		public override InputGesture Gesture
		{
			get { return base.Gesture as MultiKeyGesture; }
			set
			{
				if (!(value is MultiKeyGesture))
				{
					throw new ArgumentException();
				}

				base.Gesture = value;
			}
		}
	}
}
