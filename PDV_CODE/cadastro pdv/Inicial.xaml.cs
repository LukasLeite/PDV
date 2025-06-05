using cadastro_pdv;
using System.Windows;
using CADASTRO_PRODUTO;

namespace cadastro_pdv
{
    public partial class TelaInicial : Window
    {
        public TelaInicial()
        {
            InitializeComponent();
        }

        private void BtnCadastro_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow cadastro = new ProductWindow();
            cadastro.ShowDialog();

        }

        private void BtnVendas_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidade de Vendas em desenvolvimento.");
        }

        private void BtnRelatorios_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidade de Relatórios em desenvolvimento.");
        }

        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
