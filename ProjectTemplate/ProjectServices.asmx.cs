using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProjectTemplate
{
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]

	public class ProjectServices : System.Web.Services.WebService
	{
		////////////////////////////////////////////////////////////////////////
		///replace the values of these variables with your database credentials
		////////////////////////////////////////////////////////////////////////
		private string dbID = "runtime";
		private string dbPass = "!!Cis440";
		private string dbName = "runtime";


        ////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////
        ///call this method anywhere that you need the connection string!
        ////////////////////////////////////////////////////////////////////////
        private string getConString() {
			return "SERVER=107.180.1.16; PORT=3306; DATABASE=" + dbName+"; UID=" + dbID + "; PASSWORD=" + dbPass;
		}
		////////////////////////////////////////////////////////////////////////



		/////////////////////////////////////////////////////////////////////////
		//don't forget to include this decoration above each method that you want
		//to be exposed as a web service!
		[WebMethod(EnableSession = true)]
		/////////////////////////////////////////////////////////////////////////
		public string TestConnection()
		{
			try
			{
				string testQuery = "select * from test";

				////////////////////////////////////////////////////////////////////////
				///here's an example of using the getConString method!
				////////////////////////////////////////////////////////////////////////
				MySqlConnection con = new MySqlConnection(getConString());
				////////////////////////////////////////////////////////////////////////

				MySqlCommand cmd = new MySqlCommand(testQuery, con);
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				DataTable table = new DataTable();
				adapter.Fill(table);
				return "Success!";
			}
			catch (Exception e)
			{
				return "Something went wrong, please check your credentials and db name and try again.  Error: "+e.Message;
			}
		}

        //[WebMethod(EnableSession = true)]
        //public bool LogOn(string uid, string pass)
        //{
        //    //LOGIC: pass the parameters into the database to see if an account
        //    //with these credentials exist.  If it does, then return true.  If
        //    //it doesn't, then return false

        //    //we return this flag to tell them if they logged in or not
        //    bool success = false;

        //    //our connection string comes from our web.config file like we talked about earlier
        //    string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
        //    //here's our query.  A basic select with nothing fancy.  Note the parameters that begin with @
        //    string sqlSelect = "SELECT employeeId, adminFlag FROM Account WHERE employeeId=@idValue and AcctPassword=@passValue";

        //    //set up our connection object to be ready to use our connection string
        //    MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
        //    //set up our command object to use our connection, and our query
        //    MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

        //    //tell our command to replace the @parameters with real values
        //    //we decode them because they came to us via the web so they were encoded
        //    //for transmission (funky characters escaped, mostly)
        //    sqlCommand.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(uid));
        //    sqlCommand.Parameters.AddWithValue("@passValue", HttpUtility.UrlDecode(pass));

        //    //a data adapter acts like a bridge between our command object and 
        //    //the data we are trying to get back and put in a table object
        //    MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
        //    //here's the table we want to fill with the results from our query
        //    DataTable sqlDt = new DataTable();
        //    //here we go filling it!
        //    sqlDa.Fill(sqlDt);
        //    //check to see if any rows were returned.  If they were, it means it's 
        //    //a legit account
        //    if (sqlDt.Rows.Count > 0)
        //    {
        //        //flip our flag to true so we return a value that lets them know they're logged in
        //        success = true;
        //    }
        //    //return the result!
        //    return success;
        //}

        [WebMethod(EnableSession = true)]
        public CurrentLogon LogOn(string uid, string pass)
        {
            //LOGIC: pass the parameters into the database to see if an account
            //with these credentials exist.  If it does, then return true.  If
            //it doesn't, then return false

            CurrentLogon currentLogon = new CurrentLogon();
            currentLogon.success = false;
            currentLogon.adminFlag = false;

            //our connection string comes from our web.config file like we talked about earlier
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            //here's our query.  A basic select with nothing fancy.  Note the parameters that begin with @
            string sqlSelect = "SELECT employeeId, adminFlag FROM Account WHERE employeeId=@idValue and AcctPassword=@passValue";

            //set up our connection object to be ready to use our connection string
            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            //set up our command object to use our connection, and our query
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            //tell our command to replace the @parameters with real values
            //we decode them because they came to us via the web so they were encoded
            //for transmission (funky characters escaped, mostly)
            sqlCommand.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(uid));
            sqlCommand.Parameters.AddWithValue("@passValue", HttpUtility.UrlDecode(pass));

            //a data adapter acts like a bridge between our command object and 
            //the data we are trying to get back and put in a table object
            MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
            //here's the table we want to fill with the results from our query
            DataTable sqlDt = new DataTable();
            //here we go filling it!
            sqlDa.Fill(sqlDt);
            //check to see if any rows were returned.  If they were, it means it's 
            //a legit account
            if (sqlDt.Rows.Count > 0)
            {
                //flip our flag to true so we return a value that lets them know they're logged in
                currentLogon.success = true;
                currentLogon.employeeId = sqlDt.Rows[0]["EmployeeId"].ToString();
                if (sqlDt.Rows[0]["AdminFlag"].ToString().ToUpper() == "X")
                {
                    currentLogon.adminFlag = true;
                }

            }
            
            //return the result!
            return currentLogon;
        }


        [WebMethod(EnableSession = true)]
        public PendingAccountRequest[] GetAccountRequests()
        {//LOGIC: get all account requests and return them!
            DataTable sqlDt = new DataTable("accountrequests");

            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            
            string sqlSelect = "SELECT EmployeeId, AcctPassword, FirstName, LastName, Email, RequestDt FROM runtime.AccountRequest WHERE AccountApproval IS NULL order by RequestDt";

            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
            sqlDa.Fill(sqlDt);

            List<PendingAccountRequest> accountRequests = new List<PendingAccountRequest>();
            for (int i = 0; i < sqlDt.Rows.Count; i++)
            {
                accountRequests.Add(new PendingAccountRequest
                {
                    employeeId = sqlDt.Rows[i]["EmployeeId"].ToString(),
                    acctPassword = sqlDt.Rows[i]["AcctPassword"].ToString(),
                    firstName = sqlDt.Rows[i]["FirstName"].ToString(),
                    lastName = sqlDt.Rows[i]["LastName"].ToString(),
                    email = sqlDt.Rows[i]["Email"].ToString(),
                    requestDt = sqlDt.Rows[i]["RequestDt"].ToString()
                });
            }
            //convert the list of accounts to an array and return!
            return accountRequests.ToArray();
        }

        [WebMethod(EnableSession = true)]
        public PendingAccountRequest GetAccountRequestDetails(string employeeId)
        {
            PendingAccountRequest pendingAccount = new PendingAccountRequest();
            
            //our connection string comes from our web.config file like we talked about earlier
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            //here's our query.  A basic select with nothing fancy.  Note the parameters that begin with @
            // string sqlSelect = "SELECT employeeId, adminFlag FROM Account WHERE employeeId=@idValue and AcctPassword=@passValue";
            string sql = "SELECT * FROM AccountRequest WHERE EmployeeId=@idValue";

            //set up our connection object to be ready to use our connection string
            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            sqlConnection.Open();


            //set up our command object to use our connection, and our query
            MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConnection);

            //tell our command to replace the @parameters with real values
            //we decode them because they came to us via the web so they were encoded
            //for transmission (funky characters escaped, mostly)
            sqlCommand.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(employeeId));

            try
            {
                MySqlDataReader dr;
                dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    pendingAccount.employeeId = dr.GetString("EmployeeID");
                    pendingAccount.acctPassword = dr.GetString("AcctPassword");
                    pendingAccount.email = dr.GetString("Email");
                    pendingAccount.firstName = dr.GetString("FirstName");
                    pendingAccount.lastName = dr.GetString("LastName");
                }
                dr.Close();
            }
            catch (Exception)
            {
                
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

            return pendingAccount;
        }

        [WebMethod(EnableSession = true)]
        public string DenyAccountRequest(string employeeId)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = "DELETE FROM AccountRequest WHERE EmployeeId=@idValue";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(employeeId));

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                return "account denied success";
            }
            catch (Exception)
            {
                return "account denied failed";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public string ApproveAccountRequest(string employeeId)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = "INSERT INTO Account(EmployeeId, AcctPassword, FirstName, LastName, Email) SELECT EmployeeId, AcctPassword, FirstName, LastName, Email FROM AccountRequest WHERE AccountRequest.EmployeeId=@idValue";
            string sql2 = "DELETE FROM AccountRequest WHERE EmployeeId=@idValue";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
            cmd.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(employeeId));
            cmd2.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(employeeId));

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                return "account approval success";
            }
            catch (Exception)
            {
                return "account approval failed";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public string AccountRequest(string employeeId, string password, string fName, string lName, string email)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = "INSERT INTO AccountRequest (EmployeeId, AcctPassword, FirstName, LastName, Email) VALUES (@idValue, @pwValue, @fnValue, @lnValue, @emValue);";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(employeeId));
            cmd.Parameters.AddWithValue("@pwValue", HttpUtility.UrlDecode(password));
            cmd.Parameters.AddWithValue("@fnValue", HttpUtility.UrlDecode(fName));
            cmd.Parameters.AddWithValue("@lnValue", HttpUtility.UrlDecode(lName));
            cmd.Parameters.AddWithValue("@emValue", HttpUtility.UrlDecode(email));

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                return "account request success";
            }
            catch (Exception)
            {
                return "account request failed";
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public Account GetAccount(string employeeId)
        {
            Account currentAccount = new Account();

            //our connection string comes from our web.config file like we talked about earlier
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            //here's our query.  A basic select with nothing fancy.  Note the parameters that begin with @
            // string sqlSelect = "SELECT employeeId, adminFlag FROM Account WHERE employeeId=@idValue and AcctPassword=@passValue";
            string sql = "SELECT * FROM Account WHERE EmployeeId=@idValue";

            //set up our connection object to be ready to use our connection string
            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            sqlConnection.Open();


            //set up our command object to use our connection, and our query
            MySqlCommand sqlCommand = new MySqlCommand(sql, sqlConnection);

            //tell our command to replace the @parameters with real values
            //we decode them because they came to us via the web so they were encoded
            //for transmission (funky characters escaped, mostly)
            sqlCommand.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(employeeId));

            try
            {
                MySqlDataReader dr;
                dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    currentAccount.employeeId = dr.GetString("EmployeeID");
                    currentAccount.password = dr.GetString("AcctPassword");
                    currentAccount.firstName = dr.GetString("FirstName");
                    currentAccount.lastName = dr.GetString("LastName");
                    currentAccount.email = dr.GetString("Email");
                    currentAccount.adminFlag = Convert.ToBoolean(dr.GetString("AdminFlag"));
                    currentAccount.disableFlag = Convert.ToBoolean(dr.GetString("DisableFlag"));
                    currentAccount.disableCount = Convert.ToInt32(dr.GetString("DisableCount"));
                }
                dr.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }

            return currentAccount;
        }
    }
}
