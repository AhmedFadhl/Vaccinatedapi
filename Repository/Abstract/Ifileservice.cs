namespace Vaccinatedapi.Repository.Abstract
{
    public interface Ifileservice
    {
        public Tuple<int, string> savefile(IFormFile formFile);
        public bool Deletefile(string fileName);
    }
}
