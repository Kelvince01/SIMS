using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FIK.DAL
{
    public class SQL
    {
        private SqlConnection connection;

        public SQL(string connectionString) => this.connection = new SqlConnection(connectionString);

        public bool ExecuteQuery(List<string> SQL, ref string ErrorMsg)
        {
            ErrorMsg = "";
            bool flag = false;
            SqlTransaction sqlTransaction = (SqlTransaction)null;
            string str = "";
            try
            {
                if (this.connection != null)
                {
                    this.connection.Open();
                    sqlTransaction = this.connection.BeginTransaction();
                    foreach (string cmdText in SQL)
                    {
                        str = cmdText;
                        SqlCommand sqlCommand = new SqlCommand(cmdText, this.connection);
                        sqlCommand.CommandTimeout = 0;
                        sqlCommand.Transaction = sqlTransaction;
                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlTransaction.Commit();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                ErrorMsg = ex.Message + "\r\n" + str;
                sqlTransaction.Rollback();
            }
            finally
            {
                this.connection.Close();
            }
            return flag;
        }

        public bool Insert<T>(
          object dataObject,
          string specificProperty,
          string ExlcudeAutogeneratePrimaryKey,
          string customTable,
          ref string ErrorMsg)
        {
            bool flag1 = false;
            ErrorMsg = "";
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            if (!(dataObject is List<T> objList))
            {
                objList = new List<T>();
                T obj = (T)dataObject;
                if ((object)obj == null)
                {
                    bool flag2 = false;
                    ErrorMsg = "Invalid Object";
                    return flag2;
                }
                objList.Add(obj);
            }
            string str = objList[0].GetType().Name;
            if (!string.IsNullOrEmpty(customTable))
                str = customTable;
            string[] source = specificProperty.Split(',');
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            for (int index = 0; index < properties.Count; ++index)
            {
                PropertyDescriptor prop = properties[index];
                if (this.AllowedProperty(prop) && (string.IsNullOrEmpty(ExlcudeAutogeneratePrimaryKey) || !ExlcudeAutogeneratePrimaryKey.ToUpper().Contains(prop.Name.ToUpper())))
                {
                    if (!string.IsNullOrEmpty(specificProperty))
                    {
                        if (this.isExist(source, prop.Name))
                        {
                            stringBuilder1.Append("[" + prop.Name + "]");
                            stringBuilder1.Append(",");
                            stringBuilder2.Append("@" + prop.Name);
                            stringBuilder2.Append(",");
                        }
                    }
                    else
                    {
                        stringBuilder1.Append("[" + prop.Name + "]");
                        stringBuilder1.Append(",");
                        stringBuilder2.Append("@" + prop.Name);
                        stringBuilder2.Append(",");
                    }
                }
            }
            stringBuilder1.Remove(stringBuilder1.Length - 1, 1);
            stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
            string cmdText = string.Format("insert into [{0}] ( {1} ) values ({2}) ", (object)str, (object)stringBuilder1.ToString(), (object)stringBuilder2.ToString());
            SqlTransaction sqlTransaction = (SqlTransaction)null;
            try
            {
                if (this.connection != null)
                {
                    this.connection.Open();
                    sqlTransaction = this.connection.BeginTransaction();
                    foreach (T component in objList)
                    {
                        SqlCommand sqlCommand = new SqlCommand(cmdText, this.connection);
                        sqlCommand.CommandTimeout = 0;
                        for (int index = 0; index < properties.Count; ++index)
                        {
                            PropertyDescriptor prop = properties[index];
                            if (this.AllowedProperty(prop))
                            {
                                object obj = prop.GetValue((object)component) == null ? (object)DBNull.Value : prop.GetValue((object)component);
                                if (string.IsNullOrEmpty(ExlcudeAutogeneratePrimaryKey) || !ExlcudeAutogeneratePrimaryKey.ToUpper().Contains(prop.Name.ToUpper()))
                                {
                                    if (!string.IsNullOrEmpty(specificProperty))
                                    {
                                        if (this.isExist(source, prop.Name))
                                            sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                    }
                                    else
                                        sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                }
                            }
                        }
                        sqlCommand.Transaction = sqlTransaction;
                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlTransaction.Commit();
                    flag1 = true;
                }
            }
            catch (Exception ex)
            {
                flag1 = false;
                ErrorMsg = ex.Message + "\r\n" + cmdText;
                sqlTransaction?.Rollback();
            }
            finally
            {
                if (this.connection != null)
                    this.connection.Close();
            }
            return flag1;
        }

        public bool Delete<T>(
          object dataObject,
          string WhereClasseParameter,
          string customTable,
          ref string ErrorMsg)
        {
            bool flag1 = false;
            ErrorMsg = "";
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            if (!(dataObject is List<T> objList))
            {
                objList = new List<T>();
                T obj = (T)dataObject;
                if ((object)obj == null)
                {
                    bool flag2 = false;
                    ErrorMsg = "Invalid Object";
                    return flag2;
                }
                objList.Add(obj);
            }
            string str = objList[0].GetType().Name;
            if (!string.IsNullOrEmpty(customTable))
                str = customTable;
            string[] source = WhereClasseParameter.Split(',');
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < properties.Count; ++index)
            {
                PropertyDescriptor prop = properties[index];
                if (this.AllowedProperty(prop))
                {
                    if (!string.IsNullOrEmpty(WhereClasseParameter))
                    {
                        if (this.isExist(source, prop.Name))
                        {
                            stringBuilder.Append("[" + prop.Name + "]");
                            stringBuilder.Append("=");
                            stringBuilder.Append("@" + prop.Name);
                            stringBuilder.Append(" and ");
                        }
                    }
                    else
                    {
                        ErrorMsg = "Where parameter must be provided";
                        return false;
                    }
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 4, 4);
            string cmdText = string.Format("delete from [{0}]  where  ({1}) ", (object)str, (object)stringBuilder.ToString());
            SqlTransaction sqlTransaction = (SqlTransaction)null;
            try
            {
                if (this.connection != null)
                {
                    int num = 0;
                    this.connection.Open();
                    sqlTransaction = this.connection.BeginTransaction();
                    foreach (T component in objList)
                    {
                        SqlCommand sqlCommand = new SqlCommand(cmdText, this.connection);
                        sqlCommand.CommandTimeout = 0;
                        for (int index = 0; index < properties.Count; ++index)
                        {
                            PropertyDescriptor prop = properties[index];
                            if (this.AllowedProperty(prop))
                            {
                                object obj = prop.GetValue((object)component) == null ? (object)DBNull.Value : prop.GetValue((object)component);
                                if (this.isExist(source, prop.Name))
                                    sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                            }
                        }
                        sqlCommand.Transaction = sqlTransaction;
                        num = sqlCommand.ExecuteNonQuery();
                    }
                    sqlTransaction.Commit();
                    flag1 = true;
                    if (num <= 0)
                    {
                        flag1 = false;
                        ErrorMsg = "No record found for delete";
                    }
                }
            }
            catch (Exception ex)
            {
                flag1 = false;
                ErrorMsg = ex.Message + "\r\n" + cmdText;
                sqlTransaction?.Rollback();
            }
            finally
            {
                if (this.connection != null)
                    this.connection.Close();
            }
            return flag1;
        }

        public bool Update<T>(
          object dataObject,
          string specificProperty,
          string WhereClasseParameter,
          string customTable,
          ref string ErrorMsg)
        {
            bool flag1 = false;
            ErrorMsg = "";
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            if (!(dataObject is List<T> objList))
            {
                objList = new List<T>();
                T obj = (T)dataObject;
                if ((object)obj == null)
                {
                    bool flag2 = false;
                    ErrorMsg = "Invalid Object";
                    return flag2;
                }
                objList.Add(obj);
            }
            string str = objList[0].GetType().Name;
            if (!string.IsNullOrEmpty(customTable))
                str = customTable;
            string[] source1 = specificProperty.Split(',');
            string[] source2 = WhereClasseParameter.Split(',');
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            for (int index = 0; index < properties.Count; ++index)
            {
                PropertyDescriptor prop = properties[index];
                if (this.AllowedProperty(prop))
                {
                    string updateModifier = this.GetUpdateModifier(source1, prop.Name);
                    if (!this.isExist(source2, prop.Name))
                    {
                        if (!string.IsNullOrEmpty(specificProperty))
                        {
                            if (this.isExist(source1, prop.Name))
                            {
                                stringBuilder1.Append("[" + prop.Name + "]");
                                stringBuilder1.Append("=");
                                if (updateModifier.Contains("+"))
                                {
                                    stringBuilder1.Append("ISNULL(" + prop.Name + ",0)");
                                    stringBuilder1.Append("+");
                                }
                                else if (updateModifier.Contains("-"))
                                {
                                    stringBuilder1.Append("ISNULL(" + prop.Name + ",0)");
                                    stringBuilder1.Append("+");
                                }
                                stringBuilder1.Append("@" + prop.Name);
                                stringBuilder1.Append(",");
                            }
                        }
                        else
                        {
                            stringBuilder1.Append("[" + prop.Name + "]");
                            stringBuilder1.Append("=");
                            if (updateModifier.Contains("+"))
                            {
                                stringBuilder1.Append("ISNULL(" + prop.Name + ",0)");
                                stringBuilder1.Append("+");
                            }
                            else if (updateModifier.Contains("-"))
                            {
                                stringBuilder1.Append("ISNULL(" + prop.Name + ",0)");
                                stringBuilder1.Append("+");
                            }
                            stringBuilder1.Append("@" + prop.Name);
                            stringBuilder1.Append(",");
                        }
                    }
                    if (!string.IsNullOrEmpty(WhereClasseParameter))
                    {
                        if (this.isExist(source2, prop.Name))
                        {
                            stringBuilder2.Append("[" + prop.Name + "]");
                            stringBuilder2.Append("=");
                            stringBuilder2.Append("@" + prop.Name);
                            stringBuilder2.Append(" and ");
                        }
                    }
                    else
                    {
                        stringBuilder2.Append("[" + prop.Name + "]");
                        stringBuilder2.Append("=");
                        stringBuilder2.Append("@" + prop.Name);
                        stringBuilder2.Append(" and ");
                    }
                }
            }
            stringBuilder1.Remove(stringBuilder1.Length - 1, 1);
            stringBuilder2.Remove(stringBuilder2.Length - 4, 4);
            string cmdText = string.Format("update [{0}] set  {1}  where  ({2}) ", (object)str, (object)stringBuilder1.ToString(), (object)stringBuilder2.ToString());
            SqlTransaction sqlTransaction = (SqlTransaction)null;
            try
            {
                if (this.connection != null)
                {
                    int num = 0;
                    this.connection.Open();
                    sqlTransaction = this.connection.BeginTransaction();
                    foreach (T component in objList)
                    {
                        SqlCommand sqlCommand = new SqlCommand(cmdText, this.connection);
                        sqlCommand.CommandTimeout = 0;
                        for (int index = 0; index < properties.Count; ++index)
                        {
                            PropertyDescriptor prop = properties[index];
                            if (this.AllowedProperty(prop))
                            {
                                object obj = prop.GetValue((object)component) == null ? (object)DBNull.Value : prop.GetValue((object)component);
                                if (!this.isExist(source2, prop.Name))
                                {
                                    if (!string.IsNullOrEmpty(specificProperty))
                                    {
                                        if (this.isExist(source1, prop.Name))
                                            sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                    }
                                    else
                                        sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                }
                                if (!string.IsNullOrEmpty(WhereClasseParameter))
                                {
                                    if (this.isExist(source2, prop.Name))
                                        sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                }
                                else
                                    sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                            }
                        }
                        sqlCommand.Transaction = sqlTransaction;
                        num = sqlCommand.ExecuteNonQuery();
                    }
                    sqlTransaction.Commit();
                    flag1 = true;
                    if (num <= 0)
                    {
                        flag1 = false;
                        ErrorMsg = "No record found for update";
                    }
                }
            }
            catch (Exception ex)
            {
                flag1 = false;
                ErrorMsg = ex.Message + "\r\n" + cmdText;
                sqlTransaction?.Rollback();
            }
            finally
            {
                if (this.connection != null)
                    this.connection.Close();
            }
            return flag1;
        }

        public bool InsertUpdateComposite(CompositeModel dataObject, ref string ErrorMsg)
        {
            bool flag = false;
            ErrorMsg = "";
            List<string> stringList = new List<string>();
            foreach (CompositeModel record in dataObject.GetRecordSet())
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(record.ObjectType);
                if (record.Model.Count == 0)
                {
                    ErrorMsg = "No object pass to operation";
                    return false;
                }
                string objectName = record.ObjectName;
                StringBuilder stringBuilder1 = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();
                StringBuilder stringBuilder3 = new StringBuilder();
                StringBuilder stringBuilder4 = new StringBuilder();
                string[] source1 = record.SlectiveProperty.Split(',');
                if (string.IsNullOrEmpty(record.SlectivePropertyUpdate))
                    record.SlectivePropertyUpdate = "";
                string[] source2 = record.SlectivePropertyUpdate.Split(',');
                string[] source3 = record.WhereClauseParamForUpdateDelete.Split(',');
                string str1 = "";
                if (record.OperationMode == OperationMode.Update)
                {
                    for (int index = 0; index < properties.Count; ++index)
                    {
                        PropertyDescriptor prop = properties[index];
                        if (this.AllowedProperty(prop))
                        {
                            if (!this.isExist(source3, prop.Name))
                            {
                                string updateModifier = this.GetUpdateModifier(source1, prop.Name);
                                if (!string.IsNullOrEmpty(record.SlectiveProperty))
                                {
                                    if (this.isExist(source1, prop.Name))
                                    {
                                        stringBuilder1.Append("[" + prop.Name + "]");
                                        stringBuilder1.Append("=");
                                        if (updateModifier.Contains("+"))
                                        {
                                            stringBuilder4.Append("ISNULL(" + prop.Name + ",0)");
                                            stringBuilder4.Append("+");
                                        }
                                        else if (updateModifier.Contains("-"))
                                        {
                                            stringBuilder4.Append("ISNULL(" + prop.Name + ",0)");
                                            stringBuilder4.Append("+");
                                        }
                                        stringBuilder1.Append("@" + prop.Name);
                                        stringBuilder1.Append(",");
                                    }
                                }
                                else
                                {
                                    stringBuilder1.Append("[" + prop.Name + "]");
                                    stringBuilder1.Append("=");
                                    if (updateModifier.Contains("+"))
                                    {
                                        stringBuilder4.Append(prop.Name);
                                        stringBuilder4.Append("+");
                                    }
                                    else if (updateModifier.Contains("-"))
                                    {
                                        stringBuilder4.Append(prop.Name);
                                        stringBuilder4.Append("+");
                                    }
                                    stringBuilder1.Append("@" + prop.Name);
                                    stringBuilder1.Append(",");
                                }
                            }
                            if (!string.IsNullOrEmpty(record.WhereClauseParamForUpdateDelete))
                            {
                                if (this.isExist(source3, prop.Name))
                                {
                                    stringBuilder2.Append("[" + prop.Name + "]");
                                    stringBuilder2.Append("=");
                                    stringBuilder2.Append("@" + prop.Name);
                                    stringBuilder2.Append(" and ");
                                }
                            }
                            else
                            {
                                stringBuilder2.Append("[" + prop.Name + "]");
                                stringBuilder2.Append("=");
                                stringBuilder2.Append("@" + prop.Name);
                                stringBuilder2.Append(" and ");
                            }
                        }
                    }
                    stringBuilder1.Remove(stringBuilder1.Length - 1, 1);
                    stringBuilder2.Remove(stringBuilder2.Length - 4, 4);
                    str1 = string.Format("update [{0}] set  {1}  where  ({2}) ", (object)objectName, (object)stringBuilder1.ToString(), (object)stringBuilder2.ToString());
                    stringList.Add(str1);
                }
                if (record.OperationMode == OperationMode.Delete)
                {
                    for (int index = 0; index < properties.Count; ++index)
                    {
                        PropertyDescriptor propertyDescriptor = properties[index];
                        if (string.IsNullOrEmpty(record.WhereClauseParamForUpdateDelete))
                        {
                            ErrorMsg = "Where clause not define for delete";
                            return false;
                        }
                        if (this.isExist(source3, propertyDescriptor.Name))
                        {
                            stringBuilder2.Append("[" + propertyDescriptor.Name + "]");
                            stringBuilder2.Append("=");
                            stringBuilder2.Append("@" + propertyDescriptor.Name);
                            stringBuilder2.Append(" and ");
                        }
                    }
                    stringBuilder2.Remove(stringBuilder2.Length - 4, 4);
                    str1 = string.Format("DELETE from [{0}]  where  ({1}) ", (object)objectName, (object)stringBuilder2.ToString());
                    stringList.Add(str1);
                }
                else if (record.OperationMode == OperationMode.Insert)
                {
                    for (int index = 0; index < properties.Count; ++index)
                    {
                        PropertyDescriptor prop = properties[index];
                        if (this.AllowedProperty(prop) && (string.IsNullOrEmpty(record.ExlcudeAutogeneratePrimaryKey) || !record.ExlcudeAutogeneratePrimaryKey.ToUpper().Contains(prop.Name.ToUpper())))
                        {
                            if (!string.IsNullOrEmpty(record.SlectiveProperty))
                            {
                                if (this.isExist(source1, prop.Name))
                                {
                                    stringBuilder3.Append("[" + prop.Name + "]");
                                    stringBuilder3.Append(",");
                                    stringBuilder1.Append("@" + prop.Name);
                                    stringBuilder1.Append(",");
                                }
                            }
                            else
                            {
                                stringBuilder3.Append("[" + prop.Name + "]");
                                stringBuilder3.Append(",");
                                stringBuilder1.Append("@" + prop.Name);
                                stringBuilder1.Append(",");
                            }
                        }
                    }
                    stringBuilder3.Remove(stringBuilder3.Length - 1, 1);
                    stringBuilder1.Remove(stringBuilder1.Length - 1, 1);
                    str1 = string.Format("insert into [{0}] ( {1} ) values ({2}) ", (object)objectName, (object)stringBuilder3.ToString(), (object)stringBuilder1.ToString());
                    stringList.Add(str1);
                }
                else if (record.OperationMode == OperationMode.InsertOrUpdaet)
                {
                    if (string.IsNullOrEmpty(record.SlectivePropertyUpdate))
                    {
                        for (int index = 0; index < properties.Count; ++index)
                        {
                            PropertyDescriptor prop = properties[index];
                            if (this.AllowedProperty(prop) && (string.IsNullOrEmpty(record.ExlcudeAutogeneratePrimaryKey) || !record.ExlcudeAutogeneratePrimaryKey.ToUpper().Contains(prop.Name.ToUpper())))
                            {
                                if (!string.IsNullOrEmpty(record.SlectiveProperty))
                                {
                                    if (this.isExist(source1, prop.Name))
                                    {
                                        stringBuilder3.Append("[" + prop.Name + "]");
                                        stringBuilder3.Append(",");
                                        stringBuilder1.Append("@" + prop.Name);
                                        stringBuilder1.Append(",");
                                    }
                                }
                                else
                                {
                                    stringBuilder3.Append("[" + prop.Name + "]");
                                    stringBuilder3.Append(",");
                                    stringBuilder1.Append("@" + prop.Name);
                                    stringBuilder1.Append(",");
                                }
                                if (!this.isExist(source3, prop.Name))
                                {
                                    if (!string.IsNullOrEmpty(record.SlectiveProperty))
                                    {
                                        if (this.isExist(source1, prop.Name))
                                        {
                                            string updateModifier = this.GetUpdateModifier(source1, prop.Name);
                                            stringBuilder4.Append("[" + prop.Name + "]");
                                            stringBuilder4.Append("=");
                                            if (updateModifier.Contains("+"))
                                            {
                                                stringBuilder4.Append("ISNULL(" + prop.Name + ",0)");
                                                stringBuilder4.Append("+");
                                            }
                                            else if (updateModifier.Contains("-"))
                                            {
                                                stringBuilder4.Append("ISNULL(" + prop.Name + ",0)");
                                                stringBuilder4.Append("+");
                                            }
                                            stringBuilder4.Append("@" + prop.Name);
                                            stringBuilder4.Append(",");
                                        }
                                    }
                                    else
                                    {
                                        stringBuilder4.Append("[" + prop.Name + "]");
                                        stringBuilder4.Append("=");
                                        stringBuilder4.Append("@" + prop.Name);
                                        stringBuilder4.Append(",");
                                    }
                                }
                                if (!string.IsNullOrEmpty(record.WhereClauseParamForUpdateDelete))
                                {
                                    if (this.isExist(source3, prop.Name))
                                    {
                                        stringBuilder2.Append("[" + prop.Name + "]");
                                        stringBuilder2.Append("=");
                                        stringBuilder2.Append("@" + prop.Name);
                                        stringBuilder2.Append(" and ");
                                    }
                                }
                                else
                                {
                                    stringBuilder2.Append("[" + prop.Name + "]");
                                    stringBuilder2.Append("=");
                                    stringBuilder2.Append("@" + prop.Name);
                                    stringBuilder2.Append(" and ");
                                }
                            }
                        }
                        stringBuilder4.Remove(stringBuilder4.Length - 1, 1);
                        stringBuilder2.Remove(stringBuilder2.Length - 4, 4);
                        stringBuilder3.Remove(stringBuilder3.Length - 1, 1);
                        stringBuilder1.Remove(stringBuilder1.Length - 1, 1);
                    }
                    else
                    {
                        for (int index = 0; index < properties.Count; ++index)
                        {
                            PropertyDescriptor prop = properties[index];
                            if (this.AllowedProperty(prop) && (string.IsNullOrEmpty(record.ExlcudeAutogeneratePrimaryKey) || !record.ExlcudeAutogeneratePrimaryKey.ToUpper().Contains(prop.Name.ToUpper())))
                            {
                                if (!string.IsNullOrEmpty(record.SlectiveProperty))
                                {
                                    if (this.isExist(source1, prop.Name))
                                    {
                                        stringBuilder3.Append("[" + prop.Name + "]");
                                        stringBuilder3.Append(",");
                                        stringBuilder1.Append("@" + prop.Name);
                                        stringBuilder1.Append(",");
                                    }
                                }
                                else
                                {
                                    stringBuilder3.Append("[" + prop.Name + "]");
                                    stringBuilder3.Append(",");
                                    stringBuilder1.Append("@" + prop.Name);
                                    stringBuilder1.Append(",");
                                }
                                if (!this.isExist(source3, prop.Name))
                                {
                                    if (!string.IsNullOrEmpty(record.SlectivePropertyUpdate))
                                    {
                                        if (this.isExist(source2, prop.Name))
                                        {
                                            string updateModifier = this.GetUpdateModifier(source2, prop.Name);
                                            stringBuilder4.Append("[" + prop.Name + "]");
                                            stringBuilder4.Append("=");
                                            if (updateModifier.Contains("+"))
                                            {
                                                stringBuilder4.Append("ISNULL(" + prop.Name + ",0)");
                                                stringBuilder4.Append("+");
                                            }
                                            else if (updateModifier.Contains("-"))
                                            {
                                                stringBuilder4.Append("ISNULL(" + prop.Name + ",0)");
                                                stringBuilder4.Append("+");
                                            }
                                            stringBuilder4.Append("@" + prop.Name);
                                            stringBuilder4.Append(",");
                                        }
                                    }
                                    else
                                    {
                                        stringBuilder4.Append("[" + prop.Name + "]");
                                        stringBuilder4.Append("=");
                                        stringBuilder4.Append("@" + prop.Name);
                                        stringBuilder4.Append(",");
                                    }
                                }
                                if (!string.IsNullOrEmpty(record.WhereClauseParamForUpdateDelete))
                                {
                                    if (this.isExist(source3, prop.Name))
                                    {
                                        stringBuilder2.Append("[" + prop.Name + "]");
                                        stringBuilder2.Append("=");
                                        stringBuilder2.Append("@" + prop.Name);
                                        stringBuilder2.Append(" and ");
                                    }
                                }
                                else
                                {
                                    stringBuilder2.Append("[" + prop.Name + "]");
                                    stringBuilder2.Append("=");
                                    stringBuilder2.Append("@" + prop.Name);
                                    stringBuilder2.Append(" and ");
                                }
                            }
                        }
                        stringBuilder4.Remove(stringBuilder4.Length - 1, 1);
                        stringBuilder2.Remove(stringBuilder2.Length - 4, 4);
                        stringBuilder3.Remove(stringBuilder3.Length - 1, 1);
                        stringBuilder1.Remove(stringBuilder1.Length - 1, 1);
                    }
                    string str2 = string.Format("update [{0}] set  {1}  where  ({2}) ", (object)objectName, (object)stringBuilder4.ToString(), (object)stringBuilder2.ToString());
                    string str3 = string.Format("insert into [{0}] ( {1} ) values ({2}) ", (object)objectName, (object)stringBuilder3.ToString(), (object)stringBuilder1.ToString());
                    str1 = string.Format("\n                                        if exists(select * from [{0}] where {1} )\n                                        begin\n                                         {2}\n                                        end\n                                        else\n                                        begin\n                                           {3}\n                                        end ", (object)objectName, (object)stringBuilder2.ToString(), (object)str2, (object)str3);
                    stringList.Add(str1);
                }
                if (string.IsNullOrEmpty(str1))
                {
                    ErrorMsg = "Query Parse failed";
                    return false;
                }
            }
            SqlTransaction sqlTransaction = (SqlTransaction)null;
            string str = "";
            try
            {
                if (this.connection != null)
                {
                    int num = 0;
                    this.connection.Open();
                    sqlTransaction = this.connection.BeginTransaction();
                    int index1 = 0;
                    foreach (CompositeModel record in dataObject.GetRecordSet())
                    {
                        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(record.ObjectType);
                        string[] source4 = record.SlectiveProperty.Split(',');
                        string[] source5 = record.SlectivePropertyUpdate.Split(',');
                        string[] source6 = record.WhereClauseParamForUpdateDelete.Split(',');
                        foreach (object component in record.Model)
                        {
                            SqlCommand sqlCommand = new SqlCommand(stringList[index1], this.connection);
                            sqlCommand.CommandTimeout = 0;
                            str = stringList[index1];
                            for (int index2 = 0; index2 < properties.Count; ++index2)
                            {
                                PropertyDescriptor prop = properties[index2];
                                if (this.AllowedProperty(prop))
                                {
                                    object obj = prop.GetValue(component) == null ? (object)DBNull.Value : prop.GetValue(component);
                                    if (record.OperationMode == OperationMode.Delete)
                                    {
                                        if (this.isExist(source6, prop.Name))
                                            sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                    }
                                    else if (string.IsNullOrEmpty(record.SlectivePropertyUpdate))
                                    {
                                        if (!string.IsNullOrEmpty(record.SlectiveProperty))
                                        {
                                            if (string.IsNullOrEmpty(record.ExlcudeAutogeneratePrimaryKey) || !record.ExlcudeAutogeneratePrimaryKey.ToUpper().Contains(prop.Name.ToUpper()))
                                            {
                                                if (this.isExist(source4, prop.Name))
                                                    sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                                else if (this.isExist(source6, prop.Name))
                                                    sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                            }
                                        }
                                        else if (string.IsNullOrEmpty(record.ExlcudeAutogeneratePrimaryKey) || !record.ExlcudeAutogeneratePrimaryKey.ToUpper().Contains(prop.Name.ToUpper()))
                                            sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                    }
                                    else if (!string.IsNullOrEmpty(record.SlectiveProperty) || !string.IsNullOrEmpty(record.SlectivePropertyUpdate))
                                    {
                                        if (string.IsNullOrEmpty(record.ExlcudeAutogeneratePrimaryKey) || !record.ExlcudeAutogeneratePrimaryKey.ToUpper().Contains(prop.Name.ToUpper()))
                                        {
                                            if (this.isExist(source4, prop.Name) && sqlCommand.Parameters.IndexOf("@" + prop.Name) < 0)
                                                sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                            else if (this.isExist(source5, prop.Name) && sqlCommand.Parameters.IndexOf("@" + prop.Name) < 0)
                                                sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                            else if (this.isExist(source6, prop.Name) && sqlCommand.Parameters.IndexOf("@" + prop.Name) < 0)
                                                sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                            else if (string.IsNullOrEmpty(record.SlectiveProperty) || string.IsNullOrEmpty(record.SlectivePropertyUpdate))
                                                sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                        }
                                    }
                                    else if (string.IsNullOrEmpty(record.ExlcudeAutogeneratePrimaryKey) || !record.ExlcudeAutogeneratePrimaryKey.ToUpper().Contains(prop.Name.ToUpper()))
                                        sqlCommand.Parameters.AddWithValue("@" + prop.Name, obj);
                                }
                            }
                            sqlCommand.Transaction = sqlTransaction;
                            num = sqlCommand.ExecuteNonQuery();
                            if (num == 0)
                                throw new Exception("No record affected for " + record.OperationMode.ToString() + "\r\n Query:" + str);
                        }
                        ++index1;
                    }
                    sqlTransaction.Commit();
                    flag = true;
                    if (num <= 0)
                    {
                        flag = false;
                        ErrorMsg = "No record found for update";
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
                ErrorMsg = ex.Message + "\r\n" + str;
                sqlTransaction?.Rollback();
            }
            finally
            {
                if (this.connection != null)
                    this.connection.Close();
            }
            return flag;
        }

        private bool AllowedProperty(PropertyDescriptor prop)
        {
            bool flag = false;
            if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?) || prop.PropertyType == typeof(Decimal) || prop.PropertyType == typeof(Decimal?) || prop.PropertyType == typeof(float) || prop.PropertyType == typeof(float?) || prop.PropertyType == typeof(string) || prop.PropertyType == typeof(string) || prop.PropertyType == typeof(byte[]) || prop.PropertyType == typeof(byte) || prop.PropertyType == typeof(byte?) || prop.PropertyType == typeof(bool?) || prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(byte) || prop.PropertyType == typeof(byte?) || prop.PropertyType == typeof(char) || prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(TimeSpan?) || prop.PropertyType == typeof(TimeSpan))
                flag = true;
            foreach (object attribute in prop.Attributes)
            {
                if (attribute.GetType() == typeof(FIK_NoCUDAttribute))
                    flag = false;
            }
            return flag;
        }

        private bool isExist(string[] source, string target)
        {
            target = target.ToUpper();
            string[] source1 = new string[source.Length];
            for (int index = 0; index < source.Length; ++index)
            {
                int num = source[index].StartsWith("+") ? 1 : (source[index].StartsWith("-") ? 1 : 0);
                source1[index] = num == 0 ? source[index] : source[index].Substring(1, source[index].Length - 1);
            }
            return !string.IsNullOrEmpty(((IEnumerable<string>)source1).FirstOrDefault<string>((Func<string, bool>)(s => s.ToUpper() == target)));
        }

        private string GetUpdateModifier(string[] source, string target)
        {
            target = target.ToUpper();
            for (int index = 0; index < source.Length; ++index)
            {
                if ((source[index].StartsWith("+") || source[index].StartsWith("-")) && source[index].Substring(1, source[index].Length - 1).ToUpper() == target.ToUpper())
                    return source[index].Substring(0, 1);
            }
            return "";
        }

        public DataTable Select(string SQL, ref string msg)
        {
            DataTable dataTable1 = (DataTable)null;
            try
            {
                if (this.connection != null)
                {
                    this.connection.Open();
                    SqlCommand selectCommand = new SqlCommand(SQL, this.connection);
                    selectCommand.CommandTimeout = 0;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                    DataTable dataTable2 = new DataTable();
                    sqlDataAdapter.Fill(dataTable2);
                    dataTable1 = dataTable2;
                }
            }
            catch (Exception ex)
            {
                dataTable1 = (DataTable)null;
                msg = ex.Message;
            }
            finally
            {
                this.connection.Close();
            }
            return dataTable1;
        }

        public List<T> Select<T>(string SQL, ref string msg) where T : class, new()
        {
            try
            {
                DataTable dataTable = this.Select(SQL, ref msg);
                if (dataTable == null)
                    return (List<T>)null;
                List<T> objList = new List<T>();
                for (int index = 0; index < dataTable.Rows.Count; ++index)
                {
                    DataRow row = dataTable.Rows[index];
                    T obj1 = new T();
                    foreach (object column in (InternalDataCollectionBase)row.Table.Columns)
                    {
                        string str = column.ToString();
                        PropertyInfo property = obj1.GetType().GetProperty(str);
                        if (property != (PropertyInfo)null)
                        {
                            try
                            {
                                object obj2 = row[str];
                                if (obj2 == DBNull.Value)
                                    obj2 = (object)null;
                                property.SetValue((object)obj1, obj2, (object[])null);
                            }
                            catch
                            {
                            }
                        }
                    }
                    objList.Add(obj1);
                }
                return objList;
            }
            catch
            {
                return (List<T>)null;
            }
        }

        public T SelectFirstOrDefault<T>(string SQL, ref string msg) where T : class, new()
        {
            try
            {
                DataTable dataTable = this.Select(SQL, ref msg);
                if (dataTable == null || dataTable.Rows.Count == 0)
                    return default(T);
                DataRow row = dataTable.Rows[0];
                T obj1 = new T();
                foreach (object column in (InternalDataCollectionBase)row.Table.Columns)
                {
                    string str = column.ToString();
                    PropertyInfo property = obj1.GetType().GetProperty(str);
                    if (property != (PropertyInfo)null)
                    {
                        try
                        {
                            object obj2 = row[str];
                            if (obj2 == DBNull.Value)
                                obj2 = (object)null;
                            property.SetValue((object)obj1, obj2, (object[])null);
                        }
                        catch
                        {
                        }
                    }
                }
                return obj1;
            }
            catch
            {
                return default(T);
            }
        }

        public string GetMaxId(
          string coloumName,
          string rightStringLength,
          string tableName,
          string prefix)
        {
            string SQL = "SELECT ISNULL(MAX( CAST(SUBSTRING( " + coloumName + " ," + (object)(prefix.Length + 1) + ", LEN(" + coloumName + ") ) AS INT) ),0)\n                            FROM " + tableName + "\n                            WHERE " + coloumName + " LIKE '" + prefix + "%' ";
            string msg = "";
            DataTable dataTable = this.Select(SQL, ref msg);
            if (dataTable == null || dataTable.Rows.Count == 0)
                throw new Exception("Max generation fail, no record ,table or " + msg);
            string s = dataTable.Rows[0][0].ToString();
            if (string.IsNullOrEmpty(s))
                return prefix + Decimal.Parse("1").ToString(rightStringLength);
            Decimal num = Decimal.Parse(s) + 1M;
            return prefix + num.ToString(rightStringLength);
        }
    }
}
