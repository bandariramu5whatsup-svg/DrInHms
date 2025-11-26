namespace HanuMediSoftCore.Models.Navigation
{
    public class ModuleViewModel
    {
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public List<MenuViewModel> Menus { get; set; } = new();
    }
}
