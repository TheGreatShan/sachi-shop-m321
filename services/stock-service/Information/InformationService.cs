using Microsoft.IdentityModel.Tokens;

namespace stock;

public interface IInformationService
{
    Task<InformationResult<List<InformationPayload>>> GetInformationByProductId(Guid productId);
    Task<InformationResult<InformationPayload>> GetInformationById(Guid id);
    Task<InformationResult<Guid>> CreateInformation(InformationInput input);
    Task<InformationResult<Guid>> DeleteInformationById(Guid id);
    Task<InformationResult<Guid>> DeleteInformationByProductId(Guid id);
}

public class InformationService
    (IInformationRepository informationRepository, IProductRepository productRepository) : IInformationService
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

    public async Task<InformationResult<Guid>> CreateInformation(InformationInput informationInput)
    {
        if (!IsInputValid(informationInput) || !await productRepository.DoesExist(informationInput.ProductId))
            return new BadRequest<Guid>();

        var informationRecord = informationInput.ToInformation();
        var ack = await informationRepository.CreateInformation(informationRecord);

        if (!ack)
            return new Conflict<Guid>();

        return new Ok<Guid>(informationRecord.Id);
    }

    public async Task<InformationResult<Guid>> DeleteInformationById(Guid id)
    {
        if (!await informationRepository.DoesExist(id))
            return new BadRequest<Guid>();

        var ack = await informationRepository.DeleteByInformationId(id);

        return ack
            ? new Deleted<Guid>()
            : new Conflict<Guid>();
    }

    public async Task<InformationResult<Guid>> DeleteInformationByProductId(Guid id)
    {
        if (!await productRepository.DoesExist(id))
            return new BadRequest<Guid>();

        var ack = await informationRepository.DeleteByProductId(id);

        return ack
            ? new Deleted<Guid>()
            : new Conflict<Guid>();
    }

    private static bool IsInputValid(InformationInput input)
    {
        if (input.ProductId == Guid.Empty || input.ProductId == null || input.Information.IsNullOrEmpty() ||
            input.Stage.IsNullOrEmpty() || !Enum.TryParse<Stage>(input.Stage, out _))
            return false;
        return true;
    }
}