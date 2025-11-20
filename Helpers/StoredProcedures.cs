namespace HanuMediSoftCore.Helpers
{
    public class StoredProcedures
    {
        public const string SpOpGetServiceTypes = "SpOpGetServiceTypes";
        public const string SpOpInsertServiceType = "SpOpInsertServiceType";
        public const string SpOpGetUnits = "SpOpGetUnits";
        public const string SpOpInsertUnit = "SpOpInsertUnit";
    }


    public class UnitsProcedures
    {
        public const string Get = "SpOpGetUnits";
        public const string Save = "SpOpInsertUnit";
    } 
    
    public class CountriesProcedures
    {
        public const string Get = "SpOpGetCountries";
        public const string Save = "SpOpInsertCountry";
        public const string DdGet = "SpOpDdGetCountries";
    }
    public class StatesProcedures
    {
        public const string Get = "SpOpGetStates";
        public const string Save = "SpOpInsertState";
        public const string DdGet = "SpOpDdGetStates";
    } 
    
    public class DistrictsProcedures
    {
        public const string Get = "SpOpGetDistricts";
        public const string Save = "SpOpInsertDistrict";
    }

    public class AreasProcedures
    {
        public const string Get = "SpOpGetAreas";
        public const string Save = "SpOpInsertAreas";
    }
     public class PlacesProcedures
    {
        public const string Get = "SpOpGetPlace";
        public const string Save = "SpOpInsertPlace";
    }

    public class DepartProcedures
    {
        public const string Get = "SpOpGetDepartments";
        public const string Save = "SpOpInsertDepartment";
    }
    public class SpecilizationProcedures
    {
        public const string Get = "SpOpGetSpecialization";
        public const string Save = "SpOpInsertSpecialization";
    }

    public class ExpensesPaidToProcedures
    {
        public const string Get = "SpOpGetExpensesPaidTo";
        public const string Save = "SpOpInsertExpensesPaidTo";
        public const string DdGet = "SpOpDdGetPaidTo";
    }

    public class ExpensesPurposeProcedures
    {
        public const string Get = "SpOpGetExpensesPurpose";
        public const string Save = "SpOpInsertExpensesPurpose";
        public const string DdGet = "SpOpDdGetExpensesPurpose";

    }

    public class ExpensesTypesProcedures
    {
        public const string Get = "SpOpGetExpensesTypes";
        public const string Save = "SpOpInsertExpensesTypes";
        public const string DdGet = "SpOpDdGetExpensesTypes";
    }


}
