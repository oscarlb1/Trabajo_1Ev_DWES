CREATE DATABASE AcademiaDB;

SELECT name, database_id, create_date 
FROM sys.databases 
WHERE name = 'AcademiaDB';

USE AcademiaDB;



CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Creditos DECIMAL(6, 2) NOT NULL,
    Cursos INT NOT NULL,
    Premium BIT NOT NULL,
    Registro DATETIME NOT NULL
);

CREATE TABLE Profesor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Especialidad NVARCHAR(100) NOT NULL,
    Salario DECIMAL(10, 2) NOT NULL,
    Experiencia INT NOT NULL,
    Certificado BIT NOT NULL,
    Contrato DATETIME NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Materia (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Detalle NVARCHAR(500) NOT NULL,
    Nivel DECIMAL(4, 2) NOT NULL,
    Cantidad INT NOT NULL,
    Obligatoria BIT NOT NULL,
    Creacion DATETIME NOT NULL
);

CREATE TABLE Curso (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(200) NOT NULL,
    Detalle NVARCHAR(500) NOT NULL,
    Costo DECIMAL(8, 2) NOT NULL,
    Horas INT NOT NULL,
    Publicado BIT NOT NULL,
    Creacion DATETIME NOT NULL,
    ProfesorId INT NOT NULL,
    MateriaId INT NOT NULL,
    FOREIGN KEY (ProfesorId) REFERENCES Profesor(Id),
    FOREIGN KEY (MateriaId) REFERENCES Materia(Id)
);

CREATE TABLE Leccion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(200) NOT NULL,
    Tipo NVARCHAR(50) NOT NULL,
    Minutos INT NOT NULL,
    Examen BIT NOT NULL,
    Publicacion DATETIME NOT NULL,
    URL NVARCHAR(500) NOT NULL,
    CursoId INT NOT NULL,
    FOREIGN KEY (CursoId) REFERENCES Curso(Id)
);

CREATE TABLE Inscripcion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Progreso NVARCHAR(50) NOT NULL,
    Comentario NVARCHAR(500) NULL,
    Nota DECIMAL(4, 2) NOT NULL,
    Activa BIT NOT NULL,
    Inscripcion DATETIME NOT NULL,
    UsuarioId INT NOT NULL,
    CursoId INT NOT NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id),
    FOREIGN KEY (CursoId) REFERENCES Curso(Id)
);

-- AUTORIA --
CREATE TABLE Opinion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    FechaComentario DATETIME NOT NULL,
    Mensaje NVARCHAR(250) NOT NULL,
    Puntuacion INT NOT NULL,
    CursoId INT NOT NULL,
    FOREIGN KEY (CursoId) REFERENCES Curso(Id)
);

INSERT INTO Opinion (Nombre, FechaComentario, Mensaje, Puntuacion, CursoId) VALUES
('Andrea Martínez', '2010-08-20 09:00:00', 'Me encanta este curso.', 4, 1),
('Carlos Ruiz', '2010-08-20 09:00:00', 'Muy bueno este curso.', 5, 2);



INSERT INTO Usuario (Nombre, Email, Creditos, Cursos, Premium, Registro) VALUES
('Andrea Martínez', 'andrea.martinez@email.com', 50.50, 2, 1, '2024-01-15 10:00:00'),
('Carlos Ruiz', 'carlos.ruiz@email.com', 10.00, 0, 0, '2024-03-01 15:30:00');

INSERT INTO Profesor (Nombre, Especialidad, Salario, Experiencia, Certificado, Contrato, Email) VALUES
('Dr. Javier López', 'ArquitecturaREST', 65000.00, 15, 1, '2010-08-20 09:00:00', 'javier.lopez@academia.com'),
('Ing. Elena Gómez', 'BasesDatosSQL', 48000.50, 8, 0, '2017-05-10 11:00:00', 'elena.gomez@academia.com');

INSERT INTO Materia (Nombre, Detalle, Nivel, Cantidad, Obligatoria, Creacion) VALUES
('Backend', 'Principios y tecnologías para el desarrollo de servidores y APIs.', 3.50, 5, 1, '2023-01-01 08:00:00'),
('DataScience', 'Análisis, modelado y visualización de grandes volúmenes de datos.', 4.00, 3, 0, '2023-05-15 09:30:00');

INSERT INTO Curso (Titulo, Detalle, Costo, Horas, Publicado, Creacion, ProfesorId, MateriaId) VALUES
('API RESTful con .NET 8', 'Curso completo sobre diseño e implementación de APIs.', 49.99, 40, 1, '2024-02-01 12:00:00', 1, 1),
('Introducción a Machine Learning', 'Conceptos básicos y algoritmos de ML.', 79.50, 60, 1, '2024-03-10 14:00:00', 2, 2);

INSERT INTO Leccion (Titulo, Tipo, Minutos, Examen, Publicacion, URL, CursoId) VALUES
('Fundamentos de HTTP', 'Video', 25, 0, '2024-02-05 10:00:00', 'url_video_http', 1),
('Primeros Pasos en Python', 'Texto', 15, 0, '2024-03-15 11:30:00', 'url_documento_python', 2);

INSERT INTO Inscripcion (Progreso, Comentario, Nota, Activa, Inscripcion, UsuarioId, CursoId) VALUES
('En progreso', 'El módulo de validación es complejo.', 0.00, 1, '2024-04-01 18:00:00', 1, 1),
('Completado', 'Excelente contenido.', 9.50, 0, '2024-03-15 09:00:00', 2, 2);