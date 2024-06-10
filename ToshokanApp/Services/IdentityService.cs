using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ToshokanApp.Dtos;
using ToshokanApp.Models;
using ToshokanApp.Repositories.Base;
using ToshokanApp.Services.Base;

namespace ToshokanApp.Services;

public class IdentityService : IIdentityService
{
    private readonly IIdentityRepository identityRepository; 
    // private readonly UserManager<LoginDto> userManager;
    // private readonly SignInManager<LoginDto> signInManager;
    private readonly IOptionsSnapshot<string> connectionString;
    public IdentityService(IIdentityRepository identityRepository, IOptionsSnapshot<string> connectionString)
    {
        this.connectionString = connectionString;
        this.identityRepository = identityRepository;
    }
    public User? Login(LoginDto loginDto)
    {
        // var connection = new SqlConnection(this.connectionString.Value);
        // return connection.QueryFirstOrDefaultAsync<User>(
        //    sql: "select * from Users where [Email] = @Login and [Password] = @Password",
        //    param: loginDto
        // );
        return identityRepository.Login(loginDto);
    }

    public async Task Registration(RegistrationDto registrationDto)
    {
        // var connection = new SqlConnection(this.connectionString.Value);
        // await connection.ExecuteAsync(
        //     sql: "insert into Users([Name], [Surname], [Email], [Password]) values(@Name, @Surname, @Email, @Password)",
        //     param: registrationDto
        // );
        await identityRepository.Registration(registrationDto);
    }
}