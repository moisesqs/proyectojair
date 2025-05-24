using Microsoft.AspNetCore.Mvc;
using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Entities;
using SlaveWorkColab.Core.Http;
using TecPurisima.Ecommerce.Api.Services.Interfaces;

namespace SlaveWorkColab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentsRepository _repository;

    private readonly ICommentsService _commentService;

    public CommentsController(ICommentsService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<CommentsDto>>>> GetAll()
    {
        var response = new Response<List<CommentsDto>>
        {
            Data = await _commentService.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<CommentsDto>>> Post([FromBody] CommentsDto dto)
    {
        var response = new Response<CommentsDto>
        {
            Data = await _commentService.SaveAsync(dto)
        };

        return Created($"/api/[controller]/{response.Data.Id}", response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<CommentsDto>>> GetById(int id)
    {
        var response = new Response<CommentsDto>();

        if (!await _commentService.CommentExists(id))
        {
            response.Errors.Add("Comment not found");
            return NotFound(response);
        }

        response.Data = await _commentService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        var result = await _commentService.DeleteAsync(id);

        if (!result)
        {
            response.Errors.Add("Comment not found");
            return NotFound(response);
        }

        response.Message = "Comment successfully deleted";
        response.Data = result;
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<CommentsDto>>> Update([FromBody] CommentsDto dto)
    {
        var response = new Response<CommentsDto>();

        if (!await _commentService.CommentExists(dto.Id))
        {
            response.Errors.Add("Comment not found");
            return NotFound(response);
        }

        response.Data = await _commentService.UpdateAsync(dto);
        response.Message = "Comment successfully updated";
        return Ok(response);
    }

}
