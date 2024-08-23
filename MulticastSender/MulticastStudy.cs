using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MulticastSender
{
    class MulticastSender
    {
        // multicast 송신은 udp unicast 송신과 구현 동일
        public void sender()
        {
            string multicastIP = "239.0.0.222";
            int multicastPort = 11000;

            // UdpClient 인스턴스 생성
            using(UdpClient udpClient = new UdpClient())
            {
                // multicast를 위한 end point 생성
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(multicastIP), multicastPort);

                try
                {
                    // 보낼 메시지 생성 및 byte 배열로 변환
                    string message = "Multicast study";
                    byte[] buffer = Encoding.UTF8.GetBytes(message);

                    udpClient.Send(buffer, buffer.Length, endPoint);
                    Console.WriteLine("Success to send : " + message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fail to send Message : " + ex.Message);
                }
            }
        }
    }

    class MulticastReceiver
    {
        public void receive()
        {
            string multicastIp = "239.0.0.222";
            int multicastPort = 11000;

            using(UdpClient udpClient = new UdpClient()) 
            {
                // 멀티캐스트 그룹에 가입
                udpClient.JoinMulticastGroup(IPAddress.Parse(multicastIp));
                
                // 엔드포인트 설정(모든 수신자를 의미)
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, multicastPort);

                try
                {
                    Console.WriteLine("멀티캐스트 수신 대기 중...");

                    while(true)
                    {
                        // 데이터 수신
                        byte[] buffer = udpClient.Receive(ref remoteEndPoint);

                        string message = Encoding.UTF8.GetString(buffer);

                        Console.WriteLine("Received Message: " + message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fail to Recv from Multicast : " + ex.Message);
                }
            }
        }
    }
}
