<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Comercio_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card shadow-sm border-0">
        <div class="card-body p-4">
            <h1 class="h3 mb-0">
                <asp:Label ID="lblSaludo" runat="server" />
            </h1>
        </div>
    </div>
</asp:Content>
