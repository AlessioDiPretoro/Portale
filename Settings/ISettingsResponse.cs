namespace Portale.Settings
{
	public class ISettingsResponse
	{
		public string ErrorMessage { get; set; }
		public string SuccessMessage { get; set; }
		public bool IsSuccess { get; set; }
	}
}