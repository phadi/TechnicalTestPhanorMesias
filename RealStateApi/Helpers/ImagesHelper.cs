namespace RealStateApi.Helpers
{
    public static class ImagesHelper
    {
        public static async Task<string> SaveImage(IFormFile file, int id)
        {
            try
            {
                string directorioActual = System.IO.Directory.GetCurrentDirectory();
                var filePath = Path.Combine(directorioActual, "Images", $"{id.ToString()}_{file.FileName}");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return string.Empty;

            }catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
