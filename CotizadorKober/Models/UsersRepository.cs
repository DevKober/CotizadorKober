using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotizadorKober.Models
{
    public class UsersRepository
    {
        public UsersEntities db;
        public UsersRepository()
        {
            db = new UsersEntities();
        }

        public List<Users> GetUsers()
        {
            return db.Users.ToList();
        }

        public string InsertUser(Users user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return null;
            }
            catch (Exception ex)
            { return ex.Message; }
        }

        public string UpdateUser(Users user)
        {
            try
            {
                Users original = db.Users.FirstOrDefault(u => u.UserId == user.UserId);
                if (original != null)
                {
                    original.FirstName = user.FirstName;
                    original.LastName = user.LastName;
                    db.SaveChanges();
                    return null;
                }
                else
                    return "No se encontro el usuario";
            }
            catch(Exception ex)
            { return ex.Message; }
        }

        public string DeleteUser(int UserID)
        {
            try
            {
                Users user = db.Users.FirstOrDefault(u => u.UserId == UserID);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return null;
                }
                else
                    return "No se encontro el usuario";
            }
            catch(Exception ex)
            { return ex.Message; }
        }

        public bool HasAccess(string userName, string keyValue, string crudeValue)
        {
            bool returnAccess = false;
            Permission permission = db.Permissions.FirstOrDefault(p => (p.UserName == userName && p.PermissionKey == keyValue));
            if (permission != null)
            {
                switch(crudeValue)
                {
                    case "CREATE": return permission.CreateOpt;
                    case "READ": return permission.ReadOpt;
                    case "UPDATE": return permission.UpdateOpt;
                    case "DELETE": return permission.DeleteOpt;
                    case "EXECUTE": return permission.ExecuteOpt;
                }
            }
            return returnAccess;
        }

        public List<Roles> GetRoles()
        {
            return db.Roles.ToList();
        }
    }
}
