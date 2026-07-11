<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ClientesLista.aspx.cs" Inherits="Comercio_Web.ClientesLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h1 class="mb-0">Clientes</h1>
        <a href="ClientesFormulario.aspx" class="btn btn-primary">Agregar Cliente</a>
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
