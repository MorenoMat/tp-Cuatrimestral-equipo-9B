<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Comercio_Web.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Comercio Web — Iniciar Sesión</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light" style="background-image: radial-gradient(#cdd6ff 1px, transparent 1px); background-size: 32px 32px;">
    <form id="form1" runat="server">
        <div class="container min-vh-100 d-flex flex-column py-4">
            <div class="flex-grow-1 d-flex justify-content-center align-items-start align-items-md-center">
                <div class="card border-0 shadow-sm" style="max-width: 460px; width: 100%;">
                    <div class="card-body p-4 p-md-5">
                        <h2 class="h1 fw-bold mb-2">Bienvenido</h2>
                        <p class="text-muted mb-4">Ingresa tus credenciales para acceder</p>

                        <div class="mb-3">
                            <label class="form-label fw-semibold text-uppercase small text-secondary">Usuario</label>
                            <asp:TextBox ID="txtUsuarioLogin" runat="server" CssClass="form-control form-control-lg" placeholder="Ingresa tu usuario" />
                        </div>

                        <div class="mb-3">
                            <label class="form-label fw-semibold text-uppercase small text-secondary">Contraseña</label>
                            <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control form-control-lg" placeholder="••••••••" />
                        </div>

                        <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3" Visible="false" />

                        <asp:Button ID="btnIngresar" runat="server" Text="Iniciar Sesión  →"
                            CssClass="btn btn-primary btn-lg w-100 fw-semibold py-2" OnClick="btnIngresar_Click" />

                        <hr class="my-4" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
