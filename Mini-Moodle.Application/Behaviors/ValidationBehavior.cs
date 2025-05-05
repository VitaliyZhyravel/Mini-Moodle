using FluentValidation;
using MediatR;
using Mini_Moodle.Repositories.AccountService.Command.Login;

namespace Mini_Moodle.Behaviors;

//ValidationBehavior це клас який додає поведінку для MediatR запитів перед їх Handler обробкою.
//Тобто MediatR Використовує патерн Pipline в який можна додавати різні поведінки (MiddleWare)
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    //Додаємо через DI всі валідатори нашого конкретного TRequest які реалізують AbstractValidator<TRequest>
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    //Логіка валідації запиту
    public async Task<TResponse> Handle(
        TRequest request,//Наш запит. Наприклад AccountLoginCommand
        RequestHandlerDelegate<TResponse> next, // Наступна поведінка або Handler обробник
        CancellationToken cancellationToken)
    {
        if (_validators.Any())//якщо в нас є валідатори для цього запиту
        {
            //ValidationContext<TRequest> - це контекст валідації, який містить інформацію про запит
            //Сюди і передається наш запит тобто AccountLoginCommand
            var context = new ValidationContext<TRequest>(request);


            //Потім ми запускаємо валідацію асинхронно для всіх валідаторів
            var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            //Тут ми обираємо всі помилки валідації з усіх валідаторів(За допомогою SelectMany робимо загальний список)
            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
                throw new ValidationException(failures);
        }
        return await next();
    }


}
