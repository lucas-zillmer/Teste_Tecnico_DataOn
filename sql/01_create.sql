-- 01_create.sql
-- Script para criação das tabelas do banco da livraria
-- Compatível com MySQL (InnoDB). Ajuste pequenas sintaxes se usar outro SGDB.

DROP TABLE IF EXISTS Vendas;
DROP TABLE IF EXISTS Livros;
DROP TABLE IF EXISTS Autores;

CREATE TABLE Autores (
    Id INT NOT NULL AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Nascimento DATE NULL,
    PRIMARY KEY (Id)
) ENGINE=InnoDB;

CREATE TABLE Livros (
    Id INT NOT NULL AUTO_INCREMENT,
    Titulo VARCHAR(200) NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,
    AnoPublicacao INT NULL,
    AutorId INT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT FK_Livros_Autores FOREIGN KEY (AutorId) REFERENCES Autores(Id) ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE TABLE Vendas (
    Id INT NOT NULL AUTO_INCREMENT,
    LivroId INT NOT NULL,
    Quantidade INT NOT NULL,
    DataVenda DATE NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT FK_Vendas_Livros FOREIGN KEY (LivroId) REFERENCES Livros(Id) ON DELETE CASCADE
) ENGINE=InnoDB;
