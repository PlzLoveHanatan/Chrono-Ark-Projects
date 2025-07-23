using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Urunhilda
{
    public static class Urunhilda_Visual
    {
        private static GameObject Lock;
        private static GameObject Lock2;
        private static GameObject Lock3;
        private static GameObject HeartPink;
        private static GameObject HeartPink2;
        private static GameObject HeartGrey;
        private static GameObject HeartGrey2;
        private static GameObject Whip;
        private static GameObject WhipGrey;

        public static int SavedStackNum;

        public static void BuffCheck(BattleChar bchar, int stackNum = 0)
        {
            SavedStackNum += stackNum;
            if (SavedStackNum >= 1)
            {
                // первый Lock -> HeartGrey
                if (HeartGrey == null)
                {
                    Utils.DestroyAndNullify(ref Lock);
                    HeartGrey = Utils.CreateIcon(bchar, "HeartGrey", "Ui/HeartGrey.png",
                        new Vector3(0.59f, 0.25f, 0f), new Vector3(75f, 75f));
                }
            }

            if (SavedStackNum >= 2)
            {
                // второй Lock -> HeartGrey2
                if (HeartGrey2 == null)
                {
                    Utils.DestroyAndNullify(ref Lock3);
                    HeartGrey2 = Utils.CreateIcon(bchar, "HeartGrey2", "Ui/HeartGrey2.png",
                        new Vector3(1.99f, 0.25f, 0f), new Vector3(75f, 75f));
                }
            }

            if (SavedStackNum >= 3)
            {
                // HeartGrey -> HeartPink
                if (HeartPink == null)
                {
                    Utils.DestroyAndNullify(ref HeartGrey);
                    HeartPink = Utils.CreateIcon(bchar, "HeartPink", "Ui/HeartPink.png",
                        new Vector3(0.59f, 0.25f, 0f), new Vector3(75f, 75f));
                }
            }

            if (SavedStackNum >= 4)
            {
                // HeartGrey2 -> HeartPink2
                if (HeartPink2 == null)
                {
                    Utils.DestroyAndNullify(ref HeartGrey2);
                    HeartPink2 = Utils.CreateIcon(bchar, "HeartPink2", "Ui/HeartPink2.png",
                        new Vector3(1.99f, 0.25f, 0f), new Vector3(75f, 75f));
                }
            }

            if (SavedStackNum >= 5)
            {
                // третий Lock -> Whip
                if (Whip == null)
                {
                    Utils.DestroyAndNullify(ref Lock2);
                    Whip = Utils.CreateIcon(bchar, "Whip", "Ui/Whip.png",
                        new Vector3(1.29f, 0.5f, 0f), new Vector3(75f, 75f));
                }
            }
        }


        public static void CreateLocks(BattleChar bchar)
        {
            if (Lock == null)
            {
                Lock = Utils.CreateIcon(bchar, "Lock", "Ui/Lock.png",
                    new Vector3(0.59f, 0.25f, 0f), new Vector3(75f, 75f));
            }
            if (Lock2 == null)
            {
                Lock2 = Utils.CreateIcon(bchar, "Lock2", "Ui/Lock.png",
                    new Vector3(1.29f, 0.50f, 0f), new Vector3(75f, 75f));
            }
            if (Lock3 == null)
            {
                Lock3 = Utils.CreateIcon(bchar, "Lock3", "Ui/Lock.png",
                    new Vector3(1.99f, 0.25f, 0f), new Vector3(75f, 75f));
            }
        }
    }
}
