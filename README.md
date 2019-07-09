# Mass-Transit-Web-API

Necessário instalar manualmente os pacote NuGet para Injeção de Dependência
  - MassTransit.RabbitMQ
  - MassTransit.Extensions.DependencyInjection
  
Para Bus Iniciar com WEB API o BusService Implementa IHostedService

Para realizar a comunicação o Mass Transit Utiliza Interfaces, Producer e Consumer(Work) devem implementar a mesma interface.
  - Interface devem estar em projeto to Tipo Class Library
