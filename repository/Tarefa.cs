using MySqlConnector;

namespace TarefaForm {
    public static class TarefaRepository {
        public static MySqlConnection conexao;
        public static List<Tarefa> tarefas = new List<Tarefa>();

        public static void InitConexao() {
            string info = "server=localhost;database=listadetarefas;user id=root;password='';Allow Zero DateTime=True;";
            conexao = new MySqlConnection(info);
            try {
                conexao.Open();
            } catch {
                MessageBox.Show("Impossível conectar com o banco");
            }
        }

        public static void CloseConexao() {
            conexao.Close();
        }

        public static void Sincronizar() {
            InitConexao();
            string query = "SELECT * FROM tarefas";
            MySqlCommand command = new MySqlCommand(query, conexao);
            MySqlDataReader reader = command.ExecuteReader();
            tarefas.Clear();
            while (reader.Read()) {
                Tarefa tarefa = new Tarefa(
                    reader["Nome"].ToString() ?? "",
                    reader["Data"].ToString() ?? "",
                    reader["Hora"].ToString() ?? ""
                ) {
                    Id = reader["IdTarefa"].ToString() ?? "",
                    Concluida = Convert.ToBoolean(reader["Concluida"])
                };
                tarefas.Add(tarefa);
            }
            CloseConexao();
        }

        public static void AdicionarTarefa(Tarefa tarefa) {
            InitConexao();
            string query = "INSERT INTO tarefas (Nome, Data, Hora, Concluida) VALUES (@Nome, @Data, @Hora, @Concluida)";
            MySqlCommand command = new MySqlCommand(query, conexao);

            command.Parameters.AddWithValue("@Nome", tarefa.Nome);
            command.Parameters.AddWithValue("@Data", tarefa.Data);
            command.Parameters.AddWithValue("@Hora", tarefa.Hora);
            command.Parameters.AddWithValue("@Concluida", tarefa.Concluida);

            int rowsAffected = command.ExecuteNonQuery();
            tarefa.Id = command.LastInsertedId.ToString();
            if (rowsAffected > 0) {
                tarefas.Add(tarefa);
                MessageBox.Show("Tarefa adicionada com sucesso");
            }
            CloseConexao();
        }

        public static void AlterarTarefa(Tarefa tarefa) {
            InitConexao();
            string query = "UPDATE tarefas SET Nome=@Nome, Data=@Data, Hora=@Hora, Concluida=@Concluida WHERE IdTarefa=@IdTarefa";
            MySqlCommand command = new MySqlCommand(query, conexao);

            command.Parameters.AddWithValue("@Nome", tarefa.Nome);
            command.Parameters.AddWithValue("@Data", tarefa.Data);
            command.Parameters.AddWithValue("@Hora", tarefa.Hora);
            command.Parameters.AddWithValue("@Concluida", tarefa.Concluida);
            command.Parameters.AddWithValue("@IdTarefa", tarefa.Id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0) {
                MessageBox.Show("Tarefa atualizada com sucesso");
            } else {
                MessageBox.Show("Erro ao atualizar a tarefa");
            }
            CloseConexao();
        }

        public static void DeletarTarefa(int id) {
            InitConexao();
            string query = "DELETE FROM tarefas WHERE IdTarefa=@IdTarefa";
            MySqlCommand command = new MySqlCommand(query, conexao);
            command.Parameters.AddWithValue("@IdTarefa", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0) {
                tarefas.RemoveAll(t => t.Id == id.ToString());
                MessageBox.Show("Tarefa deletada com sucesso");
            } else {
                MessageBox.Show("Erro ao deletar a tarefa");
            }
            CloseConexao();
        }
    }
}
