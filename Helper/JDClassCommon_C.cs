using System;
using System.Data;
using System.Configuration;
using System.Web;
using VentusLibrary;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;
using System.Net;

/// <summary>
/// Summary description for JDClassCommon_C
/// </summary>
public class JDClassCommon_C
{

    public VentusLibrary.DataLibrary rs = new VentusLibrary.DataLibrary(System.Configuration.ConfigurationManager.ConnectionStrings["AU_ConnString"].ToString());
    public DataTable dt = new DataTable();
    public DataTable dt2 = new DataTable();
    //public SqlDataReader readref;
    
    //variables
    public string loggedUser;
    public string iqry;
    public string iqry2;
    public string erroTrap;
    public string MessageString;

    //Grid
    //public void LoadGrid(GridView sGrid, string sQuery, string hidegrid)
    //{
    //    string gHide = hidegrid;
    //    dt = rs.CreateDataTableFromQry(sQuery);
    //    sGrid.DataSource = dt;
    //    sGrid.DataBind();

    //    if (hidegrid == "")
    //    {
    //        sGrid.Visible = true;
    //    }

    //    dt.Dispose();
    //}



    //public void LoadListView(ListView sGrid, string sQuery, string hidegrid)
    //{
    //    string gHide = hidegrid;
    //    dt = rs.CreateDataTableFromQry(sQuery);
    //    sGrid.DataSource = dt;
    //    sGrid.DataBind();

    //    if (hidegrid == "")
    //    {
    //        sGrid.Visible = true;
    //    }

    //    dt.Dispose();
    //}


    public void LoadGrid_telerik(Telerik.Web.UI.RadGrid sGrid, string sQuery, string hidegrid)
    {
        string gHide = hidegrid;
        dt = rs.CreateDataTableFromQry(sQuery);
        sGrid.DataSource = dt;
        //sGrid.DataBind();

        if (hidegrid == "")
        {
            sGrid.Visible = true;
        }

        dt.Dispose();
    }

    //Datalist
    //public void LoadDataListGrid(DataList sGrid, string sQuery, string hidegrid)
    //{
    //    string gHide = hidegrid;
    //    dt = rs.CreateDataTableFromQry(sQuery);
    //    sGrid.DataSource = dt;
    //    sGrid.DataBind();

    //    if (hidegrid == "")
    //    {
    //        sGrid.Visible = true;
    //    }

    //    dt.Dispose();
    //}


    //public void LoadListView_Telerik(Telerik.Web.UI.RadListView sGrid, string sQuery, string hidegrid)
    //{
    //    string gHide = hidegrid;
    //    dt = rs.CreateDataTableFromQry(sQuery);
    //    sGrid.DataSource = dt;
    //    sGrid.DataBind();

    //    if (hidegrid == "")
    //    {
    //        sGrid.Visible = true;
    //    }

    //    dt.Dispose();
    //}

    //Dropdown
    //public void LoadDropDown(DropDownList ddlList, string sQuery, string TextField, string ValueField)
    //{

    //    dt = rs.CreateDataTableFromQry(sQuery);
    //    ddlList.DataSource = dt;
    //    ddlList.DataTextField = TextField;
    //    ddlList.DataValueField = ValueField;
    //    ddlList.DataBind();
    //    //ddlList.Items.Insert(0, "");
    //    dt.Dispose();

    //}

    //public void LoadDropDown_ws(DropDownList ddlList, string sQuery, string TextField, string ValueField)
    //{

    //    dt = rs.CreateDataTableFromQry(sQuery);
    //    ddlList.DataSource = dt;
    //    ddlList.DataTextField = TextField;
    //    ddlList.DataValueField = ValueField;
    //    ddlList.DataBind();
    //    ddlList.Items.Insert(0, "");
    //    dt.Dispose();

    //}


    //Dropdown
    public void LoadDropDown_rad(Telerik.Web.UI.RadComboBox ddlList_rad, string sQuery, string TextField, string ValueField)
    {

        dt = rs.CreateDataTableFromQry(sQuery);
        ddlList_rad.DataSource = dt;
        ddlList_rad.DataTextField = TextField;
        ddlList_rad.DataValueField = ValueField;
        //ddlList_rad.DataBind();
        //ddlList_rad.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem(""));
        dt.Dispose();

    }

    //Alert
    //public void AlertBox(Page aCurrentPage, string aMessage,string aPageRedirect)
    //{

    //    StringBuilder Ab = new StringBuilder();
    //    Ab.Append("alert('");
    //    Ab.Append(aMessage);
    //    if (aPageRedirect == "")
    //    {
    //        Ab.Append("');");
    //    }

    //    else
    //    {
    //        Ab.Append("'); window.location='" + aPageRedirect + "';");
    //    }

    //    aCurrentPage.ClientScript.RegisterStartupScript(aCurrentPage.GetType(), "showalert", Ab.ToString(), true);

    //}

    //public void UpdatePanelAlertBox(UpdatePanel UpdatePanel, string Message)
    //{ 
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("alert('");
    //    sb.Append(Message);
    //    sb.Append("')");

    //    ScriptManager.RegisterStartupScript(UpdatePanel, UpdatePanel.GetType(), "UpdatePanelAlertBox", sb.ToString(), true);
    //}

    //Return Value Yes/No
    public string GetChkBoxValue(Boolean vParam)
    {
        if (vParam == true)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }

    public string GetChkBoxValue_RET(String vParam)
    {
        if (vParam == "1")
        {
            return "True";
        }
        else
        {
            return "False";
        }
    }

    //Check if admin user
    public bool CheckIfAdmin(string NTLOGIN)
    {

        string UserAdmin = "";

        iqry = "Select * from Tbl_Users where NTLogin = '" + NTLOGIN + "' and IsActive = '1' ";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            UserAdmin = jdrow["UserTypeID"].ToString();
        }

        if (UserAdmin == "0")
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    //Check if admin user
    public bool DuplicateEmpID(string EmpID)
    {

        iqry = "Select emailadd from lst_user_admin where cognigence_id = '" + EmpID + "' ";
        dt = rs.CreateDataTableFromQry(iqry);
        if (dt.Rows.Count == 1)
        {
            return true;
        }
        return false;
    }

    //DUPLICATE STUDENT ID
    public bool DuplicateStudentID(string EmpID)
    {

        iqry = "Select student_no from lst_students where student_no = '" + EmpID + "' ";
        dt = rs.CreateDataTableFromQry(iqry);
        if (dt.Rows.Count == 1)
        {
            return true;
        }
        return false;
    }


    //Check if admin user
    public bool CheckAccountNo(string ACCTNO)
    {

        iqry = "Select UP_applicationid from ew_tbl_disposition where UP_applicationid = '" + ACCTNO + "' ";
        dt = rs.CreateDataTableFromQry(iqry);
        if (dt.Rows.Count == 1)
        {
            return true;
        }
        return false;
    }

    //GET EMPLOYEE INFO
    public string GetEmployeeInfo(string NTLOGIN, string TABLE_ID)
    {
        switch (TABLE_ID)
        {
            case "1":
                iqry = "Select * from Tbl_CGA_Profile_SelfAnswered where NTLogin = '" + NTLOGIN + "' ";
            break;
        
        }
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            return jdrow["EmployeeId"].ToString();
        }

        if (dt.Rows.Count == 0)
        {
            return "";
        }

        return "";

    }

    //Check if admin user
    public bool CheckIfAnswered(string EMPID)
    {
        iqry = "Select * from Tbl_ENPS_Survey where EmployeeID = '" + EMPID + "'  ";
        dt = rs.CreateDataTableFromQry(iqry);
        if (dt.Rows.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
   
    //CompletionReports
    public int CompletionCount_PerSite(string SiteId)
    {

        iqry = "Select TargetNoOfComp as cnt from Lst_Site where SiteID = '" + SiteId + "' ";
        dt = rs.CreateDataTableFromQry(iqry);
        return Convert.ToInt32(dt.Rows[0]["cnt"].ToString());

        
    }

    public DateTime GetSelectedGameStartTime(string GameId)
    {


        iqry = "Select GameStart from tbl_gamesched where GameId = '" + GameId + "'";
        dt = rs.CreateDataTableFromQry(iqry);
        return Convert.ToDateTime(dt.Rows[0]["GameStart"].ToString());


    }

    

    //public void PopUpWindow(Page CurrentPage, string URL, string Features)
    //{
    //    string strScript = null;
    //    strScript = "<script language='javascript'>";
    //    strScript += "var confirmWin = null;";
    //    strScript += "confirmWin = window.open('" + URL + "','OpenTicket','" + Features + "');";
    //    strScript += "</script>";

    //    CurrentPage.ClientScript.RegisterStartupScript(CurrentPage.GetType(), "PopUpWindow", strScript);
    //}


    //GET FACTOR ID
    public string getFactorID(string SubFactorId)
    {
        iqry = "Select * from lst_score_factor_sub where score_factor_sub_id = '" + SubFactorId + "' ";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
                return jdrow["score_factor_id"].ToString();
        }

        return "";
    }

    //GET GAME TIME
    public double GetRemainingMinutes(string SelectedGameId)
    {

        iqry = "Select GameStart from tbl_gamesched where GameId = '" + SelectedGameId + "' ";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            DateTime TimeNow = DateTime.Now;
            DateTime TimeNext = Convert.ToDateTime(jdrow["GameStart"].ToString());

            TimeSpan span = Convert.ToDateTime(TimeNext).Subtract(TimeNow);
            return span.TotalMinutes;

        }

        return 0;
    }

    //GET GAME TIME
    public bool CheckBalance(string UserId, double Ante)
    {

        iqry = "Select UserId,Balance from tbl_users where UserId = '" + UserId + "'";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {

            if (Convert.ToDouble(jdrow["Balance"].ToString()) < Ante)
            {
                return true;
            }

        }

        return false;
    }

    public double GetBalance(string UserId)
    {

        iqry = "Select UserId,Balance from tbl_users where UserId = '" + UserId + "'";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            return Convert.ToDouble(jdrow["Balance"].ToString());
        }

        return 0;
    }

    public string UpdateBalance(string UserId, double NewBalance)
    {

        iqry = "Update tbl_users SET Balance = '" + NewBalance  + "' where UserId = '" + UserId + "'";
        if (!rs.ExecuteSQL(iqry, false))
        { }
       
        return "";
    }

    public int GETLIMIT_DISPUTE()
    {
        iqry = "Select * from  lst_parameters where param_id = '2' ";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            return Convert.ToInt32(jdrow["param_value"].ToString());
        }

        return 0;

    }

    public int GETLIMIT_SO()
    {
        iqry = "Select * from  lst_parameters where param_id = '1' ";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            return Convert.ToInt32(jdrow["param_value"].ToString());
        }

        return 0;

    }

    public int GetRemainingSeconds(string SelectedGameId)
    {

        iqry = "Select GameStart from tbl_gamesched where GameId = '" + SelectedGameId + "' ";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            TimeSpan span = Convert.ToDateTime(jdrow["GameStart"].ToString()).Subtract(DateTime.Now);
            return span.Seconds;

        }

        return 0;
    }

    public string getrandomnumber_set1()
    {

        iqry = "SELECT SETNUMBERA FROM 888_lst_numberset ORDER BY RAND() LIMIT 1";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            return jdrow["SETNUMBERA"].ToString();

        }

        return "00";
    }

    //CREATE PO
    public string NewPONumber()
    {

        iqry = "Select TOP 1 * from TBL_PO_HEADER WHERE YEAR(createddate) = '" + System.DateTime.Now.Year + "' order by rec_id DESC";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (DataRow drow in dt.Rows)
        {
            int qryValue = Convert.ToInt16(Right(drow["po_number"].ToString(), 4)) + 1;
            string cValue = "";

            string DBLastYear = Convert.ToDateTime(drow["createddate"].ToString()).Year.ToString();
            string DBLastMonth = Convert.ToDateTime(drow["createddate"].ToString()).Month.ToString();

            dynamic CurrentYear = System.DateTime.Now.Year;
            dynamic CurrentMonth = DateTime.Now.Month;

            if (DBLastMonth.ToString() != CurrentMonth.ToString())
            {
                qryValue = 0;
            }


            if (qryValue >= 0 & qryValue < 10)
            {
                cValue = System.DateTime.Now.Year + "-PO" + DateTime.Now.Month + "000" + qryValue;
            }
            else if (qryValue >= 10 & qryValue < 100)
            {
                cValue = System.DateTime.Now.Year + "-PO" + DateTime.Now.Month + "00" + qryValue;
            }
            else if (qryValue >= 100 & qryValue < 1000)
            {
                cValue = System.DateTime.Now.Year + "-PO" + DateTime.Now.Month + "0" + qryValue;
            }
            else if (qryValue >= 1000)
            {
                cValue = System.DateTime.Now.Year + "-PO" + DateTime.Now.Month + qryValue;
            }

            return cValue;
        }

        if (dt.Rows.Count == 0)
        {
            return System.DateTime.Now.Year + "-PO" + DateTime.Now.Month + "0001";
        }

        return "";

    }

    //right string
    public string Right(string param, int length)
    {
        //start at the index based on the lenght of the sting minus
        //the specified lenght and assign it a variable
        string result = param.Substring(param.Length - length, length);
        //return the result of the operation
        return result;
    }

    //left string
    public string Left(string param, int length)
    {
        //we start at 0 since we want to get the characters starting from the
        //left and with the specified lenght and assign it to a variable
        string result = param.Substring(0, length);
        //return the result of the operation
        return result;
    }


    //LIMITS
    public string GetAssignedTotal_DIS(string Auditor, string DateAssigned)
    {
        iqry = "Select count(*) as DIS_Total from tbl_AuditTracker_Dispute where TransferDate = "+ VentusLibrary.CommonFunctions.FDate(DateAssigned) + "  and ValidatedBy = " + VentusLibrary.CommonFunctions.FStr(Auditor) + "";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            return jdrow["DIS_Total"].ToString();
        }

        return "0";

    }


    public string GetAssignedTotal_SO(string Auditor, string DateAssigned)
    {
        iqry = "Select count(*) as DIS_Total from tbl_AuditTracker_SO where TransferDate = " + VentusLibrary.CommonFunctions.FDate(DateAssigned) + "  and ValidatedBy = " + VentusLibrary.CommonFunctions.FStr(Auditor) + "";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            return jdrow["DIS_Total"].ToString();
        }

        return "0";

    }

    public string GetSFPQActivation(string SchoolID)
    {
        iqry = "Select * from lst_school where sch_id = '" + SchoolID + "' ";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            return Convert.ToDateTime(jdrow["date_start"].ToString()).ToShortDateString() + " - " + Convert.ToDateTime(jdrow["date_end"].ToString()).ToShortDateString();
        }

        return "0";

    }


    public string GetStudentTotal(string SchoolID)
    {
        iqry = "Select count(student_id) as TTL from lst_students where sch_id = '" + SchoolID + "' ";
        dt = rs.CreateDataTableFromQry(iqry);
        foreach (System.Data.DataRow jdrow in dt.Rows)
        {
            return jdrow["TTL"].ToString();
        }

        return "0";

    }
}
