namespace TarefaForm {
    
    public class ViewTarefa : Form {
        private readonly Label LblNome;
        private readonly Label LblData;
        private readonly Label LblHora;
        private readonly TextBox InpNome;
        private readonly TextBox InpData;
        private readonly TextBox InpHora;
        private readonly Button BtnCadastrar;
        private readonly Button BtnListar;
        private readonly Button BtnAlterar;
        private readonly Button BtnDeletar;
        private readonly DataGridView DgvTarefas;

        public ViewTarefa() {
            ControllerTarefa.Sincronizar();

            Size = new Size(500, 500);
            StartPosition = FormStartPosition.CenterScreen;

            LblNome = new Label {
                Text = "Nome: ",
                Location = new Point(50, 50),
            };
            LblData = new Label {
                Text = "Data: ",
                Location = new Point(50, 100),
            };
            LblHora = new Label {
                Text = "Hora: ",
                Location = new Point(50, 150),
            };

            InpNome = new TextBox {
                Name = "Nome",
                Location = new Point(150, 50),
                Size = new Size(200, 20)
            };
            InpData = new TextBox {
                Name = "Data",
                Location = new Point(150, 100),
                Size = new Size(200, 20)
            };
            InpHora = new TextBox {
                Name = "Hora",
                Location = new Point(150, 150),
                Size = new Size(200, 20)
            };

            BtnCadastrar = new Button {
                Text = "Cadastrar",
                Location = new Point(50, 200),
            };
            BtnCadastrar.Click += ClickCadastrar;

            BtnListar = new Button {
                Text = "Listar",
                Location = new Point(150, 200),
            };
            BtnListar.Click += ClickListar;

            BtnAlterar = new Button {
                Text = "Alterar",
                Location = new Point(250, 200),
            };
            BtnAlterar.Click += ClickAlterar;

            BtnDeletar = new Button {
                Text = "Deletar",
                Location = new Point(350, 200),
            };
            BtnDeletar.Click += ClickDeletar;

            DgvTarefas = new DataGridView {
                Location = new Point(0, 250),
                Size = new Size(500, 200),
                AutoGenerateColumns = false
            };
            DgvTarefas.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID"
            });
            DgvTarefas.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Nome",
                DataPropertyName = "Nome",
                HeaderText = "Nome"
            });
            DgvTarefas.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Data",
                DataPropertyName = "Data",
                HeaderText = "Data"
            });
            DgvTarefas.Columns.Add(new DataGridViewTextBoxColumn {
                Name = "Hora",
                DataPropertyName = "Hora",
                HeaderText = "Hora"
            });
            DgvTarefas.Columns.Add(new DataGridViewCheckBoxColumn {
                Name = "Concluida",
                DataPropertyName = "Concluida",
                HeaderText = "Concluída"
            });

            Controls.Add(LblNome);
            Controls.Add(LblData);
            Controls.Add(LblHora);
            Controls.Add(InpNome);
            Controls.Add(InpData);
            Controls.Add(InpHora);
            Controls.Add(BtnCadastrar);
            Controls.Add(BtnListar);
            Controls.Add(BtnAlterar);
            Controls.Add(BtnDeletar);
            Controls.Add(DgvTarefas);

            ListarTarefas();
        }

        private void ClickCadastrar(object? sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(InpNome.Text)) {
                MessageBox.Show("Campo Nome não pode estar vazio", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(InpData.Text)) {
                MessageBox.Show("Campo Data não pode estar vazio, use AAAA/MM/DD", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(InpHora.Text)) {
                MessageBox.Show("Campo Hora não pode estar vazio, use HH:MM", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            ControllerTarefa.CriarTarefa(InpNome.Text, InpData.Text, InpHora.Text);
            MessageBox.Show("Tarefa cadastrada com sucesso");
            ListarTarefas();
        }

        private void ClickAlterar(object? sender, EventArgs e) {
            if (DgvTarefas.SelectedRows.Count == 0) {
                MessageBox.Show("Selecione uma tarefa para alterar.");
                return;
            }

            int id = Convert.ToInt32(DgvTarefas.SelectedRows[0].Cells["Id"].Value);
            string novoNome = InpNome.Text;
            string novaData = InpData.Text;
            string novaHora = InpHora.Text;

            ControllerTarefa.AlterarTarefa(id, novoNome, novaData, novaHora);
            ListarTarefas();
        }

        private void ClickListar(object? sender, EventArgs e) {
            ListarTarefas();
        }

        private void ClickDeletar(object? sender, EventArgs e) {
            if (DgvTarefas.SelectedRows.Count > 0) {
                int id = Convert.ToInt32(DgvTarefas.SelectedRows[0].Cells["Id"].Value);
                ControllerTarefa.DeletarTarefa(id);
                ListarTarefas();
            } else {
                MessageBox.Show("Selecione a tarefa a deletar");
            }
        }

        private void ListarTarefas() {
            DgvTarefas.DataSource = null;
            DgvTarefas.DataSource = ControllerTarefa.ListarTarefa();
        }
    }
}