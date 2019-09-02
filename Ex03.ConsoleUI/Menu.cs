using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
   public class Menu
    {
        private const int k_NumberOfOptionsInMenu = 8;    
        
        public enum eGarageOptions
        {
            InsertVehicleToGarage = 1,
            ShowLicenseNumbers,
            ChangeGarageStateOfVehicle,
            AddAirToWheel,
            RefulVehicle,
            ChargeVehicle,
            ShowVehicleDetails,
            ExitFromProgram
        }

        private GarageLogic.Garage garage;

        public Menu()
        {
            garage = new GarageLogic.Garage();
        }

        public void Run()
        {
            const bool v_NotExit = true;

            IO.PrintMsg("Hello,welcome to the .NET garage!");
            eGarageOptions userChoice;    
            
            while (v_NotExit)
            {
                IO.ShowUserOptions();
                userChoice = getUserOption();

                try
                {
                    if (userChoice == eGarageOptions.ExitFromProgram)
                    {
                        IO.PrintMsg("Bye Bye,see you next time!");
                        break;
                    }
                    else if (userChoice == eGarageOptions.ShowLicenseNumbers)
                    {                    
                        string stateToFilterBy = IO.GetUserFilterChoice();
                        IO.PrintList(garage.GetLicenseNumbersOfVehicles(stateToFilterBy), "The license numbers are:");
                    }
                    else 
                    {
                        needLicenseNumber(userChoice);          
                    }
                }
                catch (FormatException ex)
                {
                    IO.ExceptionMsg(ex, ex.Message);
                }
                catch (ArgumentException ex)
                {
                    IO.ExceptionMsg(ex, ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    IO.ExceptionMsg(ex, ex.Message);
                }
            }                       
        }

        private void insertVehicleToGarge(string i_LicenseNumber)
        {           
            if (garage.IsVehicleInGarage(i_LicenseNumber))
            {
                garage.SetStateToExistedClient(i_LicenseNumber);
                IO.PrintMsg("Your vehicle is allready in the garage");
            }
            else
            {
                string ownerName = IO.GetOwnerName();
                string phoneNumber = IO.GetPhoneNumber();
                string vehicleType;
                string vehicleModel;
                string currentAirPresuure;
                string mnufacturerName;
                IO.GetVehicleProperties(out vehicleType, out vehicleModel, out mnufacturerName, out currentAirPresuure);
                Vehicle vehicle = garage.VehiclsFactory.CreateVehicle(vehicleType, vehicleModel, i_LicenseNumber, mnufacturerName, currentAirPresuure);
                List<string> vehicleProperties = vehicle.Properties;
                vehicle.Properties = IO.GetVehicleDetailsFromUser(vehicleProperties);
                garage.InsertNewVehicleToGarage(ownerName, phoneNumber, vehicle);
            }
        }

        private eGarageOptions getUserOption() 
        {
            return (eGarageOptions)Enum.Parse(typeof(eGarageOptions), IO.GetUserOptionChoice(k_NumberOfOptionsInMenu));
        }    
        
        private void needLicenseNumber(eGarageOptions i_UserChoice)
        {
            string licenseNumber = IO.GetLicenseNumber();

            if (i_UserChoice == eGarageOptions.InsertVehicleToGarage)
            {
                insertVehicleToGarge(licenseNumber);
            }
            else if (!garage.IsVehicleInGarage(licenseNumber))
            {
                IO.VehicleNotInGarageMsg(licenseNumber);
            }
            else
            {
                switch (i_UserChoice)
                {
                    case eGarageOptions.ChangeGarageStateOfVehicle:
                        {
                            changeGarageStateOfVehicle(licenseNumber);
                            break;
                        }

                    case eGarageOptions.AddAirToWheel:
                        {
                            garage.FillAirToMax(licenseNumber);
                            break;
                        }

                    case eGarageOptions.RefulVehicle:
                        {
                            refulVehicle(licenseNumber);
                            break;
                        }

                    case eGarageOptions.ChargeVehicle:
                        {
                            chargeVihicle(licenseNumber);
                            break;
                        }

                    case eGarageOptions.ShowVehicleDetails:
                        {
                            IO.PrintMsg(garage.GetClientVehicleDetails(licenseNumber));

                            break;
                        }
                }
            }
        }

        private void changeGarageStateOfVehicle(string i_LicenseNumber)
        {
            IO.PrintList(Garage.GetGrageStates(), "choose the state to change:");
            string typeToChange = IO.GetUserOptionChoice(Garage.GetGrageStates().Count);
            garage.ChangeStateOfVehicle(i_LicenseNumber, typeToChange);
        }

        private void refulVehicle(string i_LicenseNumber)
        {
            if (garage.IsFuelVehicle(i_LicenseNumber))
            {
                IO.PrintList(FuelEngine.getFuelTypes(), "choose the number of the desired fuel type:");
                string fuelTypeToSet = IO.GetUserOptionChoice(FuelEngine.getFuelTypes().Count);
                string amountOfFuelToAdd = IO.PrintMsgAndGetValue("Please enter the amount of fuel (in liters) to refuel");
                garage.Refuel(i_LicenseNumber, amountOfFuelToAdd, fuelTypeToSet);
            }
            else
            {
                IO.UnMatchingEngineMsg("fuel", i_LicenseNumber);
            }
        }

        private void chargeVihicle(string i_LicenseNumber)
        {
            if (garage.IsElectricVehicle(i_LicenseNumber))
            {
                string amountOfMinutesToCharge = IO.PrintMsgAndGetValue("Please enter number of minutes to charge");
                garage.ChargeBattery(i_LicenseNumber, amountOfMinutesToCharge);
            }
            else
            {
                IO.UnMatchingEngineMsg("electric", i_LicenseNumber);
            }
        }
    }
}
