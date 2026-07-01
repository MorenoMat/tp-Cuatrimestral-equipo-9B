<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VentasFormulario.aspx.cs" Inherits="Comercio_Web.VentasFormulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Nueva Venta</h1>

    <div class="row mb-3">
        <div class="col-md-3">
            <label class="form-label">N° Factura</label>
            <asp:TextBox ID="txtNumeroFactura" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-3">
            <label class="form-label">Cliente</label>
            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select" />
        </div>
        <div class="col-md-3">
            <label class="form-label">Usuario</label>
            <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-select" />
        </div>
        <div class="col-md-3">
            <label class="form-label">Estado</label>
            <asp:CheckBox ID="chkEstado" runat="server" Text=" Activa" CssClass="form-check-input ms-2" Checked="true" />
        </div>
    </div>

    <h4>Agregar producto</h4>
    <div class="row mb-3">
        <div class="col-md-4">
            <label class="form-label">Producto</label>
            <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select" AutoPostBack ="true" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged" />
        </div>
        <div class="col-md-2">
            <label class="form-label">Cantidad</label>
            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Text="1" />
        </div>
        <div class="col-md-2">
            <label class="form-label">Precio unitario</label>
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" Text="0" ReadOnly="true" BackColor="lightgray" />
        </div>
        <div class="col-md-2 align-self-end">
            <asp:Button ID="btnAgregarLinea" runat="server" Text="Agregar" CssClass="btn btn-secondary"
                OnClick="btnAgregarLinea_Click" />
        </div>
    </div>

    <asp:GridView ID="dgvLineas" runat="server" DataKeyNames="IdProducto"
        CssClass="table table-bordered table-sm" AutoGenerateColumns="false"
        OnRowCommand="dgvLineas_RowCommand">
        <Columns>
            <asp:BoundField HeaderText="Producto" DataField="ProductoNombre" />
            <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
            <asp:BoundField HeaderText="Precio Unit." DataField="PrecioUnitario" DataFormatString="{0:N2}" />
          
            <asp:ButtonField HeaderText="" Text="Quitar" CommandName="Quitar" ButtonType="Button"
                ControlStyle-CssClass="btn btn-sm btn-danger" />
        </Columns>
    </asp:GridView>

    <div class="mt-2">
        <strong>Total: $</strong>
        <asp:Label ID="lblTotal" runat="server" Text="0,00" CssClass="fw-bold" />
    </div>

    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mt-2" />

    <div class="mt-3">
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Venta" CssClass="btn btn-success"
            OnClick="btnGuardar_Click" />
        <a href="VentasLista.aspx" class="btn btn-secondary ms-2">Cancelar</a>
    </div>
</asp:Content>
