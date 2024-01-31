using Azure;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Portale.Settings
{
	public class UploadImg
	{
		public static List<string> AcceptedFile
		{
			get
			{
				List<string> strings = new List<string>
				{
					".jpeg",
					".jpg",
					".png"
				};

				return strings;
			}
		}

		public class ImageUploadModel
		{
			public IFormFile Image { get; set; }
		}

		public static async Task<ISettingsResponse> UploadImage(IFormFile img, string folder)
		{
			ISettingsResponse settingsResponse = new ISettingsResponse();
			string fileNameOriginal = img.FileName;
			string ext = Path.GetExtension(img.FileName);
			if (AcceptedFile.Contains(ext))
			{
				var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), folder);
				if (!Directory.Exists(uploadsFolderPath))
				{
					Directory.CreateDirectory(uploadsFolderPath);
				}

				var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(img.FileName)}";
				var filePath = Path.Combine(uploadsFolderPath, fileName);

				using (var imgLow = Image.Load(img.OpenReadStream()))
				{
					imgLow.Mutate(x => x.Resize(new ResizeOptions
					{
						Size = new Size(350, 0),
						Mode = ResizeMode.Max
					}));
					var encoder = new JpegEncoder { Quality = 30 };
					imgLow.Save(filePath, encoder);

					imgLow.Save(filePath);
				}
				settingsResponse.SuccessMessage = "Img succesufully saved as: " + fileName;
				settingsResponse.IsSuccess = true;
			}
			else
			{
				settingsResponse.ErrorMessage = "File image not supported";
				settingsResponse.IsSuccess = false;
			}
			return settingsResponse;
		}
	}
}