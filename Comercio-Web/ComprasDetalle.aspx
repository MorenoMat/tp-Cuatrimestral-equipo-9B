<%@ Page Title="Detalle de Compra" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ComprasDetalle.aspx.cs" Inherits="Comercio_Web.ComprasDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h1 class="mb-0">Detalle de Compra</h1>
        <asp:HyperLink ID="lnkVolver" runat="server" CssClass="btn btn-outline-secondary" Text="Volver" />
    </div>

    <div class="card shadow-sm border-0 mb-3">
        <div class="card-body">
            <asp:Label ID="lblDetalleCompraTitulo" runat="server" CssClass="fw-semibold d-block mb-3" />
            <div class="row g-3">
                <div class="col-md-6">
                    <span class="text-muted d-block">Total de la compra</span>
                    <asp:Label ID="lblTotalValor" runat="server" CssClass="fw-semibold" />
                </div>
            </div>
        </div>
    </div>

    <div class="card shadow-sm border-0">
        <div class="card-body p-0">
            <div class="table-responsive">
                <asp:GridView ID="dgvDetalleCompra" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped mb-0 align-middle">
                    <Columns>
                        <asp:BoundField HeaderText="Producto" DataField="ProductoNombre" />
                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                        <asp:BoundField HeaderText="Precio Unitario" DataField="PrecioUnitario" DataFormatString="{0:N2}" />
                        <asp:BoundField HeaderText="Subtotal" DataField="PrecioTotalDelProducto" DataFormatString="{0:N2}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
