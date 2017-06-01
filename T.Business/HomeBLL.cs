using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kabadi.Model;
using T.Data;
using BizBrolly.Util;
//using TelaCall.Models;

namespace T.Business
{
    public class HomeBLL
    {
        public bool VerifyLogin(ref clsSignin objSignIn)
        {
            if ((new HomeDAL()).VerifyLoginDal(ref objSignIn))
            {
                return true;
            }
            return false;
        }
       
        public int SaveKabadRequest(clsKabadRequest objKabadRequest)
        {
            return (new HomeDAL()).SaveKabadRequest(objKabadRequest);
        }
       

       
        }
    
}
