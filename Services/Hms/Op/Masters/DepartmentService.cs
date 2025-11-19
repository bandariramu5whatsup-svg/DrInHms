using HanuMediSoftCore.Helpers;
using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data;

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class DepartmentService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public DepartmentService(DatabaseHelper db)
        {
            _db = db;
        }

        public List<Dictionary<string, object?>> GetDepartments(Department model)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "@DepartmentId ", model.DepartmentId  },
                { "@DepartmentName", model.DepartmentName },
                { "@IsActive", model.IsActive }
            };

            // Using helper class
            DataTable dt = _db.ExecuteSP(DepartProcedures.Get, parameters);
            var list = _db.ToList(dt);
            return list;
        }

        public List<Dictionary<string, object?>> SaveDepartments(Department model)
        {
            var parameters = new Dictionary<string, object?>
                    {
                        { "@DepartmentId", model.DepartmentId },
                        { "@DepartmentName", model.DepartmentName },
                        { "@DepartmentCode", model.DepartmentCode },
                        { "@IsActive", model.IsActive },
                        { "@UserName", model.CreatedByName },
                        { "@UserId", model.CreatedById },
                        { "@WorkstationId", model.WorkstationId }
                    };

            // Using helper class
            DataTable dt = _db.ExecuteSP(DepartProcedures.Save, parameters);
            var list = _db.ToList(dt);
            return list;
        }
    }
}

//using HanuMediSoftCore.Helpers;
//using HanuMediSoftCore.Models.Hms.Op.Masters;
//using System.Data; 
//using static HanuMediSoftCore.Helpers.Helpers;

//namespace HanuMediSoftCore.Services.Hms.Op.Masters
//{

//    public class UnitsService(DatabaseHelper db)


//    {

//        public static string ConnStr => ConnectionHelper.ConnectionString;

//        public static DataTable GetUnitsService(Unit model)

//        {

//             var parameters = new Dictionary<string, object?>
//                {
//                    { "@UnitId", model.UnitId },
//                    { "@UnitName", model.UnitName },
//                    { "@IsActive", model.IsActive },
//                    //{ "@PageIndex", model.PageIndex },
//                    //{ "@PageSize", model.PageSize }
//                };
//            DataTable dt = ExecuteSP(ConnStr, "SpOpGetUnits", parameters);
//            return dt;
//        }
//    }
//}


