using System;

namespace LN_WEB.Entities
{
    public class FacturaEnt
    {
    public long ConMaestro { get; set; }
    public long IdUsuario { get; set; }
    public DateTime FechaCompra { get; set; }
    public decimal TotalCompra { get; set; }


    public string Nombre { get; set; }
    public decimal PrecioPagado { get; set; }
    public int CantidadPagado { get; set; }
    public decimal ImpuestoPagado { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Impuesto { get; set; }

    public decimal Total { get; set; }


}

}
