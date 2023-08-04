using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCurso01.Dto
{
    public  class AlunoDto
    {
        public int id_aluno { get; set; }
        public string nome { get; set; }
        public DateTime data_nascimento { get; set; }
        public string sexo { get; set; }
        public DateTime data_cadastro { get; set; } 
        public DateTime data_UltimaAlteracao { get; set; }
    }
}
