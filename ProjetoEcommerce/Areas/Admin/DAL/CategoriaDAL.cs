using System;
using System.Collections.Generic;
using System.Data;
using ProjetoEcommerce.Areas.Admin.Models;
namespace ProjetoEcommerce.DAL
{
    public class CategoriaDAL
    {
        MySQLPersistence _bd = new MySQLPersistence();

        public bool Salvar(Categoria categoria)
        {
            bool sucesso = false;
            string sql;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
           
            if (categoria.Id == 0)
            {
                sql = @"INSERT INTO categorias (nome) 
                        VALUES (@nome)";
            }
            else
            {
                sql = @"UPDATE categorias SET nome = @nome 
                        WHERE id = @id";

                parametros.Add("@id", categoria.Id);
            }
            parametros.Add("@nome", categoria.Nome);

            _bd.AbrirConexao();
            if (_bd.ExecutarNonQuery(sql, parametros) == 1)
            {
                if (categoria.Id == 0)
                {
                    categoria.Id = _bd.UltimoId;
                }
                sucesso = true;
            }
            _bd.FecharConexao();

            return sucesso;
        }

        public Categoria Obter(int id)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string sql = @"SELECT * 
                           FROM categorias 
                           WHERE id = @id";

            parametros.Add("@id", id);
            DataTable dt = _bd.ExecutarSelect(sql, parametros);
            if (dt.Rows.Count != 0)
            {
                Categoria c = Map(dt.Rows[0]);
                return c;
            }
            return null;
        }

        public IEnumerable<Categoria> Consulta(string nome)
        {
            List<Categoria> categorias = new List<Categoria>();
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            string sql = @"SELECT * 
                           FROM categorias 
                           WHERE nome LIKE @nome";

            parametros.Add("@nome", "%" + nome + "%");
            _bd.AbrirConexao();
            DataTable dt = _bd.ExecutarSelect(sql, parametros);
            _bd.FecharConexao();

            foreach (DataRow row in dt.Rows)
            {
                categorias.Add(Map(row));
            }

            return categorias;
        }
        
        public IEnumerable<Categoria> Listar()
        {
            List<Categoria> categorias = new List<Categoria>();
            string sql = "SELECT *  FROM categorias";

            _bd.AbrirConexao();
            DataTable dt = _bd.ExecutarSelect(sql);
            _bd.FecharConexao();

            foreach (DataRow row in dt.Rows)
            {
                categorias.Add(Map(row));
            }

            return categorias;
        }

        private Categoria Map(DataRow row)
        {
            Categoria p = new Categoria()
            {
                Id = Convert.ToInt32(row["id"]),
                Nome = row["nome"].ToString(),
            };
            return p;
        }
    }
}