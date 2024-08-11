# Testing RESTful Web Services 1/2 Day Workshop Code & Resources
## Prerequisites
For this workshop, you need the following knowledge:
* 101 level knowledge of C#
* 101 level Git knowledge (to manage with the repo and branches)

This workshop requires the following:
* Laptop with Windows (May work with Visual Studio for Mac, Postman for Mac etc but hasn't been tested)
* Visual Studio Community (or better) 2017 version 15.7.3 (https://www.visualstudio.com/downloads/)
* .NET Core SDK 2.1.300 (https://www.microsoft.com/net/download/visual-studio-sdks)
* Postman (https://www.getpostman.com/apps)
* Fork this repo & clone locally

# Have Some Cake With Your Frosting: Testing The UI and API Layers
## Prerequisites
For this workshop, no prior knowledge is required. However, the following are required to be installed:
* Docker (https://www.docker.com/community-edition)
* Chrome
* Postman (https://www.getpostman.com/apps)


## Resources
Some resources mentioned or useful during the workshops:
* Random string generator (https://goo.gl/sQ9Zej)
* http://unicodesnowmanforyou.com/
* Which Tests Should We Automate - Angie Jones (https://www.youtube.com/watch?v=VL-_pnICmGY)
* Chrome Dev Tools Network Reference
 (https://developers.google.com/web/tools/chrome-devtools/network-performance/reference)
* Danny Dainton's excellent Postman resources (https://github.com/DannyDainton/All-Things-Postman)
* Testing Heuristics Cheat Sheet (http://testobsessed.com/wp-content/uploads/2011/04/testheuristicscheatsheetv1.pdf)
* Slides for the workshops are in the Resources folder of this repo


## To Use This Application In Docker
The application can also be used in Docker, for instance for the Workshop "Have Some Cake With Your Frosting"
* Install and setup Docker (https://www.docker.com/community-edition)
* If using Docker on Windows, set Docker to use Linux Containers - either during installation or by right-clicking on the Docker icon in the system tray and choosing Switch to Linux Containers (you can switch back after the workshop)
* Verify your installation (https://docs.docker.com/get-started/#test-docker-installation)
* Run the following command from a command line anywhere on your computer
`docker run -p 8080:80 --name myapp g33klady/todoapi:latest`
This will get the latest docker image with this code running on your machine

To see what is running in Docker
`Docker ps`

To stop a Docker image
`Docker stop myapp`

To start a Docker image back up
`Docker start myapp`

Once Docker is running, application can be accessed:
http://localhost:8080

### Note
You'll need to provide a header with key "CanAccess" and value "true" to use the API via Postman. When using the Swagger specification, select the Authorization button and enter the value "true".

## Automated Test Samples
To view some automated tests written in C#, review the branch "Exercise 5".
To view the above automated tests using RestSharp, review the branch "RestSharp".

## Automated Tests of TestStrategy Testpyramid example
To view some automated tests written in C#, review the folder "Teststrategies/Testpyramid".

## Cypress Tests ausführen

### Voraussetzungen
- Node.js und npm müssen installiert sein. Sie können Node.js [hier](https://nodejs.org/) herunterladen und installieren.

### Installation
1. Navigate to your project path.
2. Install the necessary npm packages:
   ```bash
   npm install

### Execute Cypress tests
1. Navigate to path Teststrategies/Cypress
2. Execute the following command to open Cypress:
   ```bash
   npx cypress open
3. (optional) Execute the following command to start a headless test run:
   ```bash
   npx cypress run

# Deutsche Übersetzung

# Testen von RESTful Web Services 1/2 Tages Workshop Code & Ressourcen
## Voraussetzungen
Für diesen Workshop benötigen Sie folgendes Wissen:
* Grundkenntnisse in C# (101-Level)
* Grundkenntnisse in Git (101-Level), um mit dem Repository und den Branches umzugehen

Dieser Workshop erfordert folgendes:
* Laptop mit Windows (könnte auch mit Visual Studio für Mac, Postman für Mac usw. funktionieren, wurde aber nicht getestet)
* Visual Studio Community (oder besser) Version 2017 15.7.3 (https://www.visualstudio.com/downloads/)
* .NET Core SDK 2.1.300 (https://www.microsoft.com/net/download/visual-studio-sdks)
* Postman (https://www.getpostman.com/apps)
* Forken Sie dieses Repository und klonen Sie es lokal

# Have Some Cake With Your Frosting: Testen der UI- und API-Schichten
## Voraussetzungen
Für diesen Workshop ist kein Vorwissen erforderlich. Es müssen jedoch die folgenden Programme installiert sein:
* Docker (https://www.docker.com/community-edition)
* Chrome
* Postman (https://www.getpostman.com/apps)


## Ressourcen
Einige während der Workshops erwähnte oder nützliche Ressourcen:
* Zufälliger Zeichenketten-Generator (https://goo.gl/sQ9Zej)
* http://unicodesnowmanforyou.com/
* Which Tests Should We Automate - Angie Jones (https://www.youtube.com/watch?v=VL-_pnICmGY)
* Chrome Dev Tools Network Reference
 (https://developers.google.com/web/tools/chrome-devtools/network-performance/reference)
* Danny Daintons hervorragende Postman-Ressourcen (https://github.com/DannyDainton/All-Things-Postman)
* Testing Heuristics Cheat Sheet (http://testobsessed.com/wp-content/uploads/2011/04/testheuristicscheatsheetv1.pdf)
* Die Folien für die Workshops befinden sich im Ordner „Resources“ dieses Repositories


## Verwendung dieser Anwendung in Docker
Die Anwendung kann auch in Docker verwendet werden, zum Beispiel für den Workshop "Have Some Cake With Your Frosting".
* Installieren und richten Sie Docker ein (https://www.docker.com/community-edition)
* Wenn Docker unter Windows verwendet wird, stellen Sie Docker auf die Verwendung von Linux-Containern ein - entweder während der Installation oder durch Rechtsklick auf das Docker-Symbol in der Taskleiste und Auswahl von "Switch to Linux Containers" (Sie können nach dem Workshop wieder zurückwechseln)
* Überprüfen Sie Ihre Installation (https://docs.docker.com/get-started/#test-docker-installation)
* Führen Sie den folgenden Befehl über eine Kommandozeile an einem beliebigen Ort auf Ihrem Computer aus:
`docker run -p 8080:80 --name myapp g33klady/todoapi:latest`
Dies lädt das neueste Docker-Image mit diesem Code und führt es auf Ihrem Computer aus.

Um zu sehen, was in Docker läuft:
`docker ps`

Um ein Docker-Image zu stoppen:
`docker stop myapp`

Um ein Docker-Image erneut zu starten:
`docker start myapp`

Sobald Docker läuft, kann auf die Anwendung zugegriffen werden:
http://localhost:8080

### Hinweis
Sie müssen einen Header mit dem Schlüssel "CanAccess" und dem Wert "true" bereitstellen, um die API über Postman zu verwenden. Wenn Sie die Swagger-Spezifikation verwenden, wählen Sie die Schaltfläche "Authorization" und geben Sie den Wert "true" ein.

## Beispiele für automatisierte Tests
Um einige automatisierte Tests in C# zu sehen, überprüfen Sie den Branch "Exercise 5".
Um die oben genannten automatisierten Tests mit RestSharp zu sehen, überprüfen Sie den Branch "RestSharp".

## Automatisierte Tests des TestStrategy Testpyramide-Beispiels
Um einige automatisierte Tests in C# zu sehen, überprüfen Sie den Ordner "Teststrategies/Testpyramid".

## Cypress Tests ausführen

### Voraussetzungen
- Node.js und npm müssen installiert sein. Sie können Node.js [hier](https://nodejs.org/) herunterladen und installieren.

### Installation
1. Navigieren Sie zu Ihrem Projektpfad.
2. Installieren Sie die notwendigen npm-Pakete:
   ```bash
   npm install

### Ausführen der Cypress Tests
1. Navigieren Sie zum Pfad Teststrategies/Cypress
2. Führen sie den folgenden Befehl aus um Cypress zu öffnen:
   ```bash
   npx cypress open
3. (optional) Führen sie den folgenden Befehl aus um einen headless test run zu starten:
   ```bash
   npx cypress run