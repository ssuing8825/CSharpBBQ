using CSharpBbq.Business.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using CSharpBbq.Business.Model;
using CSharpBbq.Test.Utils;
using FluentAssertions;

namespace CSharpBbq.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for MovieAgentTest and is intended
    ///to contain all MovieAgentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MovieAgentTest
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
        ///A test for MovieAgent Constructor
        ///</summary>
        [TestMethod()]
        public void MovieAgentConstructorTest()
        {

            HttpClientChannel httpClientChannel = new FakeChannel<Movie>(new Movie());
            var target = new MovieAgent(httpClientChannel);
            target.Should().NotBeNull();
        }

        /// <summary>
        ///A test for MovieAgent Constructor
        ///</summary>
        [TestMethod()]
        public void MovieAgentConstructorTest1()
        {
            MovieAgent target = new MovieAgent();
            target.Should().NotBeNull();
        }

        /// <summary>
        ///A test for GetMovie
        ///</summary>
        [TestMethod()]
        public void GetMovieTest()
        {
            MovieAgent target = new MovieAgent(); // TODO: Initialize to an appropriate value
            int movieId =  1; // TODO: Initialize to an appropriate value
            Movie expected = null; // TODO: Initialize to an appropriate value
            Movie actual;
            actual = target.GetMovie(movieId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SaveMovie
        ///</summary>
        [TestMethod()]
        public void SaveMovieTest()
        {
            MovieAgent target = new MovieAgent(); // TODO: Initialize to an appropriate value
            Movie movie = null; // TODO: Initialize to an appropriate value
            Movie expected = null; // TODO: Initialize to an appropriate value
            Movie actual;
            actual = target.SaveMovie(movie);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
