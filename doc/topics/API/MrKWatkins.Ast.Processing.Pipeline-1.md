# Pipeline&lt;TNode&gt; Class
## Definition

A pipeline to process nodes in a tree. A pipeline consists of multiple named stages, each of which has one or more [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md)s running in serial or parallel. Stages can optionally specify whether pipeline processing should continue once the stage has completed. By default, processing will not continue if there are any errors in the tree.

```c#
public sealed class Pipeline<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The type of nodes in the tree. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Build(Action&lt;PipelineBuilder&lt;TNode&gt;&gt;)](MrKWatkins.Ast.Processing.Pipeline-1.Build.md) | Fluent interface to build a [Pipeline&lt;TNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md). |
| [Run(TNode)](MrKWatkins.Ast.Processing.Pipeline-1.Run.md#mrkwatkins-ast-processing-pipeline-1-run(-0)) | Runs the pipeline on the specified root node. |
| [Run(TNode, String)](MrKWatkins.Ast.Processing.Pipeline-1.Run.md#mrkwatkins-ast-processing-pipeline-1-run(-0-system-string@)) | Runs the pipeline on the specified root node. |

