#language: pt-br

Funcionalidade: GetUserID
	Dado que eu sou um consumidor da API 
	Gostaria de adicionar um produto ao carrinho de compras

Cenario: Obter identificador do cliente
	Dado que o id usuário seja 'e485f22f-edd2-4538-bdba-8ff7cef95ce4'
	E o método http 'GET'
	Quando chamar o serviço
	Entao statuscode de resposta deverá ser OK