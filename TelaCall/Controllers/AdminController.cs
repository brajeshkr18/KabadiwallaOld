using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Kabadi.Model;
using T.Business;
using System.Configuration;
using System.IO;
//using TelaCall.Models;



namespace TelaCall.Controllers
{
    [ValidateUser(AllowedRoles = "Admin")]
    public class AdminController { }
}
