using Crawl.Models;

namespace Crawl.ViewModels
{
    public class MonsterDetailViewModel : BaseViewModel
    {
        public Monster Data { get; set; }
        public MonsterDetailViewModel(Monster data = null)
        {
            Title = data?.Name;
            Data = data;
        }
    }
}
