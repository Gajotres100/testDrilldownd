using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace TestDrillDown
{
    public class Class1
    {
        public List<ExpensesEntity> Test()
        {
            string conString = "User Id=PLUS_FCM; Password=manager; Data Source=10.70.54.24:1521/GDICLOUD1; Pooling =false;";
            OracleConnection con = new OracleConnection();
            List<ExpensesEntity> list = new List<ExpensesEntity>();

            try
            {
                
                con.ConnectionString = conString;
                con.Open();
 

                string cmdtxt = "SELECT * FROM TABLE(F_EXPENSES_PER_ORG(p_OU_ID=>1))";

                //string cmdtxt = "SELECT * FROM TABLE(F_EXPENSES_PER_ORG(p_OU_ID=>18459))";

                OracleCommand oraCommand = new OracleCommand(cmdtxt, con);

                OracleDataReader oraReader = null;
                oraReader = oraCommand.ExecuteReader();

                if (oraReader.HasRows)
                {
                    while (oraReader.Read())
                    {
                        ExpensesEntity obj = new ExpensesEntity();
                        obj.Lvl = Convert.ToInt32(oraReader["LVL"]);
                        obj.OuId = Convert.ToInt32(oraReader["OU_ID"]);
                        obj.OuName = oraReader["OU_NAME"].ToString();
                        obj.ExpenseName = oraReader["EXPENSE_NAME"].ToString();
                        obj.ValueTotal = Convert.ToDecimal(oraReader["VALUE_TOTAL"]);
                        obj.Year = Convert.ToInt32(oraReader["YEAR"]);
                        obj.ValueTotal = Convert.ToDecimal(oraReader["VALUE_TOTAL"]);
                        obj.Month = Convert.ToInt32(oraReader["MONTH"]);

                        list.Add(obj);
                    }
                }

                oraReader.Close();                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return list;
        }
    }
}