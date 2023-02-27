using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment4_partylist
{
    /// <summary>
    /// Main form class for displaying output and recieving input
    /// </summary>
    public partial class MainForm : Form
    {
        Party party;
        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
        }
        //Initializes the GUI making sure to clear text boxes, lables and listboxes
        private void InitializeGUI()
        {
            //Input
            txtMaxNum.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtAmount.Text = string.Empty;

            //Output
            lblNumGuest.Text = string.Empty;
            lblTotalCost.Text = "0.0";
            lstAllGuests.Items.Clear();
            grpAddGuests.Enabled = false; //Disabled
            grpNewParty.Enabled = true; //Enabled
        }
        //Creates the max number for the party array
        private bool CreateParty()
        {
            int maxNumber = 0;
            bool ok = true;

            if (int.TryParse(txtMaxNum.Text, out maxNumber) && (maxNumber > 0))
            {
                party = new Party(maxNumber);
                MessageBox.Show($"Party list with space for {maxNumber} guests created!");
            }
            else
            {
                MessageBox.Show("Invalid total number, please try again!");
                    ok = false;
            }
            return ok;
        }

        //Validates the amount given
        private bool ReadCostPerPerson()
        {
            double amount = 0.0;
            bool ok = true;

                if(double.TryParse (txtAmount.Text, out amount) && (amount != 0.0))
            {
                party.CostPerCapita = amount;
            }
                else
            {
                MessageBox.Show("Invalid amount, please try again!");
                ok = false;
            }
            return ok;
        }

        //Updates the GUI after the button is clicked
        private void UpdateGUI()
        {
            lstAllGuests.Items.Clear();
            string[] guestList = party.GetGuestList();
            if (guestList != null)
            {
                for (int i = 0; i < guestList.Length; i++)
                {
                    string str = $"{i + 1,4} {guestList[i],-20}";
                    lstAllGuests.Items.Add(str);
                }
            }
            else
                return;

            double totalCost = party.CalcTotalCost();
            lblTotalCost.Text = totalCost.ToString("0.00");
            lblNumGuest.Text = party.Count.ToString();
        }

        //Check so name boxes arent empty
        private bool ValidateText (string text)
        {
            text = text.Trim();

            if(string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Please provide both a first name and a last name");
                    return false;
            }
            return true;
        }

        //Checks if names are given and trims them
        private bool TrimNames()
        {
            if ((!ValidateText(txtFirstName.Text)) ||
                (!ValidateText(txtLastName.Text))
                )
                return false;

            txtFirstName.Text = txtFirstName.Text.Trim();
            txtLastName.Text = txtLastName.Text.Trim();
            return true;
        }

        //Reads if an item is selected in the list box 
        private int IsListBoxItemSelectd()
        {
            int i = lstAllGuests.SelectedIndex;

            if (lstAllGuests.SelectedIndex < 0)
            {
                MessageBox.Show("Select an item in the list");
                return -1;
            }
            return i;
        }

        //Creates the party
        private void btnCreate_Click(object sender, EventArgs e)
        {
            bool maxNumOK = CreateParty();
            if (!maxNumOK)
                return;

            bool amountOK = ReadCostPerPerson();

            if(maxNumOK && amountOK) //Enables the add guests grp box if input is accepted in the New Party group box
            {
                grpAddGuests.Enabled = true;
                UpdateGUI();
            }
        }

        //Adds a new entry to the list box
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (TrimNames())
            {
                bool ok = party.AddNewGuest(txtFirstName.Text, txtLastName.Text);
                if (!ok)
                    MessageBox.Show("List is full! New guests not added!");
                else
                    UpdateGUI();
            }
        }

        //Deletes the position in the list box
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int i = IsListBoxItemSelectd();
            if (i < 0)
                return;

            party.DeleteAt(i);
            UpdateGUI();
        }

        //Changes the name in the list box position
        private void btnChange_Click(object sender, EventArgs e)
        {
            int i = IsListBoxItemSelectd();
            if (i < 0)
                return;

            if(TrimNames())
            {
                party.ChangeAt(i, txtFirstName.Text, txtLastName.Text);
                UpdateGUI();
            }
        }
    }
}
