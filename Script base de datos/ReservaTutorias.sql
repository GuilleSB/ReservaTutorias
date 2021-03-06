CREATE DATABASE ReservaTutorias
GO

USE [ReservaTutorias]
GO
/****** Object:  Table [dbo].[Horario]    Script Date: 12/18/2020 3:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horario](
	[IdHorario] [int] IDENTITY(1,1) NOT NULL,
	[IdTutor] [int] NOT NULL,
	[IdTema] [int] NOT NULL,
	[FechaHora] [datetime] NOT NULL,
	[LimiteEstudiantes] [int] NOT NULL,
	[LinkReunion] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdHorario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materia]    Script Date: 12/18/2020 3:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materia](
	[IdMateria] [int] IDENTITY(1,1) NOT NULL,
	[NombreMateria] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMateria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 12/18/2020 3:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserva](
	[IdEstudiante] [int] NOT NULL,
	[IdHorario] [int] NOT NULL,
	[IdReserva] [int] IDENTITY(1,1) NOT NULL,
	[Notas] [nvarchar](1024) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdReserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tema]    Script Date: 12/18/2020 3:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tema](
	[IdTema] [int] IDENTITY(1,1) NOT NULL,
	[NombreTema] [varchar](50) NOT NULL,
	[IdMateria] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTema] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 12/18/2020 3:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [varchar](20) NOT NULL,
	[Nombre] [varchar](30) NOT NULL,
	[Apellidos] [varchar](52) NOT NULL,
	[Correo] [varchar](50) NOT NULL,
	[Clave] [varchar](300) NOT NULL,
	[TipoUsuario] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Horario]  WITH CHECK ADD FOREIGN KEY([IdTema])
REFERENCES [dbo].[Tema] ([IdTema])
GO
ALTER TABLE [dbo].[Horario]  WITH CHECK ADD FOREIGN KEY([IdTutor])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD FOREIGN KEY([IdEstudiante])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD FOREIGN KEY([IdHorario])
REFERENCES [dbo].[Horario] ([IdHorario])
GO
ALTER TABLE [dbo].[Tema]  WITH CHECK ADD FOREIGN KEY([IdMateria])
REFERENCES [dbo].[Materia] ([IdMateria])
GO


/*USUARIOS POR DEFECTO PARA PRUEBAS*/
INSERT INTO Usuario(Cedula,Nombre,Apellidos,Correo,Clave,TipoUsuario)
		VALUES ('117360571','Guillermo','Sotomayor','admin@gmail.com',
				'??S?5;??&?m[z%?????QP1?Wy??r','Administrador');

INSERT INTO Usuario(Cedula,Nombre,Apellidos,Correo,Clave,TipoUsuario)
		VALUES ('117360572','Amanda','Solano','tutor@gmail.com',
				'??9j?????&g??t??a???,? ?J?# ','Tutor');

INSERT INTO Usuario(Cedula,Nombre,Apellidos,Correo,Clave,TipoUsuario)
		VALUES ('117360573','Armando','Porras','estudiante@gmail.com',
				'??=&?/??`?3?S??RDq?j8Zd???','Estudiante');
