namespace LijaApi.Controllers
{
    public class Pedidos
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        public string StatusPedidos { get; set; } = "Em andamento";
        public int ValorTotal { get; set; }
    }
}
