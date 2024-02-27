# Domando regras de negócio em constante mudança

## Introdução
Este projeto é um exemplo de implementação de API que visa ter alta adaptabilidade as mudanças de regras de negócio.

Usando como plano de fundo o cenário de sistema que recebe pedidos de várias fontes, com formatos e regras diferentes, esta API expõe métodos de entrada HTTP com dados estruturados.

Estes são  `OrderWithProduct` e `OrderWithService`. A distinção neste caso visa reduzir a complexidade do código no lado do servidor, recaindo para a aplicação cliente a formatação e seleção de qual recurso utilizar.

Para extensibilidade e compatibilidade com outros formatos, este modelo permite a criação de novas rotas, com suas respectivas adaptações ao modelo de dados. Com isso, a ampliação das funcionalidades não incidirá em alterações nas funcionalidades já existentes.

No caso de as alterações de regras impactarem casos de uso já existentes é possível ampliar as funcionalidades utilizando a emissão e captura de eventos, com o padrão de projeto _Mediador_, implementado com auxílio da biblioteca .NET "Mediatr". Esta flexibilidade permite também a emissão de eventos para serviços de mensageria ou pilhas de armazenamento, como RabbitMQ, Amazon SQS, ou Azure Service Bus.

## Executando aplicação
  A aplicação pode ser executada a partir do diretório raíz, com os seguintes comandos no terminal:
  
  `dotnet restore`
  
  `dotnet run --project .\CrazyOrders.API\CrazyOrders.API.csproj`
  
  Estes irão respectivamente, fazer o download das bibliotecas e dependências do projeto, e em seguida compilar e executar o projeto API. Este está configurado para habilitar a porta `5076` para requisições HTTP, e é acesível pela URL `http://localhost:5076`.

  O projeto está configurado com o documentador de API _Swagger_, e pode ser acessado pela rota `/swagger/index.html` (ou http://localhost:5076/swagger/index.html). Com este é possível executar os métodos POST para ordens de *Produtos*, e *Serviços*.

  Ao executar qualquer um dos dois métodos POST, o terminal que executa a aplicação exibirá as mensagens de log abaixo:
```
info: CrazyOrders.API.Controllers.OrderController[0]
      Create order requested: a1ca0048-b39e-4833-adcf-4b873fef95ab
info: CrazyOrders.Application.UseCases.OrderWithProductCases.CreateOrderWithProductCommandHandler[0]
      Create Order With Product - order created a1ca0048-b39e-4833-adcf-4b873fef95ab
info: CrazyOrders.Infrastructure.PaymentGateway.PaymentGateway[0]
      Processing transaction
info: CrazyOrders.Application.UseCases.OrderWithProductCases.CreateOrderWithProductCommandHandler[0]
      Create Order With Product - initiating delivery for order a1ca0048-b39e-4833-adcf-4b873fef95ab
info: CrazyOrders.Infrastructure.Deliverables.ProductShipping[0]
      Product Shipping - shipping product string
```
  Estas mensagens indicam o fluxo de exemplo para criação de um pedido com *Produto*. Primeiro é registrado o pedido em um repositório de dados:
  1. `Create Order With Product - order created a1ca0048-b39e-4833-adcf-4b873fef95ab`
  seguido pela solicitação do início do processo de entrega:
  2. `Create Order With Product - initiating delivery for order a1ca0048-b39e-4833-adcf-4b873fef95ab`
  e então a confirmação de que a entrega foi processada:
  3. `Product Shipping - shipping product string`

  Os componentes 1., 2., e 3. são respectivamente:
  1. `OrderController`
  2. `CreateOrderWithProductCommandHandler`
  3. `ProductShipping`

  Estes componentes residem em camadas diferentes, como proposto pelo modelo de "arquitetura limpa" (_Clean architecture_). Este modelo propõem a utilização de *casos de uso* para segregação de responsabilidades das funcionalidades da aplicação. Neste caso representado pelo _namespace_ `OrderWithProductCases`, na camada de *aplicação*.

  Os casos de uso são invocados pela _porta_ de entrada da aplicação, exposto como API HTTP.

  O core da aplicação, está na camada de domínio, representado pelo projeto .NET `CrazyOrders.Domain`, que segue o padrão proposto pelo Domain Drive Design, tendo como entidades agregadoras `OrderWithProduct`, e `OrderWithService`. O _value object_ `PaymentDetails` acrescenta ao modelo a capacidade de processar transações de pagamento uniformemente, podendo ser delegado a outro recurso interno/externo.
  
  Por último, a camada de infraestrutura, `CrazyOrders.Infrastructure`, fica a responsabilidade de integrar com recursos internos, como banco de dados, gateway de pagamento, sistema de frete, etc.
