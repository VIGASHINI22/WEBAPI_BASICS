using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Basics.Data;
using WebAPI_Basics.Models;

namespace WebAPI_Basics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CountryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; //injecting this only stroe data
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<IEnumerable<Country>> GetAll()
        {
            var countries = _dbContext.countries.ToList();
            if (countries == null)
            {
                return NoContent();
            }
            return Ok(countries);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<Country> GetById(int id)
        {
            var countries = _dbContext.countries.Find(id);
            if (countries == null)
            {
                return NoContent();
            }
            return Ok(countries);
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Country> Create([FromBody]Country country)
        {
            var result = _dbContext.countries.AsQueryable().Where(x => x.Name.ToLower().Trim() == country.Name.ToLower().Trim()).Any();
            if (result)
            {
                return Conflict("Country Already Exists in Database, u mf!!");
            }
            _dbContext.countries.Add(country);
            _dbContext.SaveChanges();
            return CreatedAtAction("GetById", new { id = country.Id }, country);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Country>  Update(int id, [FromBody]Country country)
        {

            if(country == null || id != country.Id)
            { 
                return BadRequest();
            }

            var countryFromDb = _dbContext.countries.Find(id);

            if (countryFromDb == null)
            {
                return NotFound();
            }

            countryFromDb.Name = country.Name;
            countryFromDb.ShortName = country.ShortName;
            countryFromDb.CountryCode = country.CountryCode;

            _dbContext.countries.Update(countryFromDb);
            _dbContext.SaveChanges();
            return NoContent();
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Country>DeleteById(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var countryFromDb = _dbContext.countries.Find(id);

            if (countryFromDb == null)
            {
                return NotFound();
            }
           
            _dbContext.countries.Remove(countryFromDb);
            _dbContext.SaveChanges();
            return Ok();  
        }

    }
}
