using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common.Core.Format;

namespace Common.Core.Test.Common.Core.Format
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [Test]
        public void MakeSlugTests()
        {
            StringExtensions.DefaultSlugDelimeter = '-';

            string slug = "This is a slug";
            Assert.AreEqual("this-is-a-slug", slug.MakeSlug());

            slug = "() _ something HARDER !$%^@ might say.";
            Assert.AreEqual("something-harder-might-say", slug.MakeSlug());

            slug = "This is a file.ext";
            Assert.AreEqual("this-is-a-file-ext", slug.MakeSlug());

            slug = "C:/myfolder/mypath/tofile.exe";
            Assert.AreEqual("c-myfolder-mypath-tofile-exe", slug.MakeSlug());
        }
    }
}
