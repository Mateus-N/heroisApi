using HeroisApi.Dtos.SuperPoderesDtos;
using HeroisApi.Exceptions;
using HeroisApi.Exceptions.MensageModel;
using HeroisApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeroisApi.Controllers;

/// <summary>
/// Controller que recebe e envia todas as requisições HTTP relacionadas aos Super Poderes
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SuperPoderesController(ISuperPoderService service) : ControllerBase
{
    private readonly ISuperPoderService service = service;

    /// <summary>
    /// Adiciona novo super poder ao banco
    /// </summary>
    /// <param name="createDto"></param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a inserção seja feita com sucesso</response>
    /// <response code="409">Caso aconteca algum conflito na inserção</response>
    /// <response code="400">Caso a entidade passada no corpo da requisição não tenha o formato esperado</response>
    [HttpPost]
    [ProducesResponseType(typeof(ReadSuperPoderesDto), 201)]
    [ProducesResponseType(typeof(MessageModel), 409)]
    [ProducesResponseType(typeof(MessageModel), 400)]
    public async Task<IActionResult> Post(CreateSuperPoderesDto createDto)
    {
        try
        {
            ReadSuperPoderesDto readDto = await service.CadastraSuperPoder(createDto);
            return CreatedAtAction(nameof(GetOne), new { readDto.Id }, readDto);
        }
        catch (DbUpdateException)
        {
            return Conflict(
                new MessageModel("O campo descrição deve ter no máximo 250 caracteres"));
        }
        catch (Exception)
        {
            return BadRequest(
                new MessageModel("Modelo de entidade não corresponde ao SuperPoder"));
        }
    }

    /// <summary>
    /// Busca e retorna todos os super poderes cadastrados no banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a busca seja feita com sucesso</response>
    /// <response code="500">Caso ocorra algum erro na listagem</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReadSuperPoderesDto>), 200)]
    public IActionResult Get()
    {
        try
        {
            IEnumerable<ReadSuperPoderesDto> readDto = service.BuscarTodos();
            return Ok(readDto);
        }
        catch (EmptyListException)
        {
            return Ok(
                new MessageModel("Lista de SuperPoderes vazia"));
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro ao buscar lista de heróis");
        }
    }

    /// <summary>
    /// Recebe um id do tipo inteiro através da URL e busca o super poder que possui este id
    /// </summary>
    /// <param name="id">Valor necessário para buscar o super poder</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a busca seja feita com sucesso</response>
    /// <response code="404">Caso o super poder não seja encontrado</response>
    /// <response code="400">Caso o id passado não seja do tipo correto</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ReadSuperPoderesDto), 200)]
    [ProducesResponseType(typeof(MessageModel), 404)]
    [ProducesResponseType(typeof(MessageModel), 400)]
    public async Task<IActionResult> GetOne(string id)
    {
        try
        {
            ReadSuperPoderesDto readDto = await service.BuscaUm(int.Parse(id));
            return Ok(readDto);
        }
        catch (NotFoundException)
        {
            return NotFound(
                new MessageModel("SuperPoder com não encontrado"));
        }
        catch (Exception)
        {
            return BadRequest(
                new MessageModel("O id deve ser passado apenas com o tipo int"));
        }
    }

    /// <summary>
    /// Recebe um id do tipo inteiro através da URL e atualiza os dados desse super poder
    /// </summary>
    /// <param name="updateDto">Objeto com campos necessários para atualizar o super poder</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a atualização seja feita com sucesso</response>
    /// <response code="404">Caso o super poder não seja encontrado</response>
    /// <response code="409">Caso ocorra algum conflito na atualização dos dados</response>
    /// <response code="400">Caso a entidade passada no corpo da requisição não tenha o formato esperado</response>
    [HttpPut]
    [ProducesResponseType(typeof(ReadSuperPoderesDto), 201)]
    [ProducesResponseType(typeof(MessageModel), 409)]
    [ProducesResponseType(typeof(MessageModel), 400)]
    [ProducesResponseType(typeof(MessageModel), 404)]
    public async Task<IActionResult> Put(UpdateSuperPoderesDto updateDto)
    {
        try
        {
            ReadSuperPoderesDto readDto = await service.AtualizaPoder(updateDto);
            return Ok(readDto);
        }
        catch (DbUpdateException)
        {
            return BadRequest(
                new MessageModel("O campo descrição deve ter no máximo 250 caracteres"));
        }
        catch (Exception)
        {
            return BadRequest(
                new MessageModel("Modelo de entidade não corresponde ao SuperPoder"));
        }
    }

    /// <summary>
    /// Recebe um id do tipo inteiro através da URL e apaga os dados desse superpoder
    /// </summary>
    /// <param name="id">Valor necessário para buscar o super poder</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a exclusão ocorra com sucesso</response>
    /// <response code="404">Caso o super poder não seja encontrado</response>
    /// <response code="400">Caso a entidade passada no corpo da requisição não tenha o formato esperado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(MessageModel), 400)]
    [ProducesResponseType(typeof(MessageModel), 404)]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await service.ApagarPoder(int.Parse(id));
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound(
                new MessageModel("SuperPoder não encontrado"));
        }
        catch (Exception)
        {
            return BadRequest(
                new MessageModel("O id deve ser passado apenas com o tipo int"));
        }
    }
}
