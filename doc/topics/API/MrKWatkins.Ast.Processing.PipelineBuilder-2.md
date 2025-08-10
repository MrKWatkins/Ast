# PipelineBuilder&lt;TContext, TBaseNode&gt; Class
## Definition

Fluent builder for a pipeline stage.

```c#
public sealed class PipelineBuilder<TContext, TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the context. |
| TBaseNode | The base type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [PipelineBuilder()](MrKWatkins.Ast.Processing.PipelineBuilder-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [AddParallelStage(Action&lt;ParallelPipelineStageBuilder&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0-1)))))) | Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-2.md) in parallel. Its name will be the number of the stage. |
| [AddParallelStage(IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addparallelstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-2.md) to be run in parallel. Its name will be the number of the stage. |
| [AddParallelStage(String, IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addparallelstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-2.md) to be run in parallel. Its name will be the number of the stage. |
| [AddParallelStage(Int32, IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addparallelstage(system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. Its name will be the number of the stage. |
| [AddParallelStage(String, Int32, IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addparallelstage(system-string-system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. |
| [AddStage(Action&lt;SerialPipelineStageBuilder&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0-1)))))) | Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-2.md) serially. Its name will be the number of the stage. |
| [AddStage&lt;TProcessor&gt;()](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addstage-1) | Adds a stage to the pipeline with a single [Processor&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-2.md). Its name will be the number of the stage. |
| [AddStage&lt;TProcessor&gt;(String)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addstage-1(system-string)) | Adds a stage with the specified name to the pipeline with a single [Processor&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-2.md). |
| [AddStage(IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-2.md) to be run serially. Its name will be the number of the stage. |
| [AddStage(String, IEnumerable&lt;Processor&lt;TContext, TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-2.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-2-addstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0-1)))))) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-2.md) to be run serially. |

