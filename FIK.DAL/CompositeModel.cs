using System;
using System.Collections.Generic;

namespace FIK.DAL
{
    public class CompositeModel
    {
        private List<CompositeModel> list = new List<CompositeModel>();

        public List<object> Model { get; private set; }

        public OperationMode OperationMode { get; private set; }

        public string ExlcudeAutogeneratePrimaryKey { get; private set; }

        public string SlectiveProperty { get; private set; }

        public string SlectivePropertyUpdate { get; set; }

        public string WhereClauseParamForUpdateDelete { get; private set; }

        public Type ObjectType { get; private set; }

        public string ObjectName { get; private set; }

        public bool AddRecordSet<T>(
          object model,
          OperationMode operationMode,
          string exlcudeAutogeneratePrimaryKey,
          string slectiveProperty,
          string whereClauseParamForUpdate,
          string customeTable)
        {
            try
            {
                if (!(model is List<T> objList))
                {
                    objList = new List<T>();
                    objList.Add((T)model ?? throw new Exception("Invalid Object Type"));
                }
                List<object> objectList = new List<object>();
                foreach (T obj in objList)
                    objectList.Add((object)obj);
                string str = objList[0].GetType().Name;
                if (!string.IsNullOrEmpty(customeTable))
                    str = customeTable;
                this.list.Add(new CompositeModel()
                {
                    Model = objectList,
                    ObjectName = str,
                    ObjectType = typeof(T),
                    OperationMode = operationMode,
                    ExlcudeAutogeneratePrimaryKey = exlcudeAutogeneratePrimaryKey,
                    SlectiveProperty = slectiveProperty,
                    WhereClauseParamForUpdateDelete = whereClauseParamForUpdate
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddRecordSet<T>(
          object model,
          OperationMode operationMode,
          string exlcudeAutogeneratePrimaryKey,
          string slectivePropertyInsert,
          string slectivePropertyUpdate,
          string whereClauseParamForUpdate,
          string customeTable)
        {
            try
            {
                if (!(model is List<T> objList))
                {
                    objList = new List<T>();
                    objList.Add((T)model ?? throw new Exception("Invalid Object Type"));
                }
                List<object> objectList = new List<object>();
                foreach (T obj in objList)
                    objectList.Add((object)obj);
                string str = objList[0].GetType().Name;
                if (!string.IsNullOrEmpty(customeTable))
                    str = customeTable;
                this.list.Add(new CompositeModel()
                {
                    Model = objectList,
                    ObjectName = str,
                    ObjectType = typeof(T),
                    OperationMode = operationMode,
                    ExlcudeAutogeneratePrimaryKey = exlcudeAutogeneratePrimaryKey,
                    SlectiveProperty = slectivePropertyInsert,
                    SlectivePropertyUpdate = slectivePropertyUpdate,
                    WhereClauseParamForUpdateDelete = whereClauseParamForUpdate
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<CompositeModel> GetRecordSet() => this.list;
    }
}
