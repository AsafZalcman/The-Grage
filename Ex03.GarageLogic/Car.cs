using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private const int k_NumOfWheels = 4;
        private const int k_MinNumOfDoors = 2;
        private const int k_MaxNumOfDoors = 5;
        private const float k_MaxAirInWheels = 31;

        internal enum eCarColor
        {
            Blue = 1,
            Red,
            Black,
            Gray,
        }

        private eCarColor m_Color;

        internal eCarColor Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        private int m_AmountOfDoors;

        internal int AmountOfDoors
        {
            get { return m_AmountOfDoors; }

            private set
            {
                if (value < k_MinNumOfDoors || value > k_MaxNumOfDoors)
                {
                    throw new ValueOutOfRangeException("door's amount", k_MaxNumOfDoors, k_MinNumOfDoors);
                }
                else
                {
                    m_AmountOfDoors = value;
                }
            }
        }

        internal Car(
            string i_Model,
            string i_LicenseNumber,
            EngineType i_EngineType,
            string i_MnufacturerName,
            string i_CurrentAirPressure)                      
            : base(
                  i_Model,
                  i_LicenseNumber,
                  i_EngineType,
                  i_MnufacturerName,
                  i_CurrentAirPressure,
                  k_MaxAirInWheels,
                  k_NumOfWheels)
        {
        }

        public override List<string> Properties
        {
            get
            {             
                List<string> propeties = new List<string>();
                propeties.Add(string.Format("Color:({0})", getCarColors()));
                propeties.Add("Number of doors:");
                propeties.Add(m_EngineType.Properties);
                
                return propeties;
            }

            set
            {
                Color = ParseUtils.EnumParse<eCarColor>(value[0], " is not a valid color for car");
                AmountOfDoors = ParseUtils.Parse<int>(value[1], " is not a valid amount of doors for car");
                m_EngineType.CurrentAmountOfEneregy = ParseUtils.Parse<float>(value[2], " is not valid amount of energy");
                UpadteCurrentPresentOfEneregy();
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
Color:{1}
Number of doors:{2}",
base.ToString(),
m_Color.ToString(),
m_AmountOfDoors.ToString());
        }
       
        private string getCarColors()
        {
            return string.Join(",", Enum.GetNames(typeof(eCarColor)));
        }     
    }
}
