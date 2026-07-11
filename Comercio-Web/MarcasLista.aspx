<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MarcasLista.aspx.cs" Inherits="Comercio_Web.MarcasLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h1 class="mb-0">Marcas</h1>
        <a href="MarcasFormulario.aspx" class="btn btn-primary">Agregar Marca</a>
    </div>
    <div class="card shadow-sm border-0 mb-3">
        <div class="card-body">
            <asp:Panel runat="server" DefaultButton="btnBuscar">
                <div class="row g-2 align-items-end">
                    <div class="col-md-8">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar marca por descripción" />
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
                <asp:GridView ID="dgvMarcas" runat="server" DataKeyNames="IdMarca"
                    CssClass="table table-bordered table-hover table-striped mb-0 align-middle" AutoGenerateColumns="false"
                    OnSelectedIndexChanged="dgvMarcas_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                        <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Editar Marca" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
