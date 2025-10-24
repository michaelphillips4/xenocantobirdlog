using XenoCanto.Models;

namespace Services
{
	public interface IXenoCantoService
	{
		Task<Data> GetAsync();
	}
}