using HanuMediSoftCore.Models.Navigation;
using Microsoft.Data.SqlClient;

namespace HanuMediSoftCore.Services.Hms
{
    public class NavigationService
    {
        private readonly IConfiguration _config;

        public NavigationService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<ModuleViewModel>> GetNavigationAsync()
        {
            var result = new List<ModuleViewModel>();

            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await con.OpenAsync();

                string query = @"
            SELECT ModuleId, ModuleName 
            FROM Modules 
            WHERE IsActive = 1 
            ORDER BY DisplayOrder;

            SELECT MenuId, MenuName, MenuIcon, ModuleId 
            FROM Menus 
            WHERE IsActive = 1 
            ORDER BY DisplayOrder;

            SELECT FormId, FormName, MenuId, PageUrl
            FROM Forms 
            WHERE IsActive = 1 
            ORDER BY DisplayOrder;
        ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    // ---------------------- MODULES -------------------------
                    var modules = new Dictionary<string, ModuleViewModel>();
                    while (await reader.ReadAsync())
                    {
                        string moduleId = reader.GetString(0);

                        modules[moduleId] = new ModuleViewModel
                        {
                            ModuleId = moduleId,
                            ModuleName = reader.GetString(1)
                        };
                    }

                    // ---------------------- MENUS ---------------------------
                    await reader.NextResultAsync();
                    var menus = new List<MenuViewModel>();

                    while (await reader.ReadAsync())
                    {
                        menus.Add(new MenuViewModel
                        {
                            MenuId = reader.GetString(0),
                            MenuName = reader.GetString(1),
                            MenuIcon = reader.GetString(2),
                            ModuleId = reader.GetString(3)
                        });
                    }

                    // ---------------------- FORMS ---------------------------
                    await reader.NextResultAsync();
                    var forms = new List<FormViewModel>();

                    while (await reader.ReadAsync())
                    {
                        forms.Add(new FormViewModel
                        {
                            FormId = reader.GetString(0),
                            FormName = reader.GetString(1),
                            MenuId = reader.GetString(2),
                            PageUrl = reader.GetString(3)
                        });
                    }

                    // ---------------------- ATTACH FORMS → MENUS -----------
                    foreach (var menu in menus)
                    {
                        menu.Forms = forms
                            .Where(f => f.MenuId == menu.MenuId)
                            .ToList();
                    }

                    // ---------------------- ATTACH MENUS → MODULES ---------
                    foreach (var module in modules.Values)
                    {
                        module.Menus = menus
                            .Where(m => m.ModuleId == module.ModuleId)
                            .ToList();

                        result.Add(module);
                    }
                }
            }

            return result;
        }

    }
}
