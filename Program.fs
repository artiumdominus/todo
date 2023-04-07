namespace Todo

open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting

module Program =
    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)
        let app = builder.Build()

        app.MapGet("/", Func<string>(fun () -> "Todos app!")) |> ignore
        app.MapGet("/todos", Func<list<Tasks.Task>>(fun () -> Tasks.list()))  |> ignore
        app.MapGet("/todos/{id}", Func<int, Tasks.Task>(fun (id) -> Tasks.show id)) |> ignore
        app.MapPost("/todos", Func<string, string, Tasks.Task>(
            fun title description -> Tasks.create (title, description)
        )) |> ignore
        app.MapPut("/todos/{id}", Func<int, string, string, string>(
            fun id title description -> Tasks.update (id, title, description)
        )) |> ignore
        app.MapDelete("/todos/{id}", Func<int, string>(fun (id) -> Tasks.delete id)) |> ignore

        app.Run()

        0 // Exit code
