-- 03_queries.sql
-- Consultas solicitadas para a livraria

-- 1) Listar todos os livros com o nome do autor (JOIN)
SELECT
    l.Id,
    l.Titulo,
    l.Preco,
    a.Nome AS Autor
FROM Livros l
LEFT JOIN Autores a ON l.AutorId = a.Id;

-- 2) Listar os livros com preço acima de R$ 50,00
SELECT Id, Titulo, Preco FROM Livros WHERE Preco > 50.00;

-- 3) Mostrar o total de livros vendidos por título (GROUP BY)
SELECT
    l.Id,
    l.Titulo,
    SUM(v.Quantidade) AS TotalVendido
FROM Livros l
LEFT JOIN Vendas v ON l.Id = v.LivroId
GROUP BY l.Id, l.Titulo
ORDER BY TotalVendido DESC;

-- 4) Buscar o autor que tem mais livros cadastrados
-- Retorna o(s) autor(es) com maior número de livros
SELECT
    a.Id,
    a.Nome,
    COUNT(l.Id) AS QuantidadeLivros
FROM Autores a
LEFT JOIN Livros l ON a.Id = l.AutorId
GROUP BY a.Id, a.Nome
ORDER BY QuantidadeLivros DESC
LIMIT 1;
