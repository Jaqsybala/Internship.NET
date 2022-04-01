using Week1.TextEditorTask;
using Xunit;

namespace UnitTests.Week1.TextEditorTask.Tests
{
    public class AddTextCommandTests
    {
        [Theory]
        [InlineData("Hello", 'H')]
        [InlineData("Aziz", 'A')]
        [InlineData("Blair", 'B')]
        [InlineData("C#", 'C')]
        [InlineData("Internship", 'I')]
        public void AddTextTes(string input, char expected)
        {
            Application app = new Application();

            app.AddText(input);

            Assert.Equal(expected, app.CurrentCursor());
        }
    }
}
