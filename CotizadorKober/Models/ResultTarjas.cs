using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CotizadorKober.Models;

namespace CotizadorKober.Models
{
    public class ResultTarjas
    {
        public IQueryable<Art> Articulos { get; set; }
        //public string Nombre { get; set; }
        //public string RutaImagen { get; set; }
        //public decimal Precio { get; set; }
    }
}