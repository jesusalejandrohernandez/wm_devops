# Install

## Jenkins Docker Windows

Intalacion de Jenkins en Docker Desktop

1. Crea la red

``` bash
docker network create jenkins
```

2. Crea contenedor para ejecutar contenedores Docker

``` bash
docker run --name jenkins-docker --rm --detach ^
--privileged --network jenkins --network-alias docker ^
--env DOCKER_TLS_CERTDIR=/certs ^
--volume jenkins-docker-certs:/certs/client ^
--volume jenkins-data:/var/jenkins_home ^
--publish 2376:2376 ^
docker:dind
```

3. Crea el contenedor Jenkinks

``` bash
docker build -t jenkins-walmart .

docker run --name jenkins-blueocean --restart=on-failure --detach ^
--network jenkins --env DOCKER_HOST=tcp://docker:2376 ^
--env DOCKER_CERT_PATH=/certs/client --env DOCKER_TLS_VERIFY=1 ^
--volume jenkins-data:/var/jenkins_home ^
--volume jenkins-docker-certs:/certs/client:ro ^
--publish 8080:8080 --publish 50000:50000 jenkins-walmart
```

#### Desbloqueo

Cuando accede por primera vez a un nuevo controlador Jenkins, se le solicita que lo desbloquee usando una contraseña generada automáticamente.

Busque http://localhost:8080 y espere hasta que aparezca la página Desbloquear Jenkins

## Sonarqube Docker Windows

Intalacion de Sonarqube Community en Docker Desktop

``` bash
docker pull sonarqube
```

``` bash
docker run -d --name sonarqube ^
--network jenkins ^
--publish 9000:9000 ^
sonarqube:latest
```

```
sudo apt update
sudo apt install wget -y
wget --version
```

```
wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
```