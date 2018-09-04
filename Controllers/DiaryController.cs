using MiWebObj.App_Start;
using MiWebObj.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MiWebObj.Controllers
{
    public class DiaryController : ApiController
    {
        Connection SqlConnection;
        List<Diary> Diaries;
        List<SqlParameter> Parameters;
        DataTableReader Reader;
        Diary _Diary;
        User _Person;
        // GET: api/Diary
        [HttpGet]
        public IEnumerable<Diary> GetAllDiaries()
        {
            Diaries = new List<Diary>();
            SqlConnection = new Connection();
            Parameters = new List<SqlParameter>();
            Reader = null;
            try
            {
                SqlConnection.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                SqlConnection.PrepararProcedimiento("sp_AllDiaries",Parameters);
                Reader = SqlConnection.EjecutarTableReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        _Person = new User()
                        {
                            Id = int.Parse(Reader["User_Id"].ToString())
                        };
                        _Diary = new Diary()
                        {
                            Id = int.Parse(Reader["Id"].ToString()),
                            Description = Reader["Description"].ToString(),
                            Date = DateTime.Parse(Reader["Date"].ToString()),
                            Person = _Person
                        };
                        Diaries.Add(_Diary);
                    }
                }
                else
                {
                    throw new Exception("Data Base Error");
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
                SqlConnection.Desconectar();
                SqlConnection = null;
                Reader = null;
            }

            return Diaries;
        }
        [HttpGet]
        // GET: api/Diary/5
        public IHttpActionResult GetDiary(int Id)
        {
            Diaries = GetAllDiaries().ToList();
            var diary = Diaries.FirstOrDefault((u) => u.Id == Id);
            if (diary == null) return NotFound();
            return Ok(diary);
        }
        [HttpPost]
        // POST: api/Diary
        public void InsertDiary([FromBody]Diary NewDiary)
        {
            SqlConnection = new Connection();
            Parameters = new List<SqlParameter>();
            try
            {
                SqlConnection.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                Parameters.Add(new SqlParameter("@Description", NewDiary.Description));
                if(NewDiary.Date != null)Parameters.Add(new SqlParameter("@Date", NewDiary.Date.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                if (NewDiary.Person.Id != 0) Parameters.Add(new SqlParameter("@IdUser", NewDiary.Person.Id));
                SqlConnection.PrepararProcedimiento("sp_InsertDiary", Parameters);
                SqlConnection.EjecutarProcedimiento();
            }
            catch (SqlException SqlExce)
            {
                throw new Exception(SqlExce.ToString());
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
            finally
            {
                SqlConnection.Desconectar();
                SqlConnection = null;
                Parameters = null;
            }

        }
        [HttpPut]
        // PUT: api/Diary/5
        public void UpdateDiary([FromBody]Diary DiaryEdited)
        {
            SqlConnection = new Connection();
            Parameters = new List<SqlParameter>();
            try
            {
                SqlConnection.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                if(DiaryEdited.Description != null) Parameters.Add(new SqlParameter("@Description",DiaryEdited.Description));
                if(DiaryEdited.Date != DateTime.MinValue)Parameters.Add(new SqlParameter("@Date", DiaryEdited.Date.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                Parameters.Add(new SqlParameter("@Id", DiaryEdited.Id));
                SqlConnection.PrepararProcedimiento("sp_UpDiary",Parameters);
                SqlConnection.EjecutarProcedimiento();
            }
            catch (SqlException SqlExce)
            {
                throw new Exception(SqlExce.ToString());
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
            finally
            {
                SqlConnection.Desconectar();
                SqlConnection = null;
                Parameters = null;
            }
        }
        [HttpDelete]
        // DELETE: api/Diary/5
        public void DeleteDiary(int Id)
        {
            SqlConnection = new Connection();
            Parameters = new List<SqlParameter>();
            try
            {
                SqlConnection.Conectar(System.Configuration.ConfigurationManager.ConnectionStrings["MiBD"].ToString());
                Parameters.Add(new SqlParameter("@Id", Id));
                SqlConnection.PrepararProcedimiento("sp_DeleteDiary", Parameters);
                SqlConnection.EjecutarProcedimiento();
            }
            catch (SqlException SqlExce)
            {
                throw new Exception(SqlExce.ToString());
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.ToString());
            }
            finally
            {
                SqlConnection.Desconectar();
                SqlConnection = null;
                Parameters = null;
            }
        }
    }
}
