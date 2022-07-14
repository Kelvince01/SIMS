using System;
using System.Data;

namespace SIMS.Extensions
{
    public static class DataTableExt
    {
        public static void ConvertColumnType(this DataTable dt, string columnName, Type newType)
        {
            using (DataColumn column = new DataColumn(columnName + "_new", newType))
            {
                int ordinal = dt.Columns[columnName].Ordinal;
                dt.Columns.Add(column);
                column.SetOrdinal(ordinal);
                foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
                    row[column.ColumnName] = Convert.ChangeType(row[columnName], newType);
                dt.Columns.Remove(columnName);
                column.ColumnName = columnName;
            }
        }
    }
}
