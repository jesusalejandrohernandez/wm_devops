pipeline {
    agent any

    environment {
        // Variables para el proyecto SonarQube
        SONAR_PROJECT_KEY = 'DevOps'
        SONAR_PROJECT_NAME = 'DevOps'
        SONAR_PROJECT_VERSION = '1.0'
        SONAR_AUTH_TOKEN = credentials('SONAR_TOKEN') // Credencial tipo Secret Text en Jenkins
    }

    stages {
        stage('Prepare Analysis') {
            steps {
                // Inicia el análisis SonarQube
                withSonarQubeEnv('SonarQube') {
                    sh '''
                        export PATH="$PATH:$HOME/.dotnet/tools"
                        dotnet sonarscanner begin /k:"${SONAR_PROJECT_KEY}" \
                        /n:"${SONAR_PROJECT_NAME}" \
                        /v:"${SONAR_PROJECT_VERSION}" \
                        /d:sonar.login="${SONAR_AUTH_TOKEN}"
                    '''
                }
            }
        }

        stage('Restaurar Dependencias') {
            steps {
                // Restaura las dependencias del proyecto
                dotnetRestore project: 'ruta/al/archivo.sln'
            }
        }

        stage('Compilar Proyecto') {
            steps {
                // Compila el proyecto utilizando el plugin .NET SDK Support
                dotnetBuild project: 'ruta/al/archivo.sln', configuration: 'Release'
            }
        }

        stage('Ejecutar Pruebas') {
            steps {
                // Ejecuta las pruebas del proyecto
                dotnetTest project: 'ruta/al/archivo.sln', configuration: 'Release'
            }
        }

        stage('End Analysis') {
            steps {
                // Finaliza el análisis SonarQube
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
