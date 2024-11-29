pipeline {
    agent any

    tools {
        // Define SonarScanner para MSBuild
        msbuildSonarQubeScanner 'SonarScanner for MSBuild'
    }

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
                        dotnet sonarscanner begin /k:"${SONAR_PROJECT_KEY}" \
                        /n:"${SONAR_PROJECT_NAME}" \
                        /v:"${SONAR_PROJECT_VERSION}" \
                        /d:sonar.login="${SONAR_AUTH_TOKEN}"
                    '''
                }
            }
        }

        stage('Build Solution') {
            steps {
                // Compilar solución .NET usando MSBuild
                script {
                    def msBuildPath = tool name: 'MSBuild', type: 'com.microsoft.jenkins.plugins.msbuild.MsBuildInstallation'
                    withEnv(["PATH+MSBUILD=${msBuildPath}/bin"]) {
                        sh 'msbuild /p:Configuration=Release'
                    }
                }
            }
        }

        stage('End Analysis') {
            steps {
                // Finaliza el análisis SonarQube
                withSonarQubeEnv('SonarQube') {
                    sh '''
                        dotnet sonarscanner end /d:sonar.login="${SONAR_AUTH_TOKEN}"
                    '''
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
