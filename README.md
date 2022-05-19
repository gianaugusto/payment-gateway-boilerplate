# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

## Getting Started

Make sure you have [installed](https://docs.docker.com/docker-for-windows/install/) and configured docker in your environment. After that, you can run the below commands from the **/src/** directory and get started with the `payment-gateway-boilerplate` immediately.

```powershell
docker-compose build
docker-compose up
```

You should be able to browse different components of the application by using the below URLs :

```
Web Api Gateway : http://localhost:7074/
Log Dashboard   : http://localhost:9000/
```

# Basic Workflow

## Synchronous Flow ##
<img src="https://www.plantuml.com/plantuml/png/NOzD2W8n38NtEKKku1Lah0LnuI5Y9s2nTagJGRoz2HN5wQBlu_rGEebIr_LHe9dcb1jLnGltT7CWmMF9NFKlFf7oarw7bhrW0ccfsfU2VWBLktcE7fz5LkumazALXDsDF_P6Y30UmTqUVGbsEOyfmj1j6k8NFtEOkCxsbpS0" alt="overview architecture" title="overview architecture" height="300" />

<br>

## Asynchronous Flow ##

<img src="https://www.plantuml.com/plantuml/png/LP1H2i8m38RVSufUm2l8h0DHy11PLxJRJ4UR8TxU_bIAoqFpqIz_AEsoSjBjjT1QUd1XPL5pzyaz0PpDUGbUTASlhFxDdzCQC53QpbfsyoF5JSPR731W9t9HDCN5e2pBZ2Ygs6lEcqyqilB0sUEs_sFaUwBw2dCu6aG9Hs7RDwm3JKo6x0JwhQ-jgcgm5j2YdPUnVCGN" alt="overview architecture" title="overview architecture" height="300" />

<br>
<br>


## Auth & samples

To be able to request endpoints you need take in consideration JWT Token, as a valid example you can generate using the following code

```console
curl --location --request POST 'https://gianaugusto.eu.auth0.com/oauth/token' \
--header 'content-type: application/json' \
--data-raw '{
    "client_id": "V8sR1eyjsbaWn6haiYABFhX73KyQg3qP",
    "client_secret": "lZlfZ9_VbIE58EVTgMYCr7dymOPz-ZU_G3HhWqLvtpyiYIFIvxyeZfMBS7uKXFfM",
    "audience": "https://localhost:7074/",
    "grant_type": "client_credentials",
    "scope":"pay:read-write"
}'
```

```console
curl --location --request POST 'https://localhost:7074/api/v1/merchants/E659D58B-9EF6-4E78-8EB0-71E7AE88A701/payments' \
--header 'accept: */*' \
--header 'Content-Type: application/json' \
--header 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IlFrWkZORFV4TXpVMlFrVXpOVGc0UXprMk4wRkdOVEkwTWtZNE1EUTFNREZGT0VRMU9FVkVNUSJ9.eyJpc3MiOiJodHRwczovL2dpYW5hdWd1c3RvLmV1LmF1dGgwLmNvbS8iLCJzdWIiOiJWOHNSMWV5anNiYVduNmhhaVlBQkZoWDczS3lRZzNxUEBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA3NC8iLCJpYXQiOjE2NTMwMDAzMjgsImV4cCI6MTY1MzA4NjcyOCwiYXpwIjoiVjhzUjFleWpzYmFXbjZoYWlZQUJGaFg3M0t5UWczcVAiLCJzY29wZSI6InBheTpyZWFkLXdyaXRlIiwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIn0.KGiU-GDrMhN1MGvFiI1fvnk8435IJPKIzl_C2iv5o9pXUbQjt1L0eyTDNRzAmy33Pk6bJz-w7k4WyjCYXxFBd-R0nCN0dyeorn8MPcn-TNBi8f-9HOJvGE30aTMNqLd756qX4DLkb_QX4nGwBt2L2l9NeoATbHVfkhax2sGAU-2cI1guAYBKE70pe2dTOdh-fkZqQ8--unVrI8RFBlej2pCieq4jCgLduHRvzGAGU8zjReT9iAZgRyr15hIuS5VkIkhPpizBXNH76Zpq3f6UjNWDc6Ui_JqatCfXwn9rlTOKbZsd9GjP6MZCbb_zuVx2n0-fVcWVKIENLC5k8kaLQg' \
--data-raw '{
  "merchantId": "E659D58B-9EF6-4E78-8EB0-71E7AE88A701",
  "createDate": "2022-05-17T13:21:47.177Z",
  "paymentType": "string",
  "amount": 200,
  "currency": "USD",
  "reference": "RD135",
  "description": "Teste save ",
  "successUrl": "http://teste.local/success?id=",
  "failureUrl": "http://teste.local/error?id=",
  "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "source": {
    "expiryMonth": 10,
    "expiryYear": 2023,
    "last4": "0832",
    "issuer": "Santander",
    "billingAddress": "Rua da alegria 1500 Porto Portugal",
    "token": "teste 123"
  }
}'
```

# Build and Test
To build or test you can easily to use visual studio or command line to executing commands bellow

Building

```powershell
docker-compose build
```

Testing
```powershell
docker-compose up integration-tests
```

# Contribute

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
