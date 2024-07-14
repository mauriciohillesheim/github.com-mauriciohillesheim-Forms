    #Lista de Tarefas

Este é um projeto console para gerenciar uma lista de tarefas. Ele permite criar, listar, alterar e deletar tarefas. Cada tarefa possui os atributos: nome, data, hora e status de conclusão.

    #Configuração do Banco de Dados

Para criar a tabela de ListaDeTarefas no MySQL, use o seguinte comando:

```sql
CREATE DATABASE listadetarefas;

CREATE TABLE tarefas (
    IdTarefa INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Data DATA NOT NULL,
    Hora TIME NOT NULL,
    Concluida BOOLEAN NOT NULL DEFAULT FALSE
);