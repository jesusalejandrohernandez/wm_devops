pipeline {
    agent any
    // agent { label 'principal' }
    environment {
        appName = "variable" 
    }
    stages {
        stage("paso 1"){
            steps {
                script {			
                    sh "echo 'hola mundo'"
                }
            }
        }
        stage('SonarScanner for .NET') {
            def scannerHome = tool 'SonarScanner for .NET'
            withSonarQubeEnv() {
                sh "dotnet ${scannerHome}/SonarScanner.MSBuild.dll begin /k:\"DevOps\""
                sh "dotnet build"
                sh "dotnet ${scannerHome}/SonarScanner.MSBuild.dll end"
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
