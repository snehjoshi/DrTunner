using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class MultiUtilityVehicle : Vehicle
    {
        private bool isHydraulicsTuningNeeded;

        public bool IsHydraulicsTuningNeeded { get => isHydraulicsTuningNeeded; set => isHydraulicsTuningNeeded = value; }

        public override string VehiclePropertyType
        {
            get
            {
                return isHydraulicsTuningNeeded ? "Hydraulics tuning is required" : "Hydraulics tuning is not required";
            }
        }
    }
}
