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
                    <div class="col-md-6">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre, teléfono, mail o CUIT" />
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
                <asp:GridView ID="dgvProveedores" runat="server" DataKeyNames="IdProveedor"
                    CssClass="table table-bordered table-hover table-striped mb-0 align-middle" AutoGenerateColumns="false"
                    OnSelectedIndexChanged="dgvProveedores_SelectedIndexChanged" ShowHeaderWhenEmpty="true">
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
        <div class="card-footer d-flex justify-content-between align-items-center flex-wrap gap-3">
            <asp:Label ID="lblPaginacion" runat="server" CssClass="text-muted mb-0" />
            <div class="d-flex align-items-center gap-2">
                <span class="text-muted">Mostrar</span>
                <asp:DropDownList ID="ddlTamanioPagina" runat="server" CssClass="form-select form-select-sm w-auto" AutoPostBack="true" OnSelectedIndexChanged="ddlTamanioPagina_SelectedIndexChanged">
                    <asp:ListItem Text="5" Value="5" />
                    <asp:ListItem Text="10" Value="10" Selected="True" />
                    <asp:ListItem Text="20" Value="20" />
                    <asp:ListItem Text="50" Value="50" />
                </asp:DropDownList>
                <span class="text-muted">resultados</span>
            </div>
            <div class="d-flex align-items-center gap-2">
                <asp:LinkButton ID="btnAnterior" runat="server" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnAnterior_Click">&lt;</asp:LinkButton>
                <asp:Repeater ID="rptPaginas" runat="server" OnItemCommand="rptPaginas_ItemCommand">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnPagina" runat="server" CssClass='<%# (bool)Eval("Actual") ? "btn btn-primary btn-sm" : "btn btn-outline-secondary btn-sm" %>' CommandName="IrAPagina" CommandArgument='<%# Eval("Numero") %>'><%# Eval("Numero") %></asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:LinkButton ID="btnSiguiente" runat="server" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnSiguiente_Click">&gt;</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
