using Microsoft.EntityFrameworkCore;

public class PersonRepository(PersonDBContext personDBContext) : IPersonRepository
{
    public async Task<bool> AddPersonAsync(PersonCreateInfo personCreateInfo)
    {
        try
        {
            bool isExisted = personDBContext.People.Any(x=>x.Name.ToLower()==personCreateInfo.Name.ToLower() && x.IsDeleted==false);
            if(isExisted) return false;
            personDBContext.People.Add(new Person
            {
                Name=personCreateInfo.Name,
                Age=personCreateInfo.Age
            });

            int res = await personDBContext.SaveChangesAsync();

            return res>0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> UpdatePersonAsync(PersonUpdateInfo personUpdateInfo)
    {
        try
        {
            Person? existingPerson = await personDBContext.People.FindAsync(personUpdateInfo.Id);
            if (existingPerson == null)
            return false;
            

            existingPerson.Name = personUpdateInfo.Name;
            existingPerson.Age = personUpdateInfo.Age;

            int result = await personDBContext.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeletePersonAsync(int id)
    {
        try
        {
            Person? person = await personDBContext.People.FindAsync(id);
            if (person == null) return false;
            
            personDBContext.People.Remove(person);
            int result = await personDBContext.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<PersonReadDTO?> GetPersonByIdAsync(int id)
    {
        try
        {
            Person? person = await personDBContext.People.FindAsync(id);
            if(person==null) return null;
            return new PersonReadDTO(){Id=person.Id, Name=person.Name, Age=person.Age};
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public  IEnumerable<PersonReadDTO> GetAllPersonsAsync()
    {
        try
        {
            return  personDBContext.People.Select(x=>new PersonReadDTO()
            {
                Id=x.Id,
                Name=x.Name,
                Age=x.Age
            }).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new List<PersonReadDTO>();
        }
    }
}
