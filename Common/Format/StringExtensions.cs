using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Core.Format
{
    public static class StringExtensions
    {
        public const string SPECIAL = "!@#$%^&*()_+-=|}{\\][\":';?></.,~` ";
        public static char DefaultSlugDelimeter = '_';

        /// <summary>
        /// Returns a slug representation of a string.
        /// Removes all special characters and whitespaces by changing them to 
        /// the DefaultSlugDelimeter char.
        /// </summary>
        /// <param name="fromString">The string for conversion.</param>
        /// <returns>A slug of a string.</returns>
        public static string MakeSlug(this string fromString)
        {
            return fromString.MakeSlug(DefaultSlugDelimeter);
        }

        /// <summary>
        /// Returns a slug representation of a string.
        /// Removes all special characters and whitespaces.
        /// </summary>
        /// <param name="fromString">The string for conversion.</param>
        /// <param name="delimeter">Delimeter to use instead of special chars.</param>
        /// <returns>A slug of a string.</returns>
        public static string MakeSlug(this string fromString, char delimeter)
        {
            string result = fromString.Trim().ToLower();
            foreach (char c in SPECIAL)
            {
                result = result.Replace(c, delimeter);
            }

            int underscores = 0;
            int i = 0;
            List<int> at = new List<int>();
            while(i < result.Length)
            {
                underscores = result[i] != delimeter ? 0 : underscores + 1;
                if (underscores > 1)
                {
                    at.Add(i);
                }
                i++;
            }
            for(i = at.Count-1; i>=0; i--)
            {
                result = result.Remove(at[i], 1);
            }

            result = result.Trim(delimeter);
            
            return result;
        }
    }
}
