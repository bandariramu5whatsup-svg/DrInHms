using HanuMediSoftCore.Helpers;
using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data;

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class ExpensesPurposeService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public ExpensesPurposeService(DatabaseHelper db)
        {
            _db = db;
        }

        public List<Dictionary<string, object?>> GetExpensesPurpose(ExpensesPurpose model)
        {
            var parameters = new Dictionary<string, object?>
                {
                    { "@ExpensesPurposeId", model.ExpensesPurposeId },
                    { "@ExpensesPurposeName", model.ExpensesPurposeName },
                    { "@IsActive", model.IsActive }
                };


            // Using helper class
            DataTable dt = _db.ExecuteSP(ExpensesPurposeProcedures.Get, parameters);
            var list = _db.ToList(dt);
            return list;
        }

        public List<Dictionary<string, object?>> SaveExpensesPurpose(ExpensesPurpose model)
        {
            var parameters = new Dictionary<string, object?>
                        {
                            { "@ExpensesPurposeId", model.ExpensesPurposeId },
                            { "@ExpensesPurposeName", model.ExpensesPurposeName },
                            { "@ExpensesPurposeCode", model.ExpensesPurposeCode },
                            { "@IsActive", model.IsActive },

                            { "@UserId", model.CreatedById },
                            { "@UserName", model.CreatedByName },
                            { "@WorkstationId", model.WorkstationId }
                        };


            // Using helper class
            DataTable dt = _db.ExecuteSP(ExpensesPurposeProcedures.Save, parameters);
            var list = _db.ToList(dt);
            return list;
        }

        public List<Dictionary<string, object?>> FillExpensesPurpose(ExpensesPurpose model)
        {
            var parameters = new Dictionary<string, object?>
            {

            };


            // Using helper class
            DataTable dt = _db.ExecuteSP(ExpensesPurposeProcedures.DdGet, parameters);
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


