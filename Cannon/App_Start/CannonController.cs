using System;
using System.Threading;
using System.IO.Ports;

using System.IO;

namespace Cannon
{
    public class CannonController
    {
        private static SerialPort currentPort;
        private static bool portFound;

        public static void SetComPort()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    currentPort = new SerialPort(port, 9600);
                    if (Hello())
                    {
                        portFound = true;
                        break;
                    }
                    else
                    {
                        portFound = false;
        
            }
                }
            }
            catch (Exception e)
            {
            }
        }

        public static bool Hello()
        {
            return SendCharacter('H', "hi");
        }

        public static bool Fire()
        {
            return SendCharacter('F', "Fired");
        }

        public static bool SendCharacter(char character, string expectedMessage)
        {
            try
            {
                //The below setting are for the Hello handshake
                byte[] buffer = new byte[1];
                buffer[0] = Convert.ToByte(character);
                int intReturnASCII = 0;
                char charReturnValue = (Char)intReturnASCII;
                currentPort.Open();
                currentPort.Write(buffer, 0, 1);
                Thread.Sleep(1000);
                int count = currentPort.BytesToRead;
                string returnMessage = "";
                while (count > 0)
                {
                    intReturnASCII = currentPort.ReadByte();
                    returnMessage = returnMessage + Convert.ToChar(intReturnASCII);
                    count--;
                }

                currentPort.Close();
                if (returnMessage.Contains(expectedMessage))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}