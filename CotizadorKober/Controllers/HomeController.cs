using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using CotizadorKober.Models;

namespace CotizadorKober.Controllers
{
    public class HomeController : Controller
    {
        //private InnovikaComEntities db = new InnovikaComEntities();
        //private CotizadorKoberModel 
        private CotizadorKoberModelContainer1 db = new CotizadorKoberModelContainer1();
        private UsersEntities db_usr = new UsersEntities();
        
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Tarjas()
        {
            return View();
        }

        public ActionResult Mezcladoras()
        {
            return View();
        }

        public ActionResult TarjaMedida(string tipo)
        {
            ViewBag.TipoTarja = tipo;
            return View();
        }

        public ActionResult MezcladoraAlcance(string tipo)
        {
            ViewBag.TipoMezcladora = tipo;
            return View();
        }

        public ActionResult TarjaTinas(string medida, string tipo)
        {
            ViewBag.Medida = medida;
            ViewBag.TipoTarja = tipo;
            return View();
        }

        public ActionResult TarjaMueble(string tinas, string medida, string tipo)
        {
            ViewBag.Medida = medida;
            ViewBag.TipoTarja = tipo;
            ViewBag.Tinas = tinas;
            return View();
        }

        public ActionResult MezcladoraAltura(string tipo, string alcance)
        {
            ViewBag.TipoMezcladora = tipo;
            ViewBag.Alcance = alcance;
            return View();
        }

        public ActionResult TarjaEscurridero(string mueble, string tinas, string medida, string tipo)
        {
            ViewBag.Medida = medida;
            ViewBag.TipoTarja = tipo;
            ViewBag.Tinas = tinas;
            ViewBag.Mueble = mueble;
            return View();
        }

        public ActionResult TarjaEscOrient(string escurridero, string mueble, string tinas, string medida, string tipo)
        {
            ViewBag.Medida = medida;
            ViewBag.TipoTarja = tipo;
            ViewBag.Tinas = tinas;
            ViewBag.Mueble = mueble;
            ViewBag.Escurridero = escurridero;
            return View();
        }

        public ActionResult MezcladoraPresion(string tipo, string alcance, string altura)
        {
            ViewBag.TipoMezcladora = tipo;
            ViewBag.Alcance = alcance;
            ViewBag.Altura = altura;
            return View();
        }

        public ActionResult TarjaVolumen(string escOrient, string mueble, string escurridero, string tinas, string medida, string tipo)
        {
            ViewBag.Medida = medida;
            ViewBag.TipoTarja = tipo;
            ViewBag.Tinas = tinas;
            ViewBag.Escurridero = escurridero;
            ViewBag.EscOrient = escOrient;
            ViewBag.Mueble = mueble;
            return View();
        }

        public ActionResult TarjaResult(string volumen, string escOrient, string mueble, string escurridero, string tinas, string medida, string tipo)
        {
            ViewBag.Medida = medida;
            ViewBag.TipoTarja = tipo;
            ViewBag.Tinas = tinas;
            ViewBag.Escurridero = escurridero;
            ViewBag.EscOrient = escOrient;
            ViewBag.Mueble = mueble;
            ViewBag.Volumen = volumen;
            return View();
        }

        public ActionResult MezcladoraResult(string tipo, string alcance, string altura, string presion)
        {
            ViewBag.TipoMezcladora = tipo;
            ViewBag.Alcance = alcance;
            ViewBag.Altura = altura;
            ViewBag.Presion = presion;
            return View();
        }

        public ActionResult TarjasCompara(string lista, string escOrient, string mueble, string escurridero, string tinas, string medida, string tipo, string volumen)
        {
            ViewBag.Medida = medida;
            ViewBag.TipoTarja = tipo;
            ViewBag.Tinas = tinas;
            ViewBag.Escurridero = escurridero;
            ViewBag.EscOrient = escOrient;
            ViewBag.Mueble = mueble;
            ViewBag.Volumen = volumen;
            ViewBag.Lista = lista;
            return View();
        }

        public ActionResult TarjasSV(string lista, string escOrient, string mueble, string escurridero, string tinas, string medida, string tipo, string volumen)
        {
            ViewBag.Medida = medida;
            ViewBag.TipoTarja = tipo;
            ViewBag.Tinas = tinas;
            ViewBag.Escurridero = escurridero;
            ViewBag.EscOrient = escOrient;
            ViewBag.Mueble = mueble;
            ViewBag.Volumen = volumen;
            ViewBag.Lista = lista;
            return View();
        }

        public ActionResult MezcladoraCompara(string lista, string tipo, string alcance, string altura, string presion)
        {
            ViewBag.TipoMezcladora = tipo;
            ViewBag.Alcance = alcance;
            ViewBag.Altura = altura;
            ViewBag.Lista = lista;
            ViewBag.Presion = presion;
            return View();
        }

        public ActionResult MezcladoraSV(string lista, string tipo, string alcance, string altura, string presion)
        {
            ViewBag.TipoMezcladora = tipo;
            ViewBag.Alcance = alcance;
            ViewBag.Altura = altura;
            ViewBag.Lista = lista;
            ViewBag.Presion = presion;
            return View();
        }

        public JsonResult MezcladoraResult2(string tipo, string alcance, string altura, string presion)
        {
            ViewBag.TipoMezcladora = tipo;
            ViewBag.Alcance = alcance;
            ViewBag.Altura = altura;
            ViewBag.Presion = presion;

            string lst = (from i in db_usr.Users where i.UserName == User.Identity.Name select i.Lista).FirstOrDefault();

            if (alcance == "corto")
            {
                if (altura == "bajo")
                {
                    var x = (from i in db.Art
                             join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                             where i.Fabricante != "PLADOS" && i.TMELLOpc1 == tipo && i.TMELLOpc2 <= 23 && i.TMELLOpc3 <= 20 && i.TMELLOpc4.ToUpper().Contains(presion.ToUpper()) && precios.Lista == lst
                             select new ArtTarja {
                                 Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                                 Marca = i.TMESubmarca,
                                 Nombre = i.Descripcion1,
                                 Precio = precios.Precio ?? 0,
                                 Moneda = precios.Moneda,
                                 Acabado = i.TMEAcabado.ToUpper(),
                                 Material = i.TMEMaterial.ToUpper()
                             }).Distinct();
                    return Json(x, JsonRequestBehavior.AllowGet);
                }
                else if (altura == "medio")
                {
                    var x = (from i in db.Art
                             join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                             where i.Fabricante != "PLADOS" && i.TMELLOpc1 == tipo && i.TMELLOpc2 <= 23 && i.TMELLOpc3 > 20 && i.TMELLOpc3 <= 40 && i.TMELLOpc4.ToUpper().Contains(presion.ToUpper()) && precios.Lista == lst
                             select new ArtTarja
                             {
                                 Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                                 Marca = i.TMESubmarca,
                                 Nombre = i.Descripcion1,
                                 Precio = precios.Precio ?? 0,
                                 Moneda = precios.Moneda,
                                 Acabado = i.TMEAcabado.ToUpper(),
                                 Material = i.TMEMaterial.ToUpper()
                             }).Distinct();
                    return Json(x, JsonRequestBehavior.AllowGet);
                }
                else if ((altura == "alto"))
                {
                    var x = (from i in db.Art
                             join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                             where i.Fabricante != "PLADOS" && i.TMELLOpc1 == tipo && i.TMELLOpc2 <= 23 && i.TMELLOpc3 >= 41 && i.TMELLOpc3 <= 100 && i.TMELLOpc4.ToUpper().Contains(presion.ToUpper()) && precios.Lista == lst
                             select new ArtTarja
                             {
                                 Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                                 Marca = i.TMESubmarca,
                                 Nombre = i.Descripcion1,
                                 Precio = precios.Precio ?? 0,
                                 Moneda = precios.Moneda,
                                 Acabado = i.TMEAcabado.ToUpper(),
                                 Material = i.TMEMaterial.ToUpper()
                             }).Distinct();
                    return Json(x, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

            }
            else //ALCANCE LARGO
            {
                if (altura == "bajo")
                {
                    var x = (from i in db.Art
                             join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                             where i.Fabricante != "PLADOS" && i.TMELLOpc1 == tipo && i.TMELLOpc2 > 23 && i.TMELLOpc3 <= 20 && i.TMELLOpc4.ToUpper().Contains(presion.ToUpper()) && precios.Lista == lst
                             select new ArtTarja
                             {
                                 Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                                 Marca = i.TMESubmarca,
                                 Nombre = i.Descripcion1,
                                 Precio = precios.Precio ?? 0,
                                 Moneda = precios.Moneda,
                                 Acabado = i.TMEAcabado.ToUpper(),
                                 Material = i.TMEMaterial.ToUpper()
                             }).Distinct();
                    return Json(x, JsonRequestBehavior.AllowGet);
                }
                else if (altura == "medio")
                {
                    var x = (from i in db.Art
                             join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                             where i.Fabricante != "PLADOS" && i.TMELLOpc1 == tipo && i.TMELLOpc2 > 23 && i.TMELLOpc3 > 20 && i.TMELLOpc3 <= 40 && i.TMELLOpc4.ToUpper().Contains(presion.ToUpper()) && precios.Lista == lst
                             select new ArtTarja
                             {
                                 Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                                 Marca = i.TMESubmarca,
                                 Nombre = i.Descripcion1,
                                 Precio = precios.Precio ?? 0,
                                 Moneda = precios.Moneda,
                                 Acabado = i.TMEAcabado.ToUpper(),
                                 Material = i.TMEMaterial.ToUpper()
                             }).Distinct();
                    return Json(x, JsonRequestBehavior.AllowGet);
                }
                else if (altura == "alto")
                {
                    var x = (from i in db.Art
                             join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                             where i.Fabricante != "PLADOS" && i.TMELLOpc1 == tipo && i.TMELLOpc2 > 23 && i.TMELLOpc3 >= 41 && i.TMELLOpc3 <= 100 && i.TMELLOpc4.ToUpper().Contains(presion.ToUpper()) && precios.Lista == lst
                             select new ArtTarja
                             {
                                 Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                                 Marca = i.TMESubmarca,
                                 Nombre = i.Descripcion1,
                                 Precio = precios.Precio ?? 0,
                                 Moneda = precios.Moneda,
                                 Acabado = i.TMEAcabado.ToUpper(),
                                 Material = i.TMEMaterial.ToUpper()
                             }).Distinct();
                    return Json(x, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }            
        }

        public JsonResult TarjaResult2(string volumen, string escOrient, string mueble, string escurridero, string tinas, string medida, string tipo)
        {
            ViewBag.Medida = medida;
            ViewBag.TipoTarja = tipo;
            ViewBag.Tinas = tinas;
            ViewBag.Escurridero = escurridero;
            ViewBag.EscOrient = escOrient;
            ViewBag.Mueble = mueble;
            ViewBag.Volumen = volumen;

            string lst = (from i in db_usr.Users where i.UserName == User.Identity.Name select i.Lista).FirstOrDefault();

            int vTinas = Convert.ToInt32(tinas);
            Boolean vEscurridero = false;
            if (escurridero == "1")
            {
                vEscurridero = true;
            }

            int mbLow = 0;
            int mbHight = 100;
            if (mueble == "100menos")
            {
                mbLow = 0;
                mbHight = 100; 
            }
            else if (mueble == "100")
            {
                mbLow = 99;
                mbHight = 101;
            }
            else if (mueble == "100mas")
            {
                mbLow = 100;
                mbHight = 999999;
            }

            if (medida == "1") 
            {
                if (volumen == "poco")
                {
                    var x = (from i in db.Art
                             join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                             where i.Fabricante != "PLADOS" && i.TMETAOpc1 == tipo && i.TMETAOpc2 <= 1000 && i.TMETAOpc3 == vTinas && i.TMETAOpc4 == vEscurridero && i.TMETAOpc5 <= 160 && precios.Lista == lst && i.Categoria == "TARJAS" && (i.TMETipo != "ACCESORIOS") && i.Estatus == "ALTA" && i.TMEEscurrideroOrientacion == escOrient && i.TMEGabinete > mbLow && i.TMEGabinete < mbHight
                             select new ArtTarja
                             {
                                 Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                                 Marca = i.TMESubmarca,
                                 Nombre = i.Descripcion1,
                                 Precio = precios.Precio ?? 0,
                                 Moneda = precios.Moneda
                                 
                             }).Distinct();

                    return Json(x, JsonRequestBehavior.AllowGet);

                    //res = (from i in db.Art
                    //       where i.TMETAOpc1 == tipo && i.TMETAOpc2 <= 1000 && i.TMETAOpc3 == vTinas && i.TMETAOpc4 == vEscurridero && i.TMETAOpc5 <= 160
                    //       select i).Distinct() as ResultTarjas;
                }
                else
                {
                    var x = (from i in db.Art
                           join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                           where i.Fabricante != "PLADOS" && i.TMETAOpc1 == tipo && i.TMETAOpc2 <= 1000 && i.TMETAOpc3 == vTinas && i.TMETAOpc4 == vEscurridero && i.TMETAOpc5 > 160 && precios.Lista == lst && i.Categoria == "TARJAS" && (i.TMETipo != "ACCESORIOS") && i.Estatus == "ALTA" && i.TMEEscurrideroOrientacion == escOrient && i.TMEGabinete > mbLow && i.TMEGabinete < mbHight
                             select new ArtTarja
                           {
                               Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                               Marca = i.TMESubmarca,
                               Nombre = i.Descripcion1,
                               Precio = precios.Precio ?? 0,
                               Moneda = precios.Moneda

                             }).Distinct();
                    return Json(x, JsonRequestBehavior.AllowGet);
                }                           

            }
            else
            {
                if (volumen == "poco")
                {
                    var x = (from i in db.Art
                           join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                           where i.Fabricante != "PLADOS" && i.TMETAOpc1 == tipo && i.TMETAOpc2 > 1000 && i.TMETAOpc3 == vTinas && i.TMETAOpc4 == vEscurridero && i.TMETAOpc5 <= 160 && precios.Lista == lst && i.Categoria == "TARJAS" && (i.TMETipo != "ACCESORIOS") && i.Estatus == "ALTA" && i.TMEEscurrideroOrientacion == escOrient && i.TMEGabinete > mbLow && i.TMEGabinete < mbHight
                             select new ArtTarja
                             {
                                 Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                                 Marca = i.TMESubmarca,
                                 Nombre = i.Descripcion1,
                                 Precio = precios.Precio ?? 0,
                                 Moneda = precios.Moneda

                             }).Distinct(); //as ResultTarjas;
                    return Json(x, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var x = (from i in db.Art
                             join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                             where i.Fabricante != "PLADOS" && i.TMETAOpc1 == tipo && i.TMETAOpc2 > 1000 && i.TMETAOpc3 == vTinas && i.TMETAOpc4 == vEscurridero && i.TMETAOpc5 > 160 && precios.Lista == lst && i.Categoria == "TARJAS" && (i.TMETipo != "ACCESORIOS") && i.Estatus == "ALTA" && i.TMEEscurrideroOrientacion == escOrient && i.TMEGabinete > mbLow && i.TMEGabinete < mbHight
                             select new ArtTarja
                             {
                                 Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                                 Marca = i.TMESubmarca,
                                 Nombre = i.Descripcion1,
                                 Precio = precios.Precio ?? 0,
                                 Moneda = precios.Moneda

                             }).Distinct(); // as ResultTarjas;
                    return Json(x, JsonRequestBehavior.AllowGet);
                }   
            }

            
        }
        
        public ActionResult ProcesaMail(string para, string subject, string msg)
        {
            //string res = "Correo enviado";

            //if (Mailing.SendMail(para, subject, msg))
            //    res = "Mensaje enviado...";
            //else
            //    res = "Ocurreio un problema al enviar el mensaje.";

            ViewBag.Message = "Correo enviado";
            db.spEnviarDBMail("", para, "", "", subject, msg, "HTML", "", "", "", "", "", false, "", false, null, "");

            //return Json(res, JsonRequestBehavior.AllowGet);
            return View();
        }

        public JsonResult LogOp(string op, string usr)
        {
            TMELogCotizadorKober l = new TMELogCotizadorKober();
            l.fecha = DateTime.Today;
            l.accion = op;
            l.usuario = usr;

            db.TMELogCotizadorKober.Add(l);
            db.SaveChanges();

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EnviaMail(string para, string subject, string msg)
        {
            if (Mailing.SendMail(para, subject, msg))
            {
                ViewBag.Message = "Mensaje enviado...";

            }
            else
            {
                ViewBag.Message = "Ocurrio un problema al enviar el mensaje.";
            }

            //return View();
            LogOp("Mail enviado", User.Identity.Name);
            return Json(new { Status = 1, Message = "Success" });
        }

        public JsonResult ResultComparativo(string lista)
        {
            string lst = (from i in db_usr.Users where i.UserName == User.Identity.Name select i.Lista).FirstOrDefault();

            var x = (from i in db.Art
                     join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                     where lista.Contains(i.Articulo.Trim().Replace("\"", "").Replace("/","")) && (precios.Lista == lst) && i.Estatus == "ALTA"
                     select new ArtTarja
                     {
                         Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                         Marca = i.TMESubmarca,
                         Nombre = i.Descripcion1,
                         Precio = precios.Precio ?? 0,
                         Moneda = precios.Moneda,
                         Material = i.TMEMaterial,
                         Calibre = i.TMECalibre ?? 0,
                         Serie = i.TMETAOpc6,
                         Acabado = i.TMEAcabado,
                         ProfundidadTina1 = i.TMETAOpc5 ?? 0,
                         ProfundidadTina2 = i.TMETAOpc7 ?? 0,
                         ProfundidadTina3 = i.TMETAOpc8 ?? 0,
                         Alcance = i.TMELLOpc2 ?? 0,
                         Altura = i.TMELLOpc3 ?? 0,
                         Presion = i.TMELLOpc4
                     }).Distinct();

            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ResultComparativoIndiv(string lista)
        {
            string lst = (from i in db_usr.Users where i.UserName == User.Identity.Name select i.Lista).FirstOrDefault();

            lista = lista.ToUpper();

            var x = (from i in db.Art
                     join precios in db.ListaPreciosD on i.Articulo equals precios.Articulo
                     where i.Articulo.Replace("\"", "").Replace("/", "").Contains(lista) && (precios.Lista == lst) && i.Estatus == "ALTA"
                     select new ArtTarja
                     {
                         //Clave = i.ClaveFabricante,
                         //Marca = i.TMESubmarca,
                         //Nombre = i.Descripcion1,
                         //Precio = precios.Precio ?? 0,
                         //Material = "",
                         //Calibre = 0,
                         //Serie = "",
                         //Acabado = "",
                         //ProfundidadTina1 = 0,
                         //ProfundidadTina2 = 0,
                         //ProfundidadTina3 = 0
                         Clave = i.Articulo.Replace("\"", "").Replace("/", ""),
                         Marca = i.TMESubmarca,

                         Nombre = i.Descripcion1,
                         Precio = precios.Precio ?? 0,
                         Moneda = precios.Moneda,
                         Material = i.TMEMaterial,

                         Calibre = i.TMECalibre ?? 0,
                         Serie = i.TMETAOpc6,
                         Acabado = i.TMEAcabado,

                         ProfundidadTina1 = i.TMETAOpc5 ?? 0,
                         ProfundidadTina2 = i.TMETAOpc7 ?? 0,
                         ProfundidadTina3 = i.TMETAOpc8 ?? 0,

                         Alcance = i.TMELLOpc2 ?? 0,
                         Presion = i.TMELLOpc4,
                         Altura = i.TMELLOpc3 ?? 0
                     }).Distinct();

            return Json(x, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultaCodigo(string codigo)
        {
            ViewBag.Codigo = codigo;
            return View();
        }
    }
}
