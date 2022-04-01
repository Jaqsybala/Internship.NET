using Week1.TextEditorTask;
using Xunit;

namespace UnitTests.Week1.TextEditorTask.Tests
{
    public class MoveToCursorCommandTests
    {
        [Theory]
        [InlineData(0, 0, 'H')]
        [InlineData(0, 1, 'e')]
        [InlineData(0, 2, 'l')]
        [InlineData(0, 3, 'l')]
        [InlineData(0, 4, 'o')]
        [InlineData(0, 5, '!')]
        public void MoveToCursorTest(int row, int col, char expected)
        {
            Application app = new Application();

            app.AddText("Hello!");
            app.MoveToCursor(row, col);

            Assert.Equal(expected, app.CurrentCursor());
        }
    }
}
