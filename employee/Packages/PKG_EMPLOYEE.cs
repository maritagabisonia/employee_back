using employee.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Newtonsoft.Json;


namespace employee.Packages
{


        public interface IPKG_EMPLOYEE
        {

        public void add_employees_list(List<Employee> employee);
        public void add_employees_with_contacts(employeeWithContacts employeeWithContacts);
        public List<EmployeeDto> get_employees();
        public void add_employees(Employee employee);
        public void delete_employees(int id);
        public void update_employees(Employee employee);
        public List<profession> get_professions();
    }

        public class PKG_EMPLOYEE : PKG_BASE, IPKG_EMPLOYEE
        {

            IConfiguration Configuration;

            public PKG_EMPLOYEE(IConfiguration configuration) : base(configuration)
            {

            }


      
        public void add_employees_list(List<Employee> employees) 
        {
              string json = JsonConvert.SerializeObject(employees);
               OracleConnection conn = new OracleConnection();
                conn.ConnectionString = Connstr;

                conn.Open();

                OracleCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = "olerning.pkg_marita_parse_employee.insert_employee_from_jsons";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("p_json", OracleDbType.Clob).Value = json;

               cmd.ExecuteNonQuery();

                conn.Close();
        }
        public void add_employees_with_contacts(employeeWithContacts employeeWithContacts) {
            string json = JsonConvert.SerializeObject(employeeWithContacts);
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;

            conn.Open();

            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.pkg_marita_contacts.insert_emp_n_cont_jsons";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_json", OracleDbType.Clob).Value = json;

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<EmployeeDto> get_employees()
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;

            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.Pkg_marita_employee.get_employees";
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("emps_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                EmployeeDto EmployeeDto = new EmployeeDto();
                EmployeeDto.Id = int.Parse(reader["id"].ToString());
                EmployeeDto.FirstName = reader["first_name"].ToString();
                EmployeeDto.LastName = reader["last_name"].ToString();
                EmployeeDto.Age = int.Parse(reader["age"].ToString());
                EmployeeDto.PersonId = reader["person_id"].ToString();
                EmployeeDto.ProfessionId = int.Parse(reader["profession_id"].ToString());
                EmployeeDto.Profession = reader["profession"].ToString() ;

                employees.Add(EmployeeDto);
            }


            conn.Close();
            return employees;

        }
        public void add_employees(Employee employee){
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;

            conn.Open();    

            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.Pkg_marita_employee.add_employee";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = employee.FirstName;
            cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = employee.LastName;
            cmd.Parameters.Add("p_age", OracleDbType.Int32).Value = employee.Age;
            cmd.Parameters.Add("p_person_id", OracleDbType.Varchar2).Value = employee.PersonId;
            cmd.Parameters.Add("p_profession_id", OracleDbType.Int32).Value = employee.ProfessionId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public void delete_employees(int id)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;

            conn.Open();

            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.Pkg_marita_employee.delete_employee_by_id";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
     
            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public void update_employees(Employee employee)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;

            conn.Open();

            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.Pkg_marita_employee.update_employee";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_id", OracleDbType.Varchar2).Value = employee.Id;
            cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = employee.FirstName;
            cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = employee.LastName;
            cmd.Parameters.Add("p_age", OracleDbType.Int32).Value = employee.Age;
            cmd.Parameters.Add("p_person_id", OracleDbType.Varchar2).Value = employee.PersonId;
            cmd.Parameters.Add("p_profession_id", OracleDbType.Int32).Value = employee.ProfessionId;

            cmd.ExecuteNonQuery();

            conn.Close();
        }
        public List<profession> get_professions()
        {
            List<profession> professions = new List<profession>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = Connstr;

            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.Pkg_marita_employee.get_profession";
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("emps_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                profession profession = new profession();
                profession.Id = int.Parse(reader["id"].ToString());
                profession.Profession = reader["Profession"].ToString();

                professions.Add(profession);
            }


            conn.Close();
            return professions;

        }






    }
    }

