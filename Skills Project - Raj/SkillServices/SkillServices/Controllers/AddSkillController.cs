using Newtonsoft.Json;
using SkillServices.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


//<system.webServer>
//		<httpProtocol>
//			<customHeaders>
//				<add name="Access-Control-Allow-Origin" value="*" />
//				<add name="Access-Control-Allow-Methods" value="*" />
//				<add name="Access-Control-Allow-Headers" value="*" />
//			</customHeaders>
//		</httpProtocol>
//	</system.webServer>

namespace SkillServices.Controllers
{
    public class AddSkillController : Controller
    {
        DataTable dt = null;

        [HttpGet]
        public JsonResult getSkills()
        {
            string res = string.Empty;
            try
            {
                DatabaseQuery dq = new DatabaseQuery();
                dt = dq.getSkills();
                if(dt!=null && dt.Rows.Count > 0)
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

        [HttpPost]
        public string saveEmployeeInfoAndSkills(string Empdata)
        {
            string res = string.Empty;
            EmpAndSkillData data = JsonConvert.DeserializeObject<EmpAndSkillData>(Empdata);
            try
            {
                DatabaseQuery dq = new DatabaseQuery();
                if (data.EmpName != null)
                {
                    if (data.EmpId == 0)
                    {
                        DataTable dt1= dq.checkName(data.EmpName);
                        if (dt1.Rows.Count > 0)
                        {
                            res = "Error: Employee with this name already exist";
                            return JsonConvert.SerializeObject(res);
                        }
                        else
                        {
                            data.EmpId = dq.saveEmployee(data.EmpName);
                        }
                        
                    }
                    int chk = 0;
                    if (data.EmpId > 0)
                    {
                        chk = dq.deleteExistingSkill(data.EmpId);
                        for (int i = 0; i < data.Skills.Length; i++)
                        {
                            chk = dq.saveEmployeeSkills(data.EmpId, data.Skills[i]);
                        }
                    }
                    if (data.EmpId > 0 && chk > 0)
                    {
                        res = "Success:Skills Saved";
                    }
                    else
                    {
                        res = "Failed:";
                    }
                }
                else
                {
                    res = "Failed:EmpName is null";
                }
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return JsonConvert.SerializeObject(res);
        }

        [HttpPost]
        public string deleteEmployee(string EmpId)
        {
            string res = string.Empty;
            int i = 0;
            try
            {
                DatabaseQuery dq = new DatabaseQuery();
                i = dq.deleteEmployee(Convert.ToInt32(EmpId));
                res = i > 0 ? "Success: Record Deleted" : "Failed: Record not deleted";
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return JsonConvert.SerializeObject(res);
        }
    }


    public class EmpAndSkillData
    {
        public int EmpId=0;
        public string EmpName;
        public int[] Skills=null;
    }
}