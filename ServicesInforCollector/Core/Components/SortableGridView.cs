using System.Linq;
using System.Windows.Forms;

namespace ServicesInforCollector.Core.Components
{
    public partial class SortableGridView : DataGridView
    {
        public SortableGridView()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        internal static SortOrder GetNewSortDirection(DataGridViewColumn lastSortedColumn, DataGridViewColumn newColumn)
        {
            var newSortDirection = SortOrder.Ascending;
            if (lastSortedColumn == null) return newSortDirection;
            if (lastSortedColumn == newColumn)
            {
                if (newColumn.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                    newSortDirection = SortOrder.Descending;
            }
            lastSortedColumn.HeaderCell.SortGlyphDirection = SortOrder.None;

            return newSortDirection;
        }

        internal DataGridViewColumn FindLastSortedColumn()
        {
            return
                Columns.Cast<DataGridViewColumn>()
                    .FirstOrDefault(column => column.HeaderCell.SortGlyphDirection != SortOrder.None);
        }

        internal DataGridViewColumn FindColumnByPropertyName(string propertyName)
        {
            foreach (
                DataGridViewColumn column in
                    Columns.Cast<DataGridViewColumn>().Where(column => column.DataPropertyName == propertyName))
            {
                return column;
            }

            return Columns[0];
        }
    }
}