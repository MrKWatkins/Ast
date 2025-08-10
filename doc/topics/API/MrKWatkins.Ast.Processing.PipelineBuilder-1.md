# PipelineBuilder&lt;TBaseNode&gt; Class
## Definition

Fluent builder for a pipeline stage.

```c#
public sealed class PipelineBuilder<TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [PipelineBuilder()](MrKWatkins.Ast.Processing.PipelineBuilder-1.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [AddParallelStage(Action&lt;ParallelPipelineStageBuilder&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0)))))) | Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-1.md) in parallel. Its name will be the number of the stage. |
| [AddParallelStage(IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage. |
| [AddParallelStage(String, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage. |
| [AddParallelStage(Int32, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. Its name will be the number of the stage. |
| [AddParallelStage(String, Int32, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-int32-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. |
| [AddStage(Action&lt;SerialPipelineStageBuilder&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0)))))) | Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-1.md) serially. Its name will be the number of the stage. |
| [AddStage&lt;TProcessor&gt;()](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1) | Adds a stage to the pipeline with a single [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). Its name will be the number of the stage. |
| [AddStage&lt;TProcessor&gt;(String)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)) | Adds a stage with the specified name to the pipeline with a single [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). |
| [AddStage(IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially. Its name will be the number of the stage. |
| [AddStage(String, IEnumerable&lt;Processor&lt;TBaseNode&gt;&gt;)](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-string-system-collections-generic-ienumerable((mrkwatkins-ast-processing-processor((-0)))))) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially. |

