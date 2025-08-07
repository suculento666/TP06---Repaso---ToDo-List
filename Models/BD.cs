using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;

public static class BD
{
    private static string _connectionString = @"Server=localhost;DataBase=TP06;Integrated Security=True;TrustServerCertificate=True;";

    // USUARIOS

    public static Usuario BuscarUsuario(string username, string password)
    {
        Usuario usuario = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE Username = @Username AND Password = @Password";
            usuario = connection.QueryFirstOrDefault<Usuario>(query, new { Username = username, Password = password });
        }
        return usuario;
    }

    public static bool ExisteUsername(string username)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "SELECT * FROM Usuarios WHERE Username = @Username";
        Usuario usuario = connection.QueryFirstOrDefault<Usuario>(query, new { Username = username });
        return usuario != null;
    }
}
    

 public static void AgregarUsuario(Usuario usuario)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"INSERT INTO Usuarios 
                        (Nombre, Apellido, Foto, Username, UltimoLogin, Password)
                         VALUES
                        (@Nombre, @Apellido, @Foto, @Username, @UltimoLogin, @Password)";

        connection.Execute(query, new
        {
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Foto = usuario.Foto,
            Username = usuario.Username,
            UltimoLogin = usuario.UltimoLogin,
            Password = usuario.Password
        });
    }
}

    public static void ActualizarUltimoLogin(int idUsuario)
    {
        string query = "UPDATE Usuarios SET UltimoLogin = GETDATE() WHERE Id = @Id";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { Id = idUsuario });
        }
    }

    public static Usuario TraerUsuarioPorId(int idUsuario)
    {
        Usuario usuario = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE Id = @Id";
            usuario = connection.QueryFirstOrDefault<Usuario>(query, new { Id = idUsuario });
        }
        return usuario;
    }

    // TAREAS (NOMBRES IGUALES A TU CONTROLLER)

    public static List<Tarea> DevolverTareas(int idUsuario)
    {
        List<Tarea> tareas = new List<Tarea>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE IdUsuario = @IdUsuario AND Eliminada = 0";
            tareas = connection.Query<Tarea>(query, new { IdUsuario = idUsuario }).AsList();
        }
        return tareas;
    }

    public static Tarea DevolverTarea(int idTarea)
    {
        Tarea tarea = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE Id = @Id";
            tarea = connection.QueryFirstOrDefault<Tarea>(query, new { Id = idTarea });
        }
        return tarea;
    }

    public static void CrearTarea(Tarea tarea)
    {
        string query = @"INSERT INTO Tareas (Titulo, Descripcion, Fecha, Finalizada, IdUsuario, Eliminada, FechaCreacion)
                         VALUES (@Titulo, @Descripcion, @Fecha, @Finalizada, @IdUsuario, 0, GETDATE())";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new
            {
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                Fecha = tarea.Fecha,
                Finalizada = tarea.Finalizada,
                IdUsuario = tarea.IdUsuario
            });
        }
    }

    public static void EditarTarea(Tarea tarea)
    {
        string query = @"UPDATE Tareas SET Titulo = @Titulo, Descripcion = @Descripcion, Fecha = @Fecha, 
                         Finalizada = @Finalizada, FechaModificacion = GETDATE()
                         WHERE Id = @Id";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new
            {
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                Fecha = tarea.Fecha,
                Finalizada = tarea.Finalizada,
                Id = tarea.Id
            });
        }
    }

    public static void EliminarTarea(int idTarea)
    {
        // Borrado lógico
        string query = @"UPDATE Tareas SET Eliminada = 1, FechaEliminacion = GETDATE() WHERE Id = @Id";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { Id = idTarea });
        }
    }

    public static void FinalizarTarea(int idTarea)
    {
        string query = "UPDATE Tareas SET Finalizada = 1 WHERE Id = @Id";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { Id = idTarea });
        }
    }
}