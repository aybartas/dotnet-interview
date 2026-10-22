// A file that DELIBERATELY contains style/analyzer warnings and FORMATTING errors.
//   dotnet build                           → IDE0005, IDE0044, IDE0051, IDE1006, CA1861 warnings (CA1822 is only suggested in the IDE)
//   dotnet format --verify-no-changes      → fails if there are formatting differences (used as a CI gate)
//   dotnet format                          → fixes formatting problems (MODIFIES THE FILE; see it with git diff)
//   dotnet format analyzers --severity warn → applies analyzer fixes
//
// Once you fix them you will have cleaned up everything that is "deliberate". To go back to the original: git checkout -- StyleViolations.cs

using System.Text;          // IDE0005: unused using
using System.Net.Http;      // IDE0005 (ImplicitUsings already adds it → redundant)

namespace Junior.DevFoundations.IdeDebugging.AnalyzersNullable;

public class StyleViolations
{
private int counter = 0;                       // IDE0044 (could be readonly) + IDE1006 (_camelCase expected) + IDE0051 (unused) + wrong indentation
    private string unusedField = "x";          // IDE0051 + IDE1006

    public bool IsAllowed(string role)
    {
        // CA1861: a new array is allocated on every call → hoist it to a static readonly field
        return Array.IndexOf(new[] { "admin", "editor" }, role) >= 0;
    }

    // CA1822: doesn't use instance data → could be 'static'
    public int Double(int x)
    {
        if(x>10)               // formatting: missing 'if (' and operator spacing
        {return x*2;}          // formatting: brace placement/spacing
        else
        {
            return x   *   2;
        }
    }
}
