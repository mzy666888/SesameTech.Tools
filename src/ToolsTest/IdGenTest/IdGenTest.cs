using SesameTech.Tools.IdGen.Snowflake;
using SesameTech.Tools.IdGen.Snowflake.Contract;

namespace ToolsTest.IdGenTest
{
    public class IdGenTest
    {

        [Fact]
        public void IdTest()
        {
            var options = new IdGeneratorOptions();
            IdGenHelper.SetIdGenerator(options);
            var x= IdGenHelper.NextId();
        }
    }
}
