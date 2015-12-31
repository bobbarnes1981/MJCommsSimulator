using System;
using System.IO.Ports;

namespace MJCommsSimulator
{
    class MegaJolt
    {
        private SerialPort m_port;

        DateTime m_startTime;

        private byte m_advanceDegrees = 0x00;

        public MegaJolt(string port)
        {
            m_startTime = DateTime.Now;

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

            int i = m_port.BytesToRead;
            int buffer;
            for (int j = 0; j < i; j++)
            {
                buffer = m_port.ReadByte();
                switch ((char)buffer)
                {
                    case 'V':
                        Console.WriteLine("{0} VERSION Request 'V'", DateTime.Now);

                        byte[] v_bytes = new Version().ToBytes();
                        m_port.Write(v_bytes, 0, v_bytes.Length);
                        break;

                    case 'S':
                        Console.WriteLine("{0} STATUS Request 'S' {1}", DateTime.Now, m_advanceDegrees);

                        byte[] s_bytes = new State() { advanceDegrees = m_advanceDegrees }.ToBytes();
                        if ((DateTime.Now - m_startTime).TotalMilliseconds > 125)
                        {
                            m_advanceDegrees++;
                            m_startTime = DateTime.Now;
                        }
                        m_port.Write(s_bytes, 0, s_bytes.Length);
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
