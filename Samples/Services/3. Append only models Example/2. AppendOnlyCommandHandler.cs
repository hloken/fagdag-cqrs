namespace Services
{
    public class AppendOnlyCommandHandler
    {
        // transaction is opened before method by the infrastructure
        public void Consume(ChangeMaritalStatus msg)
        {
            var employee = Session.GetLatest<AppendOnlyEmployee>(msg.EmployeeId);
            var newEmployeeVersion = employee.ChangeMaritalStatus(msg.NewMaritalStatus, msg.MaybeNewSurname);
            Session.Save(newEmployeeVersion);
        }
        /* 
        transaction is committed here and changes are flushed to the database
        by the infrastructure
        */ 
    }
}