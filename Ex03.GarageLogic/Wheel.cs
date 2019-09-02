using System.Collections.Generic;
using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const int k_MinValueForAmount = 0;
        private readonly string r_MnufacturerName;

        public string MnufacturerName
        {
            get { return r_MnufacturerName; }
        }

        private float m_CurrentAirPressure;

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }

            set
            {
                if (value > r_MaxAirPressure || value < k_MinValueForAmount)
                {
                    throw new ValueOutOfRangeException("air pressure of the wheels", k_MinValueForAmount, r_MaxAirPressure);
                }
                else
                {
                    m_CurrentAirPressure = value;
                }
            }
        }

        private readonly float r_MaxAirPressure;

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public static List<string> Properties()
        {
            List<string> propeties = new List<string>();

            propeties.Add("Mnufacturer name of wheel");
            propeties.Add("Current air pressure");

            return propeties;
        }

        internal Wheel(string i_MnufacturerName, float i_MaxAirPressure, float i_CurrentAirPressure)
        {
            r_MnufacturerName = i_MnufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
            CurrentAirPressure = i_CurrentAirPressure;
        }

        public void AddAir(float i_AmountOfAirToAdd)
        {
            if (i_AmountOfAirToAdd < k_MinValueForAmount)
            {
                throw new ArgumentException("{0} is not a valid value for add air", i_AmountOfAirToAdd.ToString());
            }

            CurrentAirPressure = i_AmountOfAirToAdd + m_CurrentAirPressure;
        }

        public override string ToString()
        {
           return string.Format(
 @"Mnufacturer wheels name:{0}
Wheels current air pressure:{1}",
 r_MnufacturerName,
 m_CurrentAirPressure.ToString());        
        }
    }
}
