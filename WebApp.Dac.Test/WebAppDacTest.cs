using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Dac.Test.Builders;
using WebApp.Domain.DataContext.WebApp;
using Moq;
using WebApp.Domain.Interfaces.DAC;
using WebApp.DAC;
using WebApp.Domain.Entities;

namespace WebApp.Dac.Test
{
    [TestClass]
    public class WebAppDacTest : IDisposable
    {
        //Arrange        
        private ConnectionStrings connectionStrings;
        private Users loggedInUser;
        private Mock<IWebAppDAC> WebAppDacMock;
        private WebAppDAC webAppDAC;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(Boolean disposing)
        {

        }

        [TestInitialize]
        public void Init()
        {
            //Arrange
            connectionStrings = UnitTestBuilders.BuildConnectionStrings();
            loggedInUser = UnitTestBuilders.BuildUser();            
            WebAppDacMock = new Mock<IWebAppDAC>();
            webAppDAC = new WebAppDAC(connectionStrings);

        }

        [TestMethod]
        public void LoginTest()
        {
            //Act
            var validUserResult = webAppDAC.Login(loggedInUser.Email, loggedInUser.Password);
            var invalidUserResult = webAppDAC.Login(loggedInUser.Email, "123");

            //Assert
            Assert.IsTrue(validUserResult.UserID > 0);
            Assert.IsNotNull(validUserResult);            
            Assert.IsTrue(invalidUserResult.UserID == 0);
            Assert.IsNotNull(invalidUserResult);
        }

        [TestMethod]
        public void GetUsersTest()
        {
            //Act
            var validGetUsersTest = webAppDAC.GetUsers(loggedInUser.UserID);            

            //Assert
            Assert.IsTrue(validGetUsersTest.Count > 0);
            Assert.IsNotNull(validGetUsersTest);            
        }
    }
}
