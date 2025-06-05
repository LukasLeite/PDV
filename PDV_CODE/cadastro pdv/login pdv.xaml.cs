using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace cadastro_pdv
{
    public partial class LoginWindow : Window
    {
        private string pastaExe;
        private string caminhoBancoRelativo;
        private string Dbdados;

        public LoginWindow()
        {
            pastaExe = AppDomain.CurrentDomain.BaseDirectory;

            caminhoBancoRelativo = Path.Combine(pastaExe, @"..\..\..\..\..\PDV_DB\PDV_beta.db");

            Dbdados = Path.GetFullPath(caminhoBancoRelativo);

            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string senha = SenhaBox.Password.Trim();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Preencha o ID e a senha.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (!File.Exists(Dbdados))
                {
                    MessageBox.Show("Banco de dados não encontrado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string conexaoString = $"Data Source={Dbdados};Version=3;";
                using (var conexao = new SQLiteConnection(conexaoString))
                {
                    conexao.Open();

                    string sql = "SELECT COUNT(1) FROM USERS WHERE email = @Email AND senha = @Senha";
                    using (var cmd = new SQLiteCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        long count = (long)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Login bem-sucedido!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                            BtnInicial_Click();
                            // Aqui você pode abrir a próxima janela do sistema e fechar o login
                            // Exemplo:
                            // MainWindow main = new MainWindow();
                            // main.Show();
                            // this.Close();
                        }
                        else
                        {
                            MessageBox.Show("ID ou senha incorretos!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao realizar login: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AbrirCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroWindow cadastro = new CadastroWindow();
            cadastro.Show();
            this.Close(); // fecha a tela de login
        }
        private void BtnInicial_Click()
        {
            TelaInicial inicial = new TelaInicial();
            this.Close();
            inicial.ShowDialog();
        }
    }
}
