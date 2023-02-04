using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SkillServices.Models
{
    public class DatabaseQuery
    {
        SqlConnection cnn = null;
        string query = string.Empty;
        SqlCommand sqlCommand = null;
        SqlDataAdapter sda = null;
        DataTable dataTable = null;
        public void connect()
        {
            string connetionString = "";
            connetionString = "Data Source=.;Initial Catalog=DBEmployee;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
        }

        public void disconnect()
        {
            if(cnn!=null && cnn.State == System.Data.ConnectionState.Open)
            {
                cnn.Close();
                cnn = null;
            }
        }

        public DataTable getSkills()
        {
            connect();
            query = "select * from Skills";
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = cnn;
            sqlCommand.CommandText = query;

            dataTable = new DataTable();
            sda = new SqlDataAdapter(sqlCommand);
            sda.Fill(dataTable);
            disconnect();

            return dataTable;
        }

        public int saveEmployee(string name)
        {
            connect();
            int i = 0;
            query = $"insert into Employee values('{name}')";
            query += "  select scope_identity();";
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = cnn;
            sqlCommand.CommandText = query;
            i = Convert.ToInt32(sqlCommand.ExecuteScalar());
            disconnect();

            return i;
        }

        public int saveEmployeeSkills(int EmpId,int SkillId)
        {
            connect();
            int i = 0;
            query = $"insert into EmployeeSkill values({EmpId},{SkillId})";
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = cnn;
            sqlCommand.CommandText = query;
            i = sqlCommand.ExecuteNonQuery();
            disconnect();
            return i;
        }

        public DataTable getEmpData()
        {
            connect();
            query = @"select Employee.EmpId,EmpName,
                    STUFF(
                    (select ','+Skill from EmployeeSkill ES
                    INNER JOIN Skills S ON S.SkillId = ES.SkillId WHERE ES.EmpId = EmployeeSkill.EmpId
                    FOR XML PATH('')),1,1,'') as Skills,
                    STUFF(
                    (select ','+CAST(ES.SkillId as varchar(max)) from EmployeeSkill ES WHERE ES.EmpId = EmployeeSkill.EmpId
                    FOR XML PATH('')),1,1,'') as SkillsIds
                    from Employee 
                    INNER JOIN EmployeeSkill ON EmployeeSkill.EmpId = Employee.EmpId
                    GROUP BY Employee.EmpId,EmployeeSkill.EmpId,EmpName";

            sqlCommand = new SqlCommand();
            sqlCommand.Connection = cnn;
            sqlCommand.CommandText = query;

            dataTable = new DataTable();
            sda = new SqlDataAdapter(sqlCommand);
            sda.Fill(dataTable);
            disconnect();

            return dataTable;
        }

        public int deleteEmployee(int EmpId)
        {
            connect();
            int i = 0;
            query = $"delete from EmployeeSkill where EmpId={EmpId}; delete from Employee where EmpId={EmpId};";
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = cnn;
            sqlCommand.CommandText = query;
            i = sqlCommand.ExecuteNonQuery();
            disconnect();
            return i;
        }

        public int deleteExistingSkill(int EmpId)
        {
            connect();
            int i = 0;
            query = $"delete from EmployeeSkill where EmpId={EmpId};";
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = cnn;
            sqlCommand.CommandText = query;
            i = sqlCommand.ExecuteNonQuery();
            disconnect();
            return i;
        }

        public DataTable checkName(string EmpName)
        {
            connect();
            query = $"select * from Employee where EmpName = '{EmpName}'";
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = cnn;
            sqlCommand.CommandText = query;

            dataTable = new DataTable();
            sda = new SqlDataAdapter(sqlCommand);
            sda.Fill(dataTable);
            disconnect();

            return dataTable;
        }

    }
}