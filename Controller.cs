using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/person")]

public class PersonController(IPersonRepository personRepository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetPeople()
    {
        return Ok(ApiResponse<IEnumerable<PersonReadDTO>>.Success(null!, personRepository.GetAllPersonsAsync()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPerson([FromBody] string name, int age)
    {
        PersonCreateInfo personCreateInfo = new PersonCreateInfo() { Name = name, Age = age };
        bool res = await personRepository.AddPersonAsync(personCreateInfo);
        if (res == false) return BadRequest(ApiResponse<bool>.Fail(null, res));
        return Ok(ApiResponse<bool>.Success(null, res));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePerson([FromRoute] int id)
    {
        bool res = await personRepository.DeletePersonAsync(id);
        if (res == false) return NotFound(ApiResponse<bool>.Fail(null, res));
        return Ok(ApiResponse<bool>.Success(null, res));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePerson([FromBody] PersonUpdateInfo personUpdateInfo)
    {
        bool res = await personRepository.UpdatePersonAsync(personUpdateInfo);
        if (res == false) return NotFound(ApiResponse<bool>.Fail(null, res));
        return Ok(ApiResponse<bool>.Success(null, res));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPersonById([FromRoute] int id)
    {
        PersonReadDTO? personReadDTO = await personRepository.GetPersonByIdAsync(id);
        if(personReadDTO == null) return NotFound(ApiResponse<PersonReadDTO?>.Fail(null, personReadDTO));
        return Ok(ApiResponse<PersonReadDTO?>.Success(null, personReadDTO));
    }
}