using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvenientStore_Pro
{
    public class Sum
    {
        public double dtCash { get; set; }
        public double ttCash { get; set; }
        public double dtCre { get; set; }
        public double ttCre { get; set; }
        public double Funds { get; set; }
        public Sum(double dtCash, double ttCash, double dtCre, double ttCre, double Funds)
        {
            this.dtCash = dtCash;
            this.ttCash = ttCash;
            this.dtCre = dtCre;
            this.ttCre = ttCre;
            this.Funds = Funds;
        }
        public Sum(){}
    }
}
