# DevSecOps

# Jenkins
```shell
docker build -t jenkins-axity:1.0 -f Dockerfile.Jenkins.Debian .
docker build -t jenkins-axity:1.0 -f Dockerfile.Jenkins.Alpine .
```

```shell
docker run --name jenkins-axity --restart=on-failure --detach `
--network jenkins --env DOCKER_HOST=tcp://docker:2376 `
--env DOCKER_CERT_PATH=/certs/client --env DOCKER_TLS_VERIFY=1 `
--volume jenkins-data:/var/jenkins_home `
--volume jenkins-docker-certs:/certs/client:ro `
--publish 8080:8080 --publish 50000:50000 jenkins-axity:1.0
```

http://localhost:8080/
Jenkins: admin
Pass: k7N7LA3BfPTz

# SonarQube Community
```shell
docker run --name sonarqube-axity -d -p 9000:9000 sonarqube:10.6-community
```
http://localhost:9000/
Sonar: admin
Pass: k7N7LA3BfPTz

# Install Win-kex Kali Linux
sudo apt update
sudo apt install kali-win-kex -y
kex win -s

## https://github.com/digininja/DVWA/blob/master/README.es.md
sudo apt install dvwa

sudo service mariadb start

sudo mariadb -u root -p

> create database dvwa;
> create user dvwa@localhost identified by 'p@ssw0rd';
> grant all on dvwa.* to dvwa@localhost;
> flush privileges;
