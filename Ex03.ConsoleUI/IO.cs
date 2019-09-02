using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public static class IO
    {
        public static void ShowUserOptions()
        {
            Console.WriteLine(string.Format(
@"what would you like to do?
Press (1) to put a new car in the garage
Press (2) to get list of license numbers of vehicles in the Garage
Press (3) to change state in the garage of existing vehicle
Press (4) to pump the wheels of specific vehicle to the maximum
Press (5) to refuel a specific vehicle
Press (6) to charge a specific vehicle
Press (7) to get the details of a specific vehicle
Press (8) Exit :("));
        }       
       
        public static void PrintMsg(string i_Msg)
        {
            Console.WriteLine(i_Msg);
        }

        public static void UnMatchingEngineMsg(string i_EngineType, string i_LicenseNumber)
        {
            Console.WriteLine("The vehicle {0} doesn't have an {1} engine.", i_LicenseNumber, i_EngineType);
        }

        public static void ExceptionMsg(Exception ex, string i_Msg)
        {
            Console.WriteLine(string.Format("{0}: {1}", ex.GetType(), i_Msg));
        }

        public static void VehicleNotInGarageMsg(string i_LicenseNumber)
        {
            Console.WriteLine("The vehicle {0} is not in the garage", i_LicenseNumber);
        }

        public static string PrintMsgAndGetValue(string i_Msg)
        {
            Console.WriteLine(i_Msg);
            string returnValue = Console.ReadLine();

            while (isNullOrHasWhiteSpaces(returnValue))
            {
                Console.WriteLine("Your input is not valid, please try again");
                returnValue = Console.ReadLine();
            }

            return returnValue;
        }

        private static bool isNullOrHasWhiteSpaces(string i_StrToCheck)
        {
            bool nullOrHasWhiteSpaces = string.IsNullOrEmpty(i_StrToCheck);

            if (!nullOrHasWhiteSpaces)
            {
                foreach (char charToCheck in i_StrToCheck)
                {
                    if (char.IsWhiteSpace(charToCheck))
                    {
                        nullOrHasWhiteSpaces = true;
                        break;
                    }
                }
            }

            return nullOrHasWhiteSpaces;
        }

        public static string GetPhoneNumber()
        {
            Console.WriteLine("Please enter your phone number");
            string input = Console.ReadLine();

            while (!isValidPhoneNumber(input))
            {
                Console.WriteLine("Phone number should contains only digits, please try again");
                input = Console.ReadLine();
            }

            return input;
        }

        private static bool isValidPhoneNumber(string i_PhoneNumberToCheck)
        {
            bool valid = !string.IsNullOrEmpty(i_PhoneNumberToCheck);

            if (valid)
            {
                foreach (char digit in i_PhoneNumberToCheck)
                {
                    if (!char.IsDigit(digit))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            return valid;
        }

        public static string GetLicenseNumber()
        {
            Console.WriteLine("Please enter your license number");
            string input = Console.ReadLine();

            while (!isValidLicenseNumber(input)) 
            {
                Console.WriteLine("License number should contains only digit and letters, please try again");
                input = Console.ReadLine();
            }

            return input;
        }

        private static bool isValidLicenseNumber(string i_LicenseNumberToCheck)
        {
            bool valid = !string.IsNullOrEmpty(i_LicenseNumberToCheck);

            if (valid)
            {
                foreach (char letter in i_LicenseNumberToCheck)
                {
                    if (!char.IsLetterOrDigit(letter))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            return valid;
        }

        public static string GetOwnerName()
        {
            string name;
            Console.WriteLine("Please enter your name");
            name = Console.ReadLine();

            while (!isValidName(name))
            {
                Console.WriteLine("Name should contains only letters, please try again");
                name = Console.ReadLine();
            }

            return name;
        }

        private static bool isValidName(string i_Name)
        {
            bool valid = !string.IsNullOrEmpty(i_Name) &&
                i_Name.Trim().Length == i_Name.Length &&
                i_Name.TrimEnd().Length == i_Name.Length;

            if (valid)
            {
                foreach (char letter in i_Name)
                {
                    if (!char.IsLetter(letter))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            return valid;
        }
        
        public static string GetUserFilterChoice()
        {
            int numOfOption = Garage.GetGrageStates().Count + 1;
            PrintList(Garage.GetGrageStates(), "Choose a state to filter by:");
            Console.WriteLine(string.Format("{0}. Print all the license numbers without filter", numOfOption));

            string userChoice = GetUserOptionChoice(numOfOption);

            return userChoice;
        }
        
        public static string GetUserOptionChoice(int i_NumOfOptions)
        {
            string userChoice = Console.ReadLine();

            while (!isValidChoiceFromList(i_NumOfOptions, userChoice))
            {
                Console.WriteLine("Your input is not valid, please try again");
                userChoice = Console.ReadLine();
            }

            return userChoice;
        }

        private static bool isValidChoiceFromList(int i_NumOfOptions, string i_userChoice)
        {
            int numOfChoice;
            return !isNullOrHasWhiteSpaces(i_userChoice) &&
                int.TryParse(i_userChoice, out numOfChoice) &&
                 numOfChoice <= i_NumOfOptions &&
                 numOfChoice > 0;
        }

        public static List<string> GetVehicleDetailsFromUser(List<string> i_ProprtiesForSpecificVehicle)
        {        
            return GetValidInputListFromUser(i_ProprtiesForSpecificVehicle);
        }

        public static void PrintList(List<string> i_ListToPrint, string i_Title)
        {
            if (i_ListToPrint.Count != 0)
            {
                Console.WriteLine(i_Title);
                int optionCounter = 1;

                foreach (string line in i_ListToPrint)
                {
                    Console.WriteLine("{0}. {1}", optionCounter, line);
                    optionCounter++;
                }
            }
            else
            {
                Console.WriteLine("There are no matching items");
            }
        }
      
        public static void GetVehicleProperties(out string o_VehicleType, out string o_Model, out string o_MnufacturerName, out string o_CurrentAirPressure)    
        {
            PrintList(VehicleFactory.GetVehicleTypes(), "choose a type of vehicle:");
            o_VehicleType = GetUserOptionChoice(VehicleFactory.GetVehicleTypes().Count);
            List<string> inputsForVehicle = GetValidInputListFromUser(Vehicle.BasicProperties());
            
            o_Model = inputsForVehicle[0];
            o_MnufacturerName = inputsForVehicle[1];
            o_CurrentAirPressure = inputsForVehicle[2];
        }
     
        public static List<string> GetValidInputListFromUser(List<string> i_PropertiesList)
        {
            List<string> validInputs = new List<string>(i_PropertiesList.Count);

            foreach (string propertie in i_PropertiesList) 
            {             
                validInputs.Add(PrintMsgAndGetValue(propertie));               
            }

            return validInputs;
        }       
    }
}
