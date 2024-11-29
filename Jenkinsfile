pipeline {
    agent any

    environment {
        SONAR_PROJECT_KEY = 'DevOps'
        SONAR_PROJECT_NAME = 'DevOps'
        SONAR_PROJECT_VERSION = '1.0'
        SONAR_AUTH_TOKEN = credentials('sonar-token')
    }

    stages {
        stage('Prepare Analysis') {
            steps {
                withSonarQubeEnv('SonarQube') {
                    sh "export PATH=\"$PATH:$HOME/.dotnet/tools\""
                    sh "dotnet sonarscanner begin /k:\"${SONAR_PROJECT_KEY}\" /n:\"${SONAR_PROJECT_NAME}\" /v:\"${SONAR_PROJECT_VERSION}\" /d:sonar.login=\"${SONAR_AUTH_TOKEN}\""
                }
            }
        }

        // stage('Restaurar Dependencias') {
        //     steps {
        //         sh "dotnet restore"
        //     }
        // }

        stage('Compilar Proyecto') {
            steps {
                sh "dotnet build"
            }
        }

        // stage('Ejecutar Pruebas') {
        //     steps {
        //         // Ejecuta las pruebas del proyecto
        //         dotnetTest project: 'ruta/al/archivo.sln', configuration: 'Release'
        //     }
        // }

        stage('End Analysis') {
            steps {
                withSonarQubeEnv('SonarQube') {
                    sh "dotnet sonarscanner end /d:sonar.login=${SONAR_AUTH_TOKEN}"
                   
                }
            }
        }

        stage('Quality Gate') {
            steps {
                script {
                    // Espera el resultado del Quality Gate de SonarQube
                    timeout(time: 5, unit: 'MINUTES') {
                        waitForQualityGate abortPipeline: true
                    }
                }
            }
        }
    }
}
