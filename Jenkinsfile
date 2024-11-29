pipeline {
    agent any
    // agent { label 'principal' }
    environment {
        appName = "variable" 
    }
    tools {
        sonarQube 'SonarScanner for .NET'
    }
    stages {
        stage("paso 1"){
            steps {
                script {			
                    sh "echo 'hola mundo'"
                }
            }
        }
        stage('Sonar Scan .NET') {
            steps {
                withSonarQubeEnv('SonarQube for .NET') {
                    sh "dotnet sonarscanner begin /k:\"DevOps\" /d:sonar.login=${SONAR_AUTH_TOKEN}"
                    sh "dotnet build"
                    sh "dotnet sonarscanner end /d:sonar.login=${SONAR_AUTH_TOKEN}"
                }
            }
        }
    }
    post {
        always {          
            deleteDir()
            sh "echo 'fase always'"
        }
        success {
            sh "echo 'fase success'"
        }

        failure {
            sh "echo 'fase failure'"
        }   
    }
}
