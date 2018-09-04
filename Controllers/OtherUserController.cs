using MiWebObj.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MiWebObj.Models
{
    public class OtherUserController : ApiController
    {
        List<User> ListUsers;
        Connection SqlConection;
        DataTableReader Reader;
        User Person = new User();
        List<SqlParameter> Parametros;
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            ListUsers = new List<User>();
            SqlConection = new Connection();
            Parametros = new List<SqlParameter>();
            Reader = null;
            try
            {
                //Se abre conexion
                SqlConection.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                SqlConection.PrepararProcedimiento("sp_AllUsers", Parametros);
                Reader = SqlConection.EjecutarTableReader();
                if (Reader.HasRows)
                {
                    //Leer la informacion
                    while (Reader.Read())
                    {
                        //Se crea un objeto de clase usuario
                        Person = new User()
                        {
                            Id = int.Parse(Reader["Id"].ToString()),
                            Name = Reader["Name"].ToString(),
                            FLastName = Reader["FLastName"].ToString(),
                            SLastName = Reader["SLastName"].ToString(),
                            Nick = Reader["Nick"].ToString(),
                            Password = Reader["Password"].ToString(),
                            BirthDate = DateTime.Parse(Reader["BirthDate"].ToString())
                        };
                        ListUsers.Add(Person);
                    }
                    //Se indica que se cierre la tabla
                    Reader.Close();
                    return ListUsers;
                }
                else
                {
                    throw new Exception("Error 430 Contacte a los desarrolladores");
                }

            }
            catch (SqlException SqlExce)
            {
                throw new Exception(SqlExce.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                SqlConection.Desconectar();
                SqlConection = null;
                Reader = null;
            }
        }
        [HttpGet]
        public IHttpActionResult GetUserId(int Id)
        {
            ListUsers = GetAllUsers().ToList();
            var Product = ListUsers.FirstOrDefault((u) => u.Id == Id);
            if (Product == null) return NotFound();
            return Ok(Product);
        }
        [HttpPost]
        public void InsertUser([FromBody] User Person)
        {
            SqlConection = new Connection();
            Parametros = new List<SqlParameter>();
            try
            {
                //Se abre conexion
                SqlConection.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                Parametros.Add(new SqlParameter ("@Name",Person.Name));
                Parametros.Add(new SqlParameter("@FLastName",Person.FLastName));
                Parametros.Add(new SqlParameter("@SLastName",Person.SLastName));
                Parametros.Add(new SqlParameter("@Nick",Person.Nick));
                Parametros.Add(new SqlParameter("@Password",Person.Password));
                Parametros.Add(new SqlParameter("@BirthDate",Person.BirthDate.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                SqlConection.PrepararProcedimiento("sp_InsertUser", Parametros);
                SqlConection.EjecutarProcedimiento();
            }
            catch (SqlException SqlExce)
            {
                throw new Exception(SqlExce.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                SqlConection.Desconectar();
                SqlConection = null;
                Reader = null;
            }
        }
        [HttpPut]
        public void UpdateUser([FromBody] User Person)
        {
            SqlConection = new Connection();
            Parametros = new List<SqlParameter>();
            try
            {
                //Se abre conexion
                SqlConection.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                if(Person.Name != null) Parametros.Add(new SqlParameter("@Name",Person.Name));
                if(Person.FLastName !=null) Parametros.Add(new SqlParameter("@FLastName",Person.FLastName));
                if(Person.SLastName != null) Parametros.Add(new SqlParameter("@SLastName",Person.SLastName));
                if(Person.Nick != null) Parametros.Add(new SqlParameter("@Nick",Person.Nick));
                if(Person.Password != null) Parametros.Add(new SqlParameter("@Password",Person.Password));
                if(Person.BirthDate != DateTime.MinValue) Parametros.Add(new SqlParameter("@BirthDate", Person.BirthDate.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                Parametros.Add(new SqlParameter("@Id", Person.Id));
                SqlConection.PrepararProcedimiento("sp_UpdatetUser", Parametros);
                SqlConection.EjecutarProcedimiento();
            }
            catch (SqlException SqlExce)
            {
                throw new Exception(SqlExce.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                SqlConection.Desconectar();
                SqlConection = null;
                Reader = null;
            }
        }
        [HttpDelete]
        public void DeleteUser(int Id)
        {
            SqlConection = new Connection();
            Parametros = new List<SqlParameter>();
            try
            {
                //Se abre conexion
                SqlConection.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                Parametros.Add(new SqlParameter("@Id",Id));
                SqlConection.PrepararProcedimiento("sp_DeleteUser", Parametros);
                SqlConection.EjecutarProcedimiento();
            }
            catch (SqlException SqlExce)
            {
                throw new Exception(SqlExce.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                SqlConection.Desconectar();
                SqlConection = null;
                Reader = null;
            }
        }
    }
}
