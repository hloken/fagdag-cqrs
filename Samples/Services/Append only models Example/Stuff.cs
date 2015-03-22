using System;

namespace Services
{
    #region don't look too ugly!
    public class Employee
    {
        public void ChangeMaritalStatus(MaritalStatus newMaritalStatus, string maybeNewSurname)
        {
        }
    }

    public class AppendOnlyEmployee
    {
        public object ChangeMaritalStatus(MaritalStatus newMaritalStatus, string maybeNewSurname)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MaritalStatus
    {
    }

    public class Session
    {
        public static T Get<T>(object employeeId)
        {
            throw new System.NotImplementedException();
        }

        public static T GetLatest<T>(object employeeId)
        {
            throw new NotImplementedException();
        }

        public static void Save(object newEmpVersion)
        {
            throw new NotImplementedException();
        }
    }

    public class ChangeMaritalStatus
    {
        public object EmployeeId { get; set; }
        public MaritalStatus NewMaritalStatus { get; set; }
        public string MaybeNewSurname { get; set; }
    }
#endregion
}
