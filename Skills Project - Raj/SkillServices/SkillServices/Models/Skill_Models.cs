using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkillServices.Models
{
    public class Skill_Models
    {
        public int SkillId  {get; set;}
        public string Skill { get; set; }
        public decimal Score { get; set; }
    }

    public class Employee_Models
    {
        public int EmpId { get; set; }
        public string Employee { get; set; }
    }

    public class EmployeeSkill_Models
    {
        public int EmpId { get; set; }
        public int SkillId { get; set; }
    }
}