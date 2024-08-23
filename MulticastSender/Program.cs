using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // 멀티캐스트 IP와 포트를 설정합니다."localIp": "192.168.0.16",
        string multicastIp = "224.168.0.1";
        int multicastPort = 4000;

        string multicastIp2 = "224.168.0.12";
        int multicastPort2 = 4001;

        // 멀티캐스트 그룹에 가입하기 위해 UdpClient를 생성합니다.
        UdpClient udpClient = new UdpClient();
        UdpClient udpClient2 = new UdpClient();

        try
        {
            // 멀티캐스트 IP 주소를 설정합니다.
            IPAddress multicastAddress = IPAddress.Parse(multicastIp);
            IPAddress multicastAddress2 = IPAddress.Parse(multicastIp2);

            // 멀티캐스트 그룹에 가입합니다.
            udpClient.JoinMulticastGroup(multicastAddress);
            udpClient2.JoinMulticastGroup(multicastAddress2);

            // 송신할 데이터(문자열)를 설정합니다.
            string message = "<VertiportStatus>\r\n\t<Header>\r\n\t\t\t<dataName>VertiportStatus</dataName>\r\n\t\t\t<dataNickName>D8</dataNickName>\r\n\t\t\t<transmitType>Notification</transmitType>\r\n\t\t\t<messageId>VPO01-D8-2024.05.03 13:02:15-0002</messageId>\r\n\t</Header>\r\n\t<Body>\r\n\t\t<messageDateTime>2023.04.06 12:02:04</messageDateTime>\r\n\t\t<veripotId>test</veripotId>\t\t\r\n\t\t<zoneAvailability>OPEN</zoneAvailability>\r\n\t\t<FatoAvailability>\r\n\t\t\t<Fato>\r\n\t\t\t\t<fatoId>A</fatoId>\r\n\t\t\t\t<availability>OPEN</availability>\r\n\t\t\t</Fato>\r\n\t\t\t<Fato>\r\n\t\t\t\t<fatoId>B</fatoId>\r\n\t\t\t\t<availability>CLOSE</availability>\r\n\t\t\t</Fato>\r\n\t\t</FatoAvailability>\r\n\t\t<ConfirmedReservations>\r\n\t\t\t<Resource>\r\n\t\t\t\t<resourceId>Std1</resourceId>\r\n\t\t\t\t<resourceType>Stand</resourceType>\r\n\t\t\t\t<Schedule> \r\n\t\t\t\t\t<gufi>urn:uuid:f81d4fae-7dec-11d0-a765-00a0c91e6bf56</gufi>\r\n\t\t\t\t\t<AircraftId>\r\n\t\t\t\t\t\t<callsign>KAL1234</callsign>\r\n\t\t\t\t\t\t<regNum>HL9553</regNum>\r\n\t\t\t\t\t\t<aircraftType>Joby_S4</aircraftType>\r\n\t\t\t\t\t</AircraftId>\r\n            \t\t<occupyStartTime>2023.04.06 13:00:00</occupyStartTime>\r\n            \t\t<occupyEndTime>2023.04.06 13:20:00</occupyEndTime>\r\n\t\t\t\t</Schedule>\r\n\t\t\t\t<Schedule> \r\n\t\t\t\t\t<gufi>urn:uuid:f81d4fae-7dec-11d0-a765-00a0c91e6bf57</gufi>\r\n\t\t\t\t\t<AircraftId>\r\n\t\t\t\t\t\t<callsign>KAL4321</callsign>\r\n\t\t\t\t\t\t<regNum>HL9560</regNum>\r\n\t\t\t\t\t\t<aircraftType>Joby_S4</aircraftType>\r\n\t\t\t\t\t</AircraftId>\r\n            \t\t<occupyStartTime>2023.04.06 14:10:00</occupyStartTime>\r\n            \t\t<occupyEndTime>2023.04.06 14:30:00</occupyEndTime>\r\n\t\t\t\t</Schedule>\t\r\n\t\t\t</Resource>\r\n\t\t\t<Resource>\r\n\t\t\t\t<resourceId>Std2</resourceId>\r\n\t\t\t\t<resourceType>Stand</resourceType>\r\n\t\t\t\t<Schedule> \r\n\t\t\t\t\t<gufi>urn:uuid:f81d4fae-7dec-11d0-a765-00a0c91esd31</gufi>\r\n\t\t\t\t\t<AircraftId>\r\n\t\t\t\t\t\t<callsign>CAP0013</callsign>\r\n\t\t\t\t\t\t<regNum>HL9555</regNum>\r\n\t\t\t\t\t\t<aircraftType>Joby_S4</aircraftType>\r\n\t\t\t\t\t</AircraftId>\r\n            \t\t<occupyStartTime>2023.04.06 15:00:00</occupyStartTime>\r\n            \t\t<occupyEndTime>2023.04.06 16:20:00</occupyEndTime>\r\n\t\t\t\t</Schedule>\r\n\t\t\t</Resource>\r\n\t\t</ConfirmedReservations>\r\n\t\t<ResourceAvailabilitySchedules>\r\n\t\t\t<Resource>\r\n\t\t\t\t<resourceId>Std2</resourceId>\r\n\t\t\t\t<resourceType>Stand</resourceType>\r\n\t\t\t\t<Availability>\r\n\t\t\t\t\t<aircraftClasses>A,B,C</aircraftClasses>\r\n\t\t\t\t\t<timeLower>2023.04.06 15:00:00</timeLower>\r\n            \t\t<timeUpper>2023.04.06 16:00:00</timeUpper>\r\n\t\t\t\t\t<capacity>3</capacity>\r\n\t\t\t\t\t<remainingSeats>1</remainingSeats>\r\n\t\t\t\t</Availability>\r\n\t\t\t\t<Availability>\r\n\t\t\t\t\t<aircraftClasses>A,B,C</aircraftClasses>\r\n\t\t\t\t\t<timeLower>2023.04.06 16:00:00</timeLower>\r\n            \t\t<timeUpper>2023.04.06 17:00:00</timeUpper>\r\n\t\t\t\t\t<capacity>3</capacity>\r\n\t\t\t\t\t<remainingSeats>3</remainingSeats>\r\n\t\t\t\t</Availability>\r\n\t\t\t\t<note></note>\r\n\t\t\t</Resource>\r\n\t\t</ResourceAvailabilitySchedules>\r\n\t</Body>\r\n</VertiportStatus>\r\n";

            byte[] buffer = Encoding.UTF8.GetBytes(message);

            // 멀티캐스트로 데이터(문자열)를 전송합니다.
            IPEndPoint endPoint = new IPEndPoint(multicastAddress, multicastPort);
            udpClient.Send(buffer, buffer.Length, endPoint);
            Console.WriteLine($"Sent message: {message}");

            IPEndPoint endPoint2 = new IPEndPoint(multicastAddress2 , multicastPort2);
            udpClient2.Send(buffer, buffer.Length, endPoint2);
            Console.WriteLine($"Sent message: {message}");
        }
        catch (Exception ex)
        {
            // 예외가 발생할 경우 콘솔에 출력합니다.
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // UdpClient 리소스를 해제합니다.
            udpClient.Close();
        }
    }
}