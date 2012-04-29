using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpBbq.Business.Model;

namespace CSharpBbq.Test.Utils
{
    public static class ObjectFactory
    {

        public static Movie FakeMovie()
        {
            var m = new Movie();
            m.Title = "Harry Potter";
            m.Tags = new List<Tag> {new Tag() {TagName = "Drama"}};
            return m;
        }
    }
}
