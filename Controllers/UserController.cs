using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Sample.Contexts;

namespace Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDBContext context;
        private readonly ILogger<UserController> _logger;
        private readonly string SQLitePath;
        private readonly SQLiteConnection connection;

        public UserController(AppDBContext context, ILogger<UserController> logger)
        {
            this.context = context;
            _logger = logger;
            SQLitePath = "Data Source=.\\Data\\Neta.db";
            connection = new SQLiteConnection(SQLitePath);
        }

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            var res=new List<Usuario>();
            using (connection)
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Usuario", connection))
                    {
                    IDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        res.Add(new Usuario(
                                    Convert.ToInt32(dr.GetValue(0)),
                                    dr.GetValue(1).ToString(),
                                    dr.GetValue(2).ToString(),
                                    dr.GetValue(3).ToString(),
                                     Convert.ToDateTime(dr.GetValue(4))
                                    ));
                    }
                }
            }
            return res;
        }

       
        [HttpGet("{id}")]
        public Usuario Get(int id)
        {
            var res = new Usuario();
            using (connection)
            {
                connection.Open();
                using (var command = new SQLiteCommand($"SELECT * FROM Usuario WHERE idPersona={id}", connection))
                {
                    IDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        res=new Usuario(
                                    Convert.ToInt32(dr.GetValue(0)),
                                    dr.GetValue(1).ToString(),
                                    dr.GetValue(2).ToString(),
                                    dr.GetValue(3).ToString(),
                                     Convert.ToDateTime(dr.GetValue(4))
                                    );
                    }
                }
            }
            return res;
        }

        [HttpPost]
        public void Post([FromBody] Usuario value)
        {
            using (connection)
            {
                connection.Open();
                using (var command = new SQLiteCommand($"INSERT INTO Usuario(" +
                                                       $"NombreCompleto,Telefono,Email,FechaNacimiento) " +
                                                       $"VALUES('" + value.NombreCompleto + "'," +
                                                       $"'" + value.Telefono + "'," +
                                                       $"'" + value.Email + "'," +
                                                       $"'" + value.FechaNacimiento + "'" +
                                                       $")", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        [HttpPut("{id}")]
        public void Put([FromBody]Usuario value)
        {
            using (connection)
            {
                connection.Open();
                using (var command = new SQLiteCommand($"UPDATE Usuario SET " +
                                                       "NombreCompleto='" + value.NombreCompleto + "'," +
                                                       "Telefono='" + value.Telefono + "'," +
                                                       "Email='" + value.Email + "'," +
                                                       "FechaNacimiento='" + value.FechaNacimiento + "'" +
                                                       $"WHERE IdPersona={value.idPersona}", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (connection)
            {
                connection.Open();
                using (var command = new SQLiteCommand($"DELETE FROM Usuario WHERE idPersona={id}", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
