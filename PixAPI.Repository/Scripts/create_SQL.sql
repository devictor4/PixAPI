create table Usuario 
(
	id bigint primary key identity(1, 1) not null,
	email varchar(50) not null,
	senha varchar(100) not null,
	nome varchar(50) not null,
	tipoDocumento int not null,
	documento varchar(14) not null,
	dddCelular smallint not null,
	celular bigint not null,
	dataInclusao datetime not null,
	dataAlteracao datetime null,
	dataExclusao datetime null,
	isExcluido bit not null
)

create table TipoChavePix 
(
	id bigint primary key identity(1, 1) not null,
	tipo varchar(16) not null
)

create table ChavePix 
(
	id bigint primary key identity(1, 1) not null,
	idTipo bigint not null,
	chave varchar(36) not null

	foreign key (idTipo) references TipoChavePix(id)
)

create table UsuarioChavePix 
(
	id bigint primary key identity(1, 1) not null,
	idUsuario bigint not null,
	idChavePix bigint not null,

	foreign key (idUsuario) references Usuario(id),
	foreign key (idChavePix) references ChavePix(id)
)

