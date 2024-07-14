namespace TarefaForm {
    public class Tarefa {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public bool Concluida { get; set; }

        public Tarefa(string nome, string data, string hora) {
            Nome = nome;
            Data = data;
            Hora = hora;
            TarefaRepository.AdicionarTarefa(this);
        }

        public void Alterar(string nome, string data, string hora) {
            Nome = nome;
            Data = data;
            Hora = hora;
            TarefaRepository.AlterarTarefa(this);
        }

        public static void Sincronizar() {
            TarefaRepository.Sincronizar();
        }
        
        public static List<Tarefa> ListarTarefa() {
            return TarefaRepository.tarefas;
        }

        public static void DeletarTarefa(int id) {
            TarefaRepository.DeletarTarefa(id);
        }
    }
}