namespace BackendAPI.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        public DateTime FechaReserva { get; set; }
        public string Estado { get; set; } = "Pendiente";

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int ServicioId { get; set; }
        public Servicio? Servicio { get; set; }

        public Pago? Pago { get; set; }
    }
}