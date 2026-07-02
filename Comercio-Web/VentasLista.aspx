<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VentasLista.aspx.cs" Inherits="Comercio_Web.VentasLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Ventas</h1>
    <asp:GridView ID="dgvVentas" runat="server" DataKeyNames="IdVenta"
        CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="N° Venta" DataField="IdVenta" />
            <asp:BoundField HeaderText="N° Factura" DataField="NumeroFactura" />
            <asp:BoundField HeaderText="Cliente" DataField="Cliente.Nombre" />
            <asp:BoundField HeaderText="Usuario" DataField="Usuario.Nombre" />
            <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:N2}" />
        </Columns>
    </asp:GridView>
</asp:Content>
