using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Collections.Generic;

namespace PracticaOperadores
{
    //para null coalescing operator
    public class Usuario
    {
        private string apellido;
        public string Nombre { get; set; }
        public string Apellido {
            get => apellido;
            set => apellido = value ?? throw new ArgumentNullException(nameof(value), "El apellido no puede ser nulo");
        }
    }
    public class UsuarioPagado: Usuario
    {
        public int?[] ItemsGuardados { get; set; }
    }
    //para as/is
    public class Admin : Usuario
    {
        public int?[] permisos { get; set; }
    }
    public class Proveedor
    {
        public int Agencia { get; set; }
        public int id { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            #region Ternary Operator
            //condicion ? expresion si se cumple : expresion si no se cumple
            Console.WriteLine("Ingresa tu edad");
            var edad = Convert.ToInt32(Console.ReadLine());
            bool autorizado;
            /* IF COMUN
             * if(edad >= 18)
             {
                 autorizado = true;
             }
             else
             {
                 autorizado = false;
             }*/

            /* CON OP. TERNARIO 1*/
            //edad >= 18 ? true : false;
            autorizado = edad >= 18 ? true : false; //el resultado puede ser asignado a una variable
            /* ./ DESPUES */

            /* MAS REDUCIDO */
            autorizado = edad >= 18;
            /* MAS REDUCIDO */

            //Console.WriteLine("Autorizado: ", autorizado);

            // Ej 2
            string mensaje = autorizado ? "Usuario autorizado" : "Usuario no autorizado";
            Console.WriteLine(mensaje);
            #endregion

            #region Nested Ternary Operator  (anidado)
            //Ej 3
            string mensajeB = edad >= 18 ? "Usuario autorizado" :
                                edad >= 12 ? "Usuario con restricción parental" : "Usuario no autorizado";
            Console.WriteLine(mensajeB);

            //Ej 4 Se pueden realizar operaciones dentro de la condición
            Console.WriteLine("Ingresa un entero");
            var num = Convert.ToInt32(Console.ReadLine());
            int op = 2; //1 seno, 2 coseno
            double res = op == 1 ? Math.Sin(num) : Math.Cos(num);
            Console.WriteLine("Operación {0} de {1} = {2}", op == 1 ? "Sin" : "Cos", num.ToString(), res.ToString());
            #endregion

            #region Comparaciones forma corta
            /*
            bool requierePermisoParental = false;
            if (edad <= 18)
                requierePermisoParental = true;
            */
            bool usuarioGratuito = true;
            //Ej 1
            bool requierePermisoParental = edad <= 18;
            //Ej 2
            var mostrarPublicidad = usuarioGratuito && !requierePermisoParental;
            Console.WriteLine("mostrarPublicidad? {0}", mostrarPublicidad);
            #endregion

            #region Null-Coalescing operator
            //Ej 1
            int? x = null;
            int nuevo = x ?? 0;
            //Ej 2
            int?[] nums = new int?[] { 1, null, null };
            int suma = nums.Sum() ?? 0; //requires System.Linq
            Console.WriteLine("Suma = {0}", suma);
            //Ej 3
            Console.WriteLine("Ingresa tu nombre");
            var nombre = Console.ReadLine();
            //Ej 3-1 
            var usuario = new Usuario { Nombre = nombre };
            //Ej 3-2 var usuariob = new Usuario { Nombre = null, Apellido = "león" };
            //Ej 3-3 var usuarioc = new Usuario { Apellido = null };
            #endregion

            #region Null Conditional Operator ?.
            //var rand = new Random();
            //int? item = nums[rand.Next(nums.Length-1)];
            int? item = nums[1];
            Console.WriteLine("item: {0}", item);
            //ej 1 sin operador
            string textoA;
            if (item == null)
                textoA = "No se encuentra num";
            else
                textoA = item.ToString();
            Console.WriteLine("texto A: {0}", textoA);
            //ej 2  con operador
            var textoB = item?.ToString() ?? "item nulo";
            Console.WriteLine("texto B: {0}", textoB);
            #endregion

            #region Operador is y as
            var nuevoUsuario = new UsuarioPagado { Nombre = nombre, ItemsGuardados = nums };
            if (nuevoUsuario is Usuario)
            {
                Console.WriteLine("es Usuario");
                if (nuevoUsuario is UsuarioPagado)
                    Console.WriteLine("es Usuario Pagado");
                if (nuevoUsuario is Admin)
                    Console.WriteLine("es Usuario Admin");
            }
            //Ej. AS
            object[] usuarios = new object[4] {usuario, nuevoUsuario, null, new Proveedor { Agencia = 555} };
            
            for(int i= 0; i< usuarios.Length; i++)
            {
                var us = usuarios[i] as Usuario;
                if (us != null) //validamos que sea casteable
                    Console.WriteLine("{0} Es de tipo Usuario", us);
                else
                    Console.WriteLine("{0} No es de tipo usuario", usuarios[i]);
            }
            #endregion
        }
    }
}