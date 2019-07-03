using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UralskMap
{
    public class EnumHelper
    {
        public static string Description(Enum eValue)
        {
            if (eValue == null)
                return string.Empty;
            var nAttributes = eValue.GetType().GetField(eValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return nAttributes.Any() ? (nAttributes.First() as DescriptionAttribute)?.Description : string.Empty;
        }
    }
}
