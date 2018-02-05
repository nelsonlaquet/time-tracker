using System;
using System.Linq;
using System.Windows.Forms;

namespace TimeTracker.Forms
{
	public partial class SelectSheet : Form
	{
		private GoogleServices _services;

		public Sheet SelectedSheet { get; private set; }

		public SelectSheet(GoogleServices services)
		{
			InitializeComponent();
			_services = services;
		}

		private async void SelectSheet_Load(object sender, EventArgs e)
		{
			ProgressText.Visible = true;
			SheetsListBox.Items.Clear();
			var sheets = await _services.ListSheets();

			SheetsListBox.Items.AddRange(sheets.Select(sheet => new ListViewItem(new[] { sheet.Name, sheet.ModifiedTime.ToShortDateString() }) { Tag = sheet }).ToArray());
			foreach (ColumnHeader col in SheetsListBox.Columns)
				col.Width = -1;

			ProgressText.Visible = false;
		}

		private void SheetsListBox_DoubleClick(object sender, EventArgs e)
		{
			if (SheetsListBox.SelectedItems.Count != 1)
				return;

			SelectedSheet = (Sheet) SheetsListBox.SelectedItems[0].Tag;
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
