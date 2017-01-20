using CotizadorKober.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CotizadorKober.Controllers
{
    public class BaseController : Controller 
    {
        public UsersRepository usersRepository;
        public BaseController()
        {
            usersRepository = new UsersRepository();
        }
    }
}
