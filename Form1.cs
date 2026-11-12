using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExpenseTracker
{
    public partial class Form1 : Form
    {
        List<Expense> expenses = new List<Expense>();
        DataGridView grid = new DataGridView();
        Label total = new Label();

        public Form1()
        {
            InitializeComponent();

            Text = "Pocket Expense Manager";
            Size = new Size(800, 500);
            StartPosition = FormStartPosition.CenterScreen;
            Controls.Clear();

            Panel top = new Panel();
            top.Dock = DockStyle.Top;
            top.Height = 65;
            top.BackColor = Color.LightSteelBlue;

            Button add = new Button();
            add.Text = "Add Expense";
            add.Location = new Point(20, 15);
            add.Size = new Size(110, 35);
            add.Click += AddExpense;

            Button edit = new Button();
            edit.Text = "Edit Expense";
            edit.Location = new Point(140, 15);
            edit.Size = new Size(110, 35);
            edit.Click += EditExpense;

            total.Text = "Total: Rs. 0";
            total.Location = new Point(550, 23);
            total.AutoSize = true;
            total.Font = new Font("Arial", 12, FontStyle.Bold);

            top.Controls.AddRange(new Control[] { add, edit, total });

            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            grid.Columns.Add("Name", "Expense Name");
            grid.Columns.Add("Category", "Category");
            grid.Columns.Add("Amount", "Amount");
            grid.Columns.Add("Date", "Date");

            Controls.Add(grid);
            Controls.Add(top);
        }

        void AddExpense(object sender, EventArgs e)
        {
            ExpenseForm f = new ExpenseForm();

            if (f.ShowDialog() == DialogResult.OK)
            {
                expenses.Add(f.Result);
                RefreshData();
            }
        }

        void EditExpense(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an expense!");
                return;
            }

            int index = grid.SelectedRows[0].Index;

            ExpenseForm f = new ExpenseForm(expenses[index]);

            if (f.ShowDialog() == DialogResult.OK)
            {
                expenses[index] = f.Result;
                RefreshData();
            }
        }

        void RefreshData()
        {
            grid.Rows.Clear();
            decimal sum = 0;

            foreach (Expense x in expenses)
            {
                grid.Rows.Add(x.Name, x.Category,
                    x.Amount, x.Date.ToShortDateString());

                sum += x.Amount;
            }

            total.Text = "Total: Rs. " + sum;
        }
    }
}
