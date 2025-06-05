using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SQLite;
using System.IO;

namespace CADASTRO_PRODUTO
{
    public partial class ProductWindow : Window
    {
        private string Dbdados;

        private string pastaExe;
        private string caminhoBancoRelativo;

        public ProductWindow()
        {
            pastaExe = AppDomain.CurrentDomain.BaseDirectory;

            caminhoBancoRelativo = Path.Combine(pastaExe, @"..\..\..\..\..\PDV_DB\PDV_beta.db");

            Dbdados = Path.GetFullPath(caminhoBancoRelativo);

            InitializeComponent();

            textBoxNomeProduto.Focus();

            // Eventos para navegação com Tab
            textBoxNomeProduto.PreviewKeyDown += TextBox_PreviewKeyDown;
            textBoxPrecoCompra.PreviewKeyDown += TextBox_PreviewKeyDown;
            textBoxPrecoVenda.PreviewKeyDown += TextBox_PreviewKeyDown;
            textBoxEstoque.PreviewKeyDown += TextBox_PreviewKeyDown;

            // Evento para atualizar lucro ao digitar
            textBoxPrecoCompra.TextChanged += CalcularLucro;
            textBoxPrecoVenda.TextChanged += CalcularLucro;
        }

        private void buttonSalvar_Click(object sender, RoutedEventArgs e)
        {
            string nomeProduto = textBoxNomeProduto.Text.Trim();
            string precoCompraTexto = textBoxPrecoCompra.Text.Trim();
            string precoVendaTexto = textBoxPrecoVenda.Text.Trim();
            string estoqueTexto = textBoxEstoque.Text.Trim();

            // Valida campos obrigatórios
            if (string.IsNullOrWhiteSpace(nomeProduto) ||
                string.IsNullOrWhiteSpace(precoCompraTexto) ||
                string.IsNullOrWhiteSpace(precoVendaTexto) ||
                string.IsNullOrWhiteSpace(estoqueTexto))
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Converte preços e estoque
            if (!decimal.TryParse(precoCompraTexto, out decimal precoCusto) ||
                !decimal.TryParse(precoVendaTexto, out decimal preco))
            {
                MessageBox.Show("Os preços devem ser números válidos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(estoqueTexto, out int estoque))
            {
                MessageBox.Show("Estoque deve ser um número inteiro válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal lucro = preco - precoCusto;

            StringBuilder mensagem = new StringBuilder();
            mensagem.AppendLine("Deseja realmente salvar este produto?");
            mensagem.AppendLine();
            mensagem.AppendLine($"Nome: {nomeProduto}");
            mensagem.AppendLine($"Preço de Compra: R$ {precoCusto:F2}");
            mensagem.AppendLine($"Preço de Venda: R$ {preco:F2}");
            mensagem.AppendLine($"Estoque: {estoque}");
            mensagem.AppendLine($"Lucro: R$ {lucro:F2}");

            var result = MessageBox.Show(mensagem.ToString(), "Confirmação de Cadastro", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Aqui não passa id, pois ele é autoincrement no banco
                    InserirProduto(nomeProduto, preco, precoCusto, estoque, lucro);
                    MessageBox.Show("Produto cadastrado com sucesso!", "Cadastro", MessageBoxButton.OK, MessageBoxImage.Information);
                    limpar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar o produto: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Cadastro cancelado pelo usuário.", "Cancelado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                limpar();
            }
        }

        private void InserirProduto(string nome, decimal preco, decimal precoCusto, int estoque, decimal lucro)
        {
            string conexaoString = $"Data Source={Dbdados};Version=3;";

            using (SQLiteConnection conexao = new SQLiteConnection(conexaoString))
            {
                conexao.Open();

                // IMPORTANTE: Não insira o id_produto, banco gera automaticamente
                string sql = @"INSERT INTO PRODUTO (nome, preco, preco_custo, estoque, lucro) 
                               VALUES (@nome, @preco, @precoCusto, @estoque, @lucro)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@preco", preco);
                    cmd.Parameters.AddWithValue("@precoCusto", precoCusto);
                    cmd.Parameters.AddWithValue("@estoque", estoque);
                    cmd.Parameters.AddWithValue("@lucro", lucro);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CalcularLucro(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(textBoxPrecoVenda.Text, out decimal precoVenda) &&
                decimal.TryParse(textBoxPrecoCompra.Text, out decimal precoCompra))
            {
                decimal lucro = precoVenda - precoCompra;
                labelLucro.Content = $"Lucro: R$ {lucro:F2}";
            }
            else
            {
                labelLucro.Content = "Lucro: R$ 0,00";
            }
        }

        public void limpar()
        {
            textBoxNomeProduto.Clear();
            textBoxPrecoCompra.Clear();
            textBoxPrecoVenda.Clear();
            textBoxEstoque.Clear();

            labelLucro.Content = "Lucro: R$ 0,00";

            textBoxNomeProduto.Focus();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;

                if (sender == textBoxNomeProduto)
                    textBoxPrecoCompra.Focus();
                else if (sender == textBoxPrecoCompra)
                    textBoxPrecoVenda.Focus();
                else if (sender == textBoxPrecoVenda)
                    textBoxEstoque.Focus();
                else if (sender == textBoxEstoque)
                    textBoxNomeProduto.Focus();
            }
        }
        private void buttonLimpar_Click(object sender, RoutedEventArgs e)
        {
            limpar();
        }
    }
}
