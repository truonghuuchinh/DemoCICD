using System.Reflection;

namespace DemoCICD.API;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
