using CSharpBbq.Business.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpBbq.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for ServiceAgentBaseTest and is intended
    ///to contain all ServiceAgentBaseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ServiceAgentBaseTest
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
        ///A test for Get
        ///</summary>
        public void GetTestHelper<T>()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ServiceAgentBase_Accessor target = new ServiceAgentBase_Accessor(param0); // TODO: Initialize to an appropriate value
            string methodUniformResourceIdentifier = string.Empty; // TODO: Initialize to an appropriate value
            T expected = default(T); // TODO: Initialize to an appropriate value
            T actual;
            actual = target.Get<T>(methodUniformResourceIdentifier);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        internal virtual ServiceAgentBase_Accessor CreateServiceAgentBase_Accessor()
        {
            // TODO: Instantiate an appropriate concrete class.
            ServiceAgentBase_Accessor target = null;
            return target;
        }

        [TestMethod()]
        [DeploymentItem("CSharpBbq.Business.dll")]
        public void GetTest()
        {
            GetTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        public void SaveTestHelper<T>()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ServiceAgentBase_Accessor target = new ServiceAgentBase_Accessor(param0); // TODO: Initialize to an appropriate value
            string methodUniformResourceIdentifier = string.Empty; // TODO: Initialize to an appropriate value
            T modelObject = default(T); // TODO: Initialize to an appropriate value
            T expected = default(T); // TODO: Initialize to an appropriate value
            T actual;
            actual = target.Save<T>(methodUniformResourceIdentifier, modelObject);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        [DeploymentItem("CSharpBbq.Business.dll")]
        public void SaveTest()
        {
            SaveTestHelper<GenericParameterHelper>();
        }
    }
}
