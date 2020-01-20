using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace VaultLifeAdmin.Helpers
{
    public static class TableTracker
    {
        public static object TrackCreate(object Table, string UserName)
        {
            Type tableType = Table.GetType();
            //MethodInfo method = tableClass.GetMethod("FooHasAMethod");
            PropertyInfo dateInserted = tableType.GetProperty("DateInserted");
            dateInserted.SetValue(Table, DateTime.Now, null);
            PropertyInfo dateUpdated = tableType.GetProperty("DateUpdated");
            dateUpdated.SetValue(Table, DateTime.Now, null);
            PropertyInfo USR = tableType.GetProperty("USR");
            USR.SetValue(Table, UserName, null);

            return Table;
        
        }

        public static object TrackEdit(object Table, string UserName)
        {
            Type tableType = Table.GetType();
            
            PropertyInfo dateUpdated = tableType.GetProperty("DateUpdated");
            dateUpdated.SetValue(Table, DateTime.Now, null);
            PropertyInfo USR = tableType.GetProperty("USR");
            USR.SetValue(Table, UserName, null);

            return Table;

        }

        public static object TrackCreate(object Table, string UserName, bool IsVoucherOrSerial)
        {
            Type tableType = Table.GetType();
            //MethodInfo method = tableClass.GetMethod("FooHasAMethod");
            PropertyInfo dateInserted = tableType.GetProperty("DateInserted");
            dateInserted.SetValue(Table, DateTime.Now, null);
            PropertyInfo dateUsed = tableType.GetProperty("DateUsed");
            dateUsed.SetValue(Table, null, null);
            PropertyInfo used = tableType.GetProperty("Used");
            used.SetValue(Table, false, null);

            return Table;

        }

        public static object TrackEdit(object Table, string UserName, bool IsVoucherOrSerial)
        {
            Type tableType = Table.GetType();

            PropertyInfo dateUpdated = tableType.GetProperty("DateUpdated");
            dateUpdated.SetValue(Table, DateTime.Now, null);
            PropertyInfo used = tableType.GetProperty("Used");
            used.SetValue(Table, false, null);

            return Table;

        }
    }
}