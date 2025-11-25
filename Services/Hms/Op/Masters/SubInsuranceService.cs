using HanuMediSoftCore.Helpers;
using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data;

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class SubInsuranceService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public SubInsuranceService(DatabaseHelper db)
        {
            _db = db;
        }

        public List<Dictionary<string, object?>> GetSubInsurance(SubInsurance model)
        {
            var parameters = new Dictionary<string, object?>
                {
                     { "@SubInsuranceId", model.SubInsuranceId },
                     { "@SubInsuranceName", model.SubInsuranceName },
                     { "@InsuranceId", model.InsuranceId },
                     { "@IsActive", model.IsActive }
                };


            // Using helper class
            DataTable dt = _db.ExecuteSP(SubInsuranceProcedures.Get, parameters);
            var list = _db.ToList(dt);
            return list;
        }

        public List<Dictionary<string, object?>> SaveSubInsurance(SubInsurance model)
        {
            var parameters = new Dictionary<string, object?>
                        {
                            { "@SubInsuranceId", model.SubInsuranceId },
                            { "@SubInsuranceName", model.SubInsuranceName },
                            { "@SubInsuranceCode", model.SubInsuranceCode },
                            { "@InsuranceId", model.InsuranceId },
                            { "@IsActive", model.IsActive },

                            { "@UserId", model.CreatedById },
                            { "@UserName", model.CreatedByName },
                            { "@WorkstationId", model.WorkstationId }
                        };


            // Using helper class
            DataTable dt = _db.ExecuteSP(SubInsuranceProcedures.Save, parameters);
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


