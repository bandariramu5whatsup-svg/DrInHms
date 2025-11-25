using System.Data;
using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace HanuMediSoftCore.Pages
{
    public class NavigationModel : PageModel
    {
        private readonly IConfiguration _config;

        public NavigationModel(IConfiguration config)
        {
            _config = config;
        }

        public List<ModuleDto> ModuleList { get; set; } = new();
        public List<MenuDto> MenuList { get; set; } = new();
        public List<FormDto> FormList { get; set; } = new();



        public void OnGet()
        {
            using var con = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            using var cmd = new SqlCommand("Sp_GetNavigationTree", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            using var reader = cmd.ExecuteReader();

            // TEMP DataTables (never exposed to Razor!)
            var dtModules = new DataTable();
            var dtMenus = new DataTable();
            var dtForms = new DataTable();

            dtModules.Load(reader);

            if (reader.NextResult())
                dtMenus.Load(reader);

            if (reader.NextResult())
                dtForms.Load(reader);

            // Convert Modules
            foreach (DataRow r in dtModules.Rows)
            {
                ModuleList.Add(new ModuleDto
                {
                    ModuleId = r["ModuleId"].ToString(),
                    ModuleName = r["ModuleName"].ToString()
                });
            }

            // Convert Menus
            foreach (DataRow r in dtMenus.Rows)
            {
                MenuList.Add(new MenuDto
                {
                    MenuId = r["MenuId"].ToString(),
                    MenuName = r["MenuName"].ToString(),
                    ModuleId = r["ModuleId"].ToString()
                });
            }

            // Convert Forms
            foreach (DataRow r in dtForms.Rows)
            {
                FormList.Add(new FormDto
                {
                    FormId = r["FormId"].ToString(),
                    FormName = r["FormName"].ToString(),
                    MenuId = r["MenuId"].ToString(),
                    PageUrl = r["PageUrl"].ToString()
                });
            }
        }
        }

}
