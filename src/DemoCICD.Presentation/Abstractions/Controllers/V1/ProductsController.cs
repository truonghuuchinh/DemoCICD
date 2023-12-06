﻿using Asp.Versioning;
using DemoCICD.Contract.Abstractions.Services.Product;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Extensions;
using DemoCICD.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Abstractions.Controllers.V1;
[ApiVersion(1)]
public class ProductsController : ApiController
{
    public ProductsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(typeof(Result<IEnumerable<Response.ProductResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        string? sortColumnAndOrder,
        int pageIndex,
        int pageSize)
    {
        var sortColumnOrder = SortOrderExtension.ConvertStringToSortOrderAndColumns(sortColumnAndOrder);
        var sort = SortOrderExtension.ConvertStringToSortOrder(sortOrder);
        var result = await _sender.Send(new Query.GetProductQuery(searchTerm, sortColumn, sort, sortColumnOrder, pageIndex, pageSize));
        return Ok(result);
    }

    [HttpGet("{productId:Guid}", Name = "GetProductById")]
    [ProducesResponseType(typeof(Response.ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid productId)
    {
        var result = await _sender.Send(new Query.GetProductByIdQuery(productId));
        return Ok(result);
    }

    [HttpPost(Name = "CreateProduct")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Command.CreatedProductCommand request)
    {
        var result = await _sender.Send(request);
        return Ok(result);
    }

    [HttpPut("{productId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid productId, [FromBody] Command.UpdatedProductCommand request)
    {
        if (productId != request.Id)
        {
            throw new BadRequestException("Please input match product id");
        }

        request.Id = productId;

        var result = await _sender.Send(request);
        return Ok(result);
    }

    [HttpDelete("{productId}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid productId)
    {
        var result = await _sender.Send(new Command.DeletedProductCommand(productId));
        return Ok(result);
    }
}
