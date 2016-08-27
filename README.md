# AwesomeCalendar
Required features are:

* scheduling events: one-time / full day / recurring on different intervals (same intervals that Google is using)

* editing scheduled events (one-time events / event series / single event in a series)

* displaying events on a calendar control (both one-time events and event series infinitely in the future)

* sending reminder emails about upcoming tasks

#Technologies
###Backend:
* ASP.NET (4.5)
* EasyNetQ
* Autofac
* Entity Framework
* Microsoft SQL Server
* xUnit
* Shoudly

###Frontend (not required):
* TypeScript
* Aurelia.io
* Karma.js
* Protractor.js


#Architecture
I'm going to implement a **CQRS** (Command Query Responsibility Segregation) with full **Event Sourcing**. If you're intrested in that conecept, I encourge you to visit my blog to get acquainted with that (http://foreverframe.pl/cqrses-1-a-bit-of-theory/). The diagram below presents whole backend architecture:
![cqrs1](https://cloud.githubusercontent.com/assets/7096476/16713438/1b706434-46a8-11e6-8e90-9ca5f0ce9f3e.png)


#Trello
You can follow my progress on **Trello**: (https://trello.com/b/rYiVDKLD/awesomecalendar#)
