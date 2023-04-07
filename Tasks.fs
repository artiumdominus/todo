namespace Todo

module Tasks =
  type Task =
    { Id: int
      Title: string
      Description: string
      Status: int }

  let mutable tasks: Task list = []
  let mutable last_id = 0

  let list () = tasks

  let show (id: int) = List.find (fun t -> t.Id = id) tasks

  let create (title, description) =
    last_id <- last_id + 1

    let task =
      { Id = last_id
        Title = title
        Description = description
        Status = 0 }

    tasks <- tasks @ [task]
    task

  let rec private update_list (tasks: Task list) (updated_task: Task) =
    if tasks.Head.Id = updated_task.Id then
      updated_task :: tasks.Tail
    else
      tasks.Head :: (update_list tasks.Tail updated_task)

  let update (id: int, title, description) =
    let task = List.find (fun t -> t.Id = id) tasks

    let updated_task =
      { Id = task.Id
        Title = title
        Description = description
        Status = task.Status }
    
    tasks <- update_list tasks updated_task
    "ok"

  let delete (id: int) =
    tasks <- List.filter (fun (t: Task) -> t.Id <> id) tasks
    "ok"
