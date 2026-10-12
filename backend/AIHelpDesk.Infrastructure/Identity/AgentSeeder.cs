
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace AIHelpDesk.Infrastructure.Identity;

public static class AgentSeeder
{
    public static async Task SeedAgentAsync(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        var email = configuration["AgentSeed:Email"];
        var password = configuration["AgentSeed:Password"];

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                "Agent seed credentials are not configured.");
        }

        var existingUser =
            await userManager.FindByEmailAsync(email);

        if (existingUser is not null)
        {
            if (!await userManager.IsInRoleAsync(
                    existingUser, "Agent"))
            {
                var roleResult =
                    await userManager.AddToRoleAsync(
                        existingUser, "Agent");

                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        "Failed to assign Agent role.");
                }
            }

            return;
        }

        var agent = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(
            agent, password);

        if (!result.Succeeded)
        {
            var errors = string.Join(
                ", ",
                result.Errors.Select(e => e.Description));

            throw new InvalidOperationException(
                $"Failed to create Agent: {errors}");
        }

        var addRoleResult =
            await userManager.AddToRoleAsync(
                agent, "Agent");

        if (!addRoleResult.Succeeded)
        {
            throw new InvalidOperationException(
                "Agent created, but role assignment failed.");
        }
    }
}
