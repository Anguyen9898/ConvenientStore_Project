using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DTO
{
   public class DetailBill
    {
        //public string billCode { get; set; }
        //public string barCode { get; set; }
        public string Quan { get; set; }
        public double price { get; set; }
        public string Discount { get; set; }
        public double total { get; set; }
        public DetailBill (string Quan, double price ,string dis, double total)
        {
            //this.billCode = billCode;
            //this.barCode = barCode;
            this.Quan = Quan;
            this.price = price;
            this.Discount = dis;
            this.total = total;
        }
    }
}
