namespace BackendAPI.Models
{
    public class Servicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}