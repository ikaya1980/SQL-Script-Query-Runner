<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication10._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .Hide { display:none; }

    </style>
    <div class="jumbotron">
        
        <br />
        Personel<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
            <Columns>
                <asp:BoundField  DataField="BusinessEntityID" HeaderStyle-CssClass="Hide" ItemStyle-CssClass="Hide"/>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                <asp:BoundField DataField="BirthDate" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Birth Date" />
                <asp:BoundField DataField="JobTitle" HeaderText="Job Title" />
            </Columns>
        </asp:GridView>
        <asp:DropDownList ID="dropListGemi" runat="server" Width="336px">
            <asp:ListItem Text="Nusrat" Selected="True"  Enabled="true" Value="1"></asp:ListItem>
            <asp:ListItem Text="Yıldırım" Value="2"></asp:ListItem>
            <asp:ListItem Text="Ece" Value="3"></asp:ListItem>
        </asp:DropDownList>
        <br />
        Çalışma Saati :
        <asp:TextBox ID="txtHour" runat="server" MaxLength="2"></asp:TextBox>
        <br />
        Çalışma Tarihi:
        <asp:Calendar ID="calendarWorkDate" runat="server"></asp:Calendar>
        <br />
        <asp:Button ID="btnKaydet" runat="server" OnClick="btnKaydet_Click" Text="KAYDET" />
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetDataTableFromQuery" TypeName="DB.ExecuteQuery">
            <SelectParameters>
                <asp:Parameter DefaultValue="select  emp.BusinessEntityID, FirstName, LastName, JobTitle, BirthDate from Person.Person p JOIN HumanResources.Employee emp on p.BusinessEntityID = emp.BusinessEntityID" Name="query" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>

</asp:Content>
