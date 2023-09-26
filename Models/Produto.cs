
using System.Text.Json.Serialization;

namespace MinCatalogoAPI.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public int CategoriaId { get; set; }//Chave estrangeira
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public string? Imagem { get; set; }
        public float Estoque { get; set; }
        public DateTime DtaCompra { get; set; }

        //Relacionamento de um para muitos: produto possui uma categoria e a categoria pode estar em vários produtos
        public Categoria? Categoria { get; set; }
    }
}
