using System;
using FluentAssertions;
using Implementations;
using Interfaces;
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

        public ProcessOrderWorkflowTests()
        {
            this.tracer = Substitute.For<ITracer>();
            this.trackingService = Substitute.For<ITrackingService>();
            this.proxyRepository = Substitute.For<IProxyRepository>();

            this.workflow = new ProcessOrderWorkflow(this.tracer, this.trackingService, this.proxyRepository);
        }

        ~ProcessOrderWorkflowTests()
        {
            this.workflow = null;
            this.tracer = null;
            this.trackingService = null;
            this.proxyRepository = null;
        }

        [Fact(DisplayName = "Inject tracer null reference")]
        void Inject_tracer_null_reference()
        {
            var exception =
                Record.Exception(() => new ProcessOrderWorkflow(null, this.trackingService, this.proxyRepository));

            exception.Should().BeOfType<ArgumentNullException>();
        }
    }
}
