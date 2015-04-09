using System.Data.Entity;
using System.Threading.Tasks;
using MetalPay.Payroll.Contracts.Commands;
using MetalPay.Payroll.Infrastructure;
using MetalPay.Payroll.Infrastructure.Commands;

namespace MetalPay.Payroll.UnionDues
{
    public class CreateUnionDuesHandler : ICommandHandler<CreateUnionDues>
    {
        public async Task Handle(CreateUnionDues command)
        {
            using (var context = new PayrollDbContext())
            {
                var paycode = await context.Paycodes.SingleAsync(x => x.Id == command.UnionDuesToCreate.PaycodeId && x.TenantId == command.TenantId);
                var unionDues = new Model.UnionDues(command.TenantId, command.UnionDuesId, command.UnionDuesToCreate, paycode);
                context.UnionDues.Add(unionDues);
                await context.SaveChangesAsync();
            }
        }
    }
}
