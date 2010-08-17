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
    public class tbl_supplier_manager : DataBase
    {
        public tbl_supplier_manager()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //supplierManName
            SqlParameter sqlParaSupplierManName = null;
            //supplierManContact
            SqlParameter sqlParaSupplierManContact = null;
            //supplierManEmail
            SqlParameter sqlParaSupplierManEmail = null;
            //supplierManDepart
            SqlParameter sqlParaSupplierManDepart = null;
            //supplierManTitle
            SqlParameter sqlParaSupplierManTitle = null;
            //supplierCompyId
            SqlParameter sqlParaSupplierId = null;
            //start
            SqlParameter sqlParaSt = null;
            //newCompId
            SqlParameter sqlParaId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_supplier_manager_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string supplierManName = dataSet.Tables["tbl_customer_manager"].Rows[0]["supplierManName"].ToString();
            string supplierManContact = dataSet.Tables["tbl_customer_manager"].Rows[0]["supplierManContact"].ToString();
            string supplierManEmail = dataSet.Tables["tbl_customer_manager"].Rows[0]["supplierManEmail"].ToString();
            string supplierManDepart = dataSet.Tables["tbl_customer_manager"].Rows[0]["supplierManDepart"].ToString();
            string supplierManTitle = dataSet.Tables["tbl_customer_manager"].Rows[0]["supplierManTitle"].ToString();
            int supplierId = int.Parse(dataSet.Tables["tbl_customer_manager"].Rows[0]["supplierId"].ToString());
            DateTime st = DateTime.Now;

            sqlParaSupplierManName = new SqlParameter("@supplierManName", supplierManName);
            sqlParaSupplierManContact = new SqlParameter("@supplierManContact", supplierManContact);
            sqlParaSupplierManEmail = new SqlParameter("@supplierManEmail", supplierManEmail);
            sqlParaSupplierManDepart = new SqlParameter("@supplierManDepart", supplierManDepart);
            sqlParaSupplierManTitle = new SqlParameter("@supplierManTitle", supplierManTitle);
            sqlParaSupplierId = new SqlParameter("@supplierId", supplierId);
            sqlParaSt = new SqlParameter("@startTime", st);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaSupplierManName);
            sqlCmd.Parameters.Add(sqlParaSupplierManContact);
            sqlCmd.Parameters.Add(sqlParaSupplierManEmail);
            sqlCmd.Parameters.Add(sqlParaSupplierManDepart);
            sqlCmd.Parameters.Add(sqlParaSupplierManTitle);
            sqlCmd.Parameters.Add(sqlParaSupplierId);
            sqlCmd.Parameters.Add(sqlParaSt);
            sqlCmd.Parameters.Add(sqlParaId);
            #endregion

            #region sqlDirection
            sqlParaId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string supplierManId = sqlParaId.Value.ToString();
            return supplierManId;
        }

        public void SupplierManUpdate(int supplierManId, string supplierManName, string supplierManCont, string supplierManEmail, string supplierManDep, string supplierManTitle)
        {
            #region sqlPara declare
            //supplierManTitle
            SqlParameter sqlParaSupplierManId = null;
            //supplierManName
            SqlParameter sqlParaSupplierManName = null;
            //supplierManCont
            SqlParameter sqlParaSupplierManCont = null;
            //supplierManEmail
            SqlParameter sqlParaSupplierManEmail = null;
            //supplierManDep
            SqlParameter sqlParaSupplierManDepart = null;
            //supplierManTitle
            SqlParameter sqlParaSupplierManTitle = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_supplier_manager_update";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit

            sqlParaSupplierManId = new SqlParameter("@supplierManId", supplierManId);
            sqlParaSupplierManName = new SqlParameter("@newSupplierManName", supplierManName);
            sqlParaSupplierManCont = new SqlParameter("@newSupplierManCont", supplierManCont);
            sqlParaSupplierManEmail = new SqlParameter("@newSupplierManEmail", supplierManEmail);
            sqlParaSupplierManDepart = new SqlParameter("@newSupplierManDep", supplierManDep);
            sqlParaSupplierManTitle = new SqlParameter("@newSupplierManTitle", supplierManTitle);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaSupplierManId);
            sqlCmd.Parameters.Add(sqlParaSupplierManName);
            sqlCmd.Parameters.Add(sqlParaSupplierManCont);
            sqlCmd.Parameters.Add(sqlParaSupplierManEmail);
            sqlCmd.Parameters.Add(sqlParaSupplierManDepart);
            sqlCmd.Parameters.Add(sqlParaSupplierManTitle);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public void SupplierManDel(string supplierManId)
        {
            #region sqlPara declare
            //supplierManId
            SqlParameter sqlParaSupplierManId = null;
            //supplierManEnd
            SqlParameter sqlParaSupplierManEnd = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_supplier_manager_delete";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int supplierManIdTemp = int.Parse(supplierManId);
            DateTime st = DateTime.Now;

            sqlParaSupplierManId = new SqlParameter("@delSupplierManId", supplierManIdTemp);
            sqlParaSupplierManEnd = new SqlParameter("@delEndTime", st);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaSupplierManId);
            sqlCmd.Parameters.Add(sqlParaSupplierManEnd);
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
                "FROM tbl_supplier_manager ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "tbl_supplier_manager");

            return myDataSet;
        }
    }
}