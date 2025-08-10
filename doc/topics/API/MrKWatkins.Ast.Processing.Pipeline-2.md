# Pipeline&lt;TContext, TBaseNode&gt; Class
## Definition

A pipeline to process nodes in a tree with a context. A pipeline consists of multiple named stages, each of which has one or more [Processor&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-2.md)s running in serial or parallel. Stages can optionally specify whether pipeline processing should continue once the stage has completed. By default, processing will not continue if there are any errors in the tree.

```c#
public sealed class Pipeline<TContext, TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the context. |
| TBaseNode | The base type of nodes in the tree. |

## Properties

| Name | Description |
| ---- | ----------- |
| [Stages](MrKWatkins.Ast.Processing.Pipeline-2.Stages.md) | The stages in the pipeline. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Build(Action&lt;PipelineBuilder&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.Pipeline-2.Build.md) | Fluent interface to build a [Pipeline&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-2.md). |
| [Run(TContext, TBaseNode)](MrKWatkins.Ast.Processing.Pipeline-2.Run.md#mrkwatkins-ast-processing-pipeline-2-run(-0-1)) | Runs the pipeline on the specified root node. |
| [Run(TContext, TBaseNode, String)](MrKWatkins.Ast.Processing.Pipeline-2.Run.md#mrkwatkins-ast-processing-pipeline-2-run(-0-1-system-string@)) | Runs the pipeline on the specified root node. |

