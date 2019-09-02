using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_MinValueForAmount = 0;
        private const int k_NumOfWheels = 12;
        private const float k_MaxAirInWheels = 26;

        private bool m_DoesHazardousMaterialTransport;

        internal bool DoesHazardousMaterialTransport
        {
            get { return m_DoesHazardousMaterialTransport; }         
        }

        private float m_CargoVolume;
      
        internal float CargoVolume
        {
            get { return m_CargoVolume; }

            private set
            {
                if (value < k_MinValueForAmount)
                {
                    throw new ValueOutOfRangeException("cargo volume", k_MinValueForAmount);
                }
                else
                {
                    m_CargoVolume = value;
                }
            }
        }

        internal Truck(
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
                propeties.Add("Does hazardous material transport (True or False)");
                propeties.Add("Cargo volume:");
                propeties.Add(m_EngineType.Properties);                
                return propeties;
            }

            set
            {
                m_DoesHazardousMaterialTransport = ParseUtils.Parse<bool>(value[0], " is not a valid value");
                CargoVolume = ParseUtils.Parse<float>(value[1], " is not a valid volume for truck cargo");
                m_EngineType.CurrentAmountOfEneregy = ParseUtils.Parse<float>(value[2], " is not valid amount of energy");      
                UpadteCurrentPresentOfEneregy();
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
Does hazardous material transport:{1}
Cargo volume:{2}", 
base.ToString(),
m_DoesHazardousMaterialTransport.ToString(),
m_CargoVolume.ToString());
        }      
    }
}
