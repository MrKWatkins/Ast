# Repository conventions

- For C# changes, load and follow the `csharp-conventions` skill.

- Keep `src/Ast.slnx` and `examples/Examples.slnx` separate. Do not add the examples to the source solution; their separation is intentional.
- Build and Test validates the solutions in parallel jobs. Release validates the examples before the publishing job runs.
- Tests use NUnit on Microsoft Testing Platform. Run `dotnet test` from `src` or `examples` so the corresponding `global.json` selects the runner.
- Use `MrKWatkins.Assertions` for assertions, starting with `.Should()`.
- Keep handwritten documentation in `docs/content` and navigation in `docs/site.json`. Generated API files and site output belong under ignored `artifacts`.
- Use the shared `MrKWatkins/build/dotnet/project` action for builds and releases. NuGet publication uses trusted publishing.
- Preview documentation with `python3 docs/preview.py` or the shared JetBrains run configuration.
- Preserve the GPL-3.0-only licence. Keep workflow and tooling changes lean and consistent with the shared build setup.

- Shared C# rules, test defaults and packaging assets come from `MrKWatkins.CSharpConventions`.
- Both solutions share the source settings. Examples remain non-packable and reference the source project.
- Hard wrap C# and Markdown at 180 characters. Verify whitespace with `dotnet format whitespace` for each solution.
