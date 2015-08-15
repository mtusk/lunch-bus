using System.Linq;
using System.Text;

namespace LunchBus.Storage
{
    public static class StringExtensions
    {
        public static string ToAzureKeyString(this string input)
        {
            var stringBuilder = new StringBuilder();
            var invalidCharacters = input.Where(c =>
                c != '/' &&
                c != '\\' &&
                c != '#' &&
                c != '/' &&
                c != '?' &&
                !char.IsControl(c));

            foreach (var validCharacter in invalidCharacters)
            {
                stringBuilder.Append(validCharacter);
            }

            return stringBuilder.ToString();
        }
    }
}
