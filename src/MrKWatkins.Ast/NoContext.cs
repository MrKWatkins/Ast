namespace MrKWatkins.Ast;

/// <summary>
/// The context type used by the listeners, visitors and processors that do not take a context object. It has no state; it exists so that
/// the context-free forms of those types can share the implementation of the forms that do take a context.
/// </summary>
public readonly struct NoContext;