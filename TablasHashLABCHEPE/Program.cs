using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

struct Usuario
{
    public string Nombre;
    public string Correo;
    public string Contrasena;

    public Usuario(string nombre, string correo, string contrasena)
    {
        Nombre = nombre;
        Correo = correo;
        Contrasena = EncriptarContrasena(contrasena);
    }

    private static string EncriptarContrasena(string contrasena)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
            StringBuilder hashBuilder = new StringBuilder();

            foreach (byte b in data)
            {
                hashBuilder.Append(b.ToString("x2")); // Convertir cada byte a su representación hexadecimal
            }

            return hashBuilder.ToString();
        }
    }
}

class Program
{
    static Dictionary<string, Usuario> usuarios = new Dictionary<string, Usuario>();

    static void IngresarNuevoUsuario()
    {
        Console.WriteLine("Ingrese el nombre del usuario:");
        string nombre = Console.ReadLine();

        Console.WriteLine("Ingrese el correo del usuario:");
        string correo = Console.ReadLine();

        Console.WriteLine("Ingrese la contraseña del usuario:");
        string contrasena = Console.ReadLine();

        usuarios.Add(correo, new Usuario(nombre, correo, contrasena));
        Console.WriteLine("Usuario agregado exitosamente.");
    }

    static void BuscarPorCorreo(string correo)
    {
        if (usuarios.ContainsKey(correo))
        {
            Usuario usuarioEncontrado = usuarios[correo];
            Console.WriteLine($"Usuario encontrado:\nNombre: {usuarioEncontrado.Nombre}\nCorreo: {usuarioEncontrado.Correo}\nContraseña: {usuarioEncontrado.Contrasena}");
        }
        else
        {
            Console.WriteLine("Usuario no encontrado.");
        }
    }

    static void Main(string[] args)
    {
        usuarios.Add("Juan@example.com", new Usuario("Juan", "Juan@example.com", "password123"));
        usuarios.Add("Maria@example.com", new Usuario("Maria", "Maria@example.com", "hello456"));
        usuarios.Add("Pedro@example.com", new Usuario("Pedro", "Pedro@example.com", "abc789"));

        Console.WriteLine("Bienvenido al sistema de usuarios");

        int opcion = 0;
        while (opcion != 3)
        {
            Console.WriteLine("Ingrese la opcion deseada:");
            Console.WriteLine("1. Agregar nuevo usuario");
            Console.WriteLine("2. Buscar por correo");
            Console.WriteLine("3. Salir");

            try
            {
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        IngresarNuevoUsuario();
                        break;
                    case 2:
                        Console.WriteLine("Ingrese el correo del usuario a buscar:");
                        string correo = Console.ReadLine();
                        BuscarPorCorreo(correo);
                        break;
                    case 3:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida, intente de nuevo.");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
