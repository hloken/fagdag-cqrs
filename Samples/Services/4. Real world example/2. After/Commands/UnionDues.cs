using System;
using MetalPay.Payroll.Infrastructure;
using MetalPay.Payroll.Model;
using MetalPay.Payroll.SalaryCalculation.Events;
using NServiceBus;

namespace MetalPay.Payroll.SalaryCalculation
{
    public class UnionDues : ModelBase
    {
        public UnionDues()
        {
        }

        static public UnionDues Create(string tenantId, Guid companyId, Guid unionId, string unionName, bool useAmount, decimal? duesDeductionRate, decimal? duesDeductionAmount, decimal? maximumDueAmount, decimal? minimumDueAmount, Guid paycodeId)
        {
            ValidateDueRateAndAmount(useAmount, duesDeductionAmount, duesDeductionRate, minimumDueAmount, maximumDueAmount);

            var unionDues = new UnionDues
            {
                Id = unionId,
                TenantId = tenantId,
                CompanyId = companyId,
                Name = unionName,
                UseAmount = useAmount,
                DuesDeductionRate = duesDeductionRate,
                DuesDeductionAmount = duesDeductionAmount,
                MaximumDueAmount = maximumDueAmount,
                MinimumDueAmount = minimumDueAmount,
                PaycodeId = paycodeId
            };

            return unionDues;
        }

        public void Update(Guid paycodeId, string unionName, bool useAmount, decimal? duesDeductionRate, decimal? duesDeductionAmount, decimal? maximumDueAmount, decimal? minimumDueAmount, IBus bus)
        {
            ValidateDueRateAndAmount(useAmount, duesDeductionAmount, duesDeductionRate, minimumDueAmount, maximumDueAmount);

            Name = unionName;
            UseAmount = useAmount;
            DuesDeductionRate = duesDeductionRate;
            DuesDeductionAmount = duesDeductionAmount;
            MaximumDueAmount = maximumDueAmount;
            MinimumDueAmount = minimumDueAmount;
            PaycodeId = paycodeId;

            bus.Publish(new UnionDuesWasUpdatedEvent { UnionDuesId = Id, TenantId = TenantId, CompanyId = CompanyId});
        }

        private static void ValidateDueRateAndAmount(bool useAmount, decimal? duesDeductionAmount, decimal? duesDeductionRate, decimal? minimumDueAmount, decimal? maximumDueAmount)
        {
            if (useAmount)
            {
                if (!duesDeductionAmount.HasValue)
                    throw new DomainException("Missing union dues amount for union with dues type set to 'amount'");
            }

            if (!useAmount)
            {
                if (!duesDeductionRate.HasValue)
                {
                    throw new DomainException(
                        "Missing union dues rate, minimum or maximum amount for union with dues type 'rate'");
                }

                if (minimumDueAmount.HasValue && maximumDueAmount.HasValue)
                {
                    if (minimumDueAmount.Value > maximumDueAmount.Value)
                    {
                        throw new DomainException("Union dues maximum amount is smaller than minimum amount");
                    }
                }
                if (minimumDueAmount.HasValue && minimumDueAmount.Value < 0 || maximumDueAmount.HasValue && maximumDueAmount.Value < 0)
                {
                    throw new DomainException("Union dues minimum amount and maximum amount must be positive ");
                }
            }
        }


        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public bool UseAmount { get; set; }
        public decimal? DuesDeductionRate { get; set; }
        public decimal? DuesDeductionAmount { get; set; }
        public decimal? MaximumDueAmount { get; set; }
        public decimal? MinimumDueAmount { get; set; }
        public Guid PaycodeId { get; set; }


    }
}
