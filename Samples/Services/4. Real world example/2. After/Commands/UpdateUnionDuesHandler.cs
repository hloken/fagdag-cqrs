using System.Data.Entity;
using System.Threading.Tasks;
using MetalPay.Payroll.Infrastructure.Commands;
using MetalPay.Payroll.SalaryCalculation.Commands;
using NServiceBus;

namespace MetalPay.Payroll.SalaryCalculation.CommandHandlers
{
    public class UpdateUnionDuesHandler : ICommandHandler<UpdateUnionDues>
    {
        private readonly SalaryCalculationDbContext _salaryCalculationDbContext;
        private readonly IBus _bus;

        public UpdateUnionDuesHandler(SalaryCalculationDbContext salaryCalculationDbContext, IBus bus)
        {
            _salaryCalculationDbContext = salaryCalculationDbContext;
            _bus = bus;
        }

        public async Task Handle(UpdateUnionDues command)
        {
            var union = await _salaryCalculationDbContext.UnionDues.SingleAsync(p => p.Id == command.Id && p.TenantId == command.TenantId);
            union.Update(command.PaycodeId, command.Name, command.UseAmount, command.DuesDeductionRate, command.DuesDeductionAmount, command.MaximumDueAmount, command.MinimumDueAmount, _bus);
            await _salaryCalculationDbContext.SaveChangesAsync();
        }
    }
}
