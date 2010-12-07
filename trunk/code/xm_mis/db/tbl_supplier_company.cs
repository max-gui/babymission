using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
namespace xm_mis.db
{
    public class tbl_supplier_company : DataBase
    {
        public tbl_supplier_company()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //supplierName
            SqlParameter sqlParaSupplierName = null;
            //supplierAddr
            SqlParameter sqlParaSupplierAddr = null;
            //newSupplierId
            SqlParameter sqlParaId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_supplier_company_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string sn = dataSet.Tables["tbl_supplier_company"].Rows[0]["supplierName"].ToString().Trim();
            string sa = dataSet.Tables["tbl_supplier_company"].Rows[0]["supplierAddress"].ToString().Trim();

            sqlParaSupplierName = new SqlParameter("@supplierName", sn);
            sqlParaSupplierAddr = new SqlParameter("@supplierAddress", sa);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaSupplierName);
            sqlCmd.Parameters.Add(sqlParaSupplierAddr);
            sqlCmd.Parameters.Add(sqlParaId);
            #endregion

            #region sqlDirection
            sqlParaId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string supplierId = sqlParaId.Value.ToString();
            return supplierId;
        }

        public void SupplierUpdate(int supplierId, string supplierName, string supplierAddr)
        {
            #region sqlPara declare
            //supplierId
            SqlParameter sqlParaSupplierId = null;
            //supplierName
            SqlParameter sqlParaSupplierName = null;
            //supplierAddr
            SqlParameter sqlParaSupplierAddr = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_supplier_company_update";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit

            sqlParaSupplierId = new SqlParameter("@supplierId", supplierId);
            sqlParaSupplierName = new SqlParameter("@newSupplierName", supplierName);
            sqlParaSupplierAddr = new SqlParameter("@newSupplierAddress", supplierAddr);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaSupplierId);
            sqlCmd.Parameters.Add(sqlParaSupplierName);
            sqlCmd.Parameters.Add(sqlParaSupplierAddr);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public void SupplierDel(string supplierId)
        {
            #region sqlPara declare
            //supplierId
            SqlParameter sqlParaSupplierId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_supplier_company_delete";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int supplierIdTemp = int.Parse(supplierId);

            sqlParaSupplierId = new SqlParameter("@delSupplierId", supplierIdTemp);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaSupplierId);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public DataSet SelectView()
        {
            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM tbl_supplier_company ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "tbl_supplier_company");

            return myDataSet;
        }
    }
}