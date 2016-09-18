using System;
using System.ServiceModel;

namespace TechSupport
{
	public class TechSupportServer
	{
		public static TechSupportService1Client Client= new TechSupportService1Client(new BasicHttpBinding(BasicHttpSecurityMode.Transport),
		                                                                                      new EndpointAddress("https://techsupportserver.azurewebsites.net/TechSupportService.svc"));
		
		public TechSupportServer()
		{
			
		}
	}
}
