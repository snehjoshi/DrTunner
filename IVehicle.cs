using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public interface IVehicle
    {
        string CustomerName { get; set; }
        string ContactNumber { get; set; }
        string VehiclePickUp { get; set; }
        string VehicleType { get; set; }
        string ManufactureYear { get; set; }
        string VehiclePropertyType { get; }
    }
}
