using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MetalPay.Payroll.Contracts.Read;

namespace MetalPay.Payroll.Queries
{
    public class UnionQueries
    {
        private readonly DbConnection _dbConnection;
        private const string BaseQuery = @"
                    SELECT * FROM salarycalculation.UnionDues 
                    WHERE TenantId = @tenantId AND CompanyId = @companyId";

        public UnionQueries(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Union[]> GetUnions(string tenantId, Guid companyId)
        {
            var unions = await _dbConnection.QueryAsync<Union>(BaseQuery, new { tenantId, companyId });

            return unions.ToArray();
        }

        public async Task<Union> GetUnion(string tenantId, Guid companyId, Guid unionId)
        {
            var unions = await _dbConnection.QueryAsync<Union>(string.Format("{0} AND Id = @unionId", BaseQuery), new { tenantId, companyId, unionId });

            return unions.SingleOrDefault();
        }
    }
}