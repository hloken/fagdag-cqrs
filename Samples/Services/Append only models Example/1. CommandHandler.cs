namespace Services
{
    public class CommandHandler
    {
        // transaction is opened before method by the infrastructure
        public void Consume(ChangeMaritalStatus msg)
        {
            var emp = Session.Get<Employee>(msg.EmployeeId);
            emp.ChangeMaritalStatus(msg.NewMaritalStatus, msg.MaybeNewSurname);
        }
        /* 
        transaction is committed here and changes are flushed to the database
        by the infrastructure
        */
    }
}