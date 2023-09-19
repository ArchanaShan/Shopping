using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Common;
using Moq;
using Shopping.Repository;

namespace ShoppingCartTest
{

    [TestClass]
    public class CartControllerTest
    {
        [TestMethod]
        public async Task IncreaseQuantity()
        {
            int productId = 1001;
            int orderId = 123;
            int initialQuantity = 3; 
            int expectedUpdatedQuantity = initialQuantity + 1;

            var contextOptions = new DbContextOptionsBuilder<ShoppingContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            var context = new ShoppingContext(contextOptions);

            context.OrderDetails.Add(new OrderDetail
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = initialQuantity,
                CreatedOn = DateTime.Now,
                CreatedBy = "Admin",
                ModifiedOn = DateTime.Now,
                ModifiedBy = "Admin"
            });
            context.SaveChanges();
            var spMock = new Mock<StoredProcedureCall>();
            spMock.Setup(sp => sp.UpdateOrderDetail(orderId, productId, expectedUpdatedQuantity));

            var cartRepo = new CartRepo(context, spMock.Object);

            int updatedQuantity = await cartRepo.IncreaseQuantity(productId, orderId);

            Assert.AreEqual(expectedUpdatedQuantity, updatedQuantity);
        }
        [TestMethod]
        public async Task DecreaseQuantity()
        {
            int productId = 1001;
            int orderId = 123;
            int initialQuantity = 3;
            int expectedUpdatedQuantity = initialQuantity + 1;

            var contextOptions = new DbContextOptionsBuilder<ShoppingContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            var context = new ShoppingContext(contextOptions);

            context.OrderDetails.Add(new OrderDetail
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = initialQuantity,
                CreatedOn = DateTime.Now,
                CreatedBy = "Admin",
                ModifiedOn = DateTime.Now,
                ModifiedBy = "Admin"
            });
            context.SaveChanges();
            var spMock = new Mock<StoredProcedureCall>();
            spMock.Setup(sp => sp.DecreaseOrderDetail(orderId, productId, expectedUpdatedQuantity));

            var cartRepo = new CartRepo(context, spMock.Object);

            int updatedQuantity = await cartRepo.IncreaseQuantity(productId, orderId);

            Assert.AreEqual(expectedUpdatedQuantity, updatedQuantity);
        }
    }
}