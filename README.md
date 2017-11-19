# Proposal : On automatic testing of web search engines

Web search engines are very important because they are the means by which people retrieve information from the World Wide Web. However, testing these web search engines is difficult because there are no test oracles. Moreover, it is labor-intensive to judge the relevance of search results for a large number of queries, and these relevance judgments may not be reusable since the Web data change all the time. Web search engines are becoming more and more important for people to search for information in the World Wide Web. Given a query, a good search engine should return desired search results that possess various properties such as relevance, authority, and freshness. Providing inadequate search results could mislead or dissatisfy users. Thus, the motivation of this project is to implement a test oracle for web search engines. Following paper will be implement:-

### On automatic testing of web search engines [1]

Data collection is the main challenge here. Moreover, authors have proposed seven matrixes for evaluating search oracle. Thus, it is also a challenge to implement all these matrixes within this short period of time. 

I am not sure about the extension of this paper. I think it will take whole time to implement the existing matrixes. Anyway, if I get time after the existing paper implementation, then I will go for the extension by studying new approaches. 

# What the base project did
Base project implemented following Metamorphic Relations for automatic testing of web search engines. 

## Missing Pages
* MR: MPSite
* MR: MPTitle
* MR: MPReverseJD
## Swapping Keywords
* MR: Universal SwapJD
* MR: SwapJD with Domain
## Ranking Drop with Domain
* MR: Top1Absent
* MR: Top5Absent

## What I did
I have implemented all the aforementioned MRs. However, the base project was tested against three search engines but I have tested my implemention against two search engines. Moreover, I have used different dataset. Most of the case I have achieved simillar result as base project.

## Extension
I was not able to extend the base project due to the time limitation. It took my whole time to replicate the base project.

## Dataset
Base project did not provide dataset along with it. Therefore, I had to collect the dataset. I have used following dataset:-
* http://hawttrends.appspot.com/api/terms/
* https://trends.google.com/trends/ (Hot queries of november)

## Instructions for running project
* Clone and build the project
* While building it should resolve all the dependencies from Nuget.
* Please check [GoogleHtml] (https://github.com/IITDU-AMIT-MSSE1044/course-project-dipongkor/tree/feature/GoogleHtml) branch for Google Search engine
* Please chech [BingHtml] (https://github.com/IITDU-AMIT-MSSE1044/course-project-dipongkor/tree/feature/BingHtml) branch for Bing Search engine

## Tradeoffs
NA

# References
[1] Xiang, Shaowen. "On automatic testing of web search engines." (2015).
