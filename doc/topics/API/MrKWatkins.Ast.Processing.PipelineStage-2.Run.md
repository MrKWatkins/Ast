# PipelineStage&lt;TContext, TBaseNode&gt;.Run Method
## Overloads
| Method | Description |
| ------ | ----------- |
| [Run(TContext, TBaseNode)](MrKWatkins.Ast.Processing.PipelineStage-2.Run.md#runtcontext-tbasenode) | Runs the stage, returning a tuple of whether the pipeline should continue and the potentially replaced root node. |
| [Run(TContext, TBaseNode, out TBaseNode)](MrKWatkins.Ast.Processing.PipelineStage-2.Run.md#runtcontext-tbasenode-out-tbasenode) | Runs the stage, returning the potentially new root via an out parameter. |
## Run(TContext, TBaseNode) {id="runtcontext-tbasenode"}
### Definition
Runs the stage, returning a tuple of whether the pipeline should continue and the potentially replaced root node.
```c#
public (bool Success, TBaseNode Root) Run(TContext context, TBaseNode root);
```
### Parameters
| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| root | TBaseNode | The root node to run processing on. |
### Returns
(bool Success, TBaseNode Root)
A tuple of whether processing should continue and the root node, which may have been replaced by a [Replacer&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Replacer-2.md).
## Run(TContext, TBaseNode, out TBaseNode) {id="runtcontext-tbasenode-out-tbasenode"}
### Definition
Runs the stage, returning the potentially new root via an out parameter.
```c#
public bool Run(TContext context, TBaseNode root, out TBaseNode newRoot);
```
### Parameters
| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| root | TBaseNode | The root node to run processing on. |
| newRoot | TBaseNode | The root node after processing, which may have been replaced by a [Replacer&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Replacer-2.md). |
### Returns
[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)
`true` if the pipeline should proceed to the next stage, `false` otherwise.
