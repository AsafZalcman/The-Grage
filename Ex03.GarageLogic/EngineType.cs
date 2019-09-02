using System;

namespace Ex03.GarageLogic
{
    public abstract class EngineType
    {
        private const int k_MinValueForAmount = 0;
        private float m_CurrentAmountOfEneregy;

        public float CurrentAmountOfEneregy
        {
            get { return m_CurrentAmountOfEneregy; }

            set
            {
                if (value > m_MaxAmountOfEneregy || value < k_MinValueForAmount)
                {
                    throw new ValueOutOfRangeException("eneregy in the engine", k_MinValueForAmount, MaxAmountOfEneregy);
                }
                else
                {
                    m_CurrentAmountOfEneregy = value;
                }
            }
        }

        private float m_MaxAmountOfEneregy;

        public float MaxAmountOfEneregy
        {
            get { return m_MaxAmountOfEneregy; }
        }

        public EngineType(float i_MaxAmountOfEneregy)
        {
            m_MaxAmountOfEneregy = i_MaxAmountOfEneregy;
        }

        public void ChargeEneregy(float i_AmountOfEneregyToAdd)
        {
            if (i_AmountOfEneregyToAdd < k_MinValueForAmount)
            {
                throw new ArgumentException("{0} is negative value", i_AmountOfEneregyToAdd.ToString());
            }

            CurrentAmountOfEneregy += i_AmountOfEneregyToAdd;         
        }

        public override string ToString()
        {
             return m_CurrentAmountOfEneregy.ToString();
        }

        public abstract string Properties { get; }             
    }
}
