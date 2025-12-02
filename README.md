# desafio-sistema-comercial

Implementação em C# (.NET) dos três exercícios do desafio:

- Cálculo de comissão por venda (por vendedor) usando regras definidas no enunciado em `desafio.md`.
- Registro e controle de movimentações de estoque (entrada/saída) com persistência em JSON.
- Cálculo de juros por atraso (2,5% ao dia) a partir de data de vencimento.

Esta implementação fornece um aplicativo console simples com menu e três serviços separados em `src/DesafioComercial/Services`.

## Estrutura do projeto

- `src/DesafioComercial/` - projeto console .NET
	- `Program.cs` - menu principal
	- `Services/` - serviços: `ComissaoService`, `EstoqueService`, `JurosService`
	- `Models/` - modelos de domínio (`Venda`, `Produto`, `Movimentacao`, etc.)
- `data/` - arquivos JSON de entrada e persistência:
	- `vendas.json` - vendas de exemplo
	- `estoque.json` - estoque inicial
	- `movimentacoes.json` - registro de movimentações (é atualizado em runtime)

## Requisitos

- .NET SDK (recomendo .NET 10). Verifique com:

	```powershell
	dotnet --version
	```

## Como executar

Abra o PowerShell na raiz do repositório e execute:

```powershell
dotnet build src/DesafioComercial
dotnet run --project src/DesafioComercial
```

O menu exibirá as opções para executar cada funcionalidade.

## Notas de uso

- Entrada numérica aceita `.` ou `,` como separador decimal no cálculo de juros.
- Ao registrar movimentações, o arquivo `data/movimentacoes.json` recebe um objeto com: `id`, `codigoProduto`, `tipo` (Entrada/Saida), `quantidade`, `timestamp` (UTC) e `estoqueFinal`.
- A propriedade `Movimentacao.tipo` utiliza o enum `MovimentacaoTipo` e é serializada como string (`Entrada`/`Saida`).