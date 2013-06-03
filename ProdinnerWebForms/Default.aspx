<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProdinnerWebForms.Default" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%if (Msg != null)
      {%>
    <div class="msg">
        <%=Msg %></div>
    <%} %>
    <div class="topmenu">
        <button type="button" id="bAdd" class="bt">
            <%=Mui.Add %></button>
    </div>
    <div id="f1" class="inputform" style="display: none">
        <asp:HiddenField runat="server" ID="eId" />
        <div class="efield">
            <div class="elabel">
               <%=Mui.Name %>:</div>
            <div class="einput">
                <asp:TextBox runat="server" ID="eName" ValidationGroup="g1"></asp:TextBox></div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="eName" ValidationGroup="g1"
                ErrorMessage="<%$ Resources:Mui, FieldReq %>"></asp:RequiredFieldValidator>
        </div>
        <div class="efield">
            <div class="elabel">
                <%=Mui.Chef %>:</div>
            <div class="einput">
                <o:AjaxDropdown runat="server" ID="eChef" ValidationGroup="g1" Url="~/svc/Aja.svc/ChefsDropdown">
                </o:AjaxDropdown>
            </div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="eChef" ValidationGroup="g1"
                ErrorMessage="<%$ Resources:Mui, FieldReq %>"></asp:RequiredFieldValidator>
        </div>
        <div class="efield">
            <div class="elabel">
                <%=Mui.Country %>:</div>
            <div class="einput">
                <o:Lookup runat="server" ID="eCountry" ValidationGroup="g1" SearchUrl="~/svc/Aja.svc/CountriesSearch"
                    GetUrl="~/svc/Aja.svc/CountryGet"></o:Lookup>
            </div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="eCountry" ValidationGroup="g1"
                ErrorMessage="<%$ Resources:Mui, FieldReq %>"></asp:RequiredFieldValidator>
        </div>
        <div class="efield">
            <div class="elabel">
                <%=Mui.Meals %>:</div>
            <div class="einput">
                <o:MultiLookup runat="server" ID="eMeals" ValidationGroup="g1" GetMultipleUrl="~/svc/Aja.svc/MealGetMultiple"
                    SearchUrl="~/svc/Aja.svc/MealSearch" SelectedUrl="~/svc/Aja.svc/MealSelected">
                </o:MultiLookup>
            </div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="eMeals" ValidationGroup="g1"
                ErrorMessage="<%$ Resources:Mui, FieldReq %>"></asp:RequiredFieldValidator>
        </div>
        <div class="efield">
            <div class="elabel">
                <%=Mui.Date %>:</div>
            <div class="einput">
                <asp:TextBox runat="server" ID="eDate" ValidationGroup="g1" CssClass="dt"></asp:TextBox></div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="eDate" ValidationGroup="g1"
                ErrorMessage="<%$ Resources:Mui, FieldReq %>"></asp:RequiredFieldValidator>
        </div>
        <div class="efield ebtns">
            <asp:Button runat="server" ValidationGroup="g1" Text="<%$ Resources:Mui, Save %>" OnClick="Save" CssClass="bt" />
            <button type="button" id="bCancel" class="bt">
                <%=Mui.Cancel %></button>
        </div>
    </div>
    <br />
    <fieldset id="sform">
        <legend><%=Mui.Search %></legend>
        <div class="sfield">
            <%=Mui.Name %>:
            <input type="text" id="txtSearch" name="search" /></div>
        <div class="sfield">
            <%=Mui.Chef %>:
            <o:AjaxDropdown runat="server" ID="sChef" Url="~/svc/Aja.svc/ChefsDropdown"></o:AjaxDropdown>
        </div>
        <div class="sfield">
            <div class="sfield">
                <%=Mui.Meals %>:</div>
            <o:MultiLookup runat="server" ID="sMeals" GetMultipleUrl="~/svc/Aja.svc/MealGetMultiple"
                SearchUrl="~/svc/Aja.svc/MealSearch" SelectedUrl="~/svc/Aja.svc/MealSelected"
                ClearButton="true"></o:MultiLookup>
        </div>
        <div class="sfield" style="margin-top: -3px;">
            <button type="button" id="bSearch" class="bt">
                <%=Mui.Search %></button></div>
        <div class="sfield fr">
            <%=Mui.PageSize %>:
            <select id="pageSize">
                <option value="1">1</option>
                <option value="5" selected="selected">5</option>
                <option value="10">10</option>
                <option value="30">30</option>
                <option value="100">100</option>
            </select>
        </div>
        <div class="fr" style="margin-right: 5em;">
            <div class="sfield">
                <%=Mui.SortBy %>:
                <select id="sortBy">
                    <option>---</option>
                    <option value="date"><%=Mui.Date %></option>
                    <option value="name"><%=Mui.Name %></option>
                    <option value="meals"><%=Mui.MealsCount %></option>
                </select>
            </div>
            <div class="sfield" style="margin-top: -3px;">
                <o:AjaxRadioList ID="ascDesc" runat="server" Url="~/svc/Aja.svc/AscDesc" CssClass="hli" Value="asc" />
            </div>
        </div>
    </fieldset>
    <%
        lstItems.SearchButton = "bSearch";
        lstItems.Data = new Dictionary<string, string> {
        {"search", "txtSearch"},
        {"chef", sChef.ClientID},
        {"meals", sMeals.ClientID},
        {"orderBy", "sortBy"},
        {"ascDesc", ascDesc.ClientID},
        {"pageSize", "pageSize"},
        };
    %>
    <o:AjaxList runat="server" ID="lstItems" SearchUrl="~/svc/Aja.svc/DinnersSearch" CssClass="dinnersList" />

    <script type="text/javascript">
        $(function() {
            $('#bAdd').click(function() { 
                clearForm("#f1");
                $('#f1').show(); 
            });
            $('#bCancel').click(function() { $('#f1').hide(); });
            
            $("#sform input[type='hidden'], #sortBy, #pageSize").change(function() {
                $('#bSearch').click();
            });
            
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
        <asp:Button ID="jsDelPost" runat="server" OnClick="Delete" />
        <asp:Button ID="jsEditPost" runat="server" OnClick="Edit" />
        <asp:HiddenField runat="server" ID="jsArg" />
    </div>
</asp:Content>
