using HanuMediSoftCore.Helpers;
using HanuMediSoftCore.Models.Hms.Op.Masters;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class ExpenseEntryService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public ExpenseEntryService(DatabaseHelper db)
        {
            _db = db;
        }


        //public List<Dictionary<string, object?>> SaveExpenseEntry(ExpenseDto dto)
        //{
        //    var hdr = dto.Header;

        //    // Convert Details list to JSON
        //    string json = JsonConvert.SerializeObject(dto.Details);

        //    // Parameters for SP
        //    var parameters = new Dictionary<string, object?>
        //{
        //    { "@ExpenseDate", hdr.ExpenseDate },
        //    { "@EmployeeID", hdr.EmployeeID },
        //    { "@TotalAmount", hdr.TotalAmount },
        //    { "@Remarks", hdr.Remarks },

        //    { "@CreatedByName", hdr.CreatedByName },
        //    { "@CreatedById", hdr.CreatedById },
        //    { "@WorkstationId", hdr.WorkstationId },

        //    { "@DetailsJson", json },
        //     { "@NewHeaderID", "" }


        //};
        //    SPResult result = _db.ExecuteSPWithOutput("SaveExpenseEntry", parameters);

        //    string newId = result.Output["@NewHeaderID"].ToString();

        //    // Call SP using your helper
        //    //DataTable dt = _db.ExecuteSPWithOutput("SaveExpenseEntry", parameters);

        //    //// Convert to List<Dictionary> using your helper
        //    //var result = _db.ToList(dt);

        //    return result;
        //}

        public List<Dictionary<string, object?>> SaveExpenseEntry(ExpenseDto dto)
        {
            var hdr = dto.Header;

            // Convert Details list to JSON
            string json = JsonConvert.SerializeObject(dto.Details);

            // Parameters for SP
            var parameters = new Dictionary<string, object?>
    {
        { "@ExpenseDate", hdr.ExpenseDate },
        { "@EmployeeID", "0" },
        { "@TotalAmount", hdr.TotalAmount },
        { "@Remarks", hdr.Remarks },

        { "@CreatedByName", hdr.CreatedByName },
        { "@CreatedById", hdr.CreatedById },
        { "@WorkstationId", hdr.WorkstationId },

        { "@DetailsJson", json },

        { "@NewHeaderID", "" }  // OUTPUT
    };

            // Execute SP with OUTPUT
            SPResult spResult = _db.ExecuteSPWithOutput("SaveExpenseEntry", parameters);

            // Read the new header ID
            string newId = spResult.Output["@NewHeaderID"]?.ToString() ?? "";

            // Convert DataTable to list using your helper
            var list = _db.ToList(spResult.Table);

            // Add new header id to result
            list.Add(new Dictionary<string, object?>
    {
        { "NewHeaderID", newId }
    });

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


