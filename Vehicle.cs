using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class Vehicle : IVehicle    
    {
        private string customerName;
        private string contactNumber;
        private string vehiclePickUp;
        private string vehicleType;
        private string manufactureYear;
        //Delegate

        public string CustomerName { get => customerName; set => customerName = value; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public string VehiclePickUp { get => vehiclePickUp; set => vehiclePickUp = value; }
        public string VehicleType { get => vehicleType; set => vehicleType = value; }
        public string ManufactureYear { get => manufactureYear; set => manufactureYear = value; }

        public virtual string VehiclePropertyType { get; }
    }
}
