using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerDemo;

namespace Model
{
    [Serializable]
    public class CustomerCollection
    {
        private List<Customer> lcs;
        public CustomerCollection()
        {
            lcs = new List<Customer>();
        }

        public int GenerateID()
        {
            if(lcs.Count() == 0)
            {
                return 1;
            }
            else
            {
                int max = 0;
                foreach(Customer c in lcs)
                {
                    if(max<c.CID)
                    {
                        max = c.CID;
                    }
                }
                return max + 1;
            }
        }

        public void AddCustomer(Customer c)
        {
            lcs.Add(c);
        }
        public bool UpdateCustomer(int cid, Customer c)
        {
            int position = GetPosition(cid);
            
            if (position == -1)
            {
                return false;
            }
            else
            {
                lcs[position] = c;
                return true;
            }
            
        }
        public bool DeleteCustomer(int cid)
        {
            int position = GetPosition(cid);
            if (position == -1)
            {
               return false;
            }
            else
            {
                lcs.RemoveAt(position);
                return true;
            }
            
        }
        public Customer FindCustomer(int cid)
        {
            int position = GetPosition(cid);
            
            if (position == -1)
            {
                return null;
            }
            else
            {
                return lcs[position];
            }
        }
        public List<Customer> GetCustomers()
        {
            return lcs;
        }
        public int GetPosition(int cid)
        {
            int index = 0;
            
            foreach (Customer current in lcs)
            {
                if (cid == current.CID)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }
}
