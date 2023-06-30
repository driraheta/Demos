using CoreWCF;
using System.Data;

namespace CoreWCFService1
{
    public class EchoService : IEchoService
    {
        private CoreWCFService1.Logic logica = new Logic();
        public string Echo(string text)
        {
            System.Console.WriteLine($"Received {text} from client!");
            return text;
        }

        public string ComplexEcho(EchoMessage text)
        {
            System.Console.WriteLine($"Received {text.Text} from client!");
            return text.Text;
        }

        public string FailEcho(string text)
            => throw new FaultException<EchoFault>(new EchoFault() { Text = "WCF Fault OK" }, new FaultReason("FailReason"));

        public DataTable GetVentas(string IdCliente)
        {
            return logica.GetDataVentas(IdCliente);
        }

        public void NewDataMongoo(Employee employee)
        {
            logica.NewDataMongoo(employee); 
        }

    }
}
