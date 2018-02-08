// ReSharper disable once CheckNamespace
namespace System
{
    /// <summary>
    /// System.String Extension
    /// </summary>
    public static partial class StringExtension
    {
#if NET35
        /// <summary>
        /// 指示指定的字符串是 <see langword="null" />、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="value">要测试的字符串。</param>
        /// <returns>
        /// 如果 <see langword="true" /> 参数为 <paramref name="value" /> 或 <see langword="null" />，或者如果 <see cref="F:System.String.Empty" /> 仅由空白字符组成，则为 <paramref name="value" />。
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null)
                return true;
            for (int index = 0; index < value.Length; ++index)
            {
                if (!char.IsWhiteSpace(value[index]))
                    return false;
            }
            return true;
        }

#else

        /// <summary>
        /// 指示指定的字符串是 <see langword="null" />、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="value">要测试的字符串。</param>
        /// <returns>
        /// 如果 <see langword="true" /> 参数为 <paramref name="value" /> 或 <see langword="null" />，或者如果 <see cref="F:System.String.Empty" /> 仅由空白字符组成，则为 <paramref name="value" />。
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

#endif

    }
}
