<div id="top"></div>

<!-- PROJECT SHIELDS -->
[![LinkedIn][linkedin-shield]][linkedin-url]

<h1>Chat Jobsity Challenge</h1>
<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

Chat in real time, multiroom
ASP.NET Core 5.0 - SignarR - RabbitMQ - MSSQLServer

<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [Asp.net Core 5.0](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-5.0)
* [Razor]([https://reactjs.org/](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-5.0&tabs=visual-studio))
* [SignalR]([https://vuejs.org/](https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-5.0))
* [MassTransit](https://angular.io/](https://masstransit-project.com/getting-started/)
* [RabbitMq](https://www.rabbitmq.com/#getstarted)
* [Entity Framework](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0)
* [MSSQLServer](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
* [Bootstrap](https://getbootstrap.com/)

<p align="right">(<a href="#top">back to top</a>)</p>

### Prerequisites

- Visual Studio 2019 +
- Net 5.0
- MSSQLSever 2016 +
- Erlang
- RabbitMq


### Installation

1. Make Sure RabbitMq is runnig (Use default ports or change the RabbitMq:Connection settings in Projects Bot and UI
2. Create Databases
  2.1. Set Starttup Project ChatJobsity.UI. 
  2.2. Run PM Console: Update-Database command pointing to ChatJobsity.Identity project
  2.3. Set Starttup Project ChatJobsity.Chat. 
  2.4. Run PM Console: Update-Database command pointing to ChatJobsity.Chat.Infraestructure project
4. Modify launch settings to run multiproject and select ChatJobsity.UI ChatJobsity.Chat and ChatJobsity.Bot
5. The project uses IISExpres to run, so the run ports might change depending the local enviroments port usage
6. Run projects
7. Make sure RabbitMq receive 2 connections

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->

[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: www.linkedin.com/in/joaquín-mejía-muñoz-8bb261186
[product-screenshot]: images/screenshot.png
