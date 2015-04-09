using System.Data.Entity;
using System.Threading.Tasks;
using MetalPay.Payroll.Infrastructure.Commands;
using MetalPay.Payroll.SalaryCalculation.Commands;

namespace MetalPay.Payroll.SalaryCalculation.CommandHandlers
{
    public class CreateUnionDuesHandler : ICommandHandler<CreateUnionDues>
    {
        private readonly SalaryCalculationDbContext _salaryCalculationDbContext;

        public CreateUnionDuesHandler(SalaryCalculationDbContext salaryCalculationDbContext)
        {
            _salaryCalculationDbContext = salaryCalculationDbContext;
        }

        public async Task Handle(CreateUnionDues command)
        {
            var company = await _salaryCalculationDbContext.Companies.Include(c => c.Paycodes).SingleAsync(c => c.TenantId == command.TenantId && c.Id == command.CompanyId);
            var unionDuesToCreate = command.UnionDuesToCreate;
            company.AddUnionDues(command.UnionDuesId, unionDuesToCreate.Name, unionDuesToCreate.UseAmount, unionDuesToCreate.DuesDeductionRate, unionDuesToCreate.DuesDeductionAmount, unionDuesToCreate.MaximumDueAmount, unionDuesToCreate.MinimumDueAmount, unionDuesToCreate.PaycodeId.Value);
            await _salaryCalculationDbContext.SaveChangesAsync();
        }
    }
}
