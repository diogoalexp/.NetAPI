using SampleModel.DTO;
using SampleModel.Entities;
using SampleModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SampleModel
{
    public static class Extensions
    {
        private static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem, Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        private static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem)
            where TSource : class
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }

        public static string GetaAllMessages(this Exception exception)
        {
            var messages = String.Join(Environment.NewLine, exception.FromHierarchy(ex => ex.InnerException).Select(ex => ex.Message));
            return messages;
        }

        public static string ToLogError(this ILogger logger, Exception exception, Object parametro = null)
        {
            logger.LogError(exception, "Dados informados foram: {Parametros}", JsonConvert.SerializeObject(parametro));

            return exception.GetaAllMessages();
        }

        public static PersonDTO asDto(this Person person)
        {
            return new PersonDTO
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
            };
        }


    }
}
