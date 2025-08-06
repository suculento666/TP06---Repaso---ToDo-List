public class Tarea
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime Fecha { get; set; }
    public bool Finalizada { get; set; }
    public int IdUsuario { get; set; }

 
    public bool Eliminada { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    public DateTime FechaEliminacion { get; set; }

    public Tarea() { }
}