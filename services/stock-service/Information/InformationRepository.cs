using System.Data;
using Dapper;

namespace stock;

public interface IInformationRepository
{
    Task<List<InformationRecord>> GetInformationByProductId(Guid productId);
    Task<InformationRecord> GetInformationById(Guid id);
}

public class InformationRepository(IDbConnection connection) : IInformationRepository
{
    public async Task<List<InformationRecord>> GetInformationByProductId(Guid productId)
    {
        var dbInfo = await connection.QueryAsync<DbInformation>(
            @"SELECT
                    id as Id,
                    productId as ProductId,
                    information as Information,
                    stage as Stage
                FROM Information
                WHERE productId = @id"
            , new { id = productId });

        return dbInfo.ToList().ToInformation();
    }

    public async Task<InformationRecord> GetInformationById(Guid id)
    {
        var dbInfo = await connection.QueryAsync<DbInformation>(
            @"SELECT
                    id as Id,
                    productId as ProductId,
                    information as Information,
                    stage as Stage
                FROM Information
                WHERE id = @id"
            , new { id = id });

        return dbInfo.FirstOrDefault().ToInformation();
    }
}