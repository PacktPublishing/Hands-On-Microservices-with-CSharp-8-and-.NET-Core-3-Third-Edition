using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace FlixOne.BookStore.ProductService.Test.Utilities
{
    public class CustomOutput : IOutput
    {
        private readonly ITestOutputHelper _output;

        public CustomOutput(ITestOutputHelper output) => _output = output;

        public void WriteLine(string line) => _output.WriteLine(line);
    }
}