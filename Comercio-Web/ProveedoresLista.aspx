<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProveedoresLista.aspx.cs" Inherits="Comercio_Web.ProveedoresLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h1 class="mb-0">Proveedores</h1>
        <a href="ProveedoresFormulario.aspx" class="btn btn-primary">Agregar Proveedor</a>
    </div>
    <div class="card shadow-sm border-0 mb-3">
        <div class="card-body">
            <asp:Panel runat="server" DefaultButton="btnBuscar">
                <div class="row g-2 align-items-end">
                    <div class="col-md-8">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre, teléfono, mail o CUIT" />
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
                <asp:GridView ID="dgvProveedores" runat="server" DataKeyNames="IdProveedor"
                    CssClass="table table-bordered table-hover table-striped mb-0 align-middle" AutoGenerateColumns="false"
                    OnSelectedIndexChanged="dgvProveedores_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
                        <asp:BoundField HeaderText="Email" DataField="Email" />
                        <asp:BoundField HeaderText="CUIT o CUIL" DataField="Cuit" />
                        <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Editar Proveedores" />
                        <asp:TemplateField HeaderText="Proveedor Activo">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEstadoProveedor" runat="server" Checked='<%# Eval("Activo") %>' AutoPostBack="true" OnCheckedChanged="chkAccion_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
