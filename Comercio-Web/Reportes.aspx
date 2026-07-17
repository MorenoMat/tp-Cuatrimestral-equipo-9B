<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="Comercio_Web.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .reportes-hero-title { font-weight: 700; }
        .reportes-kpi-card {
            border: 1px solid #dee2e6;
            border-radius: .75rem;
            transition: transform .15s ease, box-shadow .15s ease;
        }
        .reportes-kpi-icon {
            width: 44px;
            height: 44px;
            border-radius: .5rem;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 1.25rem;
            font-weight: 700;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <h1 class="h2 mb-1 reportes-hero-title">Reportes del negocio</h1>
        <p class="text-muted mb-0">Vista mockup para métricas globales de administración.</p>
    </div>

    <div class="row g-3 mb-4">
        <div class="col-12 col-md-6 col-xl-3">
            <div class="card reportes-kpi-card h-100">
                <div class="card-body p-3">
                    <div class="d-flex align-items-center justify-content-between mb-3">
                        <span class="reportes-kpi-icon" style="background:#e8eeff;color:#2f62ff;">💵</span>
                    </div>
                    <div class="text-uppercase text-muted small fw-semibold">Facturación del día</div>
                    <div class="fs-3 fw-semibold">$ <asp:Label ID="lblFacturacionDia" runat="server" Text="0,00" /></div>
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 col-xl-3">
            <div class="card reportes-kpi-card h-100">
                <div class="card-body p-3">
                    <div class="d-flex align-items-center justify-content-between mb-3">
                        <span class="reportes-kpi-icon" style="background:#eef1f5;color:#6c757d;">📅</span>
                    </div>
                    <div class="text-uppercase text-muted small fw-semibold">Facturación mensual</div>
                    <div class="fs-3 fw-semibold">$ <asp:Label ID="lblFacturacionMes" runat="server" Text="0,00" /></div>
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 col-xl-3">
            <div class="card reportes-kpi-card h-100">
                <div class="card-body p-3">
                    <div class="mb-3">
                        <span class="reportes-kpi-icon" style="background:#ede9ff;color:#5b4abf;">📈</span>
                    </div>
                    <div class="text-uppercase text-muted small fw-semibold">Ventas cerradas del mes</div>
                    <div class="fs-3 fw-semibold"><asp:Label ID="lblVentasMes" runat="server" Text="0" /> ventas</div>
                </div>
            </div>
        </div>
    </div>

    <div class="row g-3">
        <div class="col-12">
            <div class="card h-100">
                <div class="card-header d-flex flex-wrap gap-2 justify-content-between align-items-center">
                    <strong>Top 10 vendedores</strong>
                    <div class="d-flex align-items-center gap-2">
                        <span class="text-muted small">Periodo</span>
                        <asp:DropDownList ID="ddlPeriodoTopVendedores" runat="server" CssClass="form-select form-select-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriodoTopVendedores_SelectedIndexChanged">
                            <asp:ListItem Text="Diario" Value="diario" />
                            <asp:ListItem Text="Mensual" Value="mensual" Selected="True" />
                            <asp:ListItem Text="Anual" Value="anual" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="card-body p-0">
                    <div class="table-responsive">
                        <asp:GridView ID="dgvTopVendedores" runat="server" CssClass="table table-bordered table-striped mb-0 align-middle" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No hay ventas para el período seleccionado.">
                            <Columns>
                                <asp:BoundField HeaderText="Vendedor" DataField="Vendedor" />
                                <asp:BoundField HeaderText="Cantidad de ventas" DataField="CantidadVentas" />
                                <asp:BoundField HeaderText="Total facturado" DataField="TotalFacturado" DataFormatString="$ {0:N2}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
