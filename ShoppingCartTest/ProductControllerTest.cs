using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shopping.Controllers;
using Shopping.Repository;
using System.Security.Claims;

namespace ShoppingCartTest
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public async Task AddDataToOrderDetail_ReturnsOk()
        {
            var userId = "testuser";
            var productId = 1002;
            var quantity = 2;
            var redirect = 0;

            var productRepoMock = new Mock<IProductRepo>();
            productRepoMock.Setup(repo => repo.AddItem(productId, quantity, userId))
                .ReturnsAsync(3);

            var controller = new ProductController(productRepoMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId),
                }, "mock"))
            };

            var result = await controller.AddDataToOrderDetail(productId, quantity, redirect) as OkObjectResult;

            Assert.AreEqual(3, (int)result.Value);
        }
        public async Task GetTotalItemInCart_ReturnsOk()
        {
            var userId = "testuser";

            var productRepoMock = new Mock<IProductRepo>();
            productRepoMock.Setup(repo => repo.Cartcount(userId))
                .ReturnsAsync(5);

            var controller = new ProductController(productRepoMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, userId),
                }, "mock"))
            };

            var result = await controller.GetTotalItemInCart() as OkObjectResult;

            Assert.Equals(5, (int)result.Value);
        }
    }
}