using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
      public enum eVehicleType
      {
         Truck = 1,
         ElectricCar,
         FuelCar,
         FuelMotorBike,
         ElectricMotorBike,
      }

        public static List<string> GetVehicleTypes()
        {
            string tempStr = string.Join(",", Enum.GetNames(typeof(eVehicleType)));
            tempStr = tempStr.Replace("Fuel", "Fuel ").Replace("Electric", "Electric ");

            return new List<string>(tempStr.Split(','));
        }

        public Vehicle CreateVehicle(
            string i_TypeOfVehicle,
            string i_Model,
            string i_LicenseNumber,            
            string i_MnufacturerName,
            string i_CurrentAirPressure)
        {
            Vehicle vehicle = null;
            eVehicleType vehicleType = ParseUtils.EnumParse<eVehicleType>(i_TypeOfVehicle, " is not a type of vehicle");

            switch (vehicleType)
            {
                case eVehicleType.Truck:
                    {
                        vehicle = new Truck(i_Model, i_LicenseNumber, new FuelEngine(FuelEngine.eFuelType.Soler, 110f), i_MnufacturerName, i_CurrentAirPressure);
                        break;
                    }

                case eVehicleType.FuelCar:
                    {
                        vehicle = new Car(i_Model, i_LicenseNumber, new FuelEngine(FuelEngine.eFuelType.Octan96, 55f), i_MnufacturerName, i_CurrentAirPressure);
                        break;
                    }

                case eVehicleType.ElectricCar:
                    {
                        vehicle = new Car(i_Model, i_LicenseNumber, new ElectricityEngine(1.8f), i_MnufacturerName, i_CurrentAirPressure);
                        break;
                    }

                case eVehicleType.FuelMotorBike:
                    {
                        vehicle = new MotorBike(i_Model, i_LicenseNumber, new FuelEngine(FuelEngine.eFuelType.Octan95, 8f), i_MnufacturerName, i_CurrentAirPressure);
                        break;
                    }

                case eVehicleType.ElectricMotorBike:
                    {
                        vehicle = new MotorBike(i_Model, i_LicenseNumber, new ElectricityEngine(1.4f), i_MnufacturerName, i_CurrentAirPressure);
                        break;
                    }
            }

            return vehicle;
        }  
    }
}
