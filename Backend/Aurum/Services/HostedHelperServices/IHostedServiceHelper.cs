namespace Aurum.Services.HostedHelperServices;

public interface IHostedServiceHelper
{
	Task<bool> ProcessRegularToNormal();
}
