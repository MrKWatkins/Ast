# PipelineBuilder&lt;TNode&gt; Class
## Definition

Fluent builder for a pipeline stage.

```c#
public sealed class PipelineBuilder<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode |  |

## Constructors

| Name | Description |
| ---- | ----------- |
| [PipelineBuilder()](MrKWatkins.Ast.Processing.PipelineBuilder-1.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [AddParallelStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0)))))) | Adds a stage to the pipeline that runs [UnorderedProcessors](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) in parallel. Its name will be the number of the stage. |
| [AddParallelStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage. |
| [AddParallelStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel. Its name will be the number of the stage. |
| [AddParallelStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-int32-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. Its name will be the number of the stage. |
| [AddParallelStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-string-system-int32-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))-mrkwatkins-ast-processing-unorderedprocessor((-0))())) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run in parallel with the specified maximum degree of parallelism. |
| [AddStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0)))))) | Adds a stage to the pipeline that runs [Processors](MrKWatkins.Ast.Processing.Processor-1.md) serially. Its name will be the number of the stage. |
| [AddStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1) | Adds a stage to the pipeline with a single [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). Its name will be the number of the stage. |
| [AddStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage-1(system-string)) | Adds a stage with the specified name to the pipeline with a single [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md). |
| [AddStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(mrkwatkins-ast-processing-processor((-0))-mrkwatkins-ast-processing-processor((-0))())) | Adds a stage to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially. Its name will be the number of the stage. |
| [AddStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-string-mrkwatkins-ast-processing-processor((-0))-mrkwatkins-ast-processing-processor((-0))())) | Adds a stage with the specified name to the pipeline with the specified [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to be run serially. |

