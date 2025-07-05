namespace Brony.Extensions;

public static class Convertor
{
    public static List<T> ToObject<T>(this string text)
    {
        return Enumerable.Range(0, text.Length)
            .Select(i => text[i])
            .Select(c => (T)Convert.ChangeType(c, typeof(T)))
            .ToList();
    }
}
