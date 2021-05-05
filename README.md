# :link:BBLinq
The current state of ORMs does not allow interaction between an application and a database when there's a piece of software acting as a middle-man, 
such as BlockBase. BBLinq solves this issue by providing a fluent API able to produce, sign and emit database queries through HTTP, lin*q*ing you 
database, BlockBase node and application together.

This repository contains the current build (v0.0.2). For the latest working version, [click here](https://github.com/blockbasenetwork/bblinq/tree/v0.0.1)

To provide you with a working example, we'll be using a simple database model that represents a score system. It's composed by entities representative of players, scores and games.

## Setting up a context
You'll want to avoid a constant connection to the node / database. Therefore, all contexts are disposable, so that you only use them when needed.
First, create a class that represents your context. It should extend from BlockBaseContext, as seen on the following code snipped

```csharp


```

## Operations

### Insert
Insert one or more records on a table.

```csharp

```

### Update
Update one or more records on a table.


### Delete
Delete one of more records from a table. After setting up your context, you can perform delete operations on a set. 
Much like all other operations, you can create a query object through a Delete method, execute it through DeleteAsync,
or save it to the query batch through BatchDelete.

Deleting all the records on a table do not require any input.

```CSharp
context.Players.DeleteAsync();
```

You can also delete one record

```CSharp
context.Players.DeleteAsync(record);
```
If you want to delete more than one record based on certain conditions, you can use the Where funcion to complemente delete queries

```CSharp
context.Players.Where(player => player.Name == "player" ).DeleteAsync();
```




### Select
Select queries are by far the most complex queries you can build.