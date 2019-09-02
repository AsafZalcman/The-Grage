using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private class GarageClient
        {
            private readonly string r_OwnerName;

            public string OwnerName
            {
                get { return r_OwnerName; }
            }

            private readonly string r_OwnerPhoneNumber;

            public string OwnerPhoneNumber
            {
                get { return r_OwnerPhoneNumber; }
            }

            private readonly Vehicle r_Vehicle;

            public Vehicle vehicle
            {
                get { return r_Vehicle; }
            }

            private eStateInGarage m_StateInGarage;

            public eStateInGarage StateInGarage
            {
                get { return m_StateInGarage; }
                set { m_StateInGarage = value; }
            }

            public GarageClient(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_vehicle)
            {
                r_OwnerName = i_OwnerName;
                r_OwnerPhoneNumber = i_OwnerPhoneNumber;
                r_Vehicle = i_vehicle;
                m_StateInGarage = eStateInGarage.InAmendment;
            }

            public override string ToString()
            {
                return string.Format(
 @"Owner name:{0}
Phone number:{1}
State in garage:{2}
{3}",
 r_OwnerName,
 r_OwnerPhoneNumber,
 m_StateInGarage.ToString(),
 r_Vehicle.ToString());
            }
        }

        private const int k_NumOfMinutsInHour = 60;
       
        public enum eStateInGarage
        {
            InAmendment = 1,
            Fixed,
            Paid,
        }

        private Dictionary<string, GarageClient> m_Vehicls;
     
        private readonly VehicleFactory r_Factory;

        public VehicleFactory VehiclsFactory
        {
            get { return r_Factory; }
        }

        public Garage()
        {
            m_Vehicls = new Dictionary<string, GarageClient>();
            r_Factory = new VehicleFactory();
        }

        public static List<string> GetGrageStates()
        {
            return new List<string>(Enum.GetNames(typeof(eStateInGarage)));
        }

        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            return m_Vehicls.ContainsKey(i_LicenseNumber);
        }
      
        public void SetStateToExistedClient(string i_LicenseNumber)
        {
            m_Vehicls[i_LicenseNumber].StateInGarage = eStateInGarage.InAmendment;
        }

        public void InsertNewVehicleToGarage(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_vehicel)
        {
            m_Vehicls.Add(i_vehicel.LicenseNumber, new GarageClient(i_OwnerName, i_OwnerPhoneNumber, i_vehicel));
        }

        public List<string> GetLicenseNumbersOfVehicles(string i_StateInGarage)
        {
            bool getAllLicense;
            List<string> licenseNumbers = new List<string>();
            eStateInGarage garageStateToCheck = (eStateInGarage)Enum.Parse(typeof(eStateInGarage), i_StateInGarage);

            getAllLicense = !Enum.IsDefined(typeof(eStateInGarage), garageStateToCheck);

            foreach (KeyValuePair<string, GarageClient> item in m_Vehicls)
            {
                if (item.Value.StateInGarage == garageStateToCheck || getAllLicense)
                {
                    licenseNumbers.Add(item.Key);
                }
            }
           
            return licenseNumbers;
        }

        public void ChangeStateOfVehicle(string i_LicenseNumber, string i_NewState)
        {          
            eStateInGarage garageState = ParseUtils.EnumParse<eStateInGarage>(i_NewState, " it is not a state in the Garage");
            m_Vehicls[i_LicenseNumber].StateInGarage = garageState;
        }

        public void FillAirToMax(string i_LicenseNumber)
        {            
            foreach (Wheel wheel in m_Vehicls[i_LicenseNumber].vehicle.Wheels)
            {
                wheel.AddAir(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public void Refuel(string i_LicenseNumber, string i_AmountOfFuelToFill, string i_FuelType)
        {            
            float amountOfFuelToAdd = ParseUtils.Parse<float>(i_AmountOfFuelToFill, " is not a valid amount of fuel");
            FuelEngine.eFuelType fuelType = ParseUtils.EnumParse<FuelEngine.eFuelType>(i_FuelType, " is not a valid type of fuel");
            Vehicle vehicle = m_Vehicls[i_LicenseNumber].vehicle;
            ((FuelEngine)vehicle.EneregyType).FillFuel(amountOfFuelToAdd, fuelType);          
            vehicle.UpadteCurrentPresentOfEneregy();           
        }

        public void ChargeBattery(string i_LicenseNumber, string i_AmountOfMinutesToCharge)
        {          
            float AmountOfMinutesToCharge = ParseUtils.Parse<float>(i_AmountOfMinutesToCharge, " is not a valid number of minutes");
            Vehicle vehicle = m_Vehicls[i_LicenseNumber].vehicle;
            vehicle.EneregyType.ChargeEneregy(AmountOfMinutesToCharge / k_NumOfMinutsInHour);
            vehicle.UpadteCurrentPresentOfEneregy();
            ElectricityEngine electricEngine = vehicle.EneregyType as ElectricityEngine;           
        }
       
        public string GetClientVehicleDetails(string i_LicenseNumber)
        {           
            return m_Vehicls[i_LicenseNumber].ToString();
        }
     
        public bool IsElectricVehicle(string i_LicenseNumber)
        {
            return m_Vehicls[i_LicenseNumber].vehicle.EneregyType is ElectricityEngine;
        }

        public bool IsFuelVehicle(string i_LicenseNumber)
        {
            return m_Vehicls[i_LicenseNumber].vehicle.EneregyType is FuelEngine;
        }
    }
}
