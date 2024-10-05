using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises
{
    class ErrorHandler
    {
        #region Properties
        private static List<User> UserList { get; set; } = GetUsers();
        #endregion

        #region Variables
        private const int MIN_AGE = 10;
        private const int MAX_AGE = 100;
        #endregion

        #region Classes
        public class User
        {
            #region Properties
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            #endregion

            #region Constructors
            public User() { }

            public User(int userId, string userName, string password)
            {
                UserId = userId;
                Username = userName;
                Password = password;
            }

            public User(string userName, string password)
            {
                Username = userName;
                Password = password;
            } 
            #endregion
        }
        #endregion

        public class CustomAppException : Exception
        {

            private eErrorTipo ErrorResponseEx { get; set; } = eErrorTipo.Ninguno;

            public CustomAppException() : base()
            {
            }

            public CustomAppException(string massage, eErrorTipo tipo) : base(massage)
            {
                ErrorResponseEx = tipo;
            }
        }
        public enum eErrorTipo
        {
            Ninguno=0,Validacion,Conexion,InformacionDuplicada,Autenticacion,Desconectado=99
        }

        private static List<User> GetUsers()
        {
            return new()
            {
                new(1, "admin", "admin"),
                new(2, "sa", "12345"),
                new(3, "johndoe", "contrasena"),
                new(4, "miguel94", "19940707")
            };
        }

        public static void Main(string[] args)
        {
            const string username = "Tamaris";
            const string password = "admin";



            Console.ReadKey();
        }

        public static int RegisterUserWithoutValidations(string username, string password, string ageInput)
        {
            int userId,
                age;
            Console.WriteLine("Conexión a la base de datos");
            Console.WriteLine("Abrimos transacción");

            age = Convert.ToInt32(ageInput);

            Console.WriteLine("Ejecutamos acciones en la base de datos");

            if (!IsExistingUser(username))
                InsertUser(new(username, password));

            return 0;
        }

        public static bool IsExistingUser(string username)
        {
            return UserList != null && UserList.Any(user => user.Username == username);
            //return (UserList?.Any(user => user.Username == username)).GetValueOrDefault();
        }

        public static bool InsertUser(User user)
        {
            user.UserId = UserList != null ? (UserList.Max(user => user.UserId) + 1) : 1;
            
            if(UserList == null)
                UserList = new();

            UserList.Add(user);

            #region Comment
            //if (UserList != null)
            //    UserList.Add(user);
            //else
            //{
            //    UserList = new();
            //    UserList.Add(user);
            //} 
            #endregion

            Console.WriteLine("Acción ejecutada en base de datos => Usuario insertado correctamente");
            return true;
        }
    }
    public static void RegisterUserWithoutValidations(string username, string password, string ageInput)
    {
        Console.WriteLine("iniciamos el proceso");
        try
        {
Console.WriteLine("Abrimos la transaccion");
        }
        catch (Exception e)
        {
            Console.WriteLine("Rollback");
            throw;
        }
        
    }

    private static int validez(string ageInput)
    {
        if (!int.TryParse(ageInput, out int age))
            throw new ErrorHandler.CustomAppException("la edad viene en formato correcto",
                ErrorHandler.eErrorTipo.Validacion);
        
        if(age<MIN_AGE||age>MAX_AGE)
            throw new ErrorHandler.CustomAppException("la edad viene en formato correcto",
                ErrorHandler.eErrorTipo.Validacion);

    }
}