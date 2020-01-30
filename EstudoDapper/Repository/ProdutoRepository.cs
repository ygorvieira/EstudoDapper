using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using EstudoDapper.Entities;
using Microsoft.Extensions.Configuration;

namespace EstudoDapper.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        IConfiguration _configurarion;

        public ProdutoRepository(IConfiguration configurarion)
        {
            _configurarion = configurarion;
        }

        public string GetConnection()
        {
            var connection = _configurarion.GetSection("ConnectionStrings")
                                            .GetSection("ProdutoConnection").Value;

            return connection;
        }

        public int Add(Produto produto)
        {
            var connectionString = this.GetConnection();
            int count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "INSERT INTO Produtos(Nome, Estoque, Preco) VALUES(@Nome, @Estoque, @Preco);SELECT CAST(SCOPE_IDENTITY() as INT);";
                    count = con.Execute(query, produto);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }
        }

        public int Delete(int id)
        {
            var connectionString = this.GetConnection();
            int count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "DELETE FROM Produtos WHERE ProdutoId = " + id;
                    count = con.Execute(query);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }
        }

        public int Edit(Produto produto)
        {
            var connectionString = this.GetConnection();
            int count = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "UPDATE Produtos SET Name = @Nome, Estoque = @Estoque, Preco = @Preco WHERE ProdutoId = " + produto.ProdutoId;;
                    count = con.Execute(query, produto);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

                return count;
            }
        }

        public Produto Get(int id)
        {
            var connectionString = this.GetConnection();
            Produto produto = new Produto();

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Produtos WHERE ProdutoId = " + id;
                    produto = con.Query<Produto>(query).FirstOrDefault();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }

                return produto;
            }
        }

        public List<Produto> GetProdutos()
        {
            var connectionString = this.GetConnection();
            List<Produto> produtos = new List<Produto>();
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    var query = "SELECT * FROM Produtos";
                    produtos = con.Query<Produto>(query).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
                return produtos;
            }
        }
    }
}
