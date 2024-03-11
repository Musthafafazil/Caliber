using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CaliberMVC.Models;

namespace CaliberMVC.DBContext
{
    public class EmployeeDBConetxt
    {
        string conString = ConfigurationManager.ConnectionStrings["CaliberConnection"].ToString();

        //Get All Employees
        public List<Employee> GetAllEmployees()
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure; // Set command type to stored procedure
                command.CommandText = "GET_EMPLOYEE_MSTR_DATA_SP";

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Employee> employees = new List<Employee>();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeId = reader.GetInt32(reader.GetOrdinal("ID"));
                        employee.Name = reader.GetString(reader.GetOrdinal("EMP_NAME"));
                        employee.Email = reader.GetString(reader.GetOrdinal("EMP_MAIL"));
                        employee.DateOfBirth = reader.GetDateTime(reader.GetOrdinal("EMP_DOB"));
                        employee.ExperienceLevel = reader.GetString(reader.GetOrdinal("EMP_EXP_LEVEL"));
                        employee.Gender = reader.GetString(reader.GetOrdinal("EMP_GENDER"));
                        employee.Address = reader.GetString(reader.GetOrdinal("EMP_ADDRESS"));
                        employee.IsDeleted = reader.GetBoolean(reader.GetOrdinal("EMP_IS_DELETED"));
                        employees.Add(employee);
                    }

                    return employees;
                }
            }
        }

        // Insert Employee
        public bool InsertEmployee(Employee employee)
        {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("INSERT_EMPLOYEE_MSTR_DATA_SP", connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable tvpDataTable = new DataTable();
                tvpDataTable.Columns.Add("EmployeeId", typeof(int));
                tvpDataTable.Columns.Add("Name", typeof(string)); // Adjust data types as needed
                tvpDataTable.Columns.Add("Email", typeof(string));
                tvpDataTable.Columns.Add("DateOfBirth", typeof(DateTime)); // Assuming DateOfBirth is of type DateTime
                tvpDataTable.Columns.Add("ExperienceLevel", typeof(string));
                tvpDataTable.Columns.Add("Gender", typeof(string));
                tvpDataTable.Columns.Add("Address", typeof(string));
                tvpDataTable.Columns.Add("IsDeleted", typeof(bool));
                // Populate DataTable with Employee data

                tvpDataTable.Rows.Add(employee.EmployeeId, employee.Name, employee.Email, employee.DateOfBirth, employee.ExperienceLevel,employee.Gender, employee.Address, employee.IsDeleted);


                SqlParameter tvpParameter = new SqlParameter("@EMPLOYEE_LIST", SqlDbType.Structured);
                tvpParameter.TypeName = "TT_EMPLOYEE_LIST";
                tvpParameter.Value = tvpDataTable; 
                command.Parameters.Add(tvpParameter);


                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    success = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                    success = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            return success;

        }


        //Get  Employee BY ID
        public List<Employee> GetEmployee(int ID)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure; 
                command.CommandText = "GET_EMPLOYEE_MSTR_DATA_BY_ID_SP";
                command.Parameters.AddWithValue("@id", ID);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Employee> employees = new List<Employee>();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeId = reader.GetInt32(reader.GetOrdinal("ID"));
                        employee.Name = reader.GetString(reader.GetOrdinal("EMP_NAME"));
                        employee.Email = reader.GetString(reader.GetOrdinal("EMP_MAIL"));
                        employee.DateOfBirth = reader.GetDateTime(reader.GetOrdinal("EMP_DOB"));
                        employee.ExperienceLevel = reader.GetString(reader.GetOrdinal("EMP_EXP_LEVEL"));
                        employee.Gender = reader.GetString(reader.GetOrdinal("EMP_GENDER"));
                        employee.Address = reader.GetString(reader.GetOrdinal("EMP_ADDRESS"));
                        employee.IsDeleted = reader.GetBoolean(reader.GetOrdinal("EMP_IS_DELETED"));
                        employees.Add(employee);
                    }

                    return employees;
                }
            }
        }



    }
}