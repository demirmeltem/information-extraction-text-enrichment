#ELASTICSEARCH PERCOLATOR

## What is [ElasticSearch](https://www.elastic.co/)?
ElasticSearch is a search server. It is based on [Lucene] (https://lucene.apache.org/core/).

## What does [Percolator] (https://www.elastic.co/guide/en/elasticsearch/reference/1.3/search-percolate.html)?
The percolator allows one to register queries against an index, and then send percolate requests which include a doc,
getting back the queries that match on that doc out of the set of registered queries.

##Let's do a sample about Percolator:

-First, Download ElasticSearch, and then run elasticSearch.
	
- Second, you can download [Fiddler Web Debugger] (https://www.telerik.com/download/fiddler/fiddler4). I am using Fiddler Web Debugger, it is more clear then console. 
Or, you can use console but if you are using Windows, you need to download [cURL](https://curl.haxx.se/download.html).

- Third, we will apply one example from the official web page. I will apply according to the Fiddler Web Debugger.
					
	* Register a query in the percolator: 
	* Click Composer on Fiddler.
	* Choose PUT, enter this address "localhost:9200/my-index/.percolator/1"
	
	*Request Body:*  
						 
	
	{
		"query" : {
			"match" : {
            "message" : "bonsai tree"
			}
		}
    	}
					  	  
	  
	Then Execute.			
					
	* Match a document to the registered percolator queries:	
	  Choose GET, enter this address "localhost:9200/my-index/my-type/_percolate"
	  Request Body:    

					
    	{
		"doc" : {
			"message" : "A new bonsai tree in the office"
		}
	}

							
	Then Execute.
						
	*The given request will be like this : 
					
	{
		"took" : 19,
		"_shards" : {
			"total" : 5,
			"successful" : 5,
			"failed" : 0
		},
		"total" : 1,
		"matches" : [ 
				{
				  "_index" : "my-index",
				  "_id" : "1"
				}
		]
	}					
											
	-- It means, there is a one matching in the query. 


##What does Percolator Highlighting?
Allows highlight definitions to be included. The document being percolated is being highlight for each matching query. 
This allows you to see how each match is highlighting the document being percolated.

	*I will use same tool which is Fiddler Web Debugger. Follow steps at the below: 
	
	
***ADDING A QUERY TO THE PERCOLATOR:***

  QUERY 1:

	PUT - localhost:9200/my-index/.percolator/1
	
	{ 
		"query": { 
			"match" : { 
				"body" : "brown fox"  
			}   
		} 
	}

--------------------------------------------------------------------
   
   QUERY 2:
   
	PUT - localhost:9200/my-index/.percolator/2
	
	{ 
		"query": { 
			"match" : { 
				"body" : "lazy dog"  
			}   
		} 
	}

--------------------------------------------------------------------
	
***PERCOLATE REQUEST:***	

	GET - localhost:9200/my-index/my-type/_percolate
	
	{
		"doc" : {
			"body" : "The quick brown fox jumps over the lazy dog"
		},
		"highlight" : {
			"fields" : {
				"body" : {}
			}
		},
		"size" : 5
	}
	
--------------------------------------------------------------------

***PERCOLATE RESPONSE:***

	{
	   "took": 18,
	   "_shards": {
		  "total": 5,
		  "successful": 5,
		  "failed": 0
	   },
	   "total": 2,
	   "matches": [
		  {
			 "_index": "my-index",
			 "_id": "1",
			 "highlight": {
				"body": [
				   "The quick <em>brown</em> <em>fox</em> jumps over the lazy dog"
				]
			 }
		  },
		  {
			 "_index": "my-index",
			 "_id": "2",
			 "highlight": {
				"body": [
				   "The quick brown fox jumps over the <em>lazy</em> <em>dog</em>"
				]
			 }
		  }
	   ]
	}


