using System.Data;
using Dapper;

namespace stock;

public interface IInformationRepository
{
    Task<List<InformationRecord>> GetInformationByProductId(Guid productId);
    Task<InformationRecord> GetInformationById(Guid id);
    Task<bool> CreateInformation(InformationRecord informationRecord);
    Task<bool> UpdateInformation(InformationRecord informationRecord);
    Task<bool> DeleteByInformationId(Guid id);
    Task<bool> DeleteByProductId(Guid productId);
    Task<bool> DoesExist(Guid id);
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

    public async Task<bool> CreateInformation(InformationRecord informationRecord)
    {
        var confirm = await connection.ExecuteAsync(
            "INSERT INTO Information (id, productId, information, stage) VALUES (@id, @productId, @information, @stage)",
            new
            {
                id = informationRecord.Id, productId = informationRecord.ProductId,
                information = informationRecord.Information, stage = informationRecord.Stage.ToString()
            });

        return confirm == 1;
    }

    public async Task<bool> UpdateInformation(InformationRecord informationRecord)
    {
        var ack = await connection.ExecuteAsync(
            "UPDATE Information SET productId = @productId, information = @information, stage = @stage WHERE id = @id",
            new
            {
                id = informationRecord.Id,
                productId = informationRecord.ProductId, information = informationRecord.Information,
                stage = informationRecord.Stage
            });
        return ack == 1;
    }

    public async Task<bool> DeleteByInformationId(Guid id)
    {
        var confirm = await connection.ExecuteAsync("DELETE FROM Information WHERE id = @id", new { id });
        return confirm == 1;
    }

    public async Task<bool> DeleteByProductId(Guid productId)
    {
        var ack = await connection.ExecuteAsync("DELETE FROM Information WHERE productId = @id",
            new { id = productId });
        return ack == 1;
    }

    public async Task<bool> DoesExist(Guid id)
    {
        var confirm = await connection.QueryAsync<Guid>("SELECT id FROM Information WHERE id = @id", new { id });
        return confirm.Count() == 1;
    }
}