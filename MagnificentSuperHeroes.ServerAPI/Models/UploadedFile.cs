namespace MagnificentSuperHeroes.ServerAPI.Models
{
    public class UploadedFile
    {
        public int SuperHeroId { get; set; }

        public string FileName { get; set;}

        public byte[] FileContent { get; set; }

    }
}
