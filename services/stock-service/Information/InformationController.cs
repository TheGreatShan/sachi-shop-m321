using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace stock;

public class InformationController(IInformationService informationService) : Controller
{
    [HttpGet("/information/{productId}")]
    public async Task<ActionResult<List<InformationRecord>>> GetInformation(Guid productId)
    {
        var information = await informationService.GetInformationByProductId(productId);

        return information.Status switch
        {
            InformationResultType.BadRequest => BadRequest($"The given product Id ({productId}) is not valid"),
            InformationResultType.NotFound => NotFound($"No information found with the given product id: {productId}. Check if the product id is valid."),
            InformationResultType.Ok => Ok(information.Data),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}