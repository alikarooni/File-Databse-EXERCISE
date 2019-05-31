Here you will find three different approach to the same question, each of which has pros and cons.

3 Context File :
	
	JSONContext : A JSON file is loaded to the memory, filled with a new Record and saved as a text file. Not a benefitial approach, but easy to imlement.
	
	Binarycontext : A binary file do not need to load into memory. Also, maximum ID is keeped in the beginning of the file for when we need to find the maximum ID. In this case we do not need to iterate through whole data file to find maximum ID. But for seeking through each record we need to do 4 reading (each for one item in the Record Class). 
	
	SerializedBinaryContext : A serialized Binary Context do not need to load into memory as well as can cast to a Record Class.