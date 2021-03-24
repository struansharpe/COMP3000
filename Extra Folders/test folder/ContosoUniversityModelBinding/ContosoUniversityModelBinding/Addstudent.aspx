<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Addstudent.aspx.cs" Inherits="ContosoUniversityModelBinding.Addstudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" />
<asp:FormView runat="server" ID="addStudentForm"
    ItemType="ContosoUniversityModelBinding.Models.Student" 
    InsertMethod="InsertStudent" DefaultMode="Insert"
    OnCallingDataMethods="addStudentForm_CallingDataMethods"
    RenderOuterTable="false" OnItemInserted="addStudentForm_ItemInserted">
    <InsertItemTemplate>
        <fieldset>
            <ol>
                <asp:DynamicEntity runat="server" Mode="Insert" />
            </ol>
            <asp:Button runat="server" Text="Insert" CommandName="Insert" />
            <asp:Button runat="server" Text="Cancel" CausesValidation="false" OnClick="cancelButton_Click" />
        </fieldset>
    </InsertItemTemplate>
</asp:FormView>
</asp:Content>
