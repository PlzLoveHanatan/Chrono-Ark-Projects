using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mia
{
    public class Mia_Tonic : CustomValue
    {
        public List<string> UsedList;

        public static Mia_Tonic Instance
        {
            get
            {
                if (PlayData.TSavedata == null)
                {
                    return new Mia_Tonic();
                }
                return GetOrAddCustomValue<Mia_Tonic>(PlayData.TSavedata);
            }
        }

        public Mia_Tonic()
        {
            UsedList = new List<string>();
        }

        public static T GetOrAddCustomValue<T>(TempSaveData save) where T : CustomValue
        {
            var result = save.GetCustomValue<T>();
            if (result == null)
            {
                result = (T)Activator.CreateInstance(typeof(T));
                save.AddCustomValue(result);
            }
            return result;
        }
    }
}
