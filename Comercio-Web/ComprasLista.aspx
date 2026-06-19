<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ComprasLista.aspx.cs" Inherits="Comercio_Web.ComprasLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Compras</h1>
    <asp:GridView ID="dgvCompras" runat="server" DataKeyNames="IdCompra"
        CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="N° Compra" DataField="IdCompra" />
            <asp:BoundField HeaderText="Fecha" DataField="FechaCompra" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField HeaderText="Proveedor" DataField="Proveedor.Nombre" />
            <asp:BoundField HeaderText="Usuario" DataField="Usuario.Nombre" />
        </Columns>
    </asp:GridView>
    <a href="ComprasFormulario.aspx" class="btn btn-primary">Nueva Compra</a>
</asp:Content>
