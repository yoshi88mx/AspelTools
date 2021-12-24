using AspelTools.Client.Pages.Traspasos;
using FluentValidation;

namespace AspelTools.Client.Validators
{
    public class TraspasoPorLineaValidator: AbstractValidator<TraspasoPorLineaModelo>
    {
        public TraspasoPorLineaValidator()
        {
            RuleFor(t => t.AlmacenOrigen).NotEmpty().WithMessage("Seleccione un almacen");

            RuleFor(t => t.AlmacenDestino).NotEmpty().WithMessage("Seleccione un almacen");

            RuleFor(t => t.Linea).NotEmpty().WithMessage("Debe ingresar una linea");

            RuleFor(t => t.Linea).Length(5).WithMessage("El tamaño total de la linea debe ser de 5 caracteres");

            RuleFor(t => t.Linea).NotEqual("%%%%%").WithMessage("Debe agregar un criterio a la linea");
            
            RuleFor(t => t.Referencia).NotEmpty().WithMessage("Debe ingresar una referencia");
            RuleFor(t => t.Referencia).MaximumLength(20).WithMessage("No debe superar los 20 caracteres");
        }
    }
}
