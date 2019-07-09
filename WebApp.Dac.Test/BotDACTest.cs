using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Dac.Test.Builders;
using WebApp.Domain.DataContext.WebApp;
using Moq;
using WebApp.Domain.Interfaces.DAC;
using WebApp.DAC;
using WebApp.Domain.Entities;
using Bot.DAC.DataAccess;

namespace WebApp.Dac.Test
{
    [TestClass]
    public class BotDACTest : IDisposable
    {
        //Arrange        
        private ConnectionStrings connectionStrings;
        private User loggedInUser;
        private Stock stock;
        private Mock<IBotDAC> IBotMockDAC;
        private BotDAC botDAC;

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
            IBotMockDAC = new Mock<IBotDAC>();
            botDAC = new BotDAC(connectionStrings);
            stock = UnitTestBuilders.BuildStock();

        }

        [TestMethod]
        public void GetStockItemColumnsTest()
        {
            //Act
            var validStockItem = botDAC.GetStockItemColumns(stock.Symbol);
            var invalidStockItem = botDAC.GetStockItemColumns("123");

            //Assert
            Assert.IsTrue(validStockItem.ColumnNames != "");
            Assert.IsTrue(validStockItem.ColumnValues != "");
            Assert.IsNull(invalidStockItem.ColumnNames);
            Assert.IsNull(invalidStockItem.ColumnValues);
            Assert.IsNotNull(validStockItem);
            Assert.IsNotNull(invalidStockItem);
        }

        [TestMethod]
        public void GetStockItemTest()
        {
            //Act
            var validStockItem = botDAC.GetStockItem(stock.Symbol);

            //Assert
            Assert.IsTrue(validStockItem.ID > 0);
            Assert.IsNotNull(validStockItem);            
        }
    }
}
