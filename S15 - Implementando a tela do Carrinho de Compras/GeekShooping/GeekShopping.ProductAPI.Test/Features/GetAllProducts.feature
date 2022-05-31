#language: pt-br

Funcionalidade: GetAllProducts
	Dado que eu sou um consumidor da API de Produtos
	Gostaria de Chamar todos os produtos Cadastrados no Product API

Cenario: Obter todos os produtos cadastrados
	Dado que a url recebida seja route
	E que o método http seja 'GET'
	Quando chamar o método
	Entao statuscode de resposta devera ser OK