using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebApp.Views.User
{
    public class BehindClass
    {
        [WebMethod]
        public string PageOneMethodOne()
        {
            return "hello world";
        }
    }
}