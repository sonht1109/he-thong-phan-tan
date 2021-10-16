using System;
using System.Net;
using System.Net.Sockets;
using System.Text;  

namespace SocketServer
{
	class Program
	{		
		static void Main(string[] args)
		{
			try
			{
				int ServerPort = ConfigApp.ServerPort;
				string ServerIp = ConfigApp.ServerIp;
				IPAddress ipAddress = IPAddress.Parse(ServerIp);
				IPEndPoint localEndPoint = new IPEndPoint(ipAddress, ServerPort);
				Console.WriteLine("End service point is " + ipAddress.ToString()+ ":" + ServerPort.ToString());
				// Tạo Socket sử dụng giao thức Tcp      
				Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				// Nhúng Socket vào điểm cuối  
				listener.Bind(localEndPoint);
				// Qui định số lượng tối Socket máy chủ có thể nhận 
				listener.Listen(50);
				int Count = 0;
				while (Count<500)
				{					
					Console.WriteLine("Waiting for a connection...");
					Socket handler = listener.Accept();
					// Thông điệp từ máy khách    
					string data = null;
					byte[] bytes = null;
					while (true)
					{
						bytes = new byte[1024];
						int bytesRec = handler.Receive(bytes);
						data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
						int PosEof = data.IndexOf("<EOF>");
						if (PosEof > -1)
						{
							data = data.Substring(0, PosEof);
							break;
						}
					}
					Count++;
					Console.WriteLine(Count.ToString()+": Message received from " + handler.RemoteEndPoint.ToString() + " ==>> " + data);
					byte[] msg = Encoding.ASCII.GetBytes("Hello client, this is received message: " + data);
					handler.Send(msg);
					handler.Shutdown(SocketShutdown.Both);
					handler.Close();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}			
		}	         
	}
}
