# PipelineStageBuilder&lt;TSelf, TProcessor, TNode&gt; Class
## Definition

Fluent builder for a pipeline stage.

```c#
public abstract class PipelineStageBuilder<TSelf, TProcessor, TNode>
   where TSelf : PipelineStageBuilder<TSelf, TProcessor, TNode>
   where TProcessor : Processor<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TSelf |  |
| TProcessor |  |
| TNode |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Add&lt;TConstructableProcessor&gt;()](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.Add.md#mrkwatkins-ast-processing-pipelinestagebuilder-3-add-1) | Adds a [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) of the specified type to the current stage. |
| [Add(TProcessor, TProcessor\[\])](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.Add.md#mrkwatkins-ast-processing-pipelinestagebuilder-3-add(-1-1())) | Adds [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to the current stage. |
| [WithAlwaysContinue()](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.WithAlwaysContinue.md) | Specifies that processing should always continue after this stage. By default, processing will not continue if there are any errors in the tree. |
| [WithName(String)](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.WithName.md) | Sets the name of the stage. |
| [WithShouldContinue(Func&lt;TNode, Boolean&gt;)](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.WithShouldContinue.md) | Sets a function to determine whether processing should continue after this stage or not. Any previously registered function will be replaced. By default, processing will not continue if there are any errors in the tree. |

