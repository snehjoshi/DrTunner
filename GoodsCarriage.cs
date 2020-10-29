using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class GoodsCarriage :  Vehicle
    {
        private bool isBackrackNeeded;

        public bool IsBackrackNeeded { get => isBackrackNeeded; set => isBackrackNeeded = value; }
    
        public override string VehiclePropertyType
        {
            get
            {
                return isBackrackNeeded ? "Backrack should be installed" : "Backrack should not be installed";
            }
        }
    }
}
