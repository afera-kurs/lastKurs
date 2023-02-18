using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ASMaIoP.Model;
using ASMaIoP.View;

namespace ASMaIoP.ViewModel
{
    public static class Guard
    {
        static private int accesLevel; 
        static public int AccesLevel
        {
            set
            {
                if(value > 5 && value < 0)
                {
                     accesLevel = 0;
                    DataLog.Log.AddLog(new Event("Ошибка попытка установить уровень доступа выше возможного", EventPriority.Error));
                }
                else
                {
                    accesLevel = value;
                }
            }

            get => accesLevel;
        }

        static public bool EmployeeEditPanel
        {
            get => AccesLevel >= 3;
        }

        static public bool EmployeeControlPanel
        {
            get => AccesLevel >= 2;
        }

        static public bool RoleControlsPanel
        {
            get => AccesLevel >= 2;
        }

        static public bool RoleEditPanel
        {
            get => AccesLevel >= 3;
        }

    }
}
