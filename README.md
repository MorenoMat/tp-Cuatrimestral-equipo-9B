# tp-Cuatrimestral-equipo-9B
Comercio -> Compras y Ventas

Una aplicación web para gestionar las compras y ventas de un negocio multipropósito.
Qué hace:

Administra clientes, proveedores, productos (con marcas y categorías)
Registra compras a proveedores → actualiza stock y guarda precio de compra
Registra ventas a clientes → valida stock, descuenta stock, genera factura
Los precios de venta se calculan como: costo reciente + % ganancia
Dos perfiles: Administrador (todo) y Vendedor (solo ventas)



nota: cree un DetalleVenta, va a ver uno por cada producto que haya en una Venta, le agregue un NombreProducto para despues cargarselo ahi directamente.
guarde UltimoPrecio en  producto para no tener q crear otra tabla mas o agregarlo en otra tabla,
