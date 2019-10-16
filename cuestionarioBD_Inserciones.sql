USE cuestionarioBD;
GO
INSERT INTO TipoUsuario VALUES
('Docente', 1),
('Estudiante', 1);
GO
INSERT INTO TipoPregunta VALUES
('Respuesta Unica', 1),
('Respuesta Multiple', 1);
GO
INSERT INTO Usuario VALUES
(2,'Leonardo Acevedo', '2014047512', 'leoacevedo@upt.pe', '1234', 1),
(2,'Gisella Ticona', '0000000000', 'gisticona@upt.pe', '1234', 1),
(2,'Cristian Llatasi', '0000000000', 'crisllatasi@upt.pe', '1234', 1),
(1,'Enrique Lanchipa', '12345678', 'enrlanchipa@upt.pe', '1234', 1),
(1,'Alberto Flor', '12345678', 'albflor@upt.pe', '1234', 1),
(1,'Patrick Cuadros', '12345678', 'patcuadros@upt.pe', '1234', 1);
GO
INSERT INTO Curso VALUES
(4, 'Programacion III', 1),
(5, 'Soluciones Moviles', 1),
(5, 'Construccion II', 1),
(6, 'Inteligencia de Negocio', 1);
GO
INSERT INTO Tema VALUES
(1, 'Tema 1', 1),
(1, 'Tema 2', 1),
(1, 'Tema 3', 1),
(1, 'Tema 4', 1),
(2, 'Tema 1', 1),
(2, 'Tema 2', 1),
(2, 'Tema 3', 1),
(3, 'Tema 1', 1),
(3, 'Tema 2', 1),
(3, 'Tema 3', 1),
(3, 'Tema 4', 1),
(3, 'Tema 5', 1),
(4, 'Tema 1', 1),
(4, 'Tema 2', 1);