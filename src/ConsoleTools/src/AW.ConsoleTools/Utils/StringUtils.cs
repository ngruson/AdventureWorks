using System.Globalization;
using System.Text;

namespace AW.ConsoleTools.Utils
{
    public static class StringUtils
    {
        public static string NormalizeExt(this string value)
        {
            var sb = new StringBuilder();
            var arrayText = value.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sb.Append(letter);
            }
            return sb.ToString();
        }
    }
}