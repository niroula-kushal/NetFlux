using System;
using NetFlux;
using Xunit;

namespace NetFluxTest
{
    public class DispatcherTest
    {

        public Dispatcher dispatcher;
        public DispatcherTest()
        {
            dispatcher = new Dispatcher();
        }
        [Fact]
        public void Test__Register__registers()
        {
            dispatcher.Register<NameChangeAction>(x =>
            {

            });
            Assert.Single(dispatcher.actions);
        }

        [Fact]
        public void Test__Dispatch__dispatches_each_action()
        {
            var expectedCallCount = 2;
            var actualCallCount = 0;
            dispatcher.Register<NameChangeAction>(x =>
            {
                actualCallCount++;
            });
            dispatcher.Register<NameChangeAction>(x =>
            {
                actualCallCount++;
            });
            dispatcher.Dispatch<NameChangeAction>(new NameChangeAction());
            Assert.Equal(expectedCallCount, actualCallCount);
        }

        class NameChangeAction
        {
        }
    }
}
