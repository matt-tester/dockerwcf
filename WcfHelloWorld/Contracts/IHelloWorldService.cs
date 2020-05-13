using System.ServiceModel;

namespace WcfHelloWorld.Contracts
{
    [ServiceContract(Namespace = Namespaces.DefaultNamespace)]
    public interface IHelloWorldService
    {
        [OperationContract]
        string Hello();
    }
}