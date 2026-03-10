# PipelineStage&lt;TBaseNode&gt;.Run Method
## Overloads
| Method | Description |
| ------ | ----------- |
| [Run(TBaseNode)](MrKWatkins.Ast.Processing.PipelineStage-1.Run.md#runtbasenode) | Runs the stage, returning a tuple of whether the pipeline should continue and the potentially replaced root node. |
| [Run(TBaseNode, out TBaseNode)](MrKWatkins.Ast.Processing.PipelineStage-1.Run.md#runtbasenode-out-tbasenode) | Runs the stage, returning the potentially new root via an out parameter. |
## Run(TBaseNode) {id="runtbasenode"}
### Definition
Runs the stage, returning a tuple of whether the pipeline should continue and the potentially replaced root node.
```c#
public (bool Success, TBaseNode Root) Run(TBaseNode root);
```
### Parameters
| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TBaseNode | The root node to run processing on. |
### Returns
(bool Success, TBaseNode Root)
A tuple of whether processing should continue and the root node, which may have been replaced by a [Replacer&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Replacer-1.md).
## Run(TBaseNode, out TBaseNode) {id="runtbasenode-out-tbasenode"}
### Definition
Runs the stage, returning the potentially new root via an out parameter.
```c#
public bool Run(TBaseNode root, out TBaseNode newRoot);
```
### Parameters
| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TBaseNode | The root node to run processing on. |
| newRoot | TBaseNode | The root node after processing, which may have been replaced by a [Replacer&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Replacer-1.md). |
### Returns
[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)
`true` if the pipeline should proceed to the next stage, `false` otherwise.
