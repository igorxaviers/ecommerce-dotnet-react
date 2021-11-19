using System;
using System.Collections.Generic;
using System.Data;
using ProjetoEcommerce.Areas.Admin.Models;
namespace ProjetoEcommerce.DAL
{
    public class ProdutoDAL
    {
        MySQLPersistence _bd = new MySQLPersistence();

        public bool Salvar(Produto produto)
        {
            bool sucesso = false;
            string sql;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
           
            if (produto.Id == 0)
            {
                sql = @"INSERT INTO produtos (nome, preco, estoque, categoria_id) VALUES (@nome, @preco, @estoque, @categoria)";
            }
            else
            {
                sql = @"UPDATE produtos SET
                        nome = @nome,
                        preco = @preco,
                        estoque = @estoque,
                        categoria_id = @categoria
                        WHERE id = @id"; 

                parametros.Add("@id", produto.Id);
            }
            parametros.Add("@nome", produto.Nome);
            parametros.Add("@preco", produto.Preco);
            parametros.Add("@estoque", produto.Estoque);
            parametros.Add("@categoria", produto.Categoria.Id);

            _bd.AbrirConexao();
            if (_bd.ExecutarNonQuery(sql, parametros) == 1)
            {
                if (produto.Id == 0)
                {
                    produto.Id = _bd.UltimoId;
                }
                sucesso = true;
            }
            _bd.FecharConexao();

            return sucesso;
        }

        public Produto Obter(int id)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string sql = @"SELECT * 
                           FROM produtos 
                           WHERE id = @id";

            parametros.Add("@id", id);
            DataTable dt = _bd.ExecutarSelect(sql, parametros);
            if (dt.Rows.Count != 0)
            {
                Produto p = Map(dt.Rows[0]);
                return p;
            }
            return null;
        }

        public IEnumerable<Produto> Consulta(string nome)
        {
            List<Produto> produtos = new List<Produto>();
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            string sql = @"SELECT p.id, p.nome, p.preco, p.estoque, c.id as categoria_id, c.nome AS categoria_nome
                            FROM produtos AS p
                            INNER JOIN categorias AS c
                            ON p.categoria_id = c.id 
                            WHERE p.nome LIKE @nome";

            parametros.Add("@nome", "%" + nome + "%");
            _bd.AbrirConexao();
            DataTable dt = _bd.ExecutarSelect(sql, parametros);
            _bd.FecharConexao();

            foreach (DataRow row in dt.Rows)
            {
                produtos.Add(Map(row));
            }

            return produtos;
        }

        public bool Excluir(int id)
        {
            bool sucesso = false;
            string sql = @"DELETE FROM produtos WHERE id = @id";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@id", id);

            _bd.AbrirConexao();
            if (_bd.ExecutarNonQuery(sql, parametros) == 1)
            {
                sucesso = true;
            }
            _bd.FecharConexao();

            return sucesso;
        }
        private Produto Map(DataRow row)
        {
            Produto p = new Produto()
            {
                Id = Convert.ToInt32(row["id"]),
                Nome = row["nome"].ToString(),
                Preco = Convert.ToDecimal(row["preco"]),
                Estoque = Convert.ToInt32(row["estoque"]),
                Categoria = new Categoria(Convert.ToInt32(row["categoria_id"]), row["categoria_nome"].ToString()),
            };
            return p;
        }
    }
}