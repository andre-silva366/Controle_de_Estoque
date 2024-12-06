USE ControleAlmoxarifado;

CREATE TABLE Categoria(
	Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    Nome varchar(50)
    
);

ALTER TABLE Categoria
MODIFY COLUMN Nome VARCHAR(100) NOT NULL;

CREATE TABLE Fornecedor(
	Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    CpfCnpj VARCHAR(100) NOT NULL    
    );
    
CREATE TABLE Funcionario(
	Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    Matricula INT NOT NULL ,
    Nome VARCHAR(100) NOT NULL,
    Cargo VARCHAR(50) NOT NULL
    );
    
CREATE TABLE Produto(
	Id INT NOT NULL AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Descricao VARCHAR(250),    
    CategoriaId INT NOT NULL,
    FornecedorId INT NOT NULL,
    PRIMARY KEY(Id),
    FOREIGN KEY(CategoriaId) REFERENCES Categoria(Id),
    FOREIGN KEY (FornecedorId) REFERENCES Fornecedor(Id)
);

SET GLOBAL time_zone = 'America/Sao_Paulo';
    
CREATE TABLE Entrada(
	Id INT NOT NULL AUTO_INCREMENT,
    DataEntrada DATETIME DEFAULT CURRENT_TIMESTAMP,
    ProdutoId INT NOT NULL,    
    FornecedorId INT NOT NULL,
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(10,2),
    PrecoTotal DECIMAL(10,2),
    PRIMARY KEY (Id),
    FOREIGN KEY (ProdutoId) REFERENCES Produto(Id),
    FOREIGN KEY (FornecedorId) REFERENCES Fornecedor(Id) 
);

CREATE TABLE Saida(
	Id INT NOT NULL AUTO_INCREMENT,
    DataSaida DATETIME DEFAULT CURRENT_TIMESTAMP,
    ProdutoId INT NOT NULL,
    SolicitanteId INT NOT NULL,
    AlmoxarifeId INT NOT NULL,
    Quantidade INT NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY(ProdutoId) REFERENCES Produto(Id),
    FOREIGN KEY (SolicitanteId) REFERENCES Funcionario(Id),
    FOREIGN KEY (AlmoxarifeId) REFERENCES Funcionario(Id)
    
);
    
    
    