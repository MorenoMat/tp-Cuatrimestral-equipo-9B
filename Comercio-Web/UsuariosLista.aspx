<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="UsuariosLista.aspx.cs" Inherits="Comercio_Web.UsuariosLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h1 class="mb-0">Usuarios</h1>
        <a href="UsuariosFormulario.aspx" class="btn btn-primary">Agregar Usuario</a>
    </div>
    <div class="card shadow-sm border-0 mb-3">
        <div class="card-body">
            <asp:Panel runat="server" DefaultButton="btnBuscar">
                <div class="row g-2 align-items-end">
                    <div class="col-md-8">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por usuario o nombre" />
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
                <asp:GridView ID="dgvUsuarios" runat="server" DataKeyNames="IdUsuario"
                    CssClass="table table-bordered table-hover table-striped mb-0 align-middle" AutoGenerateColumns="false"
                    OnSelectedIndexChanged="dgvUsuarios_SelectedIndexChanged" ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:BoundField HeaderText="Usuario" DataField="UsuarioLogin" />
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:CheckBoxField HeaderText="Admin" DataField="esAdmin" />
                        <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Editar Usuario" />
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
