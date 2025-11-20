using HanuMediSoftCore.Helpers;
using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data;

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class ExpensesTypesService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public ExpensesTypesService(DatabaseHelper db)
        {
            _db = db;
        }

        public List<Dictionary<string, object?>> GetExpensesTypes(ExpensesType model)
        {
            var parameters = new Dictionary<string, object?>
                {
                    { "@ExpensesTypeId", model.ExpensesTypeId },
                    { "@ExpensesTypeName", model.ExpensesTypeName },
                    { "@IsActive", model.IsActive }
                };


            // Using helper class
            DataTable dt = _db.ExecuteSP(ExpensesTypesProcedures.Get, parameters);
            var list = _db.ToList(dt);
            return list;
        }

        public List<Dictionary<string, object?>> SaveExpensesTypes(ExpensesType model)
        {
            var parameters = new Dictionary<string, object?>
                        {
                            { "@ExpensesTypeId", model.ExpensesTypeId },
                            { "@ExpensesTypeName", model.ExpensesTypeName },
                            { "@ExpensesTypeCode", model.ExpensesTypeCode },
                            { "@IsActive", model.IsActive },

                            { "@UserId", model.CreatedById },
                            { "@UserName", model.CreatedByName },
                            { "@WorkstationId", model.WorkstationId }
                        };


            // Using helper class
            DataTable dt = _db.ExecuteSP(ExpensesTypesProcedures.Save, parameters);
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


