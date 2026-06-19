<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProductosFormulario.aspx.cs" Inherits="Comercio_Web.ProductosFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1><asp:Label ID="lblTitulo" runat="server" Text="Nuevo Producto" /></h1>
    <div class="row">
        <div class="col-md-6">
            <div class="mb-3">
                <asp:Label Text="Nombre" runat="server" CssClass="form-label" />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Descripción" runat="server" CssClass="form-label" />
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Marca" runat="server" CssClass="form-label" />
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Categoría" runat="server" CssClass="form-label" />
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
            </div>
        </div>
        <div class="col-md-6">
            <div class="mb-3">
                <asp:Label Text="Stock Actual" runat="server" CssClass="form-label" />
                <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" TextMode="Number" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Stock Mínimo" runat="server" CssClass="form-label" />
                <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" TextMode="Number" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Precio de Compra ($)" runat="server" CssClass="form-label" />
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Label Text="% Ganancia" runat="server" CssClass="form-label" />
                <asp:TextBox ID="txtGanancia" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Label Text="Proveedores" runat="server" CssClass="form-label" />
                <div class="border rounded p-2">
                    <asp:CheckBoxList ID="cblProveedores" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger ms-2" OnClick="btnEliminar_Click" Visible="false" />
    <a href="ProductosLista.aspx" class="btn btn-secondary ms-2">Cancelar</a>
</asp:Content>
