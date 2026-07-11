<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VentasLista.aspx.cs" Inherits="Comercio_Web.VentasLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h1 class="mb-0">Ventas</h1>
    </div>
    <div class="card shadow-sm border-0 mb-3">
        <div class="card-body">
            <asp:Panel runat="server" DefaultButton="btnBuscar">
                <div class="row g-2 align-items-end">
                    <div class="col-md-8">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por número de venta" />
                    </div>
                    <div class="col-md-4 d-flex gap-2">
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
                <asp:GridView ID="dgvVentas" runat="server" DataKeyNames="IdVenta"
                    CssClass="table table-bordered table-hover table-striped mb-0 align-middle" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="N° Venta" DataField="IdVenta" />
                        <asp:BoundField HeaderText="N° Factura" DataField="NumeroFactura" />
                        <asp:BoundField HeaderText="Cliente" DataField="Cliente.Nombre" />
                        <asp:BoundField HeaderText="Usuario" DataField="Usuario.Nombre" />
                        <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:N2}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
