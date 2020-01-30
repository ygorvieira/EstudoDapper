using System.Collections.Generic;
using EstudoDapper.Entities;

namespace EstudoDapper.Repository
{
    interface IProdutoRepository
    {
        int Add(Produto produto);
        List<Produto> GetProdutos();
        Produto Get(int id);
        int Edit(Produto produto);
        int Delete(int id);
    }
}
