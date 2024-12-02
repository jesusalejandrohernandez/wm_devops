FROM jenkins/jenkins:2.479.2-jdk17
USER root
RUN apt-get update && apt-get install -y lsb-release

RUN curl -fsSLo /usr/share/keyrings/docker-archive-keyring.asc \
  https://download.docker.com/linux/debian/gpg

RUN echo "deb [arch=$(dpkg --print-architecture) \
  signed-by=/usr/share/keyrings/docker-archive-keyring.asc] \
  https://download.docker.com/linux/debian \
  $(lsb_release -cs) stable" > /etc/apt/sources.list.d/docker.list

RUN apt-get update && apt-get install -y \
    docker-ce-cli \
    wget \
    apt-transport-https \
    software-properties-common && \
    rm -rf /var/lib/apt/lists/*

# Agregar el repositorio de .NET 9
RUN wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && apt-get install -y dotnet-sdk-9.0 && \
    rm -f packages-microsoft-prod.deb && \
    rm -rf /var/lib/apt/lists/* && \
    dotnet tool install --global dotnet-sonarscanner

USER jenkins

RUN jenkins-plugin-cli --plugins "blueocean docker-workflow"