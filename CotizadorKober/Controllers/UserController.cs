using CotizadorKober.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CotizadorKober.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        private CotizadorKoberModelContainer1 db = new CotizadorKoberModelContainer1();

        public static string UsuarioActual;

        //Pagina del Catálogo
        [Authorize]
        public ActionResult Index()
        {
            if (usersRepository.HasAccess(User.Identity.Name, "USERS", "READ"))
            {
                ViewBag.CanCreate = usersRepository.HasAccess(User.Identity.Name, "USERS", "CREATE");
                ViewBag.CanUpdate = usersRepository.HasAccess(User.Identity.Name, "USERS", "UPDATE");
                ViewBag.CanDelete = usersRepository.HasAccess(User.Identity.Name, "USERS", "DELETE");
                ViewBag.RoleSelectList = new SelectList(usersRepository.GetRoles(), "RoleID", "Name");
                return View(usersRepository.GetUsers());
            }
            else
                return RedirectToAction("AccessDenied", "Home");
        }

        [HttpPost]
        public JsonResult Add(string data)
        {
            if (String.IsNullOrEmpty((data ?? "").Trim()))
                return Json("Debe indicar los datos del Usuario");

            string[] values = (data ?? "").Split(new string[] { "||" }, StringSplitOptions.None);
            var sysUser = usersRepository.db.Users.Create();
            sysUser.UserName = values[0];
            sysUser.Email = values[1];
            sysUser.Password = Utils.TripleDES.Encrypt(values[2], true);
            sysUser.FirstName = values[3];
            sysUser.LastName = values[4];
            sysUser.RoleID = Convert.ToInt32(values[5]);
            //sysUser.Lista = values[6];
            usersRepository.db.Users.Add(sysUser);
            usersRepository.db.SaveChanges();
            return Json("OK");
        }

        [HttpPost]
        public JsonResult Edit(string data)
        {
            if (String.IsNullOrEmpty((data ?? "").Trim()))
                return Json("Debe indicar los datos del Usuario");

            string[] values = (data ?? "").Split(new string[] { "||" }, StringSplitOptions.None);
            string userName = values[0];
            Users sysUser = usersRepository.db.Users.FirstOrDefault(u => u.UserName == userName);
            if (sysUser == null)
                return Json("No se encontro el Usuario " + values[0]);
                
            sysUser.Email = values[1];
            if (!String.IsNullOrEmpty(values[2]))
                sysUser.Password = Utils.TripleDES.Encrypt(values[2], true);
            sysUser.FirstName = values[3];
            sysUser.LastName = values[4];
            sysUser.RoleID = Convert.ToInt32(values[5]);
            usersRepository.db.SaveChanges();
            return Json("OK");
        }

        [HttpPost]
        public JsonResult Delete(string data)
        {
            int userID = 0;
            try
            {
                userID = Convert.ToInt32(data);
            }
            catch
            {
                return Json("No se encontró el Usuario a eliminar");
            }
            Users sysUser = usersRepository.db.Users.FirstOrDefault(u => u.UserId == userID);
            if (sysUser == null)
                return Json("No se encontro el Usuario a eliminar");

            usersRepository.db.Users.Remove(sysUser);
            usersRepository.db.SaveChanges();
            return Json("OK");
        }

        [HttpGet]
        public ActionResult Login()
        {
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
        public ActionResult Login(Models.LoginModel user)
        {
            if (ModelState.IsValid) //Verificar que el modelo de datos sea válido en cuanto a la definición de las propiedades
            {
                if (Isvalid(user.UserName, user.Password))//Verificar que el email y clave exista utilizando el método privado
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false); //crea variable de usuario con el correo del usuario
                    UsuarioActual = user.UserName;
                    LogOp("Login", User.Identity.Name);
                    return RedirectToAction("Index", "Home"); //dirigir al controlador home vista Index una vez se a autenticado en el sistema
                }
                else
                {
                    ModelState.AddModelError("", "El Usuario o Password son incorrectos."); //adicionar mensaje de error al model
                }
            }
            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    var sysUser = usersRepository.db.Users.Create();
                    sysUser.Email = user.Email;
                    sysUser.Password = Utils.TripleDES.Encrypt(user.Password, true);
                    sysUser.UserName = user.UserName;
                    sysUser.FirstName = user.FirstName;
                    sysUser.LastName = user.LastName;
                    sysUser.RoleID = 2; //Se regitra siempre como consulta                    
                    sysUser.Lista = user.Lista;  //*
                    usersRepository.db.Users.Add(sysUser);
                    usersRepository.db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(user.UserName, false); //crea variable de usuario con el correo del usuario
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "La confirmación del password no coincide."); //adicionar mensaje de error al model
                }
            }
            else
            {
                ModelState.AddModelError("", "Los datos son incorrectos."); //adicionar mensaje de error al model
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool Isvalid(string UserName, string password)
        {
            bool Isvalid = false;
            var user = usersRepository.db.Users.FirstOrDefault(u => u.UserName == UserName); //consultar el primer registro con los el email del usuario
            if (user != null)
            {
                string pwdDecrypted = Utils.TripleDES.Decrypt(user.Password, true);
                if (pwdDecrypted == password) //Verificar password del usuario
                {
                    Isvalid = true;
                }
            }
            return Isvalid;
        }

        [Authorize]
        public ActionResult Roles()
        {
            if (usersRepository.HasAccess(User.Identity.Name, "ROLES", "READ"))
            {
                ViewBag.CanCreate = usersRepository.HasAccess(User.Identity.Name, "ROLES", "CREATE");
                ViewBag.CanUpdate = usersRepository.HasAccess(User.Identity.Name, "ROLES", "UPDATE");
                ViewBag.CanDelete = usersRepository.HasAccess(User.Identity.Name, "ROLES", "DELETE");
                return View(usersRepository.GetRoles());
            }
            else
                return RedirectToAction("AccessDenied", "Home");
        }

        [HttpPost]
        public JsonResult AddRole(string data)
        {
            if (String.IsNullOrEmpty((data ?? "").Trim()))
                return Json("Debe indicar los datos del Rol");

            string[] values = (data ?? "").Split(new string[] { "||" }, StringSplitOptions.None);
            var sysRole = usersRepository.db.Roles.Create();
            sysRole.Name = values[1];
            usersRepository.db.Roles.Add(sysRole);
            usersRepository.db.SaveChanges();
            return Json("OK");
        }

        [HttpPost]
        public JsonResult EditRole(string data)
        {
            if (String.IsNullOrEmpty((data ?? "").Trim()))
                return Json("Debe indicar los datos del Rol");

            string[] values = (data ?? "").Split(new string[] { "||" }, StringSplitOptions.None);
            int RoleID = Convert.ToInt32(values[0]);
            Models.Roles sysRole = usersRepository.db.Roles.FirstOrDefault(u => u.RoleID == RoleID);
            if (sysRole == null)
                return Json("No se encontro el Rol " + values[1]);

            sysRole.Name = values[1];
            usersRepository.db.SaveChanges();
            return Json("OK");
        }

        [HttpPost]
        public JsonResult DeleteRole(string data)
        {
            int roleID = 0;
            try
            {
                roleID = Convert.ToInt32(data);
            }
            catch
            {
                return Json("No se encontró el Rol a eliminar");
            }
            Models.Roles sysRole = usersRepository.db.Roles.FirstOrDefault(u => u.RoleID == roleID);
            if (sysRole == null)
                return Json("No se encontro el Rol a eliminar");

            usersRepository.db.Roles.Remove(sysRole);
            usersRepository.db.SaveChanges();
            return Json("OK");
        }

        public ActionResult SendMail()
        {
            string msg = "<table>" +
                         "<tr>" +
                         "<td><b>Header 1</b></td>" +
                         "<td><b>Header 2</b></td>" +
                         "<td><b>Header 3</b></td>" +
                         "<td><b>Header 4</b></td>" +
                         "</tr>" +
                         "<tr>" +
                         "<td>Dato 1</td>" +
                         "<td>Dato 2</td>" +
                         "<td>Dato 3</td>" +
                         "<td>Dato 4</td>" +
                         "</tr>" +
                         "</table>";
            if (Mailing.SendMail("miko187@gmail.com", "Envío de Prueba", msg))
                ViewBag.Message = "Mensaje enviado..." + msg;
            else
                ViewBag.Message = "No se envió el mensaje.";
            return View();
        }
    }
}
