using System;
using MetalPay.Payroll.Contracts.Model;
using MetalPay.Payroll.Infrastructure;

namespace MetalPay.Payroll.Model
{
    public class UnionDues : ModelBase
    {
        public UnionDues()
        {
        }

        public UnionDues(string tenantId, Guid unionId, UnionDuesToCreate unionDuesToCreate, Paycode paycode)
            : base(tenantId, unionId)
        {
            ValidateDueRateAndAmount(unionDuesToCreate);

            Name = unionDuesToCreate.Name;
            UseAmount = unionDuesToCreate.UseAmount;
            DuesDeductionRate = unionDuesToCreate.DuesDeductionRate;
            DuesDeductionAmount = unionDuesToCreate.DuesDeductionAmount;
            MaximumDueAmount = unionDuesToCreate.MaximumDueAmount;
            MinimumDueAmount = unionDuesToCreate.MinimumDueAmount;
            Paycode = paycode;
        }

        public string Name { get; set; }
        public bool UseAmount { get; set; }
        public decimal? DuesDeductionRate { get; set; }
        public decimal? DuesDeductionAmount { get; set; }
        public decimal? MaximumDueAmount { get; set; }
        public decimal? MinimumDueAmount { get; set; }
        public Paycode Paycode { get; set; }

        public void Update(UnionDuesToUpdate unionDuesToUpdate, Paycode paycode)
        {
            ValidateDueRateAndAmount(unionDuesToUpdate);

            Name = unionDuesToUpdate.Name;
            UseAmount = unionDuesToUpdate.UseAmount;
            DuesDeductionRate = unionDuesToUpdate.DuesDeductionRate;
            DuesDeductionAmount = unionDuesToUpdate.DuesDeductionAmount;
            MaximumDueAmount = unionDuesToUpdate.MaximumDueAmount;
            MinimumDueAmount = unionDuesToUpdate.MinimumDueAmount;
            Paycode = paycode;
        }

        private static void ValidateDueRateAndAmount(UnionBase unionDues)
        {
            if (unionDues.UseAmount)
            {
                if (!unionDues.DuesDeductionAmount.HasValue)
                    throw new DomainException("Missing union dues amount for union with dues type set to 'amount'");
            }

            if (!unionDues.UseAmount)
            {
                if (!unionDues.DuesDeductionRate.HasValue)
                {
                    throw new DomainException(
                        "Missing union dues rate, minimum or maximum amount for union with dues type 'rate'");
                }

                if (unionDues.MinimumDueAmount.HasValue && unionDues.MaximumDueAmount.HasValue)
                {
                    if (unionDues.MinimumDueAmount.Value > unionDues.MaximumDueAmount.Value)
                    {
                        throw new DomainException(
                            "Union dues maximum amount is smaller than minimum amount");
                    }
                }
            }
        }
    }
}
