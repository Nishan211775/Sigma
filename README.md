Future implementation
1. Instead of in memory cache we can use radish cache server
2. Make base repository so that we can put common function like Insert Update Delete and Get on BaseRepository and extend it by other class so that we dont have to write same query for all repository
3. Implement Pagination properly while getting data. Current it will return the data if we provide page number and row per page. But in the future we may extend the api to get the total rows of that query as well
4. We can use unit of work for the better transaction
5. For the protection of our api we can use authentication for it. For example identity server can be used to authentication and authorization
6. 



Total Time Spend to Complete the Task : 8 hours
