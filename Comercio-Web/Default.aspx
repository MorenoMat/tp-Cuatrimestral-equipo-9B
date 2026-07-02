<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Comercio_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row g-3 align-items-start">

        <%-- Primer tercio: Alertas de stock --%>
        <div class="col-lg-4">
            <div class="card h-100">
                <div class="card-header card-header-stock">
                    <h5 class="mb-0">&#9888; Alertas de Stock</h5>
                </div>
                <div class="card-body">
                    <asp:Panel ID="pnlAlertas" runat="server">
                    </asp:Panel>
                </div>
            </div>
        </div>

        <%-- Dos tercios restantes: Nueva Venta --%>
        <div class="col-lg-8">
            <div class="card">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <h5 class="mb-0">Nueva Venta</h5>
                </div>
                <div class="card-body">

                    <%-- Fila de metadatos --%>
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

                    <%-- Agregar producto --%>
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
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"
                                Text="0,00" ReadOnly="true" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnAgregarLinea" runat="server" Text="Agregar"
                                CssClass="btn btn-primary w-100" OnClick="btnAgregarLinea_Click" />
                        </div>
                    </div>

                    <%-- Tabla de líneas --%>
                    <asp:GridView ID="dgvLineas" runat="server" DataKeyNames="IdProducto"
                        CssClass="table table-bordered mb-1" AutoGenerateColumns="false"
                        OnRowCommand="dgvLineas_RowCommand"
                        EmptyDataText="No hay productos agregados">
                        <EmptyDataRowStyle CssClass="text-center text-muted py-3" />
                        <Columns>
                            <asp:BoundField HeaderText="Producto"       DataField="ProductoNombre" />
                            <asp:BoundField HeaderText="Cantidad"       DataField="Cantidad" />
                            <asp:BoundField HeaderText="Precio Unitario" DataField="PrecioUnitario"        DataFormatString="{0:N2}" />
                            <asp:BoundField HeaderText="Subtotal"       DataField="PrecioTotalDelProducto" DataFormatString="{0:N2}" />
                            <asp:ButtonField HeaderText="Acciones" Text="Quitar" CommandName="Quitar"
                                ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-danger" />
                        </Columns>
                    </asp:GridView>

                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mt-1" />

                </div>
                <div class="card-footer d-flex justify-content-between align-items-center">
                    <div>
                        <span class="text-muted me-1">Total:</span>
                        <span class="fs-5 fw-bold">$ <asp:Label ID="lblTotal" runat="server" Text="0,00" /></span>
                    </div>
                    <div class="d-flex gap-2">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                            CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Venta"
                            CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
