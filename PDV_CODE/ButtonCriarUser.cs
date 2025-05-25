using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace PDVvvvv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Pega os dados digitados nas TextBoxes
            string nome = textBox1.Text;
            string telefone = textBox2.Text;
            string sexo = textBox3.Text;
            string email = textBox4.Text;

            // Caminho completo para o banco SQLite
            string connectionString = @"Data Source=C:\Users\Lucas Leite\Desktop\banco\PDV_1.db;Version=3;";

            // Comando SQL de inserção
            string query = "INSERT INTO usuario (nome, email, telefone, sexo) VALUES (@nome, @email, @telefone, @sexo)";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", nome);
                        command.Parameters.AddWithValue("@telefone", telefone);
                        command.Parameters.AddWithValue("@sexo", sexo);
                        command.Parameters.AddWithValue("@email", email);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar usuário: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void textBox4_TextChanged(object sender, EventArgs e) { }
    }
}

