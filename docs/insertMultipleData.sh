curl --location --request POST 'http://localhost:8080/api/Sales/PostSale' \
--header 'Content-Type: application/json' \
--data-raw '{
  "ProductId": 1,
  "ProductName": "Strawberry",
  "Quantity": 500,
  "Price": 2,
  "Date": "2022-09-06T17:33:35.645Z"
}'

curl --location --request POST 'http://localhost:8080/api/Sales/PostSale' \
--header 'Content-Type: application/json' \
--data-raw '{
  "ProductId": 2,
  "ProductName": "Apple",
  "Quantity": 76,
  "Price": 2,
  "Date": "2022-09-06T17:33:35.645Z"
}'

curl --location --request POST 'http://localhost:8080/api/Sales/PostSale' \
--header 'Content-Type: application/json' \
--data-raw '{
  "ProductId": 3,
  "ProductName": "Banana",
  "Quantity": 200,
  "Price": 2,
  "Date": "2022-09-05T17:33:35.645Z"
}'

curl --location --request POST 'http://localhost:8080/api/Sales/PostSale' \
--header 'Content-Type: application/json' \
--data-raw '{
  "ProductId": 4,
  "ProductName": "Pineapple",
  "Quantity": 15,
  "Price": 2,
  "Date": "2022-09-05T17:33:35.645Z"
}'

curl --location --request POST 'http://localhost:8080/api/Sales/PostSale' \
--header 'Content-Type: application/json' \
--data-raw '{
  "ProductId": 5,
  "ProductName": "Watermelon",
  "Quantity": 100,
  "Price": 2,
  "Date": "2022-09-04T17:33:35.645Z"
}'

curl --location --request POST 'http://localhost:8080/api/Sales/PostSale' \
--header 'Content-Type: application/json' \
--data-raw '{
  "ProductId": 6,
  "ProductName": "Lemon",
  "Quantity": 25,
  "Price": 2,
  "Date": "2022-09-04T17:33:35.645Z"
}'

curl --location --request POST 'http://localhost:8080/api/Sales/PostSale' \
--header 'Content-Type: application/json' \
--data-raw '{
  "ProductId": 7,
  "ProductName": "Peach",
  "Quantity": 20,
  "Price": 2,
  "Date": "2022-09-03T17:33:35.645Z"
}'

curl --location --request POST 'http://localhost:8080/api/Sales/PostSale' \
--header 'Content-Type: application/json' \
--data-raw '{
  "ProductId": 8,
  "ProductName": "Tomato",
  "Quantity": 50,
  "Price": 4,
  "Date": "2022-09-03T17:33:35.645Z"
}'

curl --location --request POST 'http://localhost:8080/api/Sales/PostSale' \
--header 'Content-Type: application/json' \
--data-raw '{
  "ProductId": 9,
  "ProductName": "Lettuce",
  "Quantity": 35,
  "Price": 3,
  "Date": "2022-09-02T17:33:35.645Z"
}'