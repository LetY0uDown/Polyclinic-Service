using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace API_Host.Tools.Extensions;

internal static class WebApplicationExtensions
{
    /// <summary>
    /// Добавляет в API глобальный обработчик ошибок. Он, кажется, не работает так, как я хочу, чтобы он работал, но он работает, и это хорошо
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    internal static IApplicationBuilder AddExceptionHandler (this IApplicationBuilder app, ILogger<Program> logger)
    {
        app.UseExceptionHandler(appError => {
            appError.Run(async context => {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                // Получаем из HTTP контекста доступ к данным об ошибке
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature is null)
                    return;

                // Куда-нибудь записываем
                logger.Log(LogLevel.Error, contextFeature.Error.Message);

                // Пишем клиенту сообщение об ошибке с HTTP кодом и непосредственно текстом ошибки
                await context.Response.WriteAsync(new ErrorDetails(
                    StatusCode: context.Response.StatusCode,
                    Message: $"Произошла ошибка! D: {contextFeature.Error.Message}"
                ));
            });
        });

        return app;
    }
}