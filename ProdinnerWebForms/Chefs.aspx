<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Chefs.aspx.cs" Inherits="ProdinnerWebForms.Chefs" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%if(Msg != null)
  {%>
  <div class="msg"><%=Msg %></div>
  <%} %>
  <div class="topmenu">
    <button type="button" id="bAdd" class="bt">
        <%=Mui.Add %></button>
        </div>
    <div id="f1" style="display: none;" class="inputform" >
    <asp:HiddenField runat="server" ID="eId" />
        <div class="efield">
            <div class="elabel"><%=Mui.FirstName %>:</div>
            <div class="einput"><asp:TextBox runat="server" ID="eFirstName" ValidationGroup="g1"></asp:TextBox></div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="eFirstName" ValidationGroup="g1" ErrorMessage="<%$ Resources:Mui, FieldReq %>"></asp:RequiredFieldValidator>
        </div>
        <div class="efield">
            <div class="elabel"><%=Mui.LastName %>:</div>
            <div class="einput"><asp:TextBox runat="server" ID="eLastName" ValidationGroup="g1"></asp:TextBox></div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="eLastName" ValidationGroup="g1" ErrorMessage="<%$ Resources:Mui, FieldReq %>"></asp:RequiredFieldValidator>
        </div>
        <div class="efield ebtns">
            <asp:Button runat="server" ValidationGroup="g1" Text="<%$ Resources:Mui, Save %>" OnClick="Save" CssClass="bt" />
            <button type="button" id="bCancel" class="bt">
                <%=Mui.Cancel %></button>
        </div>
    </div>
    
    <%
        lstItems.Buttons = new List<AjaxListButton>
                                   {
                                       new AjaxListButton { JsFunc = "edit", CssClass="bedit" },
                                       new AjaxListButton { JsFunc = "del", CssClass = "bdel" }
                                   };

        lstItems.SearchButton = "bSearch";
        lstItems.Data = new Dictionary<string, string> {{"search","txtSearch"}};
    %>
    <br/>
    <input type="text" id="txtSearch" name="search"/><button type="button" id="bSearch" class="bt"><%=Mui.Search %></button>
    <o:AjaxList runat="server" ID="lstItems" SearchUrl="~/svc/Aja.svc/ChefsSearch" />

    <script type="text/javascript">
        $(function() {
            $('#bAdd').click(function() { 
                clearForm("#f1");
                $('#f1').show(); 
            });
            $('#bCancel').click(function() { $('#f1').hide(); });
            <% if(ShowForm){%>
            $('#f1').show();
            <%} %>
        });
        
        function del(id) {
            $("<div><%=Mui.ConfirmDeleteQuestion %></div>").dialog({width:450, height:200,
                buttons: [
                { text: "<%=Mui.Yes %>", click: function() { $('#<%=jsArg.ClientID %>').val(id); $('#<%=jsDelPost.ClientID %>').click(); } },
                { text: "<%=Mui.No %>", click: function() { $(this).dialog('close'); } }]
            });
        }
        
        function edit(id) {
            $('#<%=jsArg.ClientID %>').val(id);
            $('#<%=jsEditPost.ClientID %>').click();
        }
    </script>
<div style="display: none;">
<asp:Button ID="jsDelPost" runat="server" OnClick="Delete"/>
<asp:Button ID="jsEditPost" runat="server" OnClick="Edit"/>
<asp:HiddenField runat="server" ID="jsArg" />
</div>
</asp:Content>
