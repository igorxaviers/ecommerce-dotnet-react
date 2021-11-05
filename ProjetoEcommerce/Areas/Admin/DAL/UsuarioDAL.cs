using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ProjetoEcommerce.Areas.Admin.Models;

namespace ProjetoEcommerce.DAL
{
    public class UsuarioDAL
    {
        MySQLPersistence _bd = new MySQLPersistence();

        public bool Salvar(Usuario usuario)
        {
            bool sucesso = false;
            string sql;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            if (usuario.Id == 0)
            {
                sql = @"insert into Usuario (nome, senha) values (@nome, @senha)";
            }
            else
            {
                sql = @"update Usuario set nome = @nome, senha = @senha 
                        where id = @id";

                parametros.Add("@id", usuario.Id);
            }

            parametros.Add("@nome", usuario.Nome);
            parametros.Add("@senha", usuario.Senha);
            
            if (_bd.ExecutarNonQuery(sql, parametros) == 1)
            {
                if (usuario.Id == 0)
                {
                    usuario.Id = _bd.UltimoId ;
                }

                sucesso = true;
            }

            return sucesso;
        }

        public Usuario Obter(int id)
        {
            string sql = @"select * 
                           from Usuario 
                           where id = @id";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@id", id);

            DataTable dt = _bd.ExecutarSelect(sql, parametros);

            if (dt.Rows.Count == 0)
                return null;
            else
            {
                Usuario u = Map(dt.Rows[0]);

                return u;
            }

        }

        public IEnumerable<Usuario> Consulta(string nome)
        {
            List<Usuario> usuarios = new List<Usuario>();

            string sql = @"select * 
                           from Usuario 
                           where nome = @nome";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@nome", nome);

            DataTable dt = _bd.ExecutarSelect(sql, parametros);

            foreach (DataRow row in dt.Rows)
            {
                usuarios.Add(Map(row));
            }

            return usuarios;
        }

        private Usuario Map(DataRow row)
        {
            Usuario u = new Usuario()
            {
                Id = Convert.ToInt32(row["id"]),
                Nome = row["nome"].ToString(),
                Senha = row["senha"].ToString(),
            };

            return u;
        }

        public bool ValidarAutenticacao(string login, string senha)
        {
            string sql = @"select count(*) from Usuario where login = @login and senha = @senha";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@login", login);
            parametros.Add("@senha", senha);

            bool ok = Convert.ToInt32(_bd.ExecutarConsultaSimples(sql, parametros)) == 1;
            Object obj = _bd.ExecutarConsultaSimples(sql, parametros);
            return ok;
        }
    }
}
