using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class MotorBike : Vehicle
    {
        private const int k_MinValueForAmount = 0;
        private const int k_NumOfWheels = 2;
        private const float k_MaxAirInWheels = 33;
  
            internal enum eLisenceType
            {
                A = 1,
                A1,
                A2,
                B,
            }

        private eLisenceType m_LisenceType;

        internal eLisenceType LisenceType
        {
            get { return m_LisenceType; }
            set { m_LisenceType = value; }
        }

        private int m_EngineCapacity;
        
        internal int EngineCapacity
        {
            get { return m_EngineCapacity; }

            private set
            {
                if (value < k_MinValueForAmount)
                {
                    throw new ValueOutOfRangeException("engine capacity", k_MinValueForAmount);
                }
                else
                {
                    m_EngineCapacity = value;
                }           
            }
        }

        internal MotorBike(
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
                propeties.Add(string.Format("License type({0})", getLisenceTypes()));
                propeties.Add("Engine capacity:");
                propeties.Add(m_EngineType.Properties);               
                return propeties;
            }

            set
            {
                LisenceType = ParseUtils.EnumParse<eLisenceType>(value[0], " is not a Type of motorbike license");
                EngineCapacity = ParseUtils.Parse<int>(value[1], " is not a valid cacpacity for motorbike engine");
                m_EngineType.CurrentAmountOfEneregy = ParseUtils.Parse<float>(value[2], " is not a valid amount of energy");
                UpadteCurrentPresentOfEneregy();
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
Lisence type:{1}
Engine capacity:{2}",
base.ToString(),
m_LisenceType.ToString(),
m_EngineCapacity.ToString());
        }
     
       private string getLisenceTypes()
        {
            return string.Join(",", Enum.GetNames(typeof(eLisenceType)));
        }      
    }
}
