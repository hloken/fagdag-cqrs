namespace Services
{
    public class CommandHandler
    {
        // transaction is opened before method by the infrastructure
        public void Consume(ChangeMaritalStatus msg)
        {
            var employee = Session.Get<Employee>(msg.EmployeeId);
            employee.ChangeMaritalStatus(msg.NewMaritalStatus, msg.MaybeNewSurname);
        }
        /* 
        transaction is committed here and changes are flushed to the database
        by the infrastructure
        */
    }
}