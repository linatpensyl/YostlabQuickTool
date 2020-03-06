using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace PanIDandChannelModifier
{
    class Program
    {

        static void Main(string[] args)
        {
            byte panid = 0x1a;
            byte channel = 0x1a;
            SerialPort donglePort = openPort("COM3");
            SerialPort sensorPort = openPort("COM5");

            pairDevice(donglePort, sensorPort, panid, channel, 0);
            setImuMode(sensorPort);
            commitSetting(donglePort);
            commitSetting(sensorPort);


            Console.WriteLine("Operation Finished. Press Enter to exit");
            Console.ReadLine();
        }

        public static void pairDevice(SerialPort donglePort, SerialPort sensorPort, byte panid, byte channel, byte logicalID){
            byte[] serialNumber = getSerialNumber(sensorPort);
            setSensorOnLogicalID(donglePort, serialNumber, logicalID);
            changePanidAndChannel(donglePort, sensorPort, panid, channel);
            
        }

        public static void setSensorOnLogicalID(SerialPort dongle, byte[] serialNumber, byte logicalID) {
            List<byte> cmdList = new List<byte> {
                0xf7,
                0xd1,
                logicalID,
                serialNumber[0],
                serialNumber[1],
                serialNumber[2],
                serialNumber[3]
            };
            addCheckSum(cmdList);

            byte[] arr = cmdList.ToArray();

            dongle.BaseStream.Write(arr,0,arr.Length);
            
        }

        public static SerialPort openPort(string portName) {
            SerialPort port = new SerialPort(portName,115200,Parity.None,8,StopBits.One);
            if (port.IsOpen)
            {

            }
            else {
                try {
                    port.Open();
                } catch (Exception exception) {
                    Console.WriteLine(exception.Message);
                }
            }
            return port;
        }

        public static void changePanidAndChannel(SerialPort dongle, SerialPort sensor, byte panID, byte channel) {
            setPanID(dongle, panID);
            setPanID(sensor, panID);
            setChannel(dongle, channel);
            setChannel(sensor, channel);
            commitSetting(sensor);
            commitSetting(dongle);
        }

        public static void setPanID(SerialPort port, byte PanID) {
            Console.WriteLine("Setting PanID of " + port.PortName + " to " + PanID);
            List<byte> panIDList = new List<byte> {
                0xf7,
                0xc1,
                0x00,
                PanID
            };
            addCheckSum(panIDList);
            byte[] pan_id_arr = panIDList.ToArray();
            port.BaseStream.Write(pan_id_arr,0,pan_id_arr.Length);
        }

        public static void setChannel(SerialPort port, byte channel) {
            Console.WriteLine("Setting Channel of " + port.PortName + " to " + channel);
            if (channel < 0x0B || channel > 0x1A) {
                throw new ArgumentException("Please provide a channel number between 11 and 26");
            }

            List<byte> channelList = new List<byte> {
                0xf7,
                0xc3,
                channel
            };
            addCheckSum(channelList);
            byte[] channel_arr = channelList.ToArray();
            port.BaseStream.Write(channel_arr,0,channel_arr.Length);
            
        }

        public static void setImuMode(SerialPort sensorPort) {
            Console.WriteLine("Setting Channel of " + sensorPort.PortName + " to " + 0);
            List<byte> imuList = new List<byte> {
                0xf7,
                0x7b,
                0x00
            };
            addCheckSum(imuList);
            byte[] imu_arr = imuList.ToArray();
            sensorPort.BaseStream.Write(imu_arr,0,imu_arr.Length);
            commitSetting(sensorPort);
        }

        public static byte[] getSerialNumber(SerialPort port) {
            // 0xed get serial number
            List<byte> li = new List<byte> {
                0xf7,
                0xed
            };
            addCheckSum(li);
            byte[] ar = li.ToArray();
            byte[] rtn = new byte[4];
            port.BaseStream.Write(ar,0,ar.Length);
            port.BaseStream.Read(rtn, 0, rtn.Length);

            return rtn;
        }

        public static void factoryReset(SerialPort port) {
            // 0xe0 factory reset
            List<byte> li = new List<byte>
            {
                0xf7,
                0xe0
            };
            addCheckSum(li);
            byte[] ar = li.ToArray();
            port.BaseStream.Write(ar, 0, ar.Length);
        }

        public static void addCheckSum(List<byte> input) {
            byte checkSum = 0;
            for (int i = 1; i < input.Count; i++) {
                checkSum += input[i];
            }
            input.Add(checkSum);
        }

        public static void commitSetting(SerialPort port) {
            //0xe1 commit wired setting
            //0xc5 commit wireless setting

            List<byte> wiredList = new List<byte> {
                0xf7,
                0xe1
            };
            addCheckSum(wiredList);
            byte[] wired_arr = wiredList.ToArray();
            port.BaseStream.Write(wired_arr, 0, wired_arr.Length);

            List<byte> wirelessList = new List<byte> {
                0xf7,
                0xc5
            };
            addCheckSum(wirelessList);
            byte[] wireless_arr = wirelessList.ToArray();
            port.BaseStream.Write(wireless_arr,0,wireless_arr.Length);

        }

        public static List<byte[]> DeviceList = new List<byte[]> {
            new byte[]{0x12, 0x00, 0x0C, 0x8A},
            new byte[]{0x12, 0x00, 0x0C, 0x8A},
            new byte[]{0x12, 0x00, 0x0C, 0x8A},
            new byte[]{0x12, 0x00, 0x0C, 0x8A},
            new byte[]{0x12, 0x00, 0x0C, 0x8A},
            new byte[]{0x12, 0x00, 0x0C, 0x8A},
            new byte[]{0x12, 0x00, 0x0C, 0x8A},
        };
    }
}
