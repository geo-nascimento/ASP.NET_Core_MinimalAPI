using System.Text.Json.Serialization;

namespace MinCatalogoAPI.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

        //Relacionamento de um para muitos: produto possui uma categoria e a categoria pode estar em vários produtos
        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }
    }
}
