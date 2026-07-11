<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProductosLista.aspx.cs" Inherits="Comercio_Web.ProductosLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <script>
        $(function () {
            $('#<%= ddlMarca.ClientID %>').select2({
                width: '100%'
            });
            $('#<%= ddlCategoria.ClientID %>').select2({
                width: '100%'
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h1 class="mb-0">Productos</h1>
        <a href="ProductosFormulario.aspx" class="btn btn-primary">Agregar Producto</a>
    </div>
    <div class="card shadow-sm border-0 mb-3">
        <div class="card-body">
            <asp:Panel runat="server" DefaultButton="btnBuscar">
                <div class="row g-2 align-items-end">
                    <div class="col-md-3">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar producto por nombre" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblFiltroMarca" runat="server" Text="Filtrar por marca" CssClass="form-label" />
                        <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblFiltroCategoria" runat="server" Text="Filtrar por categoría" CssClass="form-label" />
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
                    </div>
                    <div class="col-md-3 d-flex gap-2">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-secondary" OnClick="btnBuscar_Click" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnLimpiar_Click" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div class="card shadow-sm border-0">
        <div class="card-body p-0">
            <div class="table-responsive">
                <asp:GridView ID="dgvProductos" runat="server" DataKeyNames="IdProducto"
                    CssClass="table table-bordered table-hover table-striped mb-0 align-middle" AutoGenerateColumns="false"
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
            </div>
        </div>
    </div>
</asp:Content>
