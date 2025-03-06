using System.ComponentModel.DataAnnotations;

namespace ApplicationToka.ViewModel
{
    public class PersonViewModel
    {
        public int? IdPersonaFisica { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El apellido paterno no puede tener más de 50 caracteres")]
        public string ApellidoPaterno { get; set; }

        [StringLength(50, ErrorMessage = "El apellido materno no puede tener más de 50 caracteres")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "El RFC es obligatorio")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "El RFC debe tener exactamente 13 caracteres")]
        [RegularExpression("^[A-Z&Ñ]{4}[0-9]{6}[A-Z0-9]{3}$", ErrorMessage = "El RFC no tiene un formato válido")]
        public string RFC { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento no es válida")]
        public DateOnly FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El valor de estatus es obligatoria")]
        public bool Activo { get; set; }

    }

}
