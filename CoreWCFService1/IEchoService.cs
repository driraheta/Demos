using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using CoreWCF;

namespace CoreWCFService1
{
    [DataContract]
    public class EchoFault
    {
        [DataMember]
        [AllowNull]
        public string Text { get; set; }
    }

    [ServiceContract]
    public interface IEchoService
    {
        [OperationContract]
        string Echo(string text);

        [OperationContract]
        string ComplexEcho(EchoMessage text);

        [OperationContract]
        [FaultContract(typeof(EchoFault))]
        string FailEcho(string text);


        [OperationContract]
        DataTable GetVentas(string IdCliente);

        [OperationContract]
        void NewDataMongoo(Employee employee);

    }

    [DataContract]
    public class Employee
    {
        [AllowNull]
        [DataMember]
        public string Age { get; set; }

        [AllowNull]
        [DataMember]
        public string City { get; set; }

        [AllowNull]
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class EchoMessage
    {
        [AllowNull]
        [DataMember]
        public string Text { get; set; }
    }


}
