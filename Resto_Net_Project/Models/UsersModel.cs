using Resto_Net_Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Resto_Net_Project.Models.UsersModel;

namespace Resto_Net_Project.Models
{
    public abstract class UsersModel : IIdentifiable
    {
        private int id;
        private string dni;
        private string nombre;
        private string email;
        private string telefono;
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (value != null && EsNombreValido(value))
                {
                    nombre = value;
                }
                else
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException("El campo Nombre no puede estar vacío");
                    }
                }
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (value != null && EsEmailValido(value))
                {
                    email = value;
                }
                else
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException("El campo Email no puede estar vacío");
                    }
                }
            }
        }

        public string Telefono
        {
            get { return telefono; }
            set
            {
                if (EsTelefonoValido(value))
                {
                    telefono = value;
                }
                else
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException("El campo Teléfono no puede estar vacío");
                    }
                }
            }
        }

        public int Id
        {
            get { return id; }
            set
            {
                if (this.id == 0)
                {
                    id = value;
                }
            }
        }

        public string Dni
        {
            get { return dni; }
            set
            {
                if (EsDniValido(value))
                {
                    dni = value;
                }
                else
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException("El campo DNI no puede estar vacío");
                    }
                }
            }
        }


        protected UsersModel(string nombre, string dni, string email, string telefono)
        {
            this.Nombre = nombre;
            this.Dni = dni;
            this.Email = email;
            this.Telefono = telefono;
        }
        private bool EsNombreValido(string nombre)
        {
            Regex regex = new Regex(@"[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$");
            return regex.IsMatch(nombre);
        }

        private bool EsEmailValido(string email)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$");
            return regex.IsMatch(email);
        }

        private bool EsTelefonoValido(string telefono)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(telefono);
        }

        private bool EsDniValido(string dni)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(dni);
        }
        public override string ToString()
        {
            return (Nombre+" - "+ Dni);
        }

    }

    public class ManagerModel : UsersModel, IIdentifiable
    {
        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                if (value != null)
                {
                    if (EsPswdValida(value))
                    {
                        password = value;
                    }
                    else
                    {
                        Console.WriteLine("La contraseña no cumple con los requisitos.");
                    }
                }
                else
                {
                    throw new ArgumentNullException("El campo Contraseña no puede estar vacío");
                }
            }
        }


        public ManagerModel(string nombre, string dni, string email, string telefono, string password)
            : base(nombre, dni, email, telefono)
        {
            this.Password = password;
        }

        

        private bool EsPswdValida(string pswd)
        {
            if (pswd.Length < 8)
            {
                return false;
            }

            bool tiene_mayuscula = false;
            bool tiene_minuscula = false;
            bool tiene_numero = false;
            bool tiene_simbolo = false;

            foreach (char c in pswd)
            {
                if (char.IsDigit(c))
                {
                    tiene_numero = true;
                }
                else if (char.IsUpper(c))
                {
                    tiene_mayuscula = true;
                }
                else if (char.IsLower(c))
                {
                    tiene_minuscula = true;
                }
                else if (!char.IsLetterOrDigit(c))
                {
                    tiene_simbolo = true;
                }
            }

            if (!tiene_mayuscula || !tiene_minuscula || !tiene_numero || !tiene_simbolo)
            {
                return false;
            }

            return true;
        }
    }

    public class MeseroModel : UsersModel, IIdentifiable
    {
        public MeseroModel(string nombre, string dni, string email, string telefono) : base(nombre, dni, email, telefono) { }

    }
}

