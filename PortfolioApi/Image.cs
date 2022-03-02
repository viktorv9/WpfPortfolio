namespace PortfolioApi;

public class Image
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string LinkURL { get; set; }

    public string[] Tags { get; set; }
    
    class ImageDb : DbContext
    {
        public ImageDb(DbContextOptions options) : base(options) { }
        public DbSet<Image> Images { get; set; }
    }
}
