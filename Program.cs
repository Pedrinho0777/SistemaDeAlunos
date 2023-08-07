using ProjetoCurso01.Dao;
using ProjetoCurso01.Dto;
using System;
using System.Globalization;

namespace ProjetoCurso01
{
    class Program
    {
        enum Menu { Cadastrar = 1, Alterar, Deletar, Listar,RelatórioPorSexo, RelatórioPorDataCadastro, RelatórioPorDataNascimento, Sair }
        static void Main(string[] args)
        {
            AlunoDao alunoDao = new AlunoDao();

            bool escolherSair = false;


            Console.WriteLine("                                        SISTEMA DE ALUNOS\n");

            Console.WriteLine("Digite seu email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Digite sua senha: ");
            string senha = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                Console.WriteLine("Campo em Branco ou Vazio\n");
            }
            bool Logar = alunoDao.VerificaLogin(email, senha);

            //Se exister o Usuário ele roda o programa, se não fecha
            if(Logar == true)
            {
                Console.Clear();
                //Cria o Loop para ser o menu do sistema
                while (!escolherSair)
                {
                    try
                    {
                        Console.WriteLine("Escolha uma opção: ");
                        Console.WriteLine("1-Cadastrar\n2-Alterar\n3-Deletar\n4-Listar\n5-RelatórioPorSexo\n6-RelatórioPorDataCadastro\n7-RelatórioPorDataNascimento\n8-Sair");
                        int resposta = int.Parse(Console.ReadLine());
                        Menu opcao = (Menu)resposta;

                        //Verifica se o que usuário digitar existe, se não ele retorna um erro
                        if (resposta == 1 || resposta == 2 || resposta == 3 || resposta == 4 || resposta == 5 || resposta == 6 || resposta == 7 || resposta == 8)
                        {
                            //Cria as opçoes do menu
                            switch (opcao)
                            {
                                case Menu.Cadastrar:
                                    Console.Clear();
                                    Console.WriteLine("------------CADASTRO DE ALUNO--------------\n");
                                    Console.WriteLine("Digite o nome do aluno: ");
                                    string nome1 = Console.ReadLine();
                                    Console.WriteLine("Digite data nascimento do aluno: ");
                                    string data_nasci1String = Console.ReadLine();

                                    //Tenta converter o que usuário digitou em data, se não retorna um erro
                                    DateTime data_nasci1;
                                    if (DateTime.TryParseExact(data_nasci1String, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data_nasci1))
                                    {
                                        Console.WriteLine("Digite o sexo do aluno (M/F): ");
                                        string sex1 = Console.ReadLine().ToUpper();
                                        DateTime data_cadas1 = DateTime.Now;

                                        //verifica se o campos estão nulos ou vazios
                                        if (string.IsNullOrWhiteSpace(nome1) || string.IsNullOrWhiteSpace(sex1))
                                        {
                                            Console.WriteLine("Campo em Branco ou vazio\n");
                                        }
                                        else
                                        {
                                            //verifica se o os campos corresponde ao que foi pedido, senão retorna o erro
                                            if (sex1 == "M" || sex1 == "F")
                                            {
                                                //Instancia um AlunoDto 
                                                AlunoDto aluno1 = new AlunoDto
                                                {
                                                    nome = nome1,
                                                    data_nascimento = data_nasci1,
                                                    sexo = sex1,
                                                    data_cadastro = data_cadas1
                                                };
                                                //Chama o metodo para inserir o aluno
                                                alunoDao.InserirAluno(aluno1);
                                            }
                                            else
                                            {
                                                Console.WriteLine("O Sexo deve conter M para masculino e F para feminino\n");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Data de nascimento inválida. Tente novamente.\n");
                                    }
                                    break;


                                case Menu.Alterar:
                                    Console.Clear();
                                    Console.WriteLine("Digite o Código do aluno: ");
                                    int id2 = int.Parse(Console.ReadLine());
                                    bool veriId = alunoDao.VerificaId(id2);
                                    if (veriId == true)
                                    {
                                        Console.WriteLine("Código do aluno não existe\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Digite o nome do aluno: ");
                                        string nome2 = Console.ReadLine();
                                        Console.WriteLine("Digite data nascimento do aluno: ");
                                        string data_nasci2String = Console.ReadLine();
                                        Console.WriteLine("Digite o sexo do aluno: ");
                                        string sex2 = Console.ReadLine().ToUpper();
                                        DateTime data_nasci2;

                                        if (DateTime.TryParseExact(data_nasci2String, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data_nasci2))
                                        {
                                            if (string.IsNullOrWhiteSpace(nome2) || string.IsNullOrWhiteSpace(sex2))
                                            {
                                                Console.WriteLine("Campo em Branco ou vazio\n");
                                                break;
                                            }
                                            else
                                            {
                                                if (sex2 == "M" || sex2 == "F")
                                                {
                                                    DateTime date_alte = DateTime.Now;

                                                    AlunoDto aluno2 = new AlunoDto
                                                    {
                                                        id_aluno = id2,
                                                        nome = nome2,
                                                        data_nascimento = data_nasci2,
                                                        sexo = sex2,
                                                        data_UltimaAlteracao = date_alte
                                                    };
                                                    alunoDao.AlterarAluno(aluno2);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("O Sexo deve conter M para masculino e F para feminino\n");
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Data de nascimento inválida!\n");
                                        }

                                    }
                                    break;

                                case Menu.Deletar:
                                    Console.Clear();
                                    Console.WriteLine("Digite o Código do aluno que deseja apagar: ");
                                    int idDele = int.Parse(Console.ReadLine());
                                    bool verificaIdDele = alunoDao.VerificaId(idDele);
                                    if (verificaIdDele == true)
                                    {
                                        Console.WriteLine("Código não existe");
                                    }
                                    alunoDao.DeletarAluno(idDele);
                                    break;

                                case Menu.Listar:
                                    Console.Clear();
                                    Console.WriteLine("Lista de Alunos Cadastrados");
                                    alunoDao.ListarAlunos();
                                    break;

                                case Menu.RelatórioPorSexo:
                                    Console.Clear();
                                    Console.WriteLine("Digite M para Masculino ou F para Feminino");
                                    string relatorioSexo = Console.ReadLine().ToUpper();
                                    Console.Clear();
                                    if (string.IsNullOrWhiteSpace(relatorioSexo))
                                    {
                                        Console.WriteLine("Campo em Branco ou vazio\n");
                                    }
                                    else
                                    {
                                        if (relatorioSexo == "M" || relatorioSexo == "F")
                                        {
                                            Console.WriteLine("---------Relatório Por Sexo----------");
                                            alunoDao.RelatorioPorSexo(relatorioSexo);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Para consultar o Relatório deve colocar M para Masculino e" +
                                                " F para Feminino\n");
                                            break;
                                        }
                                    }
                                    break;

                                case Menu.RelatórioPorDataCadastro:
                                    Console.Clear();
                                    Console.WriteLine("Digite a Primeira Data para Filtrar: ");
                                    string primeiraData = Console.ReadLine();
                                    Console.WriteLine("Digite a Segunda Data para Filtrar: ");
                                    string segundaData = Console.ReadLine();

                                    DateTime primeiraDataConvert;
                                    DateTime segundaDataConvert;

                                    //Tenta converter a data digita pelo usuário
                                    if (DateTime.TryParseExact(primeiraData, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out primeiraDataConvert))
                                    {
                                        if (DateTime.TryParseExact(segundaData, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out segundaDataConvert))
                                        {
                                            if (segundaDataConvert < primeiraDataConvert)
                                            {
                                                Console.WriteLine("A segunda data não pode ser anterior à primeira.\n");
                                                break;
                                            }
                                            Console.Clear();
                                            Console.WriteLine("------------Relatório Por Data Cadastrada-------------");
                                            alunoDao.RelatorioDataCadastro(primeiraDataConvert, segundaDataConvert);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Data inválida\n");
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Data inválida\n");
                                        break;
                                    }
                                    break;

                                case Menu.RelatórioPorDataNascimento:
                                    Console.Clear();
                                    Console.WriteLine("Digite a Primeira Data para Filtrar: ");
                                    string primeiraDataNasci = Console.ReadLine();
                                    Console.WriteLine("Digite a Segunda Data para Filtrar: ");
                                    string segundaDataNasci = Console.ReadLine();

                                    DateTime primeiraDataNasciConvert;
                                    DateTime segundaDataNasciConvert;

                                    //Tenta converter a data digita pelo usuário
                                    if (DateTime.TryParseExact(primeiraDataNasci, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out primeiraDataNasciConvert))
                                    {
                                        if (DateTime.TryParseExact(segundaDataNasci, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out segundaDataNasciConvert))
                                        {
                                            if (segundaDataNasciConvert < primeiraDataNasciConvert)
                                            {
                                                Console.WriteLine("A segunda data não pode ser anterior à primeira.\n");
                                                break;
                                            }
                                            Console.Clear();
                                            Console.WriteLine("------------Relatório Por Data Cadastrada-------------");
                                            alunoDao.RelatorioPorDataNascimento(primeiraDataNasciConvert, segundaDataNasciConvert);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Data inválida\n");
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Data inválida\n");
                                        break;
                                    }
                                    break;

                                case Menu.Sair:
                                    Console.Clear();
                                    escolherSair = true;
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Opção escolhida inválida\n");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Caracter inválido ou em branco\n");
                    }
                }

            }
            else
            {
                Console.WriteLine("Usuário ou Senha Inválida!\n");
            }
        }
     }
    }

