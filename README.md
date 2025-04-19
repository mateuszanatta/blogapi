# Blog Api

## Usage
1) Run from Visual Studio

2) Run from command line
```bash
cd blogapi
dotnet build
dotnet run --project src/API
```

Open your browser and paste the url `http://localhost:5107/swagger/index.html`

## Authentication
Authentication has the following flow
1) `POST /api/auth/register` register a new user
```
curl -X 'POST' \
  'http://localhost:5211/api/auth/register' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
    "name": "mateus",
    "email": "mateus",
    "password": "mateus"
  }'
```

2) `POST /api/auth/login` pass the user email and password in the request body

```
curl -X 'POST' \
  'http://localhost:5211/api/auth/login' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
    "email": "mateus",
    "password": "mateus"
  }'
```

## API Sample 
Since the JWT Token generate during authentication contains user information, we can avoid pass the user id back and forth and extract it from the token.
This way, we don't need to pass the user id through each endpoint.

### Account endpoints
`GET /api/posts` - Return a summarized list of all blog posts

Request example
```bash
curl -X 'GET' \
  'http://localhost:5107/api/posts' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImI2ZmY2OTJhLWU3ZGYtNDNkYi04OGY3LWMyYmE3ZTI5MzEzNiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdHJpbmciLCJleHAiOjE3NDUwODkyNzEsImlzcyI6IkdlbmVyaWNCbG9nIiwiYXVkIjoiQmxvZ1VzZXJzIn0.2esSR5CTU37QdIQ8rfMsn055j4rAaLjmp3wN5mq8-Wo'
```

Response body example
```json
[
  {
    "id": "33d2dc30-19e7-4092-96f7-59f6aaac4239",
    "userName": "string",
    "title": "First Blog Post",
    "totalComments": 1
  }
]
```

`POST /api/posts` - Create new blog post

Request example
```bash
curl -X 'POST' \
  'http://localhost:5107/api/posts' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImI2ZmY2OTJhLWU3ZGYtNDNkYi04OGY3LWMyYmE3ZTI5MzEzNiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdHJpbmciLCJleHAiOjE3NDUwODkyNzEsImlzcyI6IkdlbmVyaWNCbG9nIiwiYXVkIjoiQmxvZ1VzZXJzIn0.2esSR5CTU37QdIQ8rfMsn055j4rAaLjmp3wN5mq8-Wo' \
  -H 'Content-Type: application/json' \
  -d '{
  "title": "First Blog Post",
  "content": "First blog Post"
}'
```

Response body example
It will return a `201 Create` status
```json
{
  "content": "First blog Post",
  "comments": [],
  "id": "33d2dc30-19e7-4092-96f7-59f6aaac4239",
  "userName": "string",
  "title": "First Blog Post",
  "totalComments": 0
}
```
Also, along with the body, we return a `location` header with the endpoint to retrieve the created post
```bash
 content-type: application/json; charset=utf-8 
 date: Sat,19 Apr 2025 18:01:45 GMT 
 location: http://localhost:5107/api/posts/33d2dc30-19e7-4092-96f7-59f6aaac4239 
 server: Kestrel 
 transfer-encoding: chunked 
```


`GET /api/posts/{id}` - Retrieve a blog post by its ID

Request example
```bash
curl -X 'GET' \
  'http://localhost:5107/api/posts/33d2dc30-19e7-4092-96f7-59f6aaac4239' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImI2ZmY2OTJhLWU3ZGYtNDNkYi04OGY3LWMyYmE3ZTI5MzEzNiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdHJpbmciLCJleHAiOjE3NDUwODkyNzEsImlzcyI6IkdlbmVyaWNCbG9nIiwiYXVkIjoiQmxvZ1VzZXJzIn0.2esSR5CTU37QdIQ8rfMsn055j4rAaLjmp3wN5mq8-Wo'
```

Response body example
```json
{
  "content": "First blog Post",
  "comments": [],
  "id": "33d2dc30-19e7-4092-96f7-59f6aaac4239",
  "userName": "USerName",
  "title": "First Blog Post",
  "totalComments": 0
}
```

`POST /api/posts/{id}/comments` - Add a new comment to a given blog post

Request example
```
curl -X 'POST' \
  'http://localhost:5107/api/posts/33d2dc30-19e7-4092-96f7-59f6aaac4239/comments' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImI2ZmY2OTJhLWU3ZGYtNDNkYi04OGY3LWMyYmE3ZTI5MzEzNiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdHJpbmciLCJleHAiOjE3NDUwODkyNzEsImlzcyI6IkdlbmVyaWNCbG9nIiwiYXVkIjoiQmxvZ1VzZXJzIn0.2esSR5CTU37QdIQ8rfMsn055j4rAaLjmp3wN5mq8-Wo' \
  -H 'Content-Type: application/json' \
  -d '{
  "content": "New comment"
}'
```

Response body example
```json
{
  "userName": "USerName",
  "content": "New comment"
}
```

# Next steps
- Add Dockerfile and docker-compose to run the project in containers
- Have a better user registration/handling
   - Create roles for each user
   - Use an identity server to manage authentication/authorization
- Write unit/integration tests

# Known issues
When we load a Post that has comments from another user, we cannot load the user name