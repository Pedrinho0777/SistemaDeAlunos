using ProjetoCurso01.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCurso01.Dao
{
    public class AlunoDao : DbSql
    {
        //Método para Inserir 
        public void InserirAluno(AlunoDto aluno)
        {
            //Cria uma conexão e depois que foi usada o Using fecha a conexão
            using (SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                //Tratamento de erro
                try
                {
                    //String de comandos para fazer o Insert no banco de dados
                    string sql = "INSERT INTO Alunos (nome,data_nascimento,sexo,data_cadastro)" +
                   "VALUES (@nome,@data_nascimento,@sexo,@data_cadastro)";
                    SqlCommand cmd = new SqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("id_aluno", aluno.id_aluno);
                    cmd.Parameters.AddWithValue("nome", aluno.nome);
                    cmd.Parameters.AddWithValue("data_nascimento", aluno.data_nascimento);
                    cmd.Parameters.AddWithValue("sexo", aluno.sexo);
                    cmd.Parameters.AddWithValue("data_cadastro", aluno.data_cadastro);
                    conexao.Open();
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Cadastrado com sucesso\n");
                }//Se o insert der algum erro, vai cair aqui
                catch (Exception e)
                {
                    Console.WriteLine("Erro " + e.Message);
                }
            }
        }

        //Método para Alterar 
        public void AlterarAluno(AlunoDto aluno)
        {
            //Abre a conexão, e depois fecha quando terminar de usar
            using(SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    string sql = "UPDATE Alunos SET nome =@nome,data_nascimento=@data_nascimento," +
                        "sexo =@sexo,data_alteracao =@data_alteracao WHERE id_aluno =@id_aluno";
                    SqlCommand cmd = new SqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("id_aluno", aluno.id_aluno);
                    cmd.Parameters.AddWithValue("nome",aluno.nome);
                    cmd.Parameters.AddWithValue("data_nascimento", aluno.data_nascimento);
                    cmd.Parameters.AddWithValue("sexo", aluno.sexo);
                    cmd.Parameters.AddWithValue("data_alteracao", aluno.data_UltimaAlteracao);
                    conexao.Open();
                    //Não tem retorno nenhum por isso usa NonQuery
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Alterado com sucesso\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro " + e.Message);   
                }
            }
        }

        //Método para Deletar pelo id
        public void DeletarAluno(int? id)
        {
            //Abre a conexão e depois fecha quando terminar de usar
            using(SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    string sql = "DELETE FROM Alunos WHERE id_aluno =@id_aluno";
                    SqlCommand cmd = new SqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("id_aluno", id);
                    conexao.Open();
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Deletado com Sucesso");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro " + e.Message);
                }
            }
        }

        //Método para Listar 
        public void ListarAlunos()
        {
            using (SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    string sql = "SELECT * FROM Alunos";
                    SqlCommand cmd = new SqlCommand(sql, conexao);
                    conexao.Open();
                    //Usa DataReader pois retorna valores
                    SqlDataReader tabela = cmd.ExecuteReader();

                    //Verifica se existe algo para ler se tiver ele mostra
                    if (tabela.HasRows)
                    {
                        while (tabela.Read())
                        {
                            int id = int.Parse(tabela["id_aluno"].ToString());
                            string nome = tabela["nome"].ToString();
                            DateTime data_nasci = DateTime.Parse(tabela["data_nascimento"].ToString());
                            string se = tabela["sexo"].ToString();
                            string data_cada = tabela["data_cadastro"].ToString();
                            string date_alte = tabela["data_alteracao"].ToString();

                            Console.WriteLine("===============================");
                            Console.WriteLine("Código: " + id);
                            Console.WriteLine("Nome: " + nome);
                            Console.WriteLine("Data nascimento: " + data_nasci.ToString("dd/MM/yyyy"));
                            Console.WriteLine("Sexo: " + se);
                            Console.WriteLine("Data Cadastro: " + data_cada);
                            Console.WriteLine("Data da Última Alteração: " + date_alte);
                            Console.WriteLine("\n");
                        }
                    }
                    //se não tiver nada cadastrado ele retorna a mensagem
                    else
                    {
                        Console.WriteLine("Nenhum registro encontrado");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro " + e.Message + "\n");
                }
            }
        }

        //Método para verificar se o id existe no banco de dados 
        public bool VerificaId(int? id)
        {
            using(SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    string verificaId = "SELECT COUNT(*) FROM Alunos WHERE id_aluno =@id_aluno";
                    SqlCommand cmd = new SqlCommand(verificaId, conexao);
                    cmd.Parameters.AddWithValue("id_aluno", id);
                    conexao.Open();
                    //ele vai verificar se o id digitado existe no banco contando
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if(count == 0)
                    {
                        //Se o id não existe retorna um true
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro " + e.Message);
                }
            }
            return false;
        }

        //Método para relatório por Sexo 
        public void RelatorioPorSexo(string sexs)
        {
            using(SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    string sql = "SELECT * FROM Alunos WHERE sexo = @sexo";
                    SqlCommand cmd = new SqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("sexo", sexs);
                    conexao.Open();
                    SqlDataReader tabela = cmd.ExecuteReader();

                    if (tabela.HasRows)
                    {
                        while (tabela.Read())
                        {
                            int idAluno = int.Parse(tabela["id_aluno"].ToString());
                            string nomeAluno = tabela["nome"].ToString();
                            DateTime data_nasci = DateTime.Parse(tabela["data_nascimento"].ToString());
                            sexs = tabela["sexo"].ToString();
                            string dataCadastro = tabela["data_cadastro"].ToString();
                            string dataAlteracao = tabela["data_alteracao"].ToString();

                            Console.WriteLine("=======================");
                            Console.WriteLine("Código Aluno: " + idAluno);
                            Console.WriteLine("Nome Aluno: " + nomeAluno);
                            Console.WriteLine("Data Nascimento Aluno: " + data_nasci.ToString("dd/MM/yyyy"));
                            Console.WriteLine("Sexo Aluno: " + sexs);
                            Console.WriteLine("Data Cadastro Aluno: " + dataCadastro);
                            Console.WriteLine("Data Última Alteração: " + dataAlteracao);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhum registro encontrado\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro " + e.Message);
                }
            }
        }

        //Método para relatório por data de Cadastro
        public void RelatorioDataCadastro(DateTime data_escolhida, DateTime data_Escolhida2)
        {
            using (SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    string sql = "SELECT * FROM Alunos WHERE data_cadastro >= @data_escolhida AND data_cadastro < @data_Escolhida2";
                    SqlCommand comando = new SqlCommand(sql, conexao);
                    comando.Parameters.AddWithValue("@data_escolhida", data_escolhida);
                    comando.Parameters.AddWithValue("@data_escolhida2", data_Escolhida2.AddDays(1));
                    conexao.Open();
                    SqlDataReader tabela = comando.ExecuteReader();

                    if (tabela.HasRows)
                    {
                        while (tabela.Read())
                        {
                            int idAluno = int.Parse(tabela["id_aluno"].ToString());
                            string nomeAluno = tabela["nome"].ToString();
                            DateTime data_nasci = DateTime.Parse(tabela["data_nascimento"].ToString());
                            string sexs = tabela["sexo"].ToString();
                            string dataCadastro = tabela["data_cadastro"].ToString();
                            string dataAlteracao = tabela["data_alteracao"].ToString();

                            Console.WriteLine("=======================");
                            Console.WriteLine("Código Aluno: " + idAluno);
                            Console.WriteLine("Nome Aluno: " + nomeAluno);
                            Console.WriteLine("Data Nascimento Aluno: " + data_nasci.ToString("dd/MM/yyyy"));
                            Console.WriteLine("Sexo Aluno: " + sexs);
                            Console.WriteLine("Data Cadastro Aluno: " + dataCadastro);
                            Console.WriteLine("Data Última Alteração: " + dataAlteracao);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhum Registro encontrado\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro " + e.Message);
                }
            }
        }

        //Método para relatório por data de nascimento
        public void RelatorioPorDataNascimento(DateTime data_nascimento1, DateTime data_nascimento2)
        {
            using(SqlConnection conexao = new SqlConnection(conexaoSqlServer))
            {
                try
                {
                    string sql = "SELECT * FROM Alunos WHERE data_nascimento BETWEEN @data_nascimento1 AND @data_nascimento2";
                    SqlCommand cmd = new SqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("data_nascimento1", data_nascimento1);
                    cmd.Parameters.AddWithValue("data_nascimento2", data_nascimento2);
                    conexao.Open();
                    SqlDataReader tabela = cmd.ExecuteReader();

                    if (tabela.HasRows)
                    {
                        while (tabela.Read())
                        {
                            int idAluno = int.Parse(tabela["id_aluno"].ToString());
                            string nomeAluno = tabela["nome"].ToString();
                            DateTime data_nasci = DateTime.Parse(tabela["data_nascimento"].ToString());
                            string sexs = tabela["sexo"].ToString();
                            string dataCadastro = tabela["data_cadastro"].ToString();
                            string dataAlteracao = tabela["data_alteracao"].ToString();

                            Console.WriteLine("=======================");
                            Console.WriteLine("Código Aluno: " + idAluno);
                            Console.WriteLine("Nome Aluno: " + nomeAluno);
                            Console.WriteLine("Data Nascimento Aluno: " + data_nasci.ToString("dd/MM/yyyy"));
                            Console.WriteLine("Sexo Aluno: " + sexs);
                            Console.WriteLine("Data Cadastro Aluno: " + dataCadastro);
                            Console.WriteLine("Data Última Alteração: " + dataAlteracao);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhum regristro encontrado\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro " + e.Message);
                }
            }
        }

    }
}
