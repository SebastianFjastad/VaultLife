using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Web;
using Vaultlife.Models;

//using PlayOutLoud.BIZLayer;

namespace Vaultlife.Helpers
{
    /// <summary>
    /// Author: Sanket
    /// Implemented : Rahul.
    /// </summary>
    public static class Logger
    {
        #region -- Global Declaration Section --
        public enum LogType { Debug, Info, Warn, Error, Fatal }

        #endregion

        #region Public Methods

        /// <summary>
        /// Write a log entry in database
        /// </summary>
        /// <param name="contents">Actual contents to log</param>
        /// <param name="type">Log type</param>.


        public static void Log(object contents, LogType type)
        {
            try
            {
                int minLogLevel = Convert.ToInt32(ConfigurationManager.AppSettings["minLogLevel"]);
                if ((int)type >= minLogLevel)
                {
                    Vaultlife.Models.Member su = null;


                    if (HttpContext.Current.Session != null)
                    {
                        su = (Member)HttpContext.Current.Session[Constants.SessionLoggedInUser];
                    }

                    if (su == null)
                    {
                        writeContents(contents, type, null);

                    }
                    else
                    {

                        writeContents(contents, type, Convert.ToInt32(su.MemberID));
                    }
                }
            }
            catch
            {
                // Do nothing
            }
        }

        /// <summary>
        ///  This method will write the log entries for imported and processed records
        /// </summary>
        /// <param name="sPathName"></param>
        /// <param name="DateTime"></param>
        /// <param name="Status"></param>
        /// <param name="objError"></param>
        /// <remarks></remarks>
        public static void ErrLogs(string sPathName, string DateTime, string Status, string objError)
        {
            try
            {
                string strLogFormat = "";
                string strErrDay = null;
                string strErrMonth = null;
                string strErrYear = null;
                string strErrDate = null;
                //This function will be called every time a Error occures in the sytem.
                //This function opens a txt file under the iis root of the applcation and logges the Error details available under 
                //Err.Message, Err.Source, Err.Number, Logged in User, Error Time.


                //This function will be called every time a Error occures in the sytem.
                //This function opens a txt file under the iis root of the applcation and logges the Error details available under
                //Err.Message, Err.Source, Err.Number, Logged in User, Error Time.
                StreamWriter swErr = default(StreamWriter);
                //strLogFormat = Now.Date.ToString & Now.TimeOfDay.ToString & " ===> "
                strLogFormat += "=======================================================================================================" + Environment.NewLine;
                strLogFormat += "=============                             Log Entry                    ================================" + Environment.NewLine;
                strLogFormat += "========================================================================================================" + Environment.NewLine;
                strLogFormat += " Date      |    Status    | Error Occured(If Any)   | Operation Time   " + Environment.NewLine;
                //Now.TimeOfDay.ToString & " ===> "
                strLogFormat += "========================================================================================================";
                strErrDay = System.DateTime.Now.Day.ToString();
                strErrMonth = System.DateTime.Now.Month.ToString();
                strErrYear = System.DateTime.Now.Year.ToString();
                strErrDate = strErrDay + "-" + strErrMonth + "-" + strErrYear;
                bool fileExists = false;
                fileExists = File.Exists(sPathName + "\\" + strErrDate + ".txt");
                if ((fileExists == false))
                {
                    swErr = new StreamWriter(sPathName + "\\" + strErrDate + ".txt", true);
                    swErr.WriteLine(strLogFormat + Environment.NewLine);
                    //)+ objError.ToString + vbCrLf)
                    swErr.WriteLine(DateTime + "|" + Status + "|" + objError + "|" + System.DateTime.Now.TimeOfDay.Hours + ":" + System.DateTime.Now.TimeOfDay.Minutes + ":" + System.DateTime.Now.TimeOfDay.Seconds);
                    swErr.WriteLine("========================================================================================================");
                }
                else
                {
                    swErr = new StreamWriter(sPathName + "\\" + strErrDate + ".txt", true);
                    swErr.WriteLine(DateTime + "|" + Status + "|" + objError + "|" + System.DateTime.Now.TimeOfDay.Hours + ":" + System.DateTime.Now.TimeOfDay.Minutes + ":" + System.DateTime.Now.TimeOfDay.Seconds);

                    swErr.WriteLine("========================================================================================================");
                }
                //swErr.WriteLine(FileName & "|" & DateTime & "|" & Status & "|" & objError& Format(Now.Date, "dd-MM-yyyy") & " " & Now.TimeOfDay.Hours & ":" & Now.TimeOfDay.Minutes & ":" & Now.TimeOfDay.Seconds))
                swErr.Flush();
                swErr.Close();
            }
            catch
            {
            }
        }

        #endregion

        #region Private Methods
        private static void writeContents(object contents, LogType type, int? userId)
        {
            SqlConnection cn = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["ApplicationConnection"]));

            string ipAddr;
            ipAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddr == null || ipAddr == string.Empty)
            {
                ipAddr = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            try
            {
                if (contents is Exception)
                {
                    Exception ex = (Exception)contents;

                    SqlCommand cmd = new SqlCommand("usp_Log", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LogType", type.ToString());
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@IpAddress", ipAddr);
                    cmd.Parameters.AddWithValue("@Message", ex.Message);
                    cmd.Parameters.AddWithValue("@StackTrace", ex.StackTrace);
                    cmd.Parameters.AddWithValue("@InnerException", ex.InnerException);
                    cmd.Parameters.AddWithValue("@ExceptionType", ex.GetType().ToString());
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    string FileName = HttpContext.Current.Server.MapPath("./Logs/");
                    ErrLogs(FileName, DateTime.Now.ToShortDateString(), type.ToString(), ipAddr + " " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException + " " + ex.GetType().ToString());
                }
                else
                {
                    StackTrace stack = new StackTrace();
                    int stackIdx = -1;
                    // step over all references to methods in this class
                    string myTypeName = "Logger";
                    while (stack.GetFrame(++stackIdx).GetMethod().DeclaringType.Name.Equals(myTypeName)) ;

                    string callingObjectName = stack.GetFrame(stackIdx).GetMethod().DeclaringType.Name;
                    string callingMethodName = stack.GetFrame(stackIdx).GetMethod().Name;

                    SqlCommand cmd = new SqlCommand("usp_Log", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LogType", type.ToString());
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@IpAddress", ipAddr);
                    cmd.Parameters.AddWithValue("@Message", contents.ToString());
                    cmd.Parameters.AddWithValue("@StackTrace", string.Format("{0}::{1}", callingObjectName, callingMethodName));
                    cmd.Parameters.AddWithValue("@InnerException", "");
                    cmd.Parameters.AddWithValue("@ExceptionType", "");
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    string FileName = HttpContext.Current.Server.MapPath("./Logs/");
                    ErrLogs(FileName, DateTime.Now.ToShortDateString(), type.ToString(), ipAddr + " " + contents.ToString() + " " + string.Format("{0}::{1}", callingObjectName, callingMethodName));
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                if (cn != null)
                {
                    if (cn.State == ConnectionState.Open)
                        cn.Close();

                    cn.Dispose();
                }
            }
        }
        #endregion
    }


  
}
