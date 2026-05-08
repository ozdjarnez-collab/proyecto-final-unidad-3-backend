namespace BackendAPI.Models
{
    public class Pago
    {
        public int Id { get; set; }

        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; } = string.Empty;

        public int ReservaId { get; set; }
        public Reserva? Reserva { get; set; }
    }
}