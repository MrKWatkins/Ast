# Pipeline&lt;TNode&gt;.Run Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Run](MrKWatkins.Ast.Processing.Pipeline-1.Run.md#mrkwatkins-ast-processing-pipeline-1-run(-0)) | Runs the pipeline on the specified root node. |
| [Run](MrKWatkins.Ast.Processing.Pipeline-1.Run.md#mrkwatkins-ast-processing-pipeline-1-run(-0-system-string@)) | Runs the pipeline on the specified root node. |

## Run(TNode) {id="mrkwatkins-ast-processing-pipeline-1-run(-0)"}

Runs the pipeline on the specified root node.

```c#
public bool Run(TNode root);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipeline-1-run(-0)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TNode | The root node to run the pipeline on. |

## Returns {id="returns-mrkwatkins-ast-processing-pipeline-1-run(-0)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if all stages ran successfully, `false` otherwise.
## Run(TNode, string) {id="mrkwatkins-ast-processing-pipeline-1-run(-0-system-string@)"}

Runs the pipeline on the specified root node.

```c#
public bool Run(TNode root, out string lastStageRan);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipeline-1-run(-0-system-string@)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TNode | The root node to run the pipeline on. |
| lastStageRan | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the last stage that was ran. If `false` is returned then this will be the name of the stage that stopped further stages from continuing. |

## Returns {id="returns-mrkwatkins-ast-processing-pipeline-1-run(-0-system-string@)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if all stages ran successfully, `false` otherwise.
