using HanuMediSoftCore.Helpers;
using HanuMediSoftCore.Models.Hms.Op.Masters;
using System.Data;

namespace HanuMediSoftCore.Services.Hms.Op.Masters
{
    public class ExpensesPaidToService
    {
        private readonly DatabaseHelper _db;

        // Constructor injection
        public ExpensesPaidToService(DatabaseHelper db)
        {
            _db = db;
        }

        public List<Dictionary<string, object?>> GetExpensesPaidTo(ExpensesPaidTo model)
        {
            var parameters = new Dictionary<string, object?>
                {
                    { "@ExpensesPaidToId", model.ExpensesPaidToId },
                    { "@ExpensesPaidToName", model.ExpensesPaidToName },
                    { "@IsActive", model.IsActive }
                };


            // Using helper class
            DataTable dt = _db.ExecuteSP(CountriesProcedures.Get, parameters);
            var list = _db.ToList(dt);
            return list;
        }

        public List<Dictionary<string, object?>> SaveExpensesPaidTo(ExpensesPaidTo model)
        {
            var parameters = new Dictionary<string, object?>
                        {
                            { "@ExpensesPaidToId", model.ExpensesPaidToId },
                            { "@ExpensesPaidToName", model.ExpensesPaidToName },
                            { "@ExpensesPaidToCode", model.ExpensesPaidToCode },
                            { "@IsActive", model.IsActive },

                            { "@UserId", model.CreatedById },
                            { "@UserName", model.CreatedByName },
                            { "@WorkstationId", model.WorkstationId }
                        };


            // Using helper class
            DataTable dt = _db.ExecuteSP(CountriesProcedures.Save, parameters);
            var list = _db.ToList(dt);
            return list;
        }
    }
}

