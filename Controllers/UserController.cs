using MiWebObj.App_Start;
using MiWebObj.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MiWebObj.Controllers
{
    public class UserController: ApiController
    {
        List<User> ListUsers;
        Connection SqlConection;
        DataTableReader Reader;
        User Person = new User();
        List<SqlParameter> Parametros;
        public IEnumerable<User> GetAllUsers() {
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
                    while(Reader.Read()){ 
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
                    throw new Exception("Error 'Data Base, Rows' Contacte a los desarrolladores");
                }

            }
            catch (SqlException SqlExce)
            {
                throw new Exception(SqlExce.ToString());
            }
            catch (Exception Exce)
            {
                throw new Exception(Exce.ToString());
            }
            finally
            {
                SqlConection.Desconectar();
                SqlConection = null;
                Reader = null;
            }
        }
        public IHttpActionResult GetUserId(int Id)
        {
            ListUsers = GetAllUsers().ToList();
            var Product = ListUsers.FirstOrDefault((u) => u.Id == Id);
            if (Product == null) return NotFound();
            return Ok(Product);
        }

    }
}