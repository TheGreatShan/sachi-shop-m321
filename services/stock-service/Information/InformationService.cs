namespace stock;

public interface IInformationService
{
    Task<InformationResult<List<InformationPayload>>> GetInformationByProductId(Guid productId);
    Task<InformationResult<InformationPayload>> GetInformationById(Guid id);
}

public class InformationService(IInformationRepository informationRepository) : IInformationService
{
    public async Task<InformationResult<List<InformationPayload>>> GetInformationByProductId(Guid productId)
    {
        if (productId == Guid.Empty)
            return new BadRequest<List<InformationPayload>>();

        var information = await informationRepository.GetInformationByProductId(productId);

        if (information == null || information.Count == 0)
            return new NotFound<List<InformationPayload>>();

        return new Ok<List<InformationPayload>>(information.ToPayload());
    }

    public async Task<InformationResult<InformationPayload>> GetInformationById(Guid id)
    {
        if (id == Guid.Empty)
            return new BadRequest<InformationPayload>();

        var information = await informationRepository.GetInformationById(id);

        if (information == null)
            return new NotFound<InformationPayload>();

        return new Ok<InformationPayload>(information.ToPayload());
    }
}