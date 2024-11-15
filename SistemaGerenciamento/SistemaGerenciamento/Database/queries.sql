CREATE DATABASE sistema_gerenciamento

CREATE TABLE Clientes (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Endereco VARCHAR(200),
    Telefone VARCHAR(20),
    Email VARCHAR(100) UNIQUE
);

CREATE TABLE Produtos (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Preco DECIMAL(10, 2) NOT NULL,
    Estoque INT NOT NULL
);

CREATE TABLE Vendas (
    Id SERIAL PRIMARY KEY,
    ClienteId INT NOT NULL REFERENCES Clientes(Id) ON DELETE CASCADE,
    DataVenda TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE ItensVenda (
    Id SERIAL PRIMARY KEY,
    VendaId INT NOT NULL REFERENCES Vendas(Id) ON DELETE CASCADE,
    ProdutoId INT NOT NULL REFERENCES Produtos(Id) ON DELETE CASCADE,
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(10, 2) NOT NULL
);

INSERT INTO Clientes (Nome, Endereco, Telefone, Email) VALUES
('Maria Silva', 'Av. Paulista, 1000', '11987654321', 'maria.silva@example.com'),
('João Souza', 'Rua das Flores, 200', '11998765432', 'joao.souza@example.com'),
('Ana Pereira', 'Rua do Sol, 300', '11987651234', 'ana.pereira@example.com'),
('Carlos Lima', 'Av. Brasil, 400', '11999887766', 'carlos.lima@example.com'),
('Beatriz Santos', 'Rua das Árvores, 500', '11988776655', 'beatriz.santos@example.com');

INSERT INTO Produtos (Nome, Preco, Estoque) VALUES
('Cadeira de Escritório', 250.00, 50),
('Mesa de Jantar', 500.00, 20),
('Sofá 3 Lugares', 1200.00, 10),
('Estante de Livros', 300.00, 30),
('Cama de Casal', 900.00, 15);

INSERT INTO Vendas (ClienteId, DataVenda) VALUES
(1, '2024-11-15 09:00:00'),
(2, '2024-11-15 10:30:00'),
(3, '2024-11-16 11:45:00'),
(4, '2024-11-16 12:00:00'),
(5, '2024-11-17 13:15:00');

INSERT INTO ItensVenda (VendaId, ProdutoId, Quantidade, PrecoUnitario) VALUES
(1, 1, 2, 250.00),
(1, 2, 1, 500.00),
(2, 3, 1, 1200.00),
(2, 4, 1, 300.00),
(3, 5, 1, 900.00),
(3, 1, 1, 250.00),
(4, 2, 2, 500.00),
(4, 3, 1, 1200.00),
(5, 4, 1, 300.00),
(5, 5, 1, 900.00);