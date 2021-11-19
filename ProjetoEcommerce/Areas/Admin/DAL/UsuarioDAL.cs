using System;
using System.Collections.Generic;
using System.Data;
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
                sql = @"INSERT INTO usuarios (login, nome, senha) VALUES (@login, @nome, @senha)";
            }
            else
            {
                sql = @"UPDATE usuarios SET login = @login nome = @nome, senha = @senha, WHERE id = @id"; 
                parametros.Add("@id", usuario.Id);
            }
            parametros.Add("@login", usuario.Login);
            parametros.Add("@nome", usuario.Nome);
            parametros.Add("@senha", usuario.Senha);
            
            _bd.AbrirConexao();
            if (_bd.ExecutarNonQuery(sql, parametros) == 1)
            {
                if (usuario.Id == 0)
                {
                    usuario.Id = _bd.UltimoId ;
                }
                sucesso = true;
            }
            _bd.FecharConexao();

            return sucesso;
        }

        public Usuario Obter(int id)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string sql = @"SELECT * 
                           FROM usuarios 
                           WHERE id = @id";

            parametros.Add("@id", id);

            _bd.AbrirConexao();
            DataTable dt = _bd.ExecutarSelect(sql, parametros);
            _bd.FecharConexao();

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

            string sql = @"SELECT * 
                            FORM usuarios
                            WHERE nome LIKE @nome"; 

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
                Login = row["login"].ToString(),
                Nome = row["nome"].ToString(),
                Senha = row["senha"].ToString()
            };
            return u;
        }

        public bool ValidarAutenticacao(string login, string senha)
        {
            string sql = @"SELECT COUNT(*) FROM usuarios WHERE login = @login AND senha = @senha";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@login", login);
            parametros.Add("@senha", senha);
            
            _bd.AbrirConexao();
            bool ok = Convert.ToInt32(_bd.ExecutarConsultaSimples(sql, parametros)) == 1;
            _bd.FecharConexao();
            
            return ok;
        }
    }
}
