// Administrator
// 2023-12-13 11:50:41

using System.ComponentModel.DataAnnotations;
using SesameTech.Tools.Extensions;

namespace ToolsTest.ExtensionTest;


public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var day = Day.Monday;
        var x = day.GetAttributeOfType<DisplayAttribute>();
    }
}

public enum Day
{
    [Display(Name = "周一")]
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}
