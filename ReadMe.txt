
1. Download/Clone the repository to local machine
2. Open Exchange.sln in VS2017
3. Build the solution Exchange
4. Run solution Exchange
5. In Postman/SOAPUI sent request:

GET
1) option with 3 dates
http://localhost:54707/api/?dates=2018-02-01&dates=2018-02-15&dates=2018-03-01&currencyFrom=SEK&currencyTo=NOK

Expecting response in Postman:

A min rate of 0,9546869595 on 2018-03-01
A max rate of 0,9815486993 on 2018-02-15
An average rate of 0,970839476467

2) option with 20 dates
http://localhost:54707/api/?dates=2018-02-01&dates=2018-02-02&dates=2018-02-03&&dates=2018-02-04&dates=2018-02-05&dates=2018-02-06&dates=2018-02-07&dates=2018-02-08&dates=2018-02-09&dates=2018-02-10&dates=2018-02-11&dates=2018-02-12&dates=2018-02-13&dates=2018-02-14&dates=2018-02-15&dates=2018-02-16&dates=2018-02-17&dates=2018-02-18&dates=2018-02-19&dates=2018-02-20&dates=2018-02-21&dates=2018-02-22&dates=2018-02-23&dates=2018-02-24&dates=2018-02-25&dates=2018-02-26&dates=2018-02-27&dates=2018-02-28&currencyFrom=SEK&currencyTo=NOK

Expecting response in Postman:

A min rate of 0,9527362445 on 2018-02-28
A max rate of 0,9852686831 on 2018-02-09
An average rate of 0,973168749765