using Week1.TextEditorTask;
using Xunit;

namespace UnitTests.Week1.TextEditorTask.Tests
{
    public class DeleteCommandTests
    {
        [Theory]
        [InlineData("Hello", 'o', 0)]
        [InlineData("Hello", 'l', 1)]
        [InlineData("Hello", 'l', 2)]
        [InlineData("Hello", 'e', 3)]
        [InlineData("Hello", 'H', 4)]
        public void DeleteCharTest(string input, char expected, int iterator)
        {
            Application app = new Application();

            app.AddText(input);
            for (int i = 0; i < (input.Length - 1) - iterator; i++)
            {
                app.DeleteChar();
            }

            Assert.Equal(expected, app.CurrentCursor());
        }
    }
}
