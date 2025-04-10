pipeline {
  agent any

  environment {
    SONARQUBE_ENV = 'SonarQubeServer'
    SONAR_TOKEN = credentials('sonar-token')
    IMAGE_NAME = 'myapp:latest'
  }

  stages {
    stage('Checkout') {
      steps {
        git 'https://github.com/tuusuario/MyApp.git'
      }
    }

    stage('Restore & Build') {
      steps {
        sh 'dotnet restore'
        sh 'dotnet build --configuration Release'
      }
    }

    stage('Test with Coverage') {
      steps {
        sh '''
        dotnet test ./MyApp.Tests/MyApp.Tests.csproj \
          --no-build \
          --configuration Release \
          --collect:"XPlat Code Coverage" \
          --logger trx \
          --results-directory ./TestResults
        '''
      }
    }

    stage('Generate Coverage Report') {
      steps {
        sh '''
        dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.2.4
        export PATH="$PATH:/root/.dotnet/tools"
        reportgenerator \
          -reports:"**/coverage.cobertura.xml" \
          -targetdir:"coveragereport" \
          -reporttypes:Html
        '''
      }
    }

    stage('SonarQube Analysis') {
      steps {
        withSonarQubeEnv("${SONARQUBE_ENV}") {
          sh """
          dotnet sonarscanner begin \
            /k:"MyApp" \
            /d:sonar.host.url=$SONAR_HOST_URL \
            /d:sonar.login=$SONAR_TOKEN \
            /d:sonar.cs.opencover.reportsPaths=**/coverage.cobertura.xml

          dotnet build

          dotnet sonarscanner end /d:sonar.login=$SONAR_TOKEN
          """
        }
      }
    }

    stage('Build Docker Image') {
      steps {
        sh "docker build -t $IMAGE_NAME ."
      }
    }

    stage('Run Locally for Verification') {
      steps {
        sh '''
        docker stop myapp || true
        docker rm myapp || true
        docker run -d --name myapp -p 5000:80 $IMAGE_NAME
        '''
      }
    }
  }

  post {
    always {
      junit 'TestResults/**/*.trx'
    }
  }
}
