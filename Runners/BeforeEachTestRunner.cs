using System.Reflection;

public class BeforeEachTestRunner : MethodRunner<TestFrameworkAttribute> 
{
    public BeforeEachTestRunner (MethodInfo m) : base(m) {}
}
