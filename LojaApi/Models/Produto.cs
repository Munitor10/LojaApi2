namespace LojaApi.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Decscricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; } = 1;
    }
}
