namespace Ex03.GarageLogic
{
    public class ElectricityEngine : EngineType
    {       
        internal ElectricityEngine(float i_MaxHoursForBattery)
            : base(i_MaxHoursForBattery)
        {
        }
       
        public override string Properties
        {
            get { return "current amount of battery hours left"; }
        }

        public override string ToString()
        {
              return "current amount of battery hours left:" + base.ToString();
        }
    }
}
