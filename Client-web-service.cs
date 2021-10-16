using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWebService
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				//Khởi tạo đối tượng SampleWebServices
				SampleWebServices m_SampleWebServices = new SampleWebServices();
				//Gọi hàm
				Console.WriteLine(m_SampleWebServices.Hello("Your name"));
				string FormatDateTime = "dd/MM/yyyy HH:mm:ss.fff";
				//Lấy thời gian địa phương trên máy chủ
				Console.WriteLine("Server local time: "+ m_SampleWebServices.GetLocalDateTime().ToString(FormatDateTime));
				//Lấy thời gian UTC trên máy chủ
				Console.WriteLine("Server utc time: " + m_SampleWebServices.GetUtcDateTime().ToString(FormatDateTime));
				//Tính độ lêch thời gian giữa máy khách và máy chủ (tick) và độ trễ toàn phần
				long ClientLocalDateTimeTicks=System.DateTime.Now.Ticks;
				long LocalDiffernceTicks = m_SampleWebServices.GetLocalDiffernceTicks(ClientLocalDateTimeTicks);
				long RoundTripDelayTick = System.DateTime.Now.Ticks - ClientLocalDateTimeTicks;
				Console.WriteLine("Different between Client and Server in ticks: " + LocalDiffernceTicks.ToString() + " in ms: " + (LocalDiffernceTicks / 10000).ToString());
				Console.WriteLine("Round trip delay in ticks: " + RoundTripDelayTick.ToString() + " in ms: " + (RoundTripDelayTick / 10000).ToString());

				long ClientUtcDateTimeTicks = System.DateTime.Now.ToUniversalTime().Ticks;
				long UtcDiffernceTicks = m_SampleWebServices.GetUtcDiffernceTicks(ClientUtcDateTimeTicks);
				RoundTripDelayTick = System.DateTime.Now.ToUniversalTime().Ticks - ClientUtcDateTimeTicks;
				Console.WriteLine("Different  between Client and Server in ticks: " + UtcDiffernceTicks.ToString() + " in ms: " + (UtcDiffernceTicks / 10000).ToString());
				Console.WriteLine("Round trip delay in ticks: " + RoundTripDelayTick.ToString() + " in ms: " + (RoundTripDelayTick / 10000).ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
			Console.WriteLine("\n Press any key to end...");
			Console.ReadKey();
		}
	}
}
