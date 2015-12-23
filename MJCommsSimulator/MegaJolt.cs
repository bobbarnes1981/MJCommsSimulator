using System;
using System.IO.Ports;

namespace MJCommsSimulator
{
    class MegaJolt
    {
        private SerialPort m_port;

        private byte m_advanceDegrees = 0x00;

        public MegaJolt(string port)
        {
            m_port = new SerialPort(port, 38400, Parity.None, 8, StopBits.One);

            m_port.DataReceived += port_dataReceived;
            m_port.ErrorReceived += port_errorReceived;
        }

        public void Run()
        {
            m_port.Open();
        }

        private void port_dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine("Event Type {0}", e.EventType);
            int buffer = m_port.ReadByte();
            if (buffer != -1)
            {
                switch ((char)buffer)
                {
                    case 'S':
                        byte[] data = new State() { advanceDegrees = m_advanceDegrees }.ToBytes();
                        m_advanceDegrees++;
                        Console.WriteLine("{0} STATUS Request 'S'", DateTime.Now);
                        m_port.Write(data, 0, data.Length);
                        break;

                    default:
                        Console.WriteLine("Unhandled input {0:x2}", buffer);
                        break;
                }
            }
        }

        private void port_errorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
