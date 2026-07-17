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
                    <div class="fs-3 fw-semibold">$ 0,00</div>
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
                    <div class="fs-3 fw-semibold">$ 0,00</div>
                </div>
            </div>
        </div>

        <div class="col-12 col-md-6 col-xl-3">
            <div class="card reportes-kpi-card h-100">
                <div class="card-body p-3">
                    <div class="mb-3">
                        <span class="reportes-kpi-icon" style="background:#fff3cd;color:#8a6d3b;">⚠</span>
                    </div>
                    <div class="text-uppercase text-muted small fw-semibold">Productos con bajo stock</div>
                    <div class="fs-3 fw-semibold">0 productos</div>
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
                    <div class="fs-3 fw-semibold">0 ventas</div>
                </div>
            </div>
        </div>
    </div>

    <div class="row g-3">
        <div class="col-12 col-xl-6">
            <div class="card h-100">
                <div class="card-header"><strong>Tendencia semanal (mockup)</strong></div>
                <div class="card-body">
                    <div class="border rounded bg-light d-flex align-items-center justify-content-center" style="height: 220px;">
                        <span class="text-muted">Próximamente: gráfico de facturación</span>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-12 col-xl-6">
            <div class="card h-100">
                <div class="card-header"><strong>Top categorías (mockup)</strong></div>
                <div class="card-body">
                    <div class="border rounded bg-light d-flex align-items-center justify-content-center" style="height: 220px;">
                        <span class="text-muted">Próximamente: ranking de categorías</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
