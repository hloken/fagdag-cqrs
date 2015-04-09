using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MetalPay.Payroll.Contracts.Commands;
using MetalPay.Payroll.Contracts.Model;
using MetalPay.Payroll.Infrastructure;
using MetalPay.Payroll.Infrastructure.Auth;
using Nancy;
using Nancy.ModelBinding;

namespace MetalPay.Payroll.Api
{
    public class UnionModule : NancyModule
    {
        public UnionModule(ICommandExecutor commandExecutor)
            : base("api/union")
        {
            Get["", true] = async (p, ct) =>
            {
                string tenantId = Context.GetTenantId();

                using (var context = new PayrollDbContext())
                {
                    var unions = await context.UnionDues.Include(x => x.Paycode)
                        .Where(x => x.TenantId == tenantId)
                        .ToArrayAsync();
                    var apiUnion = unions.Select(x => new Model.Union
                    {
                        Id = x.Id,
                        Name = x.Name,
                        DuesDeductionRate = x.DuesDeductionRate,
                        DuesDeductionAmount = x.DuesDeductionAmount,
                        MaximumDueAmount = x.MaximumDueAmount,
                        MinimumDueAmount = x.MinimumDueAmount,
                        PaycodeId = x.Paycode.Id
                    });
                    return Response.AsJson(apiUnion);
                }
            };

            Get["/{unionId}", true] = async (y, ct) =>
            {
                string tenantId = Context.GetTenantId();
                var person = await GetUnion((Guid)y.unionId, tenantId);
                return Response.AsJson(person);
            };

            Put["/{unionId}", true] = async (p, ct) =>
            {
                var incomingUnion = this.Bind<UnionDuesToUpdate>();
                
                var tenantId = Context.GetTenantId();
                await commandExecutor.Execute(new UpdateUnionDues(tenantId, incomingUnion));
                return HttpStatusCode.NoContent;
            };

            Post["", true] = async (p, ct) =>
            {
                var incomingUnion = this.Bind<UnionDuesToCreate>();
                
                var tenantId = Context.GetTenantId();
                var id = Guid.NewGuid();
                await commandExecutor.Execute(new CreateUnionDues(tenantId, id, incomingUnion));
                var model = await GetUnion(id, tenantId);
                return Response.AsJson(model);
            };
        }

        private async Task<Model.Union> GetUnion(Guid id, string tenantId)
        {
            using (var context = new PayrollDbContext())
            {
                var p = await context.UnionDues.Include(x => x.Paycode)
                    .Where(x => x.TenantId == tenantId && x.Id == id)
                    .SingleOrDefaultAsync();

                var union = new Model.Union
                {
                    Id = p.Id,
                    Name = p.Name,
                    UseAmount = p.UseAmount,
                    DuesDeductionRate = p.DuesDeductionRate,
                    DuesDeductionAmount = p.DuesDeductionAmount,
                    MaximumDueAmount = p.MaximumDueAmount,
                    MinimumDueAmount = p.MinimumDueAmount,
                    PaycodeId = p.Paycode.Id
                };
                return union;
            }
        }
    }
}
