﻿using Microsoft.AspNetCore.Identity;
using PomodoroLibrary.Models.Identity;

namespace PomodoroUI.Middleware;

public class PreferredThemeMiddleware
{
    private readonly RequestDelegate _next;

    public PreferredThemeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var user = await userManager.GetUserAsync(context.User);
            context.Session.SetString("theme", user.PreferredTheme ?? "dark");
        }
        await _next(context);
    }
}
