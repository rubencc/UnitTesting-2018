using System;
using FluentAssertions;
using Implementations;
using Interfaces;
using IoC.Interfaces;
using NSubstitute;
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

        [Fact(DisplayName = "Inject tracer null reference")]
        void Inject_tracer_null_reference()
        {
            var exception =
                Record.Exception(() => new ProcessOrderWorkflow(null, this.trackingService, this.proxyRepository, this.factory));

            exception.Should().BeOfType<ArgumentNullException>();
        }
    }
}
