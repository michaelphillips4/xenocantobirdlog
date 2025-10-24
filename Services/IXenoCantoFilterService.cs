using XenoCanto.Models;

namespace Services
{
	public interface IXenoCantoFilterService
	{
		string? errorString { get; set; }
		Data? InitialData { get; set; }
		FilterModes ModeFilter { get; set; }
		string NameFilter { get; set; }
		IQueryable<Recording>? Recordings { get; set; }
	}
}