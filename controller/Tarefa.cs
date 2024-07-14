namespace TarefaForm {

    public static class ControllerTarefa {
        public static void Sincronizar() {
            Tarefa.Sincronizar();
        }

        public static void CriarTarefa(string nome, string data, string hora) {
            new Tarefa(nome, data, hora);
        }

        public static List<Tarefa> ListarTarefa() {
            return Tarefa.ListarTarefa();
        }

        public static void AlterarTarefa(int id, string nome, string data, string hora) {
            List<Tarefa> tarefas = ListarTarefa();
            Tarefa tarefa = tarefas.FirstOrDefault(t => t.Id == id.ToString());
            if (tarefa != null) {
                tarefa.Alterar(nome, data, hora);
            } else {
                Console.WriteLine("Índice inválido");
            }
        }

        public static void DeletarTarefa(int id) {
            List<Tarefa> tarefas = ListarTarefa();
            Tarefa tarefa = tarefas.FirstOrDefault(t => t.Id == id.ToString());
            if (tarefa != null) {
                Tarefa.DeletarTarefa(id);
            } else {
                Console.WriteLine("Índice inválido");
            }
        }
    }
}