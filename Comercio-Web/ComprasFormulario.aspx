<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ComprasFormulario.aspx.cs" Inherits="Comercio_Web.ComprasFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <script>
        $(function () {
            $('#<%= ddlProveedor.ClientID %>').select2({
                width: '100%'
            });
            $('#<%= ddlProducto.ClientID %>').select2({
                width: '100%'
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card shadow-sm border-0">
        <div class="card-body p-4">
            <h1 class="h3 mb-4">Nueva Compra</h1>

            <div class="row mb-3">
                <div class="col-md-4">
                    <label class="form-label">Proveedor</label>
                    <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged" />
                </div>
                <div class="col-md-4">
                    <label class="form-label">Usuario</label>
                    <asp:Label ID="lblUsuarioCompra" runat="server" CssClass="form-control" />
                </div>
            </div>

            <h4 class="h5 mb-3">Agregar producto</h4>
            <div class="row mb-3 align-items-end">
                <div class="col-md-4">
                    <label class="form-label">Producto</label>
                    <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged" />
                </div>
                <div class="col-md-2">
                    <label class="form-label">Cantidad</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Text="1" />
                </div>
                <div class="col-md-2">
                    <label class="form-label">Precio unitario</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" Text="0" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnAgregarLinea" runat="server" Text="Agregar" CssClass="btn btn-secondary w-100"
                        OnClick="btnAgregarLinea_Click" />
                </div>
                <div class="col-md-2">
                    <div>
                        <span class="text-muted me-1">Stock:</span>
                        <span class="fs-5 fw-bold"><asp:Label ID="lblStock" runat="server" Text="0,00" /></span>
                    </div>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="dgvLineas" runat="server" DataKeyNames="IdProducto"
                    CssClass="table table-bordered table-sm table-striped mb-0 align-middle" AutoGenerateColumns="false"
                    OnRowCommand="dgvLineas_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Producto" DataField="ProductoNombre" />
                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                        <asp:BoundField HeaderText="Precio Unit." DataField="PrecioUnitario" DataFormatString="{0:N2}" />
                        <asp:BoundField HeaderText="Precio Total por Producto" DataField="PrecioTotalDelProducto" DataFormatString="{0:N2}" />
                        <asp:TemplateField HeaderText="Herramientas">
                            <ItemTemplate>
                                <asp:Button ID="btnQuitar" runat="server" Text="Quitar" CommandName="Quitar" CssClass="btn btn-sm btn-danger" CommandArgument='<%# Eval("IdProducto") %>' />
                                <asp:Button ID="btnRestar" runat="server" Text="-1" CommandName="Restar" CssClass="btn btn-sm btn-warning me-1" CommandArgument='<%# Eval("IdProducto") %>' />
                                <asp:Button ID="btnSumar" runat="server" Text="+1" CommandName="Sumar" CssClass="btn btn-sm btn-warning" CommandArgument='<%# Eval("IdProducto") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="mt-3">
                <strong>Total: $</strong>
                <asp:Label ID="lblTotal" runat="server" Text="0,00" CssClass="fw-bold" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" />
            <div class="d-flex gap-2 mt-3">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Compra" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                <a href="ComprasLista.aspx" class="btn btn-secondary">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>
