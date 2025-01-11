using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace stock;

public class InformationController(IInformationService informationService) : Controller
{
    [HttpGet("/information/product/{productId}")]
    public async Task<ActionResult<List<InformationRecord>>> GetInformationByProductId(Guid productId)
    {
        var information = await informationService.GetInformationByProductId(productId);

        return information.Status switch
        {
            InformationResultType.BadRequest => BadRequest($"The given product Id ({productId}) is not valid"),
            InformationResultType.NotFound => NotFound(
                $"No information found with the given product id: {productId}. Check if the product id is valid."),
            InformationResultType.Ok => Ok(information.Data),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpGet("/information/{id}")]
    public async Task<ActionResult<InformationRecord>> GetInformationById(Guid id)
    {
        var information = await informationService.GetInformationById(id);

        return information.Status switch
        {
            InformationResultType.BadRequest => BadRequest($"The given Id ({id}) is not valid"),
            InformationResultType.NotFound => NotFound($"No information found with the given id: {id}."),
            InformationResultType.Ok => Ok(information.Data),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpPost("/information")]
    public async Task<ActionResult<InformationPayload>> CreateInformation([FromBody] InformationInput input)
    {
        var information = await informationService.CreateInformation(input);

        return information.Status switch
        {
            InformationResultType.BadRequest => BadRequest($"The given input: ({input}) is not valid"),
            InformationResultType.Conflict => Conflict($"The given input: ({input}) was not created"),
            InformationResultType.Ok => Ok(information.Data),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    [HttpPut("/information/{id}")]
    public async Task<ActionResult<InformationPayload>> UpdateInformation(Guid id, [FromBody] InformationInput input)
    {
        var information = await informationService.UpdateInformation(id, input);

        return information.Status switch
        {
            InformationResultType.BadRequest => BadRequest($"The given input: ({input}) is not valid"),
            InformationResultType.Conflict => Conflict($"The given input: ({input}) was not updated"),
            InformationResultType.Ok => Ok(information.Data),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpDelete("/information/{id}")]
    public async Task<ActionResult<InformationRecord>> DeleteInformationById(Guid id)
    {
        var information = await informationService.DeleteInformationById(id);

        return information.Status switch
        {
            InformationResultType.BadRequest => BadRequest($"The given id: ({id}) is not valid"),
            InformationResultType.Conflict => Conflict($"The given id: ({id}) could not be deleted"),
            InformationResultType.Deleted => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpDelete("/information/product/{productId}")]
    public async Task<ActionResult<InformationRecord>> DeleteInformationByProductId(Guid productId)
    {
        var information = await informationService.DeleteInformationByProductId(productId);

        return information.Status switch
        {
            InformationResultType.BadRequest => BadRequest($"The given id: ({productId}) is not valid"),
            InformationResultType.Conflict => Conflict($"The given id: ({productId}) could not be deleted"),
            InformationResultType.Deleted => NoContent(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}