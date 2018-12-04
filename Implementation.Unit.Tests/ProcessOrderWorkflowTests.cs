using System;
using System.Threading.Tasks;
using FluentAssertions;
using Implementations;
using Interfaces;
using IoC.Interfaces;
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
        private IFactory factory;

        public ProcessOrderWorkflowTests()
        {
            this.tracer = Substitute.For<ITracer>();
            this.trackingService = Substitute.For<ITrackingService>();
            this.proxyRepository = Substitute.For<IProxyRepository>();
            this.factory = Substitute.For<IFactory>();

            this.workflow = new ProcessOrderWorkflow(this.tracer, this.trackingService, this.proxyRepository, this.factory);
        }

        ~ProcessOrderWorkflowTests()
        {
            this.workflow = null;
            this.tracer = null;
            this.trackingService = null;
            this.proxyRepository = null;
            this.factory = null;
        }

        [Fact(DisplayName = "Inject tracer null tracer reference")]
        public void Inject_tracer_null_tracer_reference()
        {
            var exception =
                Record.Exception(() => new ProcessOrderWorkflow(null, this.trackingService, this.proxyRepository, this.factory));

            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Inject tracer null tracking reference")]
        public void Inject_tracer_null_tracking_reference()
        {
            var exception =
                Record.Exception(() => new ProcessOrderWorkflow(this.tracer, null, this.proxyRepository, this.factory));

            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Inject tracer null proxy reference")]
        public void Inject_tracer_null_proxy_reference()
        {
            var exception =
                Record.Exception(() => new ProcessOrderWorkflow(this.tracer, this.trackingService, null, this.factory));

            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Inject tracer null factory reference")]
        public void Inject_tracer_null_factory_reference()
        {
            var exception =
                Record.Exception(() => new ProcessOrderWorkflow(this.tracer, this.trackingService, this.proxyRepository, null));

            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = "Process null order failed")]
        public async Task Process_null_Order_failed()
        {
            IOrder order = null;

            IOrderResponse response = new OrderResponse();

            this.factory.Resolve<IOrderResponse>().Returns(x => response);

            var exception =
                await Record.ExceptionAsync(() => this.workflow.ProcessOrderAsync(order));

            exception.Should().BeOfType<ArgumentNullException>();

            await this.trackingService.Received(0)
                .CreateTrackingInfoAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .ConfigureAwait(false);

            this.tracer.Received(0).Log(Arg.Any<string>(), Arg.Any<int>());

            this.factory.Received(0).Resolve<IOrderResponse>();

            await this.proxyRepository.Received(0).AddResponseAsync(response).ConfigureAwait(false);

            await this.trackingService.Received(0).CancelTrackingAsync(Arg.Any<Guid>()).ConfigureAwait(false);
        }

        [Fact(DisplayName = "Process order succeed")]
        public async Task Process_Order_succeed()
        {
            IOrder order = new Order();

            IOrderResponse response = new OrderResponse();

            this.factory.Resolve<IOrderResponse>().Returns(x => response);

            await this.workflow.ProcessOrderAsync(order).ConfigureAwait(false);

            await this.trackingService.Received(1)
                .CreateTrackingInfoAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .ConfigureAwait(false);

            this.tracer.Received(1).Log(Arg.Any<string>(), Arg.Any<int>());

            this.factory.Received(1).Resolve<IOrderResponse>();

            await this.proxyRepository.Received(1).AddResponseAsync(response).ConfigureAwait(false);

            await this.trackingService.Received(0).CancelTrackingAsync(Arg.Any<Guid>()).ConfigureAwait(false);
        }

        [Fact(DisplayName = "Process order failed")]
        public async Task Process_Order_failed()
        {
            IOrder order = new Order();

            this.proxyRepository.AddResponseAsync(Arg.Any<IOrderResponse>()).Throws(x => new Exception());

            var exception =
                Record.ExceptionAsync(() => this.workflow.ProcessOrderAsync(order));


            await this.trackingService.Received(1)
                .CreateTrackingInfoAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
                .ConfigureAwait(false);

            this.tracer.Received(3).Log(Arg.Any<string>(), Arg.Any<int>());

            this.factory.Received(1).Resolve<IOrderResponse>();

            await this.proxyRepository.Received(1).AddResponseAsync(Arg.Any<IOrderResponse>()).ConfigureAwait(false);

            await this.trackingService.Received(1).CancelTrackingAsync(Arg.Any<Guid>()).ConfigureAwait(false);
        }
    }
}
