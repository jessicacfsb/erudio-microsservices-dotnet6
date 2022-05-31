#language: pt-br

Funcionalidade: RemoveFromCart
    Dado que eu sou um consumidor da api
    Gostaria de Remover um produto do carrinho de compras

Cenario: Remover um produto do carrinho de compras
    Dado que o Id do item seja '22'
    E o método 'DELETE'
    Quando o método for executado
    Entao statuscode de resposta devera ser OK
