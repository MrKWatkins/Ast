# PipelineStageBuilder&lt;TSelf, TStage, TBaseNode, TProcessor, TShouldContinue&gt; Class
## Definition

Fluent builder for a pipeline stage.

```c#
public abstract class PipelineStageBuilder<TSelf, TStage, TBaseNode, TProcessor, TShouldContinue>
   where TSelf : PipelineStageBuilder<TSelf, TStage, TBaseNode, TProcessor, TShouldContinue>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TSelf |  |
| TStage |  |
| TBaseNode |  |
| TProcessor |  |
| TShouldContinue |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Add&lt;TConstructableProcessor&gt;()](MrKWatkins.Ast.Processing.PipelineStageBuilder-5.Add.md#mrkwatkins-ast-processing-pipelinestagebuilder-5-add-1) | Adds a [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) of the specified type to the current stage. |
| [Add(IEnumerable&lt;TProcessor&gt;)](MrKWatkins.Ast.Processing.PipelineStageBuilder-5.Add.md#mrkwatkins-ast-processing-pipelinestagebuilder-5-add(system-collections-generic-ienumerable((-3)))) | Adds [Processors](MrKWatkins.Ast.Processing.Processor-1.md) to the current stage. |
| [WithAlwaysContinue()](MrKWatkins.Ast.Processing.PipelineStageBuilder-5.WithAlwaysContinue.md) | Specifies that processing should always continue after this stage. By default, processing will not continue if there are any errors in the tree. |
| [WithDefaultTraversal(ITraversal&lt;TBaseNode&gt;)](MrKWatkins.Ast.Processing.PipelineStageBuilder-5.WithDefaultTraversal.md) | The default [ITraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md) to use to walk through the tree. [OrderedProcessor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.OrderedProcessor-1.md)s will specify their own traversal to use. Defaults to [DepthFirstPreOrderTraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal-1.md) |
| [WithName(String)](MrKWatkins.Ast.Processing.PipelineStageBuilder-5.WithName.md) | Sets the name of the stage. |
| [WithShouldContinue(TShouldContinue)](MrKWatkins.Ast.Processing.PipelineStageBuilder-5.WithShouldContinue.md) | Sets a function to determine whether processing should continue after this stage or not. Any previously registered function will be replaced. By default, processing will not continue if there are any errors in the tree. |

