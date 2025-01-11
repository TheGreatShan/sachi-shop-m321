namespace stock;

public interface IInformationService
{
    Task<InformationResult<List<InformationPayload>>> GetInformationByProductId(Guid productId);
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
}

