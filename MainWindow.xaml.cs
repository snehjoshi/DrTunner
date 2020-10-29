using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace finalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Hashtable timeData = null;
        AppointmentList appointmentList = null;
        ObservableCollection<Appointment> obsCollection = null;

        string appointmentTime = string.Empty;
        string customerName = string.Empty;
        string contactNumber = string.Empty;
        string yearOfManufacture = string.Empty;
        int manufactureYearNumber = 0;
        bool vehiclePickUp;
        string vehicleType = string.Empty;
        bool additionalProperty;
        bool result = true;
        string fileName = "UserData.xml";

       
        public MainWindow()
        {
            InitializeComponent();

            Validation.AddErrorHandler(this.comboBoxAppointment, AppointmentValidationError);
            Validation.AddErrorHandler(this.txtCustomerName, NameValidationError);
            Validation.AddErrorHandler(this.txtContact, ContactValidationError);
            Validation.AddErrorHandler(this.txtManufactureYear, YearValidationError);

            timeData = new Hashtable();
            timeData = AddAppointment();
            DataContext = this;
            appointmentList = new AppointmentList();
            obsCollection = new ObservableCollection<Appointment>();
            ArrayList arrayList = new ArrayList(timeData.Keys);
            for (int i = 0; i < arrayList.Count; i++) {
                comboBoxAppointment.Items.Add(timeData[i]);
            }
            DisplayFromXml();
           
        }

        private void YearValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                txtManufactureYear.ToolTip = (string)e.Error.ErrorContent.ToString();
            }
            if (e.Action == ValidationErrorEventAction.Removed)
            {
                txtManufactureYear.ToolTip = null;
            }
        }

        private void ContactValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                txtContact.ToolTip = (string)e.Error.ErrorContent.ToString();
            }
            if (e.Action == ValidationErrorEventAction.Removed)
            {
                txtContact.ToolTip = null;
            }
        }

        private void NameValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                txtCustomerName.ToolTip = (string)e.Error.ErrorContent.ToString();
            }
            if (e.Action == ValidationErrorEventAction.Removed)
            {
                txtCustomerName.ToolTip = null;
            }
        }

        private void AppointmentValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                comboBoxAppointment.ToolTip = (string)e.Error.ErrorContent.ToString();
            }
            if (e.Action == ValidationErrorEventAction.Removed)
            {
                comboBoxAppointment.ToolTip = null;
            }
        }



        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            lblError.Content = "";
            lblError.Foreground = Brushes.Black;
            comboBoxAppointment.ClearValue(TextBox.BorderBrushProperty);
            txtCustomerName.ClearValue(TextBox.BorderBrushProperty);
            txtContact.ClearValue(TextBox.BorderBrushProperty);
            comboBoxTypeOfVehicle.ClearValue(TextBox.BorderBrushProperty);

            appointmentTime = comboBoxAppointment.Text.ToString();
            customerName = txtCustomerName.Text;
            contactNumber = txtContact.Text;
            vehicleType = comboBoxTypeOfVehicle.Text;
            yearOfManufacture = txtManufactureYear.Text;

            ///validation for appointment combo box
            do
            {
                if (comboBoxAppointment.SelectedIndex == -1)
                {
                    result = false;
                    MessageBox.Show("Please select Appointment");
                    break;
                }
                else
                {
                    
                    comboBoxAppointment.BorderBrush = Brushes.Black;
                    break;
                }
            } while (true);


            ///Validation forcustomer Name
            do
            {
                if (customerName.Trim().Length != 0)
                {
                    
                    txtCustomerName.Foreground = Brushes.Black;
                    char[] customerNameChar = customerName.ToArray();
                    foreach (char index in customerNameChar)
                    {
                        if (!char.IsLetter(index))
                        {
                            result = false;
                            lblError.Content = "Please enter valid customer name";
                            lblError.Foreground = Brushes.Red;
                            txtCustomerName.Foreground = Brushes.Red;
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    result = false;
                    lblError.Content = "Please enter valid customer name";
                    lblError.Foreground = Brushes.Red;
                    txtCustomerName.Foreground = Brushes.Red;
                    break;
                }
            } while (true);

            ///validation for contact number
            do
            {
                if (contactNumber.Trim().Length != 0)
                {
                   
                    txtContact.Foreground = Brushes.Black;
                    char[] contactNumberChar = contactNumber.ToArray();
                    foreach (char index in contactNumberChar)
                    {
                        if (!char.IsNumber(index))
                        {
                            result = false;
                            lblError.Content = "Please enter valid contact name";
                            lblError.Foreground = Brushes.Red;
                            txtContact.Foreground = Brushes.Red;
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    result = false;
                    lblError.Content = "Please enter valid contact name";
                    lblError.Foreground = Brushes.Red;
                    txtContact.Foreground = Brushes.Red;
                    break;
                }
            } while (true);

            ///validation for year of manufacture
            do {

                if (yearOfManufacture.Trim().Length == 0)
                {
                    result = false;
                    txtManufactureYear.Foreground = Brushes.Red;
                    break;
                }
                else if (int.TryParse(yearOfManufacture, out manufactureYearNumber) && manufactureYearNumber > 1949 && manufactureYearNumber < 2020)
                {
                    lblError.Content = "";

                    txtManufactureYear.Foreground = Brushes.Black;
                    break;
                }
                else {
                    result = false;
                    lblError.Content = "Please enter valid manufacture year [1950-2019] name";
                    lblError.Foreground = Brushes.Red;
                    txtManufactureYear.Foreground = Brushes.Red;
                    break;
                }
            } while (true);

            vehicleType = comboBoxTypeOfVehicle.Text.ToString();

            ///If validation is working, data will be saved into XML
            if (result)
            {
                try
                {
                    appointmentTime = (string)timeData[comboBoxAppointment.SelectedIndex];
                    timeData.Remove(comboBoxAppointment.SelectedIndex);
                    comboBoxAppointment.Items.Clear();
                    ArrayList arrayList = new ArrayList(timeData.Keys);
                    for (int i = 0; i < arrayList.Count; i++)
                    {
                        comboBoxAppointment.Items.Add(timeData[i]);
                    }


                    ///input fields will be cleared
                    txtCustomerName.Text = "";
                    txtContact.Text = "";
                    txtManufactureYear.Text = "";
                    comboBoxTypeOfVehicle.SelectedIndex = 0;

                    ///saving data into vehicle object before storing into XML
                    Vehicle vehicle = null;
                    switch (comboBoxTypeOfVehicle.SelectedIndex)
                    {
                        case 0:
                            {
                                vehicle = new Car()
                                {
                                    CustomerName = customerName,
                                    ContactNumber = contactNumber,
                                    VehiclePickUp = vehiclePickUp ? "Vehicle Pickup service is required" : "Vehicle Pickup service is not required",
                                    VehicleType = vehicleType,
                                    ManufactureYear = yearOfManufacture,
                                    IsVacuumNeeded = additionalProperty
                                };
                                break;
                            }
                        case 1:
                            {
                                vehicle = new GoodsCarriage()
                                {
                                    CustomerName = customerName,
                                    ContactNumber = contactNumber,
                                    VehiclePickUp = vehiclePickUp ? "Vehicle Pickup service is required" : "Vehicle Pickup service is not required",
                                    VehicleType = vehicleType,
                                    ManufactureYear = yearOfManufacture,
                                    IsBackrackNeeded = additionalProperty
                                };
                                break;
                            }
                        case 2:
                            {
                                vehicle = new MultiUtilityVehicle()
                                {
                                    CustomerName = customerName,
                                    ContactNumber = contactNumber,
                                    VehiclePickUp = vehiclePickUp ? "Vehicle Pickup service is required" : "Vehicle Pickup service is not required",
                                    VehicleType = vehicleType,
                                    ManufactureYear = yearOfManufacture,
                                    IsHydraulicsTuningNeeded = additionalProperty
                                };
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    Appointment appointmentObj = new Appointment()
                    {
                        AppTime = appointmentTime,
                        VehicleData = vehicle
                    };

                    appointmentList.Add(appointmentObj);
                    InsertIntoXml();
                }
                catch (Exception exc)
                {
                    
                }
                
            }

        }

        public void InsertIntoXml() {            
            XmlSerializer serializer = new XmlSerializer(typeof(AppointmentList));
            TextWriter textWriter = new StreamWriter(fileName);
            serializer.Serialize(textWriter, appointmentList);
            textWriter.Close();
            appointmentList.Clear();
            MessageBox.Show("Appointment booking successful!");
        }

        public Hashtable AddAppointment() {
            Hashtable appointmentData = new Hashtable();
            int hour = 8;
            string timeString = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                timeString = hour.ToString() + ":00";
                appointmentData.Add(i, timeString);
                hour++;
            }
            return appointmentData;
        }

        private void RadioPropYes_Checked(object sender, RoutedEventArgs e)
        {
            additionalProperty = true;
        }

        private void RadioPropNo_Checked(object sender, RoutedEventArgs e)
        {
            additionalProperty = false;
        }

        private void RadioServiceYes_Checked(object sender, RoutedEventArgs e)
        {
            vehiclePickUp = true;
        }

        private void RadioServiceNo_Checked(object sender, RoutedEventArgs e)
        {
            vehiclePickUp = false;
        }

        private void ComboBoxVehicleType(object sender, SelectionChangedEventArgs e)
        {
            try {
                switch (comboBoxTypeOfVehicle.SelectedIndex)
                {
                    case 0:
                        {
                            lblTypeOfProperty.Content = "Do you want vacuum cleaning ?";
                            break;
                        }
                    case 1:
                        {
                            lblTypeOfProperty.Content = "Do you want to install Backrack?";
                            break;
                        }
                    case 2:
                        {
                            lblTypeOfProperty.Content = "Do you want to do Hydraulics tuning?";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception exc) {

            }
           
        }

        private void BtnDisplay_Click(object sender, RoutedEventArgs e)
        {
            DisplayFromXml();
        }

        public void DisplayFromXml() {
            TunerGrid.DataContext = null;
            if (File.Exists(fileName)) {
                appointmentList.Clear();

                TunerGrid.Items.Refresh();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppointmentList));
                StreamReader streamReader = new StreamReader(fileName);
                appointmentList = (AppointmentList)xmlSerializer.Deserialize(streamReader);
                streamReader.Close();

                obsCollection.Clear();
               
                    foreach (Appointment appointment in appointmentList)
                {
                    
                    obsCollection.Add(appointment);
                    comboBoxAppointment.Items.Remove(appointment.AppTime);
                    
                }


                TunerGrid.ItemsSource = obsCollection;

            }           
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (TunerGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an appointment from result list!");
            }
            else
            {
                ///delete the appoint from appointList
                Appointment appDel = TunerGrid.SelectedItem as Appointment;
                if(appDel != null)
                {
                    for (int i = 0; i < appointmentList.Count(); i++)
                    {
                        if (appointmentList[i].AppTime == appDel.AppTime)
                        {
                            appointmentList.Remove(i);
                            comboBoxAppointment.Items.Add(appointmentList[i]);
                            break;
                        }
                    }
                    //save data to .xml by serialization
                    XmlSerializer serializer = new XmlSerializer(typeof(AppointmentList));
                    TextWriter tw = new StreamWriter(fileName);
                    serializer.Serialize(tw, appointmentList);
                    tw.Close();
                    obsCollection.Clear();
                    DisplayFromXml();
                    MessageBox.Show("Delete successfully!");
                }
                
            }
        }

        ///Filter Combo Box 
        private void ComboBoxFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AppointmentList newFilterList = new AppointmentList();
            switch (comboBoxFields.SelectedIndex) {
                case 0: {
                        var vehicleQuery = from appointment in appointmentList
                                    orderby appointment.VehicleData.VehicleType descending
                                    select appointment;

                        foreach (Appointment appt in vehicleQuery)
                        {
                            newFilterList.Add(appt);
                        }
                        TunerGrid.ItemsSource = newFilterList;
                        break;
                    }
                case 1: {
                        var customerName = from appointment in appointmentList
                                         orderby appointment.VehicleData.CustomerName descending
                                         select appointment;
                        foreach (Appointment appt in customerName)
                        {
                            newFilterList.Add(appt);
                        }
                        TunerGrid.ItemsSource = newFilterList;
                        break;
                    }
            }
        }

        private void txtCustomerName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCustomerName.Foreground = Brushes.Black;
        }

        private void txtContactNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            txtContact.Foreground = Brushes.Black;
        }

        private void txtManufactureYear_GotFocus(object sender, RoutedEventArgs e)
        {
            txtManufactureYear.Foreground = Brushes.Black;
        }
    }
}
