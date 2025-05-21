using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raphi
{
    public class ItemRaphi : CustomValue
    {
        public List<string> UsedList;

        public static ItemRaphi Instance
        {
            get
            {
                if (PlayData.TSavedata == null)
                {
                    return new ItemRaphi();
                }
                return GetOrAddCustomValue<ItemRaphi>(PlayData.TSavedata);
            }
        }
        public ItemRaphi()
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
