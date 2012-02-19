using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Core.Translation
{
    /// <summary>
    /// Provides some useful string extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Transforms keys in for UPPER_CASE_KEY to a pascal case 
        /// eye friendly UpperCaseKey form.
        /// </summary>
        /// <param name="upperCaseKey">Key with upper cases and underscores (for example UPPER_CASE_KEY)</param>
        public static string TransformUpperCaseKey(this string upperCaseKey)
        {
            if (upperCaseKey == null)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            bool upperLetter = true;
            for (int i = 0; i < upperCaseKey.Length; i++)
            {
                if (upperLetter)
                {
                    upperLetter = false;
                    result.Append(upperCaseKey[i]);

                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Transforms keys in PascalCase to a form with spaces (PascalCaseKey => 'Pascal case key').
        /// </summary>
        /// <param name="pascalCaseKey">Key in pascal case (for example PascalCaseKey)</param>
        /// <returns>Key with spaces beetwen upper cases (PascalCaseKey => 'Pascal case key')</returns>
        public static string TransformPascalCaseToSpaces(this string pascalCaseKey)
        {
            if (pascalCaseKey == null || pascalCaseKey.Length < 1)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            bool upperLetter = true;
            result.Append(pascalCaseKey[0]);
            for (int i = 1; i < pascalCaseKey.Length; i++)
            {

                if (upperLetter)
                {
                    upperLetter = false;
                    result.Append(pascalCaseKey[i]);
                }
            }

            return result.ToString();
        }
    }
}
