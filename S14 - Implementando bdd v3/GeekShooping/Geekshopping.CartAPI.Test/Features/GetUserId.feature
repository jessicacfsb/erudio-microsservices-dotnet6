#language: pt-br

Funcionalidade: GetUserID
	Dado que eu sou um consumidor da API 
	Gostaria de identificar o carrinho do usuário selecionado

Cenario: Obter identificador do cliente
	Dado que o id usuário seja 'e485f22f-edd2-4538-bdba-8ff7cef95ce4'
	E o método http 'GET'
	Quando chamar o serviço
	Entao statuscode de resposta deverá ser OK