<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Comercio_Web.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Comercio Web — Iniciar Sesión</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="d-flex justify-content-center align-items-center" style="min-height: 100vh;">
            <div class="card shadow" style="width: 380px;">
                <div class="card-header bg-dark text-white text-center py-3">
                    <h4 class="mb-0">Comercio Web</h4>
                </div>
                <div class="card-body p-4">
                    <h5 class="card-title mb-4">Iniciar Sesión</h5>
                    <div class="mb-3">
                        <label class="form-label">Usuario</label>
                        <asp:TextBox ID="txtUsuarioLogin" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control" />
                    </div>
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3" Visible="false" />
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar"
                        CssClass="btn btn-dark w-100" OnClick="btnIngresar_Click" />
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
