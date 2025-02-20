using FinalProject.DAL.Entities;

namespace FinalProject.Service.ViewModels
{
    public class SettingVM
    {
        public Setting Setting { get; set; }
        public List<Blog> Blogs { get; set; }
        public AppUser? AppUser { get; set; }
        public IEnumerable<CarImage> CarImages { get; set; }
        public IEnumerable<Message> Messages { get; set; }

    }
}
