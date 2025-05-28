using System.Windows;

namespace cadastro_pdv
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string id = IdTextBox.Text;
            string senha = SenhaBox.Password;

            // Exemplo simplificado (substituir por lógica real)
            if (id == "12345678" && senha == "admin")
            {
                MessageBox.Show("Login bem-sucedido!");
            }
            else
            {
                MessageBox.Show("ID ou senha incorretos!");
            }

        }

        private void AbrirCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastroWindow cadastro = new CadastroWindow();
            cadastro.Show();
            this.Close(); // fecha a tela de login
        }
    }
}
