using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service1.DataAccess;
using Service1.Dtos;
using Service1.Entities;

namespace Service1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Service1Controller : ControllerBase
    {
        private S1DbContext _con;
        public Service1Controller(S1DbContext service1)
        {
            _con = service1;
        }

        [HttpGet]
        public async ValueTask<List<User>> GetAsync()
        {
            var users = await _con.Users.ToListAsync();
            return users;
        }
        [HttpPost]
        public async ValueTask<bool> CreateAsync(UserDto userDto)
        {
            try
            {
                await _con.Users.AddAsync(new Entities.User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    UserName = userDto.UserName,
                    Password = userDto.Password,
                    Role = userDto.Role,

                });
                await _con.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        [HttpPut]
        public async ValueTask<bool> UpdateAsync(int id, UserDto newUserDto)
        {
            try
            {

                var user = await _con.Users.FirstOrDefaultAsync(x => x.Id == id);
                user.Name = newUserDto.Name;
                user.Email = newUserDto.Email;
                user.UserName = newUserDto.UserName;
                user.Password = newUserDto.Password;
                user.Role = newUserDto.Role;
                _con.Update(user);
                await _con.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        [HttpDelete]
        public async ValueTask<bool> DeleteAsync(int id)
        {
            try
            {

                var user = await _con.Users.FirstOrDefaultAsync(x => x.Id == id);

                _con.Users.Remove(user);
                await _con.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
