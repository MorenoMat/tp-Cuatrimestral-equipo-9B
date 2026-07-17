<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProductosFormulario.aspx.cs" Inherits="Comercio_Web.ProductosFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style> .validation{color: red; font-size:14px}</style>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <script>
        $(function () {
            $('#<%= ddlMarca.ClientID %>').select2({
                width: '100%',
                language: {
                    noResults: function () {
                        return 'No se encontraron resultados';
                    }
                }
            });
            $('#<%= ddlCategoria.ClientID %>').select2({
                width: '100%',
                language: {
                    noResults: function () {
                        return 'No se encontraron resultados';
                    }
                }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlFormularioProducto" runat="server" DefaultButton="btnGuardar" DefaultFocus="txtNombre">
        <div class="card shadow-sm border-0">
            <div class="card-body p-4">
                <h1 class="h3 mb-4"><asp:Label ID="lblTitulo" runat="server" Text="Nuevo Producto" /></h1>
                <div class="row">
                    <div class="col-md-6">
                        <div class="mb-3">
                            <asp:Label Text="Nombre" runat="server" CssClass="form-label" />
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="Rellena el nombre del producto" ControlToValidate="txtNombre" runat="server" CssClass="validation" />
                            <asp:RegularExpressionValidator ErrorMessage="El nombre del producto debe tener entre 3 y 50 caracteres" ControlToValidate="txtNombre" runat="server" CssClass="validation" ValidationExpression="^.{3,50}$" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Text="Descripción" runat="server" CssClass="form-label" />
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"  MaxLength="49"/>
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
                            <asp:RequiredFieldValidator ErrorMessage="Rellena el stock actual" ControlToValidate="txtStockActual" runat="server" CssClass="validation" />
                            <asp:RegularExpressionValidator ErrorMessage="El stock actual debe ser un número entero positivo" ControlToValidate="txtStockActual" runat="server" CssClass="validation" ValidationExpression="^\d+$" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Text="Stock Mínimo" runat="server" CssClass="form-label" />
                            <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" TextMode="Number" MaxLength="5" />
                            <asp:RequiredFieldValidator ErrorMessage="Rellena el stock mínimo" ControlToValidate="txtStockMinimo" runat="server" CssClass="validation" />
                            <asp:RegularExpressionValidator ErrorMessage="El stock mínimo debe ser un número entero positivo" ControlToValidate="txtStockMinimo" runat="server" CssClass="validation" ValidationExpression="^\d+$" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Text="Precio de Compra ($)" runat="server" CssClass="form-label" />
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ErrorMessage="El precio de compra es obligatorio" ControlToValidate="txtPrecio" runat="server" CssClass="validation" />
                            <asp:RegularExpressionValidator ErrorMessage="El precio de compra debe: ser positivo, tener maximo 8 digitos enteros y 2 decimales" ControlToValidate="txtPrecio" runat="server" CssClass="validation" ValidationExpression="^\d{1,8}([,]\d{1,2})?$" />
                            </div>
                        <div class="mb-3">
                            <asp:Label Text="% Ganancia" runat="server" CssClass="form-label" />
                            <asp:TextBox ID="txtGanancia" runat="server" CssClass="form-control"  />
                            <asp:RequiredFieldValidator ErrorMessage="Rellena el porcentaje de ganancia (20%-60% por lo general)" ControlToValidate="txtGanancia" runat="server" CssClass="validation" />
                            <asp:RegularExpressionValidator ErrorMessage="El porcentaje de ganancia debe ser un número válido(0-99)" ControlToValidate="txtGanancia" runat="server" CssClass="validation" ValidationExpression="^\d{1,8}([,]\d{1,2})?$" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Text="Proveedores" runat="server" CssClass="form-label" />
                            <div class="border rounded p-2">
                                <asp:CheckBoxList ID="cblProveedores" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" Visible="false" />
                    <a href="ProductosLista.aspx" class="btn btn-secondary">Cancelar</a>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
