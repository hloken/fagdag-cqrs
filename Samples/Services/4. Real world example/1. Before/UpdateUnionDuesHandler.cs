using System.Data.Entity;
using System.Threading.Tasks;
using MetalPay.Payroll.Contracts.Commands;
using MetalPay.Payroll.Infrastructure;
using MetalPay.Payroll.Infrastructure.Commands;

namespace MetalPay.Payroll.UnionDues
{
    public class UpdateUnionDuesHandler : ICommandHandler<UpdateUnionDues>
    {
        public async Task Handle(UpdateUnionDues command)
        {
            using (var context = new PayrollDbContext())
            {
                var id = command.UnionDuesToUpdate.Id;
                var union = await context.UnionDues.SingleAsync(p => p.Id == id && p.TenantId == command.TenantId);
                var paycode = await context.Paycodes.SingleAsync(x => x.Id == command.UnionDuesToUpdate.PaycodeId && x.TenantId == command.TenantId);
                union.Update(command.UnionDuesToUpdate, paycode);
                await context.SaveChangesAsync();
            }
        }
    }
}
