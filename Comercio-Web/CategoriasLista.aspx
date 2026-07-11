<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CategoriasLista.aspx.cs" Inherits="Comercio_Web.CategoriasLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h1 class="mb-0">Categorías</h1>
        <a href="CategoriasFormulario.aspx" class="btn btn-primary">Agregar Categoría</a>
    </div>
    <div class="card shadow-sm border-0 mb-3">
        <div class="card-body">
            <asp:Panel runat="server" DefaultButton="btnBuscar">
                <div class="row g-2 align-items-end">
                    <div class="col-md-8">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar categoría por descripción" />
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
                <asp:GridView ID="dgvCategorias" runat="server" DataKeyNames="IdCategoria"
                    CssClass="table table-bordered table-hover table-striped mb-0 align-middle" AutoGenerateColumns="false"
                    OnSelectedIndexChanged="dgvCategorias_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                        <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Editar Categoria" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
