# PipelineStageBuilder&lt;TSelf, TProcessor, TNode&gt;.WithAlwaysContinue Method
## Definition

Specifies that processing should always continue after this stage. By default, processing will not continue if there are any errors in the tree.

```c#
public TSelf WithAlwaysContinue();
```

## Returns

TSelf

The fluent builder.
