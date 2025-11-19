using HanuMediSoftCore.Helpers;
using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data;

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class SpecilizationService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public SpecilizationService(DatabaseHelper db)
        {
            _db = db;
        }

        public List<Dictionary<string, object?>> GetSpecilizations(Specialization model)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "@SpecializationId ", model.SpecializationId  },
                { "@SpecializationName", model.SpecializationName },
                { "@IsActive", model.IsActive }
            };

            // Using helper class
            DataTable dt = _db.ExecuteSP(DepartProcedures.Get, parameters);
            var list = _db.ToList(dt);
            return list;
        }

        public List<Dictionary<string, object?>> SaveSpecilizations(Specialization model)
        {
            var parameters = new Dictionary<string, object?>
                    {
                        { "@SpecializationId", model.SpecializationId },
                        { "@SpecializationName", model.SpecializationName },
                        { "@SpecializationCode", model.SpecializationCode },
                        { "@DepartmentId", model.DepartmentId },
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


