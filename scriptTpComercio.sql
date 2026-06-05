
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
