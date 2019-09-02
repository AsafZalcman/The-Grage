using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelEngine : EngineType
    {
       public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98,
        }

        private eFuelType m_FuelLType;

        public eFuelType FuelType
        {
            get { return m_FuelLType; }
        }

        public static List<string> getFuelTypes()
        {
            return new List<string>(Enum.GetNames(typeof(eFuelType)));
        }

        internal FuelEngine(eFuelType i_FuelType, float i_MaxAmountOfFuel)
            : base(i_MaxAmountOfFuel)
        {
            m_FuelLType = i_FuelType;          
        }

        public void FillFuel(float i_AmountOfFuelToFill, eFuelType i_FuelType)
        {
            if(i_FuelType == m_FuelLType)
            {
                ChargeEneregy(i_AmountOfFuelToFill);
            }
            else
            {
                throw new ArgumentException(i_FuelType.ToString() + " is not matching the vehicle fuel type");
            }
        }

        public override string Properties
        {
            get { return "current amount of fuel in liters:"; }         
        }

        public override string ToString()
        {
            return string.Format(
@"Current amount of fuel in liters:{0}
Type of fuel:{1}",
base.ToString(),
m_FuelLType.ToString());
        }          
    }
}
