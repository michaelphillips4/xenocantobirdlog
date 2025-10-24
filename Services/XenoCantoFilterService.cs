using XenoCanto.Models;

namespace Services
{
	public enum FilterModes
	{
		All,
		Animals,
		Birds
	}
	public class XenoCantoFilterService : IXenoCantoFilterService
	{
		private Data? initialData { get; set; }
		public Data? InitialData
		{
			get { return initialData; }
			set
			{
				initialData = value;
				Recordings = initialData?.Recordings?.AsQueryable();
			}
		}
		public IQueryable<Recording>? Recordings { get; set; }
		public string? errorString { get; set; } = null;
		public string NameFilter { get; set; } = "";

		private FilterModes _modeFilter = FilterModes.All;
		public FilterModes ModeFilter
		{
			get { return _modeFilter; }
			set
			{
				_modeFilter = value;

				filters();
			}
		}

		private void filters()
		{
			if (Recordings != null)
			{
				if (string.IsNullOrWhiteSpace(NameFilter) || ModeFilter == FilterModes.All)
				{
					Recordings = initialData?.Recordings?.AsQueryable();
				}

				if (ModeFilter != FilterModes.All)
				{
					Recordings = Recordings?
					.Where(item => item.grp == (ModeFilter == FilterModes.Animals ? "land mammals" : "birds"));
				}

				if (!string.IsNullOrWhiteSpace(NameFilter))
				{
					Recordings = Recordings?
					.Where(
						item => item.en.Contains(NameFilter, StringComparison.OrdinalIgnoreCase)
					);
				}

			}

		}

	}


}
