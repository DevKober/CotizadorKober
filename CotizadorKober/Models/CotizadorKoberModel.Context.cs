﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CotizadorKober.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class CotizadorKoberModelContainer1 : DbContext
    {
        public CotizadorKoberModelContainer1()
            : base("name=CotizadorKoberModelContainer1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Art> Art { get; set; }
        public DbSet<ListaPreciosD> ListaPreciosD { get; set; }
        public DbSet<TMELogCotizadorKober> TMELogCotizadorKober { get; set; }
    
        public virtual int spEnviarDBMail(string perfil, string para, string cC, string cCO, string asunto, string mensaje, string formato, string importancia, string sensitividad, string adjuntos, string sQL, string sQLBase, Nullable<bool> sQLAnexar, string sQLAnexarNombre, Nullable<bool> sQLConEncabezados, Nullable<int> sQLAncho, string sQLSeparador)
        {
            var perfilParameter = perfil != null ?
                new ObjectParameter("Perfil", perfil) :
                new ObjectParameter("Perfil", typeof(string));
    
            var paraParameter = para != null ?
                new ObjectParameter("Para", para) :
                new ObjectParameter("Para", typeof(string));
    
            var cCParameter = cC != null ?
                new ObjectParameter("CC", cC) :
                new ObjectParameter("CC", typeof(string));
    
            var cCOParameter = cCO != null ?
                new ObjectParameter("CCO", cCO) :
                new ObjectParameter("CCO", typeof(string));
    
            var asuntoParameter = asunto != null ?
                new ObjectParameter("Asunto", asunto) :
                new ObjectParameter("Asunto", typeof(string));
    
            var mensajeParameter = mensaje != null ?
                new ObjectParameter("Mensaje", mensaje) :
                new ObjectParameter("Mensaje", typeof(string));
    
            var formatoParameter = formato != null ?
                new ObjectParameter("Formato", formato) :
                new ObjectParameter("Formato", typeof(string));
    
            var importanciaParameter = importancia != null ?
                new ObjectParameter("Importancia", importancia) :
                new ObjectParameter("Importancia", typeof(string));
    
            var sensitividadParameter = sensitividad != null ?
                new ObjectParameter("Sensitividad", sensitividad) :
                new ObjectParameter("Sensitividad", typeof(string));
    
            var adjuntosParameter = adjuntos != null ?
                new ObjectParameter("Adjuntos", adjuntos) :
                new ObjectParameter("Adjuntos", typeof(string));
    
            var sQLParameter = sQL != null ?
                new ObjectParameter("SQL", sQL) :
                new ObjectParameter("SQL", typeof(string));
    
            var sQLBaseParameter = sQLBase != null ?
                new ObjectParameter("SQLBase", sQLBase) :
                new ObjectParameter("SQLBase", typeof(string));
    
            var sQLAnexarParameter = sQLAnexar.HasValue ?
                new ObjectParameter("SQLAnexar", sQLAnexar) :
                new ObjectParameter("SQLAnexar", typeof(bool));
    
            var sQLAnexarNombreParameter = sQLAnexarNombre != null ?
                new ObjectParameter("SQLAnexarNombre", sQLAnexarNombre) :
                new ObjectParameter("SQLAnexarNombre", typeof(string));
    
            var sQLConEncabezadosParameter = sQLConEncabezados.HasValue ?
                new ObjectParameter("SQLConEncabezados", sQLConEncabezados) :
                new ObjectParameter("SQLConEncabezados", typeof(bool));
    
            var sQLAnchoParameter = sQLAncho.HasValue ?
                new ObjectParameter("SQLAncho", sQLAncho) :
                new ObjectParameter("SQLAncho", typeof(int));
    
            var sQLSeparadorParameter = sQLSeparador != null ?
                new ObjectParameter("SQLSeparador", sQLSeparador) :
                new ObjectParameter("SQLSeparador", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spEnviarDBMail", perfilParameter, paraParameter, cCParameter, cCOParameter, asuntoParameter, mensajeParameter, formatoParameter, importanciaParameter, sensitividadParameter, adjuntosParameter, sQLParameter, sQLBaseParameter, sQLAnexarParameter, sQLAnexarNombreParameter, sQLConEncabezadosParameter, sQLAnchoParameter, sQLSeparadorParameter);
        }
    }
}
