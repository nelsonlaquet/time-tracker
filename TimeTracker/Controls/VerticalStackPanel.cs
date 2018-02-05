using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace TimeTracker.Controls
{
	public class VerticalStackPanel : Panel
	{
		private int _space = 10;
		private Color? _stripeColor;

		public Color? StripeColor
		{
			get { return _stripeColor; }
			set
			{
				_stripeColor = value;
				LayoutControls();
			}
		}

		public int Space
		{
			get { return _space; }
			set
			{
				_space = value;
				LayoutControls();
			}
		}

		public int InnerHeight => Controls.Cast<Control>().Aggregate(Space, (agg, c) => agg + c.Height + Space);

		public VerticalStackPanel()
		{
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			e.Control.Width = 10;
			base.OnControlAdded(e);
			LayoutControls();
		}

		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved(e);
			LayoutControls();
		}

		private void LayoutControls()
		{
			var height = _space;

			foreach (Control c in Controls)
				height += c.Height + _space;

			AutoScrollMinSize = new Size(0, height);
			var top = AutoScrollPosition.Y + _space;
			var width = ClientSize.Width - (_space * 2);
			var index = 0;

			foreach (Control c in Controls)
			{
				c.SetBounds(_space, top, width, c.Height);
				c.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
				top += c.Height + _space;

				if (StripeColor != null && ++index % 2 == 0)
					c.BackColor = StripeColor.Value;
				else
					c.BackColor = SystemColors.Window;
			}
		}
	}
}
