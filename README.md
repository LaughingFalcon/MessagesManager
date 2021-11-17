# MessagesManager
O projeto é uma biblioteca e possui testes unitários para garantir sua eficiencia e também possui 1 teste de integração que pode ser encontrada na classe FunctionTests.cs.

A biblioteca possui 5 funcionalidades principais:
- A criação de uma mensagem nova Para isso é necessário informar o subject, content, startDate e o expirationDate da Mensagem.
- A edição de uma mensagem já existente criada pelo usuário. Para isso é necessário informar os valores novos dos campos subject, content, startDate e expirationDate, além de informar o id da mensagem a ser editada. Como o id é algo gerado automaticamente, imagino que o usuário não terá acesso direto ao id.
- A listagem de todas as mensagens, sem o content, criadas pelo usuário que não foram deletadas.
- A deleção de mensagens criadas pelo usuário.
- A busca de mensangens específicas.

# Status
O projeto ainda está em desenvolvimento e foi criado para estudos.