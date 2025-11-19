using HanuMediSoftCore.Helpers; 
using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data; 

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class StatesService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public StatesService(DatabaseHelper db)
        {
            _db = db;
        }
        public List<Dictionary<string, object?>> GetStates(State model)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "@StateId", model.StateId },
                { "@StateName", model.@StateName },
                { "@IsActive", model.IsActive },
                { "@IsDefaultState", model.IsDefaultState },
                { "@CountryId", model.@CountryId }
            };

            // Using helper class
            DataTable dt = _db.ExecuteSP(StatesProcedures.Get, parameters);
            var list = _db.ToList(dt);
            return list;
        }
        public List<Dictionary<string, object?>> SaveStates(State model)
        {
            var parameters = new Dictionary<string, object?>
                    {
                        { "@StateId", model.StateId },
                        { "@StateName", model.StateName  },
                        { "@StateCode", model.StateCode },
                        { "@IsActive", model.IsActive },
                        { "@IsDefaultState", model.IsDefaultState },
                        { "@CountryId", model.CountryId },
                        { "@CountryName", model.CountryName },


                        { "@UserName", model.CreatedByName },
                        { "@UserId", model.CreatedById },
                        { "@WorkstationId", model.WorkstationId }
                    };

            // Using helper class
            DataTable dt = _db.ExecuteSP(StatesProcedures.Save, parameters);
            var list = _db.ToList(dt);
            return list;
        }

        public List<Dictionary<string, object?>> FillCountries(State model)
        {
            var parameters = new Dictionary<string, object?>
            {
                
            };

            // Using helper class
            DataTable dt = _db.ExecuteSP(CountriesProcedures.DdGet, parameters);
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


