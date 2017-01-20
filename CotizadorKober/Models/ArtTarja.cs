using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CotizadorKober.Models
{
    public class ArtTarja
    {
        public string Clave { get; set; }
        public string Marca { get; set; }
        public string Nombre { get; set; }
        public string Material { get; set; }
        public decimal Precio { get; set; }
        public int Calibre { get; set; }
        public string Serie { get; set; }
        public string Acabado { get; set; }
        public decimal ProfundidadTina1 { get; set; }
        public decimal ProfundidadTina2 { get; set; }
        public decimal ProfundidadTina3 { get; set; }
        public string Presion { get; set; }
        public double Alcance { get; set; }
        public double Altura { get; set; }
        public string Ruta { get; set; }
        public string Moneda { get; set; }
    }
}