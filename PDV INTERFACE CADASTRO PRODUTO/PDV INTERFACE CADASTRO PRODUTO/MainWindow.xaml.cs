using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PDV_INTERFACE_CADASTRO_PRODUTO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            textBoxNomeProduto.Focus();

            textBoxNomeProduto.PreviewKeyDown += TextBox_PreviewKeyDown;
            textBoxCodigoProduto.PreviewKeyDown += TextBox_PreviewKeyDown;
            textBoxPrecoCompra.PreviewKeyDown += TextBox_PreviewKeyDown;
            textBoxPrecoVenda.PreviewKeyDown += TextBox_PreviewKeyDown;

        }

        private void buttonSalvar_Click(object sender, RoutedEventArgs e)
        {
            // Captura os valores dos campos TextBox
            string nomeProduto = textBoxNomeProduto.Text;
            string codigoProduto = textBoxCodigoProduto.Text;
            string precoCompraTexto = textBoxPrecoCompra.Text;
            string precoVendaTexto = textBoxPrecoVenda.Text;

            // Verifica se os campos obrigatórios estão preenchidos
            if (string.IsNullOrWhiteSpace(nomeProduto) ||
                string.IsNullOrWhiteSpace(codigoProduto) ||
                string.IsNullOrWhiteSpace(precoCompraTexto) ||
                string.IsNullOrWhiteSpace(precoVendaTexto))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Tenta converter os preços para decimal
            if (!decimal.TryParse(precoCompraTexto, out decimal precoCompra) ||
                !decimal.TryParse(precoVendaTexto, out decimal precoVenda))
            {
                MessageBox.Show("Os preços devem ser números válidos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Exibe os dados do produto
            StringBuilder mensagem = new StringBuilder();
            mensagem.AppendLine("Produto cadastrado com sucesso:");
            mensagem.AppendLine($"Nome: {nomeProduto}");
            mensagem.AppendLine($"Código: {codigoProduto}");
            mensagem.AppendLine($"Preço de Compra: R$ {precoCompra:F2}");
            mensagem.AppendLine($"Preço de Venda: R$ {precoVenda:F2}");

            MessageBox.Show(mensagem.ToString(), "Cadastro", MessageBoxButton.OK, MessageBoxImage.Information);

            // Limpa os campos após salvar
            textBoxNomeProduto.Clear();
            textBoxCodigoProduto.Clear();
            textBoxPrecoCompra.Clear();
            textBoxPrecoVenda.Clear();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Pode ser usado futuramente para validações em tempo real
        }

        private void TextBoxPrecoCompra_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Pode ser usado futuramente
        }

        private void TextBoxCodigoProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Pode ser usado futuramente
        }

        private void TextBoxNomeProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Pode ser usado futuramente
        }

        private void buttonLimpar_Click(object sender, RoutedEventArgs e)
        {
            textBoxNomeProduto.Clear();
            textBoxCodigoProduto.Clear();
            textBoxPrecoCompra.Clear();
            textBoxPrecoVenda.Clear();

            // Volta o foco para o primeiro campo
            textBoxNomeProduto.Focus();
        }
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true; // Evita o comportamento padrão do Tab

                if (sender == textBoxNomeProduto)
                {
                    textBoxCodigoProduto.Focus();
                }
                else if (sender == textBoxCodigoProduto)
                {
                    textBoxPrecoCompra.Focus();
                }
                else if (sender == textBoxPrecoCompra)
                {
                    textBoxPrecoVenda.Focus();
                }
                else if (sender == textBoxPrecoVenda)
                {
                    // Último campo: volta para o primeiro
                    textBoxNomeProduto.Focus();
                }
            }
        }


    }
}
