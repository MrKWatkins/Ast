# PipelineStageBuilder&lt;TSelf, TStage, TBaseNode, TProcessor, TShouldContinue&gt;.WithDefaultTraversal Method
## Definition

The default [ITraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md) to use to walk through the tree. [OrderedProcessor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.OrderedProcessor-1.md)s will specify their own traversal to use. Defaults to [DepthFirstPreOrderTraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal-1.md)

```c#
public TSelf WithDefaultTraversal(ITraversal<TBaseNode> defaultTraversal);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| defaultTraversal | [ITraversal&lt;TBaseNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md) | The default [ITraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md). |

## Returns

TSelf

The fluent builder.
