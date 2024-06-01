using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Quan_Coffee
{
    public partial class frmBillList : SampleAddCf
    {
        public frmBillList()
        {
            InitializeComponent();
        }

        public int MainID = 0;

        private void frmBillList_Load(object sender, EventArgs e)
        {
            LoadDate();
        }

        private void LoadDate()
        {
            string qry = @"select MainID, TableName, WaiterName, orderType, status, total from tblMain
                            where status <> 'Pending' ";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvtable);
            lb.Items.Add(dgvWaiter);
            lb.Items.Add(dgvType);
            lb.Items.Add(dgvStatus);
            lb.Items.Add(dgvTotal);

            MainClass.LoadData(qry, dataGridView, lb);
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void dataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                MainID = Convert.ToInt32(dataGridView.CurrentRow.Cells["dgvid"].Value);
                this.Close();
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
