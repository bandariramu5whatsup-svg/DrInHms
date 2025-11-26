namespace HanuMediSoftCore.Models.Navigation
{
    public class MenuViewModel
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public string ModuleId { get; set; }   // NECESSARY

        public List<FormViewModel> Forms { get; set; } = new();
    }
}
