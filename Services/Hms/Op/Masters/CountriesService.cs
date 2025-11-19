using HanuMediSoftCore.Helpers;
using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data;

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class CountriesService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public CountriesService(DatabaseHelper db)
        {
            _db = db;
        }

        public List<Dictionary<string, object?>> GetCountries(Country model)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "@CountryId", model.CountryId },
                { "@CountryName", model.CountryName },
                { "@IsActive", model.IsActive },
                { "@IsDefaultCountry", model.IsDefaultCountry }
            };

            // Using helper class
            DataTable dt = _db.ExecuteSP(CountriesProcedures.Get, parameters);
            var list = _db.ToList(dt);
            return list;
        }

        public List<Dictionary<string, object?>> SaveCountries(Country model)
        {
            var parameters = new Dictionary<string, object?>
                    {
                        { "@CountryId", model.CountryId },
                        { "@CountryName", model.CountryName },
                        { "@CountryCode", model.CountryCode },
                        { "@IsActive", model.IsActive },
                        { "@IsDefaultCountry", model.IsDefaultCountry },
                        { "@UserName", model.CreatedByName },
                        { "@UserId", model.CreatedById },
                        { "@WorkstationId", model.WorkstationId }
                    };

            // Using helper class
            DataTable dt = _db.ExecuteSP(CountriesProcedures.Save, parameters);
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


