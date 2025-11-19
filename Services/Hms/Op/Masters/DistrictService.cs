using HanuMediSoftCore.Helpers; 
using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data; 

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class DistrictService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public DistrictService(DatabaseHelper db)
        {
            _db = db;
        }
        public List<Dictionary<string, object?>> GetDistricts(District model)
        {
            var parameters = new Dictionary<string, object?>
                    {
                        { "@DistrictId", model.DistrictId },
                        { "@DistrictName", model.DistrictName },
                        { "@IsActive", model.IsActive },
                        { "@IsDefaultDistrict", model.IsDefaultDistrict },
                        { "@StateId", model.StateId },
                        { "@CountryId", model.CountryId }
                    };

            // Using helper class
            DataTable dt = _db.ExecuteSP(DistrictsProcedures.Get, parameters);
            var list = _db.ToList(dt);
            return list;
        }
        public List<Dictionary<string, object?>> SaveDistricts(District model)
        {
            var parameters = new Dictionary<string, object?>
                {
                    { "@DistrictId", model.DistrictId }, // OUTPUT
                    { "@DistrictName", model.DistrictName },
                    { "@DistrictCode", model.DistrictCode },
                    { "@IsActive", model.IsActive },
                    { "@IsDefaultDistrict", model.IsDefaultDistrict },
                    { "@CountryId", model.CountryId },
                    { "@StateId", model.StateId },
                    { "@StateName", model.StateName },

                    { "@UserId", model.CreatedById },
                    { "@UserName", model.CreatedByName },
                    { "@WorkstationId", model.WorkstationId }
                };


            // Using helper class
            DataTable dt = _db.ExecuteSP(DistrictsProcedures.Save, parameters);
            var list = _db.ToList(dt);
            return list;
        }
        public List<Dictionary<string, object?>> FillStates(State model)
        {
            var parameters = new Dictionary<string, object?>
                {
                    
                };


            // Using helper class
            DataTable dt = _db.ExecuteSP(StatesProcedures.DdGet, parameters);
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


