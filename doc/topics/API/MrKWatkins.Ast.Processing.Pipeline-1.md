# Pipeline&lt;TBaseNode&gt; Class
## Definition

A pipeline to process nodes in a tree. A pipeline consists of multiple named stages, each of which has one or more [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md)s running in serial or parallel. Stages can optionally specify whether pipeline processing should continue once the stage has completed. By default, processing will not continue if there are any errors in the tree.

```c#
public sealed class Pipeline<TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes in the tree. |

## Properties

| Name | Description |
| ---- | ----------- |
| [Stages](MrKWatkins.Ast.Processing.Pipeline-1.Stages.md) | The stages in the pipeline. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Build(Action&lt;PipelineBuilder&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.Pipeline-1.Build.md) | Fluent interface to build a [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md). |
| [Run(TBaseNode)](MrKWatkins.Ast.Processing.Pipeline-1.Run.md#mrkwatkins-ast-processing-pipeline-1-run(-0)) | Runs the pipeline on the specified root node. |
| [Run(TBaseNode, String)](MrKWatkins.Ast.Processing.Pipeline-1.Run.md#mrkwatkins-ast-processing-pipeline-1-run(-0-system-string@)) | Runs the pipeline on the specified root node. |

