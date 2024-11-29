pipeline {
  agent { label 'principal' }
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

    stage("paso 2"){
        steps {
            script {			
            sh "echo 'Hola Axity'"
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
