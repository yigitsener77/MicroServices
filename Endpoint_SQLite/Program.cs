using Endpoint_SQLite;
using Endpoint_SQLite.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(opt => {
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<LibraryContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<ILibraryService, LibraryService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET Method's
app.MapGet("/books/", async (ILibraryService service, int? authorId = null, int? publisherId = null) => {
    var books = await service.GetBooksAsync(authorId, publisherId);
    if (books.Any())
    {
        return Results.Json(books);
    }
    else
    {
        return Results.NotFound("Kayýtlý kitap bulunamadý!");
    }
});

app.MapGet("/books/{id}", async (ILibraryService service, int id) => {
    var book = await service.GetBookAsync(id);
    if (book != null)
    {
        return Results.Json(book);
    }
    else
    {
        return Results.NotFound("Aradýðýnýz kitap bulunamadý!");
    }
});

app.MapGet("/authors/", async (ILibraryService service) => {
    var authors = await service.GetAuthorsAsync();
    if (authors.Any())
    {
        return Results.Json(authors);
    }
    else
    {
        return Results.NotFound("Kayýtlý yazar bulunamadý!");
    }
});

app.MapGet("/authors/{id}", async (ILibraryService service, int id) => {
    var author = await service.GetAuthorAsync(id);
    if (author != null)
    {
        return Results.Json(author);
    }
    else
    {
        return Results.NotFound("Aradýðýnýz yazar bulunamadý!");
    }
});

app.MapGet("/publishers/", async (ILibraryService service) => {
    var publishers = await service.GetPublishersAsync();
    if (publishers.Any())
    {
        return Results.Json(publishers);
    }
    else
    {
        return Results.NotFound("Kayýtlý yayýncý bulunamadý!");
    }
});

app.MapGet("/publishers/{id}", async (ILibraryService service, int id) => {
    var publisher = await service.GetPublisherAsync(id);
    if (publisher != null)
    {
        return Results.Json(publisher);
    }
    else
    {
        return Results.NotFound("Aradýðýnýz yayýncý bulunamadý!");
    }
});


//Post Method's:
app.MapPost("/books/create", async (ILibraryService service, [FromBody] Book newBook) => {
    try
    {
        if (await service.GetAuthorAsync(newBook.AuthorId) == null)
        {
            return Results.BadRequest("Author is missing or doesn't exists!");
        }

        if (await service.GetPublisherAsync(newBook.PublisherId) == null)
        {
            return Results.BadRequest("Publisher is missing or doesn't exists!");
        }

        await service.AddBookAsync(newBook);
        return Results.Created($"/books/{newBook.Id}", newBook);
    }
    catch (Exception ex)
    {
        return Results.Problem("Error: " + ex.Message);
    }
});

app.MapPost("/authors/create", async (ILibraryService service, [FromBody] Author newAuthor) => {
    try
    {
        await service.AddAuthorAsync(newAuthor);
        return Results.Created($"/authors/{newAuthor.Id}", newAuthor);
    }
    catch (Exception ex)
    {
        return Results.Problem("Hata:" + ex.Message);
    }
});

app.MapPost("/publishers/create", async (ILibraryService service, [FromBody] Publisher newPublisher) => {
    try
    {
        await service.AddPublisherAsync(newPublisher);
        return Results.Created($"/publishers/{newPublisher.Id}", newPublisher);
    }
    catch (Exception ex)
    {
        return Results.Problem("Hata:" + ex.Message);
    }
});

// PUT Method's:

app.MapPut("/books/{id}/update", async (ILibraryService service, int id, [FromBody] Book updatedBook) => {
    if (await service.GetBookAsync(id) != null)
    {
        try
        {
            if (await service.GetAuthorAsync(updatedBook.AuthorId) == null)
            {
                return Results.BadRequest("Author is missing or doesn't exists!");
            }

            if (await service.GetPublisherAsync(updatedBook.PublisherId) == null)
            {
                return Results.BadRequest("Publisher is missing or doesn't exists!");
            }

            await service.UpdateBookAsync(updatedBook);
            return Results.Json(updatedBook);
        }
        catch (Exception ex)
        {
            return Results.Problem("Error: " + ex.Message);
        }
    }
    else
    {
        return Results.NotFound("Aradýðýnýz kitap bulunamadý!");
    }
});

app.MapPut("/authors/{id}/update", async (ILibraryService service, int id, [FromBody] Author updatedAuthor) => {
    if (await service.GetAuthorAsync(id) != null)
    {
        try
        {
            await service.UpdateAuthorAsync(updatedAuthor);
            return Results.Json(updatedAuthor);
        }
        catch (Exception ex)
        {
            return Results.Problem("Error: " + ex.Message);
        }
    }
    else
    {
        return Results.NotFound("Aradýðýnýz yazar bulunamadý!");
    }
});

app.MapPut("/publishers/{id}/update", async (ILibraryService service, int id, [FromBody] Publisher updatedPublisher) => {
    if (await service.GetPublisherAsync(id) != null)
    {
        try
        {
            await service.UpdatePublisherAsync(updatedPublisher);
            return Results.Json(updatedPublisher);
        }
        catch (Exception ex)
        {
            return Results.Problem("Error: " + ex.Message);
        }
    }
    else
    {
        return Results.NotFound("Aradýðýnýz yayýncý bulunamadý!");
    }
});

// DELETE Method's
app.MapDelete("/books/{id}/delete", async (ILibraryService service, int id) => {
    try
    {
        await service.DeleteBookAsync(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem("Error: " + ex.Message);
    }
});

app.MapDelete("/authors/{id}/delete", async (ILibraryService service, int id) => {
    try
    {
        await service.DeleteAuthorAsync(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {

        return Results.Problem("Error: " + ex.Message);
    }
});

app.MapDelete("/publishers/{id}/delete", async (ILibraryService service, int id) => {
    try
    {
        await service.DeletePublisherAsync(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {

        return Results.Problem("Error: " + ex.Message);
    }
});

app.Run();
