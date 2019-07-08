using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers
{
    public abstract class CrudController<TDataObject, TICrudService> : ApiController
    {

    }
}
