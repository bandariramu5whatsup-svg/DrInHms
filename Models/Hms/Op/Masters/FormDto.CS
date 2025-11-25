namespace HanuMediSoftCore.Models.Hms.Op.Masters
{
    public class FormDto
    {
        public string FormId { get; set; }
        public string FormName { get; set; }
        public string MenuId { get; set; }
        public string PageUrl { get; set; }
    }
    public class ModuleDto
    {
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
    }

    public class MenuDto
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string ModuleId { get; set; }
    }

}
