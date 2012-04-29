using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel.Web;

namespace CSharpBbq.Test.Utils
{
   public static class ExceptionHelper
    {

        public static void AssertThrows<T>(Action method, string expectedMessage = null) where T : Exception
        {
            try
            {
                method();
                Assert.Fail("Expected exception of type {0} to be thrown.", typeof(T).ToString());
            }
            catch (T exception)
            {
                if (!string.IsNullOrEmpty(expectedMessage))
                {
                    exception.Message.Should().Be(expectedMessage);
                }
            }
        }

        public static void AssertThrowsWebFaultException<T, R>(Action method, System.Net.HttpStatusCode? expectedStatusCode = null, string expectedMessage = null) where T : WebFaultException<R>
        {
            try
            {
                method();
                Assert.Fail("Expected exception of type {0} to be thrown.", typeof(T).ToString());
            }
            catch (T exception)
            {
                if (expectedStatusCode != null)
                {
                    exception.StatusCode.Should().Equals(expectedStatusCode);
                }

                if (!string.IsNullOrEmpty(expectedMessage))
                {
                    exception.Message.Should().Be(expectedMessage);
                }
            }
        }
    }
}
