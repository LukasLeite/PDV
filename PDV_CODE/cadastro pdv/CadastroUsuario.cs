using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace cadastro_pdv
{
    public partial class CadastroWindow : Window
    {

        private string pastaExe;
        private string caminhoBancoRelativo;
        private string Dbdados;

        public CadastroWindow()
        {
            pastaExe = AppDomain.CurrentDomain.BaseDirectory;

            caminhoBancoRelativo = Path.Combine(pastaExe, @"..\..\..\..\..\PDV_DB\PDV_beta.db");

            Dbdados = Path.GetFullPath(caminhoBancoRelativo);
            InitializeComponent();
            NomeTextBox.Focus();
        }

        private void Cadastra_Click(object sender, RoutedEventArgs e)
        {
            string nome = NomeTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string senha = SenhaBox.Password.Trim();

            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (!File.Exists(Dbdados))
                {
                    MessageBox.Show("Banco de dados não encontrado no caminho especificado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string conexaoString = $"Data Source={Dbdados};Version=3;";

                using (var conexao = new SQLiteConnection(conexaoString))
                {
                    conexao.Open();

                    // Verifica se a tabela USERS existe
                    using (var cmdCheck = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='USERS';", conexao))
                    {
                        var result = cmdCheck.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Tabela USERS não existe no banco de dados.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    // Usa transação para garantir que o insert será aplicado
                    using (var trans = conexao.BeginTransaction())
                    {
                        string sql = "INSERT INTO USERS (nome, senha, email) VALUES (@Nome, @Senha, @Email)";

                        using (var cmd = new SQLiteCommand(sql, conexao, trans))
                        {
                            cmd.Parameters.AddWithValue("@Nome", nome);
                            cmd.Parameters.AddWithValue("@Senha", senha);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }

                    conexao.Close();
                }

                MessageBox.Show($"Funcionário {nome} cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                NomeTextBox.Clear();
                EmailTextBox.Clear();
                SenhaBox.Clear();
                NomeTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AbrirLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void buttonLimpar_Click(object sender, RoutedEventArgs e)
        {
            NomeTextBox.Clear();
            EmailTextBox.Clear();
            SenhaBox.Clear();
            NomeTextBox.Focus();
        }
    }
}
