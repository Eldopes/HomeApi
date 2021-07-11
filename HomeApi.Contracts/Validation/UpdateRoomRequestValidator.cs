using System.Linq;
using FluentValidation;
using HomeApi.Contracts.Models.Rooms;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор запросов обновления rjvyfns
    /// </summary>
    public class UpdateRoomRequestValidator : AbstractValidator<UpdateRoomRequest>
    {
        /// <summary>
        /// Метод, конструктор, устанавливающий правила
        /// </summary>
        public UpdateRoomRequestValidator() 
        {
            RuleFor(x => x.Voltage).NotEmpty().Must(BeIn)
                .WithMessage($"Currencies supported: {string.Join(", ", Values.ValidCurrencies)}");
            RuleFor(x => x.Name).NotEmpty().Must(BeSupported)
                .WithMessage($"Please choose one of the following locations: {string.Join(", ", Values.ValidRooms)}");
        }
        
        /// <summary>
        ///  Метод кастомной валидации для свойства name
        /// </summary>
        private bool BeSupported(string location)
        {
            return Values.ValidRooms.Any(e => e == location);
        }
        
        /// <summary>
        ///  Метод кастомной валидации для свойства Voltage
        /// </summary>
        private bool BeIn(int current)
        {
            return current is 120 or 220;
        }
    }
}