<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VentasFormulario.aspx.cs" Inherits="Comercio_Web.VentasFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card shadow-sm border-0">
        <div class="card-body p-4">
            <h1 class="h3 mb-4">Nueva Venta</h1>

            <div class="row g-3 mb-2">
                <div class="col-md-3">
                    <label class="form-label">N° Factura</label>
                    <asp:TextBox ID="txtNumeroFactura" runat="server" CssClass="form-control" placeholder="Ej: 1001" />
                </div>
                <div class="col-md-5">
                    <label class="form-label">Cliente</label>
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select" />
                </div>
                <div class="col-md-4">
                    <label class="form-label">Usuario</label>
                    <asp:Label ID="lblUsuarioVenta" runat="server" CssClass="form-control d-block" />
                </div>
            </div>

            <hr class="my-3" />

            <p class="fw-semibold mb-2">Agregar producto</p>
            <div class="row g-2 align-items-end mb-3">
                <div class="col-md-6">
                    <label class="form-label">Producto</label>
                    <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged" />
                </div>
                <div class="col-md-2">
                    <label class="form-label">Cantidad</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Text="1" />
                </div>
                <div class="col-md-2">
                    <label class="form-label">Precio unit.</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" Text="0,00" ReadOnly="true" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnAgregarLinea" runat="server" Text="Agregar" CssClass="btn btn-primary w-100" OnClick="btnAgregarLinea_Click" />
                </div>
            </div>

            <asp:GridView ID="dgvLineas" runat="server" DataKeyNames="IdProducto"
                CssClass="table table-bordered mb-1" AutoGenerateColumns="false"
                OnRowCommand="dgvLineas_RowCommand"
                EmptyDataText="No hay productos agregados">
                <EmptyDataRowStyle CssClass="text-center text-muted py-3" />
                <Columns>
                    <asp:BoundField HeaderText="Producto" DataField="ProductoNombre" />
                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                    <asp:BoundField HeaderText="Precio Unitario" DataField="PrecioUnitario" DataFormatString="{0:N2}" />
                    <asp:BoundField HeaderText="Subtotal" DataField="PrecioTotalDelProducto" DataFormatString="{0:N2}" />
                    <asp:TemplateField HeaderText="Herramientas">
                        <ItemTemplate>
                            <asp:Button ID="btnQuitar" runat="server" Text="Quitar" CommandName="Quitar" CommandArgument='<%# Eval("IdProducto") %>' CssClass="btn btn-sm btn-outline-danger" />
                            <asp:Button ID="btnRestar" runat="server" Text="-1" CommandName="Restar" CommandArgument='<%# Eval("IdProducto") %>' CssClass="btn btn-sm btn-warning me-1" />
                            <asp:Button ID="btnSumar" runat="server" Text="+1" CommandName="Sumar" CommandArgument='<%# Eval("IdProducto") %>' CssClass="btn btn-sm btn-warning" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mt-1" />

            <div class="d-flex justify-content-between align-items-center mt-3">
                <div>
                    <span class="text-muted me-1">Total:</span>
                    <span class="fs-5 fw-bold">$ <asp:Label ID="lblTotal" runat="server" Text="0,00" /></span>
                </div>
                <div class="d-flex gap-2">
                    <a href="VentasLista.aspx" class="btn btn-secondary">Cancelar</a>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Venta" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
