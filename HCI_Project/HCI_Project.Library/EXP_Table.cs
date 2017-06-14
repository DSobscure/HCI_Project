using System.Collections.Generic;

namespace HCI_Project.Library
{
    public static class EXP_Table
    {
        private static List<int> expTable = new List<int>();
        static EXP_Table()
        {
            for(int i = 0; i < 25; i++)
            {
                expTable.Add(20 * i * i + 100 * i + 100);
            }
        }
        public static int EXP(int level)
        {
            if (level >= expTable.Count)
            {
                return -1;
            }
            else
            {
                return expTable[level - 1];
            }
        }
    }
}
