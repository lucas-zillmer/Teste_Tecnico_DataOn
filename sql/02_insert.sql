-- 02_insert.sql
-- Inserção de dados de exemplo: pelo menos 3 autores, 5 livros e 4 vendas

-- Autores
INSERT INTO Autores (Nome, Nascimento) VALUES
('Jorge Amado', '1912-08-10'),
('Clarice Lispector', '1920-12-10'),
('Machado de Assis', '1839-06-21');

-- Livros (AutorId está baseado na ordem de inserção acima: 1,2,3)
INSERT INTO Livros (Titulo, Preco, AnoPublicacao, AutorId) VALUES
('Gabriela, Cravo e Canela', 45.90, 1958, 1),
('Dona Flor e Seus Dois Maridos', 59.90, 1966, 1),
('A Hora da Estrela', 39.50, 1977, 2),
('A Paixão segundo G.H.', 75.00, 1964, 2),
('Dom Casmurro', 49.90, 1899, 3);

-- Vendas (referenciando Livros por Id; ajuste se IDs diferirem)
INSERT INTO Vendas (LivroId, Quantidade, DataVenda) VALUES
(1, 2, '2026-06-01'),
(2, 1, '2026-06-02'),
(4, 3, '2026-06-03'),
(1, 1, '2026-06-05');
