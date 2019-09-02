using System;

namespace Ex03.GarageLogic
{
    public static class ParseUtils
    {
        public static T EnumParse<T>(string i_StrToParse, string i_ExceptionMsg)
        {
            try
            {
                T enumValueToReturn = (T)Enum.Parse(typeof(T), i_StrToParse, true);
                if (Enum.IsDefined(typeof(T), enumValueToReturn))
                {
                    return enumValueToReturn;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception)
            {
                throw new FormatException(i_StrToParse + i_ExceptionMsg);
            }
        }

        public static T Parse<T>(string i_StrToParse, string i_ExceptionMsg)
        {
            try
            {
                return (T)Convert.ChangeType(i_StrToParse, typeof(T));
            }
            catch(Exception)
            {
                throw new FormatException(i_StrToParse + i_ExceptionMsg);
            }
        }
    }
}