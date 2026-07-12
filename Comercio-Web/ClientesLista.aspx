<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ClientesLista.aspx.cs" Inherits="Comercio_Web.ClientesLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h1 class="mb-0">Clientes</h1>
        <a href="ClientesFormulario.aspx" class="btn btn-primary">Agregar Cliente</a>
    </div>
    <div class="card shadow-sm border-0 mb-3">
        <div class="card-body">
            <asp:Panel runat="server" DefaultButton="btnBuscar">
                <div class="row g-2 align-items-end">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por DNI, nombre o mail" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblFiltroEstado" runat="server" Text="Filtrar por estado" CssClass="form-label" />
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                            <asp:ListItem Text="TODOS" Value="" />
                            <asp:ListItem Text="Activos" Value="true" />
                            <asp:ListItem Text="Inactivos" Value="false" />
                        </asp:DropDownList>
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
                <asp:GridView ID="dgvClientes" runat="server" DataKeyNames="IdCliente"
                    CssClass="table table-bordered table-hover table-striped mb-0 align-middle" AutoGenerateColumns="false"
                    OnSelectedIndexChanged="dgvClientes_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="DNI" DataField="Dni" />
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:BoundField HeaderText="Email" DataField="Email" />
                        <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Editar Clientes" />
                        <asp:TemplateField HeaderText="Cliente Activo">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEstadoCliente" runat="server" Checked='<%# Eval("Activo") %>' AutoPostBack="true" OnCheckedChanged="chkAccion_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
