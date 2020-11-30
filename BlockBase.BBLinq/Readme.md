# BBLinq

## Annotations
Annotations are used along with models to identify certain characteristics of a table or field. To be updated...
* Encripted - Defines a field as encrypted
* Field- Defines a property as a field
* Foreign Key - Defines a property as a foreign key to another table
* Primary Key - Defines a property as a primary key
* Range - defines the field as an encrypted range
* Table - Defines the class as a table

## Builders
Builders are used to build queries and other elements necessary on BBLinq
* SqlQueryBuilder - a simple builder with methods to clear a query, append content to it and return a query as a string. It is possible to bridge with other QueryBuilders through the "Append" method, if needed as long as the other query builder extends from SqlQueryBuilder.

## Context
The context is used to store data related to the database connection and other elements that can be reused. It uses a singleton
to keep the data cached.
* ContextCache - a <string, object> dictionary. It has no restrains about its use, but it is thread-safe.
* DbContext - base context. Since it's related to elements such as the dictionary, the query builder, the query executor, and the settings, these types are all part of the context's generic type.

## Dictionaries
Dictionaries contain expressions used to build queries. 

* SqlDictionary - keeps all the words that correspond to an expression and rarely change.

## Enums
Assist in defining possible values for a certain condition (enumerables)

* BbSqlType - the possible data types.

## Helpers
Helpers assist in Http requests, signatures and encryption

## Interfaces
Interfaces help to build extensive classes such as joins and sets.

* 

## Query Executors

## Results

* Response - When a query is executed, this type parses it. It's used as an intermediary between a string response and a QueryResult.
* QueryResult - Typed response with additional info to be returned.

## Sets
Sets are used as a base to perform operations over the data.

## Settings
The settings are used to describe the connection to a node, which may be a BlockBase node or not.



