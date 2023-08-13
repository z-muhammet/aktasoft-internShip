using ebokScript.Models;
using System.Data.Entity;
using ebokScript.Models;
using ebokScript.Data;

namespace ebokScript.Context
{

    public class messagesContext : DbContext
    {
    

        public DbSet<messageScript>? message { get; set; }
        public DbSet<PageScript>? pages { get; set; }


    }


public static class messageScriptEndpoints
{
	public static void MapmessageScriptEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/messageScript", async (ebokScriptContext db) =>
        {
            return await db.messageScript.ToListAsync();
        })
        .WithName("GetAllmessageScripts")
        .Produces<List<messageScript>>(StatusCodes.Status200OK);

        routes.MapGet("/api/messageScript/{id}", async (int id, ebokScriptContext db) =>
        {
            return await db.messageScript.FindAsync(id)
                is messageScript model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetmessageScriptById")
        .Produces<messageScript>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/messageScript/{id}", async (int id, messageScript messageScript, ebokScriptContext db) =>
        {
            var foundModel = await db.messageScript.FindAsync(id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(messageScript);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdatemessageScript")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/messageScript/", async (messageScript messageScript, ebokScriptContext db) =>
        {
            db.messageScript.Add(messageScript);
            await db.SaveChangesAsync();
            return Results.Created($"/messageScripts/{messageScript.id}", messageScript);
        })
        .WithName("CreatemessageScript")
        .Produces<messageScript>(StatusCodes.Status201Created);


        routes.MapDelete("/api/messageScript/{id}", async (int id, ebokScriptContext db) =>
        {
            if (await db.messageScript.FindAsync(id) is messageScript messageScript)
            {
                db.messageScript.Remove(messageScript);
                await db.SaveChangesAsync();
                return Results.Ok(messageScript);
            }

            return Results.NotFound();
        })
        .WithName("DeletemessageScript")
        .Produces<messageScript>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
}
