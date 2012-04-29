using CSharpBbq.Business.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using FluentAssertions;
using CSharpBbq.Test.Utils;





namespace CSharpBbq.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for HttpClientExtensionsTest and is intended
    ///to contain all HttpClientExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HttpClientExtensionsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for CreateBasicAuth
        ///</summary>
        [TestMethod()]
        public void CreateBasicAuthTest()
        {
            HttpClient httpClient = null; // TODO: Initialize to an appropriate value
            HttpClientExtensions.CreateBasicAuth(httpClient);
            httpClient.DefaultRequestHeaders.Authorization.Should().NotBeNull();
        }


        /// <summary>
        ///A test for ThrowIfStatusNotSuccessful
        ///</summary>
        [TestMethod()]
        public void ThrowIfStatusNotSuccessfulTest()
        {
            //Arrange
            HttpResponseMessage response = null; // TODO: Initialize to an appropriate value

            //Act and Assert.
            ExceptionHelper.AssertThrows<Exception>(() => HttpClientExtensions.ThrowIfStatusNotSuccessful(response));
        }

    }
}
