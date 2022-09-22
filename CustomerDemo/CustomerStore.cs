using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Model;

namespace CustomerDemo
{
    public class CustomerStore
    {
        private FileStream fs = null;
        private BinaryFormatter bf = null;
        public CustomerStore()
        {
            bf = new BinaryFormatter();
        }
        public void StoreCustomers(CustomerCollection cstData) 
        {
            if (File.Exists("C:\\TPP\\Customer_Data.txt"))
            {
                File.Delete("C:\\TPP\\Customer_Data.txt");
            }
            fs = new FileStream("C:\\TPP\\Customer_Data.txt", FileMode.Create, FileAccess.Write);
            bf.Serialize(fs, cstData);
            fs.Flush();
            fs.Close();
        }
        public CustomerCollection RetrieveCustomers()
        {
            if (File.Exists("C:\\TPP\\Customer_Data.txt"))
            {
                fs = new FileStream("C:\\TPP\\Customer_Data.txt", FileMode.Open, FileAccess.Read);
                CustomerCollection cl = (CustomerCollection)bf.Deserialize(fs);
                fs.Flush();
                fs.Close();
                return cl;
            }
            else
            {
                return new CustomerCollection();
            }
        }
    }
}
