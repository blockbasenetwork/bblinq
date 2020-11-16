# BBLinq
Interacting with BlockBase databases just got easier. With ⛓BBLinq, you can write your .Net apps without having to deal with BBSQL syntax.

## Features

* **Simple and fluent API**
   You can keep adding elements to the same query without too many lines of code!
   &nbsp;

* **Allows join queries**
   You can easily perform a single query on seven tables!
   &nbsp;

* **Similar to Microsoft's Entity Framework**
  It's pretty straightforward if you've already used Entity Framework.
  &nbsp;

* **Disposable contexts**
    Our library only runs when you need it. After that, its just as if it never had existed.
      &nbsp;

* **Data Annotations**
  You database rules don't match your code rules? That's fine. Use our data annotations to match table and field names, data types and identify keys.
  &nbsp;

* **Easy to adapt to your needs**
  While it's still in its early stages, we are developing it in a way that you can adapt it to your needs! 💪

### Setting a Context

Create a Context based on the BbContext, adding the node address and the database name to the constructor's base. Include as many BbSets as you wish.

  ```csharp
  public class RailwayContext : BbContext
  {
      public RailwayContext() : base("node address", "database name")
      {
      }

      public BbSet<Customer> Customers { get; set; }
      public BbSet<Staff> Staff { get; set; }
      public BbSet<Station> Stations { get; set; }
      public BbSet<Train> Trains { get; set; }
      public BbSet<TrainStation> TrainStations { get; set; }
  } 
  ```


#### Performing Database Operations

##### Delete
```CSharp
  //Using a set
  using var ctx = new RailwayContext();
  var trainSet = new BbSet<Train>();
  var result = trainSet.DeleteAsync(customer);

  //On the context
  using var ctx = new RailwayContext();
  var result = ctx.Trains.DeleteAsync(customer);
```

##### Insert
```CSharp

  //Using a set
  using var ctx = new RailwayContext();
  var trainSet = new BbSet<Train>();
  trainSet.InsertAsync(customer);

  //On the context
  using var ctx = new RailwayContext();
  ctx.Customers.InsertAsync(customer);
```

##### List
```CSharp
  //Using a set
  using var ctx = new RailwayContext();
  var trainSet = new BbSet<Train>();
  trainSet.SelectAsync(customer).Result;

  //On the context
  using var ctx = new RailwayContext();
  var result = ctx.Customers.SelectAsync(customer).Result;
```

##### Update
```CSharp
  //Using a set
  using var ctx = new RailwayContext();
  var trainSet = new BbSet<Train>();
  trainSet.UpdateAsync(customer);

  //On the context
  using var ctx = new RailwayContext();
  var result = ctx.Customers.UpdateAsync(customer);
```

##### Join
```CSharp
  using var ctx = new RailwayContext();
  var trainSet = new BbSet<Train>();
  var trainTrainStationJoin = trainSet.Join<TrainStation>((train, trainStation) => train.Id == trainStation.StationId);
```

##### Select
```CSharp
  using var ctx = new RailwayContext();
  var trainSet = new BbSet<Train>();
  var trainTrainStationStationJoin = trainSet.Join<TrainStation>(
                                      (train, trainStation) => 
                                        train.Id == trainStation.StationId
                                      ).Join<Station>(
                                        (train, trainStation, station)=> station.Id == trainStation.StationId);
  
  //Performing a dynamic select
  var result = trainSet.SelectAsync(x => new {Code = x.TrainCode});

  //Performing a select with a condition
  var result = trainSet.Where(x => x.TrainLine == "").SelectAsync(x => new {Code = x.TrainCode});

  //Performing a typed result select
  var result = trainSet.SelectAsync<TestingTrain>(x => new TestingTrain(){Code = x.TrainCode});

  //Performing a joined select
  var result = trainTrainStationStationJoin.SelectAsync((train, trainStation, station) => new TestingTrain(){Code = train.TrainCode});

```