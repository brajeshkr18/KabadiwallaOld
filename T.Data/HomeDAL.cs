using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kabadi.Model;
using T.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.SqlClient;
//using TelaCall.Models;

namespace T.Data
{
    public class HomeDAL
    {
        public bool VerifyLoginDal(ref clsSignin objSignIn)
        {
            using (var maincontext = new Entity.TelaCallEntities())
            {
                try
                {
                    string username = objSignIn.Username;
                    string password = objSignIn.password;
                    var objUsers = (from user in maincontext.tblUsers.Where(user => user.Username == username && user.Password == password && user.IsActive == true)
                                    join Urole in maincontext.mtblRoles on user.RoleId equals Urole.id
                                    select new clsSignin
                                    {
                                        FirstName = user.FirstName,
                                        LastName = user.LastName,
                                        UserId = user.id,
                                        Role = Urole.Role
                                    }).FirstOrDefault();

                    if (objUsers != null)
                    {
                        objSignIn = objUsers;
                        return true;
                    }
                   
                }
                catch (Exception ex)
                {
                  //  return new JavascriptResult() { Script = "alert('Successfully registered');" };
                    return false;
                }
                objSignIn = null;
                return false;
            }
        }
       
        public int SaveKabadRequest(clsKabadRequest objKabadRequest)
        {
            using (var maincontext = new Entity.TelaCallEntities())
            {
                var Specility_Result = maincontext.Web_SaveKabadRequest(0, objKabadRequest.Name,
                    objKabadRequest.phone, objKabadRequest.Email, objKabadRequest.LocationId,
                    objKabadRequest.ItemTypeId,true).FirstOrDefault();
                return 1;
                //return Specility_Result.SpecialityResult;
            }
        }
      
    }
}
