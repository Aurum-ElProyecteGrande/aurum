using Aurum.Services.HostedHelperServices;
using Aurum.Services.RegularExpenseService;
using Aurum.Services.RegularIncomeServices;

namespace Aurum.HostedServices;

public class DailyRegularService(IHostedServiceHelper hostedServiceHelper, ILogger<DailyRegularService> logger) : BackgroundService
{

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			// var scheduledTime = new TimeSpan(2, 0, 0);
			var scheduledTime =DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(3));
			var timeToWait = scheduledTime - DateTime.Now.TimeOfDay;

			if (timeToWait < TimeSpan.Zero)
				timeToWait = timeToWait.Add(TimeSpan.FromDays(1));

			await Task.Delay(timeToWait, stoppingToken);

			var didComplete = await PerformTaskAsync();
			var status = didComplete ? "Successful" : "Failed";

			logger.LogInformation($"{status} addition of regular expenses and incomes.");

			await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
		}
	}

	private async Task<bool> PerformTaskAsync() =>
		await hostedServiceHelper.ProcessRegularToNormal();



}
