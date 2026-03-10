# Pipeline&lt;TContext, TBaseNode&gt;.Run Method
## Overloads
| Name | Description |
| ---- | ----------- |
| [Run(TContext, TBaseNode)](MrKWatkins.Ast.Processing.Pipeline-2.Run.md#runtcontext-tbasenode) | Runs the pipeline on the specified root node, returning a tuple with the result, the potentially replaced root node and the last stage run. |
| [Run(TContext, TBaseNode, out TBaseNode)](MrKWatkins.Ast.Processing.Pipeline-2.Run.md#runtcontext-tbasenode-out-tbasenode) | Runs the pipeline on the specified root node, returning the potentially new root via an out parameter. |
| [Run(TContext, TBaseNode, out TBaseNode, out String)](MrKWatkins.Ast.Processing.Pipeline-2.Run.md#runtcontext-tbasenode-out-tbasenode-out-string) | Runs the pipeline on the specified root node, returning the potentially new root and last stage run via out parameters. |
## Run(TContext, TBaseNode) {id="runtcontext-tbasenode"}
### Definition
Runs the pipeline on the specified root node, returning a tuple with the result, the potentially replaced root node and the last stage run.
```c#
public (bool Success, TBaseNode Root, string LastStageRun) Run(TContext context, TBaseNode root);
```
### Parameters
| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| root | TBaseNode | The root node to run the pipeline on. |
### Returns
(bool Success, TBaseNode Root, [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) LastStageRun)
A tuple of whether all stages ran successfully, the root node which may have been replaced, and the name of the last stage that was run.
## Run(TContext, TBaseNode, out TBaseNode) {id="runtcontext-tbasenode-out-tbasenode"}
### Definition
Runs the pipeline on the specified root node, returning the potentially new root via an out parameter.
```c#
public bool Run(TContext context, TBaseNode root, out TBaseNode newRoot);
```
### Parameters
| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| root | TBaseNode | The root node to run the pipeline on. |
| newRoot | TBaseNode | The root node after processing, which may have been replaced by a [Replacer&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Replacer-2.md). |
### Returns
[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)
`true` if all stages ran successfully, `false` otherwise.
## Run(TContext, TBaseNode, out TBaseNode, out String) {id="runtcontext-tbasenode-out-tbasenode-out-string"}
### Definition
Runs the pipeline on the specified root node, returning the potentially new root and last stage run via out parameters.
```c#
public bool Run(TContext context, TBaseNode root, out TBaseNode newRoot, out string lastStageRun);
```
### Parameters
| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| root | TBaseNode | The root node to run the pipeline on. |
| newRoot | TBaseNode | The root node after processing, which may have been replaced by a [Replacer&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Replacer-2.md). |
| lastStageRun | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the last stage that was run. If `false` is returned then this will be the name of the stage that stopped further stages from continuing. |
### Returns
[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)
`true` if all stages ran successfully, `false` otherwise.
