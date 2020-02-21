# dependencyinject_tutor
ini mah inject anesthesia

Create class Database using implementaion of npgsql `https://www.nuget.org/packages/Npgsql/` that have ability to create CRUD operation

```c#
class Database {

  read()
  
  create()
  
  update()
  
  delete()
}
```


Integrate Database class before with previous api service and add ability to create crud request with api 
Impelement Depedency Injection for this task, this is minimal requirement table to do this task 


# Group 1
- Mia Huljanah	
- Rashif Ilmi Nurzaman
- Hardo Fernando Silalahi

## Post Table

| name        | key|
|-------------|----|
| id          | PK |
| title       |    |
| content     |    |
| tags        |    |
| status      |    |
| create_time |    |
| update_time |    |

## Post REST API URL


| url     | method | params |
|---------|--------|--------|
| post    | POST   |        |
| post/id | PATCH  | id     |
| post/id | DELETE | id     |
| post    | GET    |        |
| post/id | GET    | id     |
