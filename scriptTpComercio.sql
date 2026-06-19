
create table Marcas(
idMarca int primary key identity(1,1),
descripcion varchar(30) not null
)
create table Categorias(
idCategoria int primary key identity(1,1),
descripcion varchar(30) not null
)

create table Productos(
idProducto int primary key identity(1,1),
nombre varchar(30)  not null,
ultimoPrecio decimal(10,2)  not null default 0,
porcentajeGanancia decimal(4,2)  not null,
stockActual int not null default 0,
stockMinimo int  not null default 0,
descripcion varchar(50)  null,
idMarca int  not null,
idCategoria int   not null

CONSTRAINT FK_Productos_Marcas FOREIGN KEY (idMarca) REFERENCES Marcas(idMarca),
CONSTRAINT FK_Productos_Categorias FOREIGN KEY (idCategoria) REFERENCES Categorias(idCategoria)
)

create table Clientes(
idCliente int primary key identity(1,1),
 dni int not null,
 nombre int not null,
 email varchar(50)
)

create table Usuarios(
idUsuario int primary key identity(1,1),
nombre varchar(30) not null,
usuarioLogin varchar(15) unique not null,
contraseña varchar(15) not null,
administrador bit not null default 0,
)

create table Proveedores(
idProveedor int primary key identity(1,1),
 dni int not null,
 nombre varchar(40) not null,
 Telefono varchar(25)
 email varchar(50)

)
