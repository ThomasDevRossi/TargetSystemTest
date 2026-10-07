# Target System

Solução em C# (.NET) com três exercícios, cada um em um projeto de console separado.

## Projetos

### 1. Cálculo de comissão
Lê o arquivo `sales.json` e calcula a comissão de cada venda e o total por vendedor.

Regras:
- Vendas abaixo de R$ 100,00: sem comissão
- Vendas abaixo de R$ 500,00: 1%
- Vendas a partir de R$ 500,00: 5%

### 2. Controle de estoque
Permite lançar entradas e saídas de mercadoria a partir do `estoque.json`. Cada movimentação possui um ID único e uma descrição, e ao final é exibida a quantidade atual do produto.

### 3. Cálculo de juros
Dado um valor e uma data de vencimento, calcula os juros até a data de hoje, considerando 2,5% ao dia (juros simples).

## Tecnologias
- C#
- .NET (versão 10.0)

## Como executar

1. Clone o repositório:
```bash
   git clone https://github.com/ThomasDevRossi/TargetSystemTest.git
```
2. Abra a solução (`.sln`) no Visual Studio.
3. Defina o projeto desejado como *Startup Project* e execute (F5), ou pelo terminal:
```bash
   cd TargetSystemTest
   dotnet run
```

## Observações
- O código (classes, variáveis e métodos) está em inglês; as mensagens exibidas no console estão em português.
