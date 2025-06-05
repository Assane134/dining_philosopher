file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Dining.scala
### java.lang.AssertionError: assertion failed: position not set for nn(<empty>) # -1 of class dotty.tools.dotc.ast.Trees$Apply in C:/Users/sankara/Documents/Cours UM6P/cloud programming/dining_philo_prob/src/main/scala/Dining.scala

occurred in the presentation compiler.

presentation compiler configuration:


action parameters:
offset: 81
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Dining.scala
text:
```scala
package dining

object Dining extends App {

  val n = args.headOption.map(_.@@)
}

```



#### Error stacktrace:

```
scala.runtime.Scala3RunTime$.assertFailed(Scala3RunTime.scala:8)
	dotty.tools.dotc.typer.Typer$.assertPositioned(Typer.scala:72)
	dotty.tools.dotc.typer.Typer.typed(Typer.scala:3297)
	dotty.tools.dotc.typer.Applications.extMethodApply(Applications.scala:2483)
	dotty.tools.dotc.typer.Applications.extMethodApply$(Applications.scala:400)
	dotty.tools.dotc.typer.Typer.extMethodApply(Typer.scala:119)
	dotty.tools.dotc.typer.Applications.tryApplyingExtensionMethod(Applications.scala:2528)
	dotty.tools.dotc.typer.Applications.tryApplyingExtensionMethod$(Applications.scala:400)
	dotty.tools.dotc.typer.Typer.tryApplyingExtensionMethod(Typer.scala:119)
	dotty.tools.dotc.interactive.Completion$Completer.tryApplyingReceiverToExtension$1(Completion.scala:526)
	dotty.tools.dotc.interactive.Completion$Completer.$anonfun$23(Completion.scala:569)
	scala.collection.immutable.List.flatMap(List.scala:294)
	scala.collection.immutable.List.flatMap(List.scala:79)
	dotty.tools.dotc.interactive.Completion$Completer.extensionCompletions(Completion.scala:566)
	dotty.tools.dotc.interactive.Completion$Completer.selectionCompletions(Completion.scala:446)
	dotty.tools.dotc.interactive.Completion$.computeCompletions(Completion.scala:218)
	dotty.tools.dotc.interactive.Completion$.rawCompletions(Completion.scala:78)
	dotty.tools.pc.completions.Completions.enrichedCompilerCompletions(Completions.scala:114)
	dotty.tools.pc.completions.Completions.completions(Completions.scala:136)
	dotty.tools.pc.completions.CompletionProvider.completions(CompletionProvider.scala:139)
	dotty.tools.pc.ScalaPresentationCompiler.complete$$anonfun$1(ScalaPresentationCompiler.scala:150)
```
#### Short summary: 

java.lang.AssertionError: assertion failed: position not set for nn(<empty>) # -1 of class dotty.tools.dotc.ast.Trees$Apply in C:/Users/sankara/Documents/Cours UM6P/cloud programming/dining_philo_prob/src/main/scala/Dining.scala