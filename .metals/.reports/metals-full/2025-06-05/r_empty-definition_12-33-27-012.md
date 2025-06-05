error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/build.sbt:
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/build.sbt
empty definition using pc, found symbol in pc: 
empty definition using semanticdb
empty definition using fallback
non-local guesses:

offset: 109
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/build.sbt
text:
```scala
name := "akka-quickstart-scala"

version := "1.0"

scalaVersion := "2.13.16"

resolvers += "Akka library repo@@sitory".at("https://repo.akka.io/maven")

lazy val akkaVersion = sys.props.getOrElse("akka.version", "2.10.6")

// adding postfixOps 
scalacOptions ++= Seq("-language:postfixOps")

// Run in a separate JVM, to make sure sbt waits until all threads have
// finished before returning.
// If you want to keep the application running while executing other
// sbt tasks, consider https://github.com/spray/sbt-revolver/
fork := true

libraryDependencies ++= Seq(
  "com.typesafe.akka" %% "akka-actor-typed" % akkaVersion,
  "com.typesafe.akka" %% "akka-actor"         % akkaVersion,
  "com.typesafe.akka" %% "akka-remote"        % akkaVersion,  // Artery is in here for 2.8.x
  "com.typesafe.akka" %% "akka-slf4j"         % akkaVersion,
  "ch.qos.logback" % "logback-classic" % "1.5.8",
  "com.typesafe.akka" %% "akka-actor-testkit-typed" % akkaVersion % Test,
  "org.scalatest" %% "scalatest" % "3.2.15" % Test)
  

```


#### Short summary: 

empty definition using pc, found symbol in pc: 