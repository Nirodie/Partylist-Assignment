using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_partylist
{
    /// <summary>
    /// Party server task to handle information given by the MainForm class
    /// </summary>
    class Party
    {
        private double costPerCapita; //user gives value to this 
        private string[] guestList; //Guest list array declared

        public Party (int maxNumOfGuests) //Constructor of this class
        {
            guestList = new string[maxNumOfGuests]; //Array created
        }
        public double CostPerCapita //Property for costPerCapita
        {
            get { return costPerCapita; }
            set
            {
                if (value >= 0)
                    costPerCapita = value;
            }
        }

        //Looks for an empty position inside the array
        private int FindVacantPos()
        {
            int vacantPos = -1;

            for (int i = 0; i < guestList.Length; i++)
            {
                if (string.IsNullOrEmpty (guestList[i]))
                {
                    vacantPos = i;
                    break; //When empty position is found the loop breaks
                }
            }
            return vacantPos;
        }

        //converts the last name to UPPERCASE and adds the the first name to it as follows "WESTIN, Hannes"
        private string FullName(string firstName, string lastName)
        {
            return lastName.ToUpper() + ", " + firstName; 
        }

        //Adds a new guest to the array
        public bool AddNewGuest (string firstName, string lastName)
        {
            bool ok = true;
            int vacantPos = FindVacantPos();
            if (vacantPos != -1)
            {
                guestList[vacantPos] = FullName(firstName, lastName);
            }
            else
                ok = false;

            return ok;
        }

        //Counts number of guests registered
        private int NumOfGuests()
        {
            int numGuests = 0;

            for(int i = 0; i < guestList.Length; i++)
            {
                if(!string.IsNullOrEmpty (guestList[i]))
                {
                    numGuests++;
                }
            }
            return numGuests;
        }

        //Returns the number of registered guests from the method above
        public int Count
        {
            get { return NumOfGuests(); }
        }

        //Calculates the total cost
        public double CalcTotalCost()
        {
            int numOfGuests = NumOfGuests();
            double totalCost = numOfGuests * costPerCapita;
            return totalCost;
        }

        //Creates an output array
        public string[] GetGuestList()
        {
            int size = NumOfGuests();

            if (size <= 0)
                return null;

            string[] guests = new string[size];

            for(int i=0, j=0; i < guestList.Length; i++)
            {
                if(!string.IsNullOrEmpty (guestList[i]))
                {
                    guests[j++] = guestList[i];
                }
            }
            return guests;
        }

        //Checks so the array isnt out of bounds
        public bool CheckIndex (int i)
        {
            return (i >= 0) && (i < guestList.Length);
        }

        //Passes one element at a time
        public string GetItemAt (int i)
        {
            if (CheckIndex(i))
                return guestList[i];

            return null;
        }

        //Empties the slot of the array
        public bool DeleteAt(int i)
        {
            bool ok = false;
            if (CheckIndex(i))
            {
                guestList[i] = string.Empty;
                MoveElementsOneStepToLeft(i);
                ok = true;
            }
                return ok;
        }

        //Prevents error when deleting item 0 from the list box
        private void MoveElementsOneStepToLeft(int index)
        {
            for (int i = index+1; i < guestList.Length; i++)
            {
                guestList[i - 1] = guestList[i];
                guestList[i] = string.Empty;
            }
        }

        //Changes the slot of the array
        public bool ChangeAt(int i, string firstName, string lastName)
        {
            bool ok = false;
            if (CheckIndex(i))
            {
                guestList[i] = FullName(firstName, lastName);
            }
            else
                ok = true;

            return ok;
        }
    }
}
