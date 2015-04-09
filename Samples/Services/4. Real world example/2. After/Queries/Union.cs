using System;

namespace MetalPay.Payroll.Contracts.Read
{
    public class Union
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool UseAmount { get; set; }
        public decimal? DuesDeductionAmount { get; set; }
        public decimal? DuesDeductionRate { get; set; }
        public decimal? MinimumDueAmount { get; set; }
        public decimal? MaximumDueAmount { get; set; }
        public Guid PaycodeId { get; set; }
    }
}
