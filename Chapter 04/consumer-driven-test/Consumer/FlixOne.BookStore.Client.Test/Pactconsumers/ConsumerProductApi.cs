using System;
using FlixOne.BookStore.Client.Test.Utilities;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace FlixOne.BookStore.Client.Test.Pactconsumers
{
    public class ConsumerProductApi : IDisposable
    {
        public ConsumerProductApi()
        {
            PactBuilder = new PactBuilder(new PactConfig
                {
                    SpecificationVersion = Constant.SpecificationVersion,
                    LogDir = Helper.SpecifyDirectory(Constant.LogDir),
                    PactDir = Helper.SpecifyDirectory(Constant.PactDir)
                })
                .ServiceConsumer(Constant.ConsumerName)
                .HasPactWith(Constant.ProviderName);

            MockProviderService = PactBuilder.MockService(Constant.Port, Constant.EnableSsl);
        }

        public IPactBuilder PactBuilder { get; }
        public IMockProviderService MockProviderService { get; }

        public string ServiceBaseUri => $"http://localhost:{Constant.Port}";

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}