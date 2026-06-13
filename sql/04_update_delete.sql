-- 04_update_delete.sql
-- Scripts para atualização e remoção

-- 1) Atualizar o preço de um livro específico (exemplo: alterar o livro Id = 3)
UPDATE Livros
SET Preco = 42.00
WHERE Id = 3;

-- 2) Deletar uma venda pelo Id (exemplo: Deletar venda com Id = 2)
DELETE FROM Vendas WHERE Id = 2;

-- Observação:
-- Ajuste os Ids nos comandos acima conforme os dados reais do seu banco.
