using HanuMediSoftCore.Helpers; 
using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data; 

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class AreasService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public AreasService(DatabaseHelper db)
        {
            _db = db;
        }
        public List<Dictionary<string, object?>> GetAreas(Area model)
        {
                            var parameters = new Dictionary<string, object?>
                {
                    { "@AreaName", model.AreaName },
                    { "@AreaId", model.AreaId },
                    { "@DistrictId", model.DistrictId },
                    { "@StateId", model.StateId },
                    { "@CountryId", model.CountryId },
                    { "@IsActive", model.IsActive },
                    { "@IsDefaultArea", model.IsDefaultArea }
                };


            // Using helper class
            DataTable dt = _db.ExecuteSP(AreasProcedures.Get, parameters);
            var list = _db.ToList(dt);
            return list;
        }
        public List<Dictionary<string, object?>> SaveAreas(Area model)
        {
            var parameters = new Dictionary<string, object?>
                    {
                        { "@AreaId", model.AreaId }, // OUTPUT parameter
                        { "@AreaName", model.AreaName },
                        { "@AreaCode", model.AreaCode },
                        { "@IsActive", model.IsActive },
                        { "@IsDefaultArea", model.IsDefaultArea },
                        { "@CountryId", model.CountryId },
                        { "@StateId", model.StateId },
                        { "@DistrictId", model.DistrictId },

                        { "@CreatedByName", model.CreatedByName },
                        { "@CreatedById", model.CreatedById },
                        { "@WorkstationId", model.WorkstationId }
                    };



            // Using helper class
            DataTable dt = _db.ExecuteSP(AreasProcedures.Save, parameters);
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


