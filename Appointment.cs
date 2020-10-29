using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class Appointment : IComparable
    {
        private string appTime;
        private Vehicle vehicleData;

        public string AppTime { get => appTime; set => appTime = value; }
        public Vehicle VehicleData { get => vehicleData; set => vehicleData = value; }

        public int CompareTo(object obj)
        {
            return this.vehicleData.CustomerName.CompareTo(((Appointment)obj).vehicleData.CustomerName);
        }
    }
}