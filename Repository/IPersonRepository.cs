public interface IPersonRepository
{
    Task<bool> AddPersonAsync(PersonCreateInfo personCreateInfo);
    Task<bool> UpdatePersonAsync(PersonUpdateInfo personUpdateInfo);
    Task<bool> DeletePersonAsync(int id);
    Task<PersonReadDTO?> GetPersonByIdAsync(int id);
    IEnumerable<PersonReadDTO> GetAllPersonsAsync();
}