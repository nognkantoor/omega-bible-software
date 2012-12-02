using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Common.Core.Error;
using Common.Core.Collections;

namespace Common.Core.XML
{
    public class Query
    {
        #region Helper classes

        /// <summary>
        /// Match class displays the information about a match in the xml query.
        /// </summary>
        public class Match
        {
            /// <summary>
            /// Initializes a new instance of the Match class.
            /// </summary>
            /// <param name="selector">Selector that was queried</param>
            /// <param name="elements">Elements that matched the selector</param>
            internal Match(string selector, params XElement[] elements)
            {
                if (elements != null)
                {
                    Elements = new List<XElement>(elements);
                }
                else
                {
                    Elements = new List<XElement>();
                }

                Selector = selector;
            }


            /// <summary>
            /// Gets the selector that was used for the query.
            /// </summary>
            public string Selector
            {
                get;
                protected set;
            }

            /// <summary>
            /// Gets the list of XElement items that matched the selector.
            /// </summary>
            public IList<XElement> Elements
            {
                get;
                protected set;
            }

            /// <summary>
            /// Gets the selected by index element matching the selector.
            /// </summary>
            /// <param name="index">Index of the element.</param>
            /// <returns>THe element from the given index.</returns>
            public XElement this[int index]
            {
                get
                {
                    if (index >= Elements.Count || index < 0) return null;

                    return Elements[index];
                }
            }

            /// <summary>
            /// Gets the number of matched elements.
            /// </summary>
            public int Count
            {
                get
                {
                    return Elements.Count;
                }
            }
        }

        internal class Selector
        {
            public Selector(string selector)
            {
                string[] parts = selector.Split(' ');
                Parts = new List<SelectorPart>();
                parts.Each(a => Parts.Add(new SelectorPart(a)));
            }

            public List<SelectorPart> Parts
            {
                get;
                protected set;
            }
        }

        internal class SelectorPart
        {
            public static readonly Exceptions WrongSelector = Exceptions.Pool.Get("SelectionPartWrongSelector")
                .Set("The given selector part contains spaces, which means it's not a proper selector part string");

            public SelectorPart(string selectorPart)
            {
                if (selectorPart.Contains(' '))
                {
                    WrapperException.Throw(WrongSelector);
                }
            }

            public List<string> ElementNames
            {
                get;
                protected set;
            }

            public List<Tuple<string, string>> ElementAttributes
            {
                get;
                protected set;
            }
        }

        #endregion Helper classes

        #region Private fields

        #endregion Private fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Query class.
        /// </summary>
        public Query()
        { 
        }

        /// <summary>
        /// Initializes a new instance of the Query class with a string
        /// xml document.
        /// </summary>
        /// <param name="xmlDocument">XML Document as string.</param>
        public Query(string xmlDocument)
        {
            Document = XDocument.Parse(xmlDocument);
        }

        /// <summary>
        /// Initializes a new instance of the Query class with a 
        /// xml document.
        /// </summary>
        /// <param name="xmlDocument">XML Document</param>
        public Query(XDocument xmlDocument)
        {
            Document = xmlDocument;
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Queries the xml document with the given selector to return an XElement matching the selector.
        /// </summary>
        /// <param name="selector">jQuery/css type selector.</param>
        /// <returns>XElements matching the selector.</returns>
        public XElement Q(string selector)
        {
            if (Document == null) return null;

            return null;
        }


        #endregion Public methods

        #region Public properties

        /// <summary>
        /// Gets or sets the xml document for querying.
        /// </summary>
        public XDocument Document
        {
            get;
            set;
        }

        #endregion Public properties
        
    }
}
