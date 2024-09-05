namespace FeedEveryone.Core.Service.Randomizing;

public interface IRandomProvider
{
    /// <summary>
    /// Возвращает случайное число в диапазоне [0, 1).
    /// </summary>
    float GetFloat();
    /// <summary>
    /// Возвращает случайное число в диапазоне [min, max).
    /// </summary>
    float GetFloat(float min, float max);
}
