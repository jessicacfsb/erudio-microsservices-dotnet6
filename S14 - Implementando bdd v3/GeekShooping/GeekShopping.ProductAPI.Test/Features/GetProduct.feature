#language: pt-br

Funcionalidade: GetProductId
	Dado que eu sou um consumidor da API de Produtos
	Gostaria de Chamar um produto por seu Id

Cenario: Obter identificador do Produto
	Dado que o id do produto seja 2
	E o token seja token
	E o metodo http 'GET'
	Quando chamar o serviço
	Entao statuscode de resposta deverá ser OK