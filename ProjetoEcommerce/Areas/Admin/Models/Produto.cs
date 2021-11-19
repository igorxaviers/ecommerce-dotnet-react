using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEcommerce.Areas.Admin.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public Categoria Categoria{ get; set; }

        public (bool, string) Validar()
        {
            if (this.Nome == null || this.Nome.Length < 3)
            {
                return (false, "Nome inválido");
            }

            if (this.Preco <= 0)
            {
                return (false, "Preço inválido");
            }

            if (this.Estoque <= 0)
            {
                return (false, "Estoque inválido");
            }
            return (true, null);
        }
    }
}
