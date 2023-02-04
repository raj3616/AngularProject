using Newtonsoft.Json;
using SkillServices.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkillServices.Controllers
{
    public class SkillDisplayController : Controller
    {
        DataTable dt = null;

        [HttpGet]
        public JsonResult getEmpData()
        {
            string res = string.Empty;
            try
            {
                DatabaseQuery dq = new DatabaseQuery();
                dt = dq.getEmpData();
                if (dt != null && dt.Rows.Count > 0)
                {
                    res = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}