using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnggCom;
using System.Collections;
using AjaxControlToolkit;
using System.Data;

public partial class TestProj_DynamicEdit : System.Web.UI.Page
{
    LLAdmin objTest = new LLAdmin();

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadgvEmployees();
    }

    protected void LoadgvEmployees()
    {
        gvEmployees.DataSource = objTest.GetTestFields();
        gvEmployees.DataBind();
    }

    protected TextBox LoadtxtEmployees()
    {
        TextBox txtEmployees = new TextBox();
        txtEmployees.ID = "txtEmployees";
        return txtEmployees;
    }

    protected AutoCompleteExtender LoadAutoCompleteExtender(TextBox txtEmployees)
    {
        txtEmployees = LoadtxtEmployees();
        AutoCompleteExtender aceEmployees = new AutoCompleteExtender();
        aceEmployees.ID = "aceEmployees";
        aceEmployees.TargetControlID = txtEmployees.ID;
        aceEmployees.ServiceMethod = "SearchEmployees";
        aceEmployees.MinimumPrefixLength = 1;
        aceEmployees.OnClientItemSelected = "GetSelectedEmployeeNo";

        return aceEmployees;
        // this.Controls.Add(txtEmployees);
        // this.Controls.Add(aceEmployees);
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> SearchEmployees(string prefixText)
    {
        OfflineTraining objOff01 = new OfflineTraining();
        List<string> NameList = new List<string>();
        DataTable dt = new DataTable();
        dt = objOff01.GetEmployeeList(prefixText);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                NameList.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Convert.ToString(dt.Rows[i]["EMP_NAME"]), Convert.ToString(dt.Rows[i]["EMP_NO"])));
            }
        }
        return NameList;
    }

    protected DropDownList GetcboEmployees()
    {
        DropDownList cboEmployees = new DropDownList();
        cboEmployees.Items.Insert(0, new ListItem("34148151", "34148151"));
        cboEmployees.Items.Insert(0, new ListItem("34131169", "34131169"));
        cboEmployees.DataBind();
        return cboEmployees;
    }

    protected void gvEmployees_DataBound(object sender, EventArgs e)
    {
        if (gvEmployees.EditIndex > -1)
        {
            // gvEmployees.Rows[gvEmployees.EditIndex].Cells[3].Controls.RemoveAt(0);
            // gvEmployees.Rows[gvEmployees.EditIndex].Cells[3].Controls.Add(LoadtxtEmployees());
            for (int i = 4; i <= 7; i++)
            {
                TextBox txtEmployees = (TextBox)gvEmployees.Rows[gvEmployees.EditIndex].Cells[i].Controls[0];
                txtEmployees.ID = "txtEmployees" + i;

                AutoCompleteExtender aceEmployees = new AutoCompleteExtender();
                aceEmployees.ID = "aceEmployees" + i;

                //aceEmployees.ClientIDMode = System.Web.UI.ClientIDMode.AutoID;
                aceEmployees.TargetControlID = txtEmployees.ID;
                aceEmployees.ServiceMethod = "SearchEmployees";
                aceEmployees.MinimumPrefixLength = 1;
                aceEmployees.CompletionListHighlightedItemCssClass = "Red1";
                aceEmployees.CompletionListCssClass = "completionList";
                aceEmployees.CompletionListItemCssClass = "Green1";
                //aceEmployees.OnClientItemSelected = "OnEdit(" + gvEmployees.EditIndex + ", " + i + ")";
                aceEmployees.OnClientItemSelected = "GetSelectedEmployeeNo";


                //HiddenField hdnEmployeeId = new HiddenField();
                //hdnEmployeeId.ID = "hdnEmployeeId";
                //hdnEmployeeId.ClientIDMode = System.Web.UI.ClientIDMode.AutoID;
                //hdnEmployeeId.Value = "";

                gvEmployees.Rows[gvEmployees.EditIndex].Cells[i].Controls.Add(aceEmployees);
                //gvEmployees.Rows[gvEmployees.EditIndex].Cells[i].Controls.Add(hdnEmployeeId);

                //HiddenField hdnFlag = new HiddenField();
                //hdnFlag.ID = "hdnFlag" + i;
                //hdnFlag.Value = "0";
                //gvEmployees.Rows[gvEmployees.EditIndex].Cells[i].Controls.Add(hdnFlag);

                // TextBox txtEmployees = (TextBox)gvEmployees.Rows[gvEmployees.EditIndex].Cells[3].Controls[0];
                // txtEmployees.ID = "txtEmployees";

                // AutoCompleteExtender aceEmployees = new AutoCompleteExtender();
                // aceEmployees.ID = "aceEmployees";

                // aceEmployees.TargetControlID = txtEmployees.ID;
                // aceEmployees.ServiceMethod = "SearchEmployees";
                // aceEmployees.MinimumPrefixLength = 1;
                // aceEmployees.CompletionListCssClass = "Grey";
                // gvEmployees.Rows[gvEmployees.EditIndex].Cells[3].Controls.Add(aceEmployees);
            }

        }
    }

    protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ((GridView)sender).EditIndex = e.NewEditIndex;
        // CALL YOUR DATABINDING METHOD HERE
        // IT MUST BE EITHER AN ILISTSOURCE, IENUMERABLE, OR IDATASOURCE.
        ((GridView)sender).DataSource = objTest.GetTestFields();
        ((GridView)sender).DataBind();
    }

    protected void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string msg = "VALIDATION-TEST-01: Description - \\n";
        //int Error = 0;
        TextBox txtFID = (TextBox)gvEmployees.Rows[gvEmployees.EditIndex].Cells[2].Controls[0];
        objTest.FID = Convert.ToInt32(txtFID.Text);

        for (int i = 4; i <= 7; i++)
        {
            TextBox txtEmployees = (TextBox)gvEmployees.Rows[gvEmployees.EditIndex].Cells[i].Controls[0];
            HiddenField hdn = (HiddenField)gvEmployees.Rows[gvEmployees.EditIndex].FindControl("hdnEmployeeId");
            //HiddenField hdnFlag = (HiddenField)gvEmployees.Rows[gvEmployees.EditIndex].Cells[i].FindControl("hdnFlag" + i);
            switch (i)
            {
                case 4:
                    //if (hdnFlag.Value == "1")
                    //{
                    objTest.SavedBy = Convert.ToInt32(txtEmployees.Text);
                    //}
                    //else
                    //{
                    //     msg += "Please select SAVED_BY from the AutoExtender List. \\n";
                    //    Error = 1;
                    //}
                    break;
                case 5:
                    //if (hdnFlag.Value == "1")
                    //{
                    objTest.UpdatedBy = Convert.ToInt32(txtEmployees.Text);
                    //}
                    //else
                    //{
                    //    msg += "Please select UPDATED_BY from the AutoExtender List. \\n";
                    //    Error = 1;
                    //}
                    break;
                case 6:
                    //if (hdnFlag.Value == "1")
                    //{
                    objTest.Extra01 = Convert.ToString(txtEmployees.Text);
                    //}
                    //else
                    //{
                    //    msg += "Please select EXTRA01 from the AutoExtender List. \\n";
                    //    Error = 1;
                    //}
                    break;
                case 7:
                    //if (hdnFlag.Value == "1")
                    //{
                    //objTest.Extra02 = Convert.ToString(txtEmployees.Text);
                    objTest.Extra02 = Convert.ToString(hdn.Value);

                    //}
                    //else
                    //{
                    //    msg += "Please select EXTRA02 from the AutoExtender List. \\n";
                    //    Error = 1;
                    //}
                    break;
            }
        }

        // CASE 01 : FIND TEXTBOX
        TextBox txtEmployee = (TextBox)gvEmployees.Rows[gvEmployees.EditIndex].Cells[3].Controls[0];
        objTest.SavedBy = Convert.ToInt32(txtEmployee.Text);

        // CASE 02 : FIND DROPDOWN
         //DropDownList cboEmployees = (DropDownList)gvEmployees.Rows[gvEmployees.EditIndex].Cells[3].Controls[0];
         //objTest.SavedBy = Convert.ToInt32(cboEmployees.SelectedValue);
        //if (Error == 0)
        //{
        string strQuery = "UPDATE TABL_LL_FIELD SET SAVED_BY = " + objTest.SavedBy + ", UDATED_BY = " + objTest.UpdatedBy + ", EXTRA01 = '" + objTest.Extra01 + "', EXTRA02 = '" + objTest.Extra02 + "' WHERE FIELD_ID = " + objTest.FID;
        if (objTest.UpdateTestFields(strQuery))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "alert('SUCCESS-TEST-01: Description - Field data updated successfully !!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "alert('ERROR-TEST-01: Description - Field data updation failed !!');", true);
        }
        gvEmployees.EditIndex = -1;
        LoadgvEmployees();
        //}
        //else ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "alert('" + msg + "');", true);
    }

    protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvEmployees.EditIndex = -1;
        gvEmployees.DataBind();
    }


    //protected List<DropDownList> LoadcboOnEdit()
    //{
    //    //STEP 01
    //    DropDownList cboOnEdit = new DropDownList();

    //    //STEP 01
    //    cboOnEdit.Items.Insert(0, new ListItem("34148151", "34148151"));
    //    cboOnEdit.Items.Insert(0, new ListItem("34131169", "34131169"));

    //    //STEP 01
    //    List<DropDownList> x = new List<DropDownList>();

    //    //STEP 01
    //    x.Add(cboOnEdit);

    //    //STEP 01
    //    return x;
    //}


}