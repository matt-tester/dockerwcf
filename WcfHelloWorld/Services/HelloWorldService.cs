using WcfHelloWorld.Contracts;

namespace WcfHelloWorld.Services
{
    public class HelloWorldService : IHelloWorldService
    {
        public string Hello()
        {
            return "Hello, World";
        }
    }
}