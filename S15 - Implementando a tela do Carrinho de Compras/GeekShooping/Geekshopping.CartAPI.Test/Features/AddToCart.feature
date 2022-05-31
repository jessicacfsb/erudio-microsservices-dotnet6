#language: pt-br

Funcionalidade: AddToCart
    Dado que eu sou um consumidor da api
    Gostaria de Adicionar um produto ao carrinho de compras

Cenario: Adicionar um produto ao carrinho de compras
    Dado que o userId do carrinho seja 'e485f22f-edd2-4538-bdba-8ff7cef95ce4'
    E o metodo 'POST'
    Quando o metodo for executado
    Entao statuscode da resposta devera ser OK
