
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExpenseTracker
{
    public class Expense
    {
        public string Name, Category;
        public decimal Amount;
        public DateTime Date;
    }

    public class ExpenseForm : Form
    {
        TextBox nameBox = new TextBox();
        TextBox amountBox = new TextBox();
        ComboBox categoryBox = new ComboBox();
        DateTimePicker dateBox = new DateTimePicker();

        public Expense Result;

        public ExpenseForm(Expense old = null)
        {
            Text = old == null ? "Add Expense" : "Edit Expense";
            Size = new Size(450, 360);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Font = new Font("Segoe UI", 10);

            AddLabel("Expense Name:", 35, 35);
            nameBox.SetBounds(155, 32, 240, 30);

            AddLabel("Category:", 35, 90);
            categoryBox.SetBounds(155, 87, 240, 30);
            categoryBox.DropDownStyle =
                ComboBoxStyle.DropDownList;

            categoryBox.Items.AddRange(new object[]
            {
                "Food", "Travel", "Shopping",
                "Education", "Other"
            });
            categoryBox.SelectedIndex = 0;

            AddLabel("Amount (Rs.):", 35, 145);
            amountBox.SetBounds(155, 142, 240, 30);

            AddLabel("Date:", 35, 200);
            dateBox.SetBounds(155, 197, 240, 30);

            Button save = new Button();
            save.Text = "Save";
            save.SetBounds(155, 260, 105, 35);
            save.Click += SaveExpense;

            Button cancel = new Button();
            cancel.Text = "Cancel";
            cancel.SetBounds(280, 260, 105, 35);
            cancel.Click += (s, e) => Close();

            Controls.AddRange(new Control[]
            {
                nameBox, categoryBox, amountBox,
                dateBox, save, cancel
            });

            if (old != null)
            {
                nameBox.Text = old.Name;
                categoryBox.SelectedItem = old.Category;
                amountBox.Text = old.Amount.ToString();
                dateBox.Value = old.Date;
            }

            AcceptButton = save;
            CancelButton = cancel;
        }

        void AddLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.SetBounds(x, y, 115, 30);
            Controls.Add(label);
        }

        void SaveExpense(object sender, EventArgs e)
        {
            decimal amount;

            if (nameBox.Text.Trim() == "" ||
                !decimal.TryParse(amountBox.Text, out amount) ||
                amount <= 0)
            {
                MessageBox.Show("Enter valid details!");
                return;
            }

            Result = new Expense()
            {
                Name = nameBox.Text.Trim(),
                Category = categoryBox.Text,
                Amount = amount,
                Date = dateBox.Value
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
