using HanuMediSoftCore.Helpers; 
using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data; 

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class PlacesService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public PlacesService(DatabaseHelper db)
        {
            _db = db;
        }
        public List<Dictionary<string, object?>> GetPlaces(Place model)
        {
                            var parameters = new Dictionary<string, object?>
                {
                    { "@PlaceId", model.PlaceId },
                    { "@PlaceName", model.PlaceName },
                    { "@IsActive", model.IsActive },
                    { "@IsDefaultPlace", model.IsDefaultPlace },
                    { "@AreaId", model.AreaId },
                    { "@DistrictId", model.DistrictId },
                    { "@StateId", model.StateId },
                    { "@CountryId", model.CountryId }
                };


            // Using helper class
            DataTable dt = _db.ExecuteSP(PlacesProcedures.Get, parameters);
            var list = _db.ToList(dt);
            return list;
        }
        public List<Dictionary<string, object?>> SavePlaces(Place model)
        {
            var parameters = new Dictionary<string, object?>
                    {
                       { "@PlaceId", model.PlaceId }, // OUTPUT
                        { "@PlaceName", model.PlaceName },
                        { "@PlaceCode", model.PlaceCode },
                        { "@IsActive", model.IsActive },
                        { "@IsDefaultPlace", model.IsDefaultPlace },
                        { "@AreaId", model.AreaId },
                        { "@DistrictId", model.DistrictId },
                        { "@StateId", model.StateId },
                        { "@CountryId", model.CountryId },

                        { "@UserId", model.CreatedById },
                        { "@UserName", model.CreatedByName },
                        { "@WorkstationId", model.WorkstationId }
                    };



            // Using helper class
            DataTable dt = _db.ExecuteSP(PlacesProcedures.Save, parameters);
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


