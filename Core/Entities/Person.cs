namespace Core.Entities
{
    public class Person
    {
        public int IdPersonaFisica { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RFC { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public int UsuarioAgrega { get; set; }
        public Boolean Activo { get; set; }
    }
}
