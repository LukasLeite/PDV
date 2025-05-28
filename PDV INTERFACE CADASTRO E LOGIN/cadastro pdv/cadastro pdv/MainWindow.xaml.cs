using System.Windows;
using System.Windows.Controls;

namespace cadastro_pdv
{
    public partial class CadastroWindow : Window
    {
        public CadastroWindow()
        {
            InitializeComponent();
            NomeTextBox.Focus();

        }

        private void Cadastra_Click(object sender, RoutedEventArgs e)
        {
            string nome = NomeTextBox.Text;
            string email = EmailTextBox.Text;
            string id = IdTextBox.Text;
            string senha = SenhaBox.Password;

            if (string.IsNullOrWhiteSpace(nome) ||
                   string.IsNullOrWhiteSpace(email) ||
                   string.IsNullOrWhiteSpace(id) ||
                   string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else 
            { 
            MessageBox.Show($"Funcionário {nome} cadastrado com sucesso!");

            NomeTextBox.Clear();
            EmailTextBox.Clear();
            IdTextBox.Clear();
            SenhaBox.Clear();
            NomeTextBox.Focus();
            }
    }
        private void AbrirLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close(); // fecha a tela de cadastro
        }
        private void buttonLimpar_Click(object sender, RoutedEventArgs e)
        {
            NomeTextBox.Clear();
            EmailTextBox.Clear();
            IdTextBox.Clear();
            SenhaBox.Clear();

            // Volta o foco para o primeiro campo
            NomeTextBox.Focus();
        }
        /*
        private void MostrarSenha_Checked(object sender, RoutedEventArgs e)
        {
            // Quando marcar "mostrar senha", copie o texto da senha para o TextBox
            SenhaTextBox.Text = SenhaBox.Password;
            SenhaTextBox.Visibility = Visibility.Visible;
            SenhaBox.Visibility = Visibility.Collapsed;
        }

        private void MostrarSenha_Unchecked(object sender, RoutedEventArgs e)
        {
            // Quando desmarcar, copie do TextBox para o PasswordBox e alterne visibilidades
            SenhaBox.Password = SenhaTextBox.Text;
            SenhaBox.Visibility = Visibility.Visible;
            SenhaTextBox.Visibility = Visibility.Collapsed;
        }

        private void SenhaBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Sempre que a senha no PasswordBox mudar, atualize o TextBox se ele estiver visível
            if (SenhaTextBox.Visibility == Visibility.Visible)
            {
                SenhaTextBox.Text = SenhaBox.Password;
            }
        }

        private void SenhaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Sempre que o TextBox mudar, atualize o PasswordBox se ele estiver visível
            if (SenhaBox.Visibility == Visibility.Visible)
            {
                SenhaBox.Password = SenhaTextBox.Text;
            }
        }
        */
    }
}
