using CSharpBbq.Business.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CSharpBbq.Business.Model;
using Moq;
using CSharpBbq.Test.Utils;
using StructureMap.AutoMocking;
using FluentAssertions;

namespace CSharpBbq.Business.Test
{


    /// <summary>
    ///This is a test class for IMovieAgentTest and is intended
    ///to contain all IMovieAgentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IMovieAgentTest
    {
        private TestContext testContextInstance;
        private Movie fakeMovie = ObjectFactory.FakeMovie();

        [TestInitialize]
        public void TestInitialize()
        {
        }

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


        internal virtual IMovieAgent CreateIMovieAgent()
        {
            IMovieAgent target = new MovieAgent(new FakeChannel<Movie>(fakeMovie));
            return target;
        }

        /// <summary>
        ///A test for GetMovie
        ///</summary>   
        [TestMethod()]
        public void GetMovieTest()
        {
            IMovieAgent target = CreateIMovieAgent(); // TODO: Initialize to an appropriate value
            int movieId = 1; // TODO: Initialize to an appropriate value
            Movie expected = fakeMovie; // TODO: Initialize to an appropriate value
            Movie actual;
            actual = target.GetMovie(movieId);
          //  actual.Should().BeSameAs(expected);
            Assert.AreEqual(expected.Title, actual.Title);
        }

        /// <summary>
        ///A test for SaveMovie
        ///</summary>
        [TestMethod()]
        public void SaveMovieTest()
        {
            IMovieAgent target = CreateIMovieAgent(); // TODO: Initialize to an appropriate value
            Movie movie = fakeMovie; // TODO: Initialize to an appropriate value
            Movie expected = fakeMovie; // TODO: Initialize to an appropriate value
            Movie actual;
            actual = target.SaveMovie(movie);
            actual.Title.Should().Be(expected.Title);
        }
    }
}
