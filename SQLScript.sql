create database bdRecibos;
use bdRecibos;

create table users (
id int  not null PRIMARY KEY IDENTITY(1,1),
email VARCHAR(100)  not null,
pass VARCHAR(50)  not null,
name VARCHAR(max)  not null
)

create table proveedores(
id int not null PRIMARY KEY IDENTITY(1,1),
proveedor varchar(max)
)

create table monedas(
id int not null PRIMARY KEY IDENTITY(1,1),
moneda varchar(200),
clave varchar(10)
)

create table recibos(
id int not null PRIMARY KEY IDENTITY(1,1),
consecutivo int not null,
proveedorId int not null FOREIGN KEY  REFERENCES proveedores(id),
monto decimal(10,4) not NULL,
monedaId int not null FOREIGN KEY REFERENCES monedas(id),
fecha datetime not null,
comentario varchar(max)
)


insert into users values ('admin@hotmail.com','1234','admin')
insert into proveedores values ('proveedor 1')
insert into monedas values ('Peso Mexicano','MXN')

select * from users
select * from proveedores
select * from monedas

insert into recibos values (1,1,500,1,getdate(),'Gasolina')

-- delete recibos
-- drop table recibos
