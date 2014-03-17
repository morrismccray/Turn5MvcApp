Turn5MvcApp
===========

DESCRIPTION:


Build a simple search page using ASP.NET MVC 4 (C#). What you build is entirely up to you, however, the application should contain a model, controller, and view and meet the requirements. You may spend as much time as you feel comfortable spending within the allotted time constraints.

TOOLBOX:

During this exercise, please use the following:

•	ASP.NET MVC 4 
•	jQuery (Feel free to use the Google CDN by including this tag in your markup <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>)
•	Git (create an account on github.com, or use existing)

You may submit your code by referencing your github repository.

REQUIREMENTS:


Frontend:

•	Markup must be valid, semantic HTML5
•	The page must contain a form with text box for the user to search, and a button to submit
•	The form must contain simple javascript validation to ensure the text box is not empty and contains at least 3 characters 
•	The form must submit to the controller asynchronously ( feel free to use jQuery's ajax function for this as it will save you time )
•	Errors must be handled and a friendly message displayed to the user should one be encountered
•	The page must display the search results in a user-friendly way ( "Name", "SKU", "Price", "Manufacturer", and "Description" are required )
•	The page must allow for sorting the results. Valid sort options and values are below: [refers to API parameter (sort_by_field)]
			Option						Value
			Name Ascending				Name:ASC
			Name Descending				Name:DESC
			Price Ascending				Price:ASC
			Price Descending				Price:DESC
			Relevance (default, no value)
•	Use CSS to style the page. It doesn't need to win any beauty pageants, but shouldn't be a plain white page either. Simple styling is fine here. It’s your search page, do whatever you’d like.


Backend:

•	Must handle the request to render the view
•	Must handle the call to the search client (see API notes on page 3)
•	Must handle parsing the XML response returned by the client 
•	Must store the following in a database:
1.	Session id ( some unique string, doesn't actually have to be a real session id )
2.	Keywords searched ( the terms the user searched for )
3.	The "Name"s of the first 10 results returned from the search client
•	Note: We don't expect you to create the database, just hook it up as you would normally, but leave connection string blank. Use whatever table structure you feel makes the most sense.

Search API:


URL:			http://api.americanmuscle.com/search/get
Method:		GET
Parameters:		
			(string) keywords  (required, minimum length 3)
			(int) page (optional, defaults to 1 if not passed)
			(string) sort_by_field (optional, ENUM( 'Price:ASC', 'Price:DESC', 'Name:ASC', 'Name:DESC' ), defaults to empty if not passed )
				
				
Sample request:	http://api.americanmuscle.com/search/get?keywords=catback+exhaust&sort_by_field=Price:ASC
[Sample keywords: catback+exhaust,tuner,keychain  (pretty much anything on our website will work here)]


Response:		XML (text/xml)
Error Codes:	
•	400		occurs due to a bad request (invalid parameters, etc)
•	500		occurs due to a fatal server error 


The response returns a bunch of stuff that you will not use. The important stuff is outlined below:


Total results: 		xml->pagination->total_products
Items per page:	25 (this is hardcoded, so you'll always get <= 25 results )
Current page:		xml->pagination->current_page (page parameter if passed)
Total pages:		xml->pagination->total_pages
Results:		xml->results
Name:			xml->results->result->Name
SKU:			xml->results->result->Sku
Price:			xml->results->result->Price
Manufacturer:		xml->results->result->Manufacturer
Description:		xml->results->result->Description

