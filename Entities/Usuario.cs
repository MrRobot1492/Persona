using System;
using System.ComponentModel.DataAnnotations;

namespace Sample
{
    public class Usuario
    {
        [Key]
        public int idPersona { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Usuario(int id, string Nombre, string Tel, string mail, DateTime Fecha)
        {
            this.idPersona = id;
            this.NombreCompleto = Nombre;
            this.Telefono = Tel;
            this.Email = mail;
            this.FechaNacimiento = Fecha;
        }
        public Usuario()
        {

        }
    }
}
