using HeroisApi.Exceptions.MensageModel;
using HeroisApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using HeroisApi.Dtos.HeroiDtos;
using HeroisApi.Services.Interfaces;

namespace HeroisApi.Controllers;

/// <summary>
/// Controller que recebe e envia todas as requisições HTTP relacionadas aos Heróis
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HeroisController(IHeroiService service) : ControllerBase
{
    private readonly IHeroiService service = service;

    /// <summary>
    /// Adiciona novo Herói ao banco de dados
    /// </summary>
    /// <param name="createDto">Objeto com campos necessários para a criação de um herói</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a inserção seja feita com sucesso</response>
    /// <response code="409">Caso aconteca algum conflito na inserção</response>
    /// <response code="400">Caso a entidade passada no corpo da requisição não tenha o formato esperado</response>
    [HttpPost]
    [ProducesResponseType(typeof(ReadHeroiDto), 201)]
    [ProducesResponseType(typeof(MessageModel), 409)]
    [ProducesResponseType(typeof(MessageModel), 400)]
    public async Task<IActionResult> Post(CreateHeroiDto createDto)
    {
        try
        {
            ReadHeroiDto readDto = await service.CadastraHeroi(createDto);
            return CreatedAtAction(nameof(GetOne), new { readDto.Id }, readDto);
        }
        catch (DbException e)
        {
            return Conflict(
                new MessageModel(e.Message));
        }
        catch (Exception)
        {
            return BadRequest(
                new MessageModel("Modelo de entidade não corresponde ao Herói"));
        }
    }

    /// <summary>
    /// Busca e retorna todos os herois cadastrados no banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a busca seja feita com sucesso</response>
    /// <response code="500">Caso ocorra algum erro na listagem</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReadHeroiDto>), 200)]
    public IActionResult Get()
    {
        try
        {
            IEnumerable<ReadHeroiDto> readDto = service.BuscarTodos();
            return Ok(readDto);
        }
        catch (EmptyListException)
        {
            return Ok(
                new MessageModel("Lista de heróis vazia"));
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro ao buscar lista de heróis");
        }
    }

    /// <summary>
    /// Recebe um id do tipo inteiro através da URL e busca o herói que possui este id
    /// </summary>
    /// <param name="id">Valor necessário para buscar o herói</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a busca seja feita com sucesso</response>
    /// <response code="404">Caso o herói não seja encontrado</response>
    /// <response code="400">Caso o id passado não seja do tipo correto</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ReadHeroiDto), 200)]
    [ProducesResponseType(typeof(MessageModel), 404)]
    [ProducesResponseType(typeof(MessageModel), 400)]
    public async Task<IActionResult> GetOne(string id)
    {
        try
        {
            ReadHeroiDto readDto = await service.BuscaUm(int.Parse(id));
            return Ok(readDto);
        }
        catch (NotFoundException)
        {
            return NotFound(
                new MessageModel("Heroi com não encontrado"));
        }
        catch (Exception)
        {
            return BadRequest(
                new MessageModel("O id deve ser passado apenas com o tipo int"));
        }
    }

    /// <summary>
    /// Recebe um id do tipo inteiro através da URL e atualiza os dados desse herói
    /// </summary>
    /// <param name="id">Valor necessário para buscar o herói</param>
    /// <param name="updateDto">Objeto com campos necessários para atualizar o herói</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a atualização seja feita com sucesso</response>
    /// <response code="404">Caso o herói não seja encontrado</response>
    /// <response code="409">Caso ocorra algum conflito na atualização dos dados</response>
    /// <response code="400">Caso a entidade passada no corpo da requisição não tenha o formato esperado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ReadHeroiDto), 201)]
    [ProducesResponseType(typeof(MessageModel), 409)]
    [ProducesResponseType(typeof(MessageModel), 400)]
    [ProducesResponseType(typeof(MessageModel), 404)]
    public async Task<IActionResult> Put(int id, UpdateHeroiDto updateDto)
    {
        try
        {
            ReadHeroiDto readDto = await service.AtualizaHeroi(id, updateDto);
            return Ok(readDto);
        }
        catch (NotFoundException)
        {
            return NotFound(
                new MessageModel("Heroi com não encontrado"));
        }
        catch (DbException e)
        {
            return Conflict(
                new MessageModel(e.Message));
        }
        catch (Exception)
        {
            return BadRequest(
                new MessageModel("Modelo de entidade não corresponde ao Heroi"));
        }
    }

    /// <summary>
    /// Recebe um id do tipo inteiro através da URL e apaga os dados desse herói
    /// </summary>
    /// <param name="id">Valor necessário para buscar o herói</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a exclusão ocorra com sucesso</response>
    /// <response code="404">Caso o herói não seja encontrado</response>
    /// <response code="400">Caso a entidade passada no corpo da requisição não tenha o formato esperado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(MessageModel), 400)]
    [ProducesResponseType(typeof(MessageModel), 404)]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await service.ApagarHeroi(int.Parse(id));
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound(
                new MessageModel("Heroi com não encontrado"));
        }
        catch (Exception)
        {
            return BadRequest(
                new MessageModel("O id deve ser passado apenas com o tipo int"));
        }
    }
}
