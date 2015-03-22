namespace Services
{
    public class AppendOnlyCommandHandler
    {
        // transaction is opened before method by the infrastructure
        public void Consume(ChangeMaritalStatus msg)
        {
            var emp = Session.GetLatest<AppendOnlyEmployee>(msg.EmployeeId);
            var newEmpVersion = emp.ChangeMaritalStatus(msg.NewMaritalStatus, msg.MaybeNewSurname);
            Session.Save(newEmpVersion);
        }
        /* 
        transaction is committed here and changes are flushed to the database
        by the infrastructure
        */ 
    }
}