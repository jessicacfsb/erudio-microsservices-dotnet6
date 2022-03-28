SELECT * FROM clientes 
LEFT OUTER JOIN vendas
ON clientes.Nome = vendas.id_cliente