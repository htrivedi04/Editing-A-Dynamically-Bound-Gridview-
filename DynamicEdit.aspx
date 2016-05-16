<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/SEIMMaster.master" AutoEventWireup="true" CodeFile="DynamicEdit.aspx.cs" Inherits="TestProj_DynamicEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="underHead" runat="Server">
    <script type="text/javascript">
        //function OnEdit(rowNo, colNo, source, eventargs)
        //{
        //    var RNo = rowNo;
        //    var CNo = colNo;
        //    alert('Test !! <br/> Row No: ' + RNo + '<br/>Cell No: ' + CNo);
        //    //document.cookie;
        //    // document.getElementById("ctl00$MainContent$gvEmployees$ctl" + RNo + "$hdnFlag" + CNo).value = "1";
        //    // ctl00$MainContent$gvEmployees$ctl18$hdnFlag5
        //}


        function GetSelectedEmployeeNo(source, eventArgs) {
            debugger;
            var HdnKey = eventArgs.get_value();
            alert(HdnKey);
            var hdnEmployeeId = source.get_id().replace("aceEmployees", "hdnEmployeeId");
            document.getElementById(hdnEmployeeId).value = HdnKey;
            alert(hdnEmployeeId.value);
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div width="100%">
        <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajax:ToolkitScriptManager>
        <%--DataKeyNames="FIELD_ID"--%>

        <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="true" OnRowEditing="gvEmployees_RowEditing" 
            OnRowUpdating="gvEmployees_RowUpdating" OnDataBound="gvEmployees_DataBound" OnRowCancelingEdit="gvEmployees_RowCancelingEdit">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnEmployeeId" runat="server" Value="0" ClientIDMode="AutoID" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

