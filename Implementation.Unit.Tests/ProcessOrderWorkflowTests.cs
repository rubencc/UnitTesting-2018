using System;
using System.Threading.Tasks;
using FluentAssertions;
using Implementations;
using Interfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Implementation.Unit.Tests
{
    [Trait("Unit Test", "ProcessOrderWorkflow")]
    public class ProcessOrderWorkflowTests
    {
        private ProcessOrderWorkflow workflow;
        private ITracer tracer;
        private ITrackingService trackingService;
        private IProxyRepository proxyRepository;
        private IOrderResponseMapper mapper;

        public ProcessOrderWorkflowTests()
        {
            this.tracer = Substitute.For<ITracer>();
            this.trackingService = Substitute.For<ITrackingService>();
            this.proxyRepository = Substitute.For<IProxyRepository>();
            this.mapper = Substitute.For<IOrderResponseMapper>();

            this.workflow = new ProcessOrderWorkflow(this.tracer, this.trackingService, this.proxyRepository, this.mapper);
        }

        ~ProcessOrderWorkflowTests()
        {
            this.workflow = null;
            this.tracer = null;
            this.trackingService = null;
            this.proxyRepository = null;
            this.mapper = null;
        }

        [Fact(DisplayName = "Inject tracer null reference")]
        public void Inject_tracer_null_tracert_reference()
        {
            var exception =
                Record.Exception(() => new ProcessOrderWorkflow(null, this.trackingService, this.proxyRepository, this.mapper));

            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Inject tracking null reference")]
        public void Inject_tracer_null_tracking_reference()
        {
            var exception =
                Record.Exception(() => new ProcessOrderWorkflow(this.tracer, null, this.proxyRepository, this.mapper));

            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Inject proxy null reference")]
        public void Inject_tracer_null_proxy_reference()
        {
            var exception =
                Record.Exception(() => new ProcessOrderWorkflow(this.tracer, this.trackingService, null, this.mapper));

            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Inject factory null reference")]
        public void Inject_tracer_null_mapper_reference()
        {
            var exception =
                Record.Exception(() => new ProcessOrderWorkflow(this.tracer, this.trackingService, this.proxyRepository, null));

            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Process Order succeed")]
        public async Task Process_Order_succeed()
        {
            //arrange
            IOrder order = new Order();

            //act
            await this.workflow.ProcessOrderAsync(order).ConfigureAwait(false);

            //assert
            this.tracer.Received(1).Log(Arg.Any<string>(), Arg.Any<int>());
            await this.trackingService
                .CreateTrackingInfoAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .ConfigureAwait(false);
            this.mapper.Received(1).MapFrom(order, Arg.Any<ITrackingInfo>());
            await this.proxyRepository.Received(1).AddResponseAsync(Arg.Any<IOrderResponse>()).ConfigureAwait(false);
        }

        [Fact(DisplayName = "Process Order failed")]
        public async Task Process_Order_failed()
        {
            IOrder order = new Order();

            this.trackingService
                .CreateTrackingInfoAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .Throws(x => new Exception());

            await this.workflow.ProcessOrderAsync(order).ConfigureAwait(false);

            this.tracer.Received(3).Log(Arg.Any<string>(), Arg.Any<int>());
            await this.trackingService.Received(1)
                .CreateTrackingInfoAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .ConfigureAwait(false);
            this.mapper.Received(0).MapFrom(order, Arg.Any<ITrackingInfo>());
            await this.proxyRepository.Received(0).AddResponseAsync(Arg.Any<IOrderResponse>()).ConfigureAwait(false);
            await this.trackingService.Received(1).CancelTrackingAsync(Arg.Any<Guid>()).ConfigureAwait(false);
        }

        //Refacor -> Pasa a estar en una dependencia.
        //[Fact(DisplayName = "Validate map")]
        //public async Task Validate_Map()
        //{
        //    IOrder order = new Order()
        //    {
        //        Id = Guid.NewGuid(),
        //        Address = "address",
        //        Name = "name",
        //        NumberOfItems = 10,
        //        PostalCode = "postalcode",
        //        Sku = "sku"
        //    };

        //    IOrderResponse response = new OrderResponse();

        //    this.factory.Resolve<IOrderResponse>().Returns(x => response);

        //    await this.workflow.ProcessOrderAsync(order).ConfigureAwait(false);

        //    response.Id.Should().NotBeEmpty();
        //    response.Address.Should().Be(order.Address);
        //    response.Name.Should().Be(order.Name);
        //    response.NumberOfItems.Should().Be(order.NumberOfItems);
        //    response.OrderId.Should().Be(order.Id);
        //    response.PostalCode.Should().Be(order.PostalCode);
        //    response.Sku.Should().Be(order.Sku);
        //}
    }
}
