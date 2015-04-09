using System;
using MetalPay.Payroll.Contracts.SalaryCalculation.Write;
using MetalPay.Payroll.Infrastructure.Auth;
using MetalPay.Payroll.Infrastructure.Commands;
using MetalPay.Payroll.Queries;
using MetalPay.Payroll.SalaryCalculation.Commands;
using Nancy;
using Nancy.ModelBinding;

namespace MetalPay.Payroll.Api
{
    public class UnionModule : NancyModule
    {
        public UnionModule(ICommandExecutor commandExecutor, UnionQueries unionQueries)
            : base("api/payroll/company/{companyId}/union")
        {
            Get["", true] = async (p, ct) =>
            {
                var tenantId = Context.GetTenantId();
                Guid companyId = p.companyId;

                return Response.AsJson(await unionQueries.GetUnions(tenantId, companyId));
            };

            Get["/{unionId}", true] = async (y, ct) =>
            {
                var tenantId = Context.GetTenantId();
                Guid companyId = y.companyId;
                Guid unionId = y.unionId;

                return Response.AsJson(await unionQueries.GetUnion(tenantId, companyId, unionId));
            };

            Put["/{unionId}", true] = async (p, ct) =>
            {
                var incomingUnion = this.Bind<UnionDuesToUpdate>();
                Guid companyId = p.companyId;
                
                var tenantId = Context.GetTenantId();
                await commandExecutor.Execute(new UpdateUnionDues(tenantId, companyId, incomingUnion.Id, incomingUnion.Name, incomingUnion.UseAmount, incomingUnion.DuesDeductionRate, incomingUnion.DuesDeductionAmount, incomingUnion.MaximumDueAmount, incomingUnion.MinimumDueAmount, incomingUnion.PaycodeId));
                return HttpStatusCode.NoContent;
            };

            Post["", true] = async (p, ct) =>
            {
                var incomingUnion = this.Bind<UnionDuesToCreate>();
                
                var tenantId = Context.GetTenantId();
                Guid companyId = p.companyId;
                var unionId = Guid.NewGuid();
                await commandExecutor.Execute(new CreateUnionDues(tenantId, companyId, unionId, incomingUnion));
                var readModel = await unionQueries.GetUnion(tenantId, companyId, unionId);
                return Response.AsJson(readModel);
            };
        }
    }
}
