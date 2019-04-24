using System;
using System.Threading.Tasks;
using FluentAssertions;
using Implementations;
using Interfaces;
using IoC.Interfaces;
using NSubstitute;
using Xunit;

namespace Implementation.Unit.Tests
{
    [Trait("Unit Test", "ProcessOrderWorkflow")]
    public class OrderResponseMapperTests
    {
        private IOrderResponseMapper mapper;
        private IFactory factory;

        public OrderResponseMapperTests()
        {
            this.factory = Substitute.For<IFactory>();

            this.factory.Resolve<IOrderResponse>().Returns(x => new OrderResponse());

            this.mapper = new OrderResponseMapper(this.factory);
        }

        ~ OrderResponseMapperTests()
        {
            this.factory = null;
            this.mapper = null;
        }

        [Fact(DisplayName = "Validate map")]
        public void Validate_Map()
        {
            IOrder order = new Order()
            {
                Id = Guid.NewGuid(),
                Address = "address",
                Name = "name",
                NumberOfItems = 10,
                PostalCode = "postalcode",
                Sku = "sku"
            };

            ITrackingInfo info = new TrackingInfo();
            
            var response = this.mapper.MapFrom(order, info);

            response.Id.Should().NotBeEmpty();
            response.Address.Should().Be(order.Address);
            response.Name.Should().Be(order.Name);
            response.NumberOfItems.Should().Be(order.NumberOfItems);
            response.OrderId.Should().Be(order.Id);
            response.PostalCode.Should().Be(order.PostalCode);
            response.Sku.Should().Be(order.Sku);
            response.TrackingInfo.Should().Be(info);

        }
    }
}
