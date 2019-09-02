using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_Model;

        public string Model
        {
            get { return r_Model; }
        }

        private readonly string r_LicenseNumber;

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        private float m_PrecentOfEnergyLeft;

        public float PowerPrecentOfEnergyLeft
        {
            get { return m_PrecentOfEnergyLeft; }            
        }

        protected EngineType m_EngineType;

        public EngineType EneregyType
        {
            get { return m_EngineType; }
        }

        private Wheel[] m_Wheels;

        internal Wheel[] Wheels
        {
            get { return m_Wheels; }                       
        }

        public static List<string> BasicProperties()
        {
            List<string> properties = new List<string>();
            properties.Add("Model");
            properties.AddRange(Wheel.Properties());
            return properties;
        }

        public abstract List<string> Properties
        {
            get;           
            set;
        }

        public Vehicle(
            string i_Model,
            string i_LicenseNumber,
            EngineType i_EngineType,
            string i_MnufacturerName,
            string i_CurrentAirPressure,
            float i_MaxAirPressure,
            int i_NumberOfWheels)
        {
            float CurrentAirPressure = ParseUtils.Parse<float>(i_CurrentAirPressure, " is not a valid amount of air");
            SetWheels(i_MnufacturerName, CurrentAirPressure, i_MaxAirPressure, i_NumberOfWheels);
            r_Model = i_Model;
            r_LicenseNumber = i_LicenseNumber;
            m_EngineType = i_EngineType;                     
        }

        public void UpadteCurrentPresentOfEneregy()
        {
            m_PrecentOfEnergyLeft = 100 * m_EngineType.CurrentAmountOfEneregy / m_EngineType.MaxAmountOfEneregy;
        }

        public override string ToString()
        {
            return string.Format(
@"License Number:{0}
Model:{1}
{2}
{3}
Precent of energy left in engine:{4}",
r_LicenseNumber,
r_Model,
m_Wheels[0].ToString(),
m_EngineType.ToString(),
m_PrecentOfEnergyLeft.ToString());
        }

        public void SetWheels(string i_MnufacturerName, float i_CurrentAirPressure, float i_MaxAirForWheel, int i_NumOfWheels)
        {
            m_Wheels = new Wheel[i_NumOfWheels];

            for (int i = 0; i < i_NumOfWheels; i++)  
            {
                m_Wheels[i] = new Wheel(i_MnufacturerName, i_MaxAirForWheel, i_CurrentAirPressure);           
            }
        }       
    }
}
