# MiniBanco

Motor de gestão de contas bancárias em C#, executado no terminal. O sistema cria contas de três tipos, registra movimentações, aplica o fechamento mensal e gera um relatório de saúde financeira com pendências e recomendações para a gerência.

## Vídeo de apresentação

[Assista no YouTube](https://youtu.be/bOKg0vKTp6w)

## Sumário

- [Funcionalidades](#funcionalidades)
- [Tecnologias](#tecnologias)
- [Como executar](#como-executar)
- [Como usar](#como-usar)
- [Tipos de conta](#tipos-de-conta)
- [Análise de saúde da conta](#análise-de-saúde-da-conta)
- [Estrutura do projeto](#estrutura-do-projeto)
- [Decisões de projeto](#decisões-de-projeto)

## Funcionalidades

- Criação de contas corrente, poupança e salário, com número de conta gerado automaticamente.
- Saque, depósito e transferência entre contas, com validações e exceções próprias.
- Extrato individual com pontuação e pendências da conta.
- Fechamento mensal aplicado a todas as contas (tarifa de manutenção, rendimento e reinício do limite de saques).
- Relatório geral com pontuação, índice de saúde, classificação e recomendações da gerência.
- Carga de contas de demonstração, com movimentações e fechamentos de mês já aplicados, para explorar o sistema sem cadastrar tudo manualmente.

## Tecnologias

- C# com .NET 10
- Aplicação de console, sem dependências externas

## Como executar

### Pré-requisito

[.NET SDK 10](https://dotnet.microsoft.com/download) instalado. Para conferir:

```bash
dotnet --version
```

### Passos

```bash
git clone https://github.com/Luscaee/minibanco-dotnet.git
cd minibanco-dotnet
dotnet run --project MiniBanco
```

O programa não usa banco de dados nem arquivos externos. Os dados existem apenas enquanto a aplicação está aberta.

Use um terminal com suporte a UTF-8 para que molduras e acentos sejam exibidos corretamente.

## Como usar

Menu principal:

| Opção | Ação |
|-------|------|
| 1 | Criar conta |
| 2 | Acessar conta pelo número |
| 3 | Aplicar fim de mês em todas as contas |
| 4 | Exibir o relatório de contas |
| 5 | Carregar contas de demonstração |
| 6 | Sair |

Dentro de uma conta é possível sacar, depositar, transferir e consultar o extrato. Valores decimais são digitados com vírgula (por exemplo, `1500,50`).

### Contas de demonstração

A opção 5 cria quatro contas com depósitos, saques, uma transferência e dois fechamentos de mês, e exibe o número de cada conta criada. Em seguida, a opção 4 mostra o relatório com as três classificações de saúde. A carga pode ser feita uma única vez por execução.

| Titular | Tipo | Classificação esperada |
|---------|------|------------------------|
| Carla Mendes | Poupança | Saudável |
| Bruno Lima | Corrente | Atenção |
| Eva Martins | Salário | Atenção |
| Diego Rocha | Corrente | Risco |

## Tipos de conta

| Tipo | Características | Fechamento do mês |
|------|-----------------|-------------------|
| Conta Corrente | Limite de cheque especial definido na criação | Tarifa de manutenção de R$ 10,00 |
| Conta Poupança | Dia de aniversário e taxa de rendimento mensal (%) | Rendimento creditado sobre o saldo |
| Conta Salário | Empregador e limite de saques mensais | Contador de saques reiniciado |

Na conta salário, cada saque além do limite mensal gera uma tarifa de R$ 5,00. Como a tarifa é cobrada além do saldo, a conta pode ficar negativa até um novo depósito.

Todas as contas começam com saldo zero, de modo que o saldo sempre corresponde à soma das transações do extrato.

## Análise de saúde da conta

Cada transação do extrato vale pontos de acordo com seu tipo:

| Transação | Pontos |
|-----------|--------|
| Depósito | 10 |
| Saque | 5 |
| Tarifa | 0 |

A pontuação máxima de cada transação é 10. O índice de saúde é a razão entre a pontuação atingida e a máxima, em porcentagem, arredondada para duas casas.

| Índice | Classificação |
|--------|---------------|
| 90% ou mais | Conta saudável |
| 60% a 89,99% | Conta em atenção |
| Abaixo de 60% | Conta em risco |

Contas sem transações são classificadas como saudáveis.

O relatório lista ainda:

- **Itens críticos:** cada tarifa cobrada, com a orientação para avaliar isenção ou migração para pacote de serviços.
- **Itens de atenção:** saques realizados enquanto a conta estava com saldo negativo.

## Estrutura do projeto

```
minibanco-dotnet/
└── MiniBanco/
    ├── MiniBanco.csproj
    └── src/
        ├── Program.cs
        ├── Domain/
        │   ├── Contas/
        │   │   ├── Conta.cs
        │   │   ├── ContaCorrente.cs
        │   │   ├── ContaPoupanca.cs
        │   │   └── ContaSalario.cs
        │   ├── Analises/
        │   │   ├── ResultadoAnalise.cs
        │   │   └── ItemPendencia.cs
        │   ├── Enums/
        │   │   ├── TipoTransacao.cs
        │   │   └── ClassificacaoConta.cs
        │   ├── Exceptions/
        │   │   ├── ValorInvalidoException.cs
        │   │   ├── SaldoInsuficienteException.cs
        │   │   └── ContaNaoExcontradaException.cs
        │   └── Transacao.cs
        ├── Services/
        │   ├── BancoService.cs
        │   ├── DadosDemonstracao.cs
        │   ├── AnaliseSaudeService.cs
        │   └── RelatorioService.cs
        └── UI/
            ├── MenuPrincipal.cs
            ├── Telas.cs
            ├── ConsoleHelper.cs
            └── TelaCarregamento.cs
```

### Camadas

| Camada | Responsabilidade |
|--------|------------------|
| Domain | Entidades, enums, exceções e regras de cada tipo de conta |
| Services | Gerenciamento das contas, cálculo da pontuação e geração dos relatórios |
| UI | Menus, telas e leitura segura de entradas do usuário |

## Decisões de projeto

- **Herança e polimorfismo:** `Conta` é uma classe abstrata. Cada tipo de conta define seu próprio comportamento de saque e de fechamento de mês.
- **Template Method no saque:** `Sacar` valida o valor, chama a validação específica de cada conta, registra a transação e atualiza o saldo. As contas filhas sobrescrevem apenas a validação.
- **Encapsulamento do extrato:** a lista de transações é privada e exposta somente para leitura.
- **Valores monetários em `decimal`**, evitando erros de arredondamento de ponto flutuante.
- **Exceções próprias** para valor inválido, saldo insuficiente e conta não encontrada, tratadas na camada de interface.
