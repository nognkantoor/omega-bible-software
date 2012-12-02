using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Windows.Markup;

namespace Common.Controls.WPF
{
    /// <summary>
    /// Class with all kinds of helpfull extensions to WPF controls.
    /// </summary>
    public static class Extensions
    {
        #region Traversing through visual tree
        
        /// <summary>
        /// Gets the first parent of a dependency object, which satisfies the condition.
        /// </summary>
        /// <param name="element">Element to start from</param>
        /// <param name="condition">Condition to satisfy</param>
        /// <returns>The found parent, or null if not found.</returns>
        public static DependencyObject GetParentWhere(this DependencyObject element, Func<DependencyObject, bool> condition)
        {
            DependencyObject parent = element;
            while (parent != null)
            {
                parent = VisualTreeHelper.GetParent(parent);
                if (condition == null || condition(parent)) return parent;
            }

            return null;
        }

        /// <summary>
        /// Gets the parent of the element.
        /// </summary>
        /// <param name="element">Element to get parent from.</param>
        /// <returns>Parent of the element (can be null)</returns>
        public static DependencyObject GetParent(this DependencyObject element)
        {
            return element.GetParentWhere(null);
        }

        /// <summary>
        /// Gets the first parent of the element, which satisfies the condition, and is of the given type.
        /// </summary>
        /// <typeparam name="T">Type of the parent.</typeparam>
        /// <param name="element">Element to start searching from.</param>
        /// <param name="condition">Condition to satisfy.</param>
        /// <returns>The found parent, or null if not found.</returns>
        public static T GetParent<T>(this DependencyObject element, Func<T, bool> condition) where T : DependencyObject
        {
            return element.GetParentWhere(f => f is T && (condition == null || condition((T)f))) as T;
        }

        /// <summary>
        /// Gets the first parent of the element, which satisfies the given type.
        /// </summary>
        /// <typeparam name="T">Type of the parent.</typeparam>
        /// <param name="element">Element to start searching from.</param>
        /// <returns>The found parent, or null if not found.</returns>
        public static T GetParent<T>(this DependencyObject element) where T : DependencyObject
        {
            return element.GetParentWhere(f => f is T) as T;
        }

        /// <summary>
        /// Gets the last parent of a dependency object, which satisfies the condition.
        /// </summary>
        /// <param name="element">Element to start from</param>
        /// <param name="condition">Condition to satisfy</param>
        /// <returns>The found parent, or null if not found.</returns>
        public static DependencyObject GetLastParentWhere(this DependencyObject element, Func<DependencyObject, bool> condition)
        {
            DependencyObject parent = element;
            DependencyObject found = null;
            while (parent != null)
            {
                parent = VisualTreeHelper.GetParent(parent);
                if (condition == null || condition(parent))
                {
                    found = parent;
                }
            }

            return found;
        }

        /// <summary>
        /// Gets the last parent of the element, which satisfies the condition, and is of the given type.
        /// </summary>
        /// <typeparam name="T">Type of the parent.</typeparam>
        /// <param name="element">Element to start searching from.</param>
        /// <param name="condition">Condition to satisfy.</param>
        /// <returns>The found parent, or null if not found.</returns>
        public static T GetLastParent<T>(this DependencyObject element, Func<T, bool> condition) where T : DependencyObject
        {
            return element.GetLastParentWhere(f => f is T && (condition == null || condition((T)f))) as T;
        }

        /// <summary>
        /// Gets the last parent of the element, which satisfies the given type.
        /// </summary>
        /// <typeparam name="T">Type of the parent.</typeparam>
        /// <param name="element">Element to start searching from.</param>
        /// <returns>The found parent, or null if not found.</returns>
        public static T GetLastParent<T>(this DependencyObject element) where T : DependencyObject
        {
            return element.GetLastParentWhere(f => f is T) as T;
        }


        #endregion Traversing through visual tree

        #region WPF application extensions

        private static string GetSearchFileFromUri(Uri uri)
        {
            if (uri.IsAbsoluteUri)
            {
                return Path.GetFileName(uri.AbsolutePath);
            }
            return uri.OriginalString;
        }

        public static void SwitchStyleDictionaries(this Application application, string currentDictionaryFileName, string switchDictionaryFileName)
        {
            if (application != null && currentDictionaryFileName != null && switchDictionaryFileName != null)
            {
                if (application.Resources != null &&
                    application.Resources.MergedDictionaries != null && 
                    application.Resources.MergedDictionaries.Count > 0)
                {
                    var dictionary = application.Resources.MergedDictionaries.FirstOrDefault(m => GetSearchFileFromUri(m.Source).EndsWith(currentDictionaryFileName));
                    if (dictionary != null)
                    {
                        var switched = TryLoadDictionary(switchDictionaryFileName);
                        if (switched.Count > 0)
                        {
                            application.Resources.MergedDictionaries.Remove(dictionary);
                            application.Resources.MergedDictionaries.Add(switched[0]);
                        }
                    }
                }
            }
        }

        public static void SetStyleDictionaries(this Application application, string stylePath, params string [] filenames)
        {
            if (stylePath != null)
            {
                List<string> searchedFiles = filenames != null ? filenames.ToList() : new List<string>();

                if (searchedFiles.Count == 0)
                {
                    if (application.Resources != null)
                    {
                        if (application.Resources.MergedDictionaries != null
                            && application.Resources.MergedDictionaries.Count > 0)
                        {
                            foreach (ResourceDictionary dict in application.Resources.MergedDictionaries)
                            {
                                searchedFiles.Add(GetSearchFileFromUri(dict.Source));
                            }
                        }

                        if (application.Resources.Source != null)
                        {
                            searchedFiles.Add(GetSearchFileFromUri(application.Resources.Source));
                        }
                    }
                }

                for (int i = 0; i < searchedFiles.Count; i++)
                {
                    searchedFiles[i] = stylePath + searchedFiles[i];
                }
                var dictionaries = TryLoadDictionary(searchedFiles.ToArray());
            }
        }

        private static IList<ResourceDictionary> TryLoadDictionary(params string [] searchedFiles)
        {
            IList<ResourceDictionary> dictionaries = new List<ResourceDictionary>();
            foreach (string file in searchedFiles)
            {
                ResourceDictionary resource = null;
                try
                {
                    try
                    {
                        resource = (ResourceDictionary)XamlReader.Load(System.Xml.XmlReader.Create(file));
                    }
                    catch (Exception)
                    {
                        resource = new ResourceDictionary() { Source = new Uri(file, UriKind.Relative) };
                    }
                }
                catch (Exception)
                {
                    resource = null;
                }

                if (resource != null)
                {
                    dictionaries.Add(resource);
                }
            }

            return dictionaries;
        }

        #endregion WPF application extensions
    }
}
