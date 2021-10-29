using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Models
{
    public class Categoria
    {
        public Categoria(){}
        public Categoria(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }
        public int Id { get; set; }
        public string Nome { get; set; }

        public (bool, string) Validar()
        {
            if (string.IsNullOrEmpty(this.Nome))
                return (false, "Nome da categoria é obrigatório");

            return (true, null);
        }
    }
}
