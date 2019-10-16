CREATE DATABASE cuestionarioBD;
GO
USE cuestionarioBD;
GO
CREATE TABLE Medalla(
    medallaID INT IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL,
    imagen VARCHAR(200) NOT NULL,
    estado BIT NOT NULL,
    PRIMARY KEY (medallaID)
);
GO
CREATE TABLE TipoUsuario(
    tipoUsuarioID INT IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL,
    estado BIT NOT NULL,
    PRIMARY KEY (tipoUsuarioID)
);
GO
CREATE TABLE Usuario(
    usuarioID INT IDENTITY(1,1),
    tipoUsuarioID INT NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    codigo VARCHAR(10) NOT NULL,
    correo VARCHAR(30) NOT NULL,
    clave VARCHAR(30) NOT NULL,
    estado BIT NOT NULL,
    PRIMARY KEY (usuarioID),
    FOREIGN KEY (tipoUsuarioID) REFERENCES TipoUsuario
);
GO
CREATE TABLE MedallaUsuario(
    medallaUsuarioID INT IDENTITY(1,1),
    medallaID INT NOT NULL,
    usuarioID INT NOT NULL,
    estado BIT NOT NULL,
    PRIMARY KEY (medallaUsuarioID),
    FOREIGN KEY (medallaID) REFERENCES Medalla,
    FOREIGN KEY (usuarioID) REFERENCES Usuario
);
GO
CREATE TABLE Curso(
    cursoID INT IDENTITY(1,1),
    usuarioID INT,
    nombre VARCHAR(50) NOT NULL,
    estado BIT NOT NULL,
    PRIMARY KEY (cursoID),
    FOREIGN KEY (usuarioID) REFERENCES Usuario
);
GO
CREATE TABLE Tema(
    temaID INT IDENTITY(1,1),
    cursoID INT NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    estado BIT NOT NULL,
    PRIMARY KEY (temaID),
    FOREIGN KEY (cursoID) REFERENCES Curso
);
GO
CREATE TABLE Cuestionario(
    cuestionarioID INT IDENTITY(1,1),
    temaID INT NOT NULL,
    duracion INT NOT NULL,
    intentos INT NOT NULL,
    estado BIT NOT NULL,
    PRIMARY KEY (cuestionarioID),
    FOREIGN KEY (temaID) REFERENCES Tema
);
GO
CREATE TABLE CuestionarioBloqueado(
    cuestionarioBloqueadoID INT IDENTITY(1,1),
    cuestionarioDesbloquearID INT NOT NULL,
    cuestionarioRequeridoID INT NOT NULL,
    PRIMARY KEY (cuestionarioBloqueadoID),
    FOREIGN KEY (cuestionarioDesbloquearID) REFERENCES Cuestionario,
    FOREIGN KEY (cuestionarioRequeridoID) REFERENCES Cuestionario
);
GO
CREATE TABLE IntentoCuestionario(
    intentoCuestionarioID INT IDENTITY(1,1),
    cuestionarioID INT NOT NULL,
    usuarioID INT NOT NULL,
    nota INT NOT NULL,
    tiempoResolucion INT NOT NULL,
    estado BIT NOT NULL,
    PRIMARY KEY (intentoCuestionarioID),
    FOREIGN KEY (cuestionarioID) REFERENCES Cuestionario,
    FOREIGN KEY (usuarioID) REFERENCES Usuario
);
GO
CREATE TABLE TipoPregunta(
    tipoPreguntaID INT IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL,
    estado BIT NOT NULL,
    PRIMARY KEY (tipoPreguntaID)
);
GO
CREATE TABLE Pregunta(
    preguntaID INT IDENTITY(1,1),
    tipoPreguntaID INT NOT NULL,
    cuestionarioID INT NOT NULL,
    pregunta VARCHAR(200) NOT NULL,
    imagen VARCHAR(200),
    estado BIT NOT NULL,
    PRIMARY KEY (preguntaID),
    FOREIGN KEY (tipoPreguntaID) REFERENCES TipoPregunta,
    FOREIGN KEY (cuestionarioID) REFERENCES Cuestionario
);
GO
CREATE TABLE Alternativa(
    alternativaID INT IDENTITY(1,1),
    preguntaID INT NOT NULL,
    alternativa VARCHAR(200) NOT NULL,
    respuestaCorrecta BIT NOT NULL,
    estado BIT NOT NULL,
    PRIMARY KEY (alternativaID),
    FOREIGN KEY (preguntaID) REFERENCES Pregunta
);
GO
CREATE TABLE Respuesta(
    respuestaID INT IDENTITY(1,1),
    cuestionarioID INT NOT NULL,
    intentoCuestionarioID INT NOT NULL,
    preguntaID INT NOT NULL,
    alternativaID INT NOT NULL,
    usuarioID INT NOT NULL,
    estado BIT NOT NULL,
    PRIMARY KEY (respuestaID),
    FOREIGN KEY (cuestionarioID) REFERENCES Cuestionario,
    FOREIGN KEY (intentoCuestionarioID) REFERENCES IntentoCuestionario,
    FOREIGN KEY (preguntaID) REFERENCES Pregunta,
    FOREIGN KEY (alternativaID) REFERENCES Alternativa,
    FOREIGN KEY (usuarioID) REFERENCES Usuario
);
GO