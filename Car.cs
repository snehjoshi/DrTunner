using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class Car : Vehicle
    {
        private bool isVacuumNeeded;

        public bool IsVacuumNeeded { get => isVacuumNeeded; set => isVacuumNeeded = value; }

       public override string VehiclePropertyType
        {
            get
            {
                return isVacuumNeeded ? "Vacuum cleaning is required" : "Vacuum cleaning is not required";
            }
        }
            
        
    }
}
