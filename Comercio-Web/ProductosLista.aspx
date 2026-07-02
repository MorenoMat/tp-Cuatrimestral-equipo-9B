<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProductosLista.aspx.cs" Inherits="Comercio_Web.ProductosLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Productos</h1>
    <asp:GridView ID="dgvProductos" runat="server" DataKeyNames="IdProducto"
        CssClass="table table-bordered table-hover" AutoGenerateColumns="false"
        OnSelectedIndexChanged="dgvProductos_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
            <asp:BoundField HeaderText="Stock Actual" DataField="StockActual" />
            <asp:BoundField HeaderText="Stock Mínimo" DataField="StockMinimo" />
            <asp:BoundField HeaderText="% Ganancia" DataField="PorcentajeGanancia" DataFormatString="{0:N2}%" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Editar Producto" />
        </Columns>
    </asp:GridView>
    <a href="ProductosFormulario.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
